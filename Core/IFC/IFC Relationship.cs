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
using System.Collections.ObjectModel;
using System.Text;
using System.Reflection;
using System.IO;
using System.ComponentModel;
using System.Linq;
using GeometryGym.STEP;

namespace GeometryGym.Ifc
{
	public partial class IfcRelAggregates : IfcRelDecomposes
	{
		internal int mRelatingObject;// : IfcObjectDefinition IFC4 IfcObject
		internal List<int> mRelatedObjects = new List<int>();// : SET [1:?] OF IfcObjectDefinition; 

		public IfcObjectDefinition RelatingObject { get { return mDatabase[mRelatingObject] as IfcObjectDefinition; } set { mRelatingObject = value.mIndex; value.mIsDecomposedBy.Add(this); } }
		public ReadOnlyCollection<IfcObjectDefinition> RelatedObjects { get { return new ReadOnlyCollection<IfcObjectDefinition>( mRelatedObjects.ConvertAll(x => mDatabase[x] as IfcObjectDefinition)); }  }
		
		internal IfcRelAggregates() : base() { }
		internal IfcRelAggregates(DatabaseIfc db, IfcRelAggregates a, bool downStream) : base(db,a)
		{
			RelatingObject = db.Factory.Duplicate(a.RelatingObject,downStream) as IfcObjectDefinition;
			if(downStream)
				a.RelatedObjects.ToList().ConvertAll(x=>db.Factory.Duplicate(x) as IfcObjectDefinition).ForEach(x => addObject(x));
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
			ReadOnlyCollection<IfcObjectDefinition> ods = RelatedObjects;
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
		internal bool removeObject(IfcObjectDefinition o)
		{
			o.mDecomposes = null;
			if (mRelatedObjects.Count == 1 && mRelatedObjects[0] == o.mIndex)
			{
				IfcElementAssembly ea = RelatingObject as IfcElementAssembly;
				if (ea != null && ea.mIsDecomposedBy.Count <= 1)
					ea.detachFromHost();
			}
			return mRelatedObjects.Remove(o.mIndex);
		}
		internal override void changeSchema(ReleaseVersion schema)
		{
			base.changeSchema(schema);
			ReadOnlyCollection<IfcObjectDefinition> ods = RelatedObjects;
			for (int jcounter = 0; jcounter < ods.Count; jcounter++)
				ods[jcounter].changeSchema(schema);
		}
		
	}
	public abstract partial class IfcRelAssigns : IfcRelationship //	ABSTRACT SUPERTYPE OF(ONEOF(IfcRelAssignsToActor, IfcRelAssignsToControl, IfcRelAssignsToGroup, IfcRelAssignsToProcess, IfcRelAssignsToProduct, IfcRelAssignsToResource))
	{
		internal List<int> mRelatedObjects = new List<int>();// : SET [1:?] OF IfcObjectDefinition;
		internal IfcObjectTypeEnum mRelatedObjectsType = IfcObjectTypeEnum.NOTDEFINED;// : OPTIONAL IfcObjectTypeEnum; IFC4 CHANGE  The attribute is deprecated and shall no longer be used. A NIL value should always be assigned.
		public ReadOnlyCollection<IfcObjectDefinition> RelatedObjects { get { return new ReadOnlyCollection<IfcObjectDefinition>( mRelatedObjects.ConvertAll(x => mDatabase[x] as IfcObjectDefinition)); } }

		protected IfcRelAssigns() : base() { }
		protected IfcRelAssigns(DatabaseIfc db) : base(db) { }
		protected IfcRelAssigns(DatabaseIfc db, IfcRelAssigns a, bool downStream) : base(db, a)
		{
			if(downStream)
				a.RelatedObjects.ToList().ForEach(x => AddRelated(db.Factory.Duplicate(x) as IfcObjectDefinition));
			mRelatedObjectsType = a.mRelatedObjectsType;
		}
		protected IfcRelAssigns(IfcObjectDefinition related) : this(new List<IfcObjectDefinition>() { related }) { }
		protected IfcRelAssigns(List<IfcObjectDefinition> relObjects) : base(relObjects[0].mDatabase) { foreach(IfcObjectDefinition od in  relObjects) AddRelated(od); }
		protected override string BuildStringSTEP()
		{
			string str = base.BuildStringSTEP() + ",(" + ParserSTEP.LinkToString(mRelatedObjects[0]);
			if (mRelatedObjects.Count > 200)
			{
				StringBuilder sb = new StringBuilder();
				sb.Append(str);
				for (int icounter = 1; icounter < mRelatedObjects.Count; icounter++)
					sb.Append(",#" + mRelatedObjects[icounter]);
				sb.Append(")," + (mDatabase.Release == ReleaseVersion.IFC2x3 && mRelatedObjectsType != IfcObjectTypeEnum.NOTDEFINED ? "." + mRelatedObjectsType + "." : "$"));
				return sb.ToString();
			}
			else
			{
				for (int icounter = 1; icounter < mRelatedObjects.Count; icounter++)
					str += ",#" + mRelatedObjects[icounter];
				return str + ")," + (mDatabase.Release == ReleaseVersion.IFC2x3 && mRelatedObjectsType != IfcObjectTypeEnum.NOTDEFINED ? "." + mRelatedObjectsType + "." : "$");
			}
		}
		protected static void parseFields(IfcRelAssigns c, List<string> arrFields, ref int ipos)
		{
			IfcRelationship.parseFields(c, arrFields, ref ipos);
			c.mRelatedObjects = ParserSTEP.SplitListLinks(arrFields[ipos++]);
			if (!Enum.TryParse<IfcObjectTypeEnum>(arrFields[ipos++].Replace(".",""),true, out c.mRelatedObjectsType))
				c.mRelatedObjectsType = IfcObjectTypeEnum.NOTDEFINED;
		}
		protected override void Parse(string str, ref int pos, int len)
		{
			base.Parse(str, ref pos, len);
			mRelatedObjects = ParserSTEP.StripListLink(str, ref pos, len);
			string field = ParserSTEP.StripField(str, ref pos, len);
			if (!Enum.TryParse<IfcObjectTypeEnum>(field.Replace(".",""),true, out mRelatedObjectsType))
				mRelatedObjectsType = IfcObjectTypeEnum.NOTDEFINED;
		}

