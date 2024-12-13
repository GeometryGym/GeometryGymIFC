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
using System.Collections.Specialized;
using System.Reflection;
using System.Linq;
using GeometryGym.STEP;

namespace GeometryGym.Ifc
{
	[Serializable, Obsolete("DELETED IFC4", false)]
	public partial class IfcRadiusDimension : IfcDimensionCurveDirectedCallout
	{
		internal IfcRadiusDimension() : base() { }
		public IfcRadiusDimension(IfcDraughtingCalloutElement content) : base(content) { }
		public IfcRadiusDimension(IEnumerable<IfcDraughtingCalloutElement> contents) : base(contents) { }
	}
	[Serializable, VersionAdded(ReleaseVersion.IFC4X3)]
	public partial class IfcRail : IfcBuiltElement
	{
		private IfcRailTypeEnum mPredefinedType = IfcRailTypeEnum.NOTDEFINED; //: OPTIONAL IfcRailTypeEnum;
		public IfcRailTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcRailTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		public IfcRail() : base() { }
		public IfcRail(DatabaseIfc db) : base(db) { }
		public IfcRail(DatabaseIfc db, IfcRail rail, DuplicateOptions options) : base(db, rail, options) { PredefinedType = rail.PredefinedType; }
		public IfcRail(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	[Serializable, VersionAdded(ReleaseVersion.IFC4X3)]
	public partial class IfcRailType : IfcBuiltElementType
	{
		private IfcRailTypeEnum mPredefinedType = IfcRailTypeEnum.NOTDEFINED; //: IfcRailTypeEnum;
		public IfcRailTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcRailTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		public IfcRailType() : base() { }
		public IfcRailType(DatabaseIfc db, IfcRailType railType, DuplicateOptions options) : base(db, railType, options) { PredefinedType = railType.PredefinedType; }
		public IfcRailType(DatabaseIfc db, string name, IfcRailTypeEnum predefinedType)
			: base(db, name) { PredefinedType = predefinedType; }
	}
	[Serializable]
	public partial class IfcRailing : IfcBuiltElement
	{
		private IfcRailingTypeEnum mPredefinedType = IfcRailingTypeEnum.NOTDEFINED;// : OPTIONAL IfcRailingTypeEnum
		public IfcRailingTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcRailingTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcRailing() : base() { }
		internal IfcRailing(DatabaseIfc db, IfcRailing r, DuplicateOptions options) : base(db, r, options) { PredefinedType = r.PredefinedType; }
		public IfcRailing(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	[Serializable]
	public partial class IfcRailingType : IfcBuiltElementType
	{
		private IfcRailingTypeEnum mPredefinedType = IfcRailingTypeEnum.NOTDEFINED;
		public IfcRailingTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcRailingTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcRailingType() : base() { }
		internal IfcRailingType(DatabaseIfc db, IfcRailingType t, DuplicateOptions options) : base(db, t, options) { PredefinedType = t.PredefinedType; }
		public IfcRailingType(DatabaseIfc db, string name, IfcRailingTypeEnum type) : base(db) { Name = name; PredefinedType = type; }
	}
	[Serializable, VersionAdded(ReleaseVersion.IFC4X3)]
	public partial class IfcRailway : IfcFacility
	{
		private IfcRailwayTypeEnum mPredefinedType = IfcRailwayTypeEnum.NOTDEFINED;// OPTIONAL : IfcRailwayTypeEnum
		public IfcRailwayTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcRailwayTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		public IfcRailway() : base() { }
		internal IfcRailway(DatabaseIfc db) : base(db) { }
		public IfcRailway(DatabaseIfc db, string name) : base(db, name) { }
		public IfcRailway(DatabaseIfc db, IfcRailway railway, DuplicateOptions options) : base(db, railway, options) { }
		public IfcRailway(IfcFacility host, string name, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { Name = name; }
		internal IfcRailway(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	[Serializable, VersionAdded(ReleaseVersion.IFC4X3)]
	public partial class IfcRailwayPart : IfcFacilityPart
	{
		private IfcRailwayPartTypeEnum mPredefinedType = IfcRailwayPartTypeEnum.NOTDEFINED; //: OPTIONAL IfcRailwayPartTypeEnum;
		public IfcRailwayPartTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcRailwayPartTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }
		public override string StepClassName { get { if (mDatabase != null && mDatabase.Release > ReleaseVersion.IFC4X2 && mDatabase.Release < ReleaseVersion.IFC4X3) return "IfcFacilityPart"; return base.StepClassName; } }
		public IfcRailwayPart() : base() { }
		public IfcRailwayPart(DatabaseIfc db) : base(db) { }
		public IfcRailwayPart(DatabaseIfc db, IfcRailwayPart railwayPart, DuplicateOptions options) : base(db, railwayPart, options) { }
		public IfcRailwayPart(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	[Serializable]
	public partial class IfcRamp : IfcBuiltElement
	{
		private IfcRampTypeEnum mPredefinedType = IfcRampTypeEnum.NOTDEFINED;// OPTIONAL : IfcRampTypeEnum
		public IfcRampTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcRampTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcRamp() : base() { }
		internal IfcRamp(DatabaseIfc db, IfcRamp r, DuplicateOptions options) : base(db, r, options) { PredefinedType = r.PredefinedType; }
		public IfcRamp(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	[Serializable]
	public partial class IfcRampFlight : IfcBuiltElement
	{
		private IfcRampFlightTypeEnum mPredefinedType = IfcRampFlightTypeEnum.NOTDEFINED;// OPTIONAL : IfcRampTypeEnum
		public IfcRampFlightTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcRampFlightTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcRampFlight() : base() { }
		internal IfcRampFlight(DatabaseIfc db, IfcRampFlight f, DuplicateOptions options) : base(db, f, options) { PredefinedType = f.PredefinedType; }
		public IfcRampFlight(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	[Serializable]
	public partial class IfcRampFlightType : IfcBuiltElementType
	{
		private IfcRampFlightTypeEnum mPredefinedType = IfcRampFlightTypeEnum.NOTDEFINED;
		public IfcRampFlightTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcRampFlightTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcRampFlightType() : base() { }
		internal IfcRampFlightType(DatabaseIfc db, IfcRampFlightType t, DuplicateOptions options) : base(db, t, options) { PredefinedType = t.PredefinedType; }
		public IfcRampFlightType(DatabaseIfc db, string name, IfcRampFlightTypeEnum type) : base(db) { Name = name; PredefinedType = type; }
	}
	[Serializable]
	public partial class IfcRampType : IfcBuiltElementType //IFC4
	{
		private IfcRampTypeEnum mPredefinedType = IfcRampTypeEnum.NOTDEFINED;
		public IfcRampTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcRampTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcRampType() : base() { }
		internal IfcRampType(DatabaseIfc db, IfcRampType t, DuplicateOptions options) : base(db, t, options) { PredefinedType = t.PredefinedType; }
		public IfcRampType(DatabaseIfc db, string name, IfcRampTypeEnum type) : base(db) { Name = name; PredefinedType = type; }
	}
	[Obsolete("DEPRECATED IFC4", false)]
	[Serializable]
	public partial class IfcRationalBezierCurve : IfcBezierCurve // DEPRECATED IFC4
	{
		internal List<double> mWeightsData = new List<double>();// : LIST [2:?] OF REAL;	
		internal IfcRationalBezierCurve() : base() { }
		internal IfcRationalBezierCurve(DatabaseIfc db, IfcRationalBezierCurve c, DuplicateOptions options) : base(db, c, options) { mWeightsData.AddRange(c.mWeightsData); }
	}
	[Serializable]
	public partial class IfcRationalBSplineCurveWithKnots : IfcBSplineCurveWithKnots
	{
		internal List<double> mWeightsData = new List<double>();// : LIST [2:?] OF REAL;	
		public List<double> WeightsData { get { return mWeightsData; } }
		internal IfcRationalBSplineCurveWithKnots() : base() { }
		internal IfcRationalBSplineCurveWithKnots(DatabaseIfc db, IfcRationalBSplineCurveWithKnots c, DuplicateOptions options) : base(db, c, options) { mWeightsData.AddRange(c.mWeightsData); }

		public IfcRationalBSplineCurveWithKnots(int degree, IEnumerable<IfcCartesianPoint> controlPoints, IEnumerable<int> multiplicities, IEnumerable<double> knots, IfcKnotType knotSpec, IEnumerable<double> weights) :
			base(degree, controlPoints, multiplicities, knots, knotSpec)
		{
			mWeightsData.AddRange(weights);
		}
	}
	[Serializable]
	public partial class IfcRationalBSplineSurfaceWithKnots : IfcBSplineSurfaceWithKnots
	{
		internal List<List<double>> mWeightsData = new List<List<double>>();// : LIST [2:?] OF LIST [2:?] OF IfcReal;
		public List<List<double>> WeightsData { get { return mWeightsData; } }
		internal IfcRationalBSplineSurfaceWithKnots() : base() { }
		internal IfcRationalBSplineSurfaceWithKnots(DatabaseIfc db, IfcRationalBSplineSurfaceWithKnots s, DuplicateOptions options) : base(db, s, options)
		{
			foreach(var list in s.WeightsData)
				mWeightsData.Add(new List<double>(list));
		}
		public IfcRationalBSplineSurfaceWithKnots(int uDegree, int vDegree, IEnumerable<IEnumerable<IfcCartesianPoint>> controlPoints, IEnumerable<int> uMultiplicities, IEnumerable<int> vMultiplicities, IEnumerable<double> uKnots, IEnumerable<double> vKnots, IfcKnotType type, List<List<double>> weights)
			: base(uDegree, vDegree, controlPoints, uMultiplicities, vMultiplicities, uKnots, vKnots, type)
		{
			foreach(List<double> list in weights)
				mWeightsData.Add(list);
		}
	}
	[Serializable, VersionAdded(ReleaseVersion.IFC4X4_DRAFT)]
	public partial class IfcRealVoxelData : IfcVoxelData
	{
		internal double[] mValues = new double[0];// :	ARRAY [1:?] OF IfcReal;
		internal IfcUnit mUnit = null;// :	OPTIONAL IfcUnit;

		public double[] Values { get { return mValues; } set { mValues = value; } }
		public IfcUnit Unit { get { return mUnit; } set { mUnit = value; } }

		internal IfcRealVoxelData() : base() { }
		internal IfcRealVoxelData(DatabaseIfc db, IfcRealVoxelData d, DuplicateOptions options) : base(db, d, options) { mValues = d.mValues; if (d.Unit != null) Unit = db.Factory.Duplicate(d.Unit); }
		public IfcRealVoxelData(IfcProduct host, IfcObjectPlacement placement, IfcProductDefinitionShape representation, double[] values)
			: base(host, placement, representation) { Values = values; }
	}
	[Serializable]
	public partial class IfcRectangleHollowProfileDef : IfcRectangleProfileDef
	{
		internal double mWallThickness;// : IfcPositiveLengthMeasure;
		internal double mInnerFilletRadius = double.NaN, mOuterFilletRadius = double.NaN;// : OPTIONAL IfcPositiveLengthMeasure;
		public double WallThickness { get { return mWallThickness; } set { mWallThickness = value; } }
		public double InnerFilletRadius { get { return mInnerFilletRadius; } set { mInnerFilletRadius = value; } }
		public double OuterFilletRadius { get { return mOuterFilletRadius; } set { mOuterFilletRadius = value; } }

		internal IfcRectangleHollowProfileDef() : base() { }
		internal IfcRectangleHollowProfileDef(DatabaseIfc db, IfcRectangleHollowProfileDef p, DuplicateOptions options) : base(db, p, options) { mWallThickness = p.mWallThickness; mInnerFilletRadius = p.mInnerFilletRadius; mOuterFilletRadius = p.mOuterFilletRadius; }
		public IfcRectangleHollowProfileDef(DatabaseIfc db, string name, double xDim, double yDim, double wallThickness)
			: base(db, name, xDim, yDim) { mWallThickness = wallThickness; }
	}
	[Serializable]
	public partial class IfcRectangleProfileDef : IfcParameterizedProfileDef //	SUPERTYPE OF(ONEOF(IfcRectangleHollowProfileDef, IfcRoundedRectangleProfileDef))
	{
		internal double mXDim, mYDim;// : IfcPositiveLengthMeasure; 
		public double XDim { get { return mXDim; } set { mXDim = value; } }
		public double YDim { get { return mYDim; } set { mYDim = value; } }

		internal IfcRectangleProfileDef() : base() { }
		internal IfcRectangleProfileDef(DatabaseIfc db, IfcRectangleProfileDef p, DuplicateOptions options) : base(db, p, options) { mXDim = p.mXDim; mYDim = p.mYDim; }
		public IfcRectangleProfileDef(DatabaseIfc db, string name, double xDim, double yDim) : base(db, name) { mXDim = xDim; mYDim = yDim; }
		internal override double ProfileDepth { get { return YDim; } }
		internal override double ProfileWidth { get { return XDim; } }
	}
	[Serializable]
	public partial class IfcRectangularPyramid : IfcCsgPrimitive3D
	{
		internal double mXLength, mYLength, mHeight;// : IfcPositiveLengthMeasure;
		public double XLength { get { return mXLength; } set { mXLength = value; } }
		public double YLength { get { return mYLength; } set { mYLength = value; } }
		public double Height { get { return mHeight; } set { mHeight = value; } }

		internal IfcRectangularPyramid() : base() { }
		internal IfcRectangularPyramid(DatabaseIfc db, IfcRectangularPyramid p, DuplicateOptions options) : base(db, p, options) { mXLength = p.mXLength; mYLength = p.mYLength; mHeight = p.mHeight; }
	}
	[Serializable]
	public partial class IfcRectangularTrimmedSurface : IfcBoundedSurface
	{
		internal IfcPlane mBasisSurface;// : IfcPlane;
		internal double mU1, mV1, mU2, mV2;// : IfcParameterValue;
		internal bool mUsense, mVsense;// : BOOLEAN; 

		public IfcPlane BasisSurface { get { return mBasisSurface; } set { mBasisSurface = value; } }

		internal IfcRectangularTrimmedSurface() : base() { }
		internal IfcRectangularTrimmedSurface(DatabaseIfc db, IfcRectangularTrimmedSurface s, DuplicateOptions options) : base(db, s, options)
		{
			BasisSurface = db.Factory.Duplicate(s.BasisSurface) as IfcPlane;
			mU1 = s.mU1;
			mU2 = s.mU2;
			mV1 = s.mV1;
			mV2 = s.mV2;
			mUsense = s.mUsense;
			mVsense = s.mVsense;
		}
	}
	[Serializable]
	public partial class IfcRecurrencePattern : BaseClassIfc // IFC4
	{
		internal IfcRecurrenceTypeEnum mRecurrenceType = IfcRecurrenceTypeEnum.WEEKLY; //:	IfcRecurrenceTypeEnum;
		internal List<int> mDayComponent = new List<int>();//	 :	OPTIONAL SET [1:?] OF IfcDayInMonthNumber;
		internal List<int> mWeekdayComponent = new List<int>();//	 :	OPTIONAL SET [1:?] OF IfcDayInWeekNumber;
		internal List<int> mMonthComponent = new List<int>();//	 :	OPTIONAL SET [1:?] OF IfcMonthInYearNumber;
		internal int mPosition = int.MinValue, mInterval = int.MinValue, mOccurrences = int.MinValue;//	 :	OPTIONAL IfcInteger;
		internal LIST<IfcTimePeriod> mTimePeriods = new LIST<IfcTimePeriod>();//	 :	OPTIONAL LIST [1:?] OF IfcTimePeriod;
		internal IfcRecurrencePattern() : base() { }
		internal IfcRecurrencePattern(DatabaseIfc db, IfcRecurrencePattern p) : base(db)
		{
			mRecurrenceType = p.mRecurrenceType;
			mDayComponent.AddRange(p.mDayComponent);
			mWeekdayComponent.AddRange(p.mWeekdayComponent);
			mMonthComponent.AddRange(p.mMonthComponent);
			mPosition = p.mPosition;
			mInterval = p.mInterval;
			mOccurrences = p.mOccurrences;
			mTimePeriods.AddRange(p.mTimePeriods.Select(x=>db.Factory.Duplicate(x)));
		}
		internal IfcRecurrencePattern(DatabaseIfc db, IfcRecurrenceTypeEnum type)
			: base(db) { mRecurrenceType = type; }
	}
	[Serializable]
	public partial class IfcReference : BaseClassIfc, IfcMetricValueSelect, IfcAppliedValueSelect // IFC4
	{
		internal string mTypeIdentifier = "", mAttributeIdentifier = ""; //: OPTIONAL IfcIdentifier;
		internal string mInstanceName = ""; //: OPTIONAL IfcLabel;
		internal LIST<int> mListPositions = new LIST<int>();//:	OPTIONAL LIST [1:?] OF INTEGER;
		private IfcReference mInnerReference = null;//:	OPTIONAL IfcReference;

		public string TypeIdentifier { get { return mTypeIdentifier; } set { mTypeIdentifier = value; } }
		public string AttributeIdentifier { get { return mAttributeIdentifier; } set { mAttributeIdentifier = value; } }
		public string InstanceName { get { return mInstanceName; } set { mInstanceName = value; } }
		public LIST<int> ListPositions { get { return mListPositions; } }
		public IfcReference InnerReference { get { return mInnerReference; } set { mInnerReference = value; } }

		internal IfcReference() : base() { }
		internal IfcReference(DatabaseIfc db, IfcReference r) : base(db, r)
		{
			mTypeIdentifier = r.mTypeIdentifier;
			mAttributeIdentifier = r.mAttributeIdentifier;
			mInstanceName = r.mInstanceName;
			mListPositions.AddRange(r.mListPositions);
			InnerReference = db.Factory.Duplicate(r.InnerReference) as IfcReference;
		}
		public IfcReference(DatabaseIfc db) : base(db) { }
		public IfcReference(DatabaseIfc db, string typeId, string attributeId, string instanceName) : base(db)
		{
			TypeIdentifier = typeId;
			AttributeIdentifier = attributeId;
			InstanceName = instanceName;
		}
		public IfcReference(string typeId, string attributeId, string instanceName, IfcReference inner)
			: this(inner.mDatabase, typeId, attributeId, instanceName) { InnerReference = inner; }
		public IfcReference(string typeId, string attributeId, string instanceName, int position, IfcReference inner)
			: this(typeId, attributeId, instanceName, inner) { mListPositions.Add(position); }
		public IfcReference(string typeId, string attributeId, string instanceName, List<int> positions, IfcReference inner)
			: this(typeId, attributeId, instanceName, inner) { mListPositions.AddRange(positions); }

		private static string stripReference(string referenceDescription)
		{
			string reference = "";
			int length = referenceDescription.Length;
			for (int icounter = 0; icounter < length; icounter++)
			{
				char c = referenceDescription[icounter];
				if (c == '.')
					return reference;
				reference += c;
				if (c == '\'')
				{
					for (++icounter; icounter < length; icounter++)
					{
						c = referenceDescription[icounter];
						reference += c;
						if (c == '\'')
						{
							if (icounter + 1 == length)
								break;
							if (referenceDescription[icounter + 1] == '\'')
							{
								reference += '\'';
								icounter++;
							}
							else
								break;
						}
					}
				}
			}
			return reference;
		}
		public static IfcReference ParseDescription(DatabaseIfc db, string referenceDescription)
		{
			// Example description 
			//RepresentationMaps.MappedRepresentation['Body'].Items[*].StyledByItem.Styles\IfcSurfaceStyle.Styles\IfcSurfaceStyleWithTextures.Textures\IfcImageTexture.UrlReference
			string reference = stripReference(referenceDescription);
			IfcReference innerReference = null;
			if (reference.Length + 1 < referenceDescription.Length)
				innerReference = ParseDescription(db, referenceDescription.Substring(reference.Length + 1));
			int i = reference.IndexOf('\\');
			string attributeId = "", instanceName = "";
			if (i > 0)
			{
				if (innerReference != null)
					innerReference.TypeIdentifier = reference.Substring(i + 1, reference.Length - i - 1);
				reference = reference.Substring(0, i);
			}
			List<int> positions = new List<int>();
			if (reference[reference.Length - 1] == ']')
			{
				i = reference.IndexOf('[');
				attributeId = reference.Substring(0, i);
				reference = reference.Substring(i + 1, reference.Length - i - 2);
				if (reference != "*")
				{
					if (!reference.Contains('\'') && reference.Contains(' '))
					{
						string[] fields = reference.Split(" ".ToCharArray());
						foreach (string s in fields)
						{
							if (int.TryParse(s, out i))
								positions.Add(i);
						}
					}
					else
					{
						if (int.TryParse(reference, out i))
							positions.Add(i);
						else
							instanceName = reference.Replace("'", "");
					}
				}
			}
			else
				attributeId = reference;
			IfcReference result = new IfcReference(db);
			result.AttributeIdentifier = attributeId;
			result.InstanceName = instanceName;
			result.mListPositions.AddRange(positions);
			result.InnerReference = innerReference;
			return result;
		}
	}
	//[Obsolete("DEPRECATED IFC4", false)]
	//ENTITY IfcReferencesValueDocument; // DEPRECATED IFC4
	[Serializable, VersionAdded(ReleaseVersion.IFC4X1)]
	public partial class IfcReferent : IfcPositioningElement
	{
		private IfcReferentTypeEnum mPredefinedType = IfcReferentTypeEnum.NOTDEFINED; //: OPTIONAL IfcReferentTypeEnum;
		[Obsolete("DEPRECATED IFC4X3", false)]
		private double mRestartDistance = double.NaN; //: OPTIONAL IfcLengthMeasure;

		public IfcReferentTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcReferentTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }
		[Obsolete("DEPRECATED IFC4X3", false)]
		public double RestartDistance { get { return mRestartDistance; } set { mRestartDistance = value; } }

		public IfcReferent() : base() { }
		internal IfcReferent(DatabaseIfc db, IfcReferent r, DuplicateOptions options) : base(db, r, options) 
		{ 
			PredefinedType = r.PredefinedType;
			mRestartDistance = r.mRestartDistance;
		}
		public IfcReferent(DatabaseIfc db) : base(db) { }
		public IfcReferent(IfcAlignment alignment) : base(alignment.Database)
		{
			IfcRelNests relNests = alignment.IsNestedBy.Where(x => x.RelatedObjects.FirstOrDefault() is IfcReferent).FirstOrDefault();
			if (relNests != null)
				relNests.RelatedObjects.Add(this);
			else
			{
				new IfcRelNests(alignment, this);
			}
		}
		public IfcReferent(IfcSite host) : base(host) { }
		public IfcReferent(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) 
			: base(host.Database) 
		{
			IfcRelNests nests = host.IsNestedBy.Where(x => x.RelatedObjects.FirstOrDefault() is IfcReferent).FirstOrDefault();
			if (nests != null)
				nests.RelatedObjects.Add(this);
			else
			{
				new IfcRelNests(host, this);
			}
			ObjectPlacement = placement;
			Representation = representation;
		}
	}
	[Serializable]
	public partial class IfcRegularTimeSeries : IfcTimeSeries
	{
		private double mTimeStep = 0; //: IfcTimeMeasure;
		private LIST<IfcTimeSeriesValue> mValues = new LIST<IfcTimeSeriesValue>(); //: LIST[1:?] OF IfcTimeSeriesValue;

		public double TimeStep { get { return mTimeStep; } set { mTimeStep = value; } }
		public LIST<IfcTimeSeriesValue> Values { get { return mValues; } set { mValues = value; } }

		public IfcRegularTimeSeries() : base() { }
		public IfcRegularTimeSeries(string name, IfcDateTimeSelect startTime, IfcDateTimeSelect endTime, IfcTimeSeriesDataTypeEnum timeSeriesDataType, IfcDataOriginEnum dataOrigin, double timeStep, IEnumerable<IfcTimeSeriesValue> values)
			: base(name, startTime, endTime, timeSeriesDataType, dataOrigin)
		{
			TimeStep = timeStep;
			Values.AddRange(values);
		}
	}
	[Serializable, VersionAdded(ReleaseVersion.IFC4X3), Obsolete("DEPRECATED IFC4X4", false)]
	public partial class IfcReinforcedSoil : IfcEarthworksElement
	{
		private IfcReinforcedSoilTypeEnum mPredefinedType = IfcReinforcedSoilTypeEnum.NOTDEFINED; //: OPTIONAL IfcReinforcedSoilTypeEnum;
		public IfcReinforcedSoilTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcReinforcedSoilTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		public IfcReinforcedSoil() : base() { }
		public IfcReinforcedSoil(DatabaseIfc db) : base(db) { }
		public IfcReinforcedSoil(DatabaseIfc db, IfcReinforcedSoil reinforcedSoil, DuplicateOptions options) : base(db, reinforcedSoil, options) { PredefinedType = reinforcedSoil.PredefinedType; }
		public IfcReinforcedSoil(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	[Serializable]
	public partial class IfcReinforcementBarProperties : IfcPreDefinedProperties
	{
		internal double mTotalCrossSectionArea = 0; //:	IfcAreaMeasure;
		internal string mSteelGrade = ""; // : IfcLabel;
		internal IfcReinforcingBarSurfaceEnum mBarSurface = IfcReinforcingBarSurfaceEnum.NOTDEFINED; // :	OPTIONAL IfcReinforcingBarSurfaceEnum;
		internal double mEffectiveDepth = double.NaN;// : OPTIONAL IfcLengthMeasure;
		internal double mNominalBarDiameter = double.NaN; //	: OPTIONAL IfcPositiveLengthMeasure;
		internal double mBarCount = double.NaN; // : OPTIONAL IfcCountMeasure; 

		public double TotalCrossSectionArea { get { return mTotalCrossSectionArea; } set { mTotalCrossSectionArea = value; } }
		public string SteelGrade { get { return mSteelGrade; } set { mSteelGrade = string.IsNullOrEmpty(value) ? "Unknown" : value; } }
		public IfcReinforcingBarSurfaceEnum BarSurface { get { return mBarSurface; } set { mBarSurface = value; } }
		public double EffectiveDepth { get { return mEffectiveDepth; } set { mEffectiveDepth = value; } }
		public double NominalBarDiameter { get { return mNominalBarDiameter; } set { mNominalBarDiameter = value; } }
		public double BarCount { get { return mBarCount; } set { mBarCount = value; } }

		internal IfcReinforcementBarProperties() : base() { }
		internal IfcReinforcementBarProperties(IfcReinforcementBarProperties p) : base()
		{
			mTotalCrossSectionArea = p.mTotalCrossSectionArea;
			mSteelGrade = p.mSteelGrade;
			mBarSurface = p.mBarSurface;
			mEffectiveDepth = p.mEffectiveDepth;
			mNominalBarDiameter = p.mNominalBarDiameter;
			mBarCount = p.mBarCount;
		}
		public IfcReinforcementBarProperties(DatabaseIfc db, double totalCrossSectionArea, string steelGrade) : base(db) { mTotalCrossSectionArea = totalCrossSectionArea; SteelGrade = steelGrade; }
	}
	[Serializable]
	public partial class IfcReinforcementDefinitionProperties : IfcPreDefinedPropertySet //IFC2x3 IfcPropertySetDefinition
	{
		internal string mDefinitionType = "";// 	:	OPTIONAL IfcLabel; 
		internal LIST<IfcSectionReinforcementProperties> mReinforcementSectionDefinitions = new LIST<IfcSectionReinforcementProperties>();// :	LIST [1:?] OF IfcSectionReinforcementProperties;

		public string DefinitionType { get { return mDefinitionType; } set { mDefinitionType = value; } }
		public LIST<IfcSectionReinforcementProperties> ReinforcementSectionDefinitions { get { return mReinforcementSectionDefinitions; } }

		internal IfcReinforcementDefinitionProperties() : base() { }
		internal IfcReinforcementDefinitionProperties(DatabaseIfc db, IfcReinforcementDefinitionProperties p, DuplicateOptions options) : base(db, p, options)
		{
			mDefinitionType = p.mDefinitionType;
			ReinforcementSectionDefinitions.AddRange(p.ReinforcementSectionDefinitions.Select(x => db.Factory.Duplicate(x) as IfcSectionReinforcementProperties));
		}
		public IfcReinforcementDefinitionProperties(string name, IEnumerable<IfcSectionReinforcementProperties> sectProps)
			: base(sectProps.First().mDatabase, name) { ReinforcementSectionDefinitions.AddRange(sectProps); }
	}
	[Serializable]
	public partial class IfcReinforcingBar : IfcReinforcingElement
	{
		private double mNominalDiameter = double.NaN;// : IfcPositiveLengthMeasure; 	IFC4 OPTIONAL
		internal double mCrossSectionArea = double.NaN;// : IfcAreaMeasure; IFC4 OPTIONAL
		internal double mBarLength = double.NaN;// : OPTIONAL IfcPositiveLengthMeasure;
		private IfcReinforcingBarTypeEnum mPredefinedType = IfcReinforcingBarTypeEnum.NOTDEFINED;// : IfcReinforcingBarRoleEnum; IFC4 OPTIONAL
		internal IfcReinforcingBarSurfaceEnum mBarSurface = IfcReinforcingBarSurfaceEnum.NOTDEFINED;// //: OPTIONAL IfcReinforcingBarSurfaceEnum; 

		public double NominalDiameter
		{
			get
			{
				if (!double.IsNaN(mNominalDiameter))
					return mNominalDiameter;
				IfcReinforcingBarType t = RelatingType() as IfcReinforcingBarType;
				return (t != null ? t.NominalDiameter : double.NaN);
			}
			set { mNominalDiameter = value; }
		}
		public double CrossSectionArea { get { return mCrossSectionArea; } set { mCrossSectionArea = value; } }
		public double BarLength { get { return mBarLength; } set { mBarLength = value; } }
		public IfcReinforcingBarTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcReinforcingBarTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }
		public IfcReinforcingBarSurfaceEnum BarSurface { get { return mBarSurface; } set { mBarSurface = value; } }

		internal IfcReinforcingBar() : base() { }
		internal IfcReinforcingBar(DatabaseIfc db, IfcReinforcingBar b, DuplicateOptions options) : base(db, b, options)
		{
			mNominalDiameter = b.mNominalDiameter;
			mCrossSectionArea = b.mCrossSectionArea;
			mBarLength = b.mBarLength;
			PredefinedType = b.PredefinedType;
			mBarSurface = b.mBarSurface;
		}
		public IfcReinforcingBar(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	[Serializable]
	public partial class IfcReinforcingBarType : IfcReinforcingElementType  
	{
		private IfcReinforcingBarTypeEnum mPredefinedType = IfcReinforcingBarTypeEnum.NOTDEFINED;// : IfcReinforcingBarTypeEnum; 
		private double mNominalDiameter = double.NaN;// : OPTIONAL IfcPositiveLengthMeasure; 
		internal double mCrossSectionArea = double.NaN;// : OPTIONAL IfcAreaMeasure; 
		internal double mBarLength = double.NaN;// : OPTIONAL IfcPositiveLengthMeasure;
		internal IfcReinforcingBarSurfaceEnum mBarSurface = IfcReinforcingBarSurfaceEnum.NOTDEFINED;// //: OPTIONAL IfcReinforcingBarSurfaceEnum; 
		internal string mBendingShapeCode = "";//	:	OPTIONAL IfcLabel;
		internal List<IfcBendingParameterSelect> mBendingParameters = new List<IfcBendingParameterSelect>();//	:	OPTIONAL LIST [1:?] OF IfcBendingParameterSelect;

		public IfcReinforcingBarTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcReinforcingBarTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }
		public double NominalDiameter { get { return mNominalDiameter; } set { mNominalDiameter = value; } }
		public double CrossSectionArea { get { return mCrossSectionArea; } set { mCrossSectionArea = value; } }
		public double BarLength { get { return mBarLength; } set { mBarLength = value; } }
		public IfcReinforcingBarSurfaceEnum BarSurface { get { return mBarSurface; } set { mBarSurface = value; } }
		public string BendingShapeCode { get { return mBendingShapeCode; } set { mBendingShapeCode = value; } }
		public List<IfcBendingParameterSelect> BendingParameters { get { return mBendingParameters; } }

		internal IfcReinforcingBarType() : base() { }
		internal IfcReinforcingBarType(DatabaseIfc db, IfcReinforcingBarType t, DuplicateOptions options) : base(db, t, options)
		{
			PredefinedType = t.PredefinedType;
			mNominalDiameter = t.mNominalDiameter;
			mCrossSectionArea = t.mCrossSectionArea;
			mBarLength = t.mBarLength;
			mBarSurface = t.mBarSurface;
			mBendingShapeCode = t.mBendingShapeCode;
			mBendingParameters.AddRange(t.mBendingParameters);
		}
		public IfcReinforcingBarType(DatabaseIfc db, string name, IfcReinforcingBarTypeEnum type)
				: base(db)
		{
			Name = name;
			PredefinedType = type;
		}
		public IfcReinforcingBarType(DatabaseIfc db, string name, IfcReinforcingBarTypeEnum type, double diameter)
			: this(db, name, type)
		{
			mNominalDiameter = diameter;
		}
	}
	[Serializable]
	public abstract partial class IfcReinforcingElement : IfcElementComponent //	ABSTRACT SUPERTYPE OF(ONEOF(IfcReinforcingBar, IfcReinforcingMesh, IfcTendon, IfcTendonAnchor))
	{
		private string mSteelGrade = "";// : OPTIONAL IfcLabel; //IFC4 DEPRECATED 
		[Obsolete("DEPRECATED IFC4", false)]
		public string SteelGrade { get { return mSteelGrade; } set { mSteelGrade = value; } }

		protected IfcReinforcingElement() : base() { }
		protected IfcReinforcingElement(DatabaseIfc db) : base(db) { }
		protected IfcReinforcingElement(DatabaseIfc db, IfcReinforcingElement e, DuplicateOptions options) : base(db, e, options) { mSteelGrade = e.mSteelGrade; }
		public IfcReinforcingElement(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	[Serializable]
	public abstract partial class IfcReinforcingElementType : IfcElementComponentType //IFC4 ABSTRACT SUPERTYPE OF(ONEOF(IfcReinforcingBarType, IfcReinforcingMeshType, IfcTendonAnchorType, IfcTendonType))
	{
		public override string StepClassName { get { return (mDatabase.mRelease <= ReleaseVersion.IFC2x3 ? "IfcTypeProduct" : base.StepClassName); } }
		protected IfcReinforcingElementType() : base() { }
		protected IfcReinforcingElementType(DatabaseIfc db) : base(db) { }
		protected IfcReinforcingElementType(DatabaseIfc db, string name) : base(db) { Name = name; }
		protected IfcReinforcingElementType(DatabaseIfc db, IfcReinforcingElementType t, DuplicateOptions options) : base(db, t, options) { }
	}
	[Serializable]
	public partial class IfcReinforcingMesh : IfcReinforcingElement
	{
		internal double mMeshLength = double.NaN, mMeshWidth = double.NaN;// : OPTIONAL IfcPositiveLengthMeasure;
		internal double mLongitudinalBarNominalDiameter = double.NaN, mTransverseBarNominalDiameter = double.NaN;// :OPTIONAL IfcPositiveLengthMeasure;
		internal double mLongitudinalBarCrossSectionArea = double.NaN, mTransverseBarCrossSectionArea = double.NaN;// : OPTIONAL IfcAreaMeasure;
		internal double mLongitudinalBarSpacing = double.NaN, mTransverseBarSpacing = double.NaN;// : OPTIONAL IfcPositiveLengthMeasure;
		private IfcReinforcingMeshTypeEnum mPredefinedType = IfcReinforcingMeshTypeEnum.NOTDEFINED; //	:	OPTIONAL IfcReinforcingMeshTypeEnum;

		public double MeshLength { get { return mMeshLength; } set { mMeshLength = value; } }
		public double MeshWidth { get { return mMeshWidth; } set { mMeshWidth = value; } }
		public double LongitudinalBarNominalDiameter { get { return mLongitudinalBarNominalDiameter; } set { mLongitudinalBarNominalDiameter = value; } }
		public double TransverseBarNominalDiameter { get { return mTransverseBarNominalDiameter; } set { mTransverseBarNominalDiameter = value; } }
		public double LongitudinalBarCrossSectionArea { get { return mLongitudinalBarCrossSectionArea; } set { mLongitudinalBarCrossSectionArea = value; } }
		public double TransverseBarCrossSectionArea { get { return mTransverseBarCrossSectionArea; } set { mTransverseBarCrossSectionArea = value; } }
		public double LongitudinalBarSpacing { get { return mLongitudinalBarSpacing; } set { mLongitudinalBarSpacing = value; } }
		public double TransverseBarSpacing { get { return mTransverseBarSpacing; } set { mTransverseBarSpacing = value; } }
		public IfcReinforcingMeshTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcReinforcingMeshTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcReinforcingMesh() : base() { }
		internal IfcReinforcingMesh(DatabaseIfc db, IfcReinforcingMesh m, DuplicateOptions options) : base(db, m, options)
		{
			mMeshLength = m.mMeshLength;
			mMeshWidth = m.mMeshWidth;
			mLongitudinalBarNominalDiameter = m.mLongitudinalBarNominalDiameter;
			mTransverseBarNominalDiameter = m.mTransverseBarNominalDiameter;
			mLongitudinalBarCrossSectionArea = m.mLongitudinalBarCrossSectionArea;
			mTransverseBarCrossSectionArea = m.mTransverseBarCrossSectionArea;
			mLongitudinalBarSpacing = m.mLongitudinalBarSpacing;
			mTransverseBarSpacing = m.mTransverseBarSpacing;
			PredefinedType = m.PredefinedType;
		}
		public IfcReinforcingMesh(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	[Serializable]
	public partial class IfcReinforcingMeshType : IfcReinforcingElementType
	{
		private IfcReinforcingMeshTypeEnum mPredefinedType = IfcReinforcingMeshTypeEnum.NOTDEFINED; //	:	OPTIONAL IfcReinforcingMeshTypeEnum;
		internal double mMeshLength = double.NaN, mMeshWidth = double.NaN;// : OPTIONAL IfcPositiveLengthMeasure;
		internal double mLongitudinalBarNominalDiameter = double.NaN, mTransverseBarNominalDiameter = double.NaN;// :OPTIONAL IfcPositiveLengthMeasure;
		internal double mLongitudinalBarCrossSectionArea = double.NaN, mTransverseBarCrossSectionArea = double.NaN;// : OPTIONAL IfcAreaMeasure;
		internal double mLongitudinalBarSpacing = double.NaN, mTransverseBarSpacing = double.NaN;// : OPTIONAL IfcPositiveLengthMeasure;
		internal string mBendingShapeCode = ""; // : OPTIONAL IfcLabel;
		internal List<IfcBendingParameterSelect> mBendingParameters = new List<IfcBendingParameterSelect>(); // : OPTIONAL LIST [1:?] OF IfcBendingParameterSelect;

		public IfcReinforcingMeshTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcReinforcingMeshTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }
		public double MeshLength { get { return mMeshLength; } set { mMeshLength = value; } }
		public double MeshWidth { get { return mMeshWidth; } set { mMeshWidth = value; } }
		public double LongitudinalBarNominalDiameter { get { return mLongitudinalBarNominalDiameter; } set { mLongitudinalBarNominalDiameter = value; } }
		public double TransverseBarNominalDiameter { get { return mTransverseBarNominalDiameter; } set { mTransverseBarNominalDiameter = value; } }
		public double LongitudinalBarCrossSectionArea { get { return mLongitudinalBarCrossSectionArea; } set { mLongitudinalBarCrossSectionArea = value; } }
		public double TransverseBarCrossSectionArea { get { return mTransverseBarCrossSectionArea; } set { mTransverseBarCrossSectionArea = value; } }
		public double LongitudinalBarSpacing { get { return mLongitudinalBarSpacing; } set { mLongitudinalBarSpacing = value; } }
		public double TransverseBarSpacing { get { return mTransverseBarSpacing; } set { mTransverseBarSpacing = value; } }
		public string BendingShapeCode { get { return mBendingShapeCode; } set { mBendingShapeCode = value; } }
		public List<IfcBendingParameterSelect> BendingParameters { get { return mBendingParameters; } }

		internal IfcReinforcingMeshType() : base() { }
		internal IfcReinforcingMeshType(DatabaseIfc db, IfcReinforcingMeshType m, DuplicateOptions options) : base(db, m, options)
		{
			PredefinedType = m.PredefinedType;
			mMeshLength = m.mMeshLength;
			mMeshWidth = m.mMeshWidth;
			mLongitudinalBarNominalDiameter = m.mLongitudinalBarNominalDiameter;
			mTransverseBarNominalDiameter = m.mTransverseBarNominalDiameter;
			mLongitudinalBarCrossSectionArea = m.mLongitudinalBarCrossSectionArea;
			mTransverseBarCrossSectionArea = m.mTransverseBarCrossSectionArea;
			mLongitudinalBarSpacing = m.mLongitudinalBarSpacing;
			mTransverseBarSpacing = m.mTransverseBarSpacing;
			mBendingShapeCode = m.mBendingShapeCode;
			mBendingParameters = new List<IfcBendingParameterSelect>(m.mBendingParameters);
		}
		public IfcReinforcingMeshType(DatabaseIfc db, string name, IfcReinforcingMeshTypeEnum t) : base(db) { Name = name; PredefinedType = t; }
	}
	[Serializable]
	public partial class IfcRelAdheresToElement : IfcRelDecomposes 
	{
		private IfcElement mRelatingElement;// : IfcElement;
		private SET<IfcSurfaceFeature> mRelatedSurfaceFeatures = new SET<IfcSurfaceFeature>();// : SET [1:?] OF IfcSurfaceFeature; 

		public IfcElement RelatingElement { get { return mRelatingElement; } set { mRelatingElement = value; if (!value.mHasSurfaceFeatures.Contains(this)) value.mHasSurfaceFeatures.Add(this); } }
		public SET<IfcSurfaceFeature> RelatedSurfaceFeatures { get { return mRelatedSurfaceFeatures; } }

		internal IfcRelAdheresToElement() : base() { }
		internal IfcRelAdheresToElement(DatabaseIfc db, IfcRelAdheresToElement r, DuplicateOptions options) : base(db, r, options)
		{
			RelatingElement = db.Factory.Duplicate(r.RelatingElement) as IfcElement;
			RelatedSurfaceFeatures.AddRange(r.mRelatedSurfaceFeatures.Select(x=> db.Factory.Duplicate(x, options) as IfcSurfaceFeature));
		}
		public IfcRelAdheresToElement(IfcElement host, IfcSurfaceFeature fes)
			: base(host.mDatabase) { RelatingElement = host; RelatedSurfaceFeatures.Add(fes); }

		protected override void initialize()
		{
			base.initialize();
			mRelatedSurfaceFeatures.CollectionChanged += mRelatedSurfaceFeatures_CollectionChanged;
		}
		private void mRelatedSurfaceFeatures_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			if (mDatabase != null && mDatabase.IsDisposed())
				return;
			if (e.NewItems != null)
			{
				foreach (IfcSurfaceFeature o in e.NewItems)
				{
					if (o.AdheresToElement != this)
						o.AdheresToElement = this;
				}
			}
			if (e.OldItems != null)
			{
				foreach (IfcSurfaceFeature o in e.OldItems)
				{
					if (o.AdheresToElement == this)
						o.mAdheresToElement = null;
				}
			}
		}
	}
	[Serializable]
	public partial class IfcRelAggregates : IfcRelDecomposes
	{
		internal IfcObjectDefinition mRelatingObject;// : IfcObjectDefinition IFC4 IfcObject
		internal SET<IfcObjectDefinition> mRelatedObjects = new SET<IfcObjectDefinition>();// : SET [1:?] OF IfcObjectDefinition; 

		public IfcObjectDefinition RelatingObject 
		{ 
			get { return mRelatingObject; } 
			set 
			{ 
				if (mRelatingObject != null) 
					mRelatingObject.mIsDecomposedBy.Remove(this);
				mRelatingObject = value;
				value.mIsDecomposedBy.Add(this); 
			} 
		}
		public SET<IfcObjectDefinition> RelatedObjects { get { return mRelatedObjects; } }

		internal IfcRelAggregates() : base() { }
		internal IfcRelAggregates(DatabaseIfc db, IfcRelAggregates a, DuplicateOptions options) : base(db, a, options)
		{
			RelatingObject = db.Factory.Duplicate(a.RelatingObject, options) as IfcObjectDefinition;
			if (options.DuplicateDownstream)
            {
				DuplicateOptions optionsNoHost = new DuplicateOptions(options) { DuplicateHost = false };
                mRelatedObjects.AddRange(a.RelatedObjects.Select(x=> db.Factory.Duplicate(x, optionsNoHost) as IfcObjectDefinition));
            }
		}
		internal IfcRelAggregates(IfcObjectDefinition relatingObject) 
			: base(relatingObject.mDatabase)
		{ 
			RelatingObject = relatingObject;
			GlobalId = ParserIfc.HashGlobalID(relatingObject.GlobalId + "Aggregates" + relatingObject.IsDecomposedBy.Count);
		}
		public IfcRelAggregates(IfcObjectDefinition relatingObject, IfcObjectDefinition relatedObject) : this(relatingObject, new List<IfcObjectDefinition>() { relatedObject }) { }
		public IfcRelAggregates(IfcObjectDefinition relatingObject, IEnumerable<IfcObjectDefinition> relatedObjects) : this(relatingObject)
		{
			foreach (IfcObjectDefinition od in relatedObjects)
				od.Decomposes = this;
		}

		protected override void initialize()
		{
			base.initialize();
			mRelatedObjects.CollectionChanged += mRelatedObjects_CollectionChanged;
		}
		private void mRelatedObjects_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			if (mDatabase != null && mDatabase.IsDisposed())
				return;
			if (e.NewItems != null)
			{
				foreach (IfcObjectDefinition o in e.NewItems)
				{
					if (o.Decomposes != this)
						o.mDecomposes = this;
				}
			}
			if (e.OldItems != null)
			{
				foreach (IfcObjectDefinition o in e.OldItems)
				{
					if (o.Decomposes == this)
						o.mDecomposes = null;
				}
			}
		}
	}
	[Serializable]
	public abstract partial class IfcRelAssigns : IfcRelationship //	ABSTRACT SUPERTYPE OF(ONEOF(IfcRelAssignsToActor, IfcRelAssignsToControl, IfcRelAssignsToGroup, IfcRelAssignsToProcess, IfcRelAssignsToProduct, IfcRelAssignsToResource))
	{
		internal SET<IfcObjectDefinition> mRelatedObjects = new SET<IfcObjectDefinition>();// : SET [1:?] OF IfcObjectDefinition;
		internal IfcObjectTypeEnum mRelatedObjectsType = IfcObjectTypeEnum.NOTDEFINED;// : OPTIONAL IfcObjectTypeEnum; IFC4 CHANGE  The attribute is deprecated and shall no longer be used. A NIL value should always be assigned.
		public SET<IfcObjectDefinition> RelatedObjects { get { return mRelatedObjects; } }

		public abstract NamedObjectIfc Relating();

		protected IfcRelAssigns() : base() { }
		protected IfcRelAssigns(DatabaseIfc db) : base(db) { }
		protected IfcRelAssigns(DatabaseIfc db, IfcRelAssigns a, DuplicateOptions options) : base(db, a, options)
		{
			if (options.DuplicateDownstream)
				RelatedObjects.AddRange(a.RelatedObjects.ConvertAll(x => db.Factory.Duplicate(x, options) as IfcObjectDefinition));
			mRelatedObjectsType = a.mRelatedObjectsType;
		}
		protected IfcRelAssigns(IfcObjectDefinition related) : this(new List<IfcObjectDefinition>() { related }) { }
		protected IfcRelAssigns(IEnumerable<IfcObjectDefinition> relObjects) : base(relObjects.First().mDatabase) { RelatedObjects.AddRange(relObjects); }

		protected override void initialize()
		{
			base.initialize();
			mRelatedObjects.CollectionChanged += mRelatedObjects_CollectionChanged;
		}
		private void mRelatedObjects_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (mDatabase != null && mDatabase.IsDisposed())
				return;
			if (e.NewItems != null)
			{
				foreach (IfcObjectDefinition d in e.NewItems)
				{
					if (!d.HasAssignments.Contains(this))
						d.HasAssignments.Add(this);
				}
			}
			if (e.OldItems != null)
			{
				foreach (IfcObjectDefinition d in e.OldItems)
				{
					d.HasAssignments.Remove(this);
				}
			}
		}
	}
	[Obsolete("DEPRECATED IFC4", false)]
	public partial class IfcRelAssignsTasks : IfcRelAssignsToControl // IFC4 DEPRECATED
	{
		private IfcScheduleTimeControl mTimeForTask;// :  	OPTIONAL IfcScheduleTimeControl; 

		public IfcScheduleTimeControl TimeForTask
		{
			get { return mTimeForTask; }
			set { mTimeForTask = value; if (value != null) value.mScheduleTimeControlAssigned = this; }
		}
		internal IfcWorkControl WorkControl { get { return mRelatingControl as IfcWorkControl; } }

		internal IfcRelAssignsTasks() : base() { }
		internal IfcRelAssignsTasks(DatabaseIfc db, IfcRelAssignsTasks r, DuplicateOptions options) : base(db, r, options) { if (r.mTimeForTask != null) TimeForTask = db.Factory.Duplicate(r.TimeForTask, options) as IfcScheduleTimeControl; }
		public IfcRelAssignsTasks(IfcWorkControl relating, IfcTask related ) : base(relating, related) { related.mOwningControls.Add(this); }
	}
	[Serializable]
	public partial class IfcRelAssignsToActor : IfcRelAssigns
	{
		internal IfcActor mRelatingActor;// : IfcActor; 
		internal IfcActorRole mActingRole;//	 :	OPTIONAL IfcActorRole;

		public IfcActor RelatingActor { get { return mRelatingActor; } set { mRelatingActor = value; value.mIsActingUpon.Add(this); } }
		public IfcActorRole ActingRole { get { return mActingRole; } set { mActingRole = value; } }

		public override NamedObjectIfc Relating() { return RelatingActor; } 

		internal IfcRelAssignsToActor() : base() { }
		internal IfcRelAssignsToActor(DatabaseIfc db, IfcRelAssignsToActor r, DuplicateOptions options) : base(db, r, options)
		{
			RelatingActor = db.Factory.Duplicate(r.RelatingActor, options) as IfcActor;
			if (r.mActingRole != null)
				ActingRole = db.Factory.Duplicate(r.ActingRole, options) as IfcActorRole;
		}
		public IfcRelAssignsToActor(IfcActor relActor) : base(relActor.mDatabase) { RelatingActor = relActor; }
		public IfcRelAssignsToActor(IfcActor relActor, IfcObjectDefinition relObject) : base(relObject) { RelatingActor = relActor; }
		public IfcRelAssignsToActor(IfcActor relActor, List<IfcObjectDefinition> relObjects) : base(relObjects) { RelatingActor = relActor; }
	}
	[Serializable]
	public partial class IfcRelAssignsToControl : IfcRelAssigns
	{
		internal IfcControl mRelatingControl;// : IfcControl; 
		public IfcControl RelatingControl { get { return mRelatingControl; } set { mRelatingControl = value; value.mControls.Add(this); } }

		public override NamedObjectIfc Relating() { return RelatingControl; } 

		internal IfcRelAssignsToControl() : base() { }
		internal IfcRelAssignsToControl(DatabaseIfc db, IfcRelAssignsToControl r, DuplicateOptions options) : base(db, r, options)
		{
			RelatingControl = db.Factory.Duplicate(r.RelatingControl, new DuplicateOptions(options.DeviationTolerance) { DuplicateDownstream = false }) as IfcControl;
		}
		public IfcRelAssignsToControl(IfcControl relating) : base(relating.mDatabase) { RelatingControl = relating;  }
		public IfcRelAssignsToControl(IfcControl relating, IfcObjectDefinition related) : base(related) { RelatingControl = relating; }
		public IfcRelAssignsToControl(IfcControl relating, IEnumerable<IfcObjectDefinition> related) : base(related) { RelatingControl = relating; }
	}
	[Serializable]
	public partial class IfcRelAssignsToGroup : IfcRelAssigns   //SUPERTYPE OF(IfcRelAssignsToGroupByFactor)
	{
		private IfcGroup mRelatingGroup;// : IfcGroup; 
		public IfcGroup RelatingGroup { get { return mRelatingGroup; } set { mRelatingGroup = value; if(value != null) value.mIsGroupedBy.Add(this); } }

		public override NamedObjectIfc Relating() { return RelatingGroup; } 

		internal IfcRelAssignsToGroup() : base() { }
		internal IfcRelAssignsToGroup(DatabaseIfc db, IfcRelAssignsToGroup a, DuplicateOptions options) : base(db, a, options)
		{
			RelatingGroup = db.Factory.Duplicate(a.RelatingGroup, new DuplicateOptions(options) { DuplicateDownstream = false }) as IfcGroup;
		}
		public IfcRelAssignsToGroup(IfcGroup relgroup) : base(relgroup.mDatabase) { RelatingGroup = relgroup; }
		public IfcRelAssignsToGroup(IfcObjectDefinition related, IfcGroup relating) : base(related) { RelatingGroup = relating; }
		public IfcRelAssignsToGroup(IEnumerable<IfcObjectDefinition> related, IfcGroup relating) : base(related) { RelatingGroup = relating; }
	}
	[Serializable]
	public partial class IfcRelAssignsToGroupByFactor : IfcRelAssignsToGroup //IFC4
	{
		public override string StepClassName { get { return (mDatabase.mRelease < ReleaseVersion.IFC4 ? "IfcRelAssignsToGroup" : base.StepClassName); } }
		internal double mFactor = 1;//	 :	IfcRatioMeasure;
		public double Factor { get { return mFactor; } set { mFactor = value; } }

		internal IfcRelAssignsToGroupByFactor() : base() { }
		internal IfcRelAssignsToGroupByFactor(DatabaseIfc db, IfcRelAssignsToGroupByFactor a, DuplicateOptions options) : base(db, a, options) { mFactor = a.mFactor; }
		public IfcRelAssignsToGroupByFactor(IfcGroup relgroup, double factor) : base(relgroup) { mFactor = factor; }
		public IfcRelAssignsToGroupByFactor(IEnumerable<IfcObjectDefinition> relObjects, IfcGroup relgroup, double factor) : base(relObjects, relgroup) { mFactor = factor; }
	}
	[Serializable]
	public partial class IfcRelAssignsToProcess : IfcRelAssigns
	{
		internal IfcProcessSelect mRelatingProcess;// : IfcProcessSelect
		internal IfcMeasureWithUnit mQuantityInProcess = null;//	 : 	OPTIONAL IfcMeasureWithUnit;
		public IfcProcessSelect RelatingProcess { get { return mRelatingProcess; } set { mRelatingProcess = value; value.OperatesOn.Add(this); } }
		public IfcMeasureWithUnit QuantityInProcess { get { return mQuantityInProcess; } set { mQuantityInProcess = value; } }

		public override NamedObjectIfc Relating() { return RelatingProcess as NamedObjectIfc; } 

		internal IfcRelAssignsToProcess() : base() { }
		internal IfcRelAssignsToProcess(DatabaseIfc db, IfcRelAssignsToProcess r, DuplicateOptions options) : base(db, r, options)
		{
			RelatingProcess = db.Factory.Duplicate(r.RelatingProcess as BaseClassIfc, new DuplicateOptions(options) { DuplicateDownstream = false }) as IfcProcess;
		}
		public IfcRelAssignsToProcess(IfcProcessSelect relating) : base(relating.Database) { RelatingProcess = relating;  }
		public IfcRelAssignsToProcess(IfcProcessSelect relating, IfcObjectDefinition related) : base(related) { RelatingProcess = relating; }
		public IfcRelAssignsToProcess(IfcProcessSelect relating, IEnumerable<IfcObjectDefinition> related) : base(related) { RelatingProcess = relating; }
	}
	[Serializable]
	public partial class IfcRelAssignsToProduct : IfcRelAssigns
	{
		private IfcProductSelect mRelatingProduct = null;// : IFC4	IfcProductSelect; : IfcProduct; 
		public IfcProductSelect RelatingProduct { get { return mRelatingProduct; } set { mRelatingProduct = value; if(value != null && !value.ReferencedBy.Contains(this)) value.ReferencedBy.Add(this); } }

		public override NamedObjectIfc Relating() { return RelatingProduct as NamedObjectIfc; }

		internal IfcRelAssignsToProduct() : base() { }
		internal IfcRelAssignsToProduct(DatabaseIfc db, IfcRelAssignsToProduct r, DuplicateOptions options) 
			: base(db, r, options) { RelatingProduct = db.Factory.Duplicate(r.RelatingProduct as BaseClassIfc, options) as IfcProductSelect; }
		public IfcRelAssignsToProduct(IfcProductSelect relatingProduct) : base(relatingProduct.Database) 
		{ 
			RelatingProduct = relatingProduct;
			GlobalId = ParserIfc.HashGlobalID(relatingProduct.GlobalId + relatingProduct.ReferencedBy.Count);
		}
		public IfcRelAssignsToProduct(IfcObjectDefinition relatedObject, IfcProductSelect relatingProduct) : this(relatingProduct) { RelatedObjects.Add(relatedObject); }
		public IfcRelAssignsToProduct(IEnumerable<IfcObjectDefinition> relelatedObjects, IfcProductSelect relatingProduct) : this(relatingProduct) { RelatedObjects.AddRange(relelatedObjects); }
	}
	[Obsolete("DEPRECATED IFC4", false)]
	public partial class IfcRelAssignsToProjectOrder : IfcRelAssignsToControl
	{
		internal IfcRelAssignsToProjectOrder() : base() { }
		internal IfcRelAssignsToProjectOrder(DatabaseIfc db, IfcRelAssignsToProjectOrder r, DuplicateOptions options) : base(db, r, options) { }
		public IfcRelAssignsToProjectOrder(IfcControl relating) : base(relating) { }
		public IfcRelAssignsToProjectOrder(IfcControl relating, IfcObjectDefinition related) : base(relating, related) { }
		public IfcRelAssignsToProjectOrder(IfcControl relating, IEnumerable<IfcObjectDefinition> related) : base(relating, related) { }

	}
	[Serializable]
	public partial class IfcRelAssignsToResource : IfcRelAssigns
	{
		internal IfcResourceSelect mRelatingResource;// : IfcResourceSelect; 
		public IfcResourceSelect RelatingResource { get { return mRelatingResource; } set { mRelatingResource = value; value.ResourceOf.Add(this); } }

		public override NamedObjectIfc Relating() { return RelatingResource as NamedObjectIfc; }

		internal IfcRelAssignsToResource() : base() { }
		internal IfcRelAssignsToResource(DatabaseIfc db, IfcRelAssignsToResource r, DuplicateOptions options) : base(db, r, options)
		{
			RelatingResource = db.Factory.Duplicate(r.RelatingResource as BaseClassIfc, new DuplicateOptions(options.DeviationTolerance) { DuplicateDownstream = false }) as IfcResource;
		}
		public IfcRelAssignsToResource(IfcResourceSelect relResource) : base(relResource.Database) { RelatingResource = relResource; }
		public IfcRelAssignsToResource(IfcResourceSelect relResource, IfcObjectDefinition relObject) : base(relObject) { RelatingResource = relResource; }
	}
	[Serializable]
	public abstract partial class IfcRelAssociates : IfcRelationship   //ABSTRACT SUPERTYPE OF (ONEOF(IfcRelAssociatesApproval,IfcRelAssociatesclassification,IfcRelAssociatesConstraint,IfcRelAssociatesDocument,IfcRelAssociatesLibrary,IfcRelAssociatesMaterial))
	{
		internal SET<IfcDefinitionSelect> mRelatedObjects = new SET<IfcDefinitionSelect>();// : SET [1:?] OF IfcDefinitionSelect IFC2x3 IfcRoot; 
		public SET<IfcDefinitionSelect> RelatedObjects { get { return mRelatedObjects; } }

		public abstract NamedObjectIfc Relating();

		protected IfcRelAssociates() : base() { }
		protected IfcRelAssociates(DatabaseIfc db) : base(db) { }
		protected IfcRelAssociates(DatabaseIfc db, IfcRelAssociates r, DuplicateOptions options) : base(db, r, options)
		{
			//RelatedObjects = r.mRelatedObjects.ConvertAll(x => db.Factory.Duplicate(r.mDatabase[x]) as IfcDefinitionSelect);
		}
		protected IfcRelAssociates(IfcDefinitionSelect related) : base(related.Database) { RelatedObjects.Add(related); }
		protected IfcRelAssociates(IEnumerable<IfcDefinitionSelect> related) : base(related.First().Database) { RelatedObjects.AddRange(related); }

		protected override void initialize()
		{
			base.initialize();
			mRelatedObjects.CollectionChanged += mRelatedObjects_CollectionChanged;
		}
		private void mRelatedObjects_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (mDatabase != null && mDatabase.IsDisposed())
				return;
			if (e.NewItems != null)
			{
				foreach (IfcDefinitionSelect d in e.NewItems)
				{
					if (!d.HasAssociations.Contains(this))
						d.HasAssociations.Add(this);
				}
			}
			if (e.OldItems != null)
			{
				foreach (IfcDefinitionSelect d in e.OldItems)
				{
					d.HasAssociations.Remove(this);	
				}
			}
		}

		public override string ToString() { return (mRelatedObjects.Count == 0 ? "" : base.ToString()); }
	}
	//[Obsolete("DEPRECATED IFC4", false)]
	//ENTITY IfcRelAssociatesAppliedValue // DEPRECATED IFC4
	[Serializable]
	public partial class IfcRelAssociatesApproval : IfcRelAssociates
	{
		internal IfcApproval mRelatingApproval;// : IfcApproval; 
		public IfcApproval RelatingApproval { get { return mRelatingApproval; } set { mRelatingApproval = value; value.ApprovedObjects.Add(this); } }

		public override NamedObjectIfc Relating() { return RelatingApproval; }

		internal IfcRelAssociatesApproval() : base() { }
		internal IfcRelAssociatesApproval(DatabaseIfc db, IfcRelAssociatesApproval r, DuplicateOptions options) : base(db, r, options) { RelatingApproval = db.Factory.Duplicate(r.mRelatingApproval, options); }
		public IfcRelAssociatesApproval(IfcApproval approval) : base(approval.Database) { RelatingApproval = approval; }
		public IfcRelAssociatesApproval(IfcDefinitionSelect related, IfcApproval approval) : base(related) { RelatingApproval = approval; }
		public IfcRelAssociatesApproval(IEnumerable<IfcDefinitionSelect> related, IfcApproval approval) : base(related) { RelatingApproval = approval; }
	}
	[Serializable]
	public partial class IfcRelAssociatesClassification : IfcRelAssociates
	{
		internal IfcClassificationSelect mRelatingClassification;// : IfcClassificationSelect; IFC2x3  	IfcClassificationNotationSelect
		public IfcClassificationSelect RelatingClassification
		{
			get { return mRelatingClassification; }
			set
			{
				mRelatingClassification = value;
				IfcClassification classification = value as IfcClassification;
				if (classification != null)
					classification.ClassificationForObjects.Add(this);
				else
				{
					IfcClassificationReference classificationReference = value as IfcClassificationReference;
					if (classificationReference != null)
						classificationReference.ClassificationRefForObjects.Add(this);
				}
			}
		}

		public override NamedObjectIfc Relating() { return RelatingClassification; } 

		internal IfcRelAssociatesClassification() : base() { }
		internal IfcRelAssociatesClassification(DatabaseIfc db, IfcRelAssociatesClassification r, DuplicateOptions options) 
			: base(db, r, options) { RelatingClassification = db.Factory.Duplicate(r.RelatingClassification); }
		public IfcRelAssociatesClassification(IfcClassificationSelect classification) : base(classification.Database) { RelatingClassification = classification; }
		public IfcRelAssociatesClassification(IfcClassificationSelect classification, IfcDefinitionSelect related) : base(related) { RelatingClassification = classification; }
		public IfcRelAssociatesClassification(IfcClassificationSelect classification, IEnumerable<IfcDefinitionSelect> related) : base(related) { RelatingClassification = classification; }
	}
	[Serializable]
	public partial class IfcRelAssociatesConstraint : IfcRelAssociates
	{
		internal string mIntent = "";// :	OPTIONAL IfcLabel;
		private IfcConstraint mRelatingConstraint;// : IfcConstraint

		public string Intent { get { return mIntent; } set { mIntent = value; } }
		public IfcConstraint RelatingConstraint { get { return mRelatingConstraint; } set { mRelatingConstraint = value; value.mConstraintForObjects.Add(this); } }

		public override NamedObjectIfc Relating() { return RelatingConstraint; } 

		internal IfcRelAssociatesConstraint() : base() { }
		internal IfcRelAssociatesConstraint(DatabaseIfc db, IfcRelAssociatesConstraint c, DuplicateOptions options) : base(db, c, options) { RelatingConstraint = db.Factory.Duplicate(c.RelatingConstraint) as IfcConstraint; }
		public IfcRelAssociatesConstraint(IfcConstraint c) : base(c.mDatabase) { RelatingConstraint = c; }
		public IfcRelAssociatesConstraint(IfcDefinitionSelect related, IfcConstraint constraint) : base(related) { RelatingConstraint = constraint; }
	}
	[Serializable, VersionAdded(ReleaseVersion.IFC4X4_DRAFT)]
	public partial class IfcRelAssociatesDataset : IfcRelAssociates
	{
		internal IfcDatasetSelect mRelatingDataset;// : IfcDatasetSelect;
		public IfcDatasetSelect RelatingDataset
		{
			get { return mRelatingDataset; }
			set
			{
				mRelatingDataset = value;
				IfcDatasetInformation datasetInformation = value as IfcDatasetInformation;
				if (datasetInformation != null)
					datasetInformation.DatasetInfoForObjects.Add(this);
				else
				{
					IfcDatasetReference datasetReference = value as IfcDatasetReference;
					if (datasetReference != null)
						datasetReference.DatasetRefForObjects.Add(this);
				}
			}
		}

		public override NamedObjectIfc Relating() { return RelatingDataset; }

		internal IfcRelAssociatesDataset() : base() { }
		internal IfcRelAssociatesDataset(DatabaseIfc db, IfcRelAssociatesDataset r, DuplicateOptions options) 
			: base(db, r, options) { RelatingDataset = db.Factory.Duplicate(r.RelatingDataset); }
		public IfcRelAssociatesDataset(IfcDatasetSelect dataset) : base(dataset.Database) { RelatingDataset = dataset; }
		public IfcRelAssociatesDataset(IfcDatasetSelect dataset, IfcDefinitionSelect related) : base(related) { RelatingDataset = dataset; }
		public IfcRelAssociatesDataset(IfcDatasetSelect dataset, IEnumerable<IfcDefinitionSelect> related) : base(related) { RelatingDataset = dataset; }
	}
	[Serializable]
	public partial class IfcRelAssociatesDocument : IfcRelAssociates
	{
		internal IfcDocumentSelect mRelatingDocument;// : IfcDocumentSelect; 
		public IfcDocumentSelect RelatingDocument { get { return mRelatingDocument; } set { mRelatingDocument = value; value.DocumentForObjects.Add(this); } }

		public override NamedObjectIfc Relating() { return RelatingDocument; } 

		internal IfcRelAssociatesDocument() : base() { }
		internal IfcRelAssociatesDocument(DatabaseIfc db, IfcRelAssociatesDocument r, DuplicateOptions options) : base(db, r, options) { RelatingDocument = db.Factory.Duplicate<IfcDocumentSelect>(r.mRelatingDocument, options); }
		public IfcRelAssociatesDocument(IfcDocumentSelect document) : base(document.Database) { RelatingDocument = document; }
		public IfcRelAssociatesDocument(IfcDefinitionSelect related, IfcDocumentSelect document) : base(related) { RelatingDocument = document; }
		public IfcRelAssociatesDocument(IEnumerable<IfcDefinitionSelect> related, IfcDocumentSelect document) : base(related) { RelatingDocument = document; }
	}
	[Serializable]
	public partial class IfcRelAssociatesLibrary : IfcRelAssociates
	{
		internal IfcLibrarySelect mRelatingLibrary;// : IfcLibrarySelect; 
		public IfcLibrarySelect RelatingLibrary { get { return mRelatingLibrary; } set { mRelatingLibrary = value; } }

		public override NamedObjectIfc Relating() { return RelatingLibrary; } 

		internal IfcRelAssociatesLibrary() : base() { }
		internal IfcRelAssociatesLibrary(DatabaseIfc db, IfcRelAssociatesLibrary r, DuplicateOptions options) : base(db, r, options) { RelatingLibrary = db.Factory.Duplicate(r.mRelatingLibrary as BaseClassIfc, options) as IfcLibrarySelect; }
		public IfcRelAssociatesLibrary(IfcLibrarySelect library) : base(library.Database) { RelatingLibrary = library; }
		public IfcRelAssociatesLibrary(IfcLibrarySelect library, IfcDefinitionSelect related) : base(related) { RelatingLibrary = library; }
		public IfcRelAssociatesLibrary(IfcLibrarySelect library, IEnumerable<IfcDefinitionSelect> related) : base(library.Database) { RelatingLibrary = library; mRelatedObjects.AddRange(related); }
	}
	[Serializable]
	public partial class IfcRelAssociatesMaterial : IfcRelAssociates
	{
		private IfcMaterialSelect mRelatingMaterial;// : IfcMaterialSelect; 
		public IfcMaterialSelect RelatingMaterial
		{
			get { return mRelatingMaterial; }
			set
			{
				IfcMaterialSelect material = RelatingMaterial;
				if (material != null)
					material.AssociatedTo.Remove(this);
				mRelatingMaterial = value;
				if (value != null)
					value.AssociatedTo.Add(this);
			}
		}

		public override NamedObjectIfc Relating() { return RelatingMaterial; } 

		internal IfcRelAssociatesMaterial() : base() { }
		internal IfcRelAssociatesMaterial(DatabaseIfc db, IfcRelAssociatesMaterial r, DuplicateOptions options) : base(db, r, options)
		{
			RelatingMaterial = db.Factory.Duplicate<IfcMaterialSelect>(r.mRelatingMaterial, options);
		}
		public IfcRelAssociatesMaterial(IfcMaterialSelect material) : base(material.Database) { RelatingMaterial = material; }
		public IfcRelAssociatesMaterial(IfcMaterialSelect material, IEnumerable<IfcDefinitionSelect> related) : base(related) { RelatingMaterial = material; }
	}
	[Serializable]
	public partial class IfcRelAssociatesProfileDef : IfcRelAssociates
	{
		private IfcProfileDef mRelatingProfileDef = null; //: IfcProfileDef;
		public IfcProfileDef RelatingProfileDef { get { return mRelatingProfileDef; } set { mRelatingProfileDef = value; } }

		public IfcRelAssociatesProfileDef() : base() { }
		internal IfcRelAssociatesProfileDef(DatabaseIfc db, IfcRelAssociatesProfileDef relAssociatesProfileDef, DuplicateOptions options) : base(db, relAssociatesProfileDef, options) { RelatingProfileDef = db.Factory.Duplicate(relAssociatesProfileDef.RelatingProfileDef) as IfcProfileDef; }
		public IfcRelAssociatesProfileDef(IEnumerable<IfcDefinitionSelect> relatedObjects, IfcProfileDef relatingProfileDef)
			: base(relatedObjects) { RelatingProfileDef = relatingProfileDef; }

		public override NamedObjectIfc Relating() { return RelatingProfileDef; }
	}
	[Obsolete("DELETED IFC4", false)]
	[Serializable]
	public partial class IfcRelAssociatesProfileProperties : IfcRelAssociates //IFC4 DELETED Replaced by IfcRelAssociatesMaterial together with material-profile sets
	{
		private IfcProfileProperties mRelatingProfileProperties;// : IfcProfileProperties;
		internal IfcShapeAspect mProfileSectionLocation;// : OPTIONAL IfcShapeAspect;
		internal IfcOrientationSelect mProfileOrientation = null; // : OPTIONAL IfcOrientationSelect;

		public IfcProfileProperties RelatingProfileProperties { get { return mRelatingProfileProperties; } set { mRelatingProfileProperties = value; } }
		public IfcShapeAspect ProfileSectionLocation { get { return mProfileSectionLocation; } set { mProfileSectionLocation = value; } }

		public override NamedObjectIfc Relating() { return RelatingProfileProperties; } 

		internal IfcRelAssociatesProfileProperties() : base() { }
		internal IfcRelAssociatesProfileProperties(DatabaseIfc db, IfcRelAssociatesProfileProperties r, DuplicateOptions options) : base(db, r, options)
		{
			RelatingProfileProperties = db.Factory.Duplicate(r.RelatingProfileProperties) as IfcProfileProperties;
			if (r.mProfileSectionLocation != null)
				ProfileSectionLocation = db.Factory.Duplicate(r.ProfileSectionLocation) as IfcShapeAspect;
			if (r.mProfileOrientation is BaseClassIfc)
				mProfileOrientation = db.Factory.Duplicate<IfcOrientationSelect>(r.mProfileOrientation);
			else
				mProfileOrientation = r.mProfileOrientation;
		}
		public IfcRelAssociatesProfileProperties(IfcProfileProperties pp) : base(pp.mDatabase) { mRelatingProfileProperties = pp; }
		public IfcRelAssociatesProfileProperties(IfcDefinitionSelect related, IfcProfileProperties pp) : base(related) { mRelatingProfileProperties = pp; }
	}
	[Serializable]
	public abstract partial class IfcRelationship : IfcRoot  //ABSTRACT SUPERTYPE OF (ONEOF (IfcRelAssigns ,IfcRelAssociates ,IfcRelConnects ,IfcRelDecomposes ,IfcRelDefines))
	{
		protected IfcRelationship() : base() { }
		internal IfcRelationship(DatabaseIfc db) : base(db) { }
		protected IfcRelationship(DatabaseIfc db, IfcRelationship r, DuplicateOptions options) : base(db, r, options) { }
	}
	[Obsolete("DEPRECATED IFC4", false)]
	[Serializable]
	public partial class IfcRelaxation : BaseClassIfc// DEPRECATED IFC4
	{
		internal double mRelaxationValue;// : IfcNormalisedRatioMeasure;
		internal double mInitialStress;// : IfcNormalisedRatioMeasure; 

		public double RelaxationValue { get { return mRelaxationValue; } set { mRelaxationValue = value; } }
		public double InitialStress { get { return mInitialStress; } set { mInitialStress = value; } }

		internal IfcRelaxation() : base() { }
		internal IfcRelaxation(DatabaseIfc db, IfcRelaxation p) : base(db) { mRelaxationValue = p.mRelaxationValue; mInitialStress = p.mInitialStress; }
	}
	[Serializable]
	public abstract partial class IfcRelConnects : IfcRelationship //ABSTRACT SUPERTYPE OF (ONEOF (IfcRelConnectsElements ,IfcRelConnectsPortToElement ,IfcRelConnectsPorts ,IfcRelConnectsStructuralActivity ,IfcRelConnectsStructuralMember
	{  //,IfcRelContainedInSpatialStructure ,IfcRelCoversBldgElements ,IfcRelCoversSpaces ,IfcRelFillsElement ,IfcRelFlowControlElements ,IfcRelInterferesElements ,IfcRelReferencedInSpatialStructure ,IfcRelSequence ,IfcRelServicesBuildings ,IfcRelSpaceBoundary))
		protected IfcRelConnects() : base() { }
		protected IfcRelConnects(DatabaseIfc db) : base(db) { }
		protected IfcRelConnects(DatabaseIfc db, IfcRelConnects r, DuplicateOptions options) : base(db, r, options) { }
	}
	[Serializable]
	public partial class IfcRelConnectsElements : IfcRelConnects //	SUPERTYPE OF(ONEOF(IfcRelConnectsPathElements, IfcRelConnectsWithRealizingElements))
	{
		internal IfcConnectionGeometry mConnectionGeometry;// : OPTIONAL IfcConnectionGeometry;
		internal IfcElement mRelatingElement;// : IfcElement;
		internal IfcElement mRelatedElement;// : IfcElement; 

		public IfcConnectionGeometry ConnectionGeometry { get { return mConnectionGeometry; } set { mConnectionGeometry = value; } }
		public IfcElement RelatingElement { get { return mRelatingElement; } set { mRelatingElement = value; if(value != null && !value.ConnectedTo.Contains(this)) value.ConnectedTo.Add(this); } }
		public IfcElement RelatedElement { get { return mRelatedElement; } set { mRelatedElement = value; if(value != null && !value.ConnectedFrom.Contains(this)) value.ConnectedFrom.Add(this); } }

		internal IfcRelConnectsElements() : base() { }
		internal IfcRelConnectsElements(DatabaseIfc db, IfcRelConnectsElements r, DuplicateOptions options) : base(db, r, options)
		{
			if (r.mConnectionGeometry != null)
				ConnectionGeometry = db.Factory.Duplicate(r.ConnectionGeometry) as IfcConnectionGeometry;
			//	RelatingElement = db.Factory.Duplicate(r.RelatingElement) as IfcElement; Handled at element
			//	RelatedElement = db.Factory.Duplicate( r.RelatedElement) as IfcElement; Handled at element
		}
		public IfcRelConnectsElements(IfcElement relating, IfcElement related) : base(relating.mDatabase)
		{
			RelatingElement = relating;
			RelatedElement = related;
		}
	}
	[Serializable]
	public partial class IfcRelConnectsPathElements : IfcRelConnectsElements
	{
		internal List<double> mRelatingPriorities = new List<double>();// : LIST [0:?] OF INTEGER;
		internal List<double> mRelatedPriorities = new List<double>();// : LIST [0:?] OF INTEGER;
		internal IfcConnectionTypeEnum mRelatedConnectionType = IfcConnectionTypeEnum.NOTDEFINED;// : IfcConnectionTypeEnum;
		internal IfcConnectionTypeEnum mRelatingConnectionType = IfcConnectionTypeEnum.NOTDEFINED;// : IfcConnectionTypeEnum; 

		internal IfcRelConnectsPathElements() : base() { }
		internal IfcRelConnectsPathElements(DatabaseIfc db, IfcRelConnectsPathElements r, DuplicateOptions options) : base(db, r, options)
		{
			mRelatingPriorities.AddRange(r.mRelatingPriorities);
			mRelatedPriorities.AddRange(r.mRelatedPriorities);
			mRelatedConnectionType = r.mRelatedConnectionType;
			mRelatingConnectionType = r.mRelatingConnectionType;
		}
	}
	[Serializable]
	public partial class IfcRelConnectsPorts : IfcRelConnects
	{
		internal IfcPort mRelatingPort;// : IfcPort;
		internal IfcPort mRelatedPort;// : IfcPort;
		internal IfcElement mRealizingElement;// : OPTIONAL IfcElement; 

		public IfcPort RelatingPort { get { return mRelatingPort; } set { mRelatingPort = value; value.mConnectedFrom = this; } }
		public IfcPort RelatedPort { get { return mRelatedPort; } set { mRelatedPort = value; value.mConnectedTo = this; } }
		public IfcElement RealizingElement { get { return mRealizingElement; } set { mRealizingElement = value; } }

		internal IfcRelConnectsPorts() : base() { }
		internal IfcRelConnectsPorts(DatabaseIfc db, IfcRelConnectsPorts r, DuplicateOptions options) : base(db, r, options)
		{
			RelatingPort = db.Factory.Duplicate(r.RelatingPort, options);
			RelatedPort = db.Factory.Duplicate(r.RelatedPort, options);
			if (r.mRealizingElement != null)
				RealizingElement = db.Factory.Duplicate(r.RealizingElement, options);
		}
		public IfcRelConnectsPorts(IfcPort relatingPort, IfcPort relatedPort) : base(relatingPort.mDatabase) { RelatingPort = relatingPort; RelatedPort = relatedPort; }

		public IfcPort ConnectedPort(IfcPort p) { return (mRelatedPort == p ? mRelatingPort : mRelatedPort); }
	}
	[Serializable]
	public partial class IfcRelConnectsPortToElement : IfcRelConnects
	{
		internal IfcPort mRelatingPort;// : IfcPort;
		internal IfcElement mRelatedElement;// : IfcElement; IFC4 IfcDistributionElement

		public IfcPort RelatingPort { get { return mRelatingPort; } set { mRelatingPort = value; value.mContainedIn = this; } }
		public IfcElement RelatedElement { get { return mRelatedElement; } set { mRelatedElement = value; value.HasPortsSS.Add(this); } }

		internal IfcRelConnectsPortToElement() : base() { }
		internal IfcRelConnectsPortToElement(DatabaseIfc db, IfcRelConnectsPortToElement r, DuplicateOptions options) : base(db, r, options)
		{
			RelatingPort = db.Factory.Duplicate(r.RelatingPort, options);
			RelatedElement = db.Factory.Duplicate(r.RelatedElement, options);
		}
		public IfcRelConnectsPortToElement(IfcPort p, IfcElement e) : base(p.mDatabase)
		{
			RelatingPort = p;
			RelatedElement = e;
		}

	}
	[Serializable]
	public partial class IfcRelConnectsStructuralActivity : IfcRelConnects
	{
		private IfcStructuralActivityAssignmentSelect mRelatingElement;// : IfcStructuralActivityAssignmentSelect; SELECT(IfcStructuralItem,IfcElement);
		private IfcStructuralActivity mRelatedStructuralActivity;// : IfcStructuralActivity; 

		public IfcStructuralActivityAssignmentSelect RelatingElement { get { return mRelatingElement; } set { mRelatingElement = value; value.AssignStructuralActivity(this); } }
		public IfcStructuralActivity RelatedStructuralActivity { get { return mRelatedStructuralActivity; } set { mRelatedStructuralActivity = value; mRelatedStructuralActivity.AssignedToStructuralItem = this;  } }

		internal IfcRelConnectsStructuralActivity() : base() { }
		internal IfcRelConnectsStructuralActivity(DatabaseIfc db, IfcRelConnectsStructuralActivity c, DuplicateOptions options) : base(db, c, options)
		{
			RelatedStructuralActivity = db.Factory.Duplicate(c.RelatedStructuralActivity, options) as IfcStructuralActivity;
		}
		public IfcRelConnectsStructuralActivity(IfcStructuralActivityAssignmentSelect item, IfcStructuralActivity a)
			: base(a.mDatabase) { RelatingElement = item; RelatedStructuralActivity = a; }
	}
	[Serializable]
	public partial class IfcRelConnectsStructuralElement : IfcRelConnects //DELETED IFC4 Replaced by IfcRelAssignsToProduct
	{
		internal IfcElement mRelatingElement;// : IfcElement;
		internal IfcStructuralMember mRelatedStructuralMember;// : IfcStructuralMember; 

		public IfcElement RelatingElement { get { return mRelatingElement as IfcElement; } set { mRelatingElement = value; value.mHasStructuralMember.Add(this); } }
		public IfcStructuralMember RelatedStructuralMember { get { return mRelatedStructuralMember as IfcStructuralMember; } set { mRelatedStructuralMember = value; value.mStructuralMemberFor = this; } }

		internal IfcRelConnectsStructuralElement() : base() { }
		internal IfcRelConnectsStructuralElement(DatabaseIfc db, IfcRelConnectsStructuralElement c, DuplicateOptions options) : base(db, c, options)
		{
			RelatingElement = db.Factory.Duplicate(c.RelatingElement, options) as IfcElement;
			RelatedStructuralMember = db.Factory.Duplicate(c.RelatedStructuralMember, options) as IfcStructuralMember;
		}
		public IfcRelConnectsStructuralElement(IfcElement elem, IfcStructuralMember memb) : base(elem.mDatabase)
		{
			if (elem.mDatabase.mRelease != ReleaseVersion.IFC2x3)
				throw new Exception(StepClassName + " Deleted IFC4!");
			RelatingElement = elem;
			RelatedStructuralMember = memb;
		}
	}
	[Serializable]
	public partial class IfcRelConnectsStructuralMember : IfcRelConnects
	{
		internal IfcStructuralMember mRelatingStructuralMember;// : IfcStructuralMember;
		internal IfcStructuralConnection mRelatedStructuralConnection;// : IfcStructuralConnection;
		internal IfcBoundaryCondition mAppliedCondition;// : OPTIONAL IfcBoundaryCondition;
		internal IfcStructuralConnectionCondition mAdditionalConditions;// : OPTIONAL IfcStructuralConnectionCondition;
		private double mSupportedLength;// : OPTIONAL IfcLengthMeasure;
		internal IfcAxis2Placement3D mConditionCoordinateSystem; // : OPTIONAL IfcAxis2Placement3D; 

		public IfcStructuralMember RelatingStructuralMember { get { return mRelatingStructuralMember; } set { mRelatingStructuralMember = value; value.mConnectedBy.Add(this); } }
		public IfcStructuralConnection RelatedStructuralConnection { get { return mRelatedStructuralConnection; } set { mRelatedStructuralConnection = value; value.mConnectsStructuralMembers.Add(this); } }
		public IfcBoundaryCondition AppliedCondition { get { return mAppliedCondition; } set { mAppliedCondition = value; } }
		public IfcStructuralConnectionCondition AdditionalConditions { get { return mAdditionalConditions; } set { mAdditionalConditions = value; } }
		public double SupportedLength { get { return mSupportedLength; } set { mSupportedLength = value; } }
		public IfcAxis2Placement3D ConditionCoordinateSystem { get { return mConditionCoordinateSystem; } set { mConditionCoordinateSystem = value; } }

		internal IfcRelConnectsStructuralMember() : base() { }
		internal IfcRelConnectsStructuralMember(DatabaseIfc db, IfcRelConnectsStructuralMember r, DuplicateOptions options) : base(db, r, options)
		{
			RelatingStructuralMember = db.Factory.Duplicate(r.RelatingStructuralMember); 
			RelatedStructuralConnection = db.Factory.Duplicate(r.RelatedStructuralConnection);
			if (r.mAppliedCondition != null)
				AppliedCondition = db.Factory.Duplicate(r.AppliedCondition);
			if (r.mAdditionalConditions != null)
				AdditionalConditions = db.Factory.Duplicate(r.AdditionalConditions);
			mSupportedLength = r.mSupportedLength;
			if (r.mConditionCoordinateSystem != null)
				ConditionCoordinateSystem = db.Factory.Duplicate(r.ConditionCoordinateSystem);
		}
		public IfcRelConnectsStructuralMember(IfcStructuralMember member, IfcStructuralConnection connection) : base(member.mDatabase)
		{
			RelatingStructuralMember = member;
			RelatedStructuralConnection = connection;
		}

		public static IfcRelConnectsStructuralMember Create(IfcStructuralCurveMember member, IfcStructuralPointConnection point, bool atStart, IfcStructuralCurveMember.ExtremityAttributes atts)
		{
			string desc = atStart ? "Start" : "End";
			if (atts == null)
				return new IfcRelConnectsStructuralMember(member, point) { Description = desc };
			double tol = member.mDatabase.Tolerance;
			if (atts.Eccentricity != null && (Math.Abs(atts.Eccentricity.Item1) > tol || Math.Abs(atts.Eccentricity.Item2) > tol || Math.Abs(atts.Eccentricity.Item3) > tol))
				return new IfcRelConnectsWithEccentricity(member, point, new IfcConnectionPointEccentricity(point.Vertex, atts.Eccentricity.Item1, atts.Eccentricity.Item2, atts.Eccentricity.Item3)) { Description = desc, AppliedCondition = atts.BoundaryCondition, AdditionalConditions = atts.StructuralConnectionCondition, SupportedLength = atts.SupportedLength, ConditionCoordinateSystem = atts.ConditionCoordinateSystem };
			return new IfcRelConnectsStructuralMember(member, point) { Description = desc, AppliedCondition = atts.BoundaryCondition, AdditionalConditions = atts.StructuralConnectionCondition, SupportedLength = atts.SupportedLength, ConditionCoordinateSystem = atts.ConditionCoordinateSystem };
		}
	}
	public partial class IfcRelConnectsWithEccentricity : IfcRelConnectsStructuralMember
	{
		internal IfcConnectionGeometry mConnectionConstraint;// : IfcConnectionGeometry
		public IfcConnectionGeometry ConnectionConstraint { get { return mConnectionConstraint; } set { mConnectionConstraint = value; } }

		internal IfcRelConnectsWithEccentricity() : base() { }
		internal IfcRelConnectsWithEccentricity(DatabaseIfc db, IfcRelConnectsWithEccentricity c, DuplicateOptions options) : base(db, c, options) { ConnectionConstraint = db.Factory.Duplicate(c.ConnectionConstraint) as IfcConnectionGeometry; }
		public IfcRelConnectsWithEccentricity(IfcStructuralMember memb, IfcStructuralConnection connection, IfcConnectionGeometry cg)
			: base(memb, connection) { mConnectionConstraint = cg; }
	}
	public partial class IfcRelConnectsWithRealizingElements : IfcRelConnectsElements
	{
		internal SET<IfcElement> mRealizingElements = new SET<IfcElement>();// :	SET [1:?] OF IfcElement;
		internal string mConnectionType = "";// : :	OPTIONAL IfcLabel; 

		public SET<IfcElement> RealizingElements { get { return mRealizingElements; } }

		internal IfcRelConnectsWithRealizingElements() : base() { }
		internal IfcRelConnectsWithRealizingElements(DatabaseIfc db, IfcRelConnectsWithRealizingElements r, DuplicateOptions options) : base(db, r, options) { RealizingElements.AddRange(r.RealizingElements.Select(x => db.Factory.Duplicate(x, options) as IfcElement)); mConnectionType = r.mConnectionType; }
		public IfcRelConnectsWithRealizingElements(IfcConnectionGeometry cg, IfcElement relating, IfcElement related, IfcElement realizing)
			: base(relating, related)
		{
			ConnectionGeometry = cg;
			RealizingElements.Add(realizing);
		}

		protected override void initialize()
		{
			base.initialize();
			mRealizingElements.CollectionChanged += mRealizingElements_CollectionChanged;
		}
		private void mRealizingElements_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (mDatabase != null && mDatabase.IsDisposed())
				return;
			if (e.NewItems != null)
			{
				foreach (IfcElement o in e.NewItems)
				{
					if (!o.IsConnectionRealization.Contains(this))
						o.IsConnectionRealization.Add(this);
				}
			}
			if (e.OldItems != null)
			{
				foreach (IfcElement o in e.NewItems)
					o.IsConnectionRealization.Remove(this);
			}
		}
	}
	[Serializable]
	public partial class IfcRelContainedInSpatialStructure : IfcRelConnects
	{
		internal SET<IfcProduct> mRelatedElements = new SET<IfcProduct>();// : SET [1:?] OF IfcProduct;
		private IfcSpatialElement mRelatingStructure = null;//  IfcSpatialElement 

		public SET<IfcProduct> RelatedElements { get { return mRelatedElements; } }
		public IfcSpatialElement RelatingStructure
		{
			get { return mRelatingStructure; }
			set
			{
				if (mRelatingStructure != value)
				{
					mRelatingStructure = value;
					if (value != null)
						value.mContainsElements.Add(this);
				}
			}
		}

		internal IfcRelContainedInSpatialStructure() : base() { }// RelatedElements = new RelatedElementsCollection(this); }
		internal IfcRelContainedInSpatialStructure(DatabaseIfc db, IfcRelContainedInSpatialStructure r, DuplicateOptions options) 
			: base(db, r, options)
		{
			DuplicateOptions relatingOptions = new DuplicateOptions(options) { DuplicateDownstream = options.mSpatialElementsToDuplicate.Contains(r.RelatingStructure) };
			RelatingStructure = db.Factory.Duplicate(r.RelatingStructure, relatingOptions);
			if (options.DuplicateDownstream)
			{
				DuplicateOptions optionsNoHost = new DuplicateOptions(options) { DuplicateHost = false };
				RelatedElements.AddRange(r.RelatedElements.Select(x => db.Factory.Duplicate(x, optionsNoHost)));
			}
		}
		public IfcRelContainedInSpatialStructure(IfcSpatialElement host) : base(host.mDatabase)
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
			RelatingStructure = host;
			GlobalId = ParserIfc.HashGlobalID(host.GlobalId + "Contains" + host.ContainsElements.Count);
		}
		public IfcRelContainedInSpatialStructure(IfcProduct related, IfcSpatialElement host) : this(host) { RelatedElements.Add(related); }
		public IfcRelContainedInSpatialStructure(IEnumerable<IfcProduct> related, IfcSpatialElement host) :this(host) { RelatedElements.AddRange(related); }
		protected override void initialize()
		{
			base.initialize();
			mRelatedElements.CollectionChanged += mRelatedElements_CollectionChanged;
		}
		private void mRelatedElements_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (mDatabase != null && mDatabase.IsDisposed())
				return;
			if (e.NewItems != null)
			{
				foreach (IfcProduct p in e.NewItems)
					relate(p);	
			}
			if (e.OldItems != null)
			{
				foreach (IfcProduct p in e.OldItems)
				{
					removeObject(p);
				}
			}
		}
		private void relate(IfcProduct product)
		{
			if (product.mContainedInStructure != this)
			{
				if (product.mContainedInStructure != null)
					product.mContainedInStructure.removeObject(product);
				product.mContainedInStructure = this;
			}
		}
		private void removeObject(IfcProduct product)
		{
			IfcElement element = product as IfcElement;
			if(element != null)
			{
				if (element.mContainedInStructure == this)
					element.mContainedInStructure = null;
				return;
			}
			IfcGrid grid = product as IfcGrid;
			if (grid != null)
			{
				if (grid.mContainedInStructure == this)
					grid.mContainedInStructure = null;
				return;
			}
			IfcAnnotation annotation = product as IfcAnnotation;
			if(annotation != null)
			{
				if (annotation.mContainedInStructure == this)
					annotation.mContainedInStructure = null;
				return;
			}
			IfcPositioningElement positioningElement = product as IfcPositioningElement;
			if (positioningElement != null)
			{
				if (positioningElement.mContainedInStructure == this)
					positioningElement.mContainedInStructure = null;
				return;
			}
		}
	}
	[Serializable]
	public partial class IfcRelCoversBldgElements : IfcRelConnects //IFC4 DEPRECATION  The relationship IfcRelCoversBldgElements shall not be used anymore, use IfcRelAggregates instead.
	{
		internal IfcElement mRelatingBuildingElement;// :	IfcElement;  
		private SET<IfcCovering> mRelatedCoverings = new SET<IfcCovering>();// : SET [1:?] OF IfcCovering;

		public IfcElement RelatingBuildingElement { get { return mRelatingBuildingElement; } set { mRelatingBuildingElement = value; value.HasCoverings.Add(this); } }
		public SET<IfcCovering> RelatedCoverings { get { return mRelatedCoverings; } }

		internal IfcRelCoversBldgElements() : base() { }
		internal IfcRelCoversBldgElements(DatabaseIfc db, IfcRelCoversBldgElements c, DuplicateOptions options) : base(db, c, options) { RelatingBuildingElement = db.Factory.Duplicate(c.RelatingBuildingElement, options) as IfcElement; c.RelatedCoverings.ToList().ForEach(x => addCovering(db.Factory.Duplicate(x, options) as IfcCovering)); }
		public IfcRelCoversBldgElements(IfcElement e, IfcCovering covering) : base(e.mDatabase)
		{
			mRelatingBuildingElement = e;
			e.mHasCoverings.Add(this);
			if (covering != null)
			{
				mRelatedCoverings.Add(covering);
				covering.mCoversElements = this;
			}
		}
		internal IfcRelCoversBldgElements(IfcElement e, List<IfcCovering> coverings) : base(e.mDatabase)
		{
			mRelatingBuildingElement = e;
			e.mHasCoverings.Add(this);
			for (int icounter = 0; icounter < coverings.Count; icounter++)
			{
				mRelatedCoverings.Add(coverings[icounter]);
				coverings[icounter].mCoversElements = this;
			}
		}

		internal void Remove(IfcCovering c) { mRelatedCoverings.Remove(c); c.mHasCoverings.Remove(this); }
		internal void addCovering(IfcCovering c) { c.mCoversElements = this; mRelatedCoverings.Add(c); }
	}
	[Serializable]
	public partial class IfcRelCoversSpaces : IfcRelConnects //IFC4 DEPRECATION  The relationship IfcRelCoversSpace shall not be used anymore, use IfcRelContainedInSpatialStructure instead.
	{
		internal IfcSpace mRelatedSpace;// : IfcSpace;
		internal SET<IfcCovering> mRelatedCoverings = new SET<IfcCovering>();// SET [1:?] OF IfcCovering; 

		public IfcSpace RelatedSpace { get { return mRelatedSpace; } set { mRelatedSpace = value; value.mHasCoverings.Add(this); } }
		public SET<IfcCovering> RelatedCoverings { get { return mRelatedCoverings; } }

		internal IfcRelCoversSpaces() : base() { }
		internal IfcRelCoversSpaces(DatabaseIfc db, IfcRelCoversSpaces r, DuplicateOptions options) : base(db, r, options)
		{
			RelatedSpace = db.Factory.Duplicate(r.RelatedSpace, options) as IfcSpace;
			RelatedCoverings.AddRange( r.RelatedCoverings.ConvertAll(x => db.Factory.Duplicate(x, options) as IfcCovering));
		}
		public IfcRelCoversSpaces(IfcSpace s, IfcCovering covering) : base(s.mDatabase)
		{
			RelatedSpace = s;
			if (covering != null)
				RelatedCoverings.Add(covering);
		}

		protected override void initialize()
		{
			base.initialize();
			mRelatedCoverings.CollectionChanged += mRelatedCoverings_CollectionChanged;
		}
		private void mRelatedCoverings_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (mDatabase != null && mDatabase.IsDisposed())
				return;
			if (e.NewItems != null)
			{
				foreach (IfcCovering c in e.NewItems)
				{
					c.CoversSpaces = this;
				}
			}
			if (e.OldItems != null)
			{
				foreach (IfcCovering c in e.OldItems)
					c.CoversSpaces = null;	
			}
		}
	}
	[Serializable]
	public partial class IfcRelDeclares : IfcRelationship //IFC4
	{
		private IfcContext mRelatingContext = null;// : 	IfcContext;
		private SET<IfcDefinitionSelect> mRelatedDefinitions = new SET<IfcDefinitionSelect>();// :	SET [1:?] OF IfcDefinitionSelect; 

		public IfcContext RelatingContext { get { return mRelatingContext; } set { mRelatingContext = value; if (!value.mDeclares.Contains(this)) value.mDeclares.Add(this); } }
		public SET<IfcDefinitionSelect> RelatedDefinitions { get { return mRelatedDefinitions; } }

		internal IfcRelDeclares() : base() { }
		internal IfcRelDeclares(IfcContext c) : base(c.mDatabase) { mRelatingContext = c; c.mDeclares.Add(this); }
		internal IfcRelDeclares(DatabaseIfc db, IfcRelDeclares r, DuplicateOptions options) : base(db, r, options)
		{
			RelatingContext = db.Factory.Duplicate(r.RelatingContext, new DuplicateOptions(options) { DuplicateDownstream = false });
			if (options.DuplicateDownstream)
				RelatedDefinitions.AddRange(r.RelatedDefinitions.ConvertAll(x => db.Factory.Duplicate<IfcDefinitionSelect>(x, options)));
		}
		public IfcRelDeclares(IfcContext c, IfcDefinitionSelect def) : this(c) { RelatedDefinitions.Add(def); }
		public IfcRelDeclares(IfcContext c, IEnumerable<IfcDefinitionSelect> defs) : this(c) { RelatedDefinitions.AddRange(defs); }

		protected override void initialize()
		{
			base.initialize();

			mRelatedDefinitions.CollectionChanged += mRelatedDefinitions_CollectionChanged;
		}
		private void mRelatedDefinitions_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			if (mDatabase != null && mDatabase.IsDisposed())
				return;
			if (e.NewItems != null)
			{
				foreach (IfcDefinitionSelect d in e.NewItems)
					d.HasContext = this;
			}
			if (e.OldItems != null)
			{
				foreach (IfcDefinitionSelect d in e.OldItems)
					d.HasContext = null;
			}
		}
	}
	[Serializable]
	public abstract partial class IfcRelDecomposes : IfcRelationship //ABSTACT  SUPERTYPE OF (ONEOF (IfcRelAggregates ,IfcRelNests ,IfcRelProjectsElement ,IfcRelVoidsElement))
	{
		protected IfcRelDecomposes() : base() { }
		protected IfcRelDecomposes(DatabaseIfc db) : base(db) { }
		protected IfcRelDecomposes(DatabaseIfc db, IfcRelDecomposes d, DuplicateOptions options) : base(db, d, options) { }
	}
	[Serializable]
	public abstract partial class IfcRelDefines : IfcRelationship // 	ABSTRACT SUPERTYPE OF(ONEOF(IfcRelDefinesByObject, IfcRelDefinesByProperties, IfcRelDefinesByTemplate, IfcRelDefinesByType))
	{
		public abstract IfcRoot Relating();

		protected IfcRelDefines() : base() { }
		protected IfcRelDefines(DatabaseIfc db) : base(db) { }
		protected IfcRelDefines(DatabaseIfc db, IfcRelDefines d, DuplicateOptions options) : base(db, d, options) { }
	}
	[Serializable]
	public partial class IfcRelDefinesByObject : IfcRelDefines
	{
		internal SET<IfcObject> mRelatedObjects = new SET<IfcObject>();// : SET [1:?] OF IfcObject;
		internal IfcObject mRelatingObject;// : IfcObject  

		public SET<IfcObject> RelatedObjects { get { return mRelatedObjects; } }
		public IfcObject RelatingObject { get { return mRelatingObject; } set { mRelatingObject = value; if (value != null) { value.mIsDeclaredBy = this; } } }

		public override IfcRoot Relating() { return RelatingObject; } 

		internal IfcRelDefinesByObject() : base() { }
		internal IfcRelDefinesByObject(DatabaseIfc db, IfcRelDefinesByObject r, DuplicateOptions options) : base(db, r, options)
		{ 
			RelatedObjects.AddRange(r.RelatedObjects.Select(x=> db.Factory.Duplicate(x, options) as IfcObject)); 
			RelatingObject = db.Factory.Duplicate(r.RelatingObject, options) as IfcObject; 
		}
		public IfcRelDefinesByObject(IfcObject relObj) : base(relObj.mDatabase) { RelatingObject = relObj; }
	}
	[Serializable]
	public partial class IfcRelDefinesByProperties : IfcRelDefines
	{
		private SET<IfcObjectDefinition> mRelatedObjects = new SET<IfcObjectDefinition>();// IFC4 change SET [1:?] OF IfcObjectDefinition; ifc2x3 : SET [1:?] OF IfcObject;  
		private SET<IfcPropertySetDefinition> mRelatingPropertyDefinition = new SET<IfcPropertySetDefinition>();// : IfcPropertySetDefinitionSelect; 

		public SET<IfcObjectDefinition> RelatedObjects { get { return mRelatedObjects; } }
		public SET<IfcPropertySetDefinition> RelatingPropertyDefinition { get { return mRelatingPropertyDefinition; } }

		public override IfcRoot Relating() { return RelatingPropertyDefinition.First() as IfcRoot; } 

		internal IfcRelDefinesByProperties() : base() { }
		private IfcRelDefinesByProperties(DatabaseIfc db) : base(db) 
		{
			Name = "NameRelDefinesByProperties";
			Description = "DescriptionRelDefinesByProperties";
		}
		internal IfcRelDefinesByProperties(DatabaseIfc db, IfcRelDefinesByProperties d, DuplicateOptions options) : base(db, d, options)
		{
			RelatingPropertyDefinition.AddRange(d.RelatingPropertyDefinition.Select(x=> db.Factory.Duplicate(x as BaseClassIfc, options) as IfcPropertySetDefinition));
		}
		public IfcRelDefinesByProperties(IfcPropertySetDefinition propertySet) 
			: base(propertySet.Database) 
		{
			mRelatingPropertyDefinition.Add(propertySet); 
			propertySet.DefinesOccurrence.Add(this); 
		}
		public IfcRelDefinesByProperties(IfcObjectDefinition relatedObject, IfcPropertySetDefinition propertySet)
			: this(propertySet) { RelatedObjects.Add(relatedObject); }
		public IfcRelDefinesByProperties(IEnumerable<IfcObjectDefinition> relatedObjects, IfcPropertySetDefinition propertySet) 
			: this(propertySet) { RelatedObjects.AddRange(relatedObjects); }

		protected override void initialize()
		{
			base.initialize();
			mRelatedObjects.CollectionChanged += mRelatedObjects_CollectionChanged;
			mRelatingPropertyDefinition.CollectionChanged += mRelatingPropertyDefinition_CollectionChanged;
		}

		protected virtual void mRelatedObjects_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (mDatabase != null && mDatabase.IsDisposed())
				return;
			if (e.NewItems != null)
			{
				foreach (IfcObjectDefinition o in e.NewItems)
				{
					if (o != null)
					{
						IfcContext context = o as IfcContext;
						if (context != null)
						{
							if (!context.mIsDefinedBy.Contains(this))
								context.mIsDefinedBy.Add(this);
						}
						else
						{
							IfcObject obj = o as IfcObject;
							if (obj != null)
							{
								if (!obj.mIsDefinedBy.Contains(this))
									obj.mIsDefinedBy.Add(this);
							}
						}
					}
				}
			}
			if (e.OldItems != null)
			{
				foreach (IfcObjectDefinition o in e.OldItems)
				{
					if (o != null)
					{
						IfcContext context = o as IfcContext;
						if (context != null)
							context.mIsDefinedBy.Remove(this);
						else
						{
							IfcObject obj = o as IfcObject;
							if (obj != null)
								obj.mIsDefinedBy.Remove(this);
						}
					}	
				}
			}
		}
		protected virtual void mRelatingPropertyDefinition_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (mDatabase != null && mDatabase.IsDisposed())
				return;
			if (e.NewItems != null)
			{
				foreach (IfcPropertySetDefinition o in e.NewItems)
				{
					if (o != null)
					{
						o.DefinesOccurrence.Add(this);
					}
				}
			}
			if (e.OldItems != null)
			{
				foreach (IfcPropertySetDefinition o in e.OldItems)
				{
					if (o != null)
					{
						o.DefinesOccurrence.Remove(this);
					}
				}
			}
		}
		protected override List<T> Extract<T>(Type type)
		{
			List<T> result = base.Extract<T>(type);
			foreach(IfcPropertySetDefinition pset in RelatingPropertyDefinition)
				result.AddRange(pset.Extract<T>());
			return result;
		}
	}
	[Serializable]
	public partial class IfcRelDefinesByTemplate : IfcRelDefines //IFC4
	{
		internal SET<IfcPropertySetDefinition> mRelatedPropertySets = new SET<IfcPropertySetDefinition>();// : SET [1:?] OF IfcPropertySetDefinition;
		internal IfcPropertySetTemplate mRelatingTemplate;// :	IfcPropertySetTemplate;

		public SET<IfcPropertySetDefinition> RelatedPropertySets { get { return mRelatedPropertySets; } }
		public IfcPropertySetTemplate RelatingTemplate
		{
			get { return mRelatingTemplate; }
			set { mRelatingTemplate = value; }
		}

		public override IfcRoot Relating() { return RelatingTemplate; } 

		internal IfcRelDefinesByTemplate() : base() { }
		internal IfcRelDefinesByTemplate(DatabaseIfc db, IfcRelDefinesByTemplate r, DuplicateOptions options) : base(db, r, options)
		{ 
			RelatedPropertySets.AddRange(r.RelatedPropertySets.Select(x => db.Factory.Duplicate(x, options)));
			RelatingTemplate = db.Factory.Duplicate(r.RelatingTemplate, options); 
		}
		public IfcRelDefinesByTemplate(IfcPropertySetTemplate relating) : base(relating.mDatabase) { RelatingTemplate = relating; }
		public IfcRelDefinesByTemplate(IfcPropertySetDefinition related, IfcPropertySetTemplate relating) : this(relating) { RelatedPropertySets.Add(related); }
		public IfcRelDefinesByTemplate(List<IfcPropertySetDefinition> related, IfcPropertySetTemplate relating) : this(relating) { RelatedPropertySets.AddRange(related); }
	}
	[Serializable]
	public partial class IfcRelDefinesByType : IfcRelDefines
	{
		internal SET<IfcObject> mRelatedObjects = new SET<IfcObject>();// : SET [1:?] OF IfcObject;
		private IfcTypeObject mRelatingType = null;// : IfcTypeObject  

		public SET<IfcObject> RelatedObjects { get { return mRelatedObjects; } }
		public IfcTypeObject RelatingType { get { return mRelatingType; } set { mRelatingType = value; if(value != null) value.mTypes = this; } }

		public override IfcRoot Relating() { return RelatingType; } 

		internal IfcRelDefinesByType() : base() { }
		internal IfcRelDefinesByType(DatabaseIfc db, IfcRelDefinesByType r, DuplicateOptions options) : base(db, r, options)
		{
			RelatingType = db.Factory.Duplicate(r.RelatingType, options) as IfcTypeObject;
		}
		public IfcRelDefinesByType(IfcTypeObject relType) : base(relType.mDatabase) { RelatingType = relType; }
		public IfcRelDefinesByType(IfcObject related, IfcTypeObject relating) : this(relating) { RelatedObjects.Add(related); }
		public IfcRelDefinesByType(List<IfcObject> related, IfcTypeObject relating) : this(relating) { RelatedObjects.AddRange(related); }
		protected override void initialize()
		{
			base.initialize();
			mRelatedObjects.CollectionChanged += mRelatedObjects_CollectionChanged;
		}
		private void mRelatedObjects_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			if (mDatabase != null && mDatabase.IsDisposed())
				return;
			if (e.NewItems != null)
			{
				foreach (IfcObject o in e.NewItems)
				{
					if (o.mIsTypedBy != this)
						o.mIsTypedBy = this;
				}
			}
			if (e.OldItems != null)
			{
				foreach (IfcObject o in e.OldItems)
					o.mIsTypedBy = null;
			}
		}
	}
	[Serializable]
	public partial class IfcRelFillsElement : IfcRelConnects
	{
		private IfcOpeningElement mRelatingOpeningElement;// : IfcOpeningElement;
		private IfcElement mRelatedBuildingElement;// :OPTIONAL IfcElement; 

		public IfcOpeningElement RelatingOpeningElement { get { return mRelatingOpeningElement; } set { mRelatingOpeningElement = value; value.mHasFillings.Add(this); } }
		public IfcElement RelatedBuildingElement { get { return mRelatedBuildingElement; } set { mRelatedBuildingElement = value; value.mFillsVoids = this; } }

		internal IfcRelFillsElement() : base() { }
		internal IfcRelFillsElement(DatabaseIfc db, IfcRelFillsElement r, DuplicateOptions options) : base(db, r, options) { RelatingOpeningElement = db.Factory.Duplicate(r.RelatingOpeningElement, options); RelatedBuildingElement = db.Factory.Duplicate(r.RelatedBuildingElement, options); }
		public IfcRelFillsElement(IfcOpeningElement oe, IfcElement e) : base(oe.mDatabase) { RelatingOpeningElement = oe; RelatedBuildingElement = e; }
	}
	[Serializable]
	public partial class IfcRelFlowControlElements : IfcRelConnects
	{
		internal IfcPort mRelatingPort;// : IfcPort;
		internal IfcElement mRelatedElement;// : IfcElement; 
		public IfcPort RelatingPort { get { return mRelatingPort; } set { mRelatingPort = value; } }
		public IfcElement RelatedElement { get { return mRelatedElement; } set { mRelatedElement = value; } }

		internal IfcRelFlowControlElements() : base() { }
		internal IfcRelFlowControlElements(DatabaseIfc db, IfcRelFlowControlElements r, DuplicateOptions options) : base(db, r, options) { RelatingPort = db.Factory.Duplicate(r.RelatingPort, options) as IfcPort; RelatedElement = db.Factory.Duplicate(r.RelatedElement, options) as IfcElement; }
	}
	//[Obsolete("DEPRECATED IFC4", false)]
	//ENTITY IfcRelInteractionRequirements  // DEPRECATED IFC4
	[Serializable]
	public partial class IfcRelInterferesElements : IfcRelConnects
	{
		internal IfcInterferenceSelect mRelatingElement;// : IfcInterferenceSelect;
		internal IfcInterferenceSelect mRelatedElement;// : IfcInterferenceSelect;
		internal IfcConnectionGeometry mInterferenceGeometry;// : OPTIONAL IfcConnectionGeometry; 
		internal string mInterferenceType = "";// : OPTIONAL IfcIdentifier;
		internal IfcLogicalEnum mImpliedOrder = IfcLogicalEnum.UNKNOWN;// : LOGICAL;
		internal IfcSpatialZone mInterferenceSpace = null;// : OPTIONAL IfcSpatialZone;

		public IfcInterferenceSelect RelatingElement { get { return mRelatingElement; } set { mRelatingElement = value; value.InterferesElements.Add(this); } }
		public IfcInterferenceSelect RelatedElement { get { return mRelatedElement; } set { mRelatedElement = value; value.IsInterferedByElements.Add(this); } }
		public IfcConnectionGeometry InterferenceGeometry { get { return mInterferenceGeometry; } set { mInterferenceGeometry = value; } }
		public string InterferenceType { get { return mInterferenceType; } set { mInterferenceType = value; } }
		public IfcLogicalEnum ImpliedOrder { get { return mImpliedOrder; } }
		public IfcSpatialZone InterferenceSpace { get { return mInterferenceSpace; } set { mInterferenceSpace = value; } }

		internal IfcRelInterferesElements() : base() { }
		internal IfcRelInterferesElements(DatabaseIfc db, IfcRelInterferesElements r, DuplicateOptions options) : base(db, r, options)
		{
			RelatingElement = db.Factory.Duplicate<IfcInterferenceSelect>(r.RelatingElement, options);
			RelatedElement = db.Factory.Duplicate<IfcInterferenceSelect>(r.RelatedElement, options);
			if (r.mInterferenceGeometry != null)
				InterferenceGeometry = db.Factory.Duplicate(r.InterferenceGeometry, options);
			if (r.InterferenceSpace != null)
				InterferenceSpace = db.Factory.Duplicate(r.InterferenceSpace, options);
			mInterferenceType = r.mInterferenceType;
			mImpliedOrder = r.mImpliedOrder;
		}
		public IfcRelInterferesElements(IfcInterferenceSelect relatingElement, IfcInterferenceSelect relatedElement)
			: base(relatingElement.Database) { RelatingElement = relatingElement; RelatedElement = relatedElement; }
	}
	[Serializable]
	public partial class IfcRelNests : IfcRelDecomposes
	{
		internal IfcObjectDefinition mRelatingObject;// : IfcObjectDefinition 
		internal LIST<IfcObjectDefinition> mRelatedObjects = new LIST<IfcObjectDefinition>();// : SET [1:?] OF IfcObjectDefinition; 

		public IfcObjectDefinition RelatingObject { get { return mRelatingObject; } set { mRelatingObject = value; if (value != null && !value.IsNestedBy.Contains(this)) value.IsNestedBy.Add(this); } }
		public LIST<IfcObjectDefinition> RelatedObjects { get { return mRelatedObjects; } }

		internal IfcRelNests() : base() { }
		internal IfcRelNests(DatabaseIfc db, IfcRelNests n, DuplicateOptions options) : base(db, n, options)
		{
			RelatingObject = db.Factory.Duplicate(n.RelatingObject, options) as IfcObjectDefinition;
			DuplicateOptions optionsNoHost = new DuplicateOptions(options) { DuplicateHost = false };
			foreach(IfcObjectDefinition od in n.RelatedObjects)
			{
				IfcObjectDefinition duplicate = db.Factory.Duplicate(od, optionsNoHost);
				RelatedObjects.Add(duplicate);
			}
		}
		public IfcRelNests(IfcObjectDefinition relatingObject) : base(relatingObject.mDatabase)
		{
			mRelatingObject = relatingObject;
			relatingObject.IsNestedBy.Add(this);
			GlobalId = ParserIfc.HashGlobalID(relatingObject.GlobalId + "Nests" + relatingObject.IsNestedBy.Count);
		}
		public IfcRelNests(IfcObjectDefinition relatingObject, IfcObjectDefinition relatedObject) : this(relatingObject)
		{
			relatedObject.Nests = this;
		}
		public IfcRelNests(IfcObjectDefinition relatingObject, params IfcObjectDefinition[] relatedObjects)
			: this(relatingObject)
		{
			foreach (IfcObjectDefinition related in relatedObjects)
				related.Nests = this;
		}
		public IfcRelNests(IfcObjectDefinition relatingObject, IEnumerable<IfcObjectDefinition> relatedObjects) : base(relatingObject.mDatabase)
		{
			mRelatingObject = relatingObject;
			relatingObject.IsNestedBy.Add(this);
			foreach (IfcObjectDefinition od in relatedObjects)
				od.Nests = this;
		}

		protected override void initialize()
		{
			base.initialize();
			mRelatedObjects.CollectionChanged += mRelatedObjects_CollectionChanged;
		}
		private void mRelatedObjects_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			if (mDatabase != null && mDatabase.IsDisposed())
				return;
			if (e.OldItems != null)
			{
				foreach (IfcObjectDefinition o in e.OldItems)
				{
					if (o.Nests == this)
						o.mNests = null;
				}
			}
			if (e.NewItems != null)
			{
				foreach (IfcObjectDefinition o in e.NewItems)
				{
					if (o.Nests != this)
						o.mNests = this;
				}
			}
		}
	}
	//[Obsolete("DEPRECATED IFC4", false)]
	//ENTITY IfcRelOccupiesSpaces // DEPRECATED IFC4
	[Obsolete("DEPRECATED IFC4", false)]
	public partial class IfcRelOverridesProperties : IfcRelDefinesByProperties // DEPRECATED IFC4
	{
		public override string StepClassName { get { return (mDatabase.mRelease <= ReleaseVersion.IFC2x3 ? base.StepClassName : "IFCRELOVERRIDESPROPERTIES"); } }
		private SET<IfcProperty> mOverridingProperties = new SET<IfcProperty>();// : 	SET [1:?] OF IfcProperty;
		public SET<IfcProperty> OverridingProperties { get { return mOverridingProperties; } }

		internal IfcRelOverridesProperties() : base() { }
		internal IfcRelOverridesProperties(DatabaseIfc db, IfcRelOverridesProperties d, DuplicateOptions options) : base(db, d, options)
		{
			mOverridingProperties.AddRange(d.OverridingProperties.Select(x => db.Factory.Duplicate(x, options)));
		}
		internal IfcRelOverridesProperties(IfcPropertySetDefinition ifcproperty) : base(ifcproperty) { }
		public IfcRelOverridesProperties(IfcObjectDefinition od, IfcPropertySetDefinition ifcproperty) : base(od, ifcproperty) { }
		public IfcRelOverridesProperties(List<IfcObjectDefinition> objs, IfcPropertySetDefinition ifcproperty) : base(objs, ifcproperty) { }
	}
	[Serializable, VersionAdded(ReleaseVersion.IFC4X2)]
	public partial class IfcRelPositions : IfcRelConnects
	{
		private IfcPositioningElement mRelatingPositioningElement = null; //: IfcPositioningElement;
		private SET<IfcProduct> mRelatedProducts = new SET<IfcProduct>(); //: SET[1:?] OF IfcProduct;

		public IfcPositioningElement RelatingPositioningElement 
		{
			get { return mRelatingPositioningElement; } 
			set { mRelatingPositioningElement = value; if (value != null) value.Positions.Add(this); }
		}
		public SET<IfcProduct> RelatedProducts { get { return mRelatedProducts; } }

		public IfcRelPositions() : base() { }
		public IfcRelPositions(IfcPositioningElement relatingPositioningElement, IEnumerable<IfcProduct> relatedProducts)
			: base(relatingPositioningElement.Database)
		{
			RelatingPositioningElement = relatingPositioningElement;
			RelatedProducts.AddRange(relatedProducts);
		}
		public IfcRelPositions(IfcPositioningElement relatingPositioningElement, params IfcProduct[] relatedProducts)
			: base(relatingPositioningElement.Database)
		{
			RelatingPositioningElement = relatingPositioningElement;
			RelatedProducts.AddRange(relatedProducts);
		}

		protected override void initialize()
		{
			base.initialize();
			mRelatedProducts.CollectionChanged += mRelatedProducts_CollectionChanged;
		}
		private void mRelatedProducts_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (mDatabase != null && mDatabase.IsDisposed())
				return;
			if (e.NewItems != null)
			{
				foreach (IfcProduct product in e.NewItems)
					product.PositionedRelativeTo.Add(this); ;
			}
			if (e.OldItems != null)
			{
				foreach (IfcProduct product in e.OldItems)
					product.PositionedRelativeTo.Clear();
			}
		}
	}
	[Serializable]
	public partial class IfcRelProjectsElement : IfcRelDecomposes // IFC2x3 IfcRelConnects
	{
		internal IfcElement mRelatingElement;// : IfcElement; 
		internal IfcFeatureElementAddition mRelatedFeatureElement;// : IfcFeatureElementAddition

		public IfcElement RelatingElement { get { return mRelatingElement; } set { mRelatingElement = value; value.HasProjections.Add(this); } }
		public IfcFeatureElementAddition RelatedFeatureElement { get { return mRelatedFeatureElement; } set { mRelatedFeatureElement = value; value.mProjectsElements.Add(this); } }

		protected IfcRelProjectsElement() : base() { }
		protected IfcRelProjectsElement(DatabaseIfc db, IfcRelProjectsElement p, DuplicateOptions options) : base(db, p, options) { RelatingElement = db.Factory.Duplicate(p.RelatingElement, options); RelatedFeatureElement = db.Factory.Duplicate(p.RelatedFeatureElement, options); }
		protected IfcRelProjectsElement(IfcElement relating, IfcFeatureElementAddition related) : base(relating.mDatabase) { mRelatingElement = relating; mRelatedFeatureElement = related; }
	}
	[Serializable]
	public partial class IfcRelReferencedInSpatialStructure : IfcRelConnects
	{
		internal SET<IfcSpatialReferenceSelect> mRelatedElements = new SET<IfcSpatialReferenceSelect>();// : SET [1:?] OF IfcProduct IFC4x3 IfcSpatialReferenceSelect;
		private IfcSpatialElement mRelatingStructure;//  IfcSpatialElement 

		public SET<IfcSpatialReferenceSelect> RelatedElements { get { return mRelatedElements; } }
		public IfcSpatialElement RelatingStructure { get { return mRelatingStructure; } set { mRelatingStructure = value; value.mReferencesElements.Add(this); } }

		internal IfcRelReferencedInSpatialStructure() : base() { }
		internal IfcRelReferencedInSpatialStructure(DatabaseIfc db, IfcRelReferencedInSpatialStructure r, DuplicateOptions options) : base(db, r, options)
		{
			if (options.DuplicateDownstream)
				RelatedElements.AddRange(r.RelatedElements.Select(x => db.Factory.Duplicate(x as BaseClassIfc, options) as IfcProduct));
			RelatingStructure = db.Factory.Duplicate(r.RelatingStructure, new DuplicateOptions(options) { DuplicateDownstream = false }) as IfcSpatialElement;
		}
		public IfcRelReferencedInSpatialStructure(IfcSpatialElement e) : base(e.mDatabase)
		{
			RelatingStructure = e;
			e.mReferencesElements.Add(this);
		}
		public IfcRelReferencedInSpatialStructure(IfcSpatialReferenceSelect related, IfcSpatialElement relating)
			: this(relating) { RelatedElements.Add(related); }
		public IfcRelReferencedInSpatialStructure(IEnumerable<IfcSpatialReferenceSelect> related, IfcSpatialElement relating) 
			: this(relating) { RelatedElements.AddRange(related); }

		protected override void initialize()
		{
			base.initialize();

			mRelatedElements.CollectionChanged += mRelatedElements_CollectionChanged;
		}
		private void mRelatedElements_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (mDatabase != null && mDatabase.IsDisposed())
				return;
			if (e.NewItems != null)
			{
				foreach (IfcSpatialReferenceSelect s in e.NewItems)
					s.ReferencedInStructures.Add(this);
			}
			if (e.OldItems != null)
			{
				foreach (IfcSpatialReferenceSelect s in e.OldItems)
					s.ReferencedInStructures.Remove(this);
			}
		}
		protected override bool DisposeWorker(bool children)
		{
			foreach(IfcSpatialReferenceSelect element in RelatedElements)
				element.ReferencedInStructures.Remove(this);

			RelatingStructure.ReferencesElements.Remove(this);
			return base.DisposeWorker(children);
		}
	}
	//[Obsolete("DEPRECATED IFC4", false)]
	//ENTITY IfcRelSchedulesCostItems // DEPRECATED IFC4 
	[Serializable]
	public partial class IfcRelSequence : IfcRelConnects
	{
		internal IfcProcess mRelatingProcess;// : IfcProcess;
		internal IfcProcess mRelatedProcess;//  IfcProcess;
		private IfcLagTime mTimeLag;// : OPTIONAL IfcLagTime; IFC2x3 	IfcTimeMeasure
		private double mTimeLagSS = double.NaN;// : OPTIONAL IfcLagTime; IFC2x3 	IfcTimeMeasure
		internal IfcSequenceEnum mSequenceType = IfcSequenceEnum.NOTDEFINED;//	 :	OPTIONAL IfcSequenceEnum;
		internal string mUserDefinedSequenceType = "";//	 :	OPTIONAL IfcLabel; 

