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

using GeometryGym.STEP;

namespace GeometryGym.Ifc
{
	public abstract partial class IfcObject : IfcObjectDefinition //ABSTRACT SUPERTYPE OF (ONEOF (IfcActor ,IfcControl ,IfcGroup ,IfcProcess ,IfcProduct ,IfcProject ,IfcResource))
	{
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			ObjectType = extractString(obj.GetValue("ObjectType", StringComparison.InvariantCultureIgnoreCase));
			foreach (IfcRelDefinesByProperties rdp in mDatabase.extractJArray<IfcRelDefinesByProperties>(obj.GetValue("IsDefinedBy", StringComparison.InvariantCultureIgnoreCase) as JArray))
				rdp.RelatedObjects.Add(this);
			JObject jobj = obj.GetValue("IsTypedBy", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				IsTypedBy = mDatabase.parseJObject<IfcRelDefinesByType>(jobj);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			setAttribute(obj, "ObjectType", ObjectType);
			if (mIsTypedBy != null)
			{
				obj["IsTypedBy"] = mIsTypedBy.getJson(this, options);
			}
			if (mIsDefinedBy.Count > 0 && options.Style != SetJsonOptions.JsonStyle.Repository)
			{
				JArray array = new JArray();
				foreach (IfcRelDefinesByProperties rdp in mIsDefinedBy)
					array.Add(rdp.getJson(this, options));
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
				ra.RelatedObjects.Add(this);
			foreach (IfcRelNests rn in mDatabase.extractJArray<IfcRelNests>(obj.GetValue("IsNestedBy", StringComparison.InvariantCultureIgnoreCase) as JArray))
				rn.RelatingObject = this;
			foreach (IfcRelAggregates ra in mDatabase.extractJArray<IfcRelAggregates>(obj.GetValue("IsDecomposedBy", StringComparison.InvariantCultureIgnoreCase) as JArray))
				ra.RelatingObject = this;
			foreach (IfcRelAssociates ra in mDatabase.extractJArray<IfcRelAssociates>(obj.GetValue("HasAssociations", StringComparison.InvariantCultureIgnoreCase) as JArray))
				ra.RelatedObjects.Add(this);
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
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
					if (host == null || ra.mIndex != host.mIndex)
						array.Add(ra.getJson(this, options));
				}
				if (array.Count > 0)
					obj["HasAssignments"] = array;
			}
			if (options.Style != SetJsonOptions.JsonStyle.Repository)
			{
				if (mIsNestedBy.Count > 0)
				{
					JArray array = new JArray();
					foreach (IfcRelNests rn in IsNestedBy)
						array.Add(rn.getJson(this, options));
					obj["IsNestedBy"] = array;
				}
				if (mIsDecomposedBy.Count > 0)
				{
					JArray array = new JArray();
					foreach (IfcRelAggregates agg in IsDecomposedBy)
						array.Add(agg.getJson(this, options));
					obj["IsDecomposedBy"] = array;
				}
			}
			if (mHasAssociations.Count > 0)
			{
				JArray array = new JArray();
				foreach (IfcRelAssociates agg in HasAssociations)
					array.Add(agg.getJson(this, options));
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
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mBenchmarkValues.Count > 0)
			{
				JArray array = new JArray();
				foreach (IfcConstraint c in BenchmarkValues)
				{
					if (c.mIndex != host.mIndex)
						array.Add(c.getJson(this, options));
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
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mPredefinedType != IfcOpeningElementTypeEnum.NOTDEFINED)
				obj["PredefinedType"] = mPredefinedType.ToString();
			if (mHasFillings.Count > 0)
			{
				JArray array = new JArray();
				foreach (IfcRelFillsElement rf in HasFillings)
				{
					if (rf.mIndex != host.mIndex)
						array.Add(rf.getJson(this, options));
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
			Roles.AddRange(mDatabase.extractJArray<IfcActorRole>(obj.GetValue("Roles", StringComparison.InvariantCultureIgnoreCase) as JArray));
			Addresses.AddRange(mDatabase.extractJArray<IfcAddress>(obj.GetValue("Addresses", StringComparison.InvariantCultureIgnoreCase) as JArray));
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			string identification = Identification;
			if (!string.IsNullOrEmpty(identification))
				obj["Identification"] = identification;
			obj["Name"] = Name;
			string description = Description;
			if (!string.IsNullOrEmpty(description))
				obj["Description"] = description;
			LIST<IfcActorRole> roles = Roles;
			if (roles.Count > 0)
			{
				JArray array = new JArray();
				foreach (IfcActorRole role in roles)
					array.Add(role.getJson(this, options));
				obj["Roles"] = array;
			}
			LIST<IfcAddress> addresses = Addresses;
			if(addresses.Count > 0)
			{
				JArray array = new JArray();
				foreach (IfcAddress address in addresses)
					array.Add(address.getJson(this, options));
				obj["Addresses"] = array;
			}
		}
	}
	public partial class IfcOwnerHistory : BaseClassIfc
	{
		protected override string genRepositoryName => IfcDateTime.FormatSTEP(LastModifiedDate == DateTime.MinValue ? CreationDate : LastModifiedDate).Replace("'","") + " " + mChangeAction.ToString();
		internal override void parseJObject(JObject obj)
		{
			base.parseJObject(obj);
			JObject jobj = obj.GetValue("OwningUser", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				OwningUser = mDatabase.parseJObject<IfcPersonAndOrganization>(jobj);
			jobj = obj.GetValue("OwningApplication", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				OwningApplication = mDatabase.parseJObject<IfcApplication>(jobj);
			JToken token = obj.GetValue("State", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Enum.TryParse<IfcStateEnum>(token.Value<string>(), true, out mState);
			token = obj.GetValue("ChangeAction", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				Enum.TryParse<IfcChangeActionEnum>(token.Value<string>(), true, out mChangeAction);
			token = obj.GetValue("LastModifiedDate", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				mLastModifiedDate = token.Value<int>();
			jobj = obj.GetValue("LastModifyingUser", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				LastModifyingUser = mDatabase.parseJObject<IfcPersonAndOrganization>(jobj);
			jobj = obj.GetValue("LastModifyingApplication", StringComparison.InvariantCultureIgnoreCase) as JObject;
			if (jobj != null)
				LastModifyingApplication = mDatabase.parseJObject<IfcApplication>(jobj);
			token = obj.GetValue("CreationDate", StringComparison.InvariantCultureIgnoreCase);
			if (token != null)
				mCreationDate = token.Value<int>();
		}
		protected override void setJSON(JObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			obj["OwningUser"] = OwningUser.getJson(this, options);
			obj["OwningApplication"] = OwningApplication.getJson(this, options);
			if (mState != IfcStateEnum.NOTDEFINED)
				obj["State"] = mState.ToString();
			obj["ChangeAction"] = mChangeAction.ToString();
			if (mLastModifiedDate > 0)
				obj["LastModifiedDate"] = mLastModifiedDate;
			if (mLastModifyingUser != null)
				obj["LastModifyingUser"] = LastModifyingUser.getJson(this, options);
			if (mLastModifyingApplication != null)
				obj["LastModifyingApplication"] = LastModifyingApplication.getJson(this, options);
			if (mCreationDate > 0)
				obj["CreationDate"] = mCreationDate;
		}
	}
}