		public void AddRelated(IfcObjectDefinition o) { mRelatedObjects.Add(o.mIndex); o.mHasAssignments.Add(this); }
		internal override void postParseRelate()
		{
			base.postParseRelate();
			ReadOnlyCollection<IfcObjectDefinition> ods = RelatedObjects;
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
	[Obsolete("DEPRECEATED IFC4", false)]
	public partial class IfcRelAssignsTasks : IfcRelAssignsToControl // IFC4 depreceated
	{
		internal int mTimeForTask;// :  	OPTIONAL IfcScheduleTimeControl; 

		public IfcScheduleTimeControl TimeForTask { get { return mDatabase[mTimeForTask] as IfcScheduleTimeControl; } set { mTimeForTask = value == null ? 0 : value.mIndex; } }
		internal IfcWorkControl WorkControl { get { return mDatabase[mRelatingControl] as IfcWorkControl; } }

		internal new ReadOnlyCollection<IfcTask> RelatedObjects { get { return new ReadOnlyCollection<IfcTask>(base.RelatedObjects.Cast<IfcTask>().ToList()); } }

		internal IfcRelAssignsTasks() : base() { }
		internal IfcRelAssignsTasks(DatabaseIfc db, IfcRelAssignsTasks r, bool downStream) : base(db,r,downStream) { if(r.mTimeForTask > 0) TimeForTask = db.Factory.Duplicate(r.TimeForTask) as IfcScheduleTimeControl; }
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

		public IfcActor RelatingActor { get { return mDatabase[mRelatingActor] as IfcActor; } set { mRelatingActor = value.mIndex; value.mIsActingUpon.Add(this); } }
		public IfcActorRole ActingRole { get { return mDatabase[mActingRole] as IfcActorRole; } set { mActingRole = value == null ? 0 : value.mIndex; } }

		internal IfcRelAssignsToActor() : base() { }
		internal IfcRelAssignsToActor(DatabaseIfc db, IfcRelAssignsToActor r, bool downStream) : base(db, r, downStream) { RelatingActor = db.Factory.Duplicate(r.RelatingActor,false) as IfcActor; if (r.mActingRole > 0) ActingRole = db.Factory.Duplicate(r.ActingRole) as IfcActorRole; } 
		public IfcRelAssignsToActor(IfcActor relActor) : base(relActor.mDatabase) { RelatingActor = relActor; }
		public IfcRelAssignsToActor(IfcActor relActor, IfcObjectDefinition relObject) : base(relObject) { RelatingActor = relActor; }
		public IfcRelAssignsToActor(IfcActor relActor, List<IfcObjectDefinition> relObjects) : base(relObjects) { RelatingActor = relActor; }
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
		public IfcControl RelatingControl { get { return mDatabase[mRelatingControl] as IfcControl; } set { mRelatingControl = value.mIndex; } }

		internal IfcRelAssignsToControl() : base() { }
		internal IfcRelAssignsToControl(DatabaseIfc db, IfcRelAssignsToControl r, bool downStream) : base(db,r,downStream) { RelatingControl = db.Factory.Duplicate(r.RelatingControl,false) as IfcControl; }
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
		internal IfcRelAssignsToGroup(DatabaseIfc db, IfcRelAssignsToGroup a, bool downStream) : base(db, a, downStream) { RelatingGroup = db.Factory.Duplicate(a.RelatingGroup,false) as IfcGroup; }
		internal IfcRelAssignsToGroup(IfcGroup relgroup) : base(relgroup.mDatabase) { RelatingGroup = relgroup; }
		internal IfcRelAssignsToGroup(IfcGroup relgroup, List<IfcObjectDefinition> relObjects) : base(relObjects) { mRelatingGroup = relgroup.mIndex; }
		internal static IfcRelAssignsToGroup Parse(string str) { IfcRelAssignsToGroup a = new IfcRelAssignsToGroup(); int pos = 0; a.Parse(str, ref pos, str.Length); return a; }
		protected override void Parse(string str, ref int pos, int len)
		{
			base.Parse(str, ref pos, len);
			mRelatingGroup = ParserSTEP.StripLink(str, ref pos, len);
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
		public double Factor { get { return mFactor; } set { mFactor = value; } }

		internal IfcRelAssignsToGroupByFactor() : base() { }
		internal IfcRelAssignsToGroupByFactor(DatabaseIfc db, IfcRelAssignsToGroupByFactor a, bool downStream) : base(db,a,downStream) { mFactor = a.mFactor; }
		internal IfcRelAssignsToGroupByFactor(IfcGroup relgroup, double factor) : base(relgroup) { mFactor = factor; }
		internal IfcRelAssignsToGroupByFactor(IfcGroup relgroup, List<IfcObjectDefinition> relObjects, double factor) : base(relgroup, relObjects) { mFactor = factor; }
		internal new static IfcRelAssignsToGroup Parse(string str)
		{
			IfcRelAssignsToGroupByFactor a = new IfcRelAssignsToGroupByFactor();
			int pos = 0, len = str.Length;
			a.Parse(str, ref pos, len);
			a.mFactor = ParserSTEP.StripDouble(str, ref pos, len);
			return a;
		}
		protected override string BuildStringSTEP() { return (mRelatedObjects.Count == 0 ? "" : base.BuildStringSTEP() + (mDatabase.Release == ReleaseVersion.IFC2x3 ? "" : "," + ParserSTEP.DoubleToString(mFactor))); }
	}
	public partial class IfcRelAssignsToProcess : IfcRelAssigns
	{
		internal int mRelatingProcess;// : IfcProcess; 
		public IfcProcess RelatingProcess { get { return mDatabase[mRelatingProcess] as IfcProcess; } set { mRelatingProcess = value.mIndex; } }
		internal IfcRelAssignsToProcess() : base() { }
		internal IfcRelAssignsToProcess(DatabaseIfc db, IfcRelAssignsToProcess r, bool downStream) : base(db,r,downStream) { RelatingProcess = db.Factory.Duplicate(r.RelatingProcess,false) as IfcProcess; }
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
		public IfcProductSelect RelatingProduct { get { return mDatabase[mRelatingProduct] as IfcProductSelect; } set { mRelatingProduct = value.Index; value.Assign(this); } }

		internal IfcRelAssignsToProduct() : base() { }
		internal IfcRelAssignsToProduct(DatabaseIfc db, IfcRelAssignsToProduct r, bool downStream) : base(db,r,downStream)
		{
			//mRelatingProduct = db.Factory.Duplicate(r.mDatabase[r.mRelatingProduct],false).mIndex;
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
				p.Assign(this);
		}

		internal override void changeSchema(ReleaseVersion schema)
		{
			base.changeSchema(schema);
			mDatabase[mRelatingProduct].changeSchema(schema);
			if (schema == ReleaseVersion.IFC2x3)
			{
				IfcProduct product = RelatingProduct as IfcProduct;
				if (product == null)
					mDatabase[mIndex] = null;
			}
		}
	}
	//[Obsolete("DEPRECEATED IFC4", false)]
	//ENTITY IfcRelAssignsToProjectOrder // DEPRECEATED IFC4 
	public partial class IfcRelAssignsToResource : IfcRelAssigns
	{
		internal int mRelatingResource;// : IfcResource; 
		public IfcResource RelatingResource { get { return mDatabase[mRelatingResource] as IfcResource; } set { mRelatingResource = value.mIndex; } }
		internal IfcRelAssignsToResource() : base() { }
		internal IfcRelAssignsToResource(DatabaseIfc db, IfcRelAssignsToResource r, bool downStream) : base(db,r,downStream) { RelatingResource = db.Factory.Duplicate(r.RelatingResource,false) as IfcResource; }
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
		public ReadOnlyCollection<IfcDefinitionSelect> RelatedObjects { get { return new ReadOnlyCollection<IfcDefinitionSelect>( mRelatedObjects.ConvertAll(x => mDatabase[x] as IfcDefinitionSelect)); } }

		protected IfcRelAssociates() : base() { }
		protected IfcRelAssociates(DatabaseIfc db) : base(db) { }
		protected IfcRelAssociates(DatabaseIfc db, IfcRelAssociates r) : base(db, r)
		{
			//RelatedObjects = r.mRelatedObjects.ConvertAll(x => db.Factory.Duplicate(r.mDatabase[x]) as IfcDefinitionSelect);
		}
		protected IfcRelAssociates(IfcDefinitionSelect related) : base(related.Database) { addRelated(related); }
		protected IfcRelAssociates(List<IfcDefinitionSelect> related) : base(related[0].Database) { related.ForEach(x => addRelated(x)); }
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
		protected override void Parse(string str, ref int pos, int len)
		{
			base.Parse(str, ref pos, len);
			mRelatedObjects = ParserSTEP.StripListLink(str, ref pos, len);
		}
		internal void addRelated(IfcDefinitionSelect obj)
		{
			if (!mRelatedObjects.Contains(obj.Index))
			{
				mRelatedObjects.Add(obj.Index);
				obj.Associate(this);
			}
		}
		internal void unassign(IfcDefinitionSelect d) { mRelatedObjects.Remove(d.Index); d.Remove(this); }
		
		public override string ToString() { return (mRelatedObjects.Count == 0 ? "" : base.ToString()); }
		internal override void postParseRelate()
		{
			base.postParseRelate();
			ReadOnlyCollection<IfcDefinitionSelect> objects = RelatedObjects;
			for (int icounter = 0; icounter < objects.Count; icounter++)
			{
				IfcDefinitionSelect r = objects[icounter];
				if (r != null)
					r.Associate(this);
			}
		}
	}
	//[Obsolete("DEPRECEATED IFC4", false)]
	//ENTITY IfcRelAssociatesAppliedValue // DEPRECEATED IFC4
	//ENTITY IfcRelAssociatesApproval
	public partial class IfcRelAssociatesClassification : IfcRelAssociates
	{
		internal int mRelatingClassification;// : IfcClassificationSelect; IFC2x3  	IfcClassificationNotationSelect
		public IfcClassificationSelect RelatingClassification
		{
			get { return mDatabase[mRelatingClassification] as IfcClassificationSelect; }
			set { mRelatingClassification = value.Index; value.ClassifyObjects(this); }
		}

		internal IfcRelAssociatesClassification() : base() { }
		internal IfcRelAssociatesClassification(DatabaseIfc db, IfcRelAssociatesClassification r) : base(db, r)
		{
			RelatingClassification = db.Factory.Duplicate(r.mDatabase[r.mRelatingClassification]) as IfcClassificationSelect;
		}
		internal IfcRelAssociatesClassification(IfcClassificationSelect classification) : base(classification.Database) { RelatingClassification = classification; }
		public IfcRelAssociatesClassification(IfcClassificationSelect classification, IfcDefinitionSelect related) : base(related) { RelatingClassification = classification; }
		
		internal static IfcRelAssociatesClassification Parse(string strDef)
		{
			IfcRelAssociatesClassification a = new IfcRelAssociatesClassification();
			int pos = 0, len = strDef.Length;
			a.Parse(strDef, ref pos, len);
			a.mRelatingClassification = ParserSTEP.StripLink(strDef, ref pos, len);
			return a;
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mRelatingClassification); }
		internal override void postParseRelate()
		{
			base.postParseRelate();
			RelatingClassification.ClassifyObjects(this);
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

		internal static IfcRelAssociatesConstraint Parse(string str)
		{
			IfcRelAssociatesConstraint a = new IfcRelAssociatesConstraint();
			int pos = 0, len = str.Length;
			a.Parse(str, ref pos, len);
			a.mIntent = ParserSTEP.StripString(str, ref pos, len);
			a.mRelatingConstraint = ParserSTEP.StripLink(str, ref pos, len);
			return a;
		}
		protected override string BuildStringSTEP() { return (RelatingConstraint == null ? "" : base.BuildStringSTEP() + (mIntent == "$" ? ",$," : ",'" + mIntent + "',") + ParserSTEP.LinkToString(mRelatingConstraint)); }
		internal override void postParseRelate()
		{
			base.postParseRelate();
			RelatingConstraint.mConstraintForObjects.Add(this);
		}

		internal override void changeSchema(ReleaseVersion schema)
		{
			base.changeSchema(schema);
			if (schema == ReleaseVersion.IFC2x3)
			{
				IfcConstraint constraint = RelatingConstraint;
				if (constraint != null)
					constraint.Destruct(true);
				return;
#warning implement
				IfcMetric metric = RelatingConstraint as IfcMetric;
				// 	TYPE IfcMetricValueSelect = SELECT (IfcDateTimeSelect, IfcMeasureWithUnit, IfcTable, IfcText, IfcTimeSeries, IfcCostValue);
				if (metric != null)
				{
					IfcValue value = metric.mDataValueValue;
					if (value == null)
					{
						IfcMetricValueSelect mv = metric.DataValue;
						if (mv == null || (mv as IfcMeasureWithUnit == null && mv as IfcDateTimeSelect == null && mv as IfcTable == null && mv as IfcTimeSeries == null && mv as IfcCostValue == null))
						{
							mDatabase[mRelatingConstraint] = null;//.Destruct(true);
							return;
						}
					}
					else
					{
						if (value as IfcText == null)
						{
							mDatabase[mRelatingConstraint] = null;//.Destruct(true);
							return;
						}
					}
				}

				if (mIntent == "$")
					mIntent = "UNKNOWN";
			}
			RelatingConstraint.changeSchema(schema);
		}
	}
	public partial class IfcRelAssociatesDocument : IfcRelAssociates
	{
		internal int mRelatingDocument;// : IfcDocumentSelect; 
		public IfcDocumentSelect RelatingDocument { get { return mDatabase[mRelatingDocument] as IfcDocumentSelect; } set { mRelatingDocument = value.Index; value.Associate(this); } }
		internal IfcRelAssociatesDocument() : base() { }
		internal IfcRelAssociatesDocument(DatabaseIfc db, IfcRelAssociatesDocument r) : base(db,r) { RelatingDocument = db.Factory.Duplicate(r.mDatabase[r.mRelatingDocument]) as IfcDocumentSelect; }
		public IfcRelAssociatesDocument(IfcDocumentSelect document) : base(document.Database) { Name = "DocAssoc"; Description = "Document Associates"; mRelatingDocument = document.Index; document.Associate(this); }
		public IfcRelAssociatesDocument(IfcDefinitionSelect related, IfcDocumentSelect document) : base(related) { RelatingDocument = document; }
		public IfcRelAssociatesDocument(List<IfcDefinitionSelect> related, IfcDocumentSelect document) : base(related) { RelatingDocument = document; }
		internal static IfcRelAssociatesDocument Parse(string str)
		{
			IfcRelAssociatesDocument a = new IfcRelAssociatesDocument();
			int pos = 0, len = str.Length;
			a.Parse(str, ref pos, len);
			a.mRelatingDocument = ParserSTEP.StripLink(str, ref pos, len);
			return a;
		}
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mRelatingDocument); }
		internal IfcDocumentSelect getDocument() { return mDatabase[mRelatingDocument] as IfcDocumentSelect; }
	}
	public partial class IfcRelAssociatesLibrary : IfcRelAssociates
	{
		internal int mRelatingLibrary;// : IfcLibrarySelect; 
		public IfcLibrarySelect RelatingLibrary { get { return mDatabase[mRelatingLibrary] as IfcLibrarySelect; } set { mRelatingLibrary = value.Index; } }
		internal IfcRelAssociatesLibrary() : base() { }
		internal IfcRelAssociatesLibrary(DatabaseIfc db, IfcRelAssociatesLibrary r) : base(db,r) { RelatingLibrary = db.Factory.Duplicate(r.mDatabase[r.mRelatingLibrary]) as IfcLibrarySelect; }
		internal IfcRelAssociatesLibrary(DatabaseIfc m, IfcLibrarySelect lib) : base(m) { Name = "LibAssoc"; Description = "Library Associates"; mRelatingLibrary = lib.Index; }
		internal static IfcRelAssociatesLibrary Parse(string str)
		{
			IfcRelAssociatesLibrary a = new IfcRelAssociatesLibrary();
			int pos = 0, len = str.Length;
			a.Parse(str, ref pos, len);
			a.mRelatingLibrary = ParserSTEP.StripLink(str, ref pos, len);
			return a;
		}
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
		internal static IfcRelAssociatesMaterial Parse(string str)
		{
			IfcRelAssociatesMaterial a = new IfcRelAssociatesMaterial();
			int pos = 0, len = str.Length;
			a.Parse(str, ref pos, len);
			a.mRelatingMaterial = ParserSTEP.StripLink(str, ref pos, len);
			return a;
		}
		protected override string BuildStringSTEP()
		{
			if (mDatabase.mRelease == ReleaseVersion.IFC2x3 && string.IsNullOrEmpty(RelatingMaterial.ToString()))
				return "";
			return base.BuildStringSTEP() + ",#" + mRelatingMaterial;
		}
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
		internal double mProfileOrientationValue = double.NaN;// : OPTIONAL IfcOrientationSelect; //TYPE IfcOrientationSelect = SELECT(IfcPlaneAngleMeasure,IfcDirection);
		internal int mProfileOrientation = 0; // : OPTIONAL IfcOrientationSelect;

