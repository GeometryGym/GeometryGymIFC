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

#if (NET || !NOIFCJSON)
#if (NEWTONSOFT)
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using JsonObject = Newtonsoft.Json.Linq.JObject;
using JsonArray = Newtonsoft.Json.Linq.JArray;
#else
using System.Text.Json.Nodes;
#endif

namespace GeometryGym.Ifc
{
	public abstract partial class IfcObject 
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			ObjectType = extractString(obj["ObjectType"]);
			
			JsonObject jobj = obj["IsTypedBy"] as JsonObject;
			if (jobj != null)
				IsTypedBy = mDatabase.ParseJsonObject<IfcRelDefinesByType>(jobj);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			setAttribute(obj, "ObjectType", ObjectType);
			if (mIsTypedBy != null)
				obj["IsTypedBy"] = mIsTypedBy.getJson(this, options);
		}
		//internal string mObjectType = "$"; //: OPTIONAL IfcLabel;
		//								   //INVERSE
		//internal IfcRelDefinesByObject mIsDeclaredBy = null;
		//internal List<IfcRelDefinesByObject> mDeclares = new List<IfcRelDefinesByObject>();
		//private IfcRelDefinesByType mIsTypedBy = null;
		//internal List<IfcRelDefinesByProperties> mIsDefinedBy = new List<IfcRelDefinesByProperties>();
	}
	public abstract partial class IfcObjectDefinition 
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			foreach (IfcRelAssigns ra in mDatabase.extractJsonArray<IfcRelAssigns>(obj["HasAssignments"] as JsonArray))
				ra.RelatedObjects.Add(this);
			foreach (IfcRelNests rn in mDatabase.extractJsonArray<IfcRelNests>(obj["IsNestedBy"] as JsonArray))
				rn.RelatingObject = this;
			foreach (IfcRelAggregates ra in mDatabase.extractJsonArray<IfcRelAggregates>(obj["IsDecomposedBy"] as JsonArray))
				ra.RelatingObject = this;
			foreach (IfcRelAssociates ra in mDatabase.extractJsonArray<IfcRelAssociates>(obj["HasAssociations"] as JsonArray))
				ra.RelatedObjects.Add(this);

			foreach (IfcRelDefinesByProperties rdp in mDatabase.extractJsonArray<IfcRelDefinesByProperties>(obj["IsDefinedBy"] as JsonArray))
				rdp.RelatedObjects.Add(this);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			//internal List<IfcRelAssigns> mHasAssignments = new List<IfcRelAssigns>();//	 : 	SET OF IfcRelAssigns FOR RelatedObjects;
			//internal IfcRelNests mNests = null;//	 :	SET [0:1] OF IfcRelNests FOR RelatedObjects;
			//internal List<IfcRelNests> mIsNestedBy = new List<IfcRelNests>();//	 :	SET OF IfcRelNests FOR RelatingObject;
			//internal IfcRelDeclares mHasContext = null;// :	SET [0:1] OF IfcRelDeclares FOR RelatedDefinitions; 
			//internal List<IfcRelAggregates> mIsDecomposedBy = new List<IfcRelAggregates>();//	 : 	SET OF IfcRelDecomposes FOR RelatingObject;

			if (mHasAssignments.Count > 0)
			{
				JsonArray array = new JsonArray();
				foreach (IfcRelAssigns ra in HasAssignments)
				{
					if (host == null || ra.StepId != host.StepId)
						array.Add(ra.getJson(this, options));
				}
				if (array.Count > 0)
					obj["HasAssignments"] = array;
			}
			if (options.Style != SetJsonOptions.JsonStyle.Repository)
			{
				if (mIsNestedBy.Count > 0)
				{
					JsonArray array = new JsonArray();
					foreach (IfcRelNests rn in IsNestedBy)
						array.Add(rn.getJson(this, options));
					obj["IsNestedBy"] = array;
				}
				if (mIsDecomposedBy.Count > 0)
				{
					JsonArray array = new JsonArray();
					foreach (IfcRelAggregates agg in IsDecomposedBy)
						array.Add(agg.getJson(this, options));
					obj["IsDecomposedBy"] = array;
				}
			}
			if (mHasAssociations.Count > 0)
			{
				JsonArray array = new JsonArray();
				foreach (IfcRelAssociates agg in HasAssociations)
					array.Add(agg.getJson(this, options));
				obj["HasAssociations"] = array;
			}

			if (mIsDefinedBy.Count > 0 && options.Style != SetJsonOptions.JsonStyle.Repository)
			{
				JsonArray array = new JsonArray();
				foreach (IfcRelDefinesByProperties rdp in mIsDefinedBy)
					array.Add(rdp.getJson(this, options));
				obj["IsDefinedBy"] = array;
			}
		}
	}
	public partial class IfcObjective
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			mBenchmarkValues.AddRange( mDatabase.extractJsonArray<IfcConstraint>(obj["BenchmarkValues"] as JsonArray));
			var node = obj["LogicalAggregator"];
			if (node != null)
				Enum.TryParse<IfcLogicalOperatorEnum>(node.GetValue<string>(), true, out mLogicalAggregator);
			node = obj["ObjectiveQualifier"];
			if (node != null)
				Enum.TryParse<IfcObjectiveEnum>(node.GetValue<string>(), true, out mObjectiveQualifier);
			UserDefinedQualifier = extractString(obj["UserDefinedQualifier"]);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mBenchmarkValues.Count > 0)
			{
				JsonArray array = new JsonArray();
				foreach (IfcConstraint c in BenchmarkValues)
				{
					if (c.StepId != host.StepId)
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
	public abstract partial class IfcObjectPlacement
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			PlacementRelTo = mDatabase.ParseJsonObject<IfcObjectPlacement>(obj["PlacementRelTo"] as JsonObject);
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mPlacementRelTo != null)
				obj["PlacementRelTo"] = PlacementRelTo.getJson(this, options);
		}
	}
	public partial class IfcOpeningElement
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["PredefinedType"];
			if (node != null)
				Enum.TryParse<IfcOpeningElementTypeEnum>(node.GetValue<string>(), out mPredefinedType);
			foreach (IfcRelFillsElement fills in mDatabase.extractJsonArray<IfcRelFillsElement>(obj["HasFillings"] as JsonArray))
				fills.RelatingOpeningElement = this;
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if (mPredefinedType != IfcOpeningElementTypeEnum.NOTDEFINED)
				obj["PredefinedType"] = mPredefinedType.ToString();
			if (mHasFillings.Count > 0)
			{
				JsonArray array = new JsonArray();
				foreach (IfcRelFillsElement rf in HasFillings)
				{
					if (rf.StepId != host.StepId)
						array.Add(rf.getJson(this, options));
				}
				if (array.Count > 0)
					obj["HasFillings"] = array;
			}
		}
	}
	public partial class IfcOrganization
	{
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			var node = obj["Identification"];
			if (node != null)
				Identification = node.GetValue<string>();
			node = obj["Name"];
			if (node != null)
				Name = node.GetValue<string>();
			node = obj["Description"];
			if (node != null)
				Description = node.GetValue<string>();
			Roles.AddRange(mDatabase.extractJsonArray<IfcActorRole>(obj["Roles"] as JsonArray));
			Addresses.AddRange(mDatabase.extractJsonArray<IfcAddress>(obj["Addresses"] as JsonArray));
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
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
				JsonArray array = new JsonArray();
				foreach (IfcActorRole role in roles)
					array.Add(role.getJson(this, options));
				obj["Roles"] = array;
			}
			LIST<IfcAddress> addresses = Addresses;
			if(addresses.Count > 0)
			{
				JsonArray array = new JsonArray();
				foreach (IfcAddress address in addresses)
					array.Add(address.getJson(this, options));
				obj["Addresses"] = array;
			}
		}
	}
	public partial class IfcOwnerHistory
	{
		protected override string genRepositoryName { get { return IfcDateTime.FormatSTEP(LastModifiedDate == DateTime.MinValue ? CreationDate : LastModifiedDate).Replace("'", "") + " " + mChangeAction.ToString(); } }
		internal override void parseJsonObject(JsonObject obj)
		{
			base.parseJsonObject(obj);
			JsonObject jobj = obj["OwningUser"] as JsonObject;
			if (jobj != null)
				OwningUser = mDatabase.ParseJsonObject<IfcPersonAndOrganization>(jobj);
			jobj = obj["OwningApplication"] as JsonObject;
			if (jobj != null)
				OwningApplication = mDatabase.ParseJsonObject<IfcApplication>(jobj);
			var node = obj["State"];
			if (node != null)
				Enum.TryParse<IfcStateEnum>(node.GetValue<string>(), true, out mState);
			node = obj["ChangeAction"];
			if (node != null)
				Enum.TryParse<IfcChangeActionEnum>(node.GetValue<string>(), true, out mChangeAction);
			node = obj["LastModifiedDate"];
			if (node != null)
				mLastModifiedDate = node.GetValue<int>();
			jobj = obj["LastModifyingUser"] as JsonObject;
			if (jobj != null)
				LastModifyingUser = mDatabase.ParseJsonObject<IfcPersonAndOrganization>(jobj);
			jobj = obj["LastModifyingApplication"] as JsonObject;
			if (jobj != null)
				LastModifyingApplication = mDatabase.ParseJsonObject<IfcApplication>(jobj);
			node = obj["CreationDate"];
			if (node != null)
				mCreationDate = node.GetValue<int>();
		}
		protected override void setJSON(JsonObject obj, BaseClassIfc host, SetJsonOptions options)
		{
			base.setJSON(obj, host, options);
			if(OwningUser != null)
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
#endif
