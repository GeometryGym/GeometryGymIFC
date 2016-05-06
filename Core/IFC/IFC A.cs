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
	public class IfcActionRequest : IfcControl
	{
		//internal string mRequestID;// : IfcIdentifier; IFC4 relocated
		internal IfcActionRequest(IfcActionRequest i) : base(i) { }
		internal IfcActionRequest() : base() { }
		internal static IfcActionRequest Parse(string strDef,Schema schema) { IfcActionRequest r = new IfcActionRequest(); int ipos = 0; parseFields(r, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return r; }
		internal static void parseFields(IfcActionRequest r,List<string> arrFields, ref int ipos,Schema schema) 
		{ 
			IfcControl.parseFields(r,arrFields, ref ipos,schema);
			if(schema == Schema.IFC2x3)
				r.mIdentification = arrFields[ipos++].Replace("'","");
		}
		protected override string BuildString() { return base.BuildString() + (mDatabase.mSchema == Schema.IFC2x3 ? ",'" + mIdentification + "'" : ""); }
	}
	public class IfcActor : IfcObject //	SUPERTYPE OF(IfcOccupant)
	{
		internal int mTheActor;//	 :	IfcActorSelect; 
		//INVERSE
	    internal List<IfcRelAssignsToActor> mIsActingUpon = new List<IfcRelAssignsToActor>();// : SET [0:?] OF IfcRelAssignsToActor FOR RelatingActor;

		internal IfcActor(IfcActor o) : base(o) { }
		internal IfcActor() : base() { }
		internal IfcActor(IfcActorSelect a) : base(a.Database)
		{
 			if(mDatabase.mModelView != ModelView.If2x3NotAssigned && mDatabase.mModelView != ModelView.Ifc4NotAssigned )
				throw new Exception("Invalid Model View for IfcActor : " + mDatabase.ModelView.ToString());
			mTheActor = a.Index; 
		}
		internal static IfcActor Parse(string strDef) { IfcActor a = new IfcActor(); int ipos = 0; parseFields(a, ParserSTEP.SplitLineFields(strDef), ref ipos); return a; }
		internal static void parseFields(IfcActor a, List<string> arrFields, ref int ipos) { IfcObject.parseFields(a, arrFields, ref ipos); a.mTheActor = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.LinkToString(mTheActor); }
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
		internal IfcActorRole(IfcActorRole o) : base() { mRole = o.mRole; mDescription = o.mDescription; mUserDefinedRole = o.mUserDefinedRole; }
		
		internal static void parseFields(IfcActorRole a,List<string> arrFields, ref int ipos)
		{ 
			string str = arrFields[ipos++];
			if (str != "$")
				a.mRole = (string.Compare(str, "COMISSIONINGENGINEER", true) == 0 ? IfcRoleEnum.COMMISSIONINGENGINEER : (IfcRoleEnum)Enum.Parse(typeof(IfcRoleEnum), str.Replace(".", "")));
			a.mUserDefinedRole = arrFields[ipos++].Replace("'","");
			a.mDescription = arrFields[ipos++].Replace("'",""); 
		}
		internal static IfcActorRole Parse(string strDef) { IfcActorRole a = new IfcActorRole(); int ipos = 0; parseFields(a, ParserSTEP.SplitLineFields(strDef), ref ipos); return a; }
		protected override string BuildString() { return base.BuildString() + ",." + (mDatabase.mSchema == Schema.IFC2x3 && mRole == IfcRoleEnum.COMMISSIONINGENGINEER ? "COMISSIONINGENGINEER" : mRole.ToString()) + (mUserDefinedRole == "$" ? ".,$," : ".,'" + mUserDefinedRole + "',") + (mDescription == "$" ? "$" : "'" + mDescription + "'") ; }
	}
	public interface IfcActorSelect : IfcInterface {  }// IfcOrganization,  IfcPerson,  IfcPersonAndOrganization);
	public partial class IfcActuator : IfcDistributionControlElement //IFC4  
	{   
		public override string KeyWord { get { return mDatabase.mSchema == Schema.IFC2x3 ? base.KeyWord : "IFCACTUATOR"; } }

		internal IfcActuatorTypeEnum mPredefinedType = IfcActuatorTypeEnum.NOTDEFINED;
		public IfcActuatorTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcActuator() : base() { }
		internal IfcActuator(IfcActuator a) : base(a) { mPredefinedType = a.mPredefinedType; }
		internal IfcActuator(IfcProduct host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement,representation, system) { }

		internal static void parseFields(IfcActuator a, List<string> arrFields, ref int ipos)
		{ 
			IfcDistributionControlElement.parseFields(a, arrFields, ref ipos);
			string s = arrFields[ipos++];
			if (s.StartsWith("."))
				a.mPredefinedType = (IfcActuatorTypeEnum)Enum.Parse(typeof(IfcActuatorTypeEnum), s.Replace(".", ""));
		}
		internal new static IfcActuator Parse(string strDef) { IfcActuator d = new IfcActuator(); int ipos = 0; parseFields(d, ParserSTEP.SplitLineFields(strDef), ref ipos); return d; }
		protected override string BuildString()
		{
			return base.BuildString() + (mDatabase.mSchema == Schema.IFC2x3 ? "" : (mPredefinedType == IfcActuatorTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + "."));
		}
	}
	public partial class IfcActuatorType : IfcDistributionControlElementType
	{
		internal IfcActuatorTypeEnum mPredefinedType = IfcActuatorTypeEnum.NOTDEFINED;// : IfcActuatorTypeEnum; 
		public IfcActuatorTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcActuatorType() : base() { }
		internal IfcActuatorType(IfcActuatorType t) : base(t)  { mPredefinedType = t.mPredefinedType; }
		internal IfcActuatorType(DatabaseIfc m, string name, IfcActuatorTypeEnum t) : base(m) { Name = name; mPredefinedType = t; }
		internal static void parseFields(IfcActuatorType t,List<string> arrFields, ref int ipos) { IfcDistributionControlElementType.parseFields(t,arrFields, ref ipos); t.mPredefinedType = (IfcActuatorTypeEnum)Enum.Parse(typeof(IfcActuatorTypeEnum),arrFields[ipos++].Replace(".","")); }
		internal new static IfcActuatorType Parse(string strDef) { IfcActuatorType t = new IfcActuatorType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildString() { return base.BuildString() + ",." + mPredefinedType.ToString() + "."; }
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
		protected IfcAddress(IfcAddress o) : base()
		{
			mPurpose = o.mPurpose;
			mDescription = o.mDescription;
			mUserDefinedPurpose = o.mUserDefinedPurpose;
		}
		protected IfcAddress(DatabaseIfc m, IfcAddressTypeEnum purpose) : base(m) { mPurpose = purpose; }
		internal static void parseFields(IfcAddress a, List<string> arrFields, ref int ipos)
		{
			string str = arrFields[ipos++];
			if (str != "$")
				a.mPurpose = (IfcAddressTypeEnum)Enum.Parse(typeof(IfcAddressTypeEnum), str.Replace(".", ""));
			a.mDescription = arrFields[ipos++].Replace("'", "");
			a.mUserDefinedPurpose = arrFields[ipos++].Replace("'", "");
		}
		protected override string BuildString() { return base.BuildString() + (mPurpose == IfcAddressTypeEnum.NOTDEFINED ? ",$," : ",." + mPurpose.ToString() + ".,") + (mDescription == "$" ? "$," : "'" + mDescription + "',") + (mUserDefinedPurpose == "$" ? "$" : "'" + mUserDefinedPurpose + "'"); }
	}
	public partial class IfcAdvancedBrep : IfcManifoldSolidBrep // IFC4
	{
		internal IfcAdvancedBrep() : base() { }
		internal IfcAdvancedBrep(IfcAdvancedBrep p) : base(p) { }
		public IfcAdvancedBrep(List<IfcAdvancedFace> faces) : base(new IfcClosedShell(faces.ConvertAll(x => (IfcFace)x))) { }
		internal IfcAdvancedBrep(IfcClosedShell s) : base(s) { }

		internal static IfcAdvancedBrep Parse(string strDef) { IfcAdvancedBrep b = new IfcAdvancedBrep(); int ipos = 0; parseFields(b, ParserSTEP.SplitLineFields(strDef), ref ipos); return b; }
		internal static void parseFields(IfcAdvancedBrep b, List<string> arrFields, ref int ipos) { IfcManifoldSolidBrep.parseFields(b, arrFields, ref ipos); }
		protected override string BuildString() { return (mDatabase.mSchema == Schema.IFC2x3 ? "" : base.BuildString()); }
	}
	public partial class IfcAdvancedBrepWithVoids : IfcAdvancedBrep
	{
		private List<int> mVoids = new List<int>();// : SET [1:?] OF IfcClosedShell

		internal List<IfcClosedShell> Voids { get { return mVoids.ConvertAll(x => mDatabase.mIfcObjects[x] as IfcClosedShell); } }
		internal IfcAdvancedBrepWithVoids() : base() { }
		internal IfcAdvancedBrepWithVoids(IfcAdvancedBrepWithVoids p) : base((IfcAdvancedBrep)p) { mVoids = new List<int>(p.mVoids.ToArray()); }

		internal new static IfcAdvancedBrepWithVoids Parse(string strDef) { IfcAdvancedBrepWithVoids b = new IfcAdvancedBrepWithVoids(); int ipos = 0; parseFields(b, ParserSTEP.SplitLineFields(strDef), ref ipos); return b; }
		internal static void parseFields(IfcAdvancedBrepWithVoids b, List<string> arrFields, ref int ipos) { IfcAdvancedBrep.parseFields(b, arrFields, ref ipos); b.mVoids = ParserSTEP.SplitListLinks(arrFields[ipos++]); }
		protected override string BuildString()
		{
			string str = base.BuildString() + ",(";
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
		internal IfcAdvancedFace(IfcAdvancedFace i) : base(i) { }
		public IfcAdvancedFace(IfcFaceOuterBound bound, IfcSurface f, bool sense) : base(bound, f, sense) { }
		public IfcAdvancedFace(List<IfcFaceBound> bounds, IfcSurface f, bool sense) : base(bounds, f, sense) { }
		public IfcAdvancedFace(IfcFaceOuterBound outer, IfcFaceBound inner, IfcSurface f, bool sense) : base(outer,inner, f, sense) { }
		internal new static IfcAdvancedFace Parse(string strDef) { IfcAdvancedFace f = new IfcAdvancedFace(); int ipos = 0; parseFields(f, ParserSTEP.SplitLineFields(strDef), ref ipos); return f; }
		internal static void parseFields(IfcAdvancedFace f, List<string> arrFields, ref int ipos) { IfcFaceSurface.parseFields(f, arrFields, ref ipos); }
		protected override string BuildString() { return (mDatabase.mSchema == Schema.IFC2x3 ? "" : base.BuildString()); }
	}
	public class IfcAirTerminal : IfcFlowTerminal //IFC4
	{
		internal IfcAirTerminalTypeEnum mPredefinedType = IfcAirTerminalTypeEnum.NOTDEFINED;// OPTIONAL : IfcAirTerminalTypeEnum;
		public IfcAirTerminalTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcAirTerminal() : base() { }
		internal IfcAirTerminal(IfcAirTerminal t) : base(t) { mPredefinedType = t.mPredefinedType; }
		public IfcAirTerminal(IfcProduct host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
		internal static void parseFields(IfcAirTerminal s, List<string> arrFields, ref int ipos)
		{
			IfcFlowTerminal.parseFields(s, arrFields, ref ipos); 
			string str = arrFields[ipos++];
			if (str[0] == '.')
				s.mPredefinedType = (IfcAirTerminalTypeEnum)Enum.Parse(typeof(IfcAirTerminalTypeEnum), str.Substring(1, str.Length - 2));
		}
		internal new static IfcAirTerminal Parse(string strDef) { IfcAirTerminal s = new IfcAirTerminal(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildString() { return base.BuildString() + (mDatabase.mSchema == Schema.IFC2x3 ? "" : (mPredefinedType == IfcAirTerminalTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }

	}
	public class IfcAirTerminalBox : IfcFlowController //IFC4
	{
		internal IfcAirTerminalBoxTypeEnum mPredefinedType = IfcAirTerminalBoxTypeEnum.NOTDEFINED;// OPTIONAL : IfcAirTerminalBoxTypeEnum;
		public IfcAirTerminalBoxTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcAirTerminalBox() : base() { }
		internal IfcAirTerminalBox(IfcAirTerminalBox b) : base(b) { mPredefinedType = b.mPredefinedType; }
		public IfcAirTerminalBox(IfcProduct host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }

		internal static void parseFields(IfcAirTerminalBox s, List<string> arrFields, ref int ipos)
		{
			IfcEnergyConversionDevice.parseFields(s, arrFields, ref ipos);
			string str = arrFields[ipos++];
			if (str[0] == '.')
				s.mPredefinedType = (IfcAirTerminalBoxTypeEnum)Enum.Parse(typeof(IfcAirTerminalBoxTypeEnum), str);
		}
		internal new static IfcAirTerminalBox Parse(string strDef) { IfcAirTerminalBox s = new IfcAirTerminalBox(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildString()
		{
			return base.BuildString() + (mDatabase.mSchema == Schema.IFC2x3 ? "" : (mPredefinedType == IfcAirTerminalBoxTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + "."));
		}
	}
	public class IfcAirTerminalBoxType : IfcFlowControllerType
	{
		internal IfcAirTerminalBoxTypeEnum mPredefinedType = IfcAirTerminalBoxTypeEnum.NOTDEFINED;// : IfcAirTerminalBoxTypeEnum; 
		public IfcAirTerminalBoxTypeEnum PredefinedType { get { return mPredefinedType;} set { mPredefinedType = value; } }

		internal IfcAirTerminalBoxType() : base() { }
		internal IfcAirTerminalBoxType(IfcAirTerminalBoxType t) : base(t) { mPredefinedType = t.mPredefinedType; }
		internal IfcAirTerminalBoxType(DatabaseIfc m, string name, IfcAirTerminalBoxTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
		internal static void parseFields(IfcAirTerminalBoxType  t,List<string> arrFields, ref int ipos) { IfcFlowControllerType.parseFields(t,arrFields, ref ipos); t.mPredefinedType = (IfcAirTerminalBoxTypeEnum)Enum.Parse(typeof(IfcAirTerminalBoxTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcAirTerminalBoxType Parse(string strDef) { IfcAirTerminalBoxType t = new IfcAirTerminalBoxType(); int ipos = 0; parseFields(t,ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildString() { return base.BuildString() + ",." + mPredefinedType.ToString() + "."; }
	}
	public partial class IfcAirTerminalType : IfcFlowTerminalType
	{
		internal IfcAirTerminalTypeEnum mPredefinedType = IfcAirTerminalTypeEnum.NOTDEFINED;// : IfcAirTerminalBoxTypeEnum; 
		public IfcAirTerminalTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcAirTerminalType(IfcAirTerminalType t) : base(t) { mPredefinedType = t.mPredefinedType; }
		internal IfcAirTerminalType() : base() { }
		public IfcAirTerminalType(DatabaseIfc m, string name, IfcAirTerminalTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }

		internal static void parseFields(IfcAirTerminalType t,List<string> arrFields, ref int ipos) { IfcFlowControllerType.parseFields(t,arrFields, ref ipos); t.mPredefinedType = (IfcAirTerminalTypeEnum)Enum.Parse(typeof(IfcAirTerminalTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcAirTerminalType Parse(string strDef) { IfcAirTerminalType t = new IfcAirTerminalType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildString() { return base.BuildString() + ",." + mPredefinedType.ToString() + "."; }
	}
	public class IfcAirToAirHeatRecovery : IfcEnergyConversionDevice //IFC4  
	{
		internal IfcAirToAirHeatRecoveryTypeEnum mPredefinedType = IfcAirToAirHeatRecoveryTypeEnum.NOTDEFINED;
		public IfcAirToAirHeatRecoveryTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcAirToAirHeatRecovery() : base() { }
		internal IfcAirToAirHeatRecovery(IfcAirToAirHeatRecovery a) : base(a) { mPredefinedType = a.mPredefinedType; }
		internal IfcAirToAirHeatRecovery(IfcProduct host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
		internal static void parseFields(IfcAirToAirHeatRecovery a, List<string> arrFields, ref int ipos)
		{
			IfcDistributionControlElement.parseFields(a, arrFields, ref ipos);
			string s = arrFields[ipos++];
			if (s.StartsWith("."))
				a.mPredefinedType = (IfcAirToAirHeatRecoveryTypeEnum)Enum.Parse(typeof(IfcAirToAirHeatRecoveryTypeEnum), s.Replace(".", ""));
		}
		internal new static IfcAirToAirHeatRecovery Parse(string strDef) { IfcAirToAirHeatRecovery d = new IfcAirToAirHeatRecovery(); int ipos = 0; parseFields(d, ParserSTEP.SplitLineFields(strDef), ref ipos); return d; }
		protected override string BuildString()
		{
			return base.BuildString() + (mPredefinedType == IfcAirToAirHeatRecoveryTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".");
		}
	}
	public class IfcAirToAirHeatRecoveryType : IfcEnergyConversionDeviceType
	{
		internal IfcAirToAirHeatRecoveryTypeEnum mPredefinedType = IfcAirToAirHeatRecoveryTypeEnum.NOTDEFINED;// : IfcAirToAirHeatRecoveryTypeEnum; 
		public IfcAirToAirHeatRecoveryTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcAirToAirHeatRecoveryType(IfcAirToAirHeatRecoveryType be) : base(be) { mPredefinedType = be.mPredefinedType; }
		internal IfcAirToAirHeatRecoveryType() : base() { }
		internal IfcAirToAirHeatRecoveryType(DatabaseIfc m, string name, IfcAirToAirHeatRecoveryTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
		internal static void parseFields(IfcAirToAirHeatRecoveryType t,List<string> arrFields, ref int ipos) {  IfcEnergyConversionDeviceType.parseFields(t,arrFields, ref ipos); t.mPredefinedType = (IfcAirToAirHeatRecoveryTypeEnum)Enum.Parse(typeof(IfcAirToAirHeatRecoveryTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcAirToAirHeatRecoveryType Parse(string strDef) { IfcAirToAirHeatRecoveryType t = new IfcAirToAirHeatRecoveryType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildString() { return base.BuildString() + ",." + mPredefinedType.ToString() + "."; }
	}
	public class IfcAlarm : IfcDistributionControlElement //IFC4  
	{
		internal IfcAlarmTypeEnum mPredefinedType = IfcAlarmTypeEnum.NOTDEFINED;
		internal IfcAlarmTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcAlarm() : base() { }
		internal IfcAlarm(IfcAlarm a) : base(a) { mPredefinedType = a.mPredefinedType; }
		internal IfcAlarm(IfcProduct host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }

		internal static void parseFields(IfcAlarm a, List<string> arrFields, ref int ipos) 
		{ 
			IfcDistributionControlElement.parseFields(a, arrFields, ref ipos);
			string s = arrFields[ipos++];
			if (s.StartsWith("."))
				a.mPredefinedType = (IfcAlarmTypeEnum)Enum.Parse(typeof(IfcAlarmTypeEnum), s.Replace(".", ""));
		}
		internal new static IfcAlarm Parse(string strDef) { IfcAlarm a = new IfcAlarm(); int ipos = 0; parseFields(a, ParserSTEP.SplitLineFields(strDef), ref ipos); return a; }
		protected override string BuildString()
		{
			return base.BuildString() + (mDatabase.mSchema == Schema.IFC2x3 ? "" : (mPredefinedType == IfcAlarmTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + "."));
		}
	}
	public class IfcAlarmType : IfcDistributionControlElementType
	{
		internal IfcAlarmTypeEnum mPredefinedType = IfcAlarmTypeEnum.NOTDEFINED;// : IfcAlarmTypeEnum; 
		public IfcAlarmTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcAlarmType(IfcAlarmType t) : base(t) { mPredefinedType = t.mPredefinedType; }
		internal IfcAlarmType() : base() { }
		internal IfcAlarmType(DatabaseIfc m, string name, IfcAlarmTypeEnum t) : base(m) { Name = name; mPredefinedType = t; }
		internal static void parseFields(IfcAlarmType t,List<string> arrFields, ref int ipos) { IfcDistributionControlElementType.parseFields(t,arrFields, ref ipos); t.mPredefinedType = (IfcAlarmTypeEnum)Enum.Parse(typeof(IfcAlarmTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcAlarmType Parse(string strDef) { IfcAlarmType t = new IfcAlarmType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildString() { return base.BuildString() + ",." + mPredefinedType.ToString() + "."; }
	}
	public partial class IfcAlignment : IfcPositioningElement //IFC4.1
	{
		internal IfcAlignmentTypeEnum mPredefinedType;// : OPTIONAL IfcAlignmentTypeEnum;
		public IfcAlignmentTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal int mHorizontal = 0;// : OPTIONAL IfcAlignment2DHorizontal;
		internal int mVertical = 0;// : OPTIONAL IfcAlignment2DVertical;
		internal string mLinearRefMethod = "$";// : OPTIONAL IfcLabel;

		internal IfcAlignment() : base() { }
		internal IfcAlignment(IfcAlignment a) : base(a) { mPredefinedType = a.mPredefinedType; mLinearRefMethod = a.mLinearRefMethod; }
		internal static void parseFields(IfcAlignment a, List<string> arrFields, ref int ipos) 
		{ 
			IfcPositioningElement.parseFields(a, arrFields, ref ipos);
			string str = arrFields[ipos++];
			if(str != "$")
				a.mPredefinedType = (IfcAlignmentTypeEnum)Enum.Parse(typeof(IfcAlignmentTypeEnum), str.Replace(".", ""));
			a.mHorizontal = ParserSTEP.ParseLink(arrFields[ipos++]);
			a.mVertical = ParserSTEP.ParseLink(arrFields[ipos++]);
			a.mLinearRefMethod = arrFields[ipos++].Replace("'", "");
		}
		internal static IfcAlignment Parse(string strDef) { IfcAlignment a = new IfcAlignment(); int ipos = 0; parseFields(a, ParserSTEP.SplitLineFields(strDef), ref ipos); return a; }
		protected override string BuildString() 
		{ 
			return base.BuildString() + (mPredefinedType== IfcAlignmentTypeEnum.NOTDEFINED ? ",$," : ",." + mPredefinedType.ToString() + ".,") + 
				ParserSTEP.LinkToString(mHorizontal) + "," + ParserSTEP.LinkToString(mVertical) + "," +	( mLinearRefMethod == "$" ? "$" : "'" + mLinearRefMethod + "'");
		}
	}
	public partial class IfcAlignment2DHorizontal : BaseClassIfc //IFC4.1
	{
		internal double mStartDistAlong;// : OPTIONAL IfcLengthMeasure;
		internal List<int> mSegments = new List<int>();// : LIST [1:?] OF IfcAlignment2DHorizontalSegment;

		internal IfcAlignment2DHorizontal() : base() { }
		internal IfcAlignment2DHorizontal(IfcAlignment2DHorizontal a) : base() { mStartDistAlong = a.mStartDistAlong; mSegments = new List<int>(a.mSegments.ToArray()); }
		internal static void parseFields(IfcAlignment2DHorizontal a, List<string> arrFields, ref int ipos)
		{
			a.mStartDistAlong = ParserSTEP.ParseDouble(arrFields[ipos++]);
			a.mSegments = ParserSTEP.SplitListLinks(arrFields[ipos++]); 
		}
		protected override string BuildString()
		{
			string str = base.BuildString() + "," + ParserSTEP.DoubleOptionalToString(mStartDistAlong) + "," + ParserSTEP.LinkToString(mSegments[0]);
			for (int icounter = 1; icounter < mSegments.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mSegments[icounter]);
			return str + ")";
		}
		internal static IfcAlignment2DHorizontal Parse(string strDef) { IfcAlignment2DHorizontal a = new IfcAlignment2DHorizontal(); int ipos = 0; parseFields(a, ParserSTEP.SplitLineFields(strDef), ref ipos); return a; }
	}
	public partial class IfcAlignment2DHorizontalSegment : IfcAlignment2DSegment //IFC4.1
	{
		internal int mCurveGeometry;// : IfcCurveSegment2D;

		public IfcCurveSegment2D CurveGeometry { get { return mDatabase.mIfcObjects[mCurveGeometry] as IfcCurveSegment2D; } }

		internal IfcAlignment2DHorizontalSegment() : base() { }
		internal IfcAlignment2DHorizontalSegment(IfcAlignment2DHorizontalSegment p) : base(p) { mCurveGeometry = p.mCurveGeometry; }
		internal IfcAlignment2DHorizontalSegment(bool tangential, string startTag, string endTag, IfcCurveSegment2D seg) : base(seg.mDatabase, tangential, startTag, endTag) { mCurveGeometry = seg.mIndex; }	
		internal static void parseFields(IfcAlignment2DHorizontalSegment s, List<string> arrFields, ref int ipos)
		{
			IfcAlignment2DSegment.parseFields(s, arrFields, ref ipos);
			s.mCurveGeometry = ParserSTEP.ParseLink(arrFields[ipos++]);
		}
		internal static IfcAlignment2DHorizontalSegment Parse(string strDef) { IfcAlignment2DHorizontalSegment a = new IfcAlignment2DHorizontalSegment(); int ipos = 0; parseFields(a, ParserSTEP.SplitLineFields(strDef), ref ipos); return a; }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.LinkToString(mCurveGeometry); }
	}
	public abstract class IfcAlignment2DSegment : BaseClassIfc //IFC4.1
	{
		internal IfcLogicalEnum mTangentialContinuity = IfcLogicalEnum.UNKNOWN;// : OPTIONAL IfcBoolean;
		private string mStartTag = "$";// : OPTIONAL IfcLabel;
		private string mEndTag = "$";// : OPTIONAL IfcLabel;
		
		public string StartTag { get { return (mStartTag == "$" ? "" : ParserIfc.Decode(mStartTag)); } set { mStartTag = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }
		public string EndTag { get { return (mEndTag == "$" ? "" : ParserIfc.Decode(mEndTag)); } set { mEndTag = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }

		protected IfcAlignment2DSegment() : base() { }
		protected IfcAlignment2DSegment(IfcAlignment2DSegment s) : base() { mTangentialContinuity = s.mTangentialContinuity; mStartTag = s.mStartTag; mEndTag = s.mEndTag; }
		protected IfcAlignment2DSegment(DatabaseIfc m, bool tangential, string startTag, string endTag) : base(m) { mTangentialContinuity = (tangential ? IfcLogicalEnum.TRUE : IfcLogicalEnum.FALSE); StartTag = startTag; EndTag = endTag; }	
		protected static void parseFields(IfcAlignment2DSegment a, List<string> arrFields, ref int ipos) 
		{
			a.mTangentialContinuity = ParserIfc.ParseIFCLogical(arrFields[ipos++]); 
			a.mStartTag = arrFields[ipos++].Replace("'", "");
			a.mEndTag = arrFields[ipos++].Replace("'", "");
		}
		protected override string BuildString()
		{
			return base.BuildString() + (mTangentialContinuity == IfcLogicalEnum.UNKNOWN ? ",$," : (mTangentialContinuity == IfcLogicalEnum.TRUE ? ",.T.," : ",.F.")) +
				(mStartTag == "$" ? "$," : "'" + mStartTag + "',") + (mEndTag == "$" ? "$" : "'" + mEndTag + "'");
		}
	}
	public partial class IfcAlignment2DVerSegCircularArc : IfcAlignment2DVerticalSegment  //IFC4x1
	{
		private double mRadius;// : IfcPositiveLengthMeasure;
		private bool mIsConvex;// : IfcBoolean;

		internal IfcAlignment2DVerSegCircularArc() : base() { }
		internal IfcAlignment2DVerSegCircularArc(IfcAlignment2DVerSegCircularArc p) : base(p) { mRadius = p.mRadius; mIsConvex = p.mIsConvex; }
		internal IfcAlignment2DVerSegCircularArc(DatabaseIfc m, bool tangential, string startTag, string endTag, double startDist, double horizontalLength, double startHeight, double startGradient, double radius, bool isCCW)
			: base(m, tangential, startTag, endTag, startDist, horizontalLength, startHeight, startGradient)
		{
			mRadius = radius;
			mIsConvex = isCCW;
		}
		internal static void parseFields(IfcAlignment2DVerSegCircularArc c, List<string> arrFields, ref int ipos)
		{
			IfcAlignment2DVerticalSegment.parseFields(c, arrFields, ref ipos);
			c.mRadius = ParserSTEP.ParseDouble(arrFields[ipos++]);
			c.mIsConvex = ParserSTEP.ParseBool(arrFields[ipos++]);
		}
		internal static IfcAlignment2DVerSegCircularArc Parse(string strDef) { IfcAlignment2DVerSegCircularArc c = new IfcAlignment2DVerSegCircularArc(); int ipos = 0; parseFields(c, ParserSTEP.SplitLineFields(strDef), ref ipos); return c; }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.DoubleToString(mRadius) + "," + ParserSTEP.BoolToString(mIsConvex); }

	}
	public partial class IfcAlignment2DVerSegLine : IfcAlignment2DVerticalSegment  //IFC4x1
	{
		internal IfcAlignment2DVerSegLine() : base() { }
		internal IfcAlignment2DVerSegLine(IfcAlignment2DVerSegLine s) : base(s) { }
		internal IfcAlignment2DVerSegLine(DatabaseIfc m, bool tangential, string startTag, string endTag, double startDist, double horizontalLength, double startHeight, double startGradient)
			: base(m, tangential, startTag, endTag, startDist, horizontalLength, startHeight, startGradient) { }
		internal static void parseFields(IfcAlignment2DVerSegLine c, List<string> arrFields, ref int ipos) { IfcAlignment2DVerticalSegment.parseFields(c, arrFields, ref ipos); }
		internal static IfcAlignment2DVerSegLine Parse(string strDef) { IfcAlignment2DVerSegLine c = new IfcAlignment2DVerSegLine(); int ipos = 0; parseFields(c, ParserSTEP.SplitLineFields(strDef), ref ipos); return c; }
	}
	public partial class IfcAlignment2DVerSegParabolicArc : IfcAlignment2DVerticalSegment  //IFC4x1
	{
		private double mParabolaConstant;// : IfcPositiveLengthMeasure;
		private bool mIsConvex;// : IfcBoolean;

		internal IfcAlignment2DVerSegParabolicArc() : base() { }
		internal IfcAlignment2DVerSegParabolicArc(IfcAlignment2DVerSegParabolicArc p) : base(p) { mParabolaConstant = p.mParabolaConstant; mIsConvex = p.mIsConvex; }
		internal IfcAlignment2DVerSegParabolicArc(DatabaseIfc m, bool tangential, string startTag, string endTag, double startDist, double horizontalLength, double startHeight, double startGradient, double radius, bool isCCW)
			: base(m, tangential, startTag, endTag, startDist, horizontalLength, startHeight, startGradient)
		{
			mParabolaConstant = radius;
			mIsConvex = isCCW;
		}
		internal static void parseFields(IfcAlignment2DVerSegParabolicArc c, List<string> arrFields, ref int ipos)
		{
			IfcAlignment2DVerticalSegment.parseFields(c, arrFields, ref ipos);
			c.mParabolaConstant = ParserSTEP.ParseDouble(arrFields[ipos++]);
			c.mIsConvex = ParserSTEP.ParseBool(arrFields[ipos++]);
		}
		internal static IfcAlignment2DVerSegParabolicArc Parse(string strDef) { IfcAlignment2DVerSegParabolicArc c = new IfcAlignment2DVerSegParabolicArc(); int ipos = 0; parseFields(c, ParserSTEP.SplitLineFields(strDef), ref ipos); return c; }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.DoubleToString(mParabolaConstant) + "," + ParserSTEP.BoolToString(mIsConvex); }
	}
	public partial class IfcAlignment2DVertical : BaseClassIfc //IFC4.1
	{
		internal List<int> mSegments = new List<int>();// : LIST [1:?] OF IfcAlignment2DVerticalSegment;
		internal IfcAlignment2DVertical() : base() { }
		internal IfcAlignment2DVertical(IfcAlignment2DVertical o) : base() { mSegments = new List<int>(o.mSegments.ToArray()); }
		internal static void parseFields(IfcAlignment2DVertical a, List<string> arrFields, ref int ipos) { a.mSegments = ParserSTEP.SplitListLinks(arrFields[ipos++]); }
		protected override string BuildString()
		{
			string str = base.BuildString() + ","  + ParserSTEP.LinkToString(mSegments[0]);
			for (int icounter = 1; icounter < mSegments.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mSegments[icounter]);
			return str + ")";
		}
		internal static IfcAlignment2DVertical Parse(string strDef) { IfcAlignment2DVertical a = new IfcAlignment2DVertical(); int ipos = 0; parseFields(a, ParserSTEP.SplitLineFields(strDef), ref ipos); return a; }
	}
	public abstract class IfcAlignment2DVerticalSegment : IfcAlignment2DSegment //IFC4.1
	{
		internal double mStartDistAlong;// : IfcLengthMeasure;
		internal double mHorizontalLength;// : IfcPositiveLengthMeasure;
		internal double mStartHeight;// : IfcLengthMeasure;
		internal double mStartGradient;// : IfcRatioMeasure; 

		protected IfcAlignment2DVerticalSegment() : base() { }
		protected IfcAlignment2DVerticalSegment(IfcAlignment2DVerticalSegment s) : base(s) { mStartDistAlong = s.mStartDistAlong; mHorizontalLength = s.mHorizontalLength; mStartHeight = s.mStartHeight; mStartGradient = s.mStartGradient; }
		protected IfcAlignment2DVerticalSegment(DatabaseIfc m, bool tangential, string startTag, string endTag, double startDist, double horizontalLength,double startHeight, double startGradient) : base(m, tangential, startTag, endTag) {  }
		protected static void parseFields(IfcAlignment2DVerticalSegment s, List<string> arrFields, ref int ipos)
		{
			IfcAlignment2DSegment.parseFields(s, arrFields, ref ipos);
			s.mStartDistAlong = ParserSTEP.ParseDouble(arrFields[ipos++]);
			s.mHorizontalLength = ParserSTEP.ParseDouble(arrFields[ipos++]);
			s.mStartHeight = ParserSTEP.ParseDouble(arrFields[ipos++]);
			s.mStartGradient = ParserSTEP.ParseDouble(arrFields[ipos++]);
		}
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.DoubleToString(mStartDistAlong) + "," + ParserSTEP.DoubleToString(mHorizontalLength) + "," + ParserSTEP.DoubleToString(mStartHeight) + "," + ParserSTEP.DoubleToString(mStartGradient); }
	}
	public class IfcAngularDimension : IfcDimensionCurveDirectedCallout //IFC4 depreceated
	{
		internal IfcAngularDimension(IfcAngularDimension el) : base(el) { }
		internal IfcAngularDimension() : base() { }
		internal new static IfcAngularDimension Parse(string strDef) { IfcAngularDimension d = new IfcAngularDimension(); int ipos = 0; parseFields(d, ParserSTEP.SplitLineFields(strDef), ref ipos); return d; }
		internal static void parseFields(IfcAngularDimension d,List<string> arrFields, ref int ipos) { IfcDimensionCurveDirectedCallout.parseFields(d,arrFields, ref ipos); }
	}
	public class IfcAnnotation : IfcProduct
	{	 //INVERSE
		//internal List<IfcRelContainedInSpatialStructure> mContainedInStructure = new List<IfcRelContainedInSpatialStructure>(); //: SET [0:1] OF IfcRelContainedInSpatialStructure FOR RelatedElements;
		internal IfcAnnotation(IfcAnnotation el) : base(el) { }
		internal IfcAnnotation() : base() { }
		internal IfcAnnotation(IfcProduct host) : base(host)
		{
			if (mDatabase.mModelView != ModelView.If2x3NotAssigned && mDatabase.mModelView != ModelView.Ifc4NotAssigned)
				throw new Exception("Invalid Model View for " + KeyWord + " : " + mDatabase.ModelView.ToString());
		}
		internal static IfcAnnotation Parse(string strDef) { int ipos = 0; IfcAnnotation a = new IfcAnnotation(); parseFields(a, ParserSTEP.SplitLineFields(strDef), ref ipos); return a; }
		internal static void parseFields(IfcAnnotation a, List<string> arrFields, ref int ipos)  { IfcProduct.parseFields(a,arrFields, ref ipos); }
#if(rRHINO)
		internal override bool drawRhino(Transform tr, int layerIndex, List<GeometryBase> voids, string building, string storey)
		{
			List<GeometryBase> vs = voids;
			if (vs == null)
				vs = new List<GeometryBase>();

			for (int icounter = 0; icounter < mHasOpenings.Count; icounter++)
			{
				List<GeometryBase> v = ((IfcRelVoidsElement)mModel.mIFCobjs[mHasOpenings[icounter]]).getGeom();
				if (v != null)
					vs.AddRange(v);
			}
			return base.drawRhino(tr, layerIndex, vs, building, storey);
		}
#endif
	}
	public abstract class IfcAnnotationCurveOccurrence : IfcAnnotationOccurrence //IFC4 Depreceated
	{
		protected IfcAnnotationCurveOccurrence(IfcAnnotationCurveOccurrence p) : base((IfcAnnotationOccurrence)p) { }
		protected IfcAnnotationCurveOccurrence() : base() { }
		protected static void parseFields(IfcAnnotationCurveOccurrence a,List<string> arrFields, ref int ipos) { IfcAnnotationOccurrence.parseFields(a,arrFields, ref ipos); }
	}
	public class IfcAnnotationFillArea : IfcGeometricRepresentationItem  //IFC4 Depreceated
	{
		internal int mOuterBoundary;// : IfcCurve;
		internal List<int> mInnerBoundaries = new List<int>();// OPTIONAL SET [1:?] OF IfcCurve; 
		internal IfcAnnotationFillArea(IfcAnnotationFillArea p) : base(p) { mOuterBoundary = p.mOuterBoundary; mInnerBoundaries = new List<int>(p.mInnerBoundaries.ToArray()); }
		internal IfcAnnotationFillArea() : base() { }
		internal static void parseFields(IfcAnnotationFillArea a, List<string> arrFields, ref int ipos)
		{
			IfcGeometricRepresentationItem.parseFields(a, arrFields, ref ipos);
			a.mOuterBoundary = ParserSTEP.ParseLink(arrFields[ipos++]);
			string str = arrFields[ipos++];
			if (str != "$")
				a.mInnerBoundaries = ParserSTEP.SplitListLinks(str);
		}
		internal static IfcAnnotationFillArea Parse(string strDef) { IfcAnnotationFillArea a = new IfcAnnotationFillArea(); int ipos = 0; parseFields(a, ParserSTEP.SplitLineFields(strDef), ref ipos); return a; }
		protected override string BuildString()
		{
			string str = base.BuildString() + "," + ParserSTEP.LinkToString(mOuterBoundary);
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
	public class IfcAnnotationFillAreaOccurrence : IfcAnnotationOccurrence //IFC4 Depreceated
	{
		internal int mFillStyleTarget;// : OPTIONAL IfcPoint;
		internal IfcGlobalOrLocalEnum  mGlobalOrLocal;// : OPTIONAL IfcGlobalOrLocalEnum; 
		internal IfcAnnotationFillAreaOccurrence(IfcAnnotationFillAreaOccurrence i) : base(i) { }
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
		protected override string BuildString() { return base.BuildString() +"," + ParserSTEP.LinkToString(mFillStyleTarget) + ",." + mGlobalOrLocal.ToString() + "."; }
	}
	public abstract class IfcAnnotationOccurrence : IfcStyledItem //DEPRECEATED IFC4
	{
		protected IfcAnnotationOccurrence(IfcAnnotationOccurrence p) : base((IfcStyledItem)p) { }
		protected IfcAnnotationOccurrence() : base() { }
		protected static void parseFields(IfcAnnotationOccurrence a,List<string> arrFields, ref int ipos) { IfcStyledItem.parseFields(a,arrFields, ref ipos); }
	}
	public class IfcAnnotationSurface : IfcGeometricRepresentationItem //DEPRECEATED IFC4
	{
		internal int mItem;// : IfcGeometricRepresentationItem;
		internal int mTextureCoordinates;// OPTIONAL IfcTextureCoordinate; 
		internal IfcAnnotationSurface(IfcAnnotationSurface p) : base(p) { mItem = p.mItem; mTextureCoordinates = p.mTextureCoordinates; }
		internal IfcAnnotationSurface() : base() { } 
		internal static void parseFields(IfcAnnotationSurface s,List<string> arrFields, ref int ipos)
		{  
			IfcGeometricRepresentationItem.parseFields(s,arrFields, ref ipos);
			s.mItem = ParserSTEP.ParseLink(arrFields[ipos++]);
			s.mTextureCoordinates = ParserSTEP.ParseLink(arrFields[ipos++]); 
		}
		internal static IfcAnnotationSurface Parse(string strDef) { IfcAnnotationSurface a = new IfcAnnotationSurface(); int ipos = 0; parseFields(a, ParserSTEP.SplitLineFields(strDef), ref ipos); return a; }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.LinkToString(mItem) + "," + ParserSTEP.LinkToString(mTextureCoordinates); }
	}
	public class IfcAnnotationSurfaceOccurrence : IfcAnnotationOccurrence //IFC4 Depreceated
	{
		internal IfcAnnotationSurfaceOccurrence(IfcAnnotationSurfaceOccurrence i) : base(i) { }
		internal IfcAnnotationSurfaceOccurrence() : base() { }
		internal new static IfcAnnotationSurfaceOccurrence Parse(string strDef) { IfcAnnotationSurfaceOccurrence a = new IfcAnnotationSurfaceOccurrence(); int ipos = 0; parseFields(a,ParserSTEP.SplitLineFields(strDef), ref ipos); return a; }
		internal static void parseFields(IfcAnnotationSurfaceOccurrence a,List<string> arrFields, ref int ipos) { IfcAnnotationOccurrence.parseFields(a,arrFields, ref ipos); }
	}
	public class IfcAnnotationSymbolOccurrence : IfcAnnotationOccurrence //IFC4 Depreceated
	{
		internal IfcAnnotationSymbolOccurrence(IfcAnnotationSymbolOccurrence i) : base(i) { }
		internal IfcAnnotationSymbolOccurrence() : base() { }
		internal new static IfcAnnotationSymbolOccurrence Parse(string strDef) { IfcAnnotationSymbolOccurrence a = new IfcAnnotationSymbolOccurrence(); int ipos = 0; parseFields(a, ParserSTEP.SplitLineFields(strDef), ref ipos); return a; }
		internal static void parseFields(IfcAnnotationSymbolOccurrence a,List<string> arrFields, ref int ipos) { IfcAnnotationOccurrence.parseFields(a,arrFields, ref ipos); }
	}
	public class IfcAnnotationTextOccurrence : IfcAnnotationOccurrence //IFC4 Depreceated
	{
		internal IfcAnnotationTextOccurrence() : base() { }
		internal IfcAnnotationTextOccurrence(IfcAnnotationTextOccurrence i) : base(i) { }
		internal new static IfcAnnotationTextOccurrence Parse(string strDef) { IfcAnnotationTextOccurrence a = new IfcAnnotationTextOccurrence(); int ipos = 0; parseFields(a, ParserSTEP.SplitLineFields(strDef), ref ipos); return a; }
		internal static void parseFields(IfcAnnotationTextOccurrence a,List<string> arrFields, ref int ipos) { IfcAnnotationOccurrence.parseFields(a,arrFields, ref ipos); }
	}
	public class IfcApplication : BaseClassIfc
	{
		internal int mApplicationDeveloper;// : IfcOrganization;
		internal string mVersion;// : IfcLabel;
		private string mApplicationFullName;// : IfcLabel;
		internal string mApplicationIdentifier;// : IfcIdentifier; 

		public string ApplicationFullName { get { return ParserIfc.Decode(mApplicationFullName); } set { mApplicationFullName =  ParserIfc.Encode(value.Replace("'", "")); } }

		public override string Name { get { return ApplicationFullName; } set { ApplicationFullName = value; } }

		internal IfcApplication() : base() { }
		internal IfcApplication(IfcApplication o) : base() { mApplicationDeveloper = o.mApplicationDeveloper; mVersion = o.mVersion; mApplicationFullName = o.mApplicationFullName; mApplicationIdentifier = o.mApplicationIdentifier; }
		internal IfcApplication(DatabaseIfc db) : base(db)
		{
			IfcOrganization o = new IfcOrganization(db, "Geometry Gym Pty Ltd");
			mApplicationDeveloper = o.mIndex;
			try
			{
				mVersion =  System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
			}
			catch (Exception) { mVersion = "Unknown"; }
		}
		internal static void parseFields(IfcApplication a, List<string> arrFields, ref int ipos)
		{
			a.mApplicationDeveloper = ParserSTEP.ParseLink(arrFields[ipos++]);
			a.mVersion = arrFields[ipos++].Replace("'", "");
			a.mApplicationFullName = arrFields[ipos++].Replace("'", "");
			a.mApplicationIdentifier = arrFields[ipos++].Replace("'", "");
		}
		protected override string BuildString()
		{
			return base.BuildString() + "," + ParserSTEP.LinkToString(mApplicationDeveloper) + ",'" + mVersion + "','" + 
				(string.IsNullOrEmpty(mApplicationFullName) ? mDatabase.ApplicationFullName : mApplicationFullName) + "','" + 
				(string.IsNullOrEmpty(mApplicationIdentifier) ? mDatabase.ApplicationIdentifier : mApplicationIdentifier) + "'";
		}
		internal static IfcApplication Parse(string strDef) { IfcApplication a = new IfcApplication(); int ipos = 0; parseFields(a, ParserSTEP.SplitLineFields(strDef), ref ipos); return a; }
	}
	public partial class IfcAppliedValue : BaseClassIfc, IfcMetricValueSelect, IfcAppliedValueSelect, IfcResourceObjectSelect //SUPERTYPE OF(IfcCostValue);
	{
		internal string mName = "$";// : OPTIONAL IfcLabel;
		internal string mDescription = "$";// : OPTIONAL IfcText;
		internal int mAppliedValue = 0;// : OPTIONAL IfcAppliedValueSelect
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

		public override string Name { get { return (mName == "$" ? "" : ParserIfc.Decode(mName)); } set { mName = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } } 
		public string Description { get { return (mDescription == "$" ? "" : ParserIfc.Decode(mDescription)); } set { mDescription = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }
		
		public List<IfcExternalReferenceRelationship> HasExternalReferences { get { return mHasExternalReferences; } }
		public List<IfcResourceConstraintRelationship> HasConstraintRelationships { get { return mHasConstraintRelationships; } }

		internal IfcAppliedValue() : base() { }
		internal IfcAppliedValue(IfcAppliedValue v) : base()
		{
			mName = v.mName; mDescription = v.mDescription; mAppliedValue = v.mAppliedValue; mAppliedValueValue = v.mAppliedValueValue; mUnitBasis = v.mUnitBasis; mApplicableDate = v.mApplicableDate;
			mFixedUntilDate = v.mFixedUntilDate; mCategory = v.mCategory; mCondition = v.mCondition; mArithmeticOperator = v.mArithmeticOperator; mComponents.AddRange(v.mComponents);
		}

		internal static IfcAppliedValue Parse(string strDef, Schema schema) { IfcAppliedValue v = new IfcAppliedValue(); int ipos = 0; parseFields(v, ParserSTEP.SplitLineFields(strDef), ref ipos, schema); return v; }
		internal static void parseFields(IfcAppliedValue a, List<string> arrFields, ref int ipos, Schema schema)
		{
			a.mName = arrFields[ipos++].Replace("'", "");
			a.mDescription = arrFields[ipos++].Replace("'", "");
			string str = arrFields[ipos++];
			a.mAppliedValueValue = ParserIfc.parseValue(str);
			if (a.mAppliedValueValue == null)
				a.mAppliedValue = ParserSTEP.ParseLink(str);
			a.mUnitBasis = ParserSTEP.ParseLink(arrFields[ipos++]);
			a.mApplicableDate = arrFields[ipos++];
			a.mFixedUntilDate = arrFields[ipos++];
			if (schema != Schema.IFC2x3)
			{
				a.mCategory = arrFields[ipos++].Replace("'", "");
				a.mCondition = arrFields[ipos++].Replace("'", "");
				string s = arrFields[ipos++];
				if (s.StartsWith("."))
					a.mArithmeticOperator = (IfcArithmeticOperatorEnum)Enum.Parse(typeof(IfcArithmeticOperatorEnum), s.Replace(".", ""));
				a.mComponents = ParserSTEP.SplitListLinks(arrFields[ipos++]);
			}
		}
		protected override string BuildString()
		{
			string str = "$";
			if (mComponents.Count > 0)
			{
				str = "(" + ParserSTEP.LinkToString(mComponents[0]);
				for (int icounter = 1; icounter < mComponents.Count; icounter++)
					str += "," + ParserSTEP.LinkToString(mComponents[icounter]);
				str += ")";
			}
			return base.BuildString() + (mName == "$" ? ",$," : ",'" + mName + "',") + (mDescription == "$" ? "$," : "'" + mDescription + "',") + (mAppliedValueValue != null ? mAppliedValueValue.ToString() : ParserSTEP.LinkToString(mAppliedValue)) + "," + ParserSTEP.LinkToString(mUnitBasis) + "," + mApplicableDate + "," + mFixedUntilDate +
				(mDatabase.mSchema == Schema.IFC2x3 ? "" : (mCategory == "$" ? ",$," : ",'" + mCategory + "',") + (mCondition == "$" ? "$," : "'" + mCondition + "',") + (mArithmeticOperator == IfcArithmeticOperatorEnum.NONE ? "$," : "." + mArithmeticOperator.ToString() + ".,") + str);
		}
	}
	public class IfcAppliedValueRelationship : BaseClassIfc //DEPRECEATED IFC4
	{
		internal int mComponentOfTotal;// : IfcAppliedValue;
		internal List< int> mComponents = new List<int>();// : SET [1:?] OF IfcAppliedValue;
		internal IfcArithmeticOperatorEnum mArithmeticOperator;// : IfcArithmeticOperatorEnum;
		internal string mName;// : OPTIONAL IfcLabel;
		internal string mDescription;// : OPTIONAL IfcText 
		internal IfcAppliedValueRelationship() : base() { }
		internal IfcAppliedValueRelationship(IfcAppliedValueRelationship o) : base()
		{
			mComponentOfTotal = o.mComponentOfTotal;
			mComponents = new List<int>(o.mComponents.ToArray());
			mArithmeticOperator = o.mArithmeticOperator;
			mName = o.mName;
			mDescription = o.mDescription;
		}
		internal static void parseFields( IfcAppliedValueRelationship a,List<string> arrFields, ref int ipos)
		{ 
			a.mComponentOfTotal = ParserSTEP.ParseLink(arrFields[ipos++]);
			a.mComponents = ParserSTEP.SplitListLinks(arrFields[ipos++]); 
			a.mArithmeticOperator =( IfcArithmeticOperatorEnum)Enum.Parse(typeof(IfcArithmeticOperatorEnum), arrFields[ipos++].Replace(".",""));
			a.mName = arrFields[ipos++];
			a.mDescription = arrFields[ipos++]; 
		}
		protected override string BuildString()
		{
			string str = base.BuildString() + "," + ParserSTEP.LinkToString(mComponentOfTotal) + ",(" +
				ParserSTEP.LinkToString(mComponents[0]);
			for(int icounter = 1; icounter < mComponents.Count;icounter++)
				str += "," + ParserSTEP.LinkToString(mComponents[icounter]);
			return str + "),." + mArithmeticOperator.ToString() + ".," + mName + "," + mDescription; 
		}
		internal static IfcAppliedValueRelationship Parse(string strDef) { IfcAppliedValueRelationship a = new IfcAppliedValueRelationship(); int ipos = 0; parseFields(a, ParserSTEP.SplitLineFields(strDef), ref ipos); return a; }
	}
	public interface IfcAppliedValueSelect : IfcInterface { }; //	IfcMeasureWithUnit, IfcValue, IfcReference); IFC2x3 //IfcRatioMeasure, IfcMeasureWithUnit, IfcMonetaryMeasure); 
	public class IfcApproval : BaseClassIfc, IfcResourceObjectSelect
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
		[Browsable(false)]
		public List<IfcExternalReferenceRelationship> HasExternalReferences { get { return mHasExternalReferences; } }
		[Browsable(false)]
		public List<IfcResourceConstraintRelationship> HasConstraintRelationships { get { return mHasConstraintRelationships; } }
		internal IfcApproval() : base() { }
		internal IfcApproval(IfcApproval o) : base()
		{
			mDescription = o.mDescription;
			mApprovalDateTime = o.mApprovalDateTime;
			mApprovalStatus = o.mApprovalStatus;
			mApprovalLevel = o.mApprovalLevel;
			mApprovalQualifier = o.mApprovalQualifier;
			mName = o.mName;
			mIdentifier = o.mIdentifier;
		}
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
		protected override string BuildString() { return base.BuildString()  + "," + mDescription + "," + ParserSTEP.LinkToString(mApprovalDateTime) + "," +  mApprovalStatus + "," + mApprovalLevel + "," + mApprovalQualifier + "," + mName + "," + mIdentifier;  }
		internal static IfcApproval Parse(string strDef) { IfcApproval a = new IfcApproval(); int ipos = 0; parseFields(a, ParserSTEP.SplitLineFields(strDef), ref ipos); return a; }
	}
	public class IfcApprovalActorRelationship : BaseClassIfc //DEPRECEATED IFC4
	{
		internal int mActor;// : IfcActorSelect;
		internal int mApproval;// : IfcApproval;
		internal int mRole;// : IfcActorRole; 
		internal IfcApprovalActorRelationship() : base() { }
		internal IfcApprovalActorRelationship(IfcApprovalActorRelationship o) : base() { mActor = o.mActor; mApproval = o.mApproval; mRole = o.mRole; }
		internal static void parseFields(IfcApprovalActorRelationship r,List<string> arrFields, ref int ipos) { r.mActor = ParserSTEP.ParseLink(arrFields[ipos++]); r.mApproval = ParserSTEP.ParseLink(arrFields[ipos++]); r.mRole = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.LinkToString(mActor) + "," + ParserSTEP.LinkToString(mApproval) + "," + ParserSTEP.LinkToString(mRole);  }
		internal static IfcApprovalActorRelationship Parse(string strDef) { IfcApprovalActorRelationship r = new IfcApprovalActorRelationship(); int ipos = 0; parseFields(r, ParserSTEP.SplitLineFields(strDef), ref ipos); return r; }
	}
	public class IfcApprovalPropertyRelationship : BaseClassIfc //DEPRECEATED IFC4
	{
		internal List<int> mApprovedProperties = new List<int>();// : SET [1:?] OF IfcProperty;
		internal int mApproval;// : IfcApproval; 
		internal IfcApprovalPropertyRelationship() : base() { }
		internal IfcApprovalPropertyRelationship(IfcApprovalPropertyRelationship o) : base() { mApprovedProperties = new List<int>(o.mApprovedProperties.ToArray()); mApproval = o.mApproval; }
		internal static void parseFields(IfcApprovalPropertyRelationship r,List<string> arrFields, ref int ipos) {  r.mApprovedProperties = ParserSTEP.SplitListLinks(arrFields[ipos++]); r.mApproval = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildString()
		{
			string str = base.BuildString() + ",(" + ParserSTEP.LinkToString(mApprovedProperties[0]);
			for(int icounter = 1; icounter < mApprovedProperties.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mApprovedProperties[icounter]);
			str += ")," + ParserSTEP.LinkToString(mApproval);
			return str;
		}
		internal static IfcApprovalPropertyRelationship Parse(string strDef) { IfcApprovalPropertyRelationship r = new IfcApprovalPropertyRelationship(); int ipos = 0; parseFields(r, ParserSTEP.SplitLineFields(strDef), ref ipos); return r; }
	}
	public class IfcApprovalRelationship : IfcResourceLevelRelationship //IFC4Change
	{
		internal int mRelatedApproval;// : IfcApproval;
		internal int mRelatingApproval;// : IfcApproval; 
		internal IfcApprovalRelationship() : base() { }
		internal IfcApprovalRelationship(IfcApprovalRelationship o) : base(o) { mRelatedApproval = o.mRelatedApproval; mRelatingApproval = o.mRelatingApproval;  }
		internal static void parseFields( IfcApprovalRelationship a,List<string> arrFields, ref int ipos,Schema schema) 
		{
			IfcResourceLevelRelationship.parseFields(a,arrFields,ref ipos,schema);
			a.mRelatedApproval = ParserSTEP.ParseLink(arrFields[ipos++]); 
			a.mRelatingApproval = ParserSTEP.ParseLink(arrFields[ipos++]);
			if (schema == Schema.IFC2x3)
			{
				a.mDescription = arrFields[ipos++];
				a.mName = arrFields[ipos++];
			}
		}
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.LinkToString(mRelatedApproval) + "," + ParserSTEP.LinkToString(mRelatingApproval) + (mDatabase.mSchema == Schema.IFC2x3 ? (mDescription == "$" ? ",$,'" : ",'" + mDescription + "','") +  mName  + "'": ""); }
		internal static IfcApprovalRelationship Parse(string strDef,Schema schema) { IfcApprovalRelationship a = new IfcApprovalRelationship(); int ipos = 0; parseFields(a, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return a; }
	}
	public partial class IfcArbitraryClosedProfileDef : IfcProfileDef
	{
		private int mOuterCurve;// : IfcBoundedCurve
		internal IfcBoundedCurve OuterCurve { get { return mDatabase.mIfcObjects[mOuterCurve] as IfcBoundedCurve; } }

		internal IfcArbitraryClosedProfileDef() : base() { }
		internal IfcArbitraryClosedProfileDef(IfcArbitraryClosedProfileDef i) : base(i) { mOuterCurve = i.mOuterCurve; }
		public IfcArbitraryClosedProfileDef(string name, IfcBoundedCurve boundedCurve) : base(boundedCurve.mDatabase) { Name = name; mOuterCurve = boundedCurve.mIndex; }//if (string.Compare(getKW, mKW) == 0) mModel.mArbProfiles.Add(this); }

		internal new static IfcArbitraryClosedProfileDef Parse(string strDef) { IfcArbitraryClosedProfileDef p = new IfcArbitraryClosedProfileDef(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos); return p; }
		internal static void parseFields(IfcArbitraryClosedProfileDef p, List<string> arrFields, ref int ipos) { IfcProfileDef.parseFields(p, arrFields, ref ipos); p.mOuterCurve = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildString() { return (mDatabase.mOutputEssential ? "" : base.BuildString() + "," + ParserSTEP.LinkToString(mOuterCurve)); }
	}
	public partial class IfcArbitraryOpenProfileDef : IfcProfileDef
	{
		private int mCurve;// : IfcBoundedCurve
		internal IfcBoundedCurve Curve { get { return mDatabase.mIfcObjects[mCurve] as IfcBoundedCurve; } }

		internal IfcArbitraryOpenProfileDef() : base() { }
		internal IfcArbitraryOpenProfileDef(IfcArbitraryOpenProfileDef i) : base((IfcProfileDef)i) { mCurve = i.mCurve; }
		internal IfcArbitraryOpenProfileDef(string name, IfcBoundedCurve boundedCurve) : base(boundedCurve.mDatabase) { Name = name; mCurve = boundedCurve.mIndex; }
		internal new static IfcArbitraryOpenProfileDef Parse(string strDef) { IfcArbitraryOpenProfileDef p = new IfcArbitraryOpenProfileDef(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos); return p; }
		internal static void parseFields(IfcArbitraryOpenProfileDef p, List<string> arrFields, ref int ipos) { IfcProfileDef.parseFields(p, arrFields, ref ipos); p.mCurve = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.LinkToString(mCurve); }
	}
	public partial class IfcArbitraryProfileDefWithVoids : IfcArbitraryClosedProfileDef
	{
		private List<int> mInnerCurves = new List<int>();// : SET [1:?] OF IfcCurve; 
		internal List<IfcCurve> InnerCurves { get { return mInnerCurves.ConvertAll(x => mDatabase.mIfcObjects[x] as IfcCurve); } }

		internal IfcArbitraryProfileDefWithVoids() : base() { }
		internal IfcArbitraryProfileDefWithVoids(IfcArbitraryProfileDefWithVoids i) : base((IfcArbitraryClosedProfileDef)i) { mInnerCurves = new List<int>(i.mInnerCurves.ToArray()); }
		public IfcArbitraryProfileDefWithVoids(string name, IfcBoundedCurve perim, IfcCurve inner) : base(name, perim) { mInnerCurves.Add(inner.mIndex); }
		public IfcArbitraryProfileDefWithVoids(string name, IfcBoundedCurve perim, List<IfcCurve> inner) : base(name, perim) { mInnerCurves = inner.ConvertAll(x => x.mIndex); }
		internal new static IfcArbitraryProfileDefWithVoids Parse(string strDef) { IfcArbitraryProfileDefWithVoids p = new IfcArbitraryProfileDefWithVoids(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos); return p; }
		internal static void parseFields(IfcArbitraryProfileDefWithVoids p, List<string> arrFields, ref int ipos) { IfcArbitraryClosedProfileDef.parseFields(p, arrFields, ref ipos); p.mInnerCurves = ParserSTEP.SplitListLinks(arrFields[ipos++]); }
		protected override string BuildString()
		{
			if (mDatabase.mOutputEssential)
				return "";
			if (mInnerCurves.Count == 0)
				return base.BuildString();
			string str = base.BuildString() + ",(" + ParserSTEP.LinkToString(mInnerCurves[0]);
			for (int icounter = 1; icounter < mInnerCurves.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mInnerCurves[icounter]);
			return str + ")";
		}
	}
	public class IfcArcIndex : IfcSegmentIndexSelect
	{
		internal int mA, mB, mC;
		public IfcArcIndex(int a, int b, int c) { mA = a; mB = b; mC = c; }
		public override string ToString() { return "IFCARCINDEX((" + mA + "," + mB + "," + mC + "))"; }
	}
	public class IfcAsset : IfcGroup
	{	
		internal string mAssetID;// : IfcIdentifier;
		internal int mOriginalValue;// : IfcCostValue;
		internal int mCurrentValue;// : IfcCostValue;
		internal int mTotalReplacementCost;// : IfcCostValue;
		internal int mOwner;// : IfcActorSelect;
		internal int mUser;// : IfcActorSelect;
		internal int mResponsiblePerson;// : IfcPerson;
		internal int mIncorporationDate;// : IfcCalendarDate;
		internal int mDepreciatedValue;// : IfcCostValue; 
		internal IfcAsset() : base() { }
		internal IfcAsset(IfcAsset p) : base(p)
		{
			mAssetID = p.mAssetID;
			mOriginalValue = p.mOriginalValue;
			mCurrentValue = p.mCurrentValue;
			mTotalReplacementCost = p.mTotalReplacementCost;
			mOwner = p.mOwner;
			mUser = p.mUser;
			mResponsiblePerson = p.mResponsiblePerson;
			mIncorporationDate = p.mIncorporationDate;
			mDepreciatedValue = p.mDepreciatedValue;
		}
		internal IfcAsset(DatabaseIfc m, string name) : base(m,name) { }
		internal new static IfcAsset Parse(string strDef) { IfcAsset a = new IfcAsset(); int ipos = 0; parseFields(a, ParserSTEP.SplitLineFields(strDef), ref ipos); return a; }
		internal static void parseFields(IfcAsset a,List<string> arrFields, ref int ipos)
		{ 
			IfcGroup.parseFields(a,arrFields, ref ipos);
			a.mAssetID = arrFields[ipos++].Replace("'","");
			a.mOriginalValue = ParserSTEP.ParseLink(arrFields[ipos++]);
			a.mCurrentValue = ParserSTEP.ParseLink(arrFields[ipos++]);
			a.mTotalReplacementCost = ParserSTEP.ParseLink(arrFields[ipos++]);
			a.mOwner = ParserSTEP.ParseLink(arrFields[ipos++]);
			a.mUser = ParserSTEP.ParseLink(arrFields[ipos++]);
			a.mResponsiblePerson = ParserSTEP.ParseLink(arrFields[ipos++]);
			a.mIncorporationDate = ParserSTEP.ParseLink(arrFields[ipos++]);
			a.mDepreciatedValue = ParserSTEP.ParseLink(arrFields[ipos++]); 
		}
		protected override string BuildString()
		{
			return base.BuildString() +",'" + mAssetID + "'," + ParserSTEP.LinkToString(mOriginalValue) + "," +ParserSTEP.LinkToString(mCurrentValue) + "," + 
				ParserSTEP.LinkToString(mTotalReplacementCost) + "," +ParserSTEP.LinkToString(mOwner) + "," +
				ParserSTEP.LinkToString(mUser) + "," +ParserSTEP.LinkToString(mResponsiblePerson) + "," +
				ParserSTEP.LinkToString(mIncorporationDate) + "," +ParserSTEP.LinkToString(mDepreciatedValue);
		}
	}
	public class IfcAsymmetricIShapeProfileDef : IfcIShapeProfileDef //IFC4 IfcParameterizedProfileDef
	{
		internal double mTopFlangeWidth;// : IfcPositiveLengthMeasure;
		internal double mTopFlangeThickness;// : OPTIONAL IfcPositiveLengthMeasure;
		internal double mTopFlangeFilletRadius;// : OPTIONAL IfcPositiveLengthMeasure;
		internal double mCentreOfGravityInY;// : OPTIONAL IfcPositiveLengthMeasure 
		internal IfcAsymmetricIShapeProfileDef() : base() { }
		internal IfcAsymmetricIShapeProfileDef(IfcAsymmetricIShapeProfileDef i) : base(i)
		{
			mTopFlangeWidth = i.mTopFlangeWidth;
			mTopFlangeThickness = i.mTopFlangeThickness;
			mTopFlangeFilletRadius = i.mTopFlangeFilletRadius;
			mCentreOfGravityInY = i.mCentreOfGravityInY;
		}
		internal static void parseFields(IfcAsymmetricIShapeProfileDef p, List<string> arrFields, ref int ipos,Schema schema)
		{
			IfcIShapeProfileDef.parseFields(p, arrFields, ref ipos,schema);
			p.mTopFlangeWidth = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mTopFlangeThickness = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mTopFlangeFilletRadius = ParserSTEP.ParseDouble(arrFields[ipos++]);
			if (schema == Schema.IFC2x3)
				p.mCentreOfGravityInY = ParserSTEP.ParseDouble(arrFields[ipos++]);
		}
		internal new static IfcAsymmetricIShapeProfileDef Parse(string strDef,Schema schema) { IfcAsymmetricIShapeProfileDef p = new IfcAsymmetricIShapeProfileDef(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return p; }

		protected override string BuildString()
		{
			return base.BuildString() + "," + ParserSTEP.DoubleToString(mTopFlangeWidth) + "," + ParserSTEP.DoubleOptionalToString(mTopFlangeThickness) + "," +
				ParserSTEP.DoubleOptionalToString(mTopFlangeFilletRadius) + (mDatabase.mSchema == Schema.IFC2x3 ? "," + ParserSTEP.DoubleOptionalToString(mCentreOfGravityInY) : "");
		}
	}
	public class IfcAudioVisualAppliance : IfcFlowTerminal //IFC4
	{
		internal IfcAudioVisualApplianceTypeEnum mPredefinedType = IfcAudioVisualApplianceTypeEnum.NOTDEFINED;// OPTIONAL : IfcAudioVisualApplianceTypeEnum;
		public IfcAudioVisualApplianceTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcAudioVisualAppliance(IfcAudioVisualAppliance a) : base(a) { }
		internal IfcAudioVisualAppliance() : base() { }
		internal static void parseFields(IfcAudioVisualAppliance s, List<string> arrFields, ref int ipos)
		{
			IfcFlowTerminal.parseFields(s, arrFields, ref ipos);
			string str = arrFields[ipos++];
			if (str[0] == '.')
				s.mPredefinedType = (IfcAudioVisualApplianceTypeEnum)Enum.Parse(typeof(IfcAudioVisualApplianceTypeEnum), str.Substring(1, str.Length - 2));
		}
		internal new static IfcAudioVisualAppliance Parse(string strDef) { IfcAudioVisualAppliance s = new IfcAudioVisualAppliance(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildString() { return base.BuildString() + (mDatabase.mSchema == Schema.IFC2x3 ? "" : ( mPredefinedType == IfcAudioVisualApplianceTypeEnum.NOTDEFINED  ? ",$" : ",." + mPredefinedType.ToString() + ".")); }

	}
	public partial class IfcAudioVisualApplianceType : IfcFlowTerminalType
	{
		internal IfcAudioVisualApplianceTypeEnum mPredefinedType = IfcAudioVisualApplianceTypeEnum.NOTDEFINED;// : IfcAudioVisualApplianceBoxTypeEnum; 
		public IfcAudioVisualApplianceTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcAudioVisualApplianceType(IfcAudioVisualApplianceType t) : base(t) { mPredefinedType = t.mPredefinedType; }
		internal IfcAudioVisualApplianceType() : base() { }
		internal IfcAudioVisualApplianceType(DatabaseIfc m, string name, IfcAudioVisualApplianceTypeEnum t) : base(m) { Name = name; mPredefinedType = t; }
		internal static void parseFields(IfcAudioVisualApplianceType t, List<string> arrFields, ref int ipos) { IfcFlowControllerType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcAudioVisualApplianceTypeEnum)Enum.Parse(typeof(IfcAudioVisualApplianceTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcAudioVisualApplianceType Parse(string strDef) { IfcAudioVisualApplianceType t = new IfcAudioVisualApplianceType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildString() { return base.BuildString() + ",." + mPredefinedType.ToString() + "."; }
	}
	public partial class IfcAxis1Placement : IfcPlacement
	{
		private int mAxis;//  : OPTIONAL IfcDirection
		public IfcDirection Axis { get { return (mAxis > 0 ? mDatabase.mIfcObjects[mAxis] as IfcDirection : null); } }
		
		internal IfcAxis1Placement() : base() { }
		internal IfcAxis1Placement(IfcAxis1Placement p) : base(p) { mAxis = p.mAxis; }
		internal IfcAxis1Placement(DatabaseIfc m) : base(m) { }
 
		protected override string BuildString() { return (mDatabase.mOutputEssential ? "" : base.BuildString() + "," + ParserSTEP.LinkToString(mAxis)); }
		internal static IfcAxis1Placement Parse(string strDef) { IfcAxis1Placement p = new IfcAxis1Placement(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos); return p; }
		internal static void parseFields(IfcAxis1Placement p, List<string> arrFields, ref int ipos) { IfcPlacement.parseFields(p, arrFields, ref ipos); p.mAxis = ParserSTEP.ParseLink(arrFields[ipos++]); }
	}
	public partial interface IfcAxis2Placement : IfcInterface { } //SELECT ( IfcAxis2Placement2D, IfcAxis2Placement3D);
	public partial class IfcAxis2Placement2D : IfcPlacement, IfcAxis2Placement
	{ 
		private int mRefDirection;// : OPTIONAL IfcDirection;
		public IfcDirection RefDirection { get { return mDatabase.mIfcObjects[mRefDirection] as IfcDirection; } set { mRefDirection = value.mIndex; } }
		
		internal IfcAxis2Placement2D() : base() { }
		internal IfcAxis2Placement2D(IfcAxis2Placement2D i) : base(i) { mRefDirection = i.mRefDirection; }
		public IfcAxis2Placement2D(DatabaseIfc db) : base(db) { }
		public IfcAxis2Placement2D(IfcCartesianPoint point) : base(point) { } 
	
		protected override string BuildString() { return (mDatabase.mOutputEssential ? "" : base.BuildString() + "," + ParserSTEP.LinkToString(mRefDirection)); }
		internal static IfcAxis2Placement2D Parse(string strDef) { IfcAxis2Placement2D p = new IfcAxis2Placement2D(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos); return p; }
		internal static void parseFields(IfcAxis2Placement2D p, List<string> arrFields, ref int ipos) { IfcPlacement.parseFields(p, arrFields, ref ipos); p.mRefDirection = ParserSTEP.ParseLink(arrFields[ipos++]); }
	}
	public partial class IfcAxis2Placement3D : IfcPlacement, IfcAxis2Placement
	{
		private int mAxis;// : OPTIONAL IfcDirection;
		private int mRefDirection;// : OPTIONAL IfcDirection; 

		internal IfcDirection Axis { get { return mDatabase.mIfcObjects[mAxis] as IfcDirection; } set { mAxis = (value == null ? 0 : value.mIndex); } }
		internal IfcDirection RefDirection { get { return mDatabase.mIfcObjects[mRefDirection] as IfcDirection; } set { mRefDirection = (value == null ? 0 : value.mIndex); } }

		internal IfcAxis2Placement3D() : base() { }
		internal IfcAxis2Placement3D(IfcAxis2Placement3D i) : base(i) { mAxis = i.mAxis; mRefDirection = i.mRefDirection; }
		public IfcAxis2Placement3D(IfcCartesianPoint p) : base(p) { }
		public IfcAxis2Placement3D(IfcCartesianPoint p, IfcDirection axis, IfcDirection refDirection) : base(p) { mAxis = (axis == null ? 0 : axis.mIndex); mRefDirection = (refDirection == null ? 0 : refDirection.mIndex); }
		internal IfcAxis2Placement3D(DatabaseIfc m) : base(m) { }  
		
		internal static IfcAxis2Placement3D Parse(string strDef) { IfcAxis2Placement3D p = new IfcAxis2Placement3D(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos); return p; }
		internal static void parseFields(IfcAxis2Placement3D p, List<string> arrFields, ref int ipos) { IfcPlacement.parseFields(p, arrFields, ref ipos); p.mAxis = ParserSTEP.ParseLink(arrFields[ipos++]); p.mRefDirection = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildString() { return (mDatabase.mOutputEssential ? "" : base.BuildString() + (mAxis > 0 ? "," + ParserSTEP.LinkToString(mAxis) : ",$") + (mRefDirection > 0 ? "," + ParserSTEP.LinkToString(mRefDirection) : ",$")); }
	}
}