		public IfcProfileProperties RelatingProfileProperties { get { return mDatabase[mRelatingProfileProperties] as IfcProfileProperties; } set { mRelatingProfileProperties = value.mIndex; } }
		public IfcShapeAspect ProfileSectionLocation { get { return mDatabase[mProfileSectionLocation] as IfcShapeAspect; } set { mProfileSectionLocation = value == null ? 0 : value.mIndex; } }
		 
		internal IfcRelAssociatesProfileProperties() : base() { }
		internal IfcRelAssociatesProfileProperties(IfcProfileProperties pp) : base(pp.mDatabase) { if (pp.mDatabase.mRelease != ReleaseVersion.IFC2x3) throw new Exception(KeyWord + " Deleted in IFC4"); mRelatingProfileProperties = pp.mIndex; }
		internal IfcRelAssociatesProfileProperties(DatabaseIfc db, IfcRelAssociatesProfileProperties r) : base(db,r)
		{
			RelatingProfileProperties = db.Factory.Duplicate(r.RelatingProfileProperties) as IfcProfileProperties; 
			if (r.mProfileSectionLocation > 0)
				ProfileSectionLocation = db.Factory.Duplicate(r.ProfileSectionLocation) as IfcShapeAspect;
			mProfileOrientation = r.mProfileOrientation;
			if (double.IsNaN(r.mProfileOrientationValue))
			{
				if (r.mProfileOrientation > 0)
					mProfileOrientation = db.Factory.Duplicate(r.mDatabase[r.mProfileOrientation]).mIndex;
			}
			else
				mProfileOrientationValue = r.mProfileOrientationValue;
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
				a.mProfileOrientationValue = ParserSTEP.ParseDouble(str.Substring(21, str.Length - 22));
			}
			else
				a.mProfileOrientation = ParserSTEP.ParseLink(arrFields[ipos++]);
		}
		protected override string BuildStringSTEP()
		{
			if (mRelatedObjects.Count == 0)
				return "";
			string str = base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mRelatingProfileProperties) + "," +
				ParserSTEP.LinkToString(mProfileSectionLocation) + ",";
			if (double.IsNaN(mProfileOrientationValue))
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
		internal IfcRelationship(DatabaseIfc db) : base(db) {  }
		protected IfcRelationship(DatabaseIfc db, IfcRelationship r) : base(db, r) { }
		protected static void parseFields(IfcRelationship r, List<string> arrFields, ref int ipos) { IfcRoot.parseFields(r, arrFields, ref ipos); }
	}