		public IfcProcess RelatingProcess { get { return mRelatingProcess; } set { mRelatingProcess = value; value.mIsPredecessorTo.Add(this); } }
		public IfcProcess RelatedProcess { get { return mRelatedProcess; } set { mRelatedProcess = value; value.mIsSuccessorFrom.Add(this); } }
		public IfcLagTime TimeLag { get { return mTimeLag; } set { mTimeLag = value; } }
		public IfcSequenceEnum SequenceType { get { return mSequenceType; } set { mSequenceType = value; } }

		internal IfcRelSequence() : base() { }
		internal IfcRelSequence(DatabaseIfc db, IfcRelSequence s, DuplicateOptions options) : base(db, s, options)
		{
			RelatingProcess = db.Factory.Duplicate(s.RelatingProcess, options) as IfcProcess;
			RelatedProcess = db.Factory.Duplicate(s.RelatedProcess, options) as IfcProcess;
			mTimeLag = s.mTimeLag;
			mSequenceType = s.mSequenceType;
			mUserDefinedSequenceType = s.mUserDefinedSequenceType;
		}
		public IfcRelSequence(IfcProcess relating, IfcProcess related) : base(relating.mDatabase)
		{
			RelatingProcess = relating;
			RelatedProcess = related;
		}
	}
	[Serializable]
	[Obsolete("DEPRECATED IFC4x3", false)]
	public partial class IfcRelServicesBuildings : IfcRelConnects
	{
		internal IfcSystem mRelatingSystem;// : IfcSystem;
		internal SET<IfcSpatialElement> mRelatedBuildings = new SET<IfcSpatialElement>();// : SET [1:?] OF IfcSpatialElement  ;

