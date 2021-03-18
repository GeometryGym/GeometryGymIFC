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
using System.Collections.Specialized;
using System.Text;
using System.Reflection;
using System.IO;
using System.ComponentModel;
using System.Linq;
using GeometryGym.STEP;
using System.Diagnostics;

namespace GeometryGym.Ifc
{
	[Obsolete("DEPRECATED IFC4", false)]
	[Serializable]
	public partial class IfcRadiusDimension : IfcDimensionCurveDirectedCallout // DEPRECATED IFC4
	{
		internal IfcRadiusDimension() : base() { }
	}
	[Serializable]
	public partial class IfcRail : IfcBuiltElement
	{
		private IfcRailTypeEnum mPredefinedType = IfcRailTypeEnum.NOTDEFINED; //: OPTIONAL IfcRailTypeEnum;
		public IfcRailTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		public IfcRail() : base() { }
		public IfcRail(DatabaseIfc db) : base(db) { }
		public IfcRail(DatabaseIfc db, IfcRail rail, DuplicateOptions options) : base(db, rail, options) { PredefinedType = rail.PredefinedType; }
		public IfcRail(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	[Serializable]
	public partial class IfcRailType : IfcBuiltElementType
	{
		private IfcRailTypeEnum mPredefinedType = IfcRailTypeEnum.NOTDEFINED; //: IfcRailTypeEnum;
		public IfcRailTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		public IfcRailType() : base() { }
		public IfcRailType(DatabaseIfc db, IfcRailType railType, DuplicateOptions options) : base(db, railType, options) { PredefinedType = railType.PredefinedType; }
		public IfcRailType(DatabaseIfc db, string name, IfcRailTypeEnum predefinedType)
			: base(db, name) { PredefinedType = predefinedType; }
	}
	[Serializable]
	public partial class IfcRailing : IfcBuiltElement
	{
		internal IfcRailingTypeEnum mPredefinedType = IfcRailingTypeEnum.NOTDEFINED;// : OPTIONAL IfcRailingTypeEnum
		public IfcRailingTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcRailing() : base() { }
		internal IfcRailing(DatabaseIfc db, IfcRailing r, DuplicateOptions options) : base(db, r, options) { mPredefinedType = r.mPredefinedType; }
		public IfcRailing(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	[Serializable]
	public partial class IfcRailingType : IfcBuiltElementType
	{
		internal IfcRailingTypeEnum mPredefinedType = IfcRailingTypeEnum.NOTDEFINED;
		public IfcRailingTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcRailingType() : base() { }
		internal IfcRailingType(DatabaseIfc db, IfcRailingType t, DuplicateOptions options) : base(db, t, options) { mPredefinedType = t.mPredefinedType; }
		public IfcRailingType(DatabaseIfc m, string name, IfcRailingTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
	}
	[Serializable]
	public partial class IfcRailway : IfcFacility
	{
		internal IfcRailwayTypeEnum mPredefinedType = IfcRailwayTypeEnum.NOTDEFINED;// OPTIONAL : IfcRailwayTypeEnum
		public IfcRailwayTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		public IfcRailway() : base() { }
		public IfcRailway(DatabaseIfc db) : base(db) { }
		public IfcRailway(DatabaseIfc db, IfcRailway railway, DuplicateOptions options) : base(db, railway, options) { }
		public IfcRailway(IfcFacility host, string name, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { Name = name; }
		public IfcRailway(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	[Serializable]
	public partial class IfcRamp : IfcBuiltElement
	{
		internal IfcRampTypeEnum mPredefinedType = IfcRampTypeEnum.NOTDEFINED;// OPTIONAL : IfcRampTypeEnum
		public IfcRampTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcRamp() : base() { }
		internal IfcRamp(DatabaseIfc db, IfcRamp r, DuplicateOptions options) : base(db, r, options) { mPredefinedType = r.mPredefinedType; }
		public IfcRamp(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	[Serializable]
	public partial class IfcRampFlight : IfcBuiltElement
	{
		internal IfcRampFlightTypeEnum mPredefinedType = IfcRampFlightTypeEnum.NOTDEFINED;// OPTIONAL : IfcRampTypeEnum
		public IfcRampFlightTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcRampFlight() : base() { }
		internal IfcRampFlight(DatabaseIfc db, IfcRampFlight f, DuplicateOptions options) : base(db, f, options) { mPredefinedType = f.mPredefinedType; }
		public IfcRampFlight(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	[Serializable]
	public partial class IfcRampFlightType : IfcBuiltElementType
	{
		internal IfcRampFlightTypeEnum mPredefinedType = IfcRampFlightTypeEnum.NOTDEFINED;
		public IfcRampFlightTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcRampFlightType() : base() { }
		internal IfcRampFlightType(DatabaseIfc db, IfcRampFlightType t, DuplicateOptions options) : base(db, t, options) { mPredefinedType = t.mPredefinedType; }
		public IfcRampFlightType(DatabaseIfc m, string name, IfcRampFlightTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
	}
	[Serializable]
	public partial class IfcRampType : IfcBuiltElementType //IFC4
	{
		internal IfcRampTypeEnum mPredefinedType = IfcRampTypeEnum.NOTDEFINED;
		public IfcRampTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcRampType() : base() { }
		internal IfcRampType(DatabaseIfc db, IfcRampType t, DuplicateOptions options) : base(db, t, options) { mPredefinedType = t.mPredefinedType; }
		public IfcRampType(DatabaseIfc m, string name, IfcRampTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
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
		public ReadOnlyCollection<double> WeightsData { get { return new ReadOnlyCollection<double>(mWeightsData); } }
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
		public ReadOnlyCollection<List<double>> WeightsData { get { return new ReadOnlyCollection<List<double>>(mWeightsData); } }
		internal IfcRationalBSplineSurfaceWithKnots() : base() { }
		internal IfcRationalBSplineSurfaceWithKnots(DatabaseIfc db, IfcRationalBSplineSurfaceWithKnots s, DuplicateOptions options) : base(db, s, options)
		{
			for (int icounter = 0; icounter < s.mWeightsData.Count; icounter++)
				mWeightsData.Add(new List<double>(s.mWeightsData[icounter].ToArray()));
		}
		public IfcRationalBSplineSurfaceWithKnots(int uDegree, int vDegree, IEnumerable<IEnumerable<IfcCartesianPoint>> controlPoints, IEnumerable<int> uMultiplicities, IEnumerable<int> vMultiplicities, IEnumerable<double> uKnots, IEnumerable<double> vKnots, IfcKnotType type, List<List<double>> weights)
			: base(uDegree, vDegree, controlPoints, uMultiplicities, vMultiplicities, uKnots, vKnots, type)
		{
			mWeightsData.AddRange(weights);
		}
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
		public IfcRectangleHollowProfileDef(DatabaseIfc m, string name, double xDim, double yDim, double wallThickness)
			: base(m, name, xDim, yDim) { mWallThickness = wallThickness; }
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
		internal int mBasisSurface;// : IfcPlane;
		internal double mU1, mV1, mU2, mV2;// : IfcParameterValue;
		internal bool mUsense, mVsense;// : BOOLEAN; 

		public IfcPlane BasisSurface { get { return mDatabase[mBasisSurface] as IfcPlane; } set { mBasisSurface = value.mIndex; } }

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
		internal int mPosition = 0, mInterval = 0, mOccurrences = 0;//	 :	OPTIONAL IfcInteger;
		internal List<int> mTimePeriods = new List<int>();//	 :	OPTIONAL LIST [1:?] OF IfcTimePeriod;
		internal IfcRecurrencePattern() : base() { }
		//internal IfcRecurrencePattern(IfcRecurrencePattern p) : base()
		//{
		//	mRecurrenceType = p.mRecurrenceType;
		//	mDayComponent.AddRange(p.mDayComponent);
		//	mWeekdayComponent.AddRange(p.mWeekdayComponent);
		//	mMonthComponent.AddRange(p.mMonthComponent);
		//	mPosition = p.mPosition;
		//	mInterval = p.mInterval;
		//	mOccurrences = p.mOccurrences;
		//	mTimePeriods.AddRange(p.mTimePeriods);
		//}
		internal IfcRecurrencePattern(DatabaseIfc m, IfcRecurrenceTypeEnum type, List<int> days, List<int> weekdays, List<int> months, int position, int interval, int occurences, List<IfcTimePeriod> periods)
			: base(m)
		{
			mRecurrenceType = type;
			if (days != null)
				mDayComponent.AddRange(days);
			if (weekdays != null)
				mWeekdayComponent.AddRange(weekdays);
			if (months != null)
				mMonthComponent.AddRange(months);
			mPosition = position;
			mInterval = interval;
			mOccurrences = occurences;
			if (periods != null)
				mTimePeriods = periods.ConvertAll(x => x.mIndex);
		}
	}
	[Serializable]
	public partial class IfcReference : BaseClassIfc, IfcMetricValueSelect, IfcAppliedValueSelect // IFC4
	{
		internal string mTypeIdentifier = "$", mAttributeIdentifier = "$"; //:	OPTIONAL IfcIdentifier;
		internal string mInstanceName = "$"; //:OPTIONAL IfcLabel;
		internal List<int> mListPositions = new List<int>();//	 :	OPTIONAL LIST [1:?] OF INTEGER;
		private int mInnerReference = 0;//	 :	OPTIONAL IfcReference;

		public string TypeIdentifier { get { return (mTypeIdentifier == "$" ? "" : ParserIfc.Decode(mTypeIdentifier)); } set { mTypeIdentifier = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public string AttributeIdentifier { get { return (mAttributeIdentifier == "$" ? "" : ParserIfc.Decode(mAttributeIdentifier)); } set { mAttributeIdentifier = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public string InstanceName { get { return (mInstanceName == "$" ? "" : ParserIfc.Decode(mInstanceName)); } set { mInstanceName = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public ReadOnlyCollection<int> ListPositions { get { return new ReadOnlyCollection<int>(mListPositions); } }
		public IfcReference InnerReference { get { return mDatabase[mInnerReference] as IfcReference; } set { mInnerReference = (value == null ? 0 : value.mIndex); } }

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
	[Serializable]
	public partial class IfcReferent : IfcPositioningElement
	{
		private IfcReferentTypeEnum mPredefinedType = IfcReferentTypeEnum.NOTDEFINED; //: OPTIONAL IfcReferentTypeEnum;
		private double mRestartDistance = double.NaN; //: OPTIONAL IfcLengthMeasure;

		public IfcReferentTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		public double RestartDistance { get { return mRestartDistance; } set { mRestartDistance = value; } }

		public IfcReferent() : base() { }
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
		public IfcRegularTimeSeries(string name, DateTime startTime, DateTime endTime, IfcTimeSeriesDataTypeEnum timeSeriesDataType, IfcDataOriginEnum dataOrigin, double timeStep, IEnumerable<IfcTimeSeriesValue> values)
			: base(values.First().Database, name, startTime, endTime, timeSeriesDataType, dataOrigin)
		{
			TimeStep = timeStep;
			Values.AddRange(values);
		}
	}
	[Serializable]
	public partial class IfcReinforcedSoil : IfcEarthworksElement
	{
		private IfcReinforcedSoilTypeEnum mPredefinedType = IfcReinforcedSoilTypeEnum.NOTDEFINED; //: OPTIONAL IfcReinforcedSoilTypeEnum;
		public IfcReinforcedSoilTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

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
		public string SteelGrade { get { return ParserIfc.Decode(mSteelGrade); } set { mSteelGrade = ParserIfc.Encode(value); } }
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
		internal string mDefinitionType = "$";// 	:	OPTIONAL IfcLabel; 
		internal List<int> mReinforcementSectionDefinitions = new List<int>();// :	LIST [1:?] OF IfcSectionReinforcementProperties;

		public string DefinitionType { get { return (mDefinitionType == "$" ? "" : ParserIfc.Decode(mDefinitionType)); } set { mDefinitionType = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public ReadOnlyCollection<IfcSectionReinforcementProperties> ReinforcementSectionDefinitions { get { return new ReadOnlyCollection<IfcSectionReinforcementProperties>(mReinforcementSectionDefinitions.ConvertAll(x => mDatabase[x] as IfcSectionReinforcementProperties)); } }

		internal IfcReinforcementDefinitionProperties() : base() { }
		internal IfcReinforcementDefinitionProperties(DatabaseIfc db, IfcReinforcementDefinitionProperties p, DuplicateOptions options) : base(db, p, options)
		{
			mDefinitionType = p.mDefinitionType;
			p.ReinforcementSectionDefinitions.ToList().ForEach(x => addSection(db.Factory.Duplicate(x) as IfcSectionReinforcementProperties));
		}
		public IfcReinforcementDefinitionProperties(string name, IEnumerable<IfcSectionReinforcementProperties> sectProps)
			: base(sectProps.First().mDatabase, name) { foreach (IfcSectionReinforcementProperties prop in sectProps) addSection(prop); }

		internal void addSection(IfcSectionReinforcementProperties section) { mReinforcementSectionDefinitions.Add(section.mIndex); }
	}
	[Serializable]
	public partial class IfcReinforcingBar : IfcReinforcingElement
	{
		private double mNominalDiameter = double.NaN;// : IfcPositiveLengthMeasure; 	IFC4 OPTIONAL
		internal double mCrossSectionArea = double.NaN;// : IfcAreaMeasure; IFC4 OPTIONAL
		internal double mBarLength = double.NaN;// : OPTIONAL IfcPositiveLengthMeasure;
		internal IfcReinforcingBarTypeEnum mPredefinedType = IfcReinforcingBarTypeEnum.NOTDEFINED;// : IfcReinforcingBarRoleEnum; IFC4 OPTIONAL
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
		public IfcReinforcingBarTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		public IfcReinforcingBarSurfaceEnum BarSurface { get { return mBarSurface; } set { mBarSurface = value; } }

		internal IfcReinforcingBar() : base() { }
		internal IfcReinforcingBar(DatabaseIfc db, IfcReinforcingBar b, DuplicateOptions options) : base(db, b, options)
		{
			mNominalDiameter = b.mNominalDiameter;
			mCrossSectionArea = b.mCrossSectionArea;
			mBarLength = b.mBarLength;
			mPredefinedType = b.mPredefinedType;
			mBarSurface = b.mBarSurface;
		}
		public IfcReinforcingBar(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	[Serializable]
	public partial class IfcReinforcingBarType : IfcReinforcingElementType  //IFC4
	{
		internal IfcReinforcingBarTypeEnum mPredefinedType = IfcReinforcingBarTypeEnum.NOTDEFINED;// : IfcReinforcingBarTypeEnum; //IFC4
		private double mNominalDiameter;// : IfcPositiveLengthMeasure; 	IFC4 OPTIONAL
		internal double mCrossSectionArea;// : IfcAreaMeasure; IFC4 OPTIONAL
		internal double mBarLength;// : OPTIONAL IfcPositiveLengthMeasure;
		internal IfcReinforcingBarSurfaceEnum mBarSurface = IfcReinforcingBarSurfaceEnum.NOTDEFINED;// //: OPTIONAL IfcReinforcingBarSurfaceEnum; 
		internal string mBendingShapeCode = "$";//	:	OPTIONAL IfcLabel;
		internal List<IfcBendingParameterSelect> mBendingParameters = new List<IfcBendingParameterSelect>();//	:	OPTIONAL LIST [1:?] OF IfcBendingParameterSelect;

		public IfcReinforcingBarTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		public double NominalDiameter { get { return mNominalDiameter; } set { mNominalDiameter = value; } }
		public double CrossSectionArea { get { return mCrossSectionArea; } set { mCrossSectionArea = value; } }
		public double BarLength { get { return mBarLength; } set { mBarLength = value; } }
		public IfcReinforcingBarSurfaceEnum BarSurface { get { return mBarSurface; } set { mBarSurface = value; } }
		public string BendingShapeCode { get { return (mBendingShapeCode == "$" ? "" : ParserIfc.Decode(mBendingShapeCode)); } set { mBendingShapeCode = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public ReadOnlyCollection<IfcBendingParameterSelect> BendingParameters { get { return new ReadOnlyCollection<IfcBendingParameterSelect>(mBendingParameters); } }

		internal IfcReinforcingBarType() : base() { }
		internal IfcReinforcingBarType(DatabaseIfc db, IfcReinforcingBarType t, DuplicateOptions options) : base(db, t, options)
		{
			mPredefinedType = t.mPredefinedType;
			mNominalDiameter = t.mNominalDiameter;
			mCrossSectionArea = t.mCrossSectionArea;
			mBarLength = t.mBarLength;
			mBarSurface = t.mBarSurface;
			mBendingShapeCode = t.mBendingShapeCode;
			mBendingParameters.AddRange(t.mBendingParameters);
		}

		public IfcReinforcingBarType(DatabaseIfc m, string name, IfcReinforcingBarTypeEnum type, double diameter)
			: base(m)
		{
			Name = name;
			mPredefinedType = type;
			mNominalDiameter = diameter;
		}
	}
	[Serializable]
	public abstract partial class IfcReinforcingElement : IfcElementComponent //	ABSTRACT SUPERTYPE OF(ONEOF(IfcReinforcingBar, IfcReinforcingMesh, IfcTendon, IfcTendonAnchor))
	{
		private string mSteelGrade = "$";// : OPTIONAL IfcLabel; //IFC4 DEPRECATED 
		[Obsolete("DEPRECATED IFC4", false)]
		public string SteelGrade { get { return (mSteelGrade == "$" ? "" : ParserIfc.Decode(mSteelGrade)); } set { mSteelGrade = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }

		protected IfcReinforcingElement() : base() { }
		protected IfcReinforcingElement(DatabaseIfc db) : base(db) { }
		protected IfcReinforcingElement(DatabaseIfc db, IfcReinforcingElement e, DuplicateOptions options) : base(db, e, options) { mSteelGrade = e.mSteelGrade; }
		public IfcReinforcingElement(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	[Serializable]
	public abstract partial class IfcReinforcingElementType : IfcElementComponentType //IFC4 ABSTRACT SUPERTYPE OF(ONEOF(IfcReinforcingBarType, IfcReinforcingMeshType, IfcTendonAnchorType, IfcTendonType))
	{
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
		internal IfcReinforcingMeshTypeEnum mPredefinedType = IfcReinforcingMeshTypeEnum.NOTDEFINED; //	:	OPTIONAL IfcReinforcingMeshTypeEnum;

		public double MeshLength { get { return mMeshLength; } set { mMeshLength = value; } }
		public double MeshWidth { get { return mMeshWidth; } set { mMeshWidth = value; } }
		public double LongitudinalBarNominalDiameter { get { return mLongitudinalBarNominalDiameter; } set { mLongitudinalBarNominalDiameter = value; } }
		public double TransverseBarNominalDiameter { get { return mTransverseBarNominalDiameter; } set { mTransverseBarNominalDiameter = value; } }
		public double LongitudinalBarCrossSectionArea { get { return mLongitudinalBarCrossSectionArea; } set { mLongitudinalBarCrossSectionArea = value; } }
		public double TransverseBarCrossSectionArea { get { return mTransverseBarCrossSectionArea; } set { mTransverseBarCrossSectionArea = value; } }
		public double LongitudinalBarSpacing { get { return mLongitudinalBarSpacing; } set { mLongitudinalBarSpacing = value; } }
		public double TransverseBarSpacing { get { return mTransverseBarSpacing; } set { mTransverseBarSpacing = value; } }
		public IfcReinforcingMeshTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

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
			mPredefinedType = m.mPredefinedType;
		}
		public IfcReinforcingMesh(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	[Serializable]
	public partial class IfcReinforcingMeshType : IfcReinforcingElementType
	{
		internal IfcReinforcingMeshTypeEnum mPredefinedType = IfcReinforcingMeshTypeEnum.NOTDEFINED; //	:	OPTIONAL IfcReinforcingMeshTypeEnum;
		internal double mMeshLength = double.NaN, mMeshWidth = double.NaN;// : OPTIONAL IfcPositiveLengthMeasure;
		internal double mLongitudinalBarNominalDiameter = double.NaN, mTransverseBarNominalDiameter = double.NaN;// :OPTIONAL IfcPositiveLengthMeasure;
		internal double mLongitudinalBarCrossSectionArea = double.NaN, mTransverseBarCrossSectionArea = double.NaN;// : OPTIONAL IfcAreaMeasure;
		internal double mLongitudinalBarSpacing = double.NaN, mTransverseBarSpacing = double.NaN;// : OPTIONAL IfcPositiveLengthMeasure;
		internal string mBendingShapeCode = "$"; // : OPTIONAL IfcLabel;
		internal List<IfcBendingParameterSelect> mBendingParameters = new List<IfcBendingParameterSelect>(); // : OPTIONAL LIST [1:?] OF IfcBendingParameterSelect;

		public IfcReinforcingMeshTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		public double MeshLength { get { return mMeshLength; } set { mMeshLength = value; } }
		public double MeshWidth { get { return mMeshWidth; } set { mMeshWidth = value; } }
		public double LongitudinalBarNominalDiameter { get { return mLongitudinalBarNominalDiameter; } set { mLongitudinalBarNominalDiameter = value; } }
		public double TransverseBarNominalDiameter { get { return mTransverseBarNominalDiameter; } set { mTransverseBarNominalDiameter = value; } }
		public double LongitudinalBarCrossSectionArea { get { return mLongitudinalBarCrossSectionArea; } set { mLongitudinalBarCrossSectionArea = value; } }
		public double TransverseBarCrossSectionArea { get { return mTransverseBarCrossSectionArea; } set { mTransverseBarCrossSectionArea = value; } }
		public double LongitudinalBarSpacing { get { return mLongitudinalBarSpacing; } set { mLongitudinalBarSpacing = value; } }
		public double TransverseBarSpacing { get { return mTransverseBarSpacing; } set { mTransverseBarSpacing = value; } }
		public string BendingShapeCode { get { return (mBendingShapeCode == "$" ? "" : ParserIfc.Decode(mBendingShapeCode)); } set { mBendingShapeCode = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public ReadOnlyCollection<IfcBendingParameterSelect> BendingParameters { get { return new ReadOnlyCollection<IfcBendingParameterSelect>(mBendingParameters); } }

		internal IfcReinforcingMeshType() : base() { }
		internal IfcReinforcingMeshType(DatabaseIfc db, IfcReinforcingMeshType m, DuplicateOptions options) : base(db, m, options)
		{
			mPredefinedType = m.mPredefinedType;
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
		public IfcReinforcingMeshType(DatabaseIfc db, string name, IfcReinforcingMeshTypeEnum t) : base(db) { Name = name; mPredefinedType = t; }
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
		internal IfcRelAggregates(IfcObjectDefinition relObject) 
			: base(relObject.mDatabase) { RelatingObject = relObject; }
		public IfcRelAggregates(IfcObjectDefinition relObject, IfcObjectDefinition relatedObject) : this(relObject, new List<IfcObjectDefinition>() { relatedObject }) { }
		public IfcRelAggregates(IfcObjectDefinition relObject, IEnumerable<IfcObjectDefinition> relatedObjects) : this(relObject)
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
		private int mTimeForTask;// :  	OPTIONAL IfcScheduleTimeControl; 

		public IfcScheduleTimeControl TimeForTask
		{
			get { return mDatabase[mTimeForTask] as IfcScheduleTimeControl; }
			set { mTimeForTask = value == null ? 0 : value.mIndex; if (value != null) value.mScheduleTimeControlAssigned = this; }
		}
		internal IfcWorkControl WorkControl { get { return mRelatingControl as IfcWorkControl; } }

		internal new ReadOnlyCollection<IfcTask> RelatedObjects { get { return new ReadOnlyCollection<IfcTask>(base.RelatedObjects.Cast<IfcTask>().ToList()); } }

		internal IfcRelAssignsTasks() : base() { }
		internal IfcRelAssignsTasks(DatabaseIfc db, IfcRelAssignsTasks r, DuplicateOptions options) : base(db, r, options) { if (r.mTimeForTask > 0) TimeForTask = db.Factory.Duplicate(r.TimeForTask, options) as IfcScheduleTimeControl; }
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
		public IfcRelAssignsToControl(IfcControl relControl) : base(relControl.mDatabase) { RelatingControl = relControl;  }
		public IfcRelAssignsToControl(IfcControl relControl, IfcObjectDefinition relObject) : base(relObject) { RelatingControl = relControl; }
		public IfcRelAssignsToControl(IfcControl relControl, IEnumerable<IfcObjectDefinition> relObjects) : base(relObjects) { RelatingControl = relControl; }
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
			RelatingGroup = db.Factory.Duplicate(a.RelatingGroup, new DuplicateOptions(options.DeviationTolerance) { DuplicateDownstream = false }) as IfcGroup;
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
		internal int mRelatingProcess;// : IfcProcessSelect
		internal IfcMeasureWithUnit mQuantityInProcess = null;//	 : 	OPTIONAL IfcMeasureWithUnit;
		public IfcProcessSelect RelatingProcess { get { return mDatabase[mRelatingProcess] as IfcProcessSelect; } set { mRelatingProcess = value.StepId; value.OperatesOn.Add(this); } }
		public IfcMeasureWithUnit QuantityInProcess { get { return mQuantityInProcess; } set { mQuantityInProcess = value; } }

		public override NamedObjectIfc Relating() { return RelatingProcess as NamedObjectIfc; } 

		internal IfcRelAssignsToProcess() : base() { }
		internal IfcRelAssignsToProcess(DatabaseIfc db, IfcRelAssignsToProcess r, DuplicateOptions options) : base(db, r, options)
		{
			RelatingProcess = db.Factory.Duplicate(r.RelatingProcess as BaseClassIfc, new DuplicateOptions(options) { DuplicateDownstream = false }) as IfcProcess;
		}
		public IfcRelAssignsToProcess(IfcProcessSelect relProcess) : base(relProcess.Database) { RelatingProcess = relProcess; }
		public IfcRelAssignsToProcess(IfcProcessSelect relProcess, IfcObjectDefinition related) : base(related) { RelatingProcess = relProcess; }
		public IfcRelAssignsToProcess(IfcProcessSelect relProcess, IEnumerable<IfcObjectDefinition> related) : base(related) { RelatingProcess = relProcess; }
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
		public IfcRelAssignsToProduct(IfcProductSelect relProduct) : base(relProduct.Database) { RelatingProduct = relProduct; }
		public IfcRelAssignsToProduct(IfcObjectDefinition relObject, IfcProductSelect relProduct) : base(relObject) { RelatingProduct = relProduct; }
		public IfcRelAssignsToProduct(IEnumerable<IfcObjectDefinition> relObjects, IfcProductSelect relProduct) : base(relObjects) { RelatingProduct = relProduct; }
	}
	//[Obsolete("DEPRECATED IFC4", false)]
	//ENTITY IfcRelAssignsToProjectOrder // DEPRECATED IFC4 
	[Serializable]
	public partial class IfcRelAssignsToResource : IfcRelAssigns
	{
		internal int mRelatingResource;// : IfcResourceSelect; 
		public IfcResourceSelect RelatingResource { get { return mDatabase[mRelatingResource] as IfcResourceSelect; } set { mRelatingResource = value.StepId; value.ResourceOf.Add(this); } }

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
		//internal int mID = 0;
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
		internal int mRelatingApproval;// : IfcApproval; 
		public IfcApproval RelatingApproval { get { return mDatabase[mRelatingApproval] as IfcApproval; } set { mRelatingApproval = value.Index; value.ApprovedObjects.Add(this); } }

		public override NamedObjectIfc Relating() { return RelatingApproval; }

		internal IfcRelAssociatesApproval() : base() { }
		internal IfcRelAssociatesApproval(DatabaseIfc db, IfcRelAssociatesApproval r, DuplicateOptions options) : base(db, r, options) { RelatingApproval = db.Factory.Duplicate(r.mDatabase[r.mRelatingApproval], options) as IfcApproval; }
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
		internal IfcRelAssociatesClassification(DatabaseIfc db, IfcRelAssociatesClassification r, DuplicateOptions options) : base(db, r, options)
		{
			RelatingClassification = db.Factory.Duplicate(r.RelatingClassification) as IfcClassificationSelect;
		}
		public IfcRelAssociatesClassification(IfcClassificationSelect classification) : base(classification.Database) { RelatingClassification = classification; }
		public IfcRelAssociatesClassification(IfcClassificationSelect classification, IfcDefinitionSelect related) : base(related) { RelatingClassification = classification; }
		public IfcRelAssociatesClassification(IfcClassificationSelect classification, IEnumerable<IfcDefinitionSelect> related) : base(related) { RelatingClassification = classification; }
	}
	[Serializable]
	public partial class IfcRelAssociatesConstraint : IfcRelAssociates
	{
		internal string mIntent = "$";// :	OPTIONAL IfcLabel;
		private int mRelatingConstraint;// : IfcConstraint

		public string Intent { get { return (mIntent == "$" ? "" : ParserIfc.Decode(mIntent)); } set { mIntent = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public IfcConstraint RelatingConstraint { get { return mDatabase[mRelatingConstraint] as IfcConstraint; } set { mRelatingConstraint = value.mIndex; value.mConstraintForObjects.Add(this); } }

		public override NamedObjectIfc Relating() { return RelatingConstraint; } 

		internal IfcRelAssociatesConstraint() : base() { }
		internal IfcRelAssociatesConstraint(DatabaseIfc db, IfcRelAssociatesConstraint c, DuplicateOptions options) : base(db, c, options) { RelatingConstraint = db.Factory.Duplicate(c.RelatingConstraint) as IfcConstraint; }
		public IfcRelAssociatesConstraint(IfcConstraint c) : base(c.mDatabase) { RelatingConstraint = c; }
		public IfcRelAssociatesConstraint(IfcDefinitionSelect related, IfcConstraint constraint) : base(related) { RelatingConstraint = constraint; }
	}
	[Serializable]
	public partial class IfcRelAssociatesDocument : IfcRelAssociates
	{
		internal int mRelatingDocument;// : IfcDocumentSelect; 
		public IfcDocumentSelect RelatingDocument { get { return mDatabase[mRelatingDocument] as IfcDocumentSelect; } set { mRelatingDocument = value.Index; value.Associate(this); } }

		public override NamedObjectIfc Relating() { return RelatingDocument; } 

		internal IfcRelAssociatesDocument() : base() { }
		internal IfcRelAssociatesDocument(DatabaseIfc db, IfcRelAssociatesDocument r, DuplicateOptions options) : base(db, r, options) { RelatingDocument = db.Factory.Duplicate(r.mDatabase[r.mRelatingDocument], options) as IfcDocumentSelect; }
		public IfcRelAssociatesDocument(IfcDocumentSelect document) : base(document.Database) { RelatingDocument = document; }
		public IfcRelAssociatesDocument(IfcDefinitionSelect related, IfcDocumentSelect document) : base(related) { RelatingDocument = document; }
		public IfcRelAssociatesDocument(IEnumerable<IfcDefinitionSelect> related, IfcDocumentSelect document) : base(related) { RelatingDocument = document; }
	}
	[Serializable]
	public partial class IfcRelAssociatesLibrary : IfcRelAssociates
	{
		internal int mRelatingLibrary;// : IfcLibrarySelect; 
		public IfcLibrarySelect RelatingLibrary { get { return mDatabase[mRelatingLibrary] as IfcLibrarySelect; } set { mRelatingLibrary = value.Index; } }

		public override NamedObjectIfc Relating() { return RelatingLibrary; } 

		internal IfcRelAssociatesLibrary() : base() { }
		internal IfcRelAssociatesLibrary(DatabaseIfc db, IfcRelAssociatesLibrary r, DuplicateOptions options) : base(db, r, options) { RelatingLibrary = db.Factory.Duplicate(r.mDatabase[r.mRelatingLibrary], options) as IfcLibrarySelect; }
		public IfcRelAssociatesLibrary(IfcLibrarySelect library, IfcDefinitionSelect related) : base(related) { RelatingLibrary = library; }
	}
	[Serializable]
	public partial class IfcRelAssociatesMaterial : IfcRelAssociates
	{
		private int mRelatingMaterial;// : IfcMaterialSelect; 
		public IfcMaterialSelect RelatingMaterial
		{
			get { return mDatabase[mRelatingMaterial] as IfcMaterialSelect; }
			set
			{
				IfcMaterialSelect material = RelatingMaterial;
				if (material != null)
					material.AssociatedTo.Remove(this);
				mRelatingMaterial = (value == null ? 0 : value.Index);
				if (value != null)
					value.AssociatedTo.Add(this);
			}
		}

		public override NamedObjectIfc Relating() { return RelatingMaterial; } 

		internal IfcRelAssociatesMaterial() : base() { }
		internal IfcRelAssociatesMaterial(DatabaseIfc db, IfcRelAssociatesMaterial r, DuplicateOptions options) : base(db, r, options)
		{
			RelatingMaterial = db.Factory.Duplicate(r.mDatabase[r.mRelatingMaterial], options) as IfcMaterialSelect;
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
		private int mRelatingProfileProperties;// : IfcProfileProperties;
		internal int mProfileSectionLocation;// : OPTIONAL IfcShapeAspect;
		internal double mProfileOrientationValue = double.NaN;// : OPTIONAL IfcOrientationSelect; //TYPE IfcOrientationSelect = SELECT(IfcPlaneAngleMeasure,IfcDirection);
		internal int mProfileOrientation = 0; // : OPTIONAL IfcOrientationSelect;

		public IfcProfileProperties RelatingProfileProperties { get { return mDatabase[mRelatingProfileProperties] as IfcProfileProperties; } set { mRelatingProfileProperties = value.mIndex; } }
		public IfcShapeAspect ProfileSectionLocation { get { return mDatabase[mProfileSectionLocation] as IfcShapeAspect; } set { mProfileSectionLocation = value == null ? 0 : value.mIndex; } }

		public override NamedObjectIfc Relating() { return RelatingProfileProperties; } 

		internal IfcRelAssociatesProfileProperties() : base() { }
		internal IfcRelAssociatesProfileProperties(DatabaseIfc db, IfcRelAssociatesProfileProperties r, DuplicateOptions options) : base(db, r, options)
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
		public IfcRelAssociatesProfileProperties(IfcProfileProperties pp) : base(pp.mDatabase) { mRelatingProfileProperties = pp.mIndex; }
		public IfcRelAssociatesProfileProperties(IfcDefinitionSelect related, IfcProfileProperties pp) : base(related) { if (pp.mDatabase.mRelease > ReleaseVersion.IFC2x3) throw new Exception(StepClassName + " Deleted in IFC4"); mRelatingProfileProperties = pp.mIndex; }
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
		internal int mRelatingPort;// : IfcPort;
		internal int mRelatedPort;// : IfcPort;
		internal int mRealizingElement;// : OPTIONAL IfcElement; 

		public IfcPort RelatingPort { get { return mDatabase[mRelatingPort] as IfcPort; } set { mRelatingPort = value.mIndex; value.mConnectedFrom = this; } }
		public IfcPort RelatedPort { get { return mDatabase[mRelatedPort] as IfcPort; } set { mRelatedPort = value.mIndex; value.mConnectedTo = this; } }
		public IfcElement RealizingElement { get { return mDatabase[mRealizingElement] as IfcElement; } set { mRealizingElement = value == null ? 0 : value.mIndex; } }

		internal IfcRelConnectsPorts() : base() { }
		internal IfcRelConnectsPorts(DatabaseIfc db, IfcRelConnectsPorts r, DuplicateOptions options) : base(db, r, options)
		{
			RelatingPort = db.Factory.Duplicate(r.RelatingPort, options) as IfcPort;
			RelatedPort = db.Factory.Duplicate(r.RelatedPort, options) as IfcPort;
			if (r.mRealizingElement > 0)
				RealizingElement = db.Factory.Duplicate(r.RealizingElement, options) as IfcElement;
		}
		public IfcRelConnectsPorts(IfcPort relatingPort, IfcPort relatedPort) : base(relatingPort.mDatabase) { RelatingPort = relatingPort; RelatedPort = relatedPort; }

		public IfcPort ConnectedPort(IfcPort p) { return (mRelatedPort == p.mIndex ? mDatabase[mRelatingPort] as IfcPort : mDatabase[mRelatedPort] as IfcPort); }
	}
	[Serializable]
	public partial class IfcRelConnectsPortToElement : IfcRelConnects
	{
		internal int mRelatingPort;// : IfcPort;
		internal int mRelatedElement;// : IfcElement; IFC4 IfcDistributionElement

		public IfcPort RelatingPort { get { return mDatabase[mRelatingPort] as IfcPort; } set { mRelatingPort = value.mIndex; value.mContainedIn = this; } }
		public IfcElement RelatedElement { get { return mDatabase[mRelatedElement] as IfcElement; } set { mRelatedElement = value.mIndex; value.HasPortsSS.Add(this); } }

		internal IfcRelConnectsPortToElement() : base() { }
		internal IfcRelConnectsPortToElement(DatabaseIfc db, IfcRelConnectsPortToElement r, DuplicateOptions options) : base(db, r, options)
		{
			RelatingPort = db.Factory.Duplicate(r.RelatingPort, options) as IfcPort;
			RelatedElement = db.Factory.Duplicate(r.RelatedElement, options) as IfcElement;
		}
		public IfcRelConnectsPortToElement(IfcPort p, IfcElement e) : base(p.mDatabase)
		{
			mRelatingPort = p.mIndex;
			p.mContainedIn = this;
			mRelatedElement = e.mIndex;
			e.mHasPorts.Add(this);
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
		public IfcStructuralMember RelatedStructuralMember { get { return mRelatedStructuralMember as IfcStructuralMember; } set { mRelatedStructuralMember = value; value.mStructuralMemberForGG = this; } }

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
		internal IfcRelConnectsStructuralMember(DatabaseIfc db, IfcRelConnectsStructuralMember r, DuplicateOptions options) : base(db, r, options)
		{
			//RelatingStructuralMember = db.Factory.Duplicate(r.RelatingStructuralMember) as IfcStructuralMember; 
			RelatedStructuralConnection = db.Factory.Duplicate(r.RelatedStructuralConnection) as IfcStructuralConnection;
			if (r.mAppliedCondition > 0)
				AppliedCondition = db.Factory.Duplicate(r.AppliedCondition) as IfcBoundaryCondition;
			if (r.mAdditionalConditions > 0)
				AdditionalConditions = db.Factory.Duplicate(r.AdditionalConditions) as IfcStructuralConnectionCondition;
			mSupportedLength = r.mSupportedLength;
			if (r.mConditionCoordinateSystem > 0)
				ConditionCoordinateSystem = db.Factory.Duplicate(r.ConditionCoordinateSystem) as IfcAxis2Placement3D;
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
		internal int mConnectionConstraint;// : IfcConnectionGeometry
		public IfcConnectionGeometry ConnectionConstraint { get { return mDatabase[mConnectionConstraint] as IfcConnectionGeometry; } set { mConnectionConstraint = value.mIndex; } }

		internal IfcRelConnectsWithEccentricity() : base() { }
		internal IfcRelConnectsWithEccentricity(DatabaseIfc db, IfcRelConnectsWithEccentricity c, DuplicateOptions options) : base(db, c, options) { ConnectionConstraint = db.Factory.Duplicate(c.ConnectionConstraint) as IfcConnectionGeometry; }
		public IfcRelConnectsWithEccentricity(IfcStructuralMember memb, IfcStructuralConnection connection, IfcConnectionGeometry cg)
			: base(memb, connection) { mConnectionConstraint = cg.mIndex; }
	}
	public partial class IfcRelConnectsWithRealizingElements : IfcRelConnectsElements
	{
		internal SET<IfcElement> mRealizingElements = new SET<IfcElement>();// :	SET [1:?] OF IfcElement;
		internal string mConnectionType = "$";// : :	OPTIONAL IfcLabel; 

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
			RelatingStructure = db.Factory.Duplicate(r.RelatingStructure, new DuplicateOptions(options) { DuplicateDownstream = false }) as IfcSpatialElement;
			if (options.DuplicateDownstream)
			{
				DuplicateOptions optionsNoHost = new DuplicateOptions(options) { DuplicateHost = false };
				RelatedElements.AddRange(r.RelatedElements.Select(x => db.Factory.Duplicate(x, optionsNoHost) as IfcProduct));
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
		public SET<IfcDefinitionSelect> RelatedDefinitions { get { return mRelatedDefinitions; } set { mRelatedDefinitions.Clear(); if (value != null) { mRelatedDefinitions = value; } } }

		internal IfcRelDeclares() : base() { }
		internal IfcRelDeclares(IfcContext c) : base(c.mDatabase) { mRelatingContext = c; c.mDeclares.Add(this); }
		internal IfcRelDeclares(DatabaseIfc db, IfcRelDeclares r, DuplicateOptions options) : base(db, r, options)
		{
			RelatingContext = db.Factory.Duplicate(r.RelatingContext, new DuplicateOptions(options) { DuplicateDownstream = false }) as IfcContext;
			if (options.DuplicateDownstream)
				RelatedDefinitions.AddRange(r.RelatedDefinitions.ConvertAll(x => db.Factory.Duplicate(r.mDatabase[x.Index], options) as IfcDefinitionSelect));
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
		internal List<int> mRelatedObjects = new List<int>();// : SET [1:?] OF IfcObject;
		internal int mRelatingObject;// : IfcObject  

		public ReadOnlyCollection<IfcObject> RelatedObjects { get { return new ReadOnlyCollection<IfcObject>(mRelatedObjects.ConvertAll(x => mDatabase[x] as IfcObject)); } }
		public IfcObject RelatingObject { get { return mDatabase[mRelatingObject] as IfcObject; } set { mRelatingObject = value.mIndex; } }

		public override IfcRoot Relating() { return RelatingObject; } 

		internal IfcRelDefinesByObject() : base() { }
		internal IfcRelDefinesByObject(DatabaseIfc db, IfcRelDefinesByObject r, DuplicateOptions options) : base(db, r, options) { r.RelatedObjects.ToList().ForEach(x => addRelated(db.Factory.Duplicate(x, options) as IfcObject)); RelatingObject = db.Factory.Duplicate(r.RelatingObject, options) as IfcObject; }
		public IfcRelDefinesByObject(IfcObject relObj) : base(relObj.mDatabase) { mRelatingObject = relObj.mIndex; relObj.mIsDeclaredBy = this; }

		internal void addRelated(IfcObject obj) { mRelatedObjects.Add(obj.mIndex); obj.mIsDeclaredBy = this; }
	}
	[Serializable]
	public partial class IfcRelDefinesByProperties : IfcRelDefines
	{
		private SET<IfcObjectDefinition> mRelatedObjects = new SET<IfcObjectDefinition>();// IFC4 change SET [1:?] OF IfcObjectDefinition; ifc2x3 : SET [1:?] OF IfcObject;  
		private IfcPropertySetDefinition mRelatingPropertyDefinition;// : IfcPropertySetDefinitionSelect; 

		public SET<IfcObjectDefinition> RelatedObjects { get { return mRelatedObjects; } }
		public IfcPropertySetDefinition RelatingPropertyDefinition { get { return mRelatingPropertyDefinition; } set { mRelatingPropertyDefinition = value; if (value != null) value.DefinesOccurrence.Add(this); } }

		public override IfcRoot Relating() { return RelatingPropertyDefinition as IfcRoot; } 

		internal IfcRelDefinesByProperties() : base() { }
		private IfcRelDefinesByProperties(DatabaseIfc db) : base(db) 
		{
			Name = "NameRelDefinesByProperties";
			Description = "DescriptionRelDefinesByProperties";
		}
		internal IfcRelDefinesByProperties(DatabaseIfc db, IfcRelDefinesByProperties d, DuplicateOptions options) : base(db, d, options)
		{
			RelatingPropertyDefinition = db.Factory.Duplicate(d.RelatingPropertyDefinition as BaseClassIfc, options) as IfcPropertySetDefinition;
		}
		public IfcRelDefinesByProperties(IfcPropertySetDefinition propertySet) 
			: base(propertySet.Database) 
		{
			mRelatingPropertyDefinition = propertySet; 
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
		protected override List<T> Extract<T>(Type type)
		{
			List<T> result = base.Extract<T>(type);
			result.AddRange(RelatingPropertyDefinition.Extract<T>());
			return result;
		}
	}
	[Serializable]
	public partial class IfcRelDefinesByTemplate : IfcRelDefines //IFC4
	{
		internal List<int> mRelatedPropertySets = new List<int>();// : SET [1:?] OF IfcPropertySetDefinition;
		internal int mRelatingTemplate;// :	IfcPropertySetTemplate;

		public ReadOnlyCollection<IfcPropertySetDefinition> RelatedPropertySets { get { return new ReadOnlyCollection<IfcPropertySetDefinition>(mRelatedPropertySets.ConvertAll(x => mDatabase[x] as IfcPropertySetDefinition)); } }
		public IfcPropertySetTemplate RelatingTemplate
		{
			get { return mDatabase[mRelatingTemplate] as IfcPropertySetTemplate; }
			set { mRelatingTemplate = value.mIndex; }
		}

		public override IfcRoot Relating() { return RelatingTemplate; } 

		internal IfcRelDefinesByTemplate() : base() { }
		internal IfcRelDefinesByTemplate(DatabaseIfc db, IfcRelDefinesByTemplate r, DuplicateOptions options) : base(db, r, options) { r.RelatedPropertySets.ToList().ForEach(x => AddRelated(db.Factory.Duplicate(x, options) as IfcPropertySetDefinition)); RelatingTemplate = db.Factory.Duplicate(r.RelatingTemplate, options) as IfcPropertySetTemplate; }
		public IfcRelDefinesByTemplate(IfcPropertySetTemplate relating) : base(relating.mDatabase) { RelatingTemplate = relating; }
		public IfcRelDefinesByTemplate(IfcPropertySetDefinition related, IfcPropertySetTemplate relating) : this(relating) { AddRelated(related); }
		public IfcRelDefinesByTemplate(List<IfcPropertySetDefinition> related, IfcPropertySetTemplate relating) : this(relating) { related.ForEach(x => AddRelated(x)); }

		public void AddRelated(IfcPropertySetDefinition pset) { mRelatedPropertySets.Add(pset.mIndex); pset.mIsDefinedBy.Add(this); }
	}
	[Serializable]
	public partial class IfcRelDefinesByType : IfcRelDefines
	{
		internal SET<IfcObject> mRelatedObjects = new SET<IfcObject>();// : SET [1:?] OF IfcObject;
		private IfcTypeObject mRelatingType = null;// : IfcTypeObject  

		public SET<IfcObject> RelatedObjects { get { return mRelatedObjects; } set { mRelatedObjects.Clear(); if (value != null) { mRelatedObjects.CollectionChanged -= mRelatedObjects_CollectionChanged; mRelatedObjects = value; mRelatedObjects.CollectionChanged += mRelatedObjects_CollectionChanged; } } }
		public IfcTypeObject RelatingType { get { return mRelatingType; } set { mRelatingType = value; if(value != null) value.mObjectTypeOf = this; } }

		public override IfcRoot Relating() { return RelatingType; } 

		internal IfcRelDefinesByType() : base() { }
		internal IfcRelDefinesByType(DatabaseIfc db, IfcRelDefinesByType r, DuplicateOptions options) : base(db, r, options)
		{
			//mRelatedObjects = new List<int>(d.mRelatedObjects.ToArray()); 
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
		private int mRelatingOpeningElement;// : IfcOpeningElement;
		private int mRelatedBuildingElement;// :OPTIONAL IfcElement; 

		public IfcOpeningElement RelatingOpeningElement { get { return mDatabase[mRelatingOpeningElement] as IfcOpeningElement; } set { mRelatingOpeningElement = value.mIndex; value.mHasFillings.Add(this); } }
		public IfcElement RelatedBuildingElement { get { return mDatabase[mRelatedBuildingElement] as IfcElement; } set { mRelatedBuildingElement = value.mIndex; value.mFillsVoids.Add(this); } }

		internal IfcRelFillsElement() : base() { }
		internal IfcRelFillsElement(DatabaseIfc db, IfcRelFillsElement r, DuplicateOptions options) : base(db, r, options) { RelatingOpeningElement = db.Factory.Duplicate(r.RelatingOpeningElement, options) as IfcOpeningElement; RelatedBuildingElement = db.Factory.Duplicate(r.RelatedBuildingElement, options) as IfcElement; }
		public IfcRelFillsElement(IfcOpeningElement oe, IfcElement e) : base(oe.mDatabase) { mRelatingOpeningElement = oe.mIndex; mRelatedBuildingElement = e.mIndex; }
	}
	[Serializable]
	public partial class IfcRelFlowControlElements : IfcRelConnects
	{
		internal int mRelatingPort;// : IfcPort;
		internal int mRelatedElement;// : IfcElement; 
		public IfcPort RelatingPort { get { return mDatabase[mRelatingPort] as IfcPort; } set { mRelatingPort = value.mIndex; } }
		public IfcElement RelatedElement { get { return mDatabase[mRelatedElement] as IfcElement; } set { mRelatedElement = value.mIndex; } }

		internal IfcRelFlowControlElements() : base() { }
		internal IfcRelFlowControlElements(DatabaseIfc db, IfcRelFlowControlElements r, DuplicateOptions options) : base(db, r, options) { RelatingPort = db.Factory.Duplicate(r.RelatingPort, options) as IfcPort; RelatedElement = db.Factory.Duplicate(r.RelatedElement, options) as IfcElement; }
	}
	//[Obsolete("DEPRECATED IFC4", false)]
	//ENTITY IfcRelInteractionRequirements  // DEPRECATED IFC4
	[Serializable]
	public partial class IfcRelInterferesElements : IfcRelConnects
	{
		internal int mRelatingElement;// : IfcInterferenceSelect;
		internal int mRelatedElement;// : IfcInterferenceSelect;
		internal int mInterferenceGeometry;// : OPTIONAL IfcConnectionGeometry; 
		internal string mInterferenceType = "$";// : OPTIONAL IfcIdentifier;
		internal IfcLogicalEnum mImpliedOrder = IfcLogicalEnum.UNKNOWN;// : LOGICAL;

		public IfcInterferenceSelect RelatingElement { get { return mDatabase[mRelatingElement] as IfcInterferenceSelect; } set { mRelatingElement = value.StepId; value.InterferesElements.Add(this); } }
		public IfcInterferenceSelect RelatedElement { get { return mDatabase[mRelatedElement] as IfcInterferenceSelect; } set { mRelatedElement = value.StepId; value.IsInterferedByElements.Add(this); } }
		public IfcConnectionGeometry InterferenceGeometry { get { return mDatabase[mInterferenceGeometry] as IfcConnectionGeometry; } set { mInterferenceGeometry = value == null ? 0 : value.mIndex; } }
		public string InterferenceType { get { return (mInterferenceType == "$" ? "" : ParserIfc.Decode(mInterferenceType)); } set { mInterferenceType = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public IfcLogicalEnum ImpliedOrder { get { return mImpliedOrder; } }

		internal IfcRelInterferesElements() : base() { }
		internal IfcRelInterferesElements(DatabaseIfc db, IfcRelInterferesElements r, DuplicateOptions options) : base(db, r, options)
		{
			RelatingElement = db.Factory.Duplicate(r.RelatingElement as BaseClassIfc, options) as IfcInterferenceSelect;
			RelatedElement = db.Factory.Duplicate(r.RelatedElement as BaseClassIfc, options) as IfcInterferenceSelect;
			if (r.mInterferenceGeometry > 0)
				InterferenceGeometry = db.Factory.Duplicate(r.InterferenceGeometry, options) as IfcConnectionGeometry;
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
			RelatedObjects.AddRange(n.RelatedObjects.Select(x => db.Factory.Duplicate(x, optionsNoHost) as IfcObjectDefinition));
		}
		public IfcRelNests(IfcObjectDefinition relatingObject) : base(relatingObject.mDatabase)
		{
			mRelatingObject = relatingObject;
			relatingObject.IsNestedBy.Add(this);
		}
		public IfcRelNests(IfcObjectDefinition relatingObject, IfcObjectDefinition relatedObject) : base(relatingObject.mDatabase)
		{
			mRelatingObject = relatingObject;
			relatingObject.IsNestedBy.Add(this);
			relatedObject.Nests = this;
		}
		public IfcRelNests(IfcObjectDefinition relatingObject, IfcObjectDefinition ro, IfcObjectDefinition ro2) : this(relatingObject, ro) { ro2.Nests = this; }
		public IfcRelNests(IfcObjectDefinition relatingObject, List<IfcObjectDefinition> relatedObjects) : base(relatingObject.mDatabase)
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
		private List<int> mOverridingProperties = new List<int>();// : 	SET [1:?] OF IfcProperty;

		public ReadOnlyCollection<IfcProperty> OverridingProperties { get { return new ReadOnlyCollection<IfcProperty>(mOverridingProperties.ConvertAll(x => mDatabase[x] as IfcProperty)); } }

		internal IfcRelOverridesProperties() : base() { }
		internal IfcRelOverridesProperties(DatabaseIfc db, IfcRelOverridesProperties d, DuplicateOptions options) : base(db, d, options)
		{
			mOverridingProperties = d.OverridingProperties.ToList().ConvertAll(x => db.Factory.Duplicate(x, options).mIndex);
		}
		internal IfcRelOverridesProperties(IfcPropertySetDefinition ifcproperty) : base(ifcproperty) { }
		public IfcRelOverridesProperties(IfcObjectDefinition od, IfcPropertySetDefinition ifcproperty) : base(od, ifcproperty) { }
		public IfcRelOverridesProperties(List<IfcObjectDefinition> objs, IfcPropertySetDefinition ifcproperty) : base(objs, ifcproperty) { }
	}
	[Serializable]
	public partial class IfcRelPositions : IfcRelConnects
	{
		private IfcPositioningElement mRelatingPositioningElement = null; //: IfcPositioningElement;
		private SET<IfcProduct> mRelatedProducts = new SET<IfcProduct>(); //: SET[1:?] OF IfcProduct;

		public IfcPositioningElement RelatingPositioningElement 
		{
			get { return mRelatingPositioningElement; } 
			set { mRelatingPositioningElement = value; if (value != null) value.Positions.Add(this); }
		}
		public SET<IfcProduct> RelatedProducts { get { return mRelatedProducts; } set { mRelatedProducts = value; } }

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
					product.PositionedRelativeTo = this; ;
			}
			if (e.OldItems != null)
			{
				foreach (IfcProduct product in e.OldItems)
					product.PositionedRelativeTo = null;
			}
		}
	}
	[Serializable]
	public partial class IfcRelProjectsElement : IfcRelDecomposes // IFC2x3 IfcRelDecomposes
	{
		internal int mRelatingElement;// : IfcElement; 
		internal int mRelatedFeatureElement;// : IfcFeatureElementAddition

		public IfcElement RelatingElement { get { return mDatabase[mRelatingElement] as IfcElement; } set { mRelatingElement = value.mIndex; value.HasProjections.Add(this); } }
		public IfcFeatureElementAddition RelatedFeatureElement { get { return mDatabase[mRelatedFeatureElement] as IfcFeatureElementAddition; } set { mRelatedFeatureElement = value.mIndex; value.mProjectsElements.Add(this); } }

		protected IfcRelProjectsElement() : base() { }
		protected IfcRelProjectsElement(DatabaseIfc db, IfcRelProjectsElement p, DuplicateOptions options) : base(db, p, options) { RelatingElement = db.Factory.Duplicate(p.RelatingElement, options) as IfcElement; RelatedFeatureElement = db.Factory.Duplicate(p.RelatedFeatureElement, options) as IfcFeatureElementAddition; }
		protected IfcRelProjectsElement(IfcElement e, IfcFeatureElementAddition a) : base(e.mDatabase) { mRelatingElement = e.mIndex; mRelatedFeatureElement = a.mIndex; }
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
		public IfcRelReferencedInSpatialStructure(IfcSpatialReferenceSelect related, IfcSpatialElement relating) : this(relating)
		{
			RelatedElements.Add(related);
		}

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
		internal int mRelatingProcess;// : IfcProcess;
		internal int mRelatedProcess;//  IfcProcess;
		private int mTimeLag;// : OPTIONAL IfcLagTime; IFC2x3 	IfcTimeMeasure
		private double mTimeLagSS = double.NaN;// : OPTIONAL IfcLagTime; IFC2x3 	IfcTimeMeasure
		internal IfcSequenceEnum mSequenceType = IfcSequenceEnum.NOTDEFINED;//	 :	OPTIONAL IfcSequenceEnum;
		internal string mUserDefinedSequenceType = "$";//	 :	OPTIONAL IfcLabel; 

		public IfcProcess RelatingProcess { get { return mDatabase[mRelatingProcess] as IfcProcess; } set { mRelatingProcess = value.mIndex; value.mIsPredecessorTo.Add(this); } }
		public IfcProcess RelatedProcess { get { return mDatabase[mRelatedProcess] as IfcProcess; } set { mRelatedProcess = value.mIndex; value.mIsSuccessorFrom.Add(this); } }
		public IfcLagTime TimeLag { get { return mDatabase[mTimeLag] as IfcLagTime; } set { mTimeLag = (value == null ? 0 : value.mIndex); } }

		internal IfcRelSequence() : base() { }
		internal IfcRelSequence(DatabaseIfc db, IfcRelSequence s, DuplicateOptions options) : base(db, s, options)
		{
			RelatingProcess = db.Factory.Duplicate(s.RelatingProcess, options) as IfcProcess;
			RelatedProcess = db.Factory.Duplicate(s.RelatedProcess, options) as IfcProcess;
			mTimeLag = s.mTimeLag;
			mSequenceType = s.mSequenceType;
			mUserDefinedSequenceType = s.mUserDefinedSequenceType;
		}
		public IfcRelSequence(IfcProcess rg, IfcProcess rd) : base(rg.mDatabase)
		{
			mRelatingProcess = rg.mIndex;
			mRelatedProcess = rd.mIndex;
		}

		internal IfcProcess getPredecessor() { return mDatabase[mRelatingProcess] as IfcProcess; }
		internal IfcProcess getSuccessor() { return mDatabase[mRelatedProcess] as IfcProcess; }
		internal TimeSpan getLag()
		{
			if (mDatabase.mRelease < ReleaseVersion.IFC4) return new TimeSpan(0, 0, (int)mTimeLag);
			IfcLagTime lt = mDatabase[(int)mTimeLag] as IfcLagTime;
			return (lt == null ? new TimeSpan(0, 0, 0) : lt.getLag());
		}
	}
	[Serializable]
	[Obsolete("DEPRECATED IFC4x3", false)]
	public partial class IfcRelServicesBuildings : IfcRelConnects
	{
		internal IfcSystem mRelatingSystem;// : IfcSystem;
		internal SET<IfcSpatialElement> mRelatedBuildings = new SET<IfcSpatialElement>();// : SET [1:?] OF IfcSpatialElement  ;

		public IfcSystem RelatingSystem { get { return mRelatingSystem; } set { mRelatingSystem = value; value.ServicesBuildings = this; } }
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
		internal int mRelatingSpace;// :	IfcSpaceBoundarySelect; : IfcSpace;
		internal IfcElement mRelatedBuildingElement;// :OPTIONAL IfcElement; Mandatory in IFC4
		internal int mConnectionGeometry;// : OPTIONAL IfcConnectionGeometry;
		internal IfcPhysicalOrVirtualEnum mPhysicalOrVirtualBoundary = IfcPhysicalOrVirtualEnum.NOTDEFINED;// : IfcPhysicalOrVirtualEnum;
		internal IfcInternalOrExternalEnum mInternalOrExternalBoundary = IfcInternalOrExternalEnum.NOTDEFINED;// : IfcInternalOrExternalEnum; 

		public IfcSpaceBoundarySelect RelatingSpace { get { return mDatabase[mRelatingSpace] as IfcSpaceBoundarySelect; } set { mRelatingSpace = value.Index; value.AddBoundary(this); } }
		public IfcElement RelatedBuildingElement { get { return mRelatedBuildingElement; } set { mRelatedBuildingElement = value; if (value != null) value.ProvidesBoundaries.Add(this); } }
		public IfcConnectionGeometry ConnectionGeometry { get { return mDatabase[mConnectionGeometry] as IfcConnectionGeometry; } set { mConnectionGeometry = (value == null ? 0 : value.mIndex); } }

		internal IfcRelSpaceBoundary() : base() { }
		internal IfcRelSpaceBoundary(DatabaseIfc db, IfcRelSpaceBoundary b, DuplicateOptions options) : base(db, b, options)
		{
			RelatingSpace = db.Factory.Duplicate(b.mDatabase[b.mRelatingSpace], options) as IfcSpaceBoundarySelect;
			if (b.mRelatedBuildingElement != null)
				RelatedBuildingElement = db.Factory.Duplicate(b.RelatedBuildingElement, options) as IfcElement;
			if (b.mConnectionGeometry > 0)
				ConnectionGeometry = db.Factory.Duplicate(b.ConnectionGeometry, options) as IfcConnectionGeometry;
			mPhysicalOrVirtualBoundary = b.mPhysicalOrVirtualBoundary;
			mInternalOrExternalBoundary = b.mInternalOrExternalBoundary;
		}
		public IfcRelSpaceBoundary(IfcSpaceBoundarySelect s, IfcElement e, IfcPhysicalOrVirtualEnum virt, IfcInternalOrExternalEnum intern) : base(s.Database)
		{
			mRelatingSpace = s.Index;
			s.AddBoundary(this);
			mRelatedBuildingElement = e;
			mPhysicalOrVirtualBoundary = virt;
			mInternalOrExternalBoundary = intern;
		}
	}
	[Serializable]
	public partial class IfcRelSpaceBoundary1stLevel : IfcRelSpaceBoundary
	{
		internal int mParentBoundary;// :	IfcRelSpaceBoundary1stLevel;
		//INVERSE	
		internal List<IfcRelSpaceBoundary1stLevel> mInnerBoundaries = new List<IfcRelSpaceBoundary1stLevel>();//	:	SET OF IfcRelSpaceBoundary1stLevel FOR ParentBoundary;

		public IfcRelSpaceBoundary1stLevel ParentBoundary { get { return mDatabase[mParentBoundary] as IfcRelSpaceBoundary1stLevel; } set { mParentBoundary = value.mIndex; value.mInnerBoundaries.Add(this); } }
		internal IfcRelSpaceBoundary1stLevel() : base() { }
		internal IfcRelSpaceBoundary1stLevel(DatabaseIfc db, IfcRelSpaceBoundary1stLevel r, DuplicateOptions options) : base(db, r, options) { ParentBoundary = db.Factory.Duplicate(r.ParentBoundary, options) as IfcRelSpaceBoundary1stLevel; }
		public IfcRelSpaceBoundary1stLevel(IfcSpaceBoundarySelect s, IfcElement e, IfcPhysicalOrVirtualEnum virt, IfcInternalOrExternalEnum intern, IfcRelSpaceBoundary1stLevel parent)
			: base(s, e, virt, intern) { mParentBoundary = parent.mIndex; }
	}
	[Serializable]
	public partial class IfcRelSpaceBoundary2ndLevel : IfcRelSpaceBoundary1stLevel
	{
		internal int mCorrespondingBoundary;// :	IfcRelSpaceBoundary2ndLevel;
											//INVERSE	
		internal List<IfcRelSpaceBoundary2ndLevel> mCorresponds = new List<IfcRelSpaceBoundary2ndLevel>();//	:	SET OF IfcRelSpaceBoundary1stLevel FOR ParentBoundary;

		public IfcRelSpaceBoundary2ndLevel CorrespondingBoundary { get { return mDatabase[mCorrespondingBoundary] as IfcRelSpaceBoundary2ndLevel; } set { mCorrespondingBoundary = value.mIndex; value.CorrespondingBoundary = this; } }

		internal IfcRelSpaceBoundary2ndLevel() : base() { }
		internal IfcRelSpaceBoundary2ndLevel(DatabaseIfc db, IfcRelSpaceBoundary2ndLevel r, DuplicateOptions options) : base(db, r, options) { CorrespondingBoundary = db.Factory.Duplicate(r.CorrespondingBoundary, options) as IfcRelSpaceBoundary2ndLevel; }
		public IfcRelSpaceBoundary2ndLevel(IfcSpaceBoundarySelect s, IfcElement e, IfcPhysicalOrVirtualEnum virt, IfcInternalOrExternalEnum intern, IfcRelSpaceBoundary1stLevel parent, IfcRelSpaceBoundary2ndLevel corresponding)
			: base(s, e, virt, intern, parent) { if (corresponding != null) mCorrespondingBoundary = corresponding.mIndex; }

	}
	[Serializable]
	public partial class IfcRelVoidsElement : IfcRelDecomposes // Ifc2x3 IfcRelConnects
	{
		private int mRelatingBuildingElement;// : IfcElement;
		private int mRelatedOpeningElement;// : IfcFeatureElementSubtraction; 

		public IfcElement RelatingBuildingElement { get { return mDatabase[mRelatingBuildingElement] as IfcElement; } set { mRelatingBuildingElement = value.mIndex; if(!value.mHasOpenings.Contains(this)) value.mHasOpenings.Add(this); } }
		public IfcFeatureElementSubtraction RelatedOpeningElement { get { return mDatabase[mRelatedOpeningElement] as IfcFeatureElementSubtraction; } set { mRelatedOpeningElement = value.mIndex; value.mVoidsElement = this; } }

		internal IfcRelVoidsElement() : base() { }
		internal IfcRelVoidsElement(DatabaseIfc db, IfcRelVoidsElement v, DuplicateOptions options) : base(db, v, options)
		{
			RelatedOpeningElement = db.Factory.Duplicate(v.RelatedOpeningElement, options) as IfcFeatureElementSubtraction;
		}
		public IfcRelVoidsElement(IfcElement host, IfcFeatureElementSubtraction fes)
			: base(host.mDatabase) { mRelatingBuildingElement = host.mIndex; host.mHasOpenings.Add(this); mRelatedOpeningElement = fes.mIndex; fes.mVoidsElement = this; }
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
				if (shapeModel != null && mContextOfItems != null)
					mContextOfItems.mRepresentationsInContext.Remove(StepId);
				mContextOfItems = value;
				if (shapeModel != null && value != null)
					value.mRepresentationsInContext[StepId]= shapeModel;
			}
		}
		public string RepresentationIdentifier { get { return mRepresentationIdentifier; } set { mRepresentationIdentifier = value; } }
		public string RepresentationType { get { return mRepresentationType; } set { mRepresentationType = value; } }
		public SET<RepresentationItem> Items { get { return mItems; } set { mItems.Clear(); if (value != null) { mItems.CollectionChanged -= mItems_CollectionChanged; mItems = value; mItems.CollectionChanged += mItems_CollectionChanged; } } }

		public IfcPresentationLayerAssignment LayerAssignment { get { return mLayerAssignment; } set { mLayerAssignment = value; } }
		public SET<IfcProductDefinitionShape> OfProductRepresentation { get { return mOfProductRepresentation; } }

		protected IfcRepresentation() : base() { }
		protected IfcRepresentation(DatabaseIfc db, IfcRepresentation<RepresentationItem> r, DuplicateOptions options) : base(db)
		{
			ContextOfItems = db.Factory.Duplicate(r.ContextOfItems) as IfcRepresentationContext;

			mRepresentationIdentifier = r.mRepresentationIdentifier;
			mRepresentationType = r.mRepresentationType;
			Items.AddRange(r.Items.Select(x => x.Duplicate(db, options) as RepresentationItem));

			if(r.mLayerAssignment != null)
			{
				IfcPresentationLayerAssignment la = db.Factory.Duplicate(r.mLayerAssignment, new DuplicateOptions(options) { DuplicateDownstream = false }) as IfcPresentationLayerAssignment;
				la.AssignedItems.Add(this);
			}
		}
		protected IfcRepresentation(IfcRepresentationContext context) : base(context.mDatabase) { ContextOfItems = context; RepresentationIdentifier = context.ContextIdentifier; }
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
			{
				if (e.NewItems != null)
				{
					foreach (IfcRepresentationItem r in e.NewItems)
						r.mRepresents.Add(shapeModel);
				}
				if (e.OldItems != null)
				{
					foreach (IfcRepresentationItem r in e.OldItems)
						r.mRepresents.Remove(shapeModel);
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
		internal string mContextIdentifier = "$";// : OPTIONAL IfcLabel;
		internal string mContextType = "$";// : OPTIONAL IfcLabel; 
		//INVERSE
		[NonSerialized] internal Dictionary<int, IfcShapeModel> mRepresentationsInContext = new Dictionary<int, IfcShapeModel>();// :	SET OF IfcRepresentation FOR ContextOfItems;

		public string ContextIdentifier { get { return (mContextIdentifier == "$" ? "" : ParserIfc.Decode(mContextIdentifier)); } set { mContextIdentifier = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public string ContextType { get { return (mContextType == "$" ? "" : ParserIfc.Decode(mContextType)); } set { mContextType = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public IEnumerable<IfcShapeModel> RepresentationsInContext { get { return mRepresentationsInContext.Values; } }

		protected IfcRepresentationContext() : base() { }
		protected IfcRepresentationContext(DatabaseIfc db) : base(db) { }
		protected IfcRepresentationContext(DatabaseIfc db, IfcRepresentationContext c) : base(db, c) { mContextIdentifier = c.mContextIdentifier; mContextType = c.mContextType; }
	}
	[Serializable]
	public abstract partial class IfcRepresentationItem : BaseClassIfc, IfcLayeredItem /*(IfcGeometricRepresentationItem,IfcMappedItem,IfcStyledItem,IfcTopologicalRepresentationItem));*/
	{ //INVERSE
		internal IfcPresentationLayerAssignment mLayerAssignment = null;// : SET [0:?] OF IfcPresentationLayerAssignment FOR AssignedItems;
		internal IfcStyledItem mStyledByItem = null;// : SET [0:1] OF IfcStyledItem FOR Item; 

		internal List<IfcShapeModel> mRepresents = new List<IfcShapeModel>();

		public IfcPresentationLayerAssignment LayerAssignment { get { return mLayerAssignment; } set { mLayerAssignment = value; } }
		public IfcStyledItem StyledByItem { get { return mStyledByItem; } set { if (value != null) value.Item = this; else mStyledByItem = null; } }

		protected IfcRepresentationItem() : base() { }
		protected IfcRepresentationItem(DatabaseIfc db, IfcRepresentationItem i, DuplicateOptions options) : base(db, i)
		{
			if(i.mLayerAssignment != null)
			{
				IfcPresentationLayerAssignment la = db.Factory.Duplicate(i.mLayerAssignment, new DuplicateOptions(db.Tolerance) { DuplicateDownstream = false }) as IfcPresentationLayerAssignment;
				la.AssignedItems.Add(this);
			}
			if (i.mStyledByItem != null)
			{
				IfcStyledItem si = db.Factory.Duplicate(i.mStyledByItem) as IfcStyledItem;
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
			return db.Factory.Duplicate(this, options) as IfcRepresentationItem;
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
			set { mMappedRepresentation = value; value.mRepresentationMap = this; }
		}  
		public SET<IfcShapeAspect> HasShapeAspects { get { return mHasShapeAspects; } }
		public SET<IfcMappedItem> MapUsage { get { return mMapUsage; } }

		internal SET<IfcTypeProduct> mRepresents = new SET<IfcTypeProduct>();// GG
		public SET<IfcTypeProduct> Represents { get { return mRepresents; } }

		internal IfcRepresentationMap() : base() { }
		internal IfcRepresentationMap(DatabaseIfc db, IfcRepresentationMap m, DuplicateOptions options) : base(db, m)
		{
			DuplicateOptions duplicateDownstream = new DuplicateOptions(options) { DuplicateDownstream = true };
			MappingOrigin = db.Factory.Duplicate((BaseClassIfc) m.mMappingOrigin, duplicateDownstream) as IfcAxis2Placement;
			MappedRepresentation = db.Factory.Duplicate(m.MappedRepresentation, duplicateDownstream) as IfcShapeModel;
			if (db.Release > ReleaseVersion.IFC2x3)
			{
				foreach (IfcShapeAspect shapeAspect in m.HasShapeAspects)
					(db.Factory.Duplicate(shapeAspect, duplicateDownstream) as IfcShapeAspect).PartOfProductDefinitionShape = this;
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

		public SET<IfcResourceObjectSelect> RelatedResourceObjects { get { return mRelatedResourceObjects; } set { mRelatedResourceObjects = value; } }
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
		internal int mRelatingConstraint;// :	IfcConstraint;
		internal List<int> mRelatedResourceObjects = new List<int>();// :	SET [1:?] OF IfcResourceObjectSelect;

		public IfcConstraint RelatingConstraint { get { return mDatabase[mRelatingConstraint] as IfcConstraint; } set { mRelatingConstraint = value.mIndex; if (!value.mPropertiesForConstraint.Contains(this)) value.mPropertiesForConstraint.Add(this); } }
		public ReadOnlyCollection<IfcResourceObjectSelect> RelatedResourceObjects { get { return new ReadOnlyCollection<IfcResourceObjectSelect>(mRelatedResourceObjects.ConvertAll(x => mDatabase[x] as IfcResourceObjectSelect)); } }

		internal IfcResourceConstraintRelationship() : base() { }
		internal IfcResourceConstraintRelationship(DatabaseIfc db, IfcResourceConstraintRelationship r) : base(db, r) { RelatingConstraint = db.Factory.Duplicate(r.RelatingConstraint) as IfcConstraint; r.mRelatedResourceObjects.ForEach(x => addRelated(db.Factory.Duplicate(r.mDatabase[x]) as IfcResourceObjectSelect)); }
		public IfcResourceConstraintRelationship(IfcConstraint constraint, IfcResourceObjectSelect related) : this(constraint, new List<IfcResourceObjectSelect>() { related }) { }
		public IfcResourceConstraintRelationship(IfcConstraint constraint, List<IfcResourceObjectSelect> related) : base(constraint.mDatabase)
		{
			RelatingConstraint = constraint;
			related.ForEach(x => addRelated(x));
		}

		internal void addRelated(IfcResourceObjectSelect r)
		{
			r.AddConstraintRelationShip(this);
			mRelatedResourceObjects.Add(r.Index);
		}

		protected override bool DisposeWorker(bool children)
		{
			for (int icounter = 0; icounter < mRelatedResourceObjects.Count; icounter++)
			{
				BaseClassIfc bc = mDatabase[mRelatedResourceObjects[icounter]];
				if (bc != null)
					bc.Dispose(children);
			}
			return base.DisposeWorker(children);
		}
	}
	[Serializable]
	public abstract partial class IfcResourceLevelRelationship : BaseClassIfc, NamedObjectIfc  //IFC4 ABSTRACT SUPERTYPE OF(ONEOF(IfcApprovalRelationship,
	{ // IfcCurrencyRelationship, IfcDocumentInformationRelationship, IfcExternalReferenceRelationship, IfcMaterialRelationship, IfcOrganizationRelationship, IfcPropertyDependencyRelationship, IfcResourceApprovalRelationship, IfcResourceConstraintRelationship));
		internal string mName = "$";// : OPTIONAL IfcLabel
		internal string mDescription = "$";// : OPTIONAL IfcText; 
		//INVERSE
		//mHasExternalReference

		public string Name { get { return (mName == "$" ? "" : ParserIfc.Decode(mName)); } set { mName = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public string Description { get { return (mDescription == "$" ? "" : ParserIfc.Decode(mDescription)); } set { mDescription = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }

		protected IfcResourceLevelRelationship() : base() { }
		protected IfcResourceLevelRelationship(DatabaseIfc db, IfcResourceLevelRelationship r) : base(db, r) { mDescription = r.mDescription; mName = r.mName; }
		protected IfcResourceLevelRelationship(DatabaseIfc db) : base(db) { }
	}
	public partial interface IfcResourceObjectSelect : IBaseClassIfc //IFC4 SELECT (	IfcPropertyAbstraction, IfcPhysicalQuantity, IfcAppliedValue, 
	{   //IfcContextDependentUnit, IfcConversionBasedUnit, IfcProfileDef, IfcActorRole, IfcApproval, IfcConstraint, IfcTimeSeries, IfcMaterialDefinition, IfcPerson, IfcPersonAndOrganization, IfcOrganization, IfcExternalReference, IfcExternalInformation););
		SET<IfcExternalReferenceRelationship> HasExternalReference { get; set; }
		void AddConstraintRelationShip(IfcResourceConstraintRelationship constraintRelationship);
	}
	public interface IfcResourceSelect : IBaseClassIfc // SELECT(IfcResource, IfcTypeResource)
	{
		SET<IfcRelAssignsToResource> ResourceOf { get; }
	}
	[Serializable]
	public partial class IfcResourceTime : IfcSchedulingTime //IFC4
	{
		internal string mScheduleWork = "$";//	 :	OPTIONAL IfcDuration;
		internal double mScheduleUsage = double.NaN; //:	OPTIONAL IfcPositiveRatioMeasure;
		internal DateTime mScheduleStart = DateTime.MinValue, mScheduleFinish = DateTime.MinValue;//:	OPTIONAL IfcDateTime;
		internal string mScheduleContour = "$";//:	OPTIONAL IfcLabel;
		internal string mLevelingDelay = "$";//	 :	OPTIONAL IfcDuration;
		internal bool mIsOverAllocated = false;//	 :	OPTIONAL BOOLEAN;
		internal DateTime mStatusTime = DateTime.MinValue;//:	OPTIONAL IfcDateTime;
		internal string mActualWork = "$";//	 :	OPTIONAL IfcDuration; 
		internal double mActualUsage = double.NaN; //:	OPTIONAL IfcPositiveRatioMeasure; 
		internal DateTime mActualStart = DateTime.MinValue, mActualFinish = DateTime.MinValue;//	 :	OPTIONAL IfcDateTime;
		internal string mRemainingWork = "$";//	 :	OPTIONAL IfcDuration;
		internal double mRemainingUsage = double.NaN, mCompletion = double.NaN;//	 :	OPTIONAL IfcPositiveRatioMeasure; 

		public string ScheduleWork { get { return (mScheduleWork == "$" ? "" : ParserIfc.Decode(mScheduleWork)); } set { mScheduleWork = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public double ScheduleUsage { get { return mScheduleUsage; } set { mScheduleUsage = value; } }
		public DateTime ScheduleStart { get { return mScheduleStart; } set { mScheduleStart = value; } }
		public DateTime ScheduleFinish { get { return mScheduleFinish; } set { mScheduleFinish = value; } }
		public string ScheduleContour { get { return (mScheduleContour == "$" ? "" : ParserIfc.Decode(mScheduleContour)); } set { mScheduleContour = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public IfcDuration LevelingDelay { get { return null; } set { mLevelingDelay = (value == null ? "$" : value.ToString()); } }
		public bool IsOverAllocated { get { return mIsOverAllocated; } set { mIsOverAllocated = value; } }
		public DateTime StatusTime { get { return mStatusTime; } set { mStatusTime = value; } }
		public IfcDuration ActualWork { get { return null; } set { mActualWork = (value == null ? "$" : value.ToString()); } }
		public double ActualUsage { get { return mActualUsage; } set { mActualUsage = value; } }
		public DateTime ActualStart { get { return mActualStart; } set { mActualStart = value; } }
		public DateTime ActualFinish { get { return mActualFinish; } set { mActualFinish = value; } }
		public IfcDuration RemainingWork { get { return null; } set { mRemainingWork = (value == null ? "$" : value.ToString()); } }
		public double RemainingUsage { get { return mRemainingUsage; } set { mRemainingUsage = value; } }
		public double Completion { get { return mCompletion; } set { mCompletion = value; } }

		internal IfcResourceTime() : base() { }
		internal IfcResourceTime(DatabaseIfc db, IfcResourceTime t) : base(db, t)
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
		internal int mAxis;//: IfcAxis1Placement
		internal double mAngle;// : IfcPlaneAngleMeasure;

		public IfcAxis1Placement Axis { get { return mDatabase[mAxis] as IfcAxis1Placement; } set { mAxis = value.mIndex; } }
		public double Angle { get { return mAngle; } set { mAngle = value; } }

		internal IfcRevolvedAreaSolid() : base() { }
		internal IfcRevolvedAreaSolid(DatabaseIfc db, IfcRevolvedAreaSolid s, DuplicateOptions options) : base(db, s, options) { Axis = db.Factory.Duplicate(s.Axis) as IfcAxis1Placement; mAngle = s.mAngle; }
		public IfcRevolvedAreaSolid(IfcProfileDef profile, IfcAxis2Placement3D position, IfcAxis1Placement axis, double angle) : base(profile, position) { Axis = axis; mAngle = angle; }
	}
	[Serializable]
	public partial class IfcRevolvedAreaSolidTapered : IfcRevolvedAreaSolid
	{
		private int mEndSweptArea;//: IfcProfileDef 
		public IfcProfileDef EndSweptArea { get { return mDatabase[mEndSweptArea] as IfcProfileDef; } set { mEndSweptArea = value.mIndex; } }

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
	[Serializable]
	public partial class IfcRoad : IfcFacility
	{
		internal IfcRoadTypeEnum mPredefinedType = IfcRoadTypeEnum.NOTDEFINED;// OPTIONAL : IfcRoadTypeEnum
		public IfcRoadTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		public IfcRoad() : base() { }
		public IfcRoad(DatabaseIfc db) : base(db) { }
		public IfcRoad(DatabaseIfc db, IfcRoad road, DuplicateOptions options) : base(db, road, options) { }
		public IfcRoad(IfcFacility host, string name, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { Name = name; }
		public IfcRoad(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	[Serializable]
	public partial class IfcRoof : IfcBuiltElement
	{
		internal IfcRoofTypeEnum mPredefinedType = IfcRoofTypeEnum.NOTDEFINED;// OPTIONAL : IfcRoofTypeEnum; 
		public IfcRoofTypeEnum PredefinedType { get { return mPredefinedType; } }

		internal IfcRoof() : base() { }
		internal IfcRoof(DatabaseIfc db, IfcRoof r, DuplicateOptions options) : base(db, r, options) { mPredefinedType = r.mPredefinedType; }
		public IfcRoof(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductDefinitionShape representation) : base(host, placement, representation) { }
	}
	[Serializable]
	public partial class IfcRoofType : IfcBuiltElementType //IFC4
	{
		internal IfcRoofTypeEnum mPredefinedType = IfcRoofTypeEnum.NOTDEFINED;
		public IfcRoofTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcRoofType() : base() { }
		internal IfcRoofType(DatabaseIfc db, IfcRoofType t, DuplicateOptions options) : base(db, t, options) { mPredefinedType = t.mPredefinedType; }
		public IfcRoofType(DatabaseIfc m, string name, IfcRoofTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
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
							BaseClassIfc obj = null;
							mDatabase.mDictionary.TryRemove(mGlobalId, out obj);
							if (!mDatabase.mDictionary.ContainsKey(value))
								mDatabase.mDictionary.TryAdd(value, this);
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
		protected IfcRoot(IfcRoot root, bool replace) : base(root, replace)
		{
			if (replace)
			{
				setGlobalId(root.mGlobalId);
				mOwnerHistory = root.mOwnerHistory;
			}
			else
				OwnerHistory = root.Database.Factory.OwnerHistoryAdded;
			mName = root.mName;
			mDescription = root.mDescription;
		}
		protected IfcRoot(DatabaseIfc db) : base(db)
		{
			setGlobalId(ParserIfc.EncodeGuid(Guid.NewGuid()));
			if (db.Release < ReleaseVersion.IFC4 || (db.mModelView != ModelView.Ifc4Reference && db.Factory.Options.GenerateOwnerHistory))
				OwnerHistory = db.Factory.OwnerHistoryAdded;
		}
		protected IfcRoot(DatabaseIfc db, IfcRoot r, DuplicateOptions options) : base(db, r)
		{
			if (db[r.GlobalId] == null)
				GlobalId = r.GlobalId;
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