	public abstract partial class IfcRelConnects : IfcRelationship //ABSTRACT SUPERTYPE OF (ONEOF (IfcRelConnectsElements ,IfcRelConnectsPortToElement ,IfcRelConnectsPorts ,IfcRelConnectsStructuralActivity ,IfcRelConnectsStructuralMember
	{  //,IfcRelContainedInSpatialStructure ,IfcRelCoversBldgElements ,IfcRelCoversSpaces ,IfcRelFillsElement ,IfcRelFlowControlElements ,IfcRelInterferesElements ,IfcRelReferencedInSpatialStructure ,IfcRelSequence ,IfcRelServicesBuildings ,IfcRelSpaceBoundary))
		protected IfcRelConnects() : base() { }
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
		//	RelatingElement = db.Factory.Duplicate(r.RelatingElement) as IfcElement; Handled at element
		//	RelatedElement = db.Factory.Duplicate( r.RelatedElement) as IfcElement; Handled at element
		}
		internal IfcRelConnectsElements(IfcElement relating, IfcElement related) : base(relating.mDatabase)
		{
			mRelatingElement = relating.mIndex;
			relating.mConnectedFrom.Add(this);
			mRelatedElement = related.mIndex;
			related.mConnectedTo.Add(this);
		}
		internal static IfcRelConnectsElements Parse(string strDef) { IfcRelConnectsElements i = new IfcRelConnectsElements(); int ipos = 0; parseFields(i, ParserSTEP.SplitLineFields(strDef), ref ipos); return i; }
		internal static void parseFields(IfcRelConnectsElements i, List<string> arrFields, ref int ipos) { IfcRelConnects.parseFields(i, arrFields, ref ipos); i.mConnectionGeometry = ParserSTEP.ParseLink(arrFields[ipos++]); i.mRelatingElement = ParserSTEP.ParseLink(arrFields[ipos++]); i.mRelatedElement = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildStringSTEP() { return (mRelatingElement == 0 || mRelatedElement == 0 ? "" : base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mConnectionGeometry) + "," + ParserSTEP.LinkToString(mRelatingElement) + "," + ParserSTEP.LinkToString(mRelatedElement)); }
		internal override void postParseRelate()
		{
			base.postParseRelate();
			IfcElement relating = RelatingElement, related = RelatedElement;
			if (relating != null)
				relating.mConnectedTo.Add(this);
			if (related != null)
				related.mConnectedFrom.Add(this);
		}
		internal IfcElement getConnected(IfcElement e) { return mDatabase[(mRelatingElement == e.mIndex ? mRelatedElement : mRelatingElement)] as IfcElement; }
	}
	public partial class IfcRelConnectsPathElements : IfcRelConnectsElements
	{
		internal List<int> mRelatingPriorities = new List<int>();// : LIST [0:?] OF INTEGER;
		internal List<int> mRelatedPriorities = new List<int>();// : LIST [0:?] OF INTEGER;

		internal IfcConnectionTypeEnum mRelatedConnectionType = IfcConnectionTypeEnum.NOTDEFINED;// : IfcConnectionTypeEnum;
		internal IfcConnectionTypeEnum mRelatingConnectionType = IfcConnectionTypeEnum.NOTDEFINED;// : IfcConnectionTypeEnum; 

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

		public IfcPort RelatingPort { get { return mDatabase[mRelatingPort] as IfcPort; } set { mRelatingPort = value.mIndex; } }
		public IfcPort RelatedPort { get { return mDatabase[mRelatedPort] as IfcPort; } set { mRelatedPort = value.mIndex; } }
		public IfcElement RealizingElement { get { return mDatabase[mRealizingElement] as IfcElement; } set { mRealizingElement = value == null ? 0 : value.mIndex; } }

		internal IfcRelConnectsPorts() : base() { }
		internal IfcRelConnectsPorts(DatabaseIfc db, IfcRelConnectsPorts r) : base(db,r)
		{
			RelatingPort = db.Factory.Duplicate(r.RelatingPort) as IfcPort;
			RelatedPort = db.Factory.Duplicate(r.RelatedPort) as IfcPort;
			if(r.mRealizingElement > 0)
				RealizingElement = db.Factory.Duplicate(r.RealizingElement) as IfcElement;
		}
		public IfcRelConnectsPorts(IfcPort relatingPort, IfcPort relatedPort) : base(relatingPort.mDatabase) { RelatingPort = relatingPort; RelatedPort = relatedPort; }
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

		public IfcPort RelatingPort { get { return mDatabase[mRelatingPort] as IfcPort; } set { mRelatingPort = value.mIndex; } }
		public IfcElement RelatedElement { get { return mDatabase[mRelatedElement] as IfcElement; } set { mRelatedElement = value.mIndex; } }

		internal IfcRelConnectsPortToElement() : base() { }
		internal IfcRelConnectsPortToElement(DatabaseIfc db, IfcRelConnectsPortToElement r) : base(db,r) { RelatingPort = db.Factory.Duplicate(r.RelatingPort) as IfcPort; RelatedElement = db.Factory.Duplicate(r.RelatedElement) as IfcElement; }
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