		public IfcSystem RelatingSystem { get { return mRelatingSystem; } set { mRelatingSystem = value; if(value != null) value.ServicesBuildings = this; } }
		public SET<IfcSpatialElement> RelatedBuildings { get { return mRelatedBuildings; } }

		internal IfcRelServicesBuildings() : base() { }
		internal IfcRelServicesBuildings(DatabaseIfc db, IfcRelServicesBuildings s, DuplicateOptions options) : base(db, s, options)
		{
			RelatingSystem = db.Factory.Duplicate(s.RelatingSystem, options) as IfcSystem;
			s.RelatedBuildings.ToList().ForEach(x => addRelated(db.Factory.Duplicate(x, new DuplicateOptions(options) { DuplicateDownstream = false }) as IfcSpatialElement));
		}
		public IfcRelServicesBuildings(IfcSystem system, IfcSpatialElement se)
			: base(system.mDatabase) { mRelatingSystem = system; system.mServicesBuildings = this; mRelatedBuildings.Add(se); se.mServicedBySystems.Add(this); }

		internal void addRelated(IfcSpatialElement spatial)
		{
			mRelatedBuildings.Add(spatial);
			spatial.mServicedBySystems.Add(this);
		}
	}
	[Serializable]
	public partial class IfcRelSpaceBoundary : IfcRelConnects
	{
		internal IfcSpaceBoundarySelect mRelatingSpace;// :	IfcSpaceBoundarySelect; : IfcSpace;
		internal IfcElement mRelatedBuildingElement;// :OPTIONAL IfcElement; Mandatory in IFC4
		internal IfcConnectionGeometry mConnectionGeometry;// : OPTIONAL IfcConnectionGeometry;
		internal IfcPhysicalOrVirtualEnum mPhysicalOrVirtualBoundary = IfcPhysicalOrVirtualEnum.NOTDEFINED;// : IfcPhysicalOrVirtualEnum;
		internal IfcInternalOrExternalEnum mInternalOrExternalBoundary = IfcInternalOrExternalEnum.NOTDEFINED;// : IfcInternalOrExternalEnum; 

