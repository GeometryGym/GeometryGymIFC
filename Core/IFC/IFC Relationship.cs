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
	public partial class IfcRelAggregates : IfcRelDecomposes
	{
		internal int mRelatingObject;// : IfcObjectDefinition IFC4 IfcObject
		internal List<int> mRelatedObjects = new List<int>();// : SET [1:?] OF IfcObjectDefinition; 

		public IfcObjectDefinition RelatingObject { get { return mDatabase[mRelatingObject] as IfcObjectDefinition; } set { mRelatingObject = value.mIndex; value.mIsDecomposedBy.Add(this); } }
		public List<IfcObjectDefinition> RelatedObjects { get { return mRelatedObjects.ConvertAll(x => mDatabase[x] as IfcObjectDefinition); }  set { mRelatedObjects = value.ConvertAll(x => x.mIndex); foreach (IfcObjectDefinition od in value) od.mDecomposes = this; } }

		internal IfcRelAggregates() : base() { }
		internal IfcRelAggregates(DatabaseIfc db, IfcRelAggregates a, bool downStream) : base(db,a)
		{
			RelatingObject = db.Factory.Duplicate(a.RelatingObject,downStream) as IfcObjectDefinition;
			if(downStream)
				RelatedObjects = a.RelatedObjects.ConvertAll(x=>db.Factory.Duplicate(x) as IfcObjectDefinition);
		}
		internal IfcRelAggregates(IfcObjectDefinition relObject) : base(relObject.mDatabase)
		{
			mRelatingObject = relObject.mIndex;
			relObject.mIsDecomposedBy.Add(this);
		}
		internal IfcRelAggregates(IfcObjectDefinition relObject, IfcObjectDefinition relatedObject) : this(relObject, new List<IfcObjectDefinition>() { relatedObject }) { }
		internal IfcRelAggregates(IfcObjectDefinition relObject, List<IfcObjectDefinition> relatedObjects) : this(relObject)
		{
			for (int icounter = 0; icounter < relatedObjects.Count; icounter++)
			{
				mRelatedObjects.Add(relatedObjects[icounter].mIndex);
				relatedObjects[icounter].mDecomposes = this;
			}
		}
		internal IfcRelAggregates(DatabaseIfc m, string container, string part, IfcObjectDefinition relObject)
			: this(relObject) { Name = container + " Container"; Description = container + " Container for " + part + "s"; }
		internal IfcRelAggregates(DatabaseIfc m, string container, string part, IfcObjectDefinition relObject, IfcObjectDefinition relatedObject)
			: this(relObject, relatedObject) { Name = container + " Container"; Description = container + " Container for " + part + "s"; }
		internal IfcRelAggregates(string container, string part, IfcObjectDefinition relObject, List<IfcObjectDefinition> relatedObjects)
			: this(relObject, relatedObjects) { Name = container + " Container"; Description = container + " Container for " + part + "s"; }
		internal static IfcRelAggregates Parse(string strDef) { IfcRelAggregates a = new IfcRelAggregates(); int ipos = 0; parseFields(a, ParserSTEP.SplitLineFields(strDef), ref ipos); return a; }
		internal static void parseFields(IfcRelAggregates a, List<string> arrFields, ref int ipos) { IfcRelDecomposes.parseFields(a, arrFields, ref ipos); a.mRelatingObject = ParserSTEP.ParseLink(arrFields[ipos++]); a.mRelatedObjects = ParserSTEP.SplitListLinks(arrFields[ipos++]); }
		protected override string BuildStringSTEP()
		{
			string str = "";
			if (mRelatedObjects.Count > 0)
			{
				str += ParserSTEP.LinkToString(mRelatedObjects[0]);
				for (int icounter = 1; icounter < mRelatedObjects.Count; icounter++)
					str += "," + ParserSTEP.LinkToString(mRelatedObjects[icounter]);
			}
			else
				return "";
			return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mRelatingObject) + ",(" + str + ")";
		}

		internal override void postParseRelate()
		{
			base.postParseRelate();
			RelatingObject.mIsDecomposedBy.Add(this);
			List<IfcObjectDefinition> ods = RelatedObjects;
			for (int icounter = 0; icounter < ods.Count; icounter++)
			{
				if (ods[icounter] != null)
					ods[icounter].mDecomposes = this;
			}
		}
		internal bool addObject(IfcObjectDefinition o)
		{
			if (mRelatedObjects.Contains(o.mIndex))
				return false;
			mRelatedObjects.Add(o.mIndex);
			o.mDecomposes = this;
			return true;
		}
	}
	public abstract partial class IfcRelAssigns : IfcRelationship //	ABSTRACT SUPERTYPE OF(ONEOF(IfcRelAssignsToActor, IfcRelAssignsToControl, IfcRelAssignsToGroup, IfcRelAssignsToProcess, IfcRelAssignsToProduct, IfcRelAssignsToResource))
	{
		internal List<int> mRelatedObjects = new List<int>();// : SET [1:?] OF IfcObjectDefinition;
		//internal IfcObjectTypeEnum mRelatedObjectsType = IfcObjectTypeEnum.NOTDEFINED;// : OPTIONAL IfcObjectTypeEnum; IFC4 CHANGE  The attribute is deprecated and shall no longer be used. A NIL value should always be assigned.
		public List<IfcObjectDefinition> RelatedObjects { get { return mRelatedObjects.ConvertAll(x => mDatabase[x] as IfcObjectDefinition); } set { mRelatedObjects = value.ConvertAll(x => x.mIndex); for (int icounter = 0; icounter < value.Count; icounter++) value[icounter].mHasAssignments.Add(this); } }

		protected IfcRelAssigns() : base() { }
		protected IfcRelAssigns(DatabaseIfc db) : base(db) { }
		protected IfcRelAssigns(IfcRelAssigns a) : base(a) { mRelatedObjects = new List<int>(a.mRelatedObjects.ToArray()); }
		protected IfcRelAssigns(DatabaseIfc db, IfcRelAssigns a) : base(db, a)
		{
			//RelatedObjects = a.RelatedObjects.ConvertAll(x => db.Factory.Duplicate(x) as IfcObjectDefinition);
		}
		protected IfcRelAssigns(IfcObjectDefinition related) : this(new List<IfcObjectDefinition>() { related }) { }
		protected IfcRelAssigns(List<IfcObjectDefinition> relObjects) : base(relObjects[0].mDatabase) { RelatedObjects = relObjects; }
		protected override string BuildStringSTEP()
		{
			string str = base.BuildStringSTEP() + ",(" + ParserSTEP.LinkToString(mRelatedObjects[0]);
			if (mRelatedObjects.Count > 200)
			{
				StringBuilder sb = new StringBuilder();
				sb.Append(str);
				for (int icounter = 1; icounter < mRelatedObjects.Count; icounter++)
					sb.Append(",#" + mRelatedObjects[icounter]);
				sb.Append("),$");
				return sb.ToString();
			}
			else
			{
				for (int icounter = 1; icounter < mRelatedObjects.Count; icounter++)
					str += ",#" + mRelatedObjects[icounter];
				return str + "),$";
			}
		}
		protected static void parseFields(IfcRelAssigns c, List<string> arrFields, ref int ipos)
		{
			IfcRelationship.parseFields(c, arrFields, ref ipos);
			c.mRelatedObjects = ParserSTEP.SplitListLinks(arrFields[ipos++]);
			ipos++; //string str = arrFields[ipos++]; //if(!str.Contains("$")) c.mRelatedObjectsType = (IfcObjectTypeEnum) Enum.Parse(typeof(IfcObjectTypeEnum),str.Replace(".",""));  
		}
		protected static void parseString(IfcRelAssigns a, string str, ref int pos)
		{
			a.parseString(str, ref pos);
			a.mRelatedObjects = ParserSTEP.StripListLink(str, ref pos);
			ParserSTEP.StripField(str, ref pos);
		}

		internal void assign(IfcObjectDefinition o) { mRelatedObjects.Add(o.mIndex); o.mHasAssignments.Add(this); }
		internal override void postParseRelate()
		{
			base.postParseRelate();
			List<IfcObjectDefinition> ods = RelatedObjects;
			for (int icounter = 0; icounter < ods.Count; icounter++)
			{
				try
				{
					IfcObjectDefinition o = ods[icounter];
					if (o != null)
						o.mHasAssignments.Add(this);
				}
				catch (Exception) { }
			}
		}
	}
	public partial class IfcRelAssignsTasks : IfcRelAssignsToControl // IFC4 depreceated
	{
		internal int mTimeForTask;// :  	OPTIONAL IfcScheduleTimeControl; 

		public IfcScheduleTimeControl TimeForTask { get { return mDatabase[mTimeForTask] as IfcScheduleTimeControl; } }
		internal IfcWorkControl WorkControl { get { return mDatabase[mRelatingControl] as IfcWorkControl; } }
		internal List<IfcTask> Tasks { get { return RelatedObjects.ConvertAll(x => x as IfcTask); } }

		internal IfcRelAssignsTasks() : base() { }
		internal IfcRelAssignsTasks(IfcRelAssignsTasks a) : base(a) { mTimeForTask = a.mTimeForTask; }
		internal IfcRelAssignsTasks(IfcWorkControl relControl, IfcScheduleTimeControl timeControl)
			: base(relControl) { if (timeControl != null) { mTimeForTask = timeControl.mIndex; timeControl.mScheduleTimeControlAssigned = this; } }
		internal IfcRelAssignsTasks(IfcWorkControl relControl, IfcTask relObject, IfcScheduleTimeControl timeControl)
			: base(relControl, relObject) { if (timeControl != null) { mTimeForTask = timeControl.mIndex; timeControl.mScheduleTimeControlAssigned = this; } }
		internal IfcRelAssignsTasks(IfcWorkControl relControl, List<IfcTask> relObjects, IfcScheduleTimeControl timeControl)
			: base(relControl, relObjects.ConvertAll(x => (IfcObjectDefinition)x)) { if (timeControl != null) { mTimeForTask = timeControl.mIndex; timeControl.mScheduleTimeControlAssigned = this; } }
		internal new static IfcRelAssignsTasks Parse(string strDef) { IfcRelAssignsTasks a = new IfcRelAssignsTasks(); int ipos = 0; parseFields(a, ParserSTEP.SplitLineFields(strDef), ref ipos); return a; }
		internal static void parseFields(IfcRelAssignsTasks c, List<string> arrFields, ref int ipos) { IfcRelAssignsToControl.parseFields(c, arrFields, ref ipos); c.mTimeForTask = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildStringSTEP() { return (mDatabase.ModelView == ModelView.Ifc2x3Coordination || mRelatedObjects.Count == 0 ? "" : base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mTimeForTask)); }
		internal override void postParseRelate()
		{
			base.postParseRelate();
			IfcScheduleTimeControl t = TimeForTask;
			if (t != null)
				t.mScheduleTimeControlAssigned = this;
		}
	}
	public partial class IfcRelAssignsToActor : IfcRelAssigns
	{
		internal int mRelatingActor;// : IfcActor; 
		internal int mActingRole;//	 :	OPTIONAL IfcActorRole;

		public IfcActor RelatingActor { get { return mDatabase[mRelatingActor] as IfcActor; } }

		internal IfcRelAssignsToActor() : base() { }
		internal IfcRelAssignsToActor(IfcRelAssignsToActor a) : base(a) { mRelatingActor = a.mRelatingActor; mActingRole = a.mActingRole; }
		internal IfcRelAssignsToActor(IfcActor relActor, IfcActorRole r) : base(relActor.mDatabase) { mRelatingActor = relActor.mIndex; if (r != null) mActingRole = r.mIndex; }
		internal IfcRelAssignsToActor(IfcActor relActor, IfcObjectDefinition relObject, IfcActorRole r) : base(relObject) { mRelatingActor = relActor.mIndex; if (r != null) mActingRole = r.mIndex; }
		internal IfcRelAssignsToActor(IfcActor relActor, List<IfcObjectDefinition> relObjects, IfcActorRole r) : base(relObjects) { mRelatingActor = relActor.mIndex; if (r != null) mActingRole = r.mIndex; }
		internal static IfcRelAssignsToActor Parse(string strDef) { IfcRelAssignsToActor a = new IfcRelAssignsToActor(); int ipos = 0; parseFields(a, ParserSTEP.SplitLineFields(strDef), ref ipos); return a; }
		internal static void parseFields(IfcRelAssignsToActor c, List<string> arrFields, ref int ipos) { IfcRelAssigns.parseFields(c, arrFields, ref ipos); c.mRelatingActor = ParserSTEP.ParseLink(arrFields[ipos++]); c.mActingRole = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildStringSTEP() { return (mDatabase.ModelView == ModelView.Ifc2x3Coordination || mRelatedObjects.Count == 0 ? "" : base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mRelatingActor)); }
		internal override void postParseRelate()
		{
			base.postParseRelate();
			IfcActor c = RelatingActor;
			if (c != null)
				c.mIsActingUpon.Add(this);
		}
	}
	public partial class IfcRelAssignsToControl : IfcRelAssigns
	{
		internal int mRelatingControl;// : IfcControl; 
		public IfcControl RelatingControl { get { return mDatabase[mRelatingControl] as IfcControl; } }

		internal IfcRelAssignsToControl() : base() { }
		internal IfcRelAssignsToControl(IfcRelAssignsToControl a) : base(a) { mRelatingControl = a.mRelatingControl; }
		internal IfcRelAssignsToControl(IfcControl relControl) : base(relControl.mDatabase) { mRelatingControl = relControl.mIndex; relControl.mControls.Add(this); }
		internal IfcRelAssignsToControl(IfcControl relControl, IfcObjectDefinition relObject) : base(relObject) { mRelatingControl = relControl.mIndex; relControl.mControls.Add(this); }
		internal IfcRelAssignsToControl(IfcControl relControl, List<IfcObjectDefinition> relObjects) : base(relObjects) { mRelatingControl = relControl.mIndex; relControl.mControls.Add(this); }
		internal static IfcRelAssignsToControl Parse(string strDef) { IfcRelAssignsToControl a = new IfcRelAssignsToControl(); int ipos = 0; parseFields(a, ParserSTEP.SplitLineFields(strDef), ref ipos); return a; }
		internal static void parseFields(IfcRelAssignsToControl c, List<string> arrFields, ref int ipos) { IfcRelAssigns.parseFields(c, arrFields, ref ipos); c.mRelatingControl = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildStringSTEP() { return (mDatabase.ModelView == ModelView.Ifc2x3Coordination || mRelatedObjects.Count == 0 ? "" : base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mRelatingControl)); }
		internal override void postParseRelate()
		{
			base.postParseRelate();
			IfcControl c = RelatingControl;
			if (c != null)
				c.mControls.Add(this);
		}
	}
	public partial class IfcRelAssignsToGroup : IfcRelAssigns 	//SUPERTYPE OF(IfcRelAssignsToGroupByFactor)
	{
		private int mRelatingGroup;// : IfcGroup; 
		public IfcGroup RelatingGroup { get { return mDatabase[mRelatingGroup] as IfcGroup; } set { mRelatingGroup = value.mIndex; value.mIsGroupedBy.Add(this); }  }

		internal IfcRelAssignsToGroup() : base() { }
		internal IfcRelAssignsToGroup(DatabaseIfc db, IfcRelAssignsToGroup a) : base(db,a) { RelatingGroup = db.Factory.Duplicate(a.RelatingGroup) as IfcGroup; }
		internal IfcRelAssignsToGroup(IfcGroup relgroup) : base(relgroup.mDatabase) { mRelatingGroup = relgroup.mIndex; relgroup.mIsGroupedBy.Add(this); }
		internal IfcRelAssignsToGroup(IfcGroup relgroup, List<IfcObjectDefinition> relObjects) : base(relObjects) { mRelatingGroup = relgroup.mIndex; }
		internal static IfcRelAssignsToGroup Parse(string strDef)
		{
			IfcRelAssignsToGroup a = new IfcRelAssignsToGroup();
			int pos = 0;
			parseString(a, strDef, ref pos);
			return a;
		}
		protected static void parseString(IfcRelAssignsToGroup a, string str, ref int pos)
		{
			IfcRelAssigns.parseString(a, str, ref pos);
			a.mRelatingGroup = ParserSTEP.StripLink(str, ref pos);
		}
		protected override string BuildStringSTEP() { return (mDatabase.ModelView == ModelView.Ifc2x3Coordination || mRelatedObjects.Count == 0 ? "" : base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mRelatingGroup)); }
		internal override void postParseRelate()
		{
			base.postParseRelate();
			IfcGroup g = RelatingGroup;
			if (g != null)
				g.mIsGroupedBy.Add(this);
		}
	}
	public partial class IfcRelAssignsToGroupByFactor : IfcRelAssignsToGroup //IFC4
	{
		public override string KeyWord { get { return (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "IfcRelAssignsToGroup" : base.KeyWord); } }
		internal double mFactor = 1;//	 :	IfcRatioMeasure;
		internal IfcRelAssignsToGroupByFactor() : base() { }
		internal IfcRelAssignsToGroupByFactor(DatabaseIfc db, IfcRelAssignsToGroupByFactor a) : base(db,a) { mFactor = a.mFactor; }
		internal IfcRelAssignsToGroupByFactor(IfcGroup relgroup, double factor) : base(relgroup) { mFactor = factor; }
		internal IfcRelAssignsToGroupByFactor(IfcGroup relgroup, List<IfcObjectDefinition> relObjects, double factor) : base(relgroup, relObjects) { mFactor = factor; }
		internal new static IfcRelAssignsToGroup Parse(string strDef)
		{
			IfcRelAssignsToGroupByFactor a = new IfcRelAssignsToGroupByFactor();
			int pos = 0;
			IfcRelAssignsToGroup.parseString(a, strDef, ref pos);
			a.mFactor = ParserSTEP.StripDouble(strDef, ref pos);
			return a;
		}
		protected override string BuildStringSTEP() { return (mRelatedObjects.Count == 0 ? "" : base.BuildStringSTEP() + (mDatabase.Release == ReleaseVersion.IFC2x3 ? "" : "," + ParserSTEP.DoubleToString(mFactor))); }
	}
	public partial class IfcRelAssignsToProcess : IfcRelAssigns
	{
		internal int mRelatingProcess;// : IfcProcess; 
		public IfcProcess RelatingProcess { get { return mDatabase[mRelatingProcess] as IfcProcess; } set { mRelatingProcess = value.mIndex; } }
		internal IfcRelAssignsToProcess() : base() { }
		internal IfcRelAssignsToProcess(IfcRelAssignsToProcess a) : base(a) { mRelatingProcess = a.mRelatingProcess; }
		internal IfcRelAssignsToProcess(IfcProcess relProcess) : base(relProcess.mDatabase) { mRelatingProcess = relProcess.mIndex; }
		internal IfcRelAssignsToProcess(IfcProcess relProcess, IfcObjectDefinition relObject) : base(relObject) { mRelatingProcess = relProcess.mIndex; }
		internal IfcRelAssignsToProcess(IfcProcess relProcess, List<IfcObjectDefinition> relObjects) : base(relObjects) { mRelatingProcess = relProcess.mIndex; }
		internal static IfcRelAssignsToProcess Parse(string strDef) { IfcRelAssignsToProcess a = new IfcRelAssignsToProcess(); int ipos = 0; parseFields(a, ParserSTEP.SplitLineFields(strDef), ref ipos); return a; }
		internal static void parseFields(IfcRelAssignsToProcess c, List<string> arrFields, ref int ipos) { IfcRelAssigns.parseFields(c, arrFields, ref ipos); c.mRelatingProcess = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildStringSTEP() { return (mDatabase.ModelView == ModelView.Ifc2x3Coordination || mRelatedObjects.Count == 0 ? "" : base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mRelatingProcess)); }
		internal override void postParseRelate()
		{
			base.postParseRelate();
			IfcProcess p = RelatingProcess;
			if (p != null)
				p.mHasAssignments.Add(this);
		}
	}
	public partial class IfcRelAssignsToProduct : IfcRelAssigns
	{
		private int mRelatingProduct;// : IFC4	IfcProductSelect; : IfcProduct; 
		public IfcProductSelect RelatingProduct { get { return mDatabase[mRelatingProduct] as IfcProductSelect; } set { mRelatingProduct = value.Index; value.ReferencedBy.Add(this); } }

		internal IfcRelAssignsToProduct() : base() { }
		internal IfcRelAssignsToProduct(DatabaseIfc db, IfcRelAssignsToProduct r) : base(db,r)
		{
			//mRelatingProduct = a.mRelatingProduct;
		}
		internal IfcRelAssignsToProduct(IfcProductSelect relProduct) : base(relProduct.Database) { RelatingProduct = relProduct; }
		public IfcRelAssignsToProduct(IfcObjectDefinition relObject, IfcProductSelect relProduct) : base(relObject) { RelatingProduct = relProduct; }
		public IfcRelAssignsToProduct(List<IfcObjectDefinition> relObjects, IfcProductSelect relProduct) : base(relObjects) { RelatingProduct = relProduct; }
		internal static IfcRelAssignsToProduct Parse(string strDef) { IfcRelAssignsToProduct a = new IfcRelAssignsToProduct(); int ipos = 0; parseFields(a, ParserSTEP.SplitLineFields(strDef), ref ipos); return a; }
		internal static void parseFields(IfcRelAssignsToProduct c, List<string> arrFields, ref int ipos) { IfcRelAssigns.parseFields(c, arrFields, ref ipos); c.mRelatingProduct = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildStringSTEP() { return (mDatabase.ModelView == ModelView.Ifc2x3Coordination || mRelatedObjects.Count == 0 ? "" : base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mRelatingProduct)); }
		internal override void postParseRelate()
		{
			base.postParseRelate();
			IfcProductSelect p = RelatingProduct;
			if (p != null)
				p.ReferencedBy.Add(this);
		}
	}
	//ENTITY IfcRelAssignsToProjectOrder // DEPRECEATED IFC4 
	public partial class IfcRelAssignsToResource : IfcRelAssigns
	{
		internal int mRelatingResource;// : IfcResource; 
		public IfcResource RelatingResource { get { return mDatabase[mRelatingResource] as IfcResource; } set { mRelatingResource = value.mIndex; } }
		internal IfcRelAssignsToResource() : base() { }
		internal IfcRelAssignsToResource(IfcRelAssignsToResource i) : base(i) { mRelatingResource = i.mRelatingResource; }
		internal IfcRelAssignsToResource(IfcResource relResource) : base(relResource.mDatabase) { mRelatingResource = relResource.mIndex; }
		internal IfcRelAssignsToResource(IfcResource relResource, IfcObjectDefinition relObject) : base(relObject) { mRelatingResource = relResource.mIndex; }
		internal static IfcRelAssignsToResource Parse(string strDef) { IfcRelAssignsToResource a = new IfcRelAssignsToResource(); int ipos = 0; parseFields(a, ParserSTEP.SplitLineFields(strDef), ref ipos); return a; }
		internal static void parseFields(IfcRelAssignsToResource c, List<string> arrFields, ref int ipos) { IfcRelAssigns.parseFields(c, arrFields, ref ipos); c.mRelatingResource = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildStringSTEP() { return (mDatabase.ModelView == ModelView.Ifc2x3Coordination || mRelatedObjects.Count == 0 ? "" : base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mRelatingResource)); }
		internal override void postParseRelate()
		{
			base.postParseRelate();
			IfcResource r = RelatingResource;
			if (r != null)
				r.mResourceOf.Add(this);
		}
	}
	public abstract partial class IfcRelAssociates : IfcRelationship   //ABSTRACT SUPERTYPE OF (ONEOF(IfcRelAssociatesApproval,IfcRelAssociatesclassification,IfcRelAssociatesConstraint,IfcRelAssociatesDocument,IfcRelAssociatesLibrary,IfcRelAssociatesMaterial))
	{
		//internal int mID = 0;
		internal List<int> mRelatedObjects = new List<int>();// : SET [1:?] OF IfcDefinitionSelect IFC2x3 IfcRoot; 
		public List<IfcDefinitionSelect> RelatedObjects
		{
			get { return mRelatedObjects.ConvertAll(x => mDatabase[x] as IfcDefinitionSelect); }
			set
			{
				for (int icounter = 0; icounter < value.Count; icounter++)
				{
					IfcDefinitionSelect ds = value[icounter];
					mRelatedObjects.Add(ds.Index);
					ds.HasAssociations.Add(this);
				}
			}
		}

		protected IfcRelAssociates() : base() { }
		protected IfcRelAssociates(IfcRelAssociates i) : base(i) { mRelatedObjects = new List<int>(i.mRelatedObjects.ToArray()); }
		protected IfcRelAssociates(DatabaseIfc db) : base(db) { }
		protected IfcRelAssociates(DatabaseIfc db, IfcRelAssociates r) : base(db, r)
		{
			
			//RelatedObjects = r.mRelatedObjects.ConvertAll(x => db.Factory.Duplicate(r.mDatabase[x]) as IfcDefinitionSelect);
		}
		protected IfcRelAssociates(IfcDefinitionSelect related) : base(related.Database) { mRelatedObjects.Add(related.Index); related.HasAssociations.Add(this); }
		protected override string BuildStringSTEP()
		{
			if (mRelatedObjects.Count == 0)
				return "";
			string str = base.BuildStringSTEP() + ",(#" + mRelatedObjects[0];
			if (mRelatedObjects.Count > 200)
			{
				StringBuilder sb = new StringBuilder();
				sb.Append(str);
				for (int icounter = 1; icounter < mRelatedObjects.Count; icounter++)
					sb.Append(",#" + mRelatedObjects[icounter]);
				sb.Append(")");
				return sb.ToString();
			}
			else
			{
				for (int icounter = 1; icounter < mRelatedObjects.Count; icounter++)
					str += ",#" + mRelatedObjects[icounter];
				return str + ")";
			}
		}
		protected static void parseFields(IfcRelAssociates a, List<string> arrFields, ref int ipos) { IfcRelationship.parseFields(a, arrFields, ref ipos); a.mRelatedObjects = ParserSTEP.SplitListLinks(arrFields[ipos++]); }
		protected override void parseString(string str, ref int pos)
		{
			base.parseString(str, ref pos);
			mRelatedObjects = ParserSTEP.StripListLink(str, ref pos);
		}
		internal void addAssociation(IfcDefinitionSelect obj) { mRelatedObjects.Add(obj.Index); obj.HasAssociations.Add(this); }
		internal void addAssociation(List<IfcDefinitionSelect> objs)
		{
			for (int icounter = 0; icounter < objs.Count; icounter++)
			{
				IfcDefinitionSelect obj = objs[icounter];
				mRelatedObjects.Add(obj.Index);
				obj.HasAssociations.Add(this);
			}
		}
		internal void unassign(IfcDefinitionSelect d) { mRelatedObjects.Remove(d.Index); d.HasAssociations.Remove(this); }
		internal void unassign(List<IfcDefinitionSelect> ds)
		{
			for (int icounter = 0; icounter < ds.Count; icounter++)
			{
				IfcDefinitionSelect d = ds[icounter];
				mRelatedObjects.Remove(d.Index);
				d.HasAssociations.Remove(this);
			}
		}
		public override string ToString() { return (mRelatedObjects.Count == 0 ? "" : base.ToString()); }
		internal override void postParseRelate()
		{
			base.postParseRelate();
			List<IfcDefinitionSelect> objects = RelatedObjects;
			for (int icounter = 0; icounter < objects.Count; icounter++)
			{
				IfcDefinitionSelect r = objects[icounter];
				if (r != null)
					r.HasAssociations.Add(this);
			}
		}
	}
	//ENTITY IfcRelAssociatesAppliedValue // DEPRECEATED IFC4
	//ENTITY IfcRelAssociatesApproval
	public partial class IfcRelAssociatesClassification : IfcRelAssociates
	{
		internal int mRelatingClassification;// : IfcClassificationSelect; IFC2x3  	IfcClassificationNotationSelect
		public IfcClassificationSelect RelatingClassification
		{
			get { return mDatabase[mRelatingClassification] as IfcClassificationSelect; }
			set { mRelatingClassification = value.Index; value.ClassificationForObjects.Add(this); }
		}

		internal IfcRelAssociatesClassification() : base() { }
		internal IfcRelAssociatesClassification(IfcClassificationSelect cl) : base(cl.Database) { mRelatingClassification = cl.Index; }
		internal IfcRelAssociatesClassification(DatabaseIfc db, IfcRelAssociatesClassification r) : base(db,r)
		{
			RelatingClassification = db.Factory.Duplicate(r.mDatabase[r.mRelatingClassification]) as IfcClassificationSelect;
		}
		internal static IfcRelAssociatesClassification Parse(string strDef) { IfcRelAssociatesClassification a = new IfcRelAssociatesClassification(); int ipos = 0; parseFields(a, ParserSTEP.SplitLineFields(strDef), ref ipos); return a; }
		internal static void parseFields(IfcRelAssociatesClassification a, List<string> arrFields, ref int ipos) { IfcRelAssociates.parseFields(a, arrFields, ref ipos); a.mRelatingClassification = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mRelatingClassification); }
		internal override void postParseRelate()
		{
			base.postParseRelate();
			RelatingClassification.ClassificationForObjects.Add(this);
		}
	}
	public partial class IfcRelAssociatesConstraint : IfcRelAssociates
	{
		internal string mIntent = "$";// :	OPTIONAL IfcLabel;
		private int mRelatingConstraint;// : IfcConstraint

		public string Intent { get { return (mIntent == "$" ? "" : ParserIfc.Decode(mIntent)); } set { mIntent = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public IfcConstraint RelatingConstraint { get { return mDatabase[mRelatingConstraint] as IfcConstraint; } set { mRelatingConstraint = value.mIndex; value.mConstraintForObjects.Add(this); } }

		internal IfcRelAssociatesConstraint() : base() { }
		internal IfcRelAssociatesConstraint(IfcConstraint c) : base(c.mDatabase) { RelatingConstraint = c; }
		internal IfcRelAssociatesConstraint(DatabaseIfc db, IfcRelAssociatesConstraint c) : base(db,c) { RelatingConstraint = db.Factory.Duplicate(c.RelatingConstraint) as IfcConstraint; }
		public IfcRelAssociatesConstraint(IfcDefinitionSelect related, IfcConstraint constraint) : base(related) { RelatingConstraint = constraint; }

		internal static IfcRelAssociatesConstraint Parse(string strDef) { IfcRelAssociatesConstraint a = new IfcRelAssociatesConstraint(); int ipos = 0; parseFields(a, ParserSTEP.SplitLineFields(strDef), ref ipos); return a; }
		internal static void parseFields(IfcRelAssociatesConstraint a, List<string> arrFields, ref int ipos) { IfcRelAssociates.parseFields(a, arrFields, ref ipos); a.mIntent = arrFields[ipos++].Replace("'", ""); a.mRelatingConstraint = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + (mIntent == "$" ? ",$," : ",'" + mIntent + "',") + ParserSTEP.LinkToString(mRelatingConstraint); }
		internal override void postParseRelate()
		{
			base.postParseRelate();
			RelatingConstraint.mConstraintForObjects.Add(this);
		}
	}
	public partial class IfcRelAssociatesDocument : IfcRelAssociates
	{
		internal int mRelatingDocument;// : IfcDocumentSelect; 
		internal IfcRelAssociatesDocument() : base() { }
		internal IfcRelAssociatesDocument(IfcRelAssociatesDocument i) : base(i) { mRelatingDocument = i.mRelatingDocument; }
		public IfcRelAssociatesDocument(IfcDefinitionSelect related, IfcDocumentSelect document) : base(related) { mRelatingDocument = document.Index; }
		internal IfcRelAssociatesDocument(IfcDocumentSelect document) : base(document.Database) { Name = "DocAssoc"; Description = "Document Associates"; mRelatingDocument = document.Index; document.Associates.Add(this); }
		internal static IfcRelAssociatesDocument Parse(string strDef) { IfcRelAssociatesDocument a = new IfcRelAssociatesDocument(); int ipos = 0; parseFields(a, ParserSTEP.SplitLineFields(strDef), ref ipos); return a; }
		internal static void parseFields(IfcRelAssociatesDocument a, List<string> arrFields, ref int ipos) { IfcRelAssociates.parseFields(a, arrFields, ref ipos); a.mRelatingDocument = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mRelatingDocument); }
		internal IfcDocumentSelect getDocument() { return mDatabase[mRelatingDocument] as IfcDocumentSelect; }
	}
	public partial class IfcRelAssociatesLibrary : IfcRelAssociates
	{
		internal string mMatName = "";
		internal int mRelatingLibrary;// : IfcLibrarySelect; 
		internal IfcRelAssociatesLibrary() : base() { }
		internal IfcRelAssociatesLibrary(IfcRelAssociatesLibrary i) : base(i) { mRelatingLibrary = i.mRelatingLibrary; }
		internal IfcRelAssociatesLibrary(DatabaseIfc m, IfcLibrarySelect lib) : base(m) { Name = "LibAssoc"; Description = "Library Associates"; mRelatingLibrary = lib.Index; }
		internal static IfcRelAssociatesLibrary Parse(string strDef) { IfcRelAssociatesLibrary a = new IfcRelAssociatesLibrary(); int ipos = 0; parseFields(a, ParserSTEP.SplitLineFields(strDef), ref ipos); return a; }
		internal static void parseFields(IfcRelAssociatesLibrary a, List<string> arrFields, ref int ipos) { IfcRelAssociates.parseFields(a, arrFields, ref ipos); a.mRelatingLibrary = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mRelatingLibrary); }
	}
	public partial class IfcRelAssociatesMaterial : IfcRelAssociates
	{
		private int mRelatingMaterial;// : IfcMaterialSelect; 
		public IfcMaterialSelect RelatingMaterial
		{
			get { return mDatabase[mRelatingMaterial] as IfcMaterialSelect; }
			set { mRelatingMaterial = (value == null ? 0 : value.Index); if(value != null) value.Associates = this; }
		}

		internal IfcRelAssociatesMaterial() : base() { }
		internal IfcRelAssociatesMaterial(IfcMaterialSelect material) : base(material.Database) { Name = "MatAssoc"; Description = "Material Associates"; mRelatingMaterial = material.Index; material.Associates = this; }
		internal IfcRelAssociatesMaterial(DatabaseIfc db, IfcRelAssociatesMaterial r) : base(db, r) { RelatingMaterial = db.Factory.Duplicate(r.mDatabase[r.mRelatingMaterial]) as IfcMaterialSelect; }
		internal static IfcRelAssociatesMaterial Parse(string strDef)
		{
			IfcRelAssociatesMaterial a = new IfcRelAssociatesMaterial();
			int pos = 0;
			a.parseString(strDef, ref pos);
			a.mRelatingMaterial = ParserSTEP.StripLink(strDef, ref pos);
			return a;
		}
		protected override string BuildStringSTEP() { return (mDatabase.mRelease == ReleaseVersion.IFC2x3 && string.IsNullOrEmpty(RelatingMaterial.ToString()) ? "" : base.BuildStringSTEP() + ",#" + mRelatingMaterial); }
		internal override void postParseRelate()
		{
			base.postParseRelate();
			IfcMaterialSelect ms = RelatingMaterial;
			if (ms != null)
				ms.Associates = this;
		}
	}
	public partial class IfcRelAssociatesProfileProperties : IfcRelAssociates //IFC4 DELETED Replaced by IfcRelAssociatesMaterial together with material-profile sets
	{
		private int mRelatingProfileProperties;// : IfcProfileProperties;
		internal int mProfileSectionLocation;// : OPTIONAL IfcShapeAspect;
		internal double mProfileOrientation;// : OPTIONAL IfcOrientationSelect; //TYPE IfcOrientationSelect = SELECT(IfcPlaneAngleMeasure,IfcDirection);
		private bool mAngle = true;
#warning fix profile orientation select
		public IfcProfileProperties RelatingProfileProperties { get { return mDatabase[mRelatingProfileProperties] as IfcProfileProperties; } }
		public IfcShapeAspect ProfileSectionLocation { get { return mDatabase[mProfileSectionLocation] as IfcShapeAspect; } set { mProfileSectionLocation = value == null ? 0 : value.mIndex; } }
		 
		internal IfcRelAssociatesProfileProperties() : base() { }
		internal IfcRelAssociatesProfileProperties(IfcProfileProperties pp) : base(pp.mDatabase) { if (pp.mDatabase.mRelease != ReleaseVersion.IFC2x3) throw new Exception(KeyWord + " Deleted in IFC4"); mRelatingProfileProperties = pp.mIndex; }
		internal IfcRelAssociatesProfileProperties(DatabaseIfc db, IfcRelAssociatesProfileProperties r) : base(db,r)
		{
			//mRelatingProfileProperties = i.mRelatingProfileProperties; 
			if (r.mProfileSectionLocation > 0)
				ProfileSectionLocation = db.Factory.Duplicate(r.ProfileSectionLocation) as IfcShapeAspect;
			mProfileOrientation = r.mProfileOrientation;
		}
		internal static IfcRelAssociatesProfileProperties Parse(string strDef) { IfcRelAssociatesProfileProperties a = new IfcRelAssociatesProfileProperties(); int ipos = 0; parseFields(a, ParserSTEP.SplitLineFields(strDef), ref ipos); return a; }
		internal static void parseFields(IfcRelAssociatesProfileProperties a, List<string> arrFields, ref int ipos)
		{
			IfcRelAssociates.parseFields(a, arrFields, ref ipos);
			a.mRelatingProfileProperties = ParserSTEP.ParseLink(arrFields[ipos++]);
			a.mProfileSectionLocation = ParserSTEP.ParseLink(arrFields[ipos++]);
			if (arrFields[ipos].StartsWith("IfcPlaneAngleMeasure(", true, System.Globalization.CultureInfo.CurrentCulture))
			{
				string str = arrFields[ipos++];
				a.mProfileOrientation = ParserSTEP.ParseDouble(str.Substring(21, str.Length - 22));
			}
			else
			{
				a.mAngle = false;
				a.mProfileOrientation = ParserSTEP.ParseLink(arrFields[ipos++]);
			}
		}
		protected override string BuildStringSTEP()
		{
			if (mRelatedObjects.Count == 0)
				return "";
			string str = base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mRelatingProfileProperties) + "," +
				ParserSTEP.LinkToString(mProfileSectionLocation) + ",";
			if (mAngle)
			{
				if (mProfileOrientation == 0)
					return str + "$";
				return str + ",IFCPLANEANGLEMEASURE(" + ParserSTEP.DoubleToString(mProfileOrientation) + ")";
			}
			return str + ParserSTEP.LinkToString((int)mProfileOrientation);
		}
	}
	public abstract partial class IfcRelationship : IfcRoot  //ABSTRACT SUPERTYPE OF (ONEOF (IfcRelAssigns ,IfcRelAssociates ,IfcRelConnects ,IfcRelDecomposes ,IfcRelDefines))
	{
		protected IfcRelationship() : base() { }
		protected IfcRelationship(IfcRelationship r) : base(r) { }
		internal IfcRelationship(DatabaseIfc db) : base(db) {  }
		protected IfcRelationship(DatabaseIfc db, IfcRelationship r) : base(db, r) { }
		protected static void parseFields(IfcRelationship r, List<string> arrFields, ref int ipos) { IfcRoot.parseFields(r, arrFields, ref ipos); }
	}
	/*internal class IfcRelaxation : IfcEntity // DEPRECEATED IFC4
	{
		public override string getKW { get { return mKW; } }
		internal new static string mKW = strCrypto.Decrypt("sSev2y4jvspcTXVmK7+oEg=="); //// IFCRELAXATION
		internal int mRelaxationValue;// : IfcNormalisedRatioMeasure;
		internal int mInitialStress;// : IfcNormalisedRatioMeasure; 
		internal IfcRelaxation(IfcRelaxation p) : base() { mRelaxationValue = p.mRelaxationValue; mInitialStress = p.mInitialStress; }
		internal IfcRelaxation() : base() { }
	 	internal new static IfcRelaxation Parse(string strDef) { int ipos = 0; return parseFields(IFCModel.mParser.splitLineFields(strDef), ref ipos); }
		internal new static IfcRelaxation parseFields(List<string> arrFields, ref int ipos)
		{
			IfcRelaxation rm = new IfcRelaxation();
			rm.mRelaxationValue = IFCModel.mParser.parseSTPLink(arrFields[ipos++]);
			rm.mInitialStress = IFCModel.mParser.parseSTPLink(arrFields[ipos++]);
			return rm;
		}
		protected override string BuildString() { return base.BuildString() + "," + IFCModel.mParser.STPLinkToString(mRelaxationValue) + "," + IFCModel.mParser.STPLinkToString(mInitialStress); }
	}*/
	public abstract partial class IfcRelConnects : IfcRelationship //ABSTRACT SUPERTYPE OF (ONEOF (IfcRelConnectsElements ,IfcRelConnectsPortToElement ,IfcRelConnectsPorts ,IfcRelConnectsStructuralActivity ,IfcRelConnectsStructuralMember
	{  //,IfcRelContainedInSpatialStructure ,IfcRelCoversBldgElements ,IfcRelCoversSpaces ,IfcRelFillsElement ,IfcRelFlowControlElements ,IfcRelInterferesElements ,IfcRelReferencedInSpatialStructure ,IfcRelSequence ,IfcRelServicesBuildings ,IfcRelSpaceBoundary))
		protected IfcRelConnects() : base() { }
		protected IfcRelConnects(IfcRelConnects i) : base(i) { }
		protected IfcRelConnects(DatabaseIfc db) : base(db) { }
		protected IfcRelConnects(DatabaseIfc db, IfcRelConnects r) : base(db, r) { }
		protected static void parseFields(IfcRelConnects i, List<string> arrFields, ref int ipos) { IfcRelationship.parseFields(i, arrFields, ref ipos); }
	}
	public partial class IfcRelConnectsElements : IfcRelConnects //	SUPERTYPE OF(ONEOF(IfcRelConnectsPathElements, IfcRelConnectsWithRealizingElements))
	{
		internal int mConnectionGeometry;// : OPTIONAL IfcConnectionGeometry;
		internal int mRelatingElement;// : IfcElement;
		internal int mRelatedElement;// : IfcElement; 

		public IfcConnectionGeometry ConnectionGeometry { get { return mDatabase[mConnectionGeometry] as IfcConnectionGeometry; } set { mConnectionGeometry = (value == null ? 0 : value.mIndex); } }
		public IfcElement RelatingElement { get { return mDatabase[mRelatingElement] as IfcElement; } set { mRelatingElement = value.mIndex;  } }
		public IfcElement RelatedElement { get { return mDatabase[mRelatedElement] as IfcElement; } set { mRelatedElement = value.mIndex; } }

		internal IfcRelConnectsElements() : base() { }
		internal IfcRelConnectsElements(DatabaseIfc db, IfcRelConnectsElements r) : base(db,r)
		{
			if (r.mConnectionGeometry >0)
				ConnectionGeometry = db.Factory.Duplicate(r.ConnectionGeometry) as IfcConnectionGeometry;
			//RelatingElement = db.Factory.Duplicate(r.RelatingElement) as IfcElement;
			//RelatedElement = db.Factory.Duplicate( r.RelatedElement) as IfcElement;
		}
		internal IfcRelConnectsElements(IfcElement relating, IfcElement related)
			: base(relating.mDatabase)
		{
			mRelatingElement = relating.mIndex;
			relating.mConnectedFrom.Add(this);
			mRelatedElement = related.mIndex;
			related.mConnectedTo.Add(this);
		}
		internal IfcRelConnectsElements(IfcConnectionGeometry cg, IfcElement relating, IfcElement related) : this(relating, related) { mConnectionGeometry = cg.mIndex; }
		internal static IfcRelConnectsElements Parse(string strDef) { IfcRelConnectsElements i = new IfcRelConnectsElements(); int ipos = 0; parseFields(i, ParserSTEP.SplitLineFields(strDef), ref ipos); return i; }
		internal static void parseFields(IfcRelConnectsElements i, List<string> arrFields, ref int ipos) { IfcRelConnects.parseFields(i, arrFields, ref ipos); i.mConnectionGeometry = ParserSTEP.ParseLink(arrFields[ipos++]); i.mRelatingElement = ParserSTEP.ParseLink(arrFields[ipos++]); i.mRelatedElement = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildStringSTEP() { return (mRelatingElement == 0 || mRelatedElement == 0 ? "" : base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mConnectionGeometry) + "," + ParserSTEP.LinkToString(mRelatingElement) + "," + ParserSTEP.LinkToString(mRelatedElement)); }
		internal override void postParseRelate()
		{
			base.postParseRelate();
			IfcElement relating = RelatingElement, related = RelatedElement;
			if (relating != null)
				relating.mConnectedFrom.Add(this);
			if (related != null)
				related.mConnectedTo.Add(this);
		}
		internal IfcElement getConnected(IfcElement e) { return mDatabase[(mRelatingElement == e.mIndex ? mRelatedElement : mRelatingElement)] as IfcElement; }
	}
	public partial class IfcRelConnectsPathElements : IfcRelConnectsElements
	{
		internal List<int> mRelatingPriorities = new List<int>();// : LIST [0:?] OF INTEGER;
		internal List<int> mRelatedPriorities = new List<int>();// : LIST [0:?] OF INTEGER;

		public IfcConnectionTypeEnum mRelatedConnectionType = IfcConnectionTypeEnum.NOTDEFINED;// : IfcConnectionTypeEnum;
		public IfcConnectionTypeEnum mRelatingConnectionType = IfcConnectionTypeEnum.NOTDEFINED;// : IfcConnectionTypeEnum; 

		internal IfcRelConnectsPathElements() : base() { }
		internal IfcRelConnectsPathElements(DatabaseIfc db, IfcRelConnectsPathElements r) : base(db,r)
		{
			mRelatingPriorities.AddRange(r.mRelatingPriorities);
			mRelatedPriorities.AddRange(r.mRelatedPriorities);
			mRelatedConnectionType = r.mRelatedConnectionType;
			mRelatingConnectionType = r.mRelatingConnectionType;
		}
		internal new static IfcRelConnectsPathElements Parse(string strDef) { IfcRelConnectsPathElements i = new IfcRelConnectsPathElements(); int ipos = 0; parseFields(i, ParserSTEP.SplitLineFields(strDef), ref ipos); return i; }
		internal static void parseFields(IfcRelConnectsPathElements i, List<string> arrFields, ref int ipos)
		{
			IfcRelConnectsElements.parseFields(i, arrFields, ref ipos);
			i.mRelatingPriorities = ParserSTEP.SplitListLinks(arrFields[ipos++]);
			i.mRelatedPriorities = ParserSTEP.SplitListLinks(arrFields[ipos++]);
			i.mRelatedConnectionType = (IfcConnectionTypeEnum)Enum.Parse(typeof(IfcConnectionTypeEnum), arrFields[ipos++].Replace(".", ""));
			i.mRelatingConnectionType = (IfcConnectionTypeEnum)Enum.Parse(typeof(IfcConnectionTypeEnum), arrFields[ipos++].Replace(".", ""));
		}
		protected override string BuildStringSTEP()
		{
			if (mRelatingElement == 0 || mRelatedElement == 0)
				return "";
			string str = base.BuildStringSTEP() + ",(";
			if (mRelatingPriorities.Count > 0)
			{
				str += ParserSTEP.LinkToString(mRelatingPriorities[0]);
				for (int icounter = 1; icounter < mRelatingPriorities.Count; icounter++)
					str += "," + ParserSTEP.LinkToString(mRelatingPriorities[icounter]);
			}
			str += "),(";
			if (mRelatedPriorities.Count > 0)
			{
				str += ParserSTEP.LinkToString(mRelatedPriorities[0]);
				for (int icounter = 1; icounter < mRelatedPriorities.Count; icounter++)
					str += "," + ParserSTEP.LinkToString(mRelatedPriorities[icounter]);
			}
			return str + "),." + mRelatedConnectionType.ToString() + ".,." + mRelatingConnectionType.ToString() + ".";
		}
	}
	public partial class IfcRelConnectsPorts : IfcRelConnects
	{
		internal int mRelatingPort;// : IfcPort;
		internal int mRelatedPort;// : IfcPort;
		internal int mRealizingElement;// : OPTIONAL IfcElement; 

		public IfcPort RelatingPort { get { return mDatabase[mRelatingPort] as IfcPort; } }
		public IfcPort RelatedPort { get { return mDatabase[mRelatedPort] as IfcPort; } }
		public IfcElement RealizingElement { get { return mDatabase[mRealizingElement] as IfcElement; } }

		internal IfcRelConnectsPorts() : base() { }
		internal IfcRelConnectsPorts(IfcRelConnectsPorts c) : base(c) { mRelatingPort = c.mRelatingPort; mRelatedPort = c.mRelatedPort; mRealizingElement = c.mRealizingElement; }
		internal IfcRelConnectsPorts(IfcPort relatingPort, IfcPort relatedPort, IfcElement realizingElement)
			: base(relatingPort.mDatabase) { mRelatingPort = relatingPort.mIndex; mRelatedPort = relatedPort.mIndex; if (realizingElement != null) mRealizingElement = realizingElement.mIndex; }
		internal static IfcRelConnectsPorts Parse(string strDef) { IfcRelConnectsPorts i = new IfcRelConnectsPorts(); int ipos = 0; parseFields(i, ParserSTEP.SplitLineFields(strDef), ref ipos); return i; }
		internal static void parseFields(IfcRelConnectsPorts i, List<string> arrFields, ref int ipos)
		{
			IfcRelConnects.parseFields(i, arrFields, ref ipos);
			i.mRelatingPort = ParserSTEP.ParseLink(arrFields[ipos++]);
			i.mRelatedPort = ParserSTEP.ParseLink(arrFields[ipos++]);
			i.mRealizingElement = ParserSTEP.ParseLink(arrFields[ipos++]);
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mRelatingPort) + "," + ParserSTEP.LinkToString(mRelatedPort) + "," + ParserSTEP.LinkToString(mRealizingElement); }
		internal override void postParseRelate()
		{
			base.postParseRelate();
			RelatingPort.mConnectedFrom = this;
			RelatedPort.mConnectedTo = this;
		}
		internal IfcPort getOtherPort(IfcPort p) { return (mRelatedPort == p.mIndex ? mDatabase[mRelatingPort] as IfcPort : mDatabase[mRelatedPort] as IfcPort); }
	}
	public partial class IfcRelConnectsPortToElement : IfcRelConnects
	{
		internal int mRelatingPort;// : IfcPort;
		internal int mRelatedElement;// : IfcElement; 

		public IfcPort RelatingPort { get { return mDatabase[mRelatingPort] as IfcPort; } }
		public IfcElement RelatedElement { get { return mDatabase[mRelatedElement] as IfcElement; } }

		internal IfcRelConnectsPortToElement() : base() { }
		internal IfcRelConnectsPortToElement(IfcRelConnectsPortToElement c) : base(c) { mRelatingPort = c.mRelatingPort; mRelatedElement = c.mRelatedElement; }
		internal IfcRelConnectsPortToElement(IfcPort p, IfcElement e) : base(p.mDatabase)
		{
			mRelatingPort = p.mIndex;
			p.mContainedIn = this;
			mRelatedElement = e.mIndex;
			e.mHasPorts.Add(this);
		}
		internal static IfcRelConnectsPortToElement Parse(string strDef) { IfcRelConnectsPortToElement i = new IfcRelConnectsPortToElement(); int ipos = 0; parseFields(i, ParserSTEP.SplitLineFields(strDef), ref ipos); return i; }
		internal static void parseFields(IfcRelConnectsPortToElement i, List<string> arrFields, ref int ipos) { IfcRelConnects.parseFields(i, arrFields, ref ipos); i.mRelatingPort = ParserSTEP.ParseLink(arrFields[ipos++]); i.mRelatedElement = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mRelatingPort) + "," + ParserSTEP.LinkToString(mRelatedElement); }
		internal override void postParseRelate()
		{
			base.postParseRelate();
			RelatingPort.mContainedIn = this;
			RelatedElement.mHasPorts.Add(this);
		}
	}
	public partial class IfcRelConnectsStructuralActivity : IfcRelConnects
	{
		private int mRelatingElement;// : IfcStructuralActivityAssignmentSelect; SELECT(IfcStructuralItem,IfcElement);
		private int mRelatedStructuralActivity;// : IfcStructuralActivity; 

		public IfcStructuralActivityAssignmentSelect RelatingElement { get { return mDatabase[mRelatingElement] as IfcStructuralActivityAssignmentSelect; } set { mRelatingElement = value.Index; value.AssignedStructuralActivity.Add(this); } }
		public IfcStructuralActivity RelatedStructuralActivity { get { return mDatabase[mRelatedStructuralActivity] as IfcStructuralActivity; } set { mRelatedStructuralActivity = value.Index; } }

		internal IfcRelConnectsStructuralActivity() : base() { }
		internal IfcRelConnectsStructuralActivity(DatabaseIfc db, IfcRelConnectsStructuralActivity c) : base(db,c)
		{
			//mRelatingElement = c.mRelatingElement; 
			RelatedStructuralActivity = db.Factory.Duplicate(c.RelatedStructuralActivity) as IfcStructuralActivity;
		}
		internal IfcRelConnectsStructuralActivity(IfcStructuralActivityAssignmentSelect item, IfcStructuralActivity a)
			: base(a.mDatabase) { mRelatingElement = item.Index; mRelatedStructuralActivity = a.mIndex; }
		internal static IfcRelConnectsStructuralActivity Parse(string strDef) { IfcRelConnectsStructuralActivity i = new IfcRelConnectsStructuralActivity(); int ipos = 0; parseFields(i, ParserSTEP.SplitLineFields(strDef), ref ipos); return i; }
		internal static void parseFields(IfcRelConnectsStructuralActivity i, List<string> arrFields, ref int ipos) { IfcRelConnects.parseFields(i, arrFields, ref ipos); i.mRelatingElement = ParserSTEP.ParseLink(arrFields[ipos++]); i.mRelatedStructuralActivity = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mRelatingElement) + "," + ParserSTEP.LinkToString(mRelatedStructuralActivity); }
		internal override void postParseRelate()
		{
			base.postParseRelate();
			RelatedStructuralActivity.AssignedToStructuralItem = this;
			IfcStructuralActivityAssignmentSelect saa = RelatingElement;
			IfcStructuralItem si = saa as IfcStructuralItem;
			if (si != null)
				si.mAssignedStructuralActivity.Add(this);
			else
			{
				IfcElement element = saa as IfcElement;
				if (element != null)
					element.mAssignedStructuralActivity.Add(this);
			}
		}
	}
	public partial class IfcRelConnectsStructuralElement : IfcRelConnects //DELETED IFC4 Replaced by IfcRelAssignsToProduct
	{
		internal int mRelatingElement;// : IfcElement;
		internal int mRelatedStructuralMember;// : IfcStructuralMember; 

		public IfcElement RelatingElement { get { return mDatabase[mRelatingElement] as IfcElement; } set { mRelatingElement = value.mIndex; value.mHasStructuralMember.Add(this); } }
		public IfcStructuralMember RelatedStructuralMember { get { return mDatabase[mRelatedStructuralMember] as IfcStructuralMember; } set { mRelatedStructuralMember = value.mIndex; value.mStructuralMemberForGG = this; } }

		internal IfcRelConnectsStructuralElement() : base() { }
		internal IfcRelConnectsStructuralElement(DatabaseIfc db, IfcRelConnectsStructuralElement c) : base(c)
		{
			//RelatingElement = c.mRelatingElement; 
			RelatedStructuralMember = db.Factory.Duplicate(c.RelatedStructuralMember) as IfcStructuralMember;
		}
		internal IfcRelConnectsStructuralElement(IfcElement elem, IfcStructuralMember memb) : base(elem.mDatabase)
		{
			if (elem.mDatabase.mRelease != ReleaseVersion.IFC2x3)
				throw new Exception(KeyWord + " Deleted IFC4!");
			RelatingElement = elem;
			RelatedStructuralMember = memb;
		}
		internal static IfcRelConnectsStructuralElement Parse(string strDef) { IfcRelConnectsStructuralElement i = new IfcRelConnectsStructuralElement(); int ipos = 0; parseFields(i, ParserSTEP.SplitLineFields(strDef), ref ipos); return i; }
		internal static void parseFields(IfcRelConnectsStructuralElement i, List<string> arrFields, ref int ipos) { IfcRelConnects.parseFields(i, arrFields, ref ipos); i.mRelatingElement = ParserSTEP.ParseLink(arrFields[ipos++]); i.mRelatedStructuralMember = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mRelatingElement) + "," + ParserSTEP.LinkToString(mRelatedStructuralMember); }
		internal override void postParseRelate()
		{
			base.postParseRelate();
			IfcElement element = RelatingElement;
			if (element != null)
				element.mHasStructuralMember.Add(this);
			IfcStructuralMember member = RelatedStructuralMember;
			if(member != null)
				member.mStructuralMemberForGG = this;
		}
	}
	public partial class IfcRelConnectsStructuralMember : IfcRelConnects
	{
		internal int mRelatingStructuralMember;// : IfcStructuralMember;
		internal int mRelatedStructuralConnection;// : IfcStructuralConnection;
		internal int mAppliedCondition;// : OPTIONAL IfcBoundaryCondition;
		internal int mAdditionalConditions;// : OPTIONAL IfcStructuralConnectionCondition;
		private double mSupportedLength;// : OPTIONAL IfcLengthMeasure;
		internal int mConditionCoordinateSystem; // : OPTIONAL IfcAxis2Placement3D; 

		public IfcStructuralMember RelatingStructuralMember { get { return mDatabase[mRelatingStructuralMember] as IfcStructuralMember; } set { mRelatingStructuralMember = value.mIndex; value.mConnectedBy.Add(this); } }
		public IfcStructuralConnection RelatedStructuralConnection { get { return mDatabase[mRelatedStructuralConnection] as IfcStructuralConnection; } set { mRelatedStructuralConnection = value.mIndex; value.mConnectsStructuralMembers.Add(this); } }
		public IfcBoundaryCondition AppliedCondition { get { return mDatabase[mAppliedCondition] as IfcBoundaryCondition; } set { mAppliedCondition = (value == null ? 0 : value.mIndex); } }
		public IfcStructuralConnectionCondition AdditionalConditions { get { return mDatabase[mAdditionalConditions] as IfcStructuralConnectionCondition; } set { mAdditionalConditions = (value == null ? 0 : value.mIndex); } }
		public double SupportedLength { get { return mSupportedLength; } set { mSupportedLength = value; } }
		public IfcAxis2Placement3D ConditionCoordinateSystem { get { return mDatabase[mConditionCoordinateSystem] as IfcAxis2Placement3D; } set { mConditionCoordinateSystem = (value == null ? 0 : value.mIndex); } }

		internal IfcRelConnectsStructuralMember() : base() { }
		internal IfcRelConnectsStructuralMember(DatabaseIfc db, IfcRelConnectsStructuralMember r) : base(db,r)
		{
			//RelatingStructuralMember = db.Factory.Duplicate(r.RelatingStructuralMember) as IfcStructuralMember; 
			RelatedStructuralConnection = db.Factory.Duplicate(r.RelatedStructuralConnection) as IfcStructuralConnection;
			if(r.mAppliedCondition > 0)
				AppliedCondition = db.Factory.Duplicate(r.AppliedCondition) as IfcBoundaryCondition;
			if(r.mAdditionalConditions > 0)
				AdditionalConditions = db.Factory.Duplicate(r.AdditionalConditions) as IfcStructuralConnectionCondition;
			mSupportedLength = r.mSupportedLength;
			if(r.mConditionCoordinateSystem > 0)
				ConditionCoordinateSystem = db.Factory.Duplicate( r.ConditionCoordinateSystem) as IfcAxis2Placement3D;
		}
		internal IfcRelConnectsStructuralMember(IfcStructuralMember member, IfcStructuralConnection connection) : base(member.mDatabase)
		{
			RelatingStructuralMember = member;
			RelatedStructuralConnection = connection;
		}

		internal static IfcRelConnectsStructuralMember Parse(string strDef) { IfcRelConnectsStructuralMember i = new IfcRelConnectsStructuralMember(); int ipos = 0; parseFields(i, ParserSTEP.SplitLineFields(strDef), ref ipos); return i; }
		internal static void parseFields(IfcRelConnectsStructuralMember i, List<string> arrFields, ref int ipos)
		{
			IfcRelConnects.parseFields(i, arrFields, ref ipos);
			i.mRelatingStructuralMember = ParserSTEP.ParseLink(arrFields[ipos++]);
			i.mRelatedStructuralConnection = ParserSTEP.ParseLink(arrFields[ipos++]);
			i.mAppliedCondition = ParserSTEP.ParseLink(arrFields[ipos++]);
			i.mAdditionalConditions = ParserSTEP.ParseLink(arrFields[ipos++]);
			i.mSupportedLength = ParserSTEP.ParseDouble(arrFields[ipos++]);
			i.mConditionCoordinateSystem = ParserSTEP.ParseLink(arrFields[ipos++]);
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mRelatingStructuralMember) + "," + ParserSTEP.LinkToString(mRelatedStructuralConnection) + "," + ParserSTEP.LinkToString(mAppliedCondition) + "," + ParserSTEP.LinkToString(mAdditionalConditions) + "," + ParserSTEP.DoubleToString(mSupportedLength) + "," + ParserSTEP.LinkToString(mConditionCoordinateSystem); }
		internal override void postParseRelate()
		{
			base.postParseRelate();
			IfcStructuralMember m = RelatingStructuralMember;
			if (m != null)
				m.mConnectedBy.Add(this);
			IfcStructuralConnection c = mDatabase[mRelatedStructuralConnection] as IfcStructuralConnection;
			if (c != null)
				c.mConnectsStructuralMembers.Add(this);
		}

		public static IfcRelConnectsStructuralMember Create(IfcStructuralCurveMember member,IfcStructuralPointConnection point, bool atStart, IfcStructuralCurveMember.ExtremityAttributes atts)
		{
			string desc = atStart ? "Start":"End";
			if (atts == null)
				return new IfcRelConnectsStructuralMember(member, point) { Description = desc };
			double tol = member.mDatabase.Tolerance;
			if (atts.Eccentricity != null && (Math.Abs( atts.Eccentricity.Item1) > tol || Math.Abs(atts.Eccentricity.Item2) > tol || Math.Abs(atts.Eccentricity.Item3) > tol))
				return new IfcRelConnectsWithEccentricity(member, point, new IfcConnectionPointEccentricity(point.Vertex, atts.Eccentricity.Item1, atts.Eccentricity.Item2, atts.Eccentricity.Item3)) { Description = desc , AppliedCondition = atts.BoundaryCondition,AdditionalConditions = atts.StructuralConnectionCondition, SupportedLength = atts.SupportedLength, ConditionCoordinateSystem = atts.ConditionCoordinateSystem };
			return new IfcRelConnectsStructuralMember(member, point) { Description = desc, AppliedCondition = atts.BoundaryCondition, AdditionalConditions = atts.StructuralConnectionCondition, SupportedLength = atts.SupportedLength, ConditionCoordinateSystem = atts.ConditionCoordinateSystem } ;
		}
	}
	public partial class IfcRelConnectsWithEccentricity : IfcRelConnectsStructuralMember
	{
		internal int mConnectionConstraint;// : IfcConnectionGeometry
		public IfcConnectionGeometry ConnectionConstraint { get { return mDatabase[mConnectionConstraint] as IfcConnectionGeometry; } set { mConnectionConstraint = value.mIndex; } }

		internal IfcRelConnectsWithEccentricity() : base() { }
		internal IfcRelConnectsWithEccentricity(DatabaseIfc db, IfcRelConnectsWithEccentricity c) : base(db,c) { ConnectionConstraint = db.Factory.Duplicate(c.ConnectionConstraint) as IfcConnectionGeometry; }
		internal IfcRelConnectsWithEccentricity(IfcStructuralMember memb, IfcStructuralConnection connection, IfcConnectionGeometry cg)
			: base(memb, connection) { mConnectionConstraint = cg.mIndex; }
		internal new static IfcRelConnectsWithEccentricity Parse(string strDef) { IfcRelConnectsWithEccentricity i = new IfcRelConnectsWithEccentricity(); int ipos = 0; parseFields(i, ParserSTEP.SplitLineFields(strDef), ref ipos); return i; }
		internal static void parseFields(IfcRelConnectsWithEccentricity i, List<string> arrFields, ref int ipos) { IfcRelConnectsStructuralMember.parseFields(i, arrFields, ref ipos); i.mConnectionConstraint = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mConnectionConstraint); }
	}
	public partial class IfcRelConnectsWithRealizingElements : IfcRelConnectsElements
	{
		internal List<int> mRealizingElements = new List<int>();// :	SET [1:?] OF IfcElement;
		internal string mConnectionType = "$";// : :	OPTIONAL IfcLabel; 

		public List<IfcElement> RealizingElements { get { return mRealizingElements.ConvertAll(x => mDatabase[x] as IfcElement); } set { mRealizingElements = value.ConvertAll(x => x.mIndex); }  }
		
		internal IfcRelConnectsWithRealizingElements() : base() { }
		internal IfcRelConnectsWithRealizingElements(DatabaseIfc db, IfcRelConnectsWithRealizingElements r) : base(db,r) { RealizingElements = r.RealizingElements.ConvertAll(x => db.Factory.Duplicate(x) as IfcElement); mConnectionType = r.mConnectionType;  }
		internal IfcRelConnectsWithRealizingElements(IfcConnectionGeometry cg, IfcElement relating, IfcElement related, IfcElement realizing, string connection)
			: base(relating, related)
		{
			ConnectionGeometry = cg;
			mRealizingElements.Add(realizing.mIndex);
			if (!string.IsNullOrEmpty(connection))
				mConnectionType = connection.Replace("'", "");
		}
		internal new static IfcRelConnectsWithRealizingElements Parse(string strDef) { IfcRelConnectsWithRealizingElements i = new IfcRelConnectsWithRealizingElements(); int ipos = 0; parseFields(i, ParserSTEP.SplitLineFields(strDef), ref ipos); return i; }
		internal static void parseFields(IfcRelConnectsWithRealizingElements i, List<string> arrFields, ref int ipos) { IfcRelConnects.parseFields(i, arrFields, ref ipos); i.mRealizingElements = ParserSTEP.SplitListLinks(arrFields[ipos++]); i.mConnectionType = arrFields[ipos++].Replace("'", ""); }
		protected override string BuildStringSTEP()
		{
			if (mRealizingElements.Count == 0)
				return "";
			string str = base.BuildStringSTEP() + ",(" + ParserSTEP.LinkToString(mRealizingElements[0]);
			for (int icounter = 1; icounter < mRealizingElements.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mRealizingElements[icounter]);
			return str + (mConnectionType == "$" ? "),$" : "),'" + mConnectionType + "'");
		}
		internal override void postParseRelate()
		{
			base.postParseRelate();
			for (int icounter = 0; icounter < mRealizingElements.Count; icounter++)
			{
				IfcElement e = mDatabase[mRealizingElements[icounter]] as IfcElement;
				if (e != null)
					e.mIsConnectionRealization.Add(this);
			}
		}
	}
	public partial class IfcRelContainedInSpatialStructure : IfcRelConnects
	{
		internal List<int> mRelatedElements = new List<int>();// : SET [1:?] OF IfcProduct;
		private int mRelatingStructure;//  IfcSpatialElement 

		public List<IfcProduct> RelatedElements { get { return (mRelatedElements.Count == 0 ? new List<IfcProduct>() : mRelatedElements.ConvertAll(x => mDatabase[x] as IfcProduct)); } set { mRelatedElements = value.ConvertAll(x => x.mIndex);  foreach (IfcProduct p in value) relate(p); } }
		public IfcSpatialElement RelatingStructure { get { return mDatabase[mRelatingStructure] as IfcSpatialElement; } set { mRelatingStructure = value.mIndex; value.mContainsElements.Add(this); } }

		internal IfcRelContainedInSpatialStructure() : base() { }
		internal IfcRelContainedInSpatialStructure(DatabaseIfc db, IfcRelContainedInSpatialStructure r,bool downstream) : base(db, r)
		{
			if(downstream)
				RelatedElements = r.RelatedElements.ConvertAll(x => db.Factory.Duplicate(x) as IfcProduct);
			db.Factory.Duplicate(RelatingStructure,false);
		}
		internal IfcRelContainedInSpatialStructure(IfcSpatialElement e) : base(e.mDatabase)
		{
			string containerName = "";
			if (e as IfcBuilding != null)
				containerName = "Building";
			else if (e as IfcBuildingStorey != null)
				containerName = "BuildingStorey";
			else if (e as IfcExternalSpatialElement != null)
				containerName = "ExternalSpatialElement";
			else if (e as IfcSite != null)
				containerName = "Site";
			else if (e as IfcSpace != null)
				containerName = "Space";
			Name = containerName;
			Description = containerName + " Container for Elements";
			mRelatingStructure = e.mIndex;
			e.mContainsElements.Add(this);
		}
		protected override string BuildStringSTEP()
		{
			if (mRelatedElements.Count <= 0)
				return "";
			string list = "";
			int icounter;
			if (mRelatedElements.Count > 100)
			{
				StringBuilder sb = new StringBuilder();
				for (icounter = 0; icounter < mRelatedElements.Count; icounter++)
				{
					if (!string.IsNullOrEmpty(mDatabase[mRelatedElements[icounter]].ToString()))
					{
						sb.Append(",(#" + mRelatedElements[0]);
						break;
					}
				}
				for (icounter++; icounter < mRelatedElements.Count; icounter++)
				{
					if (!string.IsNullOrEmpty(mDatabase[mRelatedElements[icounter]].ToString()))
						sb.Append(",#" + mRelatedElements[icounter]);
				}
				list = sb.ToString();
			}
			else
			{
				for (icounter = 0; icounter < mRelatedElements.Count; icounter++)
				{
					if (!string.IsNullOrEmpty(mDatabase[mRelatedElements[icounter]].ToString()))
					{
						list = ",(#" + mRelatedElements[0];
						break;
					}

				}
				for (icounter++; icounter < mRelatedElements.Count; icounter++)
				{
					if (!string.IsNullOrEmpty(mDatabase[mRelatedElements[icounter]].ToString()))
						list += ",#" + mRelatedElements[icounter];
				}
			}
			return base.BuildStringSTEP() + list + "),#" + mRelatingStructure;
		}
		internal static IfcRelContainedInSpatialStructure Parse(string strDef)
		{
			IfcRelContainedInSpatialStructure c = new IfcRelContainedInSpatialStructure();
			int pos = 0;
			c.parseString(strDef, ref pos);
			c.mRelatedElements = ParserSTEP.StripListLink(strDef, ref pos);
			c.mRelatingStructure = ParserSTEP.StripLink(strDef, ref pos);
			return c;
		}
		internal override void postParseRelate()
		{
			base.postParseRelate();
			IfcSpatialElement se = RelatingStructure;
			if (se != null)
				se.mContainsElements.Add(this);
			List<IfcProduct> products = RelatedElements;
			for (int icounter = 0; icounter < products.Count; icounter++)
				relate( products[icounter] as IfcProduct);
		}
		internal void add(IfcProduct product) { mRelatedElements.Add(product.mIndex); relate(product); }
		private void relate(IfcProduct product)
		{
			IfcElement element = product as IfcElement;
			if (element != null)
				element.mContainedInStructure = this;
			else
			{
				IfcGrid grid = product as IfcGrid;
				if (grid != null)
					grid.mContainedInStructure = this;
				else
				{
					IfcAnnotation annotation = product as IfcAnnotation;
					if (annotation != null)
						annotation.mContainedInStructure = this;
				}
			}
		}
		internal List<IfcBuildingElement> getBuildingElements()
		{
			List<IfcProduct> ps = RelatedElements;
			List<IfcBuildingElement> result = new List<IfcBuildingElement>(ps.Count);
			for (int icounter = 0; icounter < ps.Count; icounter++)
			{
				IfcBuildingElement e = ps[icounter] as IfcBuildingElement;
				if (e != null)
					result.Add(e);
			}
			return result;
		}
		internal void removeObject(IfcElement e)
		{
			e.mContainedInStructure = null;
			mRelatedElements.Remove(e.mIndex);
		}
	}
	public partial class IfcRelCoversBldgElements : IfcRelConnects //IFC4 DEPRECATION  The relationship IfcRelCoversBldgElements shall not be used anymore, use IfcRelAggregates instead.
	{
		internal int mRelatingBuildingElement;// :	IfcElement;  
		private List<int> mRelatedCoverings = new List<int>();// : SET [1:?] OF IfcCovering;

		public IfcElement RelatingBuildingElement { get { return mDatabase[mRelatingBuildingElement] as IfcElement; } }
		public List<IfcCovering> RelatedCoverings { get { return mRelatedCoverings.ConvertAll(x => mDatabase[x] as IfcCovering); } }

		internal IfcRelCoversBldgElements() : base() { }
		internal IfcRelCoversBldgElements(IfcRelCoversBldgElements c) : base(c) { mRelatingBuildingElement = c.mRelatingBuildingElement; mRelatedCoverings = new List<int>(c.mRelatedCoverings.ToArray()); }
		internal IfcRelCoversBldgElements(IfcElement e, IfcCovering covering) : base(e.mDatabase)
		{
			mRelatingBuildingElement = e.mIndex;
			e.mHasCoverings.Add(this);
			if (covering != null)
			{
				mRelatedCoverings.Add(covering.mIndex);
				covering.mCoversElements = this;
			}
		}
		internal IfcRelCoversBldgElements(IfcElement e, List<IfcCovering> coverings) : base(e.mDatabase)
		{
			mRelatingBuildingElement = e.mIndex;
			e.mHasCoverings.Add(this);
			for (int icounter = 0; icounter < coverings.Count; icounter++)
			{
				mRelatedCoverings.Add(coverings[icounter].mIndex);
				coverings[icounter].mCoversElements = this;
			}
		}
		internal static IfcRelCoversBldgElements Parse(string strDef) { IfcRelCoversBldgElements i = new IfcRelCoversBldgElements(); int ipos = 0; parseFields(i, ParserSTEP.SplitLineFields(strDef), ref ipos); return i; }
		internal static void parseFields(IfcRelCoversBldgElements i, List<string> arrFields, ref int ipos) { IfcRelConnects.parseFields(i, arrFields, ref ipos); i.mRelatingBuildingElement = ParserSTEP.ParseLink(arrFields[ipos++]); i.mRelatedCoverings = ParserSTEP.SplitListLinks(arrFields[ipos++]); }
		protected override string BuildStringSTEP()
		{
			string str = base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mRelatingBuildingElement) + ",(";
			if (mRelatedCoverings.Count > 0)
			{
				str += ParserSTEP.LinkToString(mRelatedCoverings[0]);
				for (int icounter = 1; icounter < mRelatedCoverings.Count; icounter++)
					str += "," + ParserSTEP.LinkToString(mRelatedCoverings[icounter]);
			}
			else
				return "";
			return str + ")";
		}
		internal override void postParseRelate()
		{
			base.postParseRelate();
			IfcElement e = RelatingBuildingElement;
			if (e != null)
				e.mHasCoverings.Add(this);
			List<IfcCovering> coverings = RelatedCoverings;
			for (int icounter = 0; icounter < coverings.Count; icounter++)
				coverings[icounter].mCoversElements = this;
		}
		internal void Remove(IfcCovering c) { mRelatedCoverings.Remove(c.mIndex); c.mHasCoverings.Remove(this); }
		internal void addCovering(IfcCovering c) { c.mCoversElements = this; mRelatedCoverings.Add(c.mIndex); }
	}
	public partial class IfcRelCoversSpaces : IfcRelConnects //IFC4 DEPRECATION  The relationship IfcRelCoversSpace shall not be used anymore, use IfcRelContainedInSpatialStructure instead.
	{
		internal int mRelatedSpace;// : IfcSpace;
		internal List<int> mRelatedCoverings = new List<int>();// SET [1:?] OF IfcCovering; 

		public IfcSpace RelatedSpace { get { return mDatabase[mRelatedSpace] as IfcSpace; } }
		public List<IfcCovering> RelatedCoverings { get { return mRelatedCoverings.ConvertAll(x => mDatabase[x] as IfcCovering); } }

		internal IfcRelCoversSpaces() : base() { }
		internal IfcRelCoversSpaces(IfcRelCoversSpaces c) : base(c) { mRelatedSpace = c.mRelatedSpace; mRelatedCoverings = new List<int>(c.mRelatedCoverings.ToArray()); }
		internal IfcRelCoversSpaces(IfcSpace s, IfcCovering covering) : base(s.mDatabase)
		{
			mRelatedSpace = s.mIndex;
			s.mHasCoverings.Add(this);
			if (covering != null)
			{
				mRelatedCoverings.Add(covering.mIndex);
				covering.mCoversSpaces = this;
			}
		}
		internal static IfcRelCoversSpaces Parse(string strDef) { IfcRelCoversSpaces i = new IfcRelCoversSpaces(); int ipos = 0; parseFields(i, ParserSTEP.SplitLineFields(strDef), ref ipos); return i; }
		internal static void parseFields(IfcRelCoversSpaces i, List<string> arrFields, ref int ipos) { IfcRelConnects.parseFields(i, arrFields, ref ipos); i.mRelatedSpace = ParserSTEP.ParseLink(arrFields[ipos++]); i.mRelatedCoverings = ParserSTEP.SplitListLinks(arrFields[ipos++]); }
		protected override string BuildStringSTEP()
		{
			if (mRelatedCoverings.Count == 0)
				return "";
			string str = base.BuildStringSTEP() + ",#" + mRelatedSpace + ",(#" + mRelatedCoverings[0];
			for (int icounter = 1; icounter < mRelatedCoverings.Count; icounter++)
				str += ",#" + mRelatedCoverings[icounter];
			return str + ")";
		}
	
		internal void addCovering(IfcCovering c) { c.mCoversSpaces = this; mRelatedCoverings.Add(c.mIndex); }
		internal override void postParseRelate()
		{
			base.postParseRelate();
			RelatedSpace.mHasCoverings.Add(this);
			for (int icounter = 0; icounter < mRelatedCoverings.Count; icounter++)
			{
				IfcCovering cov = mDatabase[mRelatedCoverings[icounter]] as IfcCovering;
				if (cov != null)
					cov.mCoversSpaces = this;
			}
		}
	}
	public partial class IfcRelDeclares : IfcRelationship //IFC4
	{
		private int mRelatingContext;// : 	IfcContext;
		private List<int> mRelatedDefinitions = new List<int>();// :	SET [1:?] OF IfcDefinitionSelect; 

		public IfcContext RelatingContext { get { return mDatabase[mRelatingContext] as IfcContext; } set { mRelatingContext = value.mIndex; if (!value.mDeclares.Contains(this)) value.mDeclares.Add(this); } }
		public List<IfcDefinitionSelect> RelatedDefinitions { get { return mRelatedDefinitions.ConvertAll(x => mDatabase[x] as IfcDefinitionSelect); } set { mRelatedDefinitions = value.ConvertAll(x => x.Index); } }

		internal IfcRelDeclares() : base() { }
		internal IfcRelDeclares(IfcContext c) : base(c.mDatabase) { mRelatingContext = c.mIndex; c.mDeclares.Add(this); }
		internal IfcRelDeclares(DatabaseIfc db, IfcRelDeclares r,bool downStream) : base(db,r)
		{
			RelatingContext = db.Factory.Duplicate(r.RelatingContext, false) as IfcContext; 
			if(downStream)
				RelatedDefinitions = r.mRelatedDefinitions.ConvertAll(x => db.Factory.Duplicate( r.mDatabase[x]) as IfcDefinitionSelect);
		}
		public IfcRelDeclares(IfcContext c, IfcDefinitionSelect def) : this(c, new List<IfcDefinitionSelect>() { def }) { }
		public IfcRelDeclares(IfcContext c, List<IfcDefinitionSelect> defs) : this(c)
		{
			for (int icounter = 0; icounter < defs.Count; icounter++)
				AddDefinition(defs[icounter]);
		}
		internal void AddRelated(IfcDefinitionSelect d) { mRelatedDefinitions.Add(d.Index); d.HasContext = this; }
		internal void RemoveRelated(IfcDefinitionSelect d) { mRelatedDefinitions.Remove(d.Index); d.HasContext = null; }
		internal static IfcRelDeclares Parse(string strDef) { IfcRelDeclares i = new IfcRelDeclares(); int ipos = 0; parseFields(i, ParserSTEP.SplitLineFields(strDef), ref ipos); return i; }
		internal static void parseFields(IfcRelDeclares i, List<string> arrFields, ref int ipos) { IfcRelationship.parseFields(i, arrFields, ref ipos); i.mRelatingContext = ParserSTEP.ParseLink(arrFields[ipos++]); i.mRelatedDefinitions = ParserSTEP.SplitListLinks(arrFields[ipos++]); }
		protected override string BuildStringSTEP()
		{
			if (mDatabase.mRelease == ReleaseVersion.IFC2x3 || mRelatingContext == 0 || mRelatedDefinitions.Count == 0)
				return "";
			string str = ",(" + ParserSTEP.LinkToString(mRelatedDefinitions[0]);
			for (int icounter = 1; icounter < mRelatedDefinitions.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mRelatedDefinitions[icounter]);
			return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mRelatingContext) + str + ")";
		}
		internal override void postParseRelate()
		{
			base.postParseRelate();
			RelatingContext.mDeclares.Add(this);
			for (int icounter = 0; icounter < mRelatedDefinitions.Count; icounter++)
				(mDatabase[mRelatedDefinitions[icounter]] as IfcDefinitionSelect).HasContext = this;
		}
		internal void AddDefinition(IfcDefinitionSelect d)
		{
			if (mRelatedDefinitions.Contains(d.Index))
			{
				mRelatedDefinitions.Add(d.Index);
				d.HasContext = this;
			}
		}
	}
	public abstract partial class IfcRelDecomposes : IfcRelationship //ABSTACT  SUPERTYPE OF (ONEOF (IfcRelAggregates ,IfcRelNests ,IfcRelProjectsElement ,IfcRelVoidsElement))
	{
		protected IfcRelDecomposes() : base() { }
		protected IfcRelDecomposes(IfcRelDecomposes d) : base(d) { }
		protected IfcRelDecomposes(DatabaseIfc db) : base(db) { }
		protected IfcRelDecomposes(DatabaseIfc db, IfcRelDecomposes d) : base(db,d) { }
		internal static void parseFields(IfcRelDecomposes d, List<string> arrFields, ref int ipos) { IfcRelationship.parseFields(d, arrFields, ref ipos); }
	}
	public abstract partial class IfcRelDefines : IfcRelationship // 	ABSTRACT SUPERTYPE OF(ONEOF(IfcRelDefinesByObject, IfcRelDefinesByProperties, IfcRelDefinesByTemplate, IfcRelDefinesByType))
	{
		protected IfcRelDefines() : base() { }
		protected IfcRelDefines(IfcRelDefines d) : base(d) { }
		protected IfcRelDefines(DatabaseIfc db) : base(db) { }
		protected IfcRelDefines(DatabaseIfc db, IfcRelDefines d) : base(db, d) { }
		protected static void parseFields(IfcRelDefines r, List<string> arrFields, ref int ipos) { IfcRelationship.parseFields(r, arrFields, ref ipos); }
	}
	public partial class IfcRelDefinesByObject : IfcRelDefines
	{
		internal List<int> mRelatedObjects = new List<int>();// : SET [1:?] OF IfcObject;
		internal int mRelatingObject;// : IfcObject  

		public List<IfcObject> RelatedObjects { get { return mRelatedObjects.ConvertAll(x => mDatabase[x] as IfcObject); } }
		public IfcObject RelatingObject { get { return mDatabase[mRelatingObject] as IfcObject; } }

		internal IfcRelDefinesByObject() : base() { }
		internal IfcRelDefinesByObject(IfcRelDefinesByObject d) : base(d) { mRelatedObjects = new List<int>(d.mRelatedObjects.ToArray()); mRelatingObject = d.mRelatingObject; }
		internal IfcRelDefinesByObject(IfcObject relObj) : base(relObj.mDatabase) { mRelatingObject = relObj.mIndex; relObj.mIsDeclaredBy = this; }
		protected override string BuildStringSTEP()
		{
			if (mRelatedObjects.Count == 0)
				return "";
			string str = base.BuildStringSTEP() + ",(" + ParserSTEP.LinkToString(mRelatedObjects[0]);
			for (int icounter = 1; icounter < mRelatedObjects.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mRelatedObjects[icounter]);
			return str + ")," + ParserSTEP.LinkToString(mRelatingObject);
		}
		internal static void parseFields(IfcRelDefinesByObject t, List<string> arrFields, ref int ipos) { IfcRelDefines.parseFields(t, arrFields, ref ipos); t.mRelatedObjects = ParserSTEP.SplitListLinks(arrFields[ipos++]); t.mRelatingObject = ParserSTEP.ParseLink(arrFields[ipos++]); }
		internal static IfcRelDefinesByObject Parse(string strDef) { IfcRelDefinesByObject t = new IfcRelDefinesByObject(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		internal override void postParseRelate()
		{
			base.postParseRelate();
			if (mRelatingObject > 0)
			{
				IfcObject ot = mDatabase[mRelatingObject] as IfcObject;
				if (ot != null)
					ot.mIsDeclaredBy = this;
			}
			List<IfcObject> objects = RelatedObjects;
			for (int icounter = 0; icounter < objects.Count; icounter++)
			{
				IfcObject o = objects[icounter];
				if (o != null)
					o.mDeclares.Add(this);
			}
		}
		internal void assign(IfcObject obj) { mRelatedObjects.Add(obj.mIndex); obj.mIsDeclaredBy = this; }
	}
	public partial class IfcRelDefinesByProperties : IfcRelDefines
	{
		private List<int> mRelatedObjects = new List<int>();// IFC4 change	SET [1:1] OF IfcObjectDefinition; ifc2x3 : SET [1:?] OF IfcObject;  
		private int mRelatingPropertyDefinition;// : IfcPropertySetDefinition; 

		public List<IfcObjectDefinition> RelatedObjects { get { return mRelatedObjects.ConvertAll(x => mDatabase[x] as IfcObjectDefinition); } set { mRelatedObjects.Clear(); assign(value); } }
		public IfcPropertySetDefinition RelatingPropertyDefinition { get { return mDatabase[mRelatingPropertyDefinition] as IfcPropertySetDefinition; } set { mRelatingPropertyDefinition = value.mIndex; } }

		internal IfcRelDefinesByProperties() : base() { }
		private IfcRelDefinesByProperties(DatabaseIfc db) : base(db) { Name = "NameRelDefinesByProperties"; Description = "DescriptionRelDefinesByProperties"; }
		internal IfcRelDefinesByProperties(DatabaseIfc db, IfcRelDefinesByProperties d) : base(db, d)
		{
			//RelatedObjects = d.RelatedObjects.ConvertAll(x=>db.Factory.Duplicate(x) as IfcObjectDefinition);
			RelatingPropertyDefinition = db.Factory.Duplicate(d.RelatingPropertyDefinition) as IfcPropertySetDefinition;
		}
		internal IfcRelDefinesByProperties(IfcPropertySetDefinition ifcproperty) : base(ifcproperty.mDatabase) { mRelatingPropertyDefinition = ifcproperty.mIndex; }
		public IfcRelDefinesByProperties(IfcObjectDefinition od, IfcPropertySetDefinition ifcproperty) : this(new List<IfcObjectDefinition>() { od }, ifcproperty) { }
		public IfcRelDefinesByProperties(List<IfcObjectDefinition> objs, IfcPropertySetDefinition ifcproperty) : this(ifcproperty) { assign(objs); }
		
		protected override string BuildStringSTEP()
		{
			if (mRelatedObjects.Count == 0 || RelatingPropertyDefinition == null || string.IsNullOrEmpty(RelatingPropertyDefinition.ToString()))
				return "";
			string str = base.BuildStringSTEP() + ",(" + ParserSTEP.LinkToString(mRelatedObjects[0]);
			for (int icounter = 1; icounter < mRelatedObjects.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mRelatedObjects[icounter]);
			return str + ")," + ParserSTEP.LinkToString(mRelatingPropertyDefinition);
		}
		internal static IfcRelDefinesByProperties Parse(string str)
		{
			IfcRelDefinesByProperties d = new IfcRelDefinesByProperties();
			int pos = 0;
			d.parseString(str, ref pos);
			d.mRelatedObjects = ParserSTEP.StripListLink(str, ref pos);// splitListSTPLinks(arrFields[ipos++]);
			d.mRelatingPropertyDefinition = ParserSTEP.StripLink(str, ref pos);//.parseSTPLink(arrFields[ipos++]);
			return d;
		}

		internal void assign(IfcObjectDefinition od)
		{
			mRelatedObjects.Add(od.Index);
			IfcContext context = od as IfcContext;
			if (context != null)
			{
				if(!context.mIsDefinedBy.Contains(this))
					context.mIsDefinedBy.Add(this);
			}
			else
			{
				IfcObject obj = od as IfcObject;
				if (obj != null && !obj.mIsDefinedBy.Contains(this))
					obj.mIsDefinedBy.Add(this);
			}
		}
		internal void assign(List<IfcObjectDefinition> objs)
		{
			for (int icounter = 0; icounter < objs.Count; icounter++)
				assign(objs[icounter]);
		}
	
		internal void unassign(List<IfcObjectDefinition> objs)
		{
			for (int icounter = 0; icounter < objs.Count; icounter++)
				RemoveRelated(objs[icounter]);
		}
		internal void RemoveRelated(IfcObjectDefinition od)
		{
			mRelatedObjects.Remove(od.Index);
			IfcContext context = od as IfcContext;
			if(context != null)
				context.mIsDefinedBy.Remove(this);
			else
			{
				IfcObject obj = od as IfcObject;
				if (obj != null)
					obj.mIsDefinedBy.Remove(this);
			}
		}
		internal override void postParseRelate()
		{
			base.postParseRelate();
			RelatingPropertyDefinition.DefinesOccurrence = this;
			List<IfcObjectDefinition> related = RelatedObjects;
			for (int icounter = 0; icounter < related.Count; icounter++)
			{
				IfcObject o = related[icounter] as IfcObject;
				if (o != null)
					o.mIsDefinedBy.Add(this);
				else
				{
					IfcContext context = related[icounter] as IfcContext;
					if (context != null)
						context.mIsDefinedBy.Add(this);
				}
			}
		}
	}
	public partial class IfcRelDefinesByTemplate : IfcRelDefines
	{
		internal List<int> mRelatedPropertySets = new List<int>();// : SET [1:?] OF IfcPropertySetDefinition;
		internal int mRelatingTemplate;// :	IfcPropertySetTemplate;

		public List<IfcPropertySetDefinition> RelatedPropertySets { get { return mRelatedPropertySets.ConvertAll(x => mDatabase[x] as IfcPropertySetDefinition); } set { mRelatedPropertySets.Clear(); assign(value); } }
		public IfcPropertySetTemplate RelatingTemplate { get { return mDatabase[mRelatingTemplate] as IfcPropertySetTemplate; } set { mRelatingTemplate = value.mIndex; } }

		internal IfcRelDefinesByTemplate() : base() { }
		internal IfcRelDefinesByTemplate(DatabaseIfc db, IfcRelDefinesByTemplate r) : base(db,r) { RelatedPropertySets = r.RelatedPropertySets.ConvertAll(x => db.Factory.Duplicate(x) as IfcPropertySetDefinition); RelatingTemplate = db.Factory.Duplicate(r.RelatingTemplate) as IfcPropertySetTemplate; }
		protected override string BuildStringSTEP()
		{
			if (mRelatedPropertySets.Count == 0)
				return "";
			string str = base.BuildStringSTEP() + ",(" + ParserSTEP.LinkToString(mRelatedPropertySets[0]);
			for (int icounter = 1; icounter < mRelatedPropertySets.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mRelatedPropertySets[icounter]);
			return str + ")," + ParserSTEP.LinkToString(mRelatingTemplate);
		}
		internal static void parseFields(IfcRelDefinesByTemplate t, List<string> arrFields, ref int ipos) { IfcRelDefines.parseFields(t, arrFields, ref ipos); t.mRelatedPropertySets = ParserSTEP.SplitListLinks(arrFields[ipos++]); t.mRelatingTemplate = ParserSTEP.ParseLink(arrFields[ipos++]); }
		internal static IfcRelDefinesByTemplate Parse(string strDef) { IfcRelDefinesByTemplate t = new IfcRelDefinesByTemplate(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		internal override void postParseRelate()
		{
			base.postParseRelate();
			if (mRelatingTemplate > 0)
			{
				IfcPropertySetTemplate rt = RelatingTemplate;
				if (rt != null)
					rt.mDefines.Add(this);
			}
			List<IfcPropertySetDefinition> psets = RelatedPropertySets;
			for (int icounter = 0; icounter < psets.Count; icounter++)
			{
				IfcPropertySetDefinition pset = psets[icounter];
				if (pset != null)
					pset.mIsDefinedBy.Add(this);
			}
		}
		internal void assign(List<IfcPropertySetDefinition> objs)
		{
			for (int icounter = 0; icounter < objs.Count; icounter++)
				assign(objs[icounter]);
		}
		internal void assign(IfcPropertySetDefinition pset) { mRelatedPropertySets.Add(pset.mIndex); pset.mIsDefinedBy.Add(this); }
	}
	public partial class IfcRelDefinesByType : IfcRelDefines
	{
		internal List<int> mRelatedObjects = new List<int>();// : SET [1:?] OF IfcObject;
		private int mRelatingType;// : IfcTypeObject  

		public IfcTypeObject RelatingType { get { return mDatabase[mRelatingType] as IfcTypeObject; } set { mRelatingType = value.mIndex; } }
		public List<IfcObject> RelatedObjects { get { return mRelatedObjects.ConvertAll(x => mDatabase[x] as IfcObject); } set { mRelatedObjects = value.ConvertAll(x => x.mIndex); for (int icounter = 0; icounter < value.Count; icounter++) value[icounter].mIsTypedBy = this; } }

		internal IfcRelDefinesByType() : base() { }
		internal IfcRelDefinesByType(IfcTypeObject relType) : base(relType.mDatabase) { mRelatingType = relType.mIndex; relType.mObjectTypeOf = this; }
		internal IfcRelDefinesByType(DatabaseIfc db, IfcRelDefinesByType r) : base(db, r)
		{
			//mRelatedObjects = new List<int>(d.mRelatedObjects.ToArray()); 
			RelatingType = db.Factory.Duplicate(r.RelatingType) as IfcTypeObject;
		}
		internal IfcRelDefinesByType(IfcObject related, IfcTypeObject relating) : this(relating) { mRelatedObjects.Add(related.mIndex); }
		protected override string BuildStringSTEP()
		{
			if (mRelatedObjects.Count == 0)
				return "";
			IfcTypeObject to = RelatingType;
			if (to == null || string.IsNullOrEmpty(to.ToString()))
				return "";
			string str = base.BuildStringSTEP() + ",(" + ParserSTEP.LinkToString(mRelatedObjects[0]);
			for (int icounter = 1; icounter < mRelatedObjects.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mRelatedObjects[icounter]);
			return str + ")," + ParserSTEP.LinkToString(mRelatingType);
		}
		internal static void parseFields(IfcRelDefinesByType t, List<string> arrFields, ref int ipos) { IfcRelDefines.parseFields(t, arrFields, ref ipos); t.mRelatedObjects = ParserSTEP.SplitListLinks(arrFields[ipos++]); t.mRelatingType = ParserSTEP.ParseLink(arrFields[ipos++]); }
		internal static IfcRelDefinesByType Parse(string strDef) { IfcRelDefinesByType t = new IfcRelDefinesByType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		internal override void postParseRelate()
		{
			base.postParseRelate();
			if (mRelatingType > 0)
			{
				IfcTypeObject ot = RelatingType;
				if (ot != null)
					ot.mObjectTypeOf = this;
			}
			for (int icounter = 0; icounter < mRelatedObjects.Count; icounter++)
			{
				IfcObject o = mDatabase[mRelatedObjects[icounter]] as IfcObject;
				if (o != null)
					o.IsTypedBy = this;
			}
		}

		public void AssignObj(IfcObject obj) //TODO CHECK CLASS NAME MATCHES INSTANCE using reflection
		{
			mRelatedObjects.Add(obj.mIndex);
			if (obj.mIsTypedBy != null)
				obj.mIsTypedBy.mRelatedObjects.Remove(obj.mIndex);
			obj.mIsTypedBy = this;
		}
	}
	public partial class IfcRelFillsElement : IfcRelConnects
	{
		private int mRelatingOpeningElement;// : IfcOpeningElement;
		private int mRelatedBuildingElement;// :OPTIONAL IfcElement; 

		public IfcOpeningElement RelatingOpeningElement { get { return mDatabase[mRelatingOpeningElement] as IfcOpeningElement; } }
		public IfcElement RelatedBuildingElement { get { return mDatabase[mRelatedBuildingElement] as IfcElement; } }

		internal IfcRelFillsElement() : base() { }
		internal IfcRelFillsElement(IfcRelFillsElement f) : base(f) { mRelatingOpeningElement = f.mRelatingOpeningElement; mRelatedBuildingElement = f.mRelatedBuildingElement; }
		internal IfcRelFillsElement(IfcOpeningElement oe, IfcElement e) : base(oe.mDatabase) { mRelatingOpeningElement = oe.mIndex; mRelatedBuildingElement = e.mIndex; }
		internal static IfcRelFillsElement Parse(string strDef) { IfcRelFillsElement i = new IfcRelFillsElement(); int ipos = 0; parseFields(i, ParserSTEP.SplitLineFields(strDef), ref ipos); return i; }
		internal static void parseFields(IfcRelFillsElement i, List<string> arrFields, ref int ipos) { IfcRelConnects.parseFields(i, arrFields, ref ipos); i.mRelatingOpeningElement = ParserSTEP.ParseLink(arrFields[ipos++]); i.mRelatedBuildingElement = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mRelatingOpeningElement) + "," + ParserSTEP.LinkToString(mRelatedBuildingElement); }
		internal override void postParseRelate()
		{
			base.postParseRelate();
			RelatedBuildingElement.mFillsVoids.Add(this);
			RelatingOpeningElement.mHasFillings.Add(this);
		}
	}
	public partial class IfcRelFlowControlElements : IfcRelConnects
	{
		internal int mRelatingPort;// : IfcPort;
		internal int mRelatedElement;// : IfcElement; 
		internal IfcRelFlowControlElements() : base() { }
		internal IfcRelFlowControlElements(IfcRelFlowControlElements i) : base(i) { mRelatingPort = i.mRelatingPort; mRelatedElement = i.mRelatedElement; }
		internal static IfcRelFlowControlElements Parse(string strDef) { IfcRelFlowControlElements i = new IfcRelFlowControlElements(); int ipos = 0; parseFields(i, ParserSTEP.SplitLineFields(strDef), ref ipos); return i; }
		internal static void parseFields(IfcRelFlowControlElements i, List<string> arrFields, ref int ipos) { IfcRelConnects.parseFields(i, arrFields, ref ipos); i.mRelatingPort = ParserSTEP.ParseLink(arrFields[ipos++]); i.mRelatedElement = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mRelatingPort) + "," + ParserSTEP.LinkToString(mRelatedElement); }
	}
	//ENTITY IfcRelInteractionRequirements  // DEPRECEATED IFC4
	public partial class IfcRelNests : IfcRelDecomposes
	{
		internal int mRelatingObject;// : IfcObjectDefinition 
		internal List<int> mRelatedObjects = new List<int>();// : SET [1:?] OF IfcObjectDefinition; 

		public IfcObjectDefinition RelatingObject { get { return mDatabase[mRelatingObject] as IfcObjectDefinition; } set { mRelatingObject = value.mIndex; if (!value.mIsNestedBy.Contains(this)) value.mIsNestedBy.Add(this); } }
		public List<IfcObjectDefinition> RelatedObjects { get { return mRelatedObjects.ConvertAll(x => mDatabase[x] as IfcObjectDefinition); } set { mRelatedObjects = value.ConvertAll(x => x.mIndex); } }

		internal IfcRelNests() : base() { }
		internal IfcRelNests(IfcRelNests n) : base(n) { mRelatingObject = n.mRelatingObject; mRelatedObjects = new List<int>(n.mRelatedObjects.ToArray()); }
		public IfcRelNests(IfcObjectDefinition relatingObject) : base(relatingObject.mDatabase)
		{
			mRelatingObject = relatingObject.mIndex;
			relatingObject.mIsNestedBy.Add(this);
		}
		internal IfcRelNests(IfcObjectDefinition relatingObject, IfcObjectDefinition relatedObject) : base(relatingObject.mDatabase)
		{
			mRelatingObject = relatingObject.mIndex;
			mRelatedObjects.Add(relatedObject.mIndex);
			relatingObject.mIsNestedBy.Add(this);
			relatedObject.mNests = this;
		}
		internal IfcRelNests(IfcObjectDefinition relatingObject, IfcObjectDefinition ro, IfcObjectDefinition ro2) : this(relatingObject, ro) { mRelatedObjects.Add(ro2.mIndex); ro2.mNests = this; ; }
		internal IfcRelNests(IfcObjectDefinition relatingObject, List<IfcObjectDefinition> relatedObjects) : base(relatingObject.mDatabase)
		{
			mRelatingObject = relatingObject.mIndex;
			relatingObject.mIsNestedBy.Add(this);
			for (int icounter = 0; icounter < relatedObjects.Count; icounter++)
			{
				mRelatedObjects.Add(relatedObjects[icounter].mIndex);
				relatedObjects[icounter].mNests = this;
			}
		}
		internal static IfcRelNests Parse(string strDef) { IfcRelNests a = new IfcRelNests(); int ipos = 0; parseFields(a, ParserSTEP.SplitLineFields(strDef), ref ipos); return a; }
		internal static void parseFields(IfcRelNests a, List<string> arrFields, ref int ipos) { IfcRelDecomposes.parseFields(a, arrFields, ref ipos); a.mRelatingObject = ParserSTEP.ParseLink(arrFields[ipos++]); a.mRelatedObjects = ParserSTEP.SplitListLinks(arrFields[ipos++]); }
		protected override string BuildStringSTEP()
		{
			string str = "";
			if (mRelatedObjects.Count > 0)
			{
				str += ParserSTEP.LinkToString(mRelatedObjects[0]);
				for (int icounter = 1; icounter < mRelatedObjects.Count; icounter++)
					str += "," + ParserSTEP.LinkToString(mRelatedObjects[icounter]);
			}
			else
				return "";
			return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mRelatingObject) + ",(" + str + ")";
		}
		internal override void postParseRelate()
		{
			base.postParseRelate();
			IfcObjectDefinition relating = RelatingObject;
			relating.relateNested(this);
			List<IfcObjectDefinition> ods = RelatedObjects;
			foreach (IfcObjectDefinition od in ods)
			{
				if (od == null)
					continue;
				od.mNests = this;
				IfcDistributionPort dp = od as IfcDistributionPort;
				if (dp != null)
				{
					IfcFlowSegment fs = relating as IfcFlowSegment;
					if (fs != null)
					{
						if (dp.mFlowDirection == IfcFlowDirectionEnum.SOURCE)
							fs.mSourcePort = dp;
						else if (dp.mFlowDirection == IfcFlowDirectionEnum.SINK)
							fs.mSinkPort = dp;
					}
				}
			}
		}
		internal void addObject(IfcObjectDefinition o)
		{
			o.mNests = this;
			if (!mRelatedObjects.Contains(o.mIndex))
				mRelatedObjects.Add(o.mIndex);
		}
	}
	//ENTITY IfcRelOccupiesSpaces // DEPRECEATED IFC4
	//ENTITY IfcRelOverridesProperties // DEPRECEATED IFC4
	public partial class IfcRelProjectsElement : IfcRelDecomposes // IFC2x3 IfcRelDecomposes
	{
		internal int mRelatingElement;// : IfcElement; 
		internal int mRelatedFeatureElement;// : IfcFeatureElementAddition

		public IfcElement RelatingElement { get { return mDatabase[mRelatingElement] as IfcElement; } }
		public IfcFeatureElementAddition RelatedFeatureElement { get { return mDatabase[mRelatedFeatureElement] as IfcFeatureElementAddition; } }

		protected IfcRelProjectsElement() : base() { }
		protected IfcRelProjectsElement(IfcRelProjectsElement p) : base(p) { mRelatingElement = p.mRelatingElement; mRelatedFeatureElement = p.mRelatedFeatureElement; }
		protected IfcRelProjectsElement(IfcElement e, IfcFeatureElementAddition a) : base(e.mDatabase) { mRelatingElement = e.mIndex; mRelatedFeatureElement = a.mIndex; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mRelatingElement) + "," + ParserSTEP.LinkToString(mRelatedFeatureElement); }
		protected static void parseFields(IfcRelProjectsElement c, List<string> arrFields, ref int ipos) { IfcRelDecomposes.parseFields(c, arrFields, ref ipos); c.mRelatingElement = ParserSTEP.ParseLink(arrFields[ipos++]); c.mRelatedFeatureElement = ParserSTEP.ParseLink(arrFields[ipos++]); }
		internal override void postParseRelate()
		{
			base.postParseRelate();
			RelatingElement.mHasProjections.Add(this);
			RelatedFeatureElement.mProjectsElements.Add(this);
		}
	}
	//ENTITY IfcRelReferencedInSpatialStructure
	//ENTITY IfcRelSchedulesCostItems // DEPRECEATED IFC4 
	public partial class IfcRelSequence : IfcRelConnects
	{
		internal int mRelatingProcess;// : IfcProcess;
		internal int mRelatedProcess;//  IfcProcess;
		internal double mTimeLag;// : OPTIONAL IfcLagTime; IFC2x3 	IfcTimeMeasure
		internal IfcSequenceEnum mSequenceType = IfcSequenceEnum.NOTDEFINED;//	 :	OPTIONAL IfcSequenceEnum;
		internal string mUserDefinedSequenceType = "$";//	 :	OPTIONAL IfcLabel; 

		public IfcProcess RelatingProcess { get { return mDatabase[mRelatingProcess] as IfcProcess; } set { mRelatingProcess = value.mIndex; } }
		public IfcProcess RelatedProcess { get { return mDatabase[mRelatedProcess] as IfcProcess; } set { mRelatedProcess = value.mIndex; } }

		internal IfcRelSequence() : base() { }
		internal IfcRelSequence(IfcRelSequence s) : base(s) { mRelatingProcess = s.mRelatingProcess; mRelatedProcess = s.mRelatedProcess; mTimeLag = s.mTimeLag; mSequenceType = s.mSequenceType; mUserDefinedSequenceType = s.mUserDefinedSequenceType; }
		internal IfcRelSequence(IfcProcess rg, IfcProcess rd, IfcLagTime lag, IfcSequenceEnum st, string userSeqType) : base(rg.mDatabase)
		{
			mRelatingProcess = rg.mIndex;
			mRelatedProcess = rd.mIndex;
			if (lag != null)
				mTimeLag = (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? (int)lag.getSecondsDuration() : lag.mIndex);
			mSequenceType = st;
			if (!string.IsNullOrEmpty(userSeqType))
				mUserDefinedSequenceType = userSeqType.Replace("'", "");
		}
		internal static IfcRelSequence Parse(string strDef,ReleaseVersion schema) { IfcRelSequence i = new IfcRelSequence(); int ipos = 0; parseFields(i, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return i; }
		internal static void parseFields(IfcRelSequence i, List<string> arrFields, ref int ipos,ReleaseVersion schema)
		{
			IfcRelConnects.parseFields(i, arrFields, ref ipos);
			i.mRelatingProcess = ParserSTEP.ParseLink(arrFields[ipos++]);
			i.mRelatedProcess = ParserSTEP.ParseLink(arrFields[ipos++]);
			if (schema == ReleaseVersion.IFC2x3)
				i.mTimeLag = ParserSTEP.ParseDouble(arrFields[ipos++]);
			else
				i.mTimeLag = ParserSTEP.ParseLink(arrFields[ipos++]);
			string s = arrFields[ipos++];
			if (s != "$")
				i.mSequenceType = (IfcSequenceEnum)Enum.Parse(typeof(IfcSequenceEnum), s.Replace(".", ""));
			if (schema != ReleaseVersion.IFC2x3)
				i.mUserDefinedSequenceType = arrFields[ipos++];
		}
		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mRelatingProcess) + "," + ParserSTEP.LinkToString(mRelatedProcess) + "," + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? ParserSTEP.DoubleToString(mTimeLag) :
				ParserSTEP.LinkToString((int)mTimeLag)) + ",." + mSequenceType + (mDatabase.mRelease == ReleaseVersion.IFC2x3 ? "." : (mUserDefinedSequenceType == "$" ? ".,$" : ".,'" + mUserDefinedSequenceType + "'"));
		}
		internal override void postParseRelate()
		{
			base.postParseRelate();
			RelatingProcess.mIsPredecessorTo.Add(this);
			RelatedProcess.mIsSuccessorFrom.Add(this);
		}
		internal IfcProcess getPredecessor() { return mDatabase[mRelatingProcess] as IfcProcess; }
		internal IfcProcess getSuccessor() { return mDatabase[mRelatedProcess] as IfcProcess; }
		internal TimeSpan getLag()
		{
			if (mDatabase.mRelease == ReleaseVersion.IFC2x3) return new TimeSpan(0, 0, (int)mTimeLag);
			IfcLagTime lt = mDatabase[(int)mTimeLag] as IfcLagTime;
			return (lt == null ? new TimeSpan(0, 0, 0) : lt.getLag());
		}
	}
	public partial class IfcRelServicesBuildings : IfcRelConnects
	{
		internal int mRelatingSystem;// : IfcSystem;
		internal List<int> mRelatedBuildings = new List<int>();// : SET [1:?] OF IfcSpatialElement  ;

		public IfcSystem RelatingSystem { get { return mDatabase[mRelatingSystem] as IfcSystem; } set { mRelatingSystem = value.mIndex; } }
		public List<IfcSpatialElement> RelatedBuildings
		{
			get { return mRelatedBuildings.ConvertAll(x => mDatabase[x] as IfcSpatialElement); }
			set
			{
				mRelatedBuildings = value.ConvertAll(x => x.mIndex);
				for (int icounter = 0; icounter < value.Count; icounter++)
					value[icounter].mServicedBySystems.Add(this);
			}
		}

		internal IfcRelServicesBuildings() : base() { }
		internal IfcRelServicesBuildings(DatabaseIfc db, IfcRelServicesBuildings s) : base(db,s)
		{
			//mRelatingSystem = s.mRelatingSystem;
			RelatedBuildings = s.RelatedBuildings.ConvertAll(x => db.Factory.Duplicate(x) as IfcSpatialElement);
		}
		internal IfcRelServicesBuildings(IfcSystem sys, IfcSpatialElement se)
			: base(sys.mDatabase) { mRelatingSystem = sys.mIndex; mRelatedBuildings.Add(se.mIndex); se.mServicedBySystems.Add(this); }
		internal static IfcRelServicesBuildings Parse(string strDef) { IfcRelServicesBuildings i = new IfcRelServicesBuildings(); int ipos = 0; parseFields(i, ParserSTEP.SplitLineFields(strDef), ref ipos); return i; }
		internal static void parseFields(IfcRelServicesBuildings i, List<string> arrFields, ref int ipos) { IfcRelConnects.parseFields(i, arrFields, ref ipos); i.mRelatingSystem = ParserSTEP.ParseLink(arrFields[ipos++]); i.mRelatedBuildings = ParserSTEP.SplitListLinks(arrFields[ipos++]); }
		protected override string BuildStringSTEP()
		{
			if (mRelatedBuildings.Count == 0)
				return "";
			string str = base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mRelatingSystem) + ",(" + ParserSTEP.LinkToString(mRelatedBuildings[0]);
			for (int icounter = 1; icounter < mRelatedBuildings.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mRelatedBuildings[icounter]);
			return str + ")";
		}
		internal override void postParseRelate()
		{
			base.postParseRelate();
			RelatingSystem.mServicesBuildings = this;
			for (int icounter = 0; icounter < mRelatedBuildings.Count; icounter++)
			{
				IfcSpatialStructureElement se = mDatabase[mRelatedBuildings[icounter]] as IfcSpatialStructureElement;
				if (se != null)
					se.mServicedBySystems.Add(this);
			}
		}
	}
	public partial class IfcRelSpaceBoundary : IfcRelConnects
	{
		internal int mRelatingSpace;// :	IfcSpaceBoundarySelect; : IfcSpace;
		internal int mRelatedBuildingElement;// :OPTIONAL IfcElement;
		internal int mConnectionGeometry;// : OPTIONAL IfcConnectionGeometry;
		internal IfcPhysicalOrVirtualEnum mPhysicalOrVirtualBoundary = IfcPhysicalOrVirtualEnum.NOTDEFINED;// : IfcPhysicalOrVirtualEnum;
		internal IfcInternalOrExternalEnum mInternalOrExternalBoundary = IfcInternalOrExternalEnum.NOTDEFINED;// : IfcInternalOrExternalEnum; 

		public IfcSpaceBoundarySelect RelatingSpace { get { return mDatabase[mRelatingSpace] as IfcSpaceBoundarySelect; } }
		public IfcElement RelatedBuildingElement { get { return mDatabase[mRelatedBuildingElement] as IfcElement; } set { mRelatedBuildingElement = value == null ? 0 : value.mIndex; } }

		internal IfcRelSpaceBoundary() : base() { }
		internal IfcRelSpaceBoundary(IfcRelSpaceBoundary p) : base(p) { mRelatingSpace = p.mRelatingSpace; mRelatedBuildingElement = p.mRelatedBuildingElement; mConnectionGeometry = p.mConnectionGeometry; mPhysicalOrVirtualBoundary = p.mPhysicalOrVirtualBoundary; mInternalOrExternalBoundary = p.mInternalOrExternalBoundary; }
		internal IfcRelSpaceBoundary(IfcSpaceBoundarySelect s, IfcElement e, IfcConnectionGeometry g, IfcPhysicalOrVirtualEnum virt, IfcInternalOrExternalEnum intern) : base(s.Database)
		{
			mRelatingSpace = s.Index;
			s.BoundedBy.Add(this);
			mRelatedBuildingElement = e.mIndex;
			if (g != null)
				mConnectionGeometry = g.mIndex;
			mPhysicalOrVirtualBoundary = virt;
			mInternalOrExternalBoundary = intern;
		}
		internal static IfcRelSpaceBoundary Parse(string strDef) { IfcRelSpaceBoundary i = new IfcRelSpaceBoundary(); int ipos = 0; parseFields(i, ParserSTEP.SplitLineFields(strDef), ref ipos); return i; }
		internal static void parseFields(IfcRelSpaceBoundary i, List<string> arrFields, ref int ipos)
		{
			IfcRelConnects.parseFields(i, arrFields, ref ipos);
			i.mRelatingSpace = ParserSTEP.ParseLink(arrFields[ipos++]);
			i.mRelatedBuildingElement = ParserSTEP.ParseLink(arrFields[ipos++]);
			i.mConnectionGeometry = ParserSTEP.ParseLink(arrFields[ipos++]);
			string s = arrFields[ipos++];
			if (s != "$")
				i.mPhysicalOrVirtualBoundary = (IfcPhysicalOrVirtualEnum)Enum.Parse(typeof(IfcPhysicalOrVirtualEnum), s.Replace(".", ""));
			s = arrFields[ipos++];
			if (s != "$")
				i.mInternalOrExternalBoundary = (IfcInternalOrExternalEnum)Enum.Parse(typeof(IfcInternalOrExternalEnum), s.Replace(".", ""));
		}
		protected override string BuildStringSTEP() { return (mRelatedBuildingElement == 0 || mRelatingSpace == 0 ? "" : base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mRelatingSpace) + "," + ParserSTEP.LinkToString(mRelatedBuildingElement) + "," + ParserSTEP.LinkToString(mConnectionGeometry) + ",." + mPhysicalOrVirtualBoundary.ToString() + ".,." + mInternalOrExternalBoundary.ToString() + "."); }
		internal override void postParseRelate()
		{
			base.postParseRelate();
			RelatingSpace.BoundedBy.Add(this);
			IfcElement e = RelatedBuildingElement;
			if (e != null)
				e.mProvidesBoundaries.Add(this);
		}
	}
	public partial class IfcRelSpaceBoundary1stLevel : IfcRelSpaceBoundary
	{
		internal int mParentBoundary;// :	IfcRelSpaceBoundary1stLevel;
		//INVERSE	
		internal List<IfcRelSpaceBoundary1stLevel> mInnerBoundaries = new List<IfcRelSpaceBoundary1stLevel>();//	:	SET OF IfcRelSpaceBoundary1stLevel FOR ParentBoundary;

		internal IfcRelSpaceBoundary1stLevel() : base() { }
		internal IfcRelSpaceBoundary1stLevel(IfcRelSpaceBoundary1stLevel p) : base(p) { mParentBoundary = p.mParentBoundary; }
		internal IfcRelSpaceBoundary1stLevel(IfcSpaceBoundarySelect s, IfcElement e, IfcConnectionGeometry g, IfcPhysicalOrVirtualEnum virt, IfcInternalOrExternalEnum intern, IfcRelSpaceBoundary1stLevel parent)
			: base(s, e, g, virt, intern) { mParentBoundary = parent.mIndex; }
		internal static new IfcRelSpaceBoundary1stLevel Parse(string strDef) { IfcRelSpaceBoundary1stLevel i = new IfcRelSpaceBoundary1stLevel(); int ipos = 0; parseFields(i, ParserSTEP.SplitLineFields(strDef), ref ipos); return i; }
		internal static void parseFields(IfcRelSpaceBoundary1stLevel i, List<string> arrFields, ref int ipos)
		{
			IfcRelSpaceBoundary.parseFields(i, arrFields, ref ipos);
			i.mParentBoundary = ParserSTEP.ParseLink(arrFields[ipos++]);
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mParentBoundary); }
		internal override void postParseRelate()
		{
			base.postParseRelate();
			IfcRelSpaceBoundary1stLevel s = mDatabase[mParentBoundary] as IfcRelSpaceBoundary1stLevel;
			s.mInnerBoundaries.Add(this);
		}
	}
	public partial class IfcRelSpaceBoundary2ndLevel : IfcRelSpaceBoundary1stLevel
	{
		internal int mCorrespondingBoundary;// :	IfcRelSpaceBoundary2ndLevel;
		//INVERSE	
		internal List<IfcRelSpaceBoundary2ndLevel> mCorresponds = new List<IfcRelSpaceBoundary2ndLevel>();//	:	SET OF IfcRelSpaceBoundary1stLevel FOR ParentBoundary;

		public IfcRelSpaceBoundary2ndLevel CorrespondingBoundary { get { return mDatabase[mCorrespondingBoundary] as IfcRelSpaceBoundary2ndLevel; } set { mCorrespondingBoundary = value.mIndex; } }

		internal IfcRelSpaceBoundary2ndLevel() : base() { }
		internal IfcRelSpaceBoundary2ndLevel(IfcRelSpaceBoundary2ndLevel p) : base(p) { mCorrespondingBoundary = p.mCorrespondingBoundary; }
		internal IfcRelSpaceBoundary2ndLevel(IfcSpaceBoundarySelect s, IfcElement e, IfcConnectionGeometry g, IfcPhysicalOrVirtualEnum virt, IfcInternalOrExternalEnum intern, IfcRelSpaceBoundary1stLevel parent, IfcRelSpaceBoundary2ndLevel corresponding)
			: base(s, e, g, virt, intern, parent) { if (corresponding != null) mCorrespondingBoundary = corresponding.mIndex; }
		internal static new IfcRelSpaceBoundary2ndLevel Parse(string strDef) { IfcRelSpaceBoundary2ndLevel i = new IfcRelSpaceBoundary2ndLevel(); int ipos = 0; parseFields(i, ParserSTEP.SplitLineFields(strDef), ref ipos); return i; }
		internal static void parseFields(IfcRelSpaceBoundary2ndLevel i, List<string> arrFields, ref int ipos)
		{
			IfcRelSpaceBoundary1stLevel.parseFields(i, arrFields, ref ipos);
			i.mCorrespondingBoundary = ParserSTEP.ParseLink(arrFields[ipos++]);
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mCorrespondingBoundary); }
		internal override void postParseRelate()
		{
			base.postParseRelate();
			CorrespondingBoundary.mCorresponds.Add(this);
		}
	}
	public partial class IfcRelVoidsElement : IfcRelDecomposes // Ifc2x3 IfcRelConnects
	{
		private int mRelatingBuildingElement;// : IfcElement;
		private int mRelatedOpeningElement;// : IfcFeatureElementSubtraction; 

		public IfcElement RelatingBuildingElement { get { return mDatabase[mRelatingBuildingElement] as IfcElement; } set { mRelatingBuildingElement = value.mIndex; value.mHasOpenings.Add(this); } }
		public IfcFeatureElementSubtraction RelatedOpeningElement { get { return mDatabase[mRelatedOpeningElement] as IfcFeatureElementSubtraction; } set { mRelatedOpeningElement = value.mIndex; value.mVoidsElement = this; } }

		internal IfcRelVoidsElement() : base() { }
		internal IfcRelVoidsElement(DatabaseIfc db, IfcRelVoidsElement v) : base(db, v) { RelatedOpeningElement = db.Factory.Duplicate(v.RelatedOpeningElement) as IfcFeatureElementSubtraction; }
		public IfcRelVoidsElement(IfcElement host, IfcFeatureElementSubtraction fes)
			: base(host.mDatabase) { mRelatingBuildingElement = host.mIndex; host.mHasOpenings.Add(this); mRelatedOpeningElement = fes.mIndex; fes.mVoidsElement = this; }
		internal static IfcRelVoidsElement Parse(string strDef) { IfcRelVoidsElement i = new IfcRelVoidsElement(); int ipos = 0; parseFields(i, ParserSTEP.SplitLineFields(strDef), ref ipos); return i; }
		internal static void parseFields(IfcRelVoidsElement i, List<string> arrFields, ref int ipos) { IfcRelConnects.parseFields(i, arrFields, ref ipos); i.mRelatingBuildingElement = ParserSTEP.ParseLink(arrFields[ipos++]); i.mRelatedOpeningElement = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mRelatingBuildingElement) + "," + ParserSTEP.LinkToString(mRelatedOpeningElement); }
		internal override void postParseRelate()
		{
			base.postParseRelate();
			IfcElement elem = RelatingBuildingElement;
			if (elem != null)
				elem.mHasOpenings.Add(this);
			IfcFeatureElementSubtraction es = RelatedOpeningElement;
			if (es != null)
				es.mVoidsElement = this;
		}
	}
	
}