		public IfcStructuralActivityAssignmentSelect RelatingElement { get { return mDatabase[mRelatingElement] as IfcStructuralActivityAssignmentSelect; } set { mRelatingElement = value.Index; value.AssignStructuralActivity(this); } }
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
			saa.AssignStructuralActivity(this);
		}
	}
	public partial class IfcRelConnectsStructuralElement : IfcRelConnects //DELETED IFC4 Replaced by IfcRelAssignsToProduct
	{
		internal int mRelatingElement;// : IfcElement;
		internal int mRelatedStructuralMember;// : IfcStructuralMember; 

		public IfcElement RelatingElement { get { return mDatabase[mRelatingElement] as IfcElement; } set { mRelatingElement = value.mIndex; value.mHasStructuralMember.Add(this); } }
		public IfcStructuralMember RelatedStructuralMember { get { return mDatabase[mRelatedStructuralMember] as IfcStructuralMember; } set { mRelatedStructuralMember = value.mIndex; value.mStructuralMemberForGG = this; } }

		internal IfcRelConnectsStructuralElement() : base() { }
		internal IfcRelConnectsStructuralElement(DatabaseIfc db, IfcRelConnectsStructuralElement c) : base(db,c)
		{
			RelatingElement = db.Factory.Duplicate(c.RelatingElement) as IfcElement; 
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

		public ReadOnlyCollection<IfcElement> RealizingElements { get { return new ReadOnlyCollection<IfcElement>( mRealizingElements.ConvertAll(x => mDatabase[x] as IfcElement)); } }
		
		internal IfcRelConnectsWithRealizingElements() : base() { }
		internal IfcRelConnectsWithRealizingElements(DatabaseIfc db, IfcRelConnectsWithRealizingElements r) : base(db,r) { r.RealizingElements.ToList().ConvertAll(x => db.Factory.Duplicate(x) as IfcElement).ForEach(x=>addRealizingElement(x)); mConnectionType = r.mConnectionType;  }
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

		internal void addRealizingElement(IfcElement element)
		{
			mRealizingElements.Add(element.mIndex);
			element.mIsConnectionRealization.Add(this);
		}
	}
	public partial class IfcRelContainedInSpatialStructure : IfcRelConnects
	{
		internal List<int> mRelatedElements = new List<int>();// : SET [1:?] OF IfcProduct;
		private int mRelatingStructure;//  IfcSpatialElement 

		public ReadOnlyCollection<IfcProduct> RelatedElements { get { return new ReadOnlyCollection<IfcProduct>( mRelatedElements.ConvertAll(x => mDatabase[x] as IfcProduct)); } } 
		public IfcSpatialElement RelatingStructure { get { return mDatabase[mRelatingStructure] as IfcSpatialElement; } set { mRelatingStructure = value.mIndex; value.mContainsElements.Add(this); } }

		internal IfcRelContainedInSpatialStructure() : base() { }
		internal IfcRelContainedInSpatialStructure(DatabaseIfc db, IfcRelContainedInSpatialStructure r,bool downstream) : base(db, r)
		{
			if(downstream)
				r.RelatedElements.ToList().ForEach(x => addRelated( db.Factory.Duplicate(x) as IfcProduct));
			RelatingStructure = db.Factory.Duplicate(r.RelatingStructure,false) as IfcSpatialElement;
		}
		internal IfcRelContainedInSpatialStructure(IfcSpatialElement host) : base(host.mDatabase)
		{
			string containerName = "";
			if (host as IfcBuilding != null)
				containerName = "Building";
			else if (host as IfcBuildingStorey != null)
				containerName = "BuildingStorey";
			else if (host as IfcExternalSpatialElement != null)
				containerName = "ExternalSpatialElement";
			else if (host as IfcSite != null)
				containerName = "Site";
			else if (host as IfcSpace != null)
				containerName = "Space";
			Name = containerName;
			Description = containerName + " Container for Elements";
			mRelatingStructure = host.mIndex;
			host.mContainsElements.Add(this);
		}
		internal IfcRelContainedInSpatialStructure(IfcProduct related, IfcSpatialElement host) : this(host) { relate(related); }	
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
		internal static IfcRelContainedInSpatialStructure Parse(string str)
		{
			IfcRelContainedInSpatialStructure c = new IfcRelContainedInSpatialStructure();
			int pos = 0, len = str.Length;
			c.Parse(str, ref pos, len);
			c.mRelatedElements = ParserSTEP.StripListLink(str, ref pos, len);
			c.mRelatingStructure = ParserSTEP.StripLink(str, ref pos, len);
			return c;
		}
		internal override void postParseRelate()
		{
			base.postParseRelate();
			IfcSpatialElement se = RelatingStructure;
			if (se != null)
				se.mContainsElements.Add(this);
			ReadOnlyCollection<IfcProduct> products = RelatedElements;
			for (int icounter = 0; icounter < products.Count; icounter++)
				relate( products[icounter] as IfcProduct);
		}
		internal void addRelated(IfcProduct product) { if (!mRelatedElements.Contains(product.mIndex)) { mRelatedElements.Add(product.mIndex); relate(product); } }
		private void relate(IfcProduct product)
		{
			IfcElement element = product as IfcElement;
			if (element != null)
			{
				if(element.mContainedInStructure != null)
					element.mContainedInStructure.removeObject(element);
				element.mContainedInStructure = this;
			}
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

		public IfcElement RelatingBuildingElement { get { return mDatabase[mRelatingBuildingElement] as IfcElement; } set { mRelatingBuildingElement = value.mIndex; } }
		public ReadOnlyCollection<IfcCovering> RelatedCoverings { get { return new ReadOnlyCollection<IfcCovering>(mRelatedCoverings.ConvertAll(x => mDatabase[x] as IfcCovering)); } }

		internal IfcRelCoversBldgElements() : base() { }
		internal IfcRelCoversBldgElements(DatabaseIfc db, IfcRelCoversBldgElements c) : base(db,c) { RelatingBuildingElement = db.Factory.Duplicate(c.RelatingBuildingElement) as IfcElement; c.RelatedCoverings.ToList().ForEach(x => addCovering( db.Factory.Duplicate(x) as IfcCovering)); }
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
			ReadOnlyCollection<IfcCovering> coverings = RelatedCoverings;
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

		public IfcSpace RelatedSpace { get { return mDatabase[mRelatedSpace] as IfcSpace; } set { mRelatedSpace = value.mIndex; value.mHasCoverings.Add(this); } }
		public ReadOnlyCollection<IfcCovering> RelatedCoverings { get { return new ReadOnlyCollection<IfcCovering>( mRelatedCoverings.ConvertAll(x => mDatabase[x] as IfcCovering)); } }

		internal IfcRelCoversSpaces() : base() { }
		internal IfcRelCoversSpaces(DatabaseIfc db, IfcRelCoversSpaces r) : base(db,r) { RelatedSpace = db.Factory.Duplicate(r.RelatedSpace) as IfcSpace; r.RelatedCoverings.ToList().ForEach(x => addCovering( db.Factory.Duplicate(x) as IfcCovering)); }
		internal IfcRelCoversSpaces(IfcSpace s, IfcCovering covering) : base(s.mDatabase)
		{
			RelatedSpace = s;
			if (covering != null)
				addCovering(covering);
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
		public ReadOnlyCollection<IfcDefinitionSelect> RelatedDefinitions { get { return new ReadOnlyCollection<IfcDefinitionSelect>( mRelatedDefinitions.ConvertAll(x => mDatabase[x] as IfcDefinitionSelect)); } }

		internal IfcRelDeclares() : base() { }
		internal IfcRelDeclares(IfcContext c) : base(c.mDatabase) { mRelatingContext = c.mIndex; c.mDeclares.Add(this); }
		internal IfcRelDeclares(DatabaseIfc db, IfcRelDeclares r,bool downStream) : base(db,r)
		{
			RelatingContext = db.Factory.Duplicate(r.RelatingContext, false) as IfcContext; 
			if(downStream)
				r.mRelatedDefinitions.ForEach(x => AddRelated( db.Factory.Duplicate( r.mDatabase[x]) as IfcDefinitionSelect));
		}
		public IfcRelDeclares(IfcContext c, IfcDefinitionSelect def) : this(c, new List<IfcDefinitionSelect>() { def }) { }
		public IfcRelDeclares(IfcContext c, List<IfcDefinitionSelect> defs) : this(c) { defs.ForEach(x => AddRelated(x)); }
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
		internal void AddRelated(IfcDefinitionSelect d)
		{
			if (!mRelatedDefinitions.Contains(d.Index))
			{
				mRelatedDefinitions.Add(d.Index);
				d.HasContext = this;
			}
		}
		internal void RemoveRelated(IfcDefinitionSelect d) { mRelatedDefinitions.Remove(d.Index); d.HasContext = null; }

		internal override void changeSchema(ReleaseVersion schema)
		{
			base.changeSchema(schema);
			for (int icounter = 0; icounter < mRelatedDefinitions.Count; icounter++)
				mDatabase[mRelatedDefinitions[icounter]].changeSchema(schema);
		}
	}
	public abstract partial class IfcRelDecomposes : IfcRelationship //ABSTACT  SUPERTYPE OF (ONEOF (IfcRelAggregates ,IfcRelNests ,IfcRelProjectsElement ,IfcRelVoidsElement))
	{
		protected IfcRelDecomposes() : base() { }
		protected IfcRelDecomposes(DatabaseIfc db) : base(db) { }
		protected IfcRelDecomposes(DatabaseIfc db, IfcRelDecomposes d) : base(db,d) { }
		internal static void parseFields(IfcRelDecomposes d, List<string> arrFields, ref int ipos) { IfcRelationship.parseFields(d, arrFields, ref ipos); }
	}
	public abstract partial class IfcRelDefines : IfcRelationship // 	ABSTRACT SUPERTYPE OF(ONEOF(IfcRelDefinesByObject, IfcRelDefinesByProperties, IfcRelDefinesByTemplate, IfcRelDefinesByType))
	{
		protected IfcRelDefines() : base() { }
		protected IfcRelDefines(DatabaseIfc db) : base(db) { }
		protected IfcRelDefines(DatabaseIfc db, IfcRelDefines d) : base(db, d) { }
		protected static void parseFields(IfcRelDefines r, List<string> arrFields, ref int ipos) { IfcRelationship.parseFields(r, arrFields, ref ipos); }
	}
	public partial class IfcRelDefinesByObject : IfcRelDefines
	{
		internal List<int> mRelatedObjects = new List<int>();// : SET [1:?] OF IfcObject;
		internal int mRelatingObject;// : IfcObject  

		public ReadOnlyCollection<IfcObject> RelatedObjects { get { return new ReadOnlyCollection<IfcObject>( mRelatedObjects.ConvertAll(x => mDatabase[x] as IfcObject)); } }
		public IfcObject RelatingObject { get { return mDatabase[mRelatingObject] as IfcObject; } set { mRelatingObject = value.mIndex; } }

		internal IfcRelDefinesByObject() : base() { }
		internal IfcRelDefinesByObject(DatabaseIfc db, IfcRelDefinesByObject r) : base(db,r) { r.RelatedObjects.ToList().ForEach(x => addRelated( db.Factory.Duplicate(x) as IfcObject)); RelatingObject = db.Factory.Duplicate(r.RelatingObject) as IfcObject; }
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
			ReadOnlyCollection<IfcObject> objects = RelatedObjects;
			for (int icounter = 0; icounter < objects.Count; icounter++)
			{
				IfcObject o = objects[icounter];
				if (o != null)
					o.mDeclares.Add(this);
			}
		}
		internal void addRelated(IfcObject obj) { mRelatedObjects.Add(obj.mIndex); obj.mIsDeclaredBy = this; }
	}
	public partial class IfcRelDefinesByProperties : IfcRelDefines
	{
		private List<int> mRelatedObjects = new List<int>();// IFC4 change	SET [1:1] OF IfcObjectDefinition; ifc2x3 : SET [1:?] OF IfcObject;  
		private int mRelatingPropertyDefinition;// : IfcPropertySetDefinition; 

		public ReadOnlyCollection<IfcObjectDefinition> RelatedObjects { get { return new ReadOnlyCollection<IfcObjectDefinition>( mRelatedObjects.ConvertAll(x => mDatabase[x] as IfcObjectDefinition)); } }
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
		public IfcRelDefinesByProperties(IEnumerable<IfcObjectDefinition> objs, IfcPropertySetDefinition ifcproperty) : this(ifcproperty) { foreach(IfcObjectDefinition od in objs) AddRelated(od); }
		
		protected override string BuildStringSTEP()
		{
			IfcPropertySetDefinition pset = RelatingPropertyDefinition;
			if (mRelatedObjects.Count == 0 || pset == null || pset.isEmpty)
				return "";
			string str = base.BuildStringSTEP() + ",(" + ParserSTEP.LinkToString(mRelatedObjects[0]);
			for (int icounter = 1; icounter < mRelatedObjects.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mRelatedObjects[icounter]);
			return str + ")," + ParserSTEP.LinkToString(mRelatingPropertyDefinition);
		}
		internal static IfcRelDefinesByProperties Parse(string str)
		{
			IfcRelDefinesByProperties d = new IfcRelDefinesByProperties();
			int pos = 0, len = str.Length;
			d.Parse(str, ref pos, len);
			return d;
		}
		protected override void Parse(string str, ref int pos, int len)
		{
			base.Parse(str, ref pos, len);
			mRelatedObjects = ParserSTEP.StripListLink(str, ref pos, len);
			mRelatingPropertyDefinition = ParserSTEP.StripLink(str, ref pos, len);
		}
		public void AddRelated(IfcObjectDefinition od)
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

		protected override List<T> Extract<T>(Type type)
		{
			List<T> result = base.Extract<T>(type);
			result.AddRange(RelatingPropertyDefinition.Extract<T>());
			return result;
		}
		internal override void postParseRelate()
		{
			base.postParseRelate();
			RelatingPropertyDefinition.DefinesOccurrence = this;
			ReadOnlyCollection<IfcObjectDefinition> related = RelatedObjects;
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

		internal override void changeSchema(ReleaseVersion schema)
		{
			base.changeSchema(schema);
			RelatingPropertyDefinition.changeSchema(schema);
		}
	}
	public partial class IfcRelDefinesByTemplate : IfcRelDefines //IFC4
	{
		internal List<int> mRelatedPropertySets = new List<int>();// : SET [1:?] OF IfcPropertySetDefinition;
		internal int mRelatingTemplate;// :	IfcPropertySetTemplate;

		public ReadOnlyCollection<IfcPropertySetDefinition> RelatedPropertySets { get { return new ReadOnlyCollection<IfcPropertySetDefinition>( mRelatedPropertySets.ConvertAll(x => mDatabase[x] as IfcPropertySetDefinition)); } }
		public IfcPropertySetTemplate RelatingTemplate
		{
			get { return mDatabase[mRelatingTemplate] as IfcPropertySetTemplate; }
			set { mRelatingTemplate = value.mIndex; }
		}

		internal IfcRelDefinesByTemplate() : base() { }
		internal IfcRelDefinesByTemplate(DatabaseIfc db, IfcRelDefinesByTemplate r) : base(db,r) { r.RelatedPropertySets.ToList().ForEach(x => AddRelated( db.Factory.Duplicate(x) as IfcPropertySetDefinition)); RelatingTemplate = db.Factory.Duplicate(r.RelatingTemplate) as IfcPropertySetTemplate; }
		public IfcRelDefinesByTemplate(IfcPropertySetTemplate relating) : base(relating.mDatabase) { RelatingTemplate = relating; }
		public IfcRelDefinesByTemplate(IfcPropertySetDefinition related, IfcPropertySetTemplate relating) : this(relating) { AddRelated(related); }
		public IfcRelDefinesByTemplate(List<IfcPropertySetDefinition> related, IfcPropertySetTemplate relating) : this(relating) { related.ForEach(x => AddRelated(x)); }

		protected override string BuildStringSTEP()
		{
			if (mDatabase.Release == ReleaseVersion.IFC2x3 || mRelatedPropertySets.Count == 0)
				return "";
			string str = base.BuildStringSTEP() + ",(" + ParserSTEP.LinkToString(mRelatedPropertySets[0]);
			for (int icounter = 1; icounter < mRelatedPropertySets.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mRelatedPropertySets[icounter]);
			return str + ")," + ParserSTEP.LinkToString(mRelatingTemplate);
		}
		internal static void parseFields(IfcRelDefinesByTemplate t, List<string> arrFields, ref int ipos)
		{
			IfcRelDefines.parseFields(t, arrFields, ref ipos);
			t.mRelatedPropertySets = ParserSTEP.SplitListLinks(arrFields[ipos++]);
			t.mRelatingTemplate = ParserSTEP.ParseLink(arrFields[ipos++]);
		}
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
			ReadOnlyCollection<IfcPropertySetDefinition> psets = RelatedPropertySets;
			for (int icounter = 0; icounter < psets.Count; icounter++)
			{ 
				IfcPropertySetDefinition pset = psets[icounter];
				if (pset != null)
					pset.mIsDefinedBy.Add(this);
			}
		}
		public void AddRelated(IfcPropertySetDefinition pset) { mRelatedPropertySets.Add(pset.mIndex); pset.mIsDefinedBy.Add(this); }
	}
	public partial class IfcRelDefinesByType : IfcRelDefines
	{
		internal List<int> mRelatedObjects = new List<int>();// : SET [1:?] OF IfcObject;
		private int mRelatingType;// : IfcTypeObject  

		public ReadOnlyCollection<IfcObject> RelatedObjects { get { return new ReadOnlyCollection<IfcObject>( mRelatedObjects.ConvertAll(x => mDatabase[x] as IfcObject)); } }
		public IfcTypeObject RelatingType { get { return mDatabase[mRelatingType] as IfcTypeObject; } set { mRelatingType = value.mIndex; } }

		internal IfcRelDefinesByType() : base() { }
		internal IfcRelDefinesByType(IfcTypeObject relType) : base(relType.mDatabase) { mRelatingType = relType.mIndex; relType.mObjectTypeOf = this; }
		internal IfcRelDefinesByType(DatabaseIfc db, IfcRelDefinesByType r) : base(db, r)
		{
			//mRelatedObjects = new List<int>(d.mRelatedObjects.ToArray()); 
			RelatingType = db.Factory.Duplicate(r.RelatingType) as IfcTypeObject;
		}
		public IfcRelDefinesByType(IfcObject related, IfcTypeObject relating) : this(relating) { mRelatedObjects.Add(related.mIndex); }
		public IfcRelDefinesByType(List<IfcObject> related, IfcTypeObject relating) : this(relating) { related.ForEach(x=>AddRelated(x)); }

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
		internal static IfcRelDefinesByType Parse(string str)
		{
			IfcRelDefinesByType t = new IfcRelDefinesByType();
			int pos = 0, len = str.Length;
			t.Parse(str, ref pos, len);
			t.mRelatedObjects = ParserSTEP.StripListLink(str, ref pos, len);
			t.mRelatingType = ParserSTEP.StripLink(str, ref pos, len);
			return t;
		}
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

		public void AddRelated(IfcObject obj) //TODO CHECK CLASS NAME MATCHES INSTANCE using reflection
		{
			if (!mRelatedObjects.Contains(obj.mIndex))
				mRelatedObjects.Add(obj.mIndex);
			if (obj.mIsTypedBy != null && obj.mIsTypedBy != this)
				obj.mIsTypedBy.mRelatedObjects.Remove(obj.mIndex);
			obj.mIsTypedBy = this;
		}
	}
	public partial class IfcRelFillsElement : IfcRelConnects
	{
		private int mRelatingOpeningElement;// : IfcOpeningElement;
		private int mRelatedBuildingElement;// :OPTIONAL IfcElement; 

		public IfcOpeningElement RelatingOpeningElement { get { return mDatabase[mRelatingOpeningElement] as IfcOpeningElement; } set { mRelatingOpeningElement = value.mIndex; } }
		public IfcElement RelatedBuildingElement { get { return mDatabase[mRelatedBuildingElement] as IfcElement; } set { mRelatedBuildingElement = value.mIndex; } }

		internal IfcRelFillsElement() : base() { }
		internal IfcRelFillsElement(DatabaseIfc db, IfcRelFillsElement r) : base(db,r) { RelatingOpeningElement = db.Factory.Duplicate(r) as IfcOpeningElement; RelatedBuildingElement = db.Factory.Duplicate(r.RelatedBuildingElement) as IfcElement; }
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

		public IfcPort RelatingPort { get { return mDatabase[mRelatingPort] as IfcPort; } set { mRelatingPort = value.mIndex; } }
		public IfcElement RelatedElement { get { return mDatabase[mRelatedElement] as IfcElement; } set { mRelatedElement = value.mIndex; } }
		internal IfcRelFlowControlElements() : base() { }
		internal IfcRelFlowControlElements(DatabaseIfc db, IfcRelFlowControlElements r) : base(db,r) { RelatingPort = db.Factory.Duplicate(r.RelatingPort) as IfcPort; RelatedElement = db.Factory.Duplicate(r.RelatedElement) as IfcElement; }
		internal static IfcRelFlowControlElements Parse(string strDef) { IfcRelFlowControlElements i = new IfcRelFlowControlElements(); int ipos = 0; parseFields(i, ParserSTEP.SplitLineFields(strDef), ref ipos); return i; }
		internal static void parseFields(IfcRelFlowControlElements i, List<string> arrFields, ref int ipos) { IfcRelConnects.parseFields(i, arrFields, ref ipos); i.mRelatingPort = ParserSTEP.ParseLink(arrFields[ipos++]); i.mRelatedElement = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mRelatingPort) + "," + ParserSTEP.LinkToString(mRelatedElement); }
	}
	//[Obsolete("DEPRECEATED IFC4", false)]
	//ENTITY IfcRelInteractionRequirements  // DEPRECEATED IFC4
	public partial class IfcRelInterferesElements : IfcRelConnects
	{
		internal int mRelatingElement;// : IfcElement;
		internal int mRelatedElement;// : IfcElement;
		internal int mInterferenceGeometry;// : OPTIONAL IfcConnectionGeometry; 
		internal string mInterferenceType = "$";// : OPTIONAL IfcIdentifier;
		internal IfcLogicalEnum mImpliedOrder = IfcLogicalEnum.UNKNOWN;// : LOGICAL;

		public IfcElement RelatingElement { get { return mDatabase[mRelatingElement] as IfcElement; } set { mRelatingElement = value.mIndex; } }
		public IfcElement RelatedElement { get { return mDatabase[mRelatedElement] as IfcElement; } set { mRelatedElement = value.mIndex; } }
		public IfcConnectionGeometry InterferenceGeometry { get { return mDatabase[mInterferenceGeometry] as IfcConnectionGeometry; } set { mInterferenceGeometry = value == null ? 0 : value.mIndex; } }
		public string InterferenceType { get { return (mInterferenceType == "$" ? "" : ParserIfc.Decode(mInterferenceType)); } set { mInterferenceType = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public IfcLogicalEnum ImpliedOrder { get { return mImpliedOrder; } }

		internal IfcRelInterferesElements() : base() { }
		internal IfcRelInterferesElements(DatabaseIfc db, IfcRelInterferesElements r) : base(db, r)
		{
			RelatingElement = db.Factory.Duplicate(r.RelatingElement) as IfcElement;
			RelatedElement = db.Factory.Duplicate(r.RelatedElement) as IfcElement;
			if (r.mInterferenceGeometry > 0)
				InterferenceGeometry = db.Factory.Duplicate(r.InterferenceGeometry) as IfcConnectionGeometry;
			mInterferenceType = r.mInterferenceType;
			mImpliedOrder = r.mImpliedOrder;
		}
		internal IfcRelInterferesElements(IfcElement relatingElement, IfcElement relatedElement)
			: base(relatingElement.mDatabase) { RelatingElement = relatingElement; RelatedElement = relatedElement; }
		internal static IfcRelInterferesElements Parse(string strDef) { IfcRelInterferesElements i = new IfcRelInterferesElements(); int ipos = 0; parseFields(i, ParserSTEP.SplitLineFields(strDef), ref ipos); return i; }
		internal static void parseFields(IfcRelInterferesElements i, List<string> arrFields, ref int ipos)
		{
			IfcRelConnects.parseFields(i, arrFields, ref ipos);
			i.mRelatingElement = ParserSTEP.ParseLink(arrFields[ipos++]);
			i.mRelatedElement = ParserSTEP.ParseLink(arrFields[ipos++]);
			i.mInterferenceGeometry = ParserSTEP.ParseLink(arrFields[ipos++]);
			i.mInterferenceType = arrFields[ipos++].Replace("'", "");
			i.mImpliedOrder = ParserIfc.ParseIFCLogical(arrFields[ipos++]);
		}
		protected override string BuildStringSTEP()
		{
			return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mRelatingElement) + "," + ParserSTEP.LinkToString(mRelatedElement) + "," + 
				ParserSTEP.LinkToString(mInterferenceGeometry) + (mInterferenceType == "$" ? ",$," : ",'" + mInterferenceType + "',") + ParserIfc.LogicalToString(mImpliedOrder);
		}
		internal override void postParseRelate()
		{
			base.postParseRelate();
			RelatingElement.mInterferesElements.Add(this);
			RelatedElement.mIsInterferedByElements.Add(this);
		}
	}
	public partial class IfcRelNests : IfcRelDecomposes
	{
		internal int mRelatingObject;// : IfcObjectDefinition 
		internal List<int> mRelatedObjects = new List<int>();// : SET [1:?] OF IfcObjectDefinition; 

		public IfcObjectDefinition RelatingObject { get { return mDatabase[mRelatingObject] as IfcObjectDefinition; } set { mRelatingObject = value.mIndex; if (!value.mIsNestedBy.Contains(this)) value.mIsNestedBy.Add(this); } }
		public ReadOnlyCollection<IfcObjectDefinition> RelatedObjects { get { return new ReadOnlyCollection<IfcObjectDefinition>( mRelatedObjects.ConvertAll(x => mDatabase[x] as IfcObjectDefinition)); } }

		internal IfcRelNests() : base() { }
		internal IfcRelNests(DatabaseIfc db, IfcRelNests n) : base(db,n) { RelatingObject = db.Factory.Duplicate(n.RelatingObject) as IfcObjectDefinition; n.RelatedObjects.ToList().ForEach(x => addRelated( db.Factory.Duplicate(x) as IfcObjectDefinition)); }
		public IfcRelNests(IfcObjectDefinition relatingObject) : base(relatingObject.mDatabase)
		{
			mRelatingObject = relatingObject.mIndex;
			relatingObject.mIsNestedBy.Add(this);
		}
		public IfcRelNests(IfcObjectDefinition relatingObject, IfcObjectDefinition relatedObject) : base(relatingObject.mDatabase)
		{
			mRelatingObject = relatingObject.mIndex;
			mRelatedObjects.Add(relatedObject.mIndex);
			relatingObject.mIsNestedBy.Add(this);
			relatedObject.mNests = this;
		}
		public IfcRelNests(IfcObjectDefinition relatingObject, IfcObjectDefinition ro, IfcObjectDefinition ro2) : this(relatingObject, ro) { mRelatedObjects.Add(ro2.mIndex); ro2.mNests = this; ; }
		public IfcRelNests(IfcObjectDefinition relatingObject, List<IfcObjectDefinition> relatedObjects) : base(relatingObject.mDatabase)
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
			ReadOnlyCollection<IfcObjectDefinition> ods = RelatedObjects;
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
		internal void addRelated(IfcObjectDefinition o)
		{
			o.mNests = this;
			if (!mRelatedObjects.Contains(o.mIndex))
				mRelatedObjects.Add(o.mIndex);
		}

		internal override void changeSchema(ReleaseVersion schema)
		{
			base.changeSchema(schema);
			ReadOnlyCollection<IfcObjectDefinition> ods = RelatedObjects;
			for (int icounter = 0; icounter < ods.Count; icounter++)
				ods[icounter].changeSchema(schema);
		}
	}
	//[Obsolete("DEPRECEATED IFC4", false)]
	//ENTITY IfcRelOccupiesSpaces // DEPRECEATED IFC4
	[Obsolete("DEPRECEATED IFC4", false)]
	public partial class IfcRelOverridesProperties : IfcRelDefinesByProperties // DEPRECEATED IFC4
	{
		public override string KeyWord { get { return (mDatabase.mRelease <= ReleaseVersion.IFC2x3 ? base.KeyWord : "IFCRELOVERRIDESPROPERTIES"); } }
		private List<int> mOverridingProperties = new List<int>();// : 	SET [1:?] OF IfcProperty;

		public ReadOnlyCollection<IfcProperty> OverridingProperties { get { return new ReadOnlyCollection<IfcProperty>(mOverridingProperties.ConvertAll(x => mDatabase[x] as IfcProperty)); } }

		internal IfcRelOverridesProperties() : base() { }
		internal IfcRelOverridesProperties(DatabaseIfc db, IfcRelOverridesProperties d) : base(db, d)
		{
			mOverridingProperties = d.OverridingProperties.ToList().ConvertAll(x=>db.Factory.Duplicate(x).mIndex);
		}
		internal IfcRelOverridesProperties(IfcPropertySetDefinition ifcproperty) : base(ifcproperty) { }
		public IfcRelOverridesProperties(IfcObjectDefinition od, IfcPropertySetDefinition ifcproperty) : base(od,ifcproperty) { }
		public IfcRelOverridesProperties(List<IfcObjectDefinition> objs, IfcPropertySetDefinition ifcproperty) : base(objs,ifcproperty) { }

		protected override string BuildStringSTEP()
		{
			string str = base.BuildStringSTEP();
			if (string.IsNullOrEmpty(str))
				return "";
			str += ",(#" + mOverridingProperties[0];
			for (int icounter = 1; icounter < mOverridingProperties.Count; icounter++)
				str += ",#" + mOverridingProperties[icounter];
			return str + ")";
		}
		internal new static IfcRelOverridesProperties Parse(string str)
		{
			IfcRelOverridesProperties d = new IfcRelOverridesProperties();
			int pos = 0, len = str.Length;
			d.Parse(str, ref pos, len);
			return d;
		}
		protected override void Parse(string str, ref int pos, int len)
		{
			base.Parse(str, ref pos, len);
			mOverridingProperties = ParserSTEP.StripListLink(str, ref pos, len);
		}
	}
	public partial class IfcRelProjectsElement : IfcRelDecomposes // IFC2x3 IfcRelDecomposes
	{
		internal int mRelatingElement;// : IfcElement; 
		internal int mRelatedFeatureElement;// : IfcFeatureElementAddition

		public IfcElement RelatingElement { get { return mDatabase[mRelatingElement] as IfcElement; } set { mRelatingElement = value.mIndex; } }
		public IfcFeatureElementAddition RelatedFeatureElement { get { return mDatabase[mRelatedFeatureElement] as IfcFeatureElementAddition; } set { mRelatedFeatureElement = value.mIndex; } }

		protected IfcRelProjectsElement() : base() { }
		protected IfcRelProjectsElement(DatabaseIfc db, IfcRelProjectsElement p) : base(db, p) { RelatingElement = db.Factory.Duplicate(p.RelatingElement) as IfcElement; RelatedFeatureElement = db.Factory.Duplicate(p.RelatedFeatureElement) as IfcFeatureElementAddition; }
		protected IfcRelProjectsElement(IfcElement e, IfcFeatureElementAddition a) : base(e.mDatabase) { mRelatingElement = e.mIndex; mRelatedFeatureElement = a.mIndex; }
		protected override string BuildStringSTEP() { return base.BuildStringSTEP() + "," + ParserSTEP.LinkToString(mRelatingElement) + "," + ParserSTEP.LinkToString(mRelatedFeatureElement); }
		internal static IfcRelProjectsElement Parse(string strDef) { IfcRelProjectsElement i = new IfcRelProjectsElement(); int ipos = 0; parseFields(i, ParserSTEP.SplitLineFields(strDef), ref ipos); return i; }
		protected static void parseFields(IfcRelProjectsElement c, List<string> arrFields, ref int ipos) { IfcRelDecomposes.parseFields(c, arrFields, ref ipos); c.mRelatingElement = ParserSTEP.ParseLink(arrFields[ipos++]); c.mRelatedFeatureElement = ParserSTEP.ParseLink(arrFields[ipos++]); }
		internal override void postParseRelate()
		{
			base.postParseRelate();
			RelatingElement.mHasProjections.Add(this);
			RelatedFeatureElement.mProjectsElements.Add(this);
		}

	}
	public partial class IfcRelReferencedInSpatialStructure : IfcRelConnects
	{
		internal List<int> mRelatedElements = new List<int>();// : SET [1:?] OF IfcProduct;
		private int mRelatingStructure;//  IfcSpatialElement 

		public ReadOnlyCollection<IfcProduct> RelatedElements { get { return new ReadOnlyCollection<IfcProduct>( mRelatedElements.ConvertAll(x => mDatabase[x] as IfcProduct)); } }
		public IfcSpatialElement RelatingStructure { get { return mDatabase[mRelatingStructure] as IfcSpatialElement; } set { mRelatingStructure = value.mIndex; value.mReferencesElements.Add(this); } }

		internal IfcRelReferencedInSpatialStructure() : base() { }
		internal IfcRelReferencedInSpatialStructure(DatabaseIfc db, IfcRelReferencedInSpatialStructure r, bool downstream) : base(db, r)
		{
			if (downstream)
				r.RelatedElements.ToList().ForEach(x => addRelated( db.Factory.Duplicate(x) as IfcProduct));
			RelatingStructure = db.Factory.Duplicate(r.RelatingStructure, false) as IfcSpatialElement;
		}
		internal IfcRelReferencedInSpatialStructure(IfcSpatialElement e) : base(e.mDatabase)
		{
			mRelatingStructure = e.mIndex;
			e.mReferencesElements.Add(this);
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
		internal static IfcRelReferencedInSpatialStructure Parse(string str)
		{
			IfcRelReferencedInSpatialStructure c = new IfcRelReferencedInSpatialStructure();
			int pos = 0, len = str.Length;
			c.Parse(str, ref pos, len);
			c.mRelatedElements = ParserSTEP.StripListLink(str, ref pos, len);
			c.mRelatingStructure = ParserSTEP.StripLink(str, ref pos, len);
			return c;
		}
		internal override void postParseRelate()
		{
			base.postParseRelate();
			IfcSpatialElement se = RelatingStructure;
			if (se != null)
				se.mReferencesElements.Add(this);
			ReadOnlyCollection<IfcProduct> products = RelatedElements;
			for (int icounter = 0; icounter < products.Count; icounter++)
				relate(products[icounter] as IfcProduct);
		}
		internal void addRelated(IfcProduct product) { if (!mRelatedElements.Contains(product.mIndex)) { mRelatedElements.Add(product.mIndex); relate(product); } }
		private void relate(IfcProduct product)
		{
			IfcElement element = product as IfcElement;
			if (element != null)
			{
				if (element.mContainedInStructure != null)
					element.mContainedInStructure.removeObject(element);
				element.mReferencedInStructures.Add(this);
			}
			else
			{
				//IfcGrid grid = product as IfcGrid;
				//if (grid != null)
				//	grid.mContainedInStructure = this;
				//else
				//{
				//	IfcAnnotation annotation = product as IfcAnnotation;
				//	if (annotation != null)
				//		annotation.mContainedInStructure = this;
				//}
			}
		}
		
		internal void removeObject(IfcElement e)
		{
			e.mReferencedInStructures.Remove(this);
			mRelatedElements.Remove(e.mIndex);
		}
	}
	//[Obsolete("DEPRECEATED IFC4", false)]
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
		internal IfcRelSequence(DatabaseIfc db, IfcRelSequence s) : base(db, s)
		{
			RelatingProcess = db.Factory.Duplicate(s.RelatingProcess) as IfcProcess;
			RelatedProcess = db.Factory.Duplicate(s.RelatedProcess) as IfcProcess;
			mTimeLag = s.mTimeLag;
			mSequenceType = s.mSequenceType;
			mUserDefinedSequenceType = s.mUserDefinedSequenceType;
		}
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

		public IfcSystem RelatingSystem { get { return mDatabase[mRelatingSystem] as IfcSystem; } set { mRelatingSystem = value.mIndex; value.ServicesBuildings = this; } }
		public ReadOnlyCollection<IfcSpatialElement> RelatedBuildings { get { return new ReadOnlyCollection<IfcSpatialElement>( mRelatedBuildings.ConvertAll(x => mDatabase[x] as IfcSpatialElement)); } }

		internal IfcRelServicesBuildings() : base() { }
		internal IfcRelServicesBuildings(DatabaseIfc db, IfcRelServicesBuildings s) : base(db,s)
		{
			RelatingSystem = db.Factory.Duplicate(s.RelatingSystem) as IfcSystem;
			s.RelatedBuildings.ToList().ForEach(x => addRelated( db.Factory.Duplicate(x,false) as IfcSpatialElement));
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
		internal void addRelated(IfcSpatialElement building)
		{
			mRelatedBuildings.Add(building.mIndex);
			building.mServicedBySystems.Add(this);
		}
	}
	public partial class IfcRelSpaceBoundary : IfcRelConnects
	{
		internal int mRelatingSpace;// :	IfcSpaceBoundarySelect; : IfcSpace;
		internal int mRelatedBuildingElement;// :OPTIONAL IfcElement;
		internal int mConnectionGeometry;// : OPTIONAL IfcConnectionGeometry;
		internal IfcPhysicalOrVirtualEnum mPhysicalOrVirtualBoundary = IfcPhysicalOrVirtualEnum.NOTDEFINED;// : IfcPhysicalOrVirtualEnum;
		internal IfcInternalOrExternalEnum mInternalOrExternalBoundary = IfcInternalOrExternalEnum.NOTDEFINED;// : IfcInternalOrExternalEnum; 

		public IfcSpaceBoundarySelect RelatingSpace { get { return mDatabase[mRelatingSpace] as IfcSpaceBoundarySelect; } set { mRelatingSpace = value.Index; } }
		public IfcElement RelatedBuildingElement { get { return mDatabase[mRelatedBuildingElement] as IfcElement; } set { mRelatedBuildingElement = value == null ? 0 : value.mIndex; } }
		public IfcConnectionGeometry ConnectionGeometry { get { return mDatabase[mConnectionGeometry] as IfcConnectionGeometry; } set { mConnectionGeometry = (value == null ? 0 : value.mIndex); } }


		internal IfcRelSpaceBoundary() : base() { }
		internal IfcRelSpaceBoundary(DatabaseIfc db, IfcRelSpaceBoundary b) : base(db,b)
		{
			RelatingSpace = db.Factory.Duplicate(b.mDatabase[b.mRelatingSpace]) as IfcSpaceBoundarySelect;
			if(b.mRelatedBuildingElement > 0)
				RelatedBuildingElement = db.Factory.Duplicate(b.RelatedBuildingElement) as IfcElement;
			if(b.mConnectionGeometry > 0 )
				ConnectionGeometry = db.Factory.Duplicate(b.ConnectionGeometry) as IfcConnectionGeometry;
			mPhysicalOrVirtualBoundary = b.mPhysicalOrVirtualBoundary;
			mInternalOrExternalBoundary = b.mInternalOrExternalBoundary;
		}
		internal IfcRelSpaceBoundary(IfcSpaceBoundarySelect s, IfcElement e, IfcConnectionGeometry g, IfcPhysicalOrVirtualEnum virt, IfcInternalOrExternalEnum intern) : base(s.Database)
		{
			mRelatingSpace = s.Index;
			s.AddBoundary(this);
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
			RelatingSpace.AddBoundary(this);
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

		public IfcRelSpaceBoundary1stLevel ParentBoundary { get { return mDatabase[mParentBoundary] as IfcRelSpaceBoundary1stLevel; } set { mParentBoundary = value.mIndex; } }
		internal IfcRelSpaceBoundary1stLevel() : base() { }
		internal IfcRelSpaceBoundary1stLevel(DatabaseIfc db, IfcRelSpaceBoundary1stLevel r) : base(db,r) { ParentBoundary = db.Factory.Duplicate(r.ParentBoundary) as IfcRelSpaceBoundary1stLevel; }
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
		internal IfcRelSpaceBoundary2ndLevel(DatabaseIfc db, IfcRelSpaceBoundary2ndLevel r) : base(db,r) { CorrespondingBoundary = db.Factory.Duplicate(r.CorrespondingBoundary) as IfcRelSpaceBoundary2ndLevel; }
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