		public IfcSpaceBoundarySelect RelatingSpace { get { return mRelatingSpace; } set { mRelatingSpace = value; value.AddBoundary(this); } }
		public IfcElement RelatedBuildingElement { get { return mRelatedBuildingElement; } set { mRelatedBuildingElement = value; if (value != null) value.ProvidesBoundaries.Add(this); } }
		public IfcConnectionGeometry ConnectionGeometry { get { return mConnectionGeometry; } set { mConnectionGeometry = value; } }

		internal IfcRelSpaceBoundary() : base() { }
		internal IfcRelSpaceBoundary(DatabaseIfc db, IfcRelSpaceBoundary b, DuplicateOptions options) : base(db, b, options)
		{
			RelatingSpace = db.Factory.Duplicate<IfcSpaceBoundarySelect>(b.mRelatingSpace, options);
			if (b.mRelatedBuildingElement != null)
				RelatedBuildingElement = db.Factory.Duplicate(b.RelatedBuildingElement, options) as IfcElement;
			if (b.mConnectionGeometry != null)
				ConnectionGeometry = db.Factory.Duplicate(b.ConnectionGeometry, options);
			mPhysicalOrVirtualBoundary = b.mPhysicalOrVirtualBoundary;
			mInternalOrExternalBoundary = b.mInternalOrExternalBoundary;
		}
		public IfcRelSpaceBoundary(IfcSpaceBoundarySelect s, IfcElement e, IfcPhysicalOrVirtualEnum virt, IfcInternalOrExternalEnum intern) : base(s.Database)
		{
			mRelatingSpace = s;
			s.AddBoundary(this);
			mRelatedBuildingElement = e;
			mPhysicalOrVirtualBoundary = virt;
			mInternalOrExternalBoundary = intern;
		}
	}
	[Serializable]
	public partial class IfcRelSpaceBoundary1stLevel : IfcRelSpaceBoundary
	{
		internal IfcRelSpaceBoundary1stLevel mParentBoundary;// :	IfcRelSpaceBoundary1stLevel;
		//INVERSE	
		internal List<IfcRelSpaceBoundary1stLevel> mInnerBoundaries = new List<IfcRelSpaceBoundary1stLevel>();//	:	SET OF IfcRelSpaceBoundary1stLevel FOR ParentBoundary;

