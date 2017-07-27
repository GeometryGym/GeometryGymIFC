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

using Newtonsoft.Json.Linq;


namespace GeometryGym.Ifc
{
	public abstract partial class IfcObject : IfcObjectDefinition //ABSTRACT SUPERTYPE OF (ONEOF (IfcActor ,IfcControl ,IfcGroup ,IfcProcess ,IfcProduct ,IfcProject ,IfcResource))
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			ObjectType = extractString(obj.GetValue("ObjectType", StringComparison.InvariantCultureIgnoreCase));
			foreach (IfcRelDefinesByProperties rdp in mDatabase.extractJArray<IfcRelDefinesByProperties>(obj.GetValue("IsDefinedBy", StringComparison.InvariantCultureIgnoreCase) as JArray))
				rdp.AddRelated(this);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			setAttribute(obj, "ObjectType", ObjectType);
			if (mIsTypedBy != null)
				obj["IsTypedBy"] = mIsTypedBy.getJson(this, processed);
			if (mIsDefinedBy.Count > 0)
			{
				JArray array = new JArray();
				foreach (IfcRelDefinesByProperties rdp in mIsDefinedBy)
					array.Add(rdp.getJson(this, processed));
				obj["IsDefinedBy"] = array;
			}
		}
		//internal string mObjectType = "$"; //: OPTIONAL IfcLabel;
		//								   //INVERSE
		//internal IfcRelDefinesByObject mIsDeclaredBy = null;
		//internal List<IfcRelDefinesByObject> mDeclares = new List<IfcRelDefinesByObject>();
		//private IfcRelDefinesByType mIsTypedBy = null;
		//internal List<IfcRelDefinesByProperties> mIsDefinedBy = new List<IfcRelDefinesByProperties>();
	}
	public abstract partial class IfcObjectDefinition : IfcRoot, IfcDefinitionSelect  //ABSTRACT SUPERTYPE OF (ONEOF ((IfcContext, IfcObject, IfcTypeObject))))
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			foreach (IfcRelAssigns ra in mDatabase.extractJArray<IfcRelAssigns>(obj.GetValue("HasAssignments", StringComparison.InvariantCultureIgnoreCase) as JArray))
				ra.AddRelated(this);
			foreach (IfcRelNests rn in mDatabase.extractJArray<IfcRelNests>(obj.GetValue("IsNestedBy", StringComparison.InvariantCultureIgnoreCase) as JArray))
				rn.RelatingObject = this;
			foreach (IfcRelAggregates ra in mDatabase.extractJArray<IfcRelAggregates>(obj.GetValue("IsDecomposedBy", StringComparison.InvariantCultureIgnoreCase) as JArray))
				ra.RelatingObject = this;
			foreach (IfcRelAssociates ra in mDatabase.extractJArray<IfcRelAssociates>(obj.GetValue("HasAssociations", StringComparison.InvariantCultureIgnoreCase) as JArray))
				ra.addRelated(this);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			//internal List<IfcRelAssigns> mHasAssignments = new List<IfcRelAssigns>();//	 : 	SET OF IfcRelAssigns FOR RelatedObjects;
			//internal IfcRelNests mNests = null;//	 :	SET [0:1] OF IfcRelNests FOR RelatedObjects;
			//internal List<IfcRelNests> mIsNestedBy = new List<IfcRelNests>();//	 :	SET OF IfcRelNests FOR RelatingObject;
			//internal IfcRelDeclares mHasContext = null;// :	SET [0:1] OF IfcRelDeclares FOR RelatedDefinitions; 
			//internal List<IfcRelAggregates> mIsDecomposedBy = new List<IfcRelAggregates>();//	 : 	SET OF IfcRelDecomposes FOR RelatingObject;

			if (mHasAssignments.Count > 0)
			{
				JArray array = new JArray();
				foreach (IfcRelAssigns ra in HasAssignments)
				{
					if (ra.mIndex != host.mIndex)
						array.Add(ra.getJson(this, processed));
				}
				if (array.Count > 0)
					obj["HasAssignments"] = array;
			}
			if (mIsNestedBy.Count > 0)
			{
				JArray array = new JArray();
				foreach (IfcRelNests rn in IsNestedBy)
					array.Add(rn.getJson(this, processed));
				obj["IsNestedBy"] = array;
			}
			if (mIsDecomposedBy.Count > 0)
			{
				JArray array = new JArray();
				foreach (IfcRelAggregates agg in IsDecomposedBy)
					array.Add(agg.getJson(this, processed));
				obj["IsDecomposedBy"] = array;
			}
			if (mHasAssociations.Count > 0)
			{
				JArray array = new JArray();
				foreach (IfcRelAssociates agg in HasAssociations)
					array.Add(agg.getJson(this, processed));
				obj["HasAssociations"] = array;
			}
		}
	}
	public partial class IfcObjective : IfcConstraint
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			mDatabase.extractJArray<IfcConstraint>(obj.GetValue("BenchmarkValues", StringComparison.InvariantCultureIgnoreCase) as JArray).ForEach(x=>AddBenchmark(x));
			JToken token = obj.GetValue("LogicalAggregator", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Enum.TryParse<IfcLogicalOperatorEnum>(token.Value<string>(), true, out mLogicalAggregator);
			token = obj.GetValue("ObjectiveQualifier", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Enum.TryParse<IfcObjectiveEnum>(token.Value<string>(), true, out mObjectiveQualifier);
			UserDefinedQualifier = extractString(obj.GetValue("UserDefinedQualifier", StringComparison.InvariantCultureIgnoreCase));
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			if (mBenchmarkValues.Count > 0)
			{
				JArray array = new JArray();
				foreach (IfcConstraint c in BenchmarkValues)
				{
					if (c.mIndex != host.mIndex)
						array.Add(c.getJson(this, processed));
				}
				if (array.Count > 0)
					obj["BenchmarkValues"] = array;
			}
			if (mLogicalAggregator != IfcLogicalOperatorEnum.NONE)
				obj["LogicalAggregator"] = mLogicalAggregator.ToString();
			if (mObjectiveQualifier != IfcObjectiveEnum.NOTDEFINED)
				obj["ObjectiveQualifier"] = mObjectiveQualifier.ToString();
			setAttribute(obj, "UserDefinedQualifier", UserDefinedQualifier);
		}
	}
	public partial class IfcOpeningElement : IfcFeatureElementSubtraction //SUPERTYPE OF(IfcOpeningStandardCase)
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("PredefinedType", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Enum.TryParse<IfcOpeningElementTypeEnum>(token.Value<string>(), out mPredefinedType);
			foreach (IfcRelFillsElement fills in mDatabase.extractJArray<IfcRelFillsElement>(obj.GetValue("HasFillings", StringComparison.InvariantCultureIgnoreCase) as JArray))
				fills.RelatingOpeningElement = this;
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			if (mPredefinedType != IfcOpeningElementTypeEnum.NOTDEFINED)
				obj["PredefinedType"] = mPredefinedType.ToString();
			if (mHasFillings.Count > 0)
			{
				JArray array = new JArray();
				foreach (IfcRelFillsElement rf in HasFillings)
				{
					if (rf.mIndex != host.mIndex)
						array.Add(rf.getJson(this, processed));
				}
				if (array.Count > 0)
					obj["HasFillings"] = array;
			}
		}
	}
	public partial class IfcOrganization : BaseClassIfc, IfcActorSelect, IfcResourceObjectSelect
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("Identification", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Identification = token.Value<string>();
			token = obj.GetValue("Name", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Name = token.Value<string>();
			token = obj.GetValue("Description", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Description = token.Value<string>();
			mDatabase.extractJArray<IfcActorRole>(obj.GetValue("Roles", StringComparison.InvariantCultureIgnoreCase) as JArray).ForEach(x=>AddRole(x));
			mDatabase.extractJArray<IfcAddress>(obj.GetValue("Addresses", StringComparison.InvariantCultureIgnoreCase) as JArray).ForEach(x=>AddAddress(x));
		}
		protected override void setJSON(JObject obj, BaseClassIfc host,  HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			string identification = Identification;
			if (!string.IsNullOrEmpty(identification))
				obj["Identification"] = identification;
			obj["Name"] = Name;
			string description = Description;
			if (!string.IsNullOrEmpty(description))
				obj["Description"] = description;
			ReadOnlyCollection<IfcActorRole> roles = Roles;
			if (roles.Count > 0)
			{
				JArray array = new JArray();
				foreach (IfcActorRole role in roles)
					array.Add(role.getJson(this, processed));
				obj["Roles"] = array;
			}
			ReadOnlyCollection<IfcAddress> addresses = Addresses;
			if(addresses.Count > 0)
			{
				JArray array = new JArray();
				foreach (IfcAddress address in addresses)
					array.Add(address.getJson(this, processed));
				obj["Addresses"] = array;
			}
		}
	}
	public partial class IfcOwnerHistory : BaseClassIfc
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JToken token = obj.GetValue("OwningUser", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
			{
				JObject jobj = token as JObject;
				if (jobj != null)
					OwningUser = mDatabase.parseJObject<IfcPersonAndOrganization>(jobj);
			}
			token = obj.GetValue("OwningApplication", StringComparison.InvariantCultureIgnoreCase);
			if(token != null)
			{
				JObject jobj = token as JObject;
				if (jobj != null)
					OwningApplication = mDatabase.parseJObject<IfcApplication>(jobj);
			}
		}
		protected override void setJSON(JObject obj, BaseClassIfc host,  HashSet<int> processed)
		{
			base.setJSON(obj, host, processed);
			obj["OwningUser"] = OwningUser.getJson(this, processed);
			obj["OwningApplication"] = OwningApplication.getJson(this, processed);
			if (mState != IfcStateEnum.NA)
				obj["State"] = mState.ToString();
			obj["ChangeAction"] = mChangeAction.ToString();
			if (mLastModifiedDate > 0)
				obj["LastModifiedDate"] = mLastModifiedDate;
			if (mLastModifyingUser > 0)
				obj["LastModifyingUser"] = LastModifyingUser.getJson(this, processed);
			if (mLastModifyingApplication > 0)
				obj["LastModifyingApplication"] = LastModifyingApplication.getJson(this, processed);
			if (mCreationDate > 0)
				obj["CreationDate"] = mCreationDate;
		}
	}
}
