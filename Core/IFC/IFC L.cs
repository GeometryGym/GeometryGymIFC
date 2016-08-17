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
	public partial class IfcLaborResource : IfcConstructionResource
	{
		internal IfcLaborResourceTypeEnum mPredefinedType = IfcLaborResourceTypeEnum.NOTDEFINED;// OPTIONAL : IfcRoofTypeEnum; 
		public IfcLaborResourceTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcLaborResource() : base() { }
		internal IfcLaborResource(DatabaseIfc db, IfcLaborResource r) : base(db,r) { mPredefinedType = r.mPredefinedType; }
		internal IfcLaborResource(DatabaseIfc db) : base(db) { }
		internal static IfcLaborResource Parse(string strDef, ReleaseVersion schema) { IfcLaborResource r = new IfcLaborResource(); int ipos = 0; parseFields(r, ParserSTEP.SplitLineFields(strDef), ref ipos, schema); return r; }
		internal static void parseFields(IfcLaborResource r, List<string> arrFields, ref int ipos, ReleaseVersion schema)
		{
			IfcConstructionResource.parseFields(r, arrFields, ref ipos,schema);
			if (schema != ReleaseVersion.IFC2x3)
			{
				string str = arrFields[ipos++];
				if (str[0] == '.')
					r.mPredefinedType = (IfcLaborResourceTypeEnum)Enum.Parse(typeof(IfcLaborResourceTypeEnum), str.Substring(1, str.Length - 2));
			}
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."; }
	}
	public partial class IfcLaborResourceType : IfcConstructionResourceType //IFC4
	{
		internal IfcLaborResourceTypeEnum mPredefinedType = IfcLaborResourceTypeEnum.NOTDEFINED;
		public IfcLaborResourceTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcLaborResourceType() : base() { }
		internal IfcLaborResourceType(DatabaseIfc db, IfcLaborResourceType t) : base(db ,t) { mPredefinedType = t.mPredefinedType; }
		internal IfcLaborResourceType(DatabaseIfc m, string name, IfcLaborResourceTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
		internal static void parseFields(IfcLaborResourceType t, List<string> arrFields, ref int ipos) { IfcLaborResourceType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcLaborResourceTypeEnum)Enum.Parse(typeof(IfcLaborResourceTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcLaborResourceType Parse(string strDef) { IfcLaborResourceType t = new IfcLaborResourceType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."; }
	}
	public partial class IfcLagTime : IfcSchedulingTime //IFC4
	{
		internal IfcTimeOrRatioSelect mLagValue;//	IfcTimeOrRatioSelect
		internal IfcTaskDurationEnum mDurationType = IfcTaskDurationEnum.NOTDEFINED;//	IfcTaskDurationEnum; 
		internal IfcLagTime() : base() { }
		//internal IfcLagTime(IfcLagTime i) : base(i) { mLagValue = i.mLagValue; mDurationType = i.mDurationType; }
		internal IfcLagTime(DatabaseIfc db,  IfcTimeOrRatioSelect lag, IfcTaskDurationEnum nature) : base(db) { mLagValue = lag; mDurationType = nature; }
		internal static IfcLagTime Parse(string strDef) { IfcLagTime f = new IfcLagTime(); int ipos = 0; parseFields(f, ParserSTEP.SplitLineFields(strDef), ref ipos); return f; }
		internal static void parseFields(IfcLagTime f, List<string> arrFields, ref int ipos) { IfcSchedulingTime.parseFields(f, arrFields, ref ipos); }
		protected override string BuildStringSTEP() { return (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : base.BuildStringSTEP() + "," + mLagValue.String + ",." + mDurationType.ToString() + "."); }
		internal double getSecondsDuration() { IfcDuration d = mLagValue as IfcDuration; return (d == null ? 0 : d.ToSeconds()); }
		internal TimeSpan getLag() { return new TimeSpan(0, 0, (int)getSecondsDuration()); }
	}
	public partial class IfcLamp : IfcFlowTerminal //IFC4
	{
		internal IfcLampTypeEnum mPredefinedType = IfcLampTypeEnum.NOTDEFINED;// OPTIONAL : IfcLampTypeEnum;
		public IfcLampTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcLamp() : base() { }
		internal IfcLamp(DatabaseIfc db, IfcLamp l) : base(db, l) { mPredefinedType = l.mPredefinedType; }
		internal IfcLamp(IfcProduct host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
		internal static void parseFields(IfcLamp s, List<string> arrFields, ref int ipos)
		{
			IfcFlowTerminal.parseFields(s, arrFields, ref ipos);
			string str = arrFields[ipos++];
			if (str[0] == '.')
				s.mPredefinedType = (IfcLampTypeEnum)Enum.Parse(typeof(IfcLampTypeEnum), str.Substring(1, str.Length - 2));
		}
		internal new static IfcLamp Parse(string strDef) { IfcLamp s = new IfcLamp(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mPredefinedType == IfcLampTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }

	}
	public partial class IfcLampType : IfcFlowTerminalType
	{
		internal IfcLampTypeEnum mPredefinedType = IfcLampTypeEnum.NOTDEFINED;// : IfcLampTypeEnum; 
		public IfcLampTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcLampType() : base() { }
		internal IfcLampType(DatabaseIfc db, IfcLampType t) : base(db, t) { mPredefinedType = t.mPredefinedType; }
		internal IfcLampType(DatabaseIfc m, string name, IfcLampTypeEnum t) : base(m) { Name = name; mPredefinedType = t; }
		internal static void parseFields(IfcLampType t, List<string> arrFields, ref int ipos) { IfcFlowControllerType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcLampTypeEnum)Enum.Parse(typeof(IfcLampTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcLampType Parse(string strDef) { IfcLampType t = new IfcLampType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."; }
	}
	public interface IfcLayeredItem : IBaseClassIfc { List<IfcPresentationLayerAssignment> LayerAssignments { get; } }// = SELECT(IfcRepresentationItem, IfcRepresentation);
	public partial class IfcLibraryInformation : IfcExternalInformation
	{
		internal string mName;// :	IfcLabel;
		internal string mVersion = "$";//:	OPTIONAL IfcLabel;
		internal int mPublisher;//	 :	OPTIONAL IfcActorSelect;
		internal string mVersionDate = "$"; // :	OPTIONAL IfcDateTime;
		internal int mVersionDateSS = 0; // 
		internal string mLocation = "$";//	 :	OPTIONAL IfcURIReference; //IFC4 Added
		internal string mDescription = "$";//	 :	OPTIONAL IfcText; //IFC4 Added
		internal List<int> mLibraryReference = new List<int>();// IFC2x3 : 	OPTIONAL SET[1:?] OF IfcLibraryReference;
		//INVERSE
		internal List<IfcRelAssociatesLibrary> mLibraryRefForObjects = new List<IfcRelAssociatesLibrary>();//IFC4 :	SET [0:?] OF IfcRelAssociatesLibrary FOR RelatingLibrary;
		internal List<IfcLibraryReference> mHasLibraryReferences = new List<IfcLibraryReference>();//	:	SET OF IfcLibraryReference FOR ReferencedLibrary;

		public override string Name { get { return ParserIfc.Decode(mName); } set { mName = (string.IsNullOrEmpty(value) ? "UNKNOWN" : ParserIfc.Encode(value)); } } 
		public string Version { get { return (mVersion == "$" ? "" : ParserIfc.Decode(mVersion)); } set { mVersion = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public IfcActorSelect Publisher { get { return mDatabase[mPublisher] as IfcActorSelect; } set { mPublisher = (value == null ? 0 : value.Index); } }
		public string Location { get { return (mLocation == "$" ? "" : ParserIfc.Decode(mLocation)); } set { mLocation = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public string Description { get { return (mDescription == "$" ? "" : ParserIfc.Decode(mDescription)); } set { mDescription = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }

		internal IfcLibraryInformation() : base() { }
		internal IfcLibraryInformation(DatabaseIfc db, IfcLibraryInformation i) : base(db,i) { mName = i.mName; mVersion = i.mVersion; if(i.mPublisher > 0) Publisher = db.Factory.Duplicate(i.mDatabase[ i.mPublisher]) as IfcActorSelect; mVersionDate = i.mVersionDate; mLocation = i.mLocation; mDescription = i.mDescription; }
		public IfcLibraryInformation(DatabaseIfc db, string name) : base(db) { Name = name; }
		internal static IfcLibraryInformation Parse(string strDef, ReleaseVersion schema) { IfcLibraryInformation f = new IfcLibraryInformation(); int ipos = 0; parseFields(f, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return f; }
		internal static void parseFields(IfcLibraryInformation f, List<string> arrFields, ref int ipos, ReleaseVersion schema)
		{
			IfcExternalInformation.parseFields(f, arrFields, ref ipos);
			f.mName = arrFields[ipos++].Replace("'", "");
			f.mVersion = arrFields[ipos++].Replace("'", "");
			f.mPublisher = ParserSTEP.ParseLink(arrFields[ipos++]);
			if (schema == ReleaseVersion.IFC2x3)
			{
				ipos++;
				string str = arrFields[ipos++];
				f.mLibraryReference = ParserSTEP.SplitListLinks(str.Substring(1,str.Length-2));	
			}
			else
			{
				f.mVersionDate = arrFields[ipos++].Replace("'", "");
				f.mLocation = arrFields[ipos++];
				f.mDescription = arrFields[ipos++];
			}
		}
		protected override string BuildStringSTEP()
		{
			string result = base.BuildStringSTEP() + ",'" + mName + (mVersion == "$" ? "',$," : "','" + mVersion + "',") + ParserSTEP.LinkToString(mPublisher);
			if (mDatabase.Release == ReleaseVersion.IFC2x3)
			{
				string refs =  mHasLibraryReferences.Count > 0 ? "#" + mHasLibraryReferences[0].mIndex : "";
				for (int icounter = 1; icounter < mHasLibraryReferences.Count; icounter++)
					refs += ",#" + mHasLibraryReferences[icounter].mIndex;
				return result + ",$,(" + refs + ")"; //TODO date
			}
			return result + (mVersionDate == "$" ? ",$," : ",'" + mVersionDate + "',") + (mLocation == "$" ? "$," : "'" + mLocation + "',") + (mDescription == "$" ? "$" : "'" + mDescription + "'");
		}
		internal override void postParseRelate()
		{
			base.postParseRelate();
			if (mDatabase.Release == ReleaseVersion.IFC2x3)
			{
				foreach (int i in mLibraryReference)
					(mDatabase[i] as IfcLibraryReference).ReferencedLibrary = this;
			}
			
		}
	}
	public partial class IfcLibraryReference : IfcExternalReference, IfcLibrarySelect
	{
		internal string mDescription = "$";//IFC4	 :	OPTIONAL IfcText;
		internal string mLanguage = "$"; //IFC4	 :	OPTIONAL IfcLanguageId;
		internal int mReferencedLibrary; //	 :	OPTIONAL IfcLibraryInformation; ifc2x3 INVERSE ReferenceIntoLibrary
		//INVERSE
		internal List<IfcRelAssociatesLibrary> mLibraryRefForObjects = new List<IfcRelAssociatesLibrary>();//IFC4 :	SET [0:?] OF IfcRelAssociatesLibrary FOR RelatingLibrary;

		public string Description { get { return (mDescription == "$" ? "" : ParserIfc.Decode(mDescription)); } set { mDescription = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }
		public string Language { get { return (mLanguage == "$" ? "" : ParserIfc.Decode(mLanguage)); } set { mLanguage = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }
		public IfcLibraryInformation ReferencedLibrary { get { return mDatabase[mReferencedLibrary] as IfcLibraryInformation; } set { mReferencedLibrary = (value == null ? 0 : value.mIndex); if (!value.mHasLibraryReferences.Contains(this)) value.mHasLibraryReferences.Add(this); } }

		internal IfcLibraryReference() : base() { }
		internal IfcLibraryReference(DatabaseIfc db, IfcLibraryReference r) : base(db,r) { mDescription = r.mDescription; mLanguage = r.mLanguage; ReferencedLibrary = db.Factory.Duplicate(r.ReferencedLibrary) as IfcLibraryInformation; }
		public IfcLibraryReference(DatabaseIfc db) : base(db) { }
		public IfcLibraryReference(IfcLibraryInformation referenced) : base(referenced.mDatabase) { ReferencedLibrary = referenced; }
		internal static IfcLibraryReference Parse(string strDef, ReleaseVersion schema) { IfcLibraryReference f = new IfcLibraryReference(); int ipos = 0; parseFields(f, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return f; }
		internal static void parseFields(IfcLibraryReference f, List<string> arrFields, ref int ipos, ReleaseVersion schema)
		{
			IfcExternalReference.parseFields(f, arrFields, ref ipos);
			if(schema != ReleaseVersion.IFC2x3)
			{
				f.mDescription = arrFields[ipos++].Replace("'", "");
				f.mLanguage = arrFields[ipos++].Replace("'", "");
				f.mReferencedLibrary = ParserSTEP.ParseLink(arrFields[ipos++]);
			}
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : ((mDescription == "$" ? ",$," : ",'" + mDescription + "',") + (mLanguage == "$" ? "$," : "'" + mLanguage + "',") + ParserSTEP.LinkToString(mReferencedLibrary))); }
		internal override void postParseRelate()
		{
			base.postParseRelate();
			if (mDatabase.Release != ReleaseVersion.IFC2x3 && mReferencedLibrary > 0)
				ReferencedLibrary.mHasLibraryReferences.Add(this);
		}
	}
	public interface IfcLibrarySelect //SELECT ( IfcLibraryReference,  IfcLibraryInformation);
	{
		int Index { get; }
		//IfcRelAssociatesLibrary Associates { get; }
		//string Name { get; }
	}
	//ENTITY IfcLightDistributionData;
	public interface IfcLightDistributionDataSourceSelect : IBaseClassIfc { } //SELECT(IfcExternalReference,IfcLightIntensityDistribution);
	public partial class IfcLightFixture : IfcFlowTerminal
	{
		internal IfcLightFixtureTypeEnum mPredefinedType = IfcLightFixtureTypeEnum.NOTDEFINED;// : OPTIONAL IfcLightFixtureTypeEnum; 
		public IfcLightFixtureTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcLightFixture() : base() { }
		internal IfcLightFixture(DatabaseIfc db, IfcLightFixture f) : base(db, f) { mPredefinedType = f.mPredefinedType; }
		internal IfcLightFixture(IfcProduct host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
		internal static void parseFields(IfcLightFixture t, List<string> arrFields, ref int ipos) { IfcFlowTerminal.parseFields(t, arrFields, ref ipos); string s = arrFields[ipos++]; if (s[0] == '.') t.mPredefinedType = (IfcLightFixtureTypeEnum)Enum.Parse(typeof(IfcLightFixtureTypeEnum), s.Substring(1, s.Length - 2)); }
		internal new static IfcLightFixture Parse(string strDef) { IfcLightFixture t = new IfcLightFixture(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "" : (mPredefinedType == IfcLightFixtureTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
	}
	public partial class IfcLightFixtureType : IfcFlowTerminalType
	{
		internal IfcLightFixtureTypeEnum mPredefinedType = IfcLightFixtureTypeEnum.NOTDEFINED;// : IfcLightFixtureTypeEnum; 
		public IfcLightFixtureTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcLightFixtureType() : base() { }
		internal IfcLightFixtureType(DatabaseIfc db, IfcLightFixtureType t) : base(db, t) { mPredefinedType = t.mPredefinedType; }
		internal IfcLightFixtureType(DatabaseIfc m, string name, IfcLightFixtureTypeEnum t) : base(m) { Name = name; mPredefinedType = t; }
		internal static void parseFields(IfcLightFixtureType t, List<string> arrFields, ref int ipos) { IfcFlowControllerType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcLightFixtureTypeEnum)Enum.Parse(typeof(IfcLightFixtureTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcLightFixtureType Parse(string strDef) { IfcLightFixtureType t = new IfcLightFixtureType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + ",." + mPredefinedType.ToString() + "."; }
	}
	//ENTITY IfcLightIntensityDistribution ,IfcLightDistributionDataSourceSelect
	public abstract partial class IfcLightSource : IfcGeometricRepresentationItem //ABSTRACT SUPERTYPE OF (ONEOF (IfcLightSourceAmbient ,IfcLightSourceDirectional ,IfcLightSourceGoniometric ,IfcLightSourcePositional))
	{
		internal string mName = "$";// : OPTIONAL IfcLabel;
		internal int mLightColour;// : IfcColourRgb;
		internal double mAmbientIntensity;// : OPTIONAL IfcNormalisedRatioMeasure;
		internal double mIntensity;// : OPTIONAL IfcNormalisedRatioMeasure; 
		protected IfcLightSource() : base() { }
		protected IfcLightSource(DatabaseIfc db, IfcLightSource l) : base(db,l) { mName = l.mName; mLightColour = l.mLightColour; mAmbientIntensity = l.mAmbientIntensity; mIntensity = l.mIntensity; }
		protected static void parseFields(IfcLightSource l, List<string> arrFields, ref int ipos)
		{
			IfcGeometricRepresentationItem.parseFields(l, arrFields, ref ipos);
			l.mName = arrFields[ipos++];
			l.mLightColour = ParserSTEP.ParseLink(arrFields[ipos++]);
			l.mAmbientIntensity = ParserSTEP.ParseDouble(arrFields[ipos++]);
			l.mIntensity = ParserSTEP.ParseDouble(arrFields[ipos++]);
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + mName + "," + ParserSTEP.LinkToString(mLightColour) + "," + ParserSTEP.DoubleOptionalToString(mAmbientIntensity) + "," + ParserSTEP.DoubleOptionalToString(mIntensity); }
	}
	public partial class IfcLightSourceAmbient : IfcLightSource
	{
		internal IfcLightSourceAmbient() : base() { }
		//internal IfcLightSourceAmbient(IfcLightSourceAmbient el) : base((IfcLightSourceAmbient)el) { }
		internal static IfcLightSourceAmbient Parse(string strDef) { IfcLightSourceAmbient l = new IfcLightSourceAmbient(); int ipos = 0; parseFields(l, ParserSTEP.SplitLineFields(strDef), ref ipos); return l; }
		internal static void parseFields(IfcLightSourceAmbient l, List<string> arrFields, ref int ipos) { IfcLightSource.parseFields(l, arrFields, ref ipos); }
	}
	public partial class IfcLightSourceDirectional : IfcLightSource
	{
		internal int mOrientation;// : IfcDirection; 
		internal IfcLightSourceDirectional() : base() { }
		//internal IfcLightSourceDirectional(IfcLightSourceDirectional el) : base((IfcLightSource)el) { mOrientation = el.mOrientation; }
		internal static IfcLightSourceDirectional Parse(string strDef) { IfcLightSourceDirectional l = new IfcLightSourceDirectional(); int ipos = 0; parseFields(l, ParserSTEP.SplitLineFields(strDef), ref ipos); return l; }
		internal static void parseFields(IfcLightSourceDirectional l, List<string> arrFields, ref int ipos) { IfcLightSource.parseFields(l, arrFields, ref ipos); l.mOrientation = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mOrientation); }
	}
	public partial class IfcLightSourceGoniometric : IfcLightSource
	{
		internal int mPosition;// : IfcAxis2Placement3D;
		internal int mColourAppearance;// : OPTIONAL IfcColourRgb;
		internal double mColourTemperature;// : IfcReal;
		internal double mLuminousFlux;// : IfcLuminousFluxMeasure;
		internal IfcLightEmissionSourceEnum mLightEmissionSource;// : IfcLightEmissionSourceEnum;
		internal int mLightDistributionDataSource; // IfcLightDistributionDataSourceSelect; 
		internal IfcLightSourceGoniometric() : base() { }
		//internal IfcLightSourceGoniometric(DatabaseIfc db, IfcLightSourceGoniometric el)
		//	: base(el)
		//{
		//	mPosition = el.mPosition;
		//	mColourAppearance = el.mColourAppearance;
		//	mColourTemperature = el.mColourTemperature;
		//	mLuminousFlux = el.mLuminousFlux;
		//	mLightEmissionSource = el.mLightEmissionSource;
		//	mLightDistributionDataSource = el.mLightDistributionDataSource;
		//}
		internal static IfcLightSourceGoniometric Parse(string strDef) { IfcLightSourceGoniometric l = new IfcLightSourceGoniometric(); int ipos = 0; parseFields(l, ParserSTEP.SplitLineFields(strDef), ref ipos); return l; }
		internal static void parseFields(IfcLightSourceGoniometric l, List<string> arrFields, ref int ipos)
		{
			IfcLightSource.parseFields(l, arrFields, ref ipos);
			l.mPosition = ParserSTEP.ParseLink(arrFields[ipos++]);
			l.mColourAppearance = ParserSTEP.ParseLink(arrFields[ipos++]);
			l.mColourTemperature = ParserSTEP.ParseDouble(arrFields[ipos++]);
			l.mLuminousFlux = ParserSTEP.ParseDouble(arrFields[ipos++]);
			l.mLightEmissionSource = (IfcLightEmissionSourceEnum)Enum.Parse(typeof(IfcLightEmissionSourceEnum), arrFields[ipos++].Replace(".", ""));
			l.mLightDistributionDataSource = ParserSTEP.ParseLink(arrFields[ipos++]);
		}
		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mPosition) + "," + ParserSTEP.LinkToString(mColourAppearance) + "," +
				ParserSTEP.DoubleToString(mColourTemperature) + "," + ParserSTEP.DoubleToString(mLuminousFlux) + ",." +
				mLightEmissionSource.ToString() + ".," + ParserSTEP.LinkToString(mLightDistributionDataSource);
		}
	}
	public partial class IfcLightSourcePositional : IfcLightSource
	{
		internal int mPosition;// : IfcCartesianPoint;
		internal double mRadius;// : IfcPositiveLengthMeasure;
		internal double mConstantAttenuation;// : IfcReal;
		internal double mDistanceAttenuation;// : IfcReal;
		internal double mQuadricAttenuation;// : IfcReal; 
		internal IfcLightSourcePositional() : base() { }
		//internal IfcLightSourcePositional(IfcLightSourcePositional el)
		//	: base((IfcLightSource)el)
		//{
		//	mPosition = el.mPosition;
		//	mRadius = el.mRadius;
		//	mConstantAttenuation = el.mConstantAttenuation;
		//	mDistanceAttenuation = el.mDistanceAttenuation;
		//	mQuadricAttenuation = el.mQuadricAttenuation;
		//}
		internal static IfcLightSourcePositional Parse(string strDef) { IfcLightSourcePositional l = new IfcLightSourcePositional(); int ipos = 0; parseFields(l, ParserSTEP.SplitLineFields(strDef), ref ipos); return l; }
		internal static void parseFields(IfcLightSourcePositional l, List<string> arrFields, ref int ipos)
		{
			IfcLightSource.parseFields(l, arrFields, ref ipos);
			l.mPosition = ParserSTEP.ParseLink(arrFields[ipos++]);
			l.mRadius = ParserSTEP.ParseDouble(arrFields[ipos++]);
			l.mConstantAttenuation = ParserSTEP.ParseDouble(arrFields[ipos++]);
			l.mDistanceAttenuation = ParserSTEP.ParseDouble(arrFields[ipos++]);
			l.mQuadricAttenuation = ParserSTEP.ParseDouble(arrFields[ipos++]);
		}
		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mPosition) + "," + ParserSTEP.DoubleToString(mRadius) + "," +
				ParserSTEP.DoubleToString(mConstantAttenuation) + "," + ParserSTEP.DoubleToString(mDistanceAttenuation) + "," +
				ParserSTEP.DoubleToString(mQuadricAttenuation);
		}
	}
	public partial class IfcLightSourceSpot : IfcLightSource
	{
		internal int mOrientation;// : IfcDirection;
		internal double mConcentrationExponent;// :  IfcReal;
		internal double mSpreadAngle;// : IfcPositivePlaneAngleMeasure;
		internal double mBeamWidthAngle;// : IfcPositivePlaneAngleMeasure; 
		internal IfcLightSourceSpot() : base() { }
		//internal IfcLightSourceSpot(IfcLightSourceSpot el) : base(el) { mOrientation = el.mOrientation; mConcentrationExponent = el.mConcentrationExponent; mSpreadAngle = el.mSpreadAngle; mBeamWidthAngle = el.mBeamWidthAngle; }
		internal static IfcLightSourceSpot Parse(string strDef) { IfcLightSourceSpot l = new IfcLightSourceSpot(); int ipos = 0; parseFields(l, ParserSTEP.SplitLineFields(strDef), ref ipos); return l; }
		internal static void parseFields(IfcLightSourceSpot l, List<string> arrFields, ref int ipos) { IfcLightSource.parseFields(l, arrFields, ref ipos); l.mOrientation = ParserSTEP.ParseLink(arrFields[ipos++]); l.mConcentrationExponent = ParserSTEP.ParseDouble(arrFields[ipos++]); l.mSpreadAngle = ParserSTEP.ParseDouble(arrFields[ipos++]); l.mBeamWidthAngle = ParserSTEP.ParseDouble(arrFields[ipos++]); }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mOrientation) + "," + ParserSTEP.DoubleToString(mConcentrationExponent) + "," + ParserSTEP.DoubleToString(mSpreadAngle) + "," + ParserSTEP.DoubleToString(mBeamWidthAngle); }
	}
	public partial class IfcLine : IfcCurve
	{
		internal int mPnt;// : IfcCartesianPoint;
		internal int mDir;// : IfcVector; 

		public IfcCartesianPoint Pnt { get { return mDatabase[mPnt] as IfcCartesianPoint; } set { mPnt = value.mIndex; } }
		public IfcVector Dir { get { return mDatabase[mDir] as IfcVector; } set { mDir = value.mIndex; } }

		internal IfcLine() : base() { }
		internal IfcLine(DatabaseIfc db, IfcLine l) : base(db,l) { Pnt = db.Factory.Duplicate( l.Pnt) as IfcCartesianPoint; Dir = db.Factory.Duplicate( l.Dir) as IfcVector; }
		public IfcLine(IfcCartesianPoint point, IfcVector dir) : base(point.mDatabase) { Pnt = point; Dir = dir; }
		internal static IfcLine Parse(string strDef) { IfcLine l = new IfcLine(); int ipos = 0; parseFields(l, ParserSTEP.SplitLineFields(strDef), ref ipos); return l; }
		internal static void parseFields(IfcLine l, List<string> arrFields, ref int ipos) { IfcCurve.parseFields(l, arrFields, ref ipos); l.mPnt = ParserSTEP.ParseLink(arrFields[ipos++]); l.mDir = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mPnt) + "," + ParserSTEP.LinkToString(mDir); }
	}
	public partial class IfcLinearDimension : IfcDimensionCurveDirectedCallout // DEPRECEATED IFC4
	{
		internal IfcLinearDimension() : base() { }
		//internal IfcLinearDimension(IfcAngularDimension el) : base((IfcDimensionCurveDirectedCallout)el) { }
		internal new static IfcLinearDimension Parse(string strDef) { IfcLinearDimension d = new IfcLinearDimension(); int ipos = 0; parseFields(d, ParserSTEP.SplitLineFields(strDef), ref ipos); return d; }
		internal static void parseFields(IfcLinearDimension d, List<string> arrFields, ref int ipos) { IfcDimensionCurveDirectedCallout.parseFields(d, arrFields, ref ipos); }
	}
	public partial class IfcLineIndex : IfcSegmentIndexSelect
	{
		internal List<int> mIndices = new List<int>();
		public IfcLineIndex(int a, int b) { mIndices.Add(a); mIndices.Add(b); }
		public IfcLineIndex(IEnumerable<int> indices) { mIndices.AddRange(indices); }
		public override string ToString()
		{
			string indices = "";
			for (int icounter = 1; icounter < mIndices.Count; icounter++)
				indices += "," + mIndices[icounter];
			return "IFCLINEINDEX((" + mIndices[0] + indices + "))";
		}
	}
	
	public partial class IfcLocalPlacement : IfcObjectPlacement
	{
		private int mPlacementRelTo;// : OPTIONAL IfcObjectPlacement;
		private int mRelativePlacement;// : IfcAxis2Placement;

		private bool mCalculated = false;

		public IfcObjectPlacement PlacementRelTo
		{
			get { return mDatabase[mPlacementRelTo] as IfcObjectPlacement; }
			set
			{
				IfcObjectPlacement pl = PlacementRelTo;
				if (pl != null)
					pl.mReferencedByPlacements.Remove(this);
				if (value == null)
					mPlacementRelTo = 0;
				else
				{
					mPlacementRelTo = value.mIndex;
					value.mReferencedByPlacements.Add(this);
				}
			}
		}
		public IfcAxis2Placement RelativePlacement
		{
			get { return mDatabase[mRelativePlacement] as IfcAxis2Placement; }
			set { mRelativePlacement = value.Index; mCalculated = false; }
		}

		internal IfcLocalPlacement() : base() { }
		internal IfcLocalPlacement(DatabaseIfc db, IfcLocalPlacement p) : base(db, p)
		{
			if (p.mPlacementRelTo > 0)
				PlacementRelTo = db.Factory.Duplicate(p.PlacementRelTo) as IfcObjectPlacement;
			RelativePlacement = db.Factory.Duplicate(p.mDatabase[p.mRelativePlacement]) as IfcAxis2Placement;
		}
		public IfcLocalPlacement(IfcAxis2Placement placement) : base(placement.Database) { RelativePlacement = placement; }
		public IfcLocalPlacement(IfcObjectPlacement relativeTo, IfcAxis2Placement placement) : this(placement)
		{
			if (relativeTo != null)
			{
				mPlacementRelTo = relativeTo.mIndex;
				relativeTo.mReferencedByPlacements.Add(this);
			}
		}
		
		internal static IfcLocalPlacement Parse(string strDef) { IfcLocalPlacement p = new IfcLocalPlacement(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos); return p; }
		internal static void parseFields(IfcLocalPlacement p, List<string> arrFields, ref int ipos) { IfcObjectPlacement.parseFields(p, arrFields, ref ipos); p.mPlacementRelTo = ParserSTEP.ParseLink(arrFields[ipos++]); p.mRelativePlacement = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildStringSTEP()
		{
			if (mPlacesObject.Count == 0 && mReferencedByPlacements.Count == 0)
				return "";
			return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mPlacementRelTo) + "," + ParserSTEP.LinkToString(mRelativePlacement == 0 ? mDatabase.Factory.PlaneXYPlacement.mIndex : mRelativePlacement);
		}

		internal override void postParseRelate()
		{
			base.postParseRelate();
			if (mPlacementRelTo > 0)
				PlacementRelTo.mReferencedByPlacements.Add(this);
		}
	}
	public partial class IfcLocalTime : BaseClassIfc, IfcDateTimeSelect // DEPRECEATED IFC4
	{
		internal int mHourComponent;// : IfcHourInDay;
		internal int mMinuteComponent;// : OPTIONAL IfcMinuteInHour;
		internal double mSecondComponent;// : OPTIONAL IfcSecondInMinute;
		internal int mZone;// OPTIONAL IfcCoordinatedUniversalTimeOffset;
		internal int mDaylightSavingOffset;// : OPTIONAL IfcDaylightSavingHour; 

		public IfcCoordinatedUniversalTimeOffset Zone { get { return mDatabase[mZone] as IfcCoordinatedUniversalTimeOffset; } set { mZone = (value == null ? 0 : value.mIndex); } }
		public int DaylightSavingOffset { get { return mDaylightSavingOffset; } set { mDaylightSavingOffset = value; } }
		internal IfcLocalTime() : base() { }
		internal IfcLocalTime(DatabaseIfc db, IfcLocalTime t) : base(db,t)
		{
			mHourComponent = t.mHourComponent;
			mMinuteComponent = t.mMinuteComponent;
			mSecondComponent = t.mSecondComponent;
			mZone = t.mZone;
			mDaylightSavingOffset = t.mDaylightSavingOffset;
		}
		internal IfcLocalTime(DatabaseIfc m, int hour, int min, int sec) : base(m) { mHourComponent = hour; mMinuteComponent = min; mSecondComponent = sec; }
		internal static void parseFields(IfcLocalTime s, List<string> arrFields, ref int ipos)
		{
			s.mHourComponent = int.Parse(arrFields[ipos++]);
			s.mMinuteComponent = ParserSTEP.ParseInt(arrFields[ipos++]);
			s.mSecondComponent = ParserSTEP.ParseDouble(arrFields[ipos++]);
			s.mZone = ParserSTEP.ParseLink(arrFields[ipos++]);
			s.mDaylightSavingOffset = int.Parse(arrFields[ipos++]);
		}
		internal static IfcLocalTime Parse(string strDef) { IfcLocalTime t = new IfcLocalTime(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + mHourComponent + "," + mMinuteComponent + "," + ParserSTEP.DoubleToString(mSecondComponent) + "," + ParserSTEP.LinkToString(mZone) + "," + mDaylightSavingOffset; }
		public DateTime DateTime
		{
			get
			{
				return new DateTime(0, 0,0, mHourComponent, mMinuteComponent, (int)mSecondComponent);
			}
		}
	}
	public partial class IfcLoop : IfcTopologicalRepresentationItem /*SUPERTYPE OF (ONEOF (IfcEdgeLoop ,IfcPolyLoop ,IfcVertexLoop))*/
	{ 
		internal IfcLoop() : base() { }
		internal IfcLoop(DatabaseIfc db) : base(db) { }
		internal IfcLoop(DatabaseIfc db, IfcLoop l) : base(db,l) { }
		internal static IfcLoop Parse(string strDef) { IfcLoop l = new IfcLoop(); int ipos = 0; parseFields(l, ParserSTEP.SplitLineFields(strDef), ref ipos); return l; }
		internal static void parseFields(IfcLoop l, List<string> arrFields, ref int ipos) { IfcTopologicalRepresentationItem.parseFields(l, arrFields, ref ipos); }
	}
	public partial class IfcLShapeProfileDef : IfcParameterizedProfileDef
	{
		internal double mDepth, mWidth, mThickness;// : IfcPositiveLengthMeasure;
		internal double mFilletRadius = double.NaN, mEdgeRadius = double.NaN;// : OPTIONAL IfcPositiveLengthMeasure;
		internal double mLegSlope = double.NaN;// : OPTIONAL IfcPlaneAngleMeasure;
		internal double mCentreOfGravityInX = double.NaN, mCentreOfGravityInY = double.NaN;// : OPTIONAL IfcPositiveLengthMeasure 
		internal IfcLShapeProfileDef() : base() { }
		internal IfcLShapeProfileDef(DatabaseIfc db, IfcLShapeProfileDef p) : base(db, p)
		{
			mDepth = p.mDepth;
			mWidth = p.mWidth;
			mThickness = p.mThickness;
			mFilletRadius = p.mFilletRadius;
			mEdgeRadius = p.mEdgeRadius;
			mLegSlope = p.mLegSlope;
			mCentreOfGravityInX = p.mCentreOfGravityInX;
			mCentreOfGravityInY = p.mCentreOfGravityInY;
		}
		public IfcLShapeProfileDef(DatabaseIfc db, string name, double depth, double width, double thickness, double filletRadius, double edgeRadius, double legSlope)
			: base(db,name)
		{
			mDepth = depth;
			mWidth = width;
			mThickness = thickness;
			mFilletRadius = filletRadius;
			mEdgeRadius = edgeRadius;
			mLegSlope = legSlope;
			
		}

		internal static void parseFields(IfcLShapeProfileDef p, List<string> arrFields, ref int ipos, ReleaseVersion schema)
		{
			IfcParameterizedProfileDef.parseFields(p, arrFields, ref ipos);
			p.mDepth = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mWidth = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mThickness = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mFilletRadius = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mEdgeRadius = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mLegSlope = ParserSTEP.ParseDouble(arrFields[ipos++]);
			if (schema == ReleaseVersion.IFC2x3)
			{
				p.mCentreOfGravityInX = ParserSTEP.ParseDouble(arrFields[ipos++]);
				p.mCentreOfGravityInY = ParserSTEP.ParseDouble(arrFields[ipos++]);
			}
		}
		internal static IfcLShapeProfileDef Parse(string strDef, ReleaseVersion schema) { IfcLShapeProfileDef p = new IfcLShapeProfileDef(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return p; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.DoubleToString(mDepth) + "," + ParserSTEP.DoubleToString(mWidth) + "," + ParserSTEP.DoubleToString(mThickness) + "," + ParserSTEP.DoubleOptionalToString(mFilletRadius) + "," + ParserSTEP.DoubleOptionalToString(mEdgeRadius) + "," + ParserSTEP.DoubleOptionalToString(mLegSlope) + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "," + ParserSTEP.DoubleOptionalToString(mCentreOfGravityInX) + "," + ParserSTEP.DoubleOptionalToString(mCentreOfGravityInY) : ""); }
	}
}