		public IfcRelSpaceBoundary1stLevel ParentBoundary { get { return mParentBoundary; } set { mParentBoundary = value; value.mInnerBoundaries.Add(this); } }
		internal IfcRelSpaceBoundary1stLevel() : base() { }
		internal IfcRelSpaceBoundary1stLevel(DatabaseIfc db, IfcRelSpaceBoundary1stLevel r, DuplicateOptions options) 
			: base(db, r, options) { ParentBoundary = db.Factory.Duplicate(r.ParentBoundary, options); }
		public IfcRelSpaceBoundary1stLevel(IfcSpaceBoundarySelect s, IfcElement e, IfcPhysicalOrVirtualEnum virt, IfcInternalOrExternalEnum intern, IfcRelSpaceBoundary1stLevel parent)
			: base(s, e, virt, intern) { mParentBoundary = parent; }
	}
	[Serializable]
	public partial class IfcRelSpaceBoundary2ndLevel : IfcRelSpaceBoundary1stLevel
	{
		internal IfcRelSpaceBoundary2ndLevel mCorrespondingBoundary;//:	IfcRelSpaceBoundary2ndLevel;
		//INVERSE	
		internal List<IfcRelSpaceBoundary2ndLevel> mCorresponds = new List<IfcRelSpaceBoundary2ndLevel>();//:	SET OF IfcRelSpaceBoundary1stLevel FOR ParentBoundary;

		public IfcRelSpaceBoundary2ndLevel CorrespondingBoundary { get { return mCorrespondingBoundary; } set { mCorrespondingBoundary = value; value.CorrespondingBoundary = this; } }

		internal IfcRelSpaceBoundary2ndLevel() : base() { }
		internal IfcRelSpaceBoundary2ndLevel(DatabaseIfc db, IfcRelSpaceBoundary2ndLevel r, DuplicateOptions options) : base(db, r, options) { CorrespondingBoundary = db.Factory.Duplicate(r.CorrespondingBoundary, options) as IfcRelSpaceBoundary2ndLevel; }
		public IfcRelSpaceBoundary2ndLevel(IfcSpaceBoundarySelect s, IfcElement e, IfcPhysicalOrVirtualEnum virt, IfcInternalOrExternalEnum intern, IfcRelSpaceBoundary1stLevel parent, IfcRelSpaceBoundary2ndLevel corresponding)
			: base(s, e, virt, intern, parent) { if (corresponding != null) mCorrespondingBoundary = corresponding; }

	}
	[Serializable]
	public partial class IfcRelVoidsElement : IfcRelDecomposes // Ifc2x3 IfcRelConnects
	{
		private IfcElement mRelatingBuildingElement;// : IfcElement;
		private IfcFeatureElementSubtraction mRelatedOpeningElement;// : IfcFeatureElementSubtraction; 

		public IfcElement RelatingBuildingElement { get { return mRelatingBuildingElement; } set { mRelatingBuildingElement = value; if(!value.mHasOpenings.Contains(this)) value.mHasOpenings.Add(this); } }
		public IfcFeatureElementSubtraction RelatedOpeningElement { get { return mRelatedOpeningElement; } set { mRelatedOpeningElement = value; if(value != null) value.mVoidsElement = this; } }

		internal IfcRelVoidsElement() : base() { }
		internal IfcRelVoidsElement(DatabaseIfc db, IfcRelVoidsElement v, DuplicateOptions options) : base(db, v, options)
		{
			RelatingBuildingElement = db.Factory.Duplicate(v.RelatingBuildingElement, options);
			RelatedOpeningElement = db.Factory.Duplicate(v.RelatedOpeningElement, options) as IfcFeatureElementSubtraction;
		}
		public IfcRelVoidsElement(IfcElement host, IfcFeatureElementSubtraction fes)
			: base(host.mDatabase) { RelatingBuildingElement = host; RelatedOpeningElement = fes; }
	}
	[Serializable]
	public partial class IfcReparametrisedCompositeCurveSegment : IfcCompositeCurveSegment
	{
		private double mParamLength;// : IfcParameterValue
		public double ParamLength { get { return mParamLength; } set { mParamLength = value; } }

		internal IfcReparametrisedCompositeCurveSegment() : base() { }
		internal IfcReparametrisedCompositeCurveSegment(DatabaseIfc db, IfcReparametrisedCompositeCurveSegment s, DuplicateOptions options) : base(db, s, options) { mParamLength = s.mParamLength; }
		public IfcReparametrisedCompositeCurveSegment(IfcTransitionCode tc, bool sense, IfcBoundedCurve bc, double paramLength) : base(tc, sense, bc) { mParamLength = paramLength; }
	}
	[Serializable]
	public partial class IfcRepresentation<RepresentationItem> : BaseClassIfc, IfcLayeredItem where RepresentationItem : IfcRepresentationItem // Abstract IFC4 ,SUPERTYPE OF (ONEOF(IfcShapeModel,IfcStyleModel));
	{
		private IfcRepresentationContext mContextOfItems = null;// : IfcRepresentationContext;
		internal string mRepresentationIdentifier = "";//  : OPTIONAL IfcLabel; //RepresentationIdentifier: Name of the representation, e.g. 'Body' for 3D shape, 'FootPrint' for 2D ground view, 'Axis' for reference axis, 		
		private string mRepresentationType = "";//  : OPTIONAL IfcLabel;
		private SET<RepresentationItem> mItems = new SET<RepresentationItem>();//  : SET [1:?] OF IfcRepresentationItem; 
		//INVERSE 
		internal IfcRepresentationMap mRepresentationMap = null;//	 : 	SET [0:1] OF IfcRepresentationMap FOR MappedRepresentation;
		internal IfcPresentationLayerAssignment mLayerAssignment = null;// IFC4 change : 	SET OF IfcPresentationLayerAssignment FOR AssignedItems;
		internal SET<IfcProductDefinitionShape> mOfProductRepresentation = new SET<IfcProductDefinitionShape>();/// IFC4 change	 : 	SET [0:n] OF IfcProductRepresentation FOR Representations;

		public IfcRepresentationContext ContextOfItems
		{
			get { return mContextOfItems; }
			set
			{
				IfcShapeModel shapeModel = this as IfcShapeModel;
				if (shapeModel != null && mContextOfItems is IfcGeometricRepresentationContext geometricRepresentationContext)
					geometricRepresentationContext.mRepresentationsInContext.Remove(shapeModel);
				mContextOfItems = value;
				if (shapeModel != null && value is IfcGeometricRepresentationContext geometricRepContext)
					geometricRepContext.mRepresentationsInContext.Add(shapeModel);
			}
		}
		public string RepresentationIdentifier { get { return mRepresentationIdentifier; } set { mRepresentationIdentifier = value; } }
		public string RepresentationType { get { return mRepresentationType; } set { mRepresentationType = value; } }
		public SET<RepresentationItem> Items { get { return mItems; } }

		public IfcRepresentationMap RepresentationMap { get { return mRepresentationMap; } set { mRepresentationMap = value; } }
		public IfcPresentationLayerAssignment LayerAssignment { get { return mLayerAssignment; } set { mLayerAssignment = value; } }
		public SET<IfcProductDefinitionShape> OfProductRepresentation { get { return mOfProductRepresentation; } }

		protected IfcRepresentation() : base() { }
		protected IfcRepresentation(DatabaseIfc db, IfcRepresentation<RepresentationItem> r, DuplicateOptions options) : base(db)
		{
			IfcGeometricRepresentationSubContext subContext = r.ContextOfItems as IfcGeometricRepresentationSubContext;
			if(subContext != null)
			{
				IfcGeometricRepresentationSubContext.SubContextIdentifier id = IfcGeometricRepresentationSubContext.SubContextIdentifier.Axis;
				if (Enum.TryParse<IfcGeometricRepresentationSubContext.SubContextIdentifier>(subContext.ContextIdentifier, out id))
				{
					IfcGeometricRepresentationSubContext sub = null;
					if (db.Factory.mSubContexts.TryGetValue(id, out sub))
						ContextOfItems = sub;
				}
			}
			if(ContextOfItems == null)
				ContextOfItems = db.Factory.Duplicate(r.ContextOfItems, options);

			mRepresentationIdentifier = r.mRepresentationIdentifier;
			mRepresentationType = r.mRepresentationType;
			Items.AddRange(r.Items.Select(x => x.Duplicate(db, options) as RepresentationItem));

			if(options.DuplicatePresentationStyling && r.mLayerAssignment != null)
			{
				IfcPresentationLayerAssignment la = db.Factory.Duplicate(r.mLayerAssignment, new DuplicateOptions(options) { DuplicateDownstream = false }) as IfcPresentationLayerAssignment;
				la.AssignedItems.Add(this);
			}
		}
		protected IfcRepresentation(IfcRepresentationContext context) : base(context.mDatabase) 
		{ 
			ContextOfItems = context; 
			RepresentationIdentifier = context.ContextIdentifier;
			RepresentationType = context.ContextType; 
		}
		protected IfcRepresentation(IfcRepresentationContext context, RepresentationItem item) : this(context) { mItems.Add(item); }
		protected IfcRepresentation(IfcRepresentationContext context, IEnumerable<RepresentationItem> items) : this(context) { Items.AddRange(items); }
		protected override void initialize()
		{
			base.initialize();
			mItems.CollectionChanged += mItems_CollectionChanged;
		}
		private void mItems_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			if (mDatabase != null && mDatabase.IsDisposed())
				return;
			IfcShapeModel shapeModel = this as IfcShapeModel;
			if(shapeModel != null)
			{
				if (e.NewItems != null)
				{
					foreach (IfcRepresentationItem r in e.NewItems)
						r.Represents.Add(shapeModel);
				}
				if (e.OldItems != null)
				{
					foreach (IfcRepresentationItem r in e.OldItems)
						r.Represents.Remove(shapeModel);
				}
			}
		}
		protected override List<T> Extract<T>(Type type)
		{
			List<T> result = base.Extract<T>(type);
			foreach (IfcRepresentationItem item in Items)
				result.AddRange(item.Extract<T>());
			return result;
		}
	}
	[Serializable]
	public abstract partial class IfcRepresentationContext : BaseClassIfc //ABSTRACT SUPERTYPE OF(IfcGeometricRepresentationContext);
	{
		internal string mContextIdentifier = "";// : OPTIONAL IfcLabel;
		internal string mContextType = "";// : OPTIONAL IfcLabel;
	
		public string ContextIdentifier { get { return mContextIdentifier; } set { mContextIdentifier = value; } }
		public string ContextType { get { return mContextType; } set { mContextType = value; } }
		

		protected IfcRepresentationContext() : base() { }
		protected IfcRepresentationContext(DatabaseIfc db) : base(db) { }
		protected IfcRepresentationContext(DatabaseIfc db, IfcRepresentationContext c, DuplicateOptions options) : base(db, c) { mContextIdentifier = c.mContextIdentifier; mContextType = c.mContextType; }
	}
	[Serializable]
	public abstract partial class IfcRepresentationItem : BaseClassIfc, IfcLayeredItem /*(IfcGeometricRepresentationItem,IfcMappedItem,IfcStyledItem,IfcTopologicalRepresentationItem));*/
	{ //INVERSE
		private IfcPresentationLayerAssignment mLayerAssignment = null;// : SET [0:?] OF IfcPresentationLayerAssignment FOR AssignedItems;
		internal IfcStyledItem mStyledByItem = null;// : SET [0:1] OF IfcStyledItem FOR Item; 
		private HashSet<IfcShapeModel> mRepresents = new HashSet<IfcShapeModel>(); //GeometryGym Inverse Attribute

		public IfcPresentationLayerAssignment LayerAssignment { get { return mLayerAssignment; } set { mLayerAssignment = value; } }
		public IfcStyledItem StyledByItem 
		{ 
			get { return mStyledByItem; } 
			set 
			{ 
				if (value != null) 
					value.Item = this; 
				else 
					mStyledByItem = null; 
			}
		}
		public HashSet<IfcShapeModel> Represents { get { return mRepresents; } }

		protected IfcRepresentationItem() : base() { }
		protected IfcRepresentationItem(DatabaseIfc db, IfcRepresentationItem i, DuplicateOptions options) : base(db, i)
		{
			if(options.DuplicatePresentationStyling && i.mLayerAssignment != null)
			{
				IfcPresentationLayerAssignment la = db.Factory.Duplicate(i.mLayerAssignment, new DuplicateOptions(db.Tolerance) { DuplicateDownstream = false }) as IfcPresentationLayerAssignment;
				la.AssignedItems.Add(this);
			}
			if (options.DuplicatePresentationStyling && i.mStyledByItem != null)
			{
				IfcStyledItem si = db.Factory.Duplicate(i.mStyledByItem, options) as IfcStyledItem;
				si.Item = this;
			}
		}
		protected IfcRepresentationItem(DatabaseIfc db) : base(db) { }

		internal override bool isDuplicate(BaseClassIfc e, double tol)
		{
			IfcRepresentationItem ri = e as IfcRepresentationItem;
			if (ri == null)
				return false;
			if (mStyledByItem != null)
			{
				if (ri.mStyledByItem != null)
				{
					if (!mStyledByItem.isDuplicate(ri.mStyledByItem, tol))
						return false;
				}
				else
					return false;
			}
			else if (ri.mStyledByItem != null)
				return false;
			return base.isDuplicate(e, tol);
		}
		internal IfcRepresentationItem Duplicate(DatabaseIfc db, DuplicateOptions options)
		{
			IfcRepresentationItem result = DuplicateWorker(db, options);
			if(result != null)
			{
				if(mStyledByItem != null)
					(db.Factory.Duplicate(mStyledByItem) as IfcStyledItem).Item = result;
				return result;
			}
			return db.Factory.Duplicate(this, options);
		}
		protected virtual IfcRepresentationItem DuplicateWorker(DatabaseIfc db, DuplicateOptions options) { return null; }
	}
	[Serializable]
	public partial class IfcRepresentationMap : BaseClassIfc, IfcProductRepresentationSelect
	{
		private IfcAxis2Placement mMappingOrigin;// : IfcAxis2Placement;
		private IfcShapeModel mMappedRepresentation;// : IfcRepresentation;
		//INVERSE
		internal SET<IfcShapeAspect> mHasShapeAspects = new SET<IfcShapeAspect>();//	:	SET [0:?] OF IfcShapeAspect FOR PartOfProductDefinitionShape;
		internal SET<IfcMappedItem> mMapUsage = new SET<IfcMappedItem>();//: 	SET OF IfcMappedItem FOR MappingSource;

		public IfcAxis2Placement MappingOrigin { get { return mMappingOrigin; } set { mMappingOrigin = value; } }
		public IfcShapeModel MappedRepresentation
		{
			get { return mMappedRepresentation; }
			set { mMappedRepresentation = value; if(value != null) value.mRepresentationMap = this; }
		}  
		public SET<IfcShapeAspect> HasShapeAspects { get { return mHasShapeAspects; } }
		public SET<IfcMappedItem> MapUsage { get { return mMapUsage; } }

		internal SET<IfcTypeProduct> mRepresents = new SET<IfcTypeProduct>();// GG
		public SET<IfcTypeProduct> Represents { get { return mRepresents; } }

		internal IfcRepresentationMap() : base() { }
		internal IfcRepresentationMap(DatabaseIfc db, IfcRepresentationMap m, DuplicateOptions options) : base(db, m)
		{
			DuplicateOptions duplicateDownstream = new DuplicateOptions(options) { DuplicateDownstream = true };
			MappingOrigin = db.Factory.Duplicate(m.mMappingOrigin, duplicateDownstream);
			MappedRepresentation = db.Factory.Duplicate(m.MappedRepresentation, duplicateDownstream);
			if (db.Release > ReleaseVersion.IFC2x3)
			{
				foreach (IfcShapeAspect shapeAspect in m.HasShapeAspects)
					db.Factory.Duplicate(shapeAspect, duplicateDownstream).PartOfProductDefinitionShape = this;
			}
		}
		public IfcRepresentationMap(IfcAxis2Placement placement, IfcShapeRepresentation representation) : base(representation.mDatabase) { MappingOrigin = placement; MappedRepresentation = representation; }
		public IfcRepresentationMap(IfcAxis2Placement placement, IfcTopologyRepresentation representation) : base(representation.mDatabase) { MappingOrigin = placement; MappedRepresentation = representation; }
	}
	[Serializable]
	public abstract partial class IfcResource : IfcObject, IfcResourceSelect //ABSTRACT SUPERTYPE OF (ONEOF (IfcConstructionResource))
	{
		internal string mIdentification = "";// :OPTIONAL IfcIdentifier;
		internal string mLongDescription = "";//: OPTIONAL IfcText; 
		//INVERSE 
		internal SET<IfcRelAssignsToResource> mResourceOf = new SET<IfcRelAssignsToResource>();// : SET [0:?] OF IfcRelAssignsToResource FOR RelatingResource; 

		public string Identification { get { return mIdentification; } set { mIdentification = value; } }
		public string LongDescription { get { return mLongDescription; } set { mLongDescription = value; } }
		public SET<IfcRelAssignsToResource> ResourceOf { get { return mResourceOf; } } 

		protected IfcResource() : base() { }
		protected IfcResource(DatabaseIfc db, IfcResource r, DuplicateOptions options) : base(db, r, options) { }
		protected IfcResource(DatabaseIfc db) : base(db) { }
	}
	[Serializable]
	public partial class IfcResourceApprovalRelationship : IfcResourceLevelRelationship
	{
		private SET<IfcResourceObjectSelect> mRelatedResourceObjects = new SET<IfcResourceObjectSelect>(); //: SET[1:?] OF IfcResourceObjectSelect;
		private IfcApproval mRelatingApproval = null; //: IfcApproval;

		public SET<IfcResourceObjectSelect> RelatedResourceObjects { get { return mRelatedResourceObjects; } }
		public IfcApproval RelatingApproval { get { return mRelatingApproval; } set { mRelatingApproval = value; } }

		public IfcResourceApprovalRelationship() : base() { }
		public IfcResourceApprovalRelationship(IEnumerable<IfcResourceObjectSelect> relatedResourceObjects, IfcApproval relatingApproval)
			: base(relatingApproval.Database)
		{
			RelatedResourceObjects.AddRange(relatedResourceObjects);
			RelatingApproval = relatingApproval;
		}
	}
	[Serializable]
	public partial class IfcResourceConstraintRelationship : IfcResourceLevelRelationship  // IfcPropertyConstraintRelationship; // DEPRECATED IFC4 renamed
	{
		internal IfcConstraint mRelatingConstraint;// :	IfcConstraint;
		internal SET<IfcResourceObjectSelect> mRelatedResourceObjects = new SET<IfcResourceObjectSelect>();// :	SET [1:?] OF IfcResourceObjectSelect;

		public IfcConstraint RelatingConstraint { get { return mRelatingConstraint; } set { mRelatingConstraint = value; if (!value.mPropertiesForConstraint.Contains(this)) value.mPropertiesForConstraint.Add(this); } }
		public SET<IfcResourceObjectSelect> RelatedResourceObjects { get { return mRelatedResourceObjects; } }

		internal IfcResourceConstraintRelationship() : base() { }
		internal IfcResourceConstraintRelationship(DatabaseIfc db, IfcResourceConstraintRelationship r, DuplicateOptions options) 
			: base(db, r, options) 
		{
			RelatingConstraint = db.Factory.Duplicate(r.RelatingConstraint) as IfcConstraint;
			RelatedResourceObjects.AddRange(r.mRelatedResourceObjects.Select(x => db.Factory.Duplicate(x as BaseClassIfc) as IfcResourceObjectSelect)); }
		public IfcResourceConstraintRelationship(IfcConstraint constraint, IfcResourceObjectSelect related) : this(constraint, new List<IfcResourceObjectSelect>() { related }) { }
		public IfcResourceConstraintRelationship(IfcConstraint constraint, List<IfcResourceObjectSelect> related) : base(constraint.mDatabase)
		{
			RelatingConstraint = constraint;
			RelatedResourceObjects.AddRange(related);
		}

		protected override void initialize()
		{
			base.initialize();
			mRelatedResourceObjects.CollectionChanged += mRelatedResourceObjects_CollectionChanged;
		}
		private void mRelatedResourceObjects_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (mDatabase != null && mDatabase.IsDisposed())
				return;
			if (e.NewItems != null)
			{
				foreach (IfcResourceObjectSelect o in e.NewItems)
				{
					o.AddConstraintRelationShip(this);
				}
			}
			if (e.OldItems != null)
			{
				foreach (IfcResourceObjectSelect o in e.OldItems)
				{
				}
			}
		}

		protected override bool DisposeWorker(bool children)
		{
			foreach(IfcResourceObjectSelect r in RelatedResourceObjects.ToList())
			{
				(r as BaseClassIfc).Dispose(children);
			}
			return base.DisposeWorker(children);
		}
	}
	[Serializable]
	public abstract partial class IfcResourceLevelRelationship : BaseClassIfc, NamedObjectIfc  //IFC4 ABSTRACT SUPERTYPE OF(ONEOF(IfcApprovalRelationship,
	{ // IfcCurrencyRelationship, IfcDocumentInformationRelationship, IfcExternalReferenceRelationship, IfcMaterialRelationship, IfcOrganizationRelationship, IfcPropertyDependencyRelationship, IfcResourceApprovalRelationship, IfcResourceConstraintRelationship));
		internal string mName = "";// : OPTIONAL IfcLabel
		internal string mDescription = "";// : OPTIONAL IfcText; 
		//INVERSE
		//mHasExternalReference

		public string Name { get { return mName; } set { mName = value; } }
		public string Description { get { return mDescription; } set { mDescription = value; } }

		protected IfcResourceLevelRelationship() : base() { }
		protected IfcResourceLevelRelationship(DatabaseIfc db) : base(db) { }
		protected IfcResourceLevelRelationship(DatabaseIfc db, IfcResourceLevelRelationship r, DuplicateOptions options)
			: base(db)
		{
			Name = r.Name;
			Description = r.Description; 
		}
	}
	public partial interface IfcResourceObjectSelect : IBaseClassIfc //IFC4 SELECT (	IfcPropertyAbstraction, IfcPhysicalQuantity, IfcAppliedValue, 
	{   //IfcContextDependentUnit, IfcConversionBasedUnit, IfcProfileDef, IfcActorRole, IfcApproval, IfcConstraint, IfcTimeSeries, IfcMaterialDefinition, IfcPerson, IfcPersonAndOrganization, IfcOrganization, IfcExternalReference, IfcExternalInformation););
		SET<IfcExternalReferenceRelationship> HasExternalReference { get; }
		void AddConstraintRelationShip(IfcResourceConstraintRelationship constraintRelationship);
	}
	public interface IfcResourceSelect : IBaseClassIfc // SELECT(IfcResource, IfcTypeResource)
	{
		SET<IfcRelAssignsToResource> ResourceOf { get; }
	}
	[Serializable]
	public partial class IfcResourceTime : IfcSchedulingTime //IFC4
	{
		internal string mScheduleWork = "";//	 :	OPTIONAL IfcDuration;
		internal double mScheduleUsage = double.NaN; //:	OPTIONAL IfcPositiveRatioMeasure;
		internal DateTime mScheduleStart = DateTime.MinValue, mScheduleFinish = DateTime.MinValue;//:	OPTIONAL IfcDateTime;
		internal string mScheduleContour = "";//:	OPTIONAL IfcLabel;
		internal IfcDuration mLevelingDelay = null;//	 :	OPTIONAL IfcDuration;
		internal bool mIsOverAllocated = false;//	 :	OPTIONAL BOOLEAN;
		internal DateTime mStatusTime = DateTime.MinValue;//:	OPTIONAL IfcDateTime;
		internal IfcDuration mActualWork = null;//	 :	OPTIONAL IfcDuration; 
		internal double mActualUsage = double.NaN; //:	OPTIONAL IfcPositiveRatioMeasure; 
		internal DateTime mActualStart = DateTime.MinValue, mActualFinish = DateTime.MinValue;//	 :	OPTIONAL IfcDateTime;
		internal IfcDuration mRemainingWork = null;//	 :	OPTIONAL IfcDuration;
		internal double mRemainingUsage = double.NaN, mCompletion = double.NaN;//	 :	OPTIONAL IfcPositiveRatioMeasure; 

		public string ScheduleWork { get { return mScheduleWork; } set { mScheduleWork = value; } }
		public double ScheduleUsage { get { return mScheduleUsage; } set { mScheduleUsage = value; } }
		public DateTime ScheduleStart { get { return mScheduleStart; } set { mScheduleStart = value; } }
		public DateTime ScheduleFinish { get { return mScheduleFinish; } set { mScheduleFinish = value; } }
		public string ScheduleContour { get { return mScheduleContour; } set { mScheduleContour = value; } }
		public IfcDuration LevelingDelay { get { return mLevelingDelay; } set { mLevelingDelay = value; } }
		public bool IsOverAllocated { get { return mIsOverAllocated; } set { mIsOverAllocated = value; } }
		public DateTime StatusTime { get { return mStatusTime; } set { mStatusTime = value; } }
		public IfcDuration ActualWork { get { return mActualWork; } set { mActualWork = value; } }
		public double ActualUsage { get { return mActualUsage; } set { mActualUsage = value; } }
		public DateTime ActualStart { get { return mActualStart; } set { mActualStart = value; } }
		public DateTime ActualFinish { get { return mActualFinish; } set { mActualFinish = value; } }
		public IfcDuration RemainingWork { get { return mRemainingWork; } set { mRemainingWork = value; } }
		public double RemainingUsage { get { return mRemainingUsage; } set { mRemainingUsage = value; } }
		public double Completion { get { return mCompletion; } set { mCompletion = value; } }

		internal IfcResourceTime() : base() { }
		internal IfcResourceTime(DatabaseIfc db, IfcResourceTime t, DuplicateOptions options) : base(db, t, options)
		{
			mScheduleWork = t.mScheduleWork; mScheduleUsage = t.mScheduleUsage; mScheduleStart = t.mScheduleStart; mScheduleFinish = t.mScheduleFinish; mScheduleContour = t.mScheduleContour;
			mLevelingDelay = t.mLevelingDelay; mIsOverAllocated = t.mIsOverAllocated; mStatusTime = t.mStatusTime; mActualWork = t.mActualWork; mActualUsage = t.mActualUsage;
			mActualStart = t.mActualStart; mActualFinish = t.mActualFinish; mRemainingWork = t.mRemainingWork; mRemainingUsage = t.mRemainingUsage; mCompletion = t.mCompletion;
		}
		public IfcResourceTime(DatabaseIfc db) : base(db) { }
	}
	[Serializable]
	public partial class IfcRevolvedAreaSolid : IfcSweptAreaSolid // SUPERTYPE OF(IfcRevolvedAreaSolidTapered)
	{
		internal IfcAxis1Placement mAxis;//: IfcAxis1Placement
		internal double mAngle;// : IfcPlaneAngleMeasure;

		public IfcAxis1Placement Axis { get { return mAxis; } set { mAxis = value; } }
		public double Angle { get { return mAngle; } set { mAngle = value; } }

		internal IfcRevolvedAreaSolid() : base() { }
		internal IfcRevolvedAreaSolid(DatabaseIfc db, IfcRevolvedAreaSolid s, DuplicateOptions options) : base(db, s, options) { Axis = db.Factory.Duplicate(s.Axis) as IfcAxis1Placement; mAngle = s.mAngle; }
		public IfcRevolvedAreaSolid(IfcProfileDef profile, IfcAxis2Placement3D position, IfcAxis1Placement axis, double angle) : base(profile, position) { Axis = axis; mAngle = angle; }
	}
	[Serializable]
	public partial class IfcRevolvedAreaSolidTapered : IfcRevolvedAreaSolid
	{
		private IfcProfileDef mEndSweptArea;//: IfcProfileDef 
		public IfcProfileDef EndSweptArea { get { return mEndSweptArea; } set { mEndSweptArea = value; } }

		internal IfcRevolvedAreaSolidTapered() : base() { }
		internal IfcRevolvedAreaSolidTapered(DatabaseIfc db, IfcRevolvedAreaSolidTapered e, DuplicateOptions options) : base(db, e, options) { EndSweptArea = db.Factory.Duplicate(e.EndSweptArea, options) as IfcProfileDef; }
		public IfcRevolvedAreaSolidTapered(IfcProfileDef start, IfcAxis2Placement3D placement, IfcAxis1Placement axis, double angle, IfcProfileDef end) : base(start, placement, axis, angle) { EndSweptArea = end; }
	}
	[Obsolete("DELETED IFC4", false)]
	[Serializable]
	public partial class IfcRibPlateProfileProperties : IfcProfileProperties
	{
		internal double mThickness, mRibHeight, mRibWidth, mRibSpacing;// : OPTIONAL IfcPositiveLengthMeasure;
		internal IfcRibPlateDirectionEnum mDirection;// : IfcRibPlateDirectionEnum;*/
		internal IfcRibPlateProfileProperties() : base() { }
		internal IfcRibPlateProfileProperties(DatabaseIfc db, IfcRibPlateProfileProperties p, DuplicateOptions options) : base(db, p, options)
		{
			mThickness = p.mThickness;
			mRibHeight = p.mRibHeight;
			mRibWidth = p.mRibWidth;
			mRibSpacing = p.mRibSpacing;
			mDirection = p.mDirection;
		}
	}
	[Serializable]
	public partial class IfcRightCircularCone : IfcCsgPrimitive3D
	{
		internal double mHeight, mBottomRadius;// : IfcPositiveLengthMeasure;
		internal IfcRightCircularCone() : base() { }
		internal IfcRightCircularCone(DatabaseIfc db, IfcRightCircularCone c, DuplicateOptions options) : base(db, c, options) { mHeight = c.mHeight; mBottomRadius = c.mBottomRadius; }
	}
	[Serializable]
	public partial class IfcRightCircularCylinder : IfcCsgPrimitive3D
	{
		internal double mHeight, mRadius;// : IfcPositiveLengthMeasure;
		internal IfcRightCircularCylinder() : base() { }
		internal IfcRightCircularCylinder(DatabaseIfc db, IfcRightCircularCylinder c, DuplicateOptions options) : base(db, c, options) { mHeight = c.mHeight; mRadius = c.mRadius; }
	}
	[Serializable, VersionAdded(ReleaseVersion.IFC4X3_ADD2)]
	public partial class IfcRigidOperation : IfcCoordinateOperation
	{
		internal IfcMeasureValue mFirstCoordinate, mSecondCoordinate;// :  	IfcMeasureValue;
		internal double mHeight = double.NaN;// 	:	IfcLengthMeasure;
		public IfcMeasureValue FirstCoordinate { get { return mFirstCoordinate; } set { mFirstCoordinate = value; } }  //IfcMeasureValue
		public IfcMeasureValue SecondCoordinate { get { return mSecondCoordinate; } set { mSecondCoordinate = value; } }  //IfcMeasureValue
		public double Height { get { return mHeight; } set { mHeight = value; } }  //IfcLengthMeasure

		internal IfcRigidOperation() : base() { }
		internal IfcRigidOperation(DatabaseIfc db, IfcRigidOperation o) : base(db, o) { mFirstCoordinate = o.mFirstCoordinate; mSecondCoordinate = o.mSecondCoordinate; mHeight = o.mHeight; }
		public IfcRigidOperation(IfcCoordinateReferenceSystemSelect source, IfcCoordinateReferenceSystem target, IfcLengthMeasure firstCoordinate, IfcLengthMeasure secondCoordinate, double height)
			: base(source, target)
		{
			FirstCoordinate = firstCoordinate;		
			SecondCoordinate = secondCoordinate;
			Height = height;	
		}
		public IfcRigidOperation(IfcCoordinateReferenceSystemSelect source, IfcCoordinateReferenceSystem target, IfcPlaneAngleMeasure firstCoordinate, IfcPlaneAngleMeasure secondCoordinate, double height)
			: base(source, target)
		{
			FirstCoordinate = firstCoordinate;
			SecondCoordinate = secondCoordinate;
			Height = height;
		}
		public override IfcCoordinateOperation CreateDuplicate(IfcCoordinateReferenceSystemSelect source)
		{
			if (FirstCoordinate is IfcLengthMeasure lengthFirst && SecondCoordinate is IfcLengthMeasure lengthSecond)
			{
				var rigidOperation = new IfcRigidOperation(source, TargetCRS, lengthFirst, lengthSecond, Height);
				return rigidOperation;
			}
			if(FirstCoordinate is IfcPlaneAngleMeasure angleFirst && SecondCoordinate is IfcPlaneAngleMeasure angleSecond)
			{
				var rigidOperation = new IfcRigidOperation(source, TargetCRS, angleFirst, angleSecond, Height);
				return rigidOperation;
			}
			return null;
		}
	}
	[Serializable, VersionAdded(ReleaseVersion.IFC4X3)]
	public partial class IfcRoad : IfcFacility
	{
		private IfcRoadTypeEnum mPredefinedType = IfcRoadTypeEnum.NOTDEFINED;// OPTIONAL : IfcRoadTypeEnum
		public IfcRoadTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcRoadTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		public IfcRoad() : base() { }
		internal IfcRoad(DatabaseIfc db) : base(db) { }
		public IfcRoad(DatabaseIfc db, string name) : base(db, name) { }
		public IfcRoad(DatabaseIfc db, IfcRoad road, DuplicateOptions options) : base(db, road, options) { }
		public IfcRoad(IfcFacility host, string name, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { Name = name; }
		internal IfcRoad(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	[Serializable]
	public partial class IfcRoadPart : IfcFacilityPart
	{
		private IfcRoadPartTypeEnum mPredefinedType = IfcRoadPartTypeEnum.NOTDEFINED; //: OPTIONAL IfcRoadPartTypeEnum;
		public IfcRoadPartTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcRoadPartTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }
		public override string StepClassName { get { if (mDatabase != null && mDatabase.Release > ReleaseVersion.IFC4X2 && mDatabase.Release < ReleaseVersion.IFC4X3) return "IfcFacilityPart"; return base.StepClassName; } }
		public IfcRoadPart() : base() { }
		public IfcRoadPart(DatabaseIfc db) : base(db) { }
		public IfcRoadPart(DatabaseIfc db, IfcRoadPart roadPart, DuplicateOptions options) : base(db, roadPart, options) { }
		public IfcRoadPart(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	[Serializable]
	public partial class IfcRoof : IfcBuiltElement
	{
		private IfcRoofTypeEnum mPredefinedType = IfcRoofTypeEnum.NOTDEFINED;// OPTIONAL : IfcRoofTypeEnum; 
		public IfcRoofTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcRoofTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcRoof() : base() { }
		internal IfcRoof(DatabaseIfc db, IfcRoof r, DuplicateOptions options) : base(db, r, options) { PredefinedType = r.PredefinedType; }
		public IfcRoof(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	[Serializable]
	public partial class IfcRoofType : IfcBuiltElementType //IFC4
	{
		private IfcRoofTypeEnum mPredefinedType = IfcRoofTypeEnum.NOTDEFINED;
		public IfcRoofTypeEnum PredefinedType { get { return mPredefinedType; }  set { mPredefinedType = validPredefinedType<IfcRoofTypeEnum>(value, mDatabase == null ? ReleaseVersion.IFC4X3 : mDatabase.Release); } }

		internal IfcRoofType() : base() { }
		internal IfcRoofType(DatabaseIfc db, IfcRoofType t, DuplicateOptions options) : base(db, t, options) { PredefinedType = t.PredefinedType; }
		public IfcRoofType(DatabaseIfc db, string name, IfcRoofTypeEnum type) : base(db) { Name = name; PredefinedType = type; }
	}
	[Serializable]
	public abstract partial class IfcRoot : BaseClassIfc, NamedObjectIfc //ABSTRACT SUPERTYPE OF (ONEOF (IfcObjectDefinition ,IfcPropertyDefinition ,IfcRelationship));
	{
		private IfcOwnerHistory mOwnerHistory = null;// : IfcOwnerHistory  IFC4  OPTIONAL;
		private string mName = ""; //: OPTIONAL IfcLabel;
		private string mDescription = ""; //: OPTIONAL IfcText; 

		public string GlobalId
		{
			get { return mGlobalId; }
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					if (string.Compare(mGlobalId, value, false) != 0)
					{
						if (mDatabase != null)
						{
							if(!string.IsNullOrEmpty(mGlobalId))
								mDatabase.mDictionary.TryRemove(mGlobalId, out BaseClassIfc obj);
							mDatabase.mDictionary[value] = this;
						}
						setGlobalId(value);
					}
				}
			}
		}
		public Guid Guid
		{
			get
			{
				return ParserIfc.DecodeGlobalID(mGlobalId);
			}
			set
			{
				if (value != Guid.Empty)
					GlobalId = ParserIfc.EncodeGuid(value);
			}
		}

		public IfcOwnerHistory OwnerHistory
		{
			get { return mOwnerHistory; }
			set { mOwnerHistory = value; }
		}
		public virtual string Name { get { return mName; } set { mName = value; } }
		public string Description { get { return mDescription; } set { mDescription = value; } }

		protected IfcRoot() : base() { setGlobalId(ParserIfc.EncodeGuid(Guid.NewGuid())); }
		protected IfcRoot(DatabaseIfc db) : base(db)
		{
			setGlobalId(ParserIfc.EncodeGuid(Guid.NewGuid()));
			if (db != null && (db.Release < ReleaseVersion.IFC4 || (db.mModelView != ModelView.Ifc4Reference && db.Factory.Options.GenerateOwnerHistory)))
				OwnerHistory = db.Factory.OwnerHistoryAdded;
		}
		protected IfcRoot(IfcRoot root) : base(root.Database)
		{
			OwnerHistory = root.OwnerHistory;
			Name = root.Name;
			Description = root.Description;
		}
		protected IfcRoot(DatabaseIfc db, IfcRoot r, DuplicateOptions options) : base(db, r)
		{
			if (r.GlobalId != GlobalId)
			{
				if (db[r.GlobalId] == null)
					GlobalId = r.GlobalId;
				else
					Guid = Guid.NewGuid();
			}
			if (options.OwnerHistory != null)
				OwnerHistory = options.OwnerHistory;
			else if (options.DuplicateOwnerHistory && r.mOwnerHistory != null)
				OwnerHistory = db.Factory.Duplicate(r.OwnerHistory, options) as IfcOwnerHistory;
			mName = r.mName;
			mDescription = r.mDescription;
		}
		protected IfcRoot(DatabaseIfc db, IfcRoot r, IfcOwnerHistory ownerHistory) : base(db, r)
		{
			if(db[r.GlobalId] == null)
				GlobalId = r.GlobalId;
			if (ownerHistory != null)
				OwnerHistory = ownerHistory;
			else if (r.mOwnerHistory != null)
				OwnerHistory = db.Factory.Duplicate(r.OwnerHistory) as IfcOwnerHistory;
			mName = r.mName;
			mDescription = r.mDescription;
		}
		internal override bool isDuplicate(BaseClassIfc e, double tol)
		{
			IfcRoot root = e as IfcRoot; // || root.mOwnerHistory != mOwnerHistory
			if (root == null || string.Compare(root.Name, Name) != 0 || string.Compare(root.Description, Description) != 0)
				return false;
			return base.isDuplicate(e, tol);
		}
	}
	[Serializable]
	public partial class IfcRotationalStiffnessSelect
	{
		internal bool mRigid = false;
		internal IfcRotationalStiffnessMeasure mStiffness = null;

		public bool Rigid { get { return mRigid; } }
		public IfcRotationalStiffnessMeasure Stiffness { get { return mStiffness; } }

		internal IfcRotationalStiffnessSelect(IfcRotationalStiffnessMeasure stiff) { mStiffness = stiff; }
		public IfcRotationalStiffnessSelect(bool fix) { mRigid = fix; }
		public IfcRotationalStiffnessSelect(double stiff) { mStiffness = new IfcRotationalStiffnessMeasure(stiff); }
	}
	//[Obsolete("DEPRECATED IFC4", false)]
	//ENTITY IfcRoundedEdgeFeature // DEPRECATED IFC4
	[Serializable]
	public partial class IfcRoundedRectangleProfileDef : IfcRectangleProfileDef
	{
		internal double mRoundingRadius;// : IfcPositiveLengthMeasure; 
		public double RoundingRadius { get { return mRoundingRadius; } set { mRoundingRadius = value; } }
		internal IfcRoundedRectangleProfileDef() : base() { }
		internal IfcRoundedRectangleProfileDef(DatabaseIfc db, IfcRoundedRectangleProfileDef p, DuplicateOptions options) : base(db, p, options) { mRoundingRadius = p.mRoundingRadius; }
		public IfcRoundedRectangleProfileDef(DatabaseIfc db, string name, double xDim, double yDim, double roundingRadius) : base(db, name, xDim, yDim) { mRoundingRadius = roundingRadius; }
	}
}
