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
using System.Collections.Specialized;

namespace GeometryGym.Ifc
{
	[Obsolete("DEPRECEATED IFC4", false)]
	[Serializable]
	public partial class IfcRadiusDimension : IfcDimensionCurveDirectedCallout // DEPRECEATED IFC4
	{
		internal IfcRadiusDimension() : base() { }
	}
	[Serializable]
	public partial class IfcRailing : IfcBuildingElement
	{
		internal IfcRailingTypeEnum mPredefinedType = IfcRailingTypeEnum.NOTDEFINED;// : OPTIONAL IfcRailingTypeEnum
		public IfcRailingTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcRailing() : base() { }
		internal IfcRailing(DatabaseIfc db, IfcRailing r, IfcOwnerHistory ownerHistory, bool downStream) : base(db, r, ownerHistory, downStream) { mPredefinedType = r.mPredefinedType; }
		public IfcRailing(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation) : base(host, placement, representation) { }
	}
	[Serializable]
	public partial class IfcRailingType : IfcBuildingElementType
	{
		internal IfcRailingTypeEnum mPredefinedType = IfcRailingTypeEnum.NOTDEFINED;
		public IfcRailingTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcRailingType() : base() { }
		internal IfcRailingType(DatabaseIfc db, IfcRailingType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mPredefinedType = t.mPredefinedType; }
		public IfcRailingType(DatabaseIfc m, string name, IfcRailingTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
	}
	[Serializable]
	public partial class IfcRamp : IfcBuildingElement
	{
		internal IfcRampTypeEnum mPredefinedType = IfcRampTypeEnum.NOTDEFINED;// OPTIONAL : IfcRampTypeEnum
		public IfcRampTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcRamp() : base() { }
		internal IfcRamp(DatabaseIfc db, IfcRamp r, IfcOwnerHistory ownerHistory, bool downStream) : base(db, r, ownerHistory, downStream) { mPredefinedType = r.mPredefinedType; }
		public IfcRamp(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation) : base(host, placement, representation) { }
	}
	[Serializable]
	public partial class IfcRampFlight : IfcBuildingElement
	{
		internal IfcRampFlightTypeEnum mPredefinedType = IfcRampFlightTypeEnum.NOTDEFINED;// OPTIONAL : IfcRampTypeEnum
		public IfcRampFlightTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcRampFlight() : base() { }
		internal IfcRampFlight(DatabaseIfc db, IfcRampFlight f, IfcOwnerHistory ownerHistory, bool downStream) : base(db, f, ownerHistory, downStream) { mPredefinedType = f.mPredefinedType; }
		public IfcRampFlight(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation) : base(host, placement, representation) { }
	}
	[Serializable]
	public partial class IfcRampFlightType : IfcBuildingElementType
	{
		internal IfcRampFlightTypeEnum mPredefinedType = IfcRampFlightTypeEnum.NOTDEFINED;
		public IfcRampFlightTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcRampFlightType() : base() { }
		internal IfcRampFlightType(DatabaseIfc db, IfcRampFlightType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mPredefinedType = t.mPredefinedType; }
		public IfcRampFlightType(DatabaseIfc m, string name, IfcRampFlightTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
	}
	[Serializable]
	public partial class IfcRampType : IfcBuildingElementType //IFC4
	{
		internal IfcRampTypeEnum mPredefinedType = IfcRampTypeEnum.NOTDEFINED;
		public IfcRampTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcRampType() : base() { }
		internal IfcRampType(DatabaseIfc db, IfcRampType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mPredefinedType = t.mPredefinedType; }
		public IfcRampType(DatabaseIfc m, string name, IfcRampTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
	}
	[Obsolete("DEPRECEATED IFC4", false)]
	[Serializable]
	public partial class IfcRationalBezierCurve : IfcBezierCurve // DEPRECEATED IFC4
	{
		internal List<double> mWeightsData = new List<double>();// : LIST [2:?] OF REAL;	
		internal IfcRationalBezierCurve() : base() { }
		internal IfcRationalBezierCurve(DatabaseIfc db, IfcRationalBezierCurve c) : base(db, c) { mWeightsData.AddRange(c.mWeightsData); }
	}
	[Serializable]
	public partial class IfcRationalBSplineCurveWithKnots : IfcBSplineCurveWithKnots
	{
		internal List<double> mWeightsData = new List<double>();// : LIST [2:?] OF REAL;	
		internal IfcRationalBSplineCurveWithKnots() : base() { }
		internal IfcRationalBSplineCurveWithKnots(DatabaseIfc db, IfcRationalBSplineCurveWithKnots c) : base(db, c) { mWeightsData.AddRange(c.mWeightsData); }
	}
	[Serializable]
	public partial class IfcRationalBSplineSurfaceWithKnots : IfcBSplineSurfaceWithKnots
	{
		internal List<List<double>> mWeightsData = new List<List<double>>();// : LIST [2:?] OF REAL;	
		internal IfcRationalBSplineSurfaceWithKnots() : base() { }
		internal IfcRationalBSplineSurfaceWithKnots(DatabaseIfc db, IfcRationalBSplineSurfaceWithKnots s) : base(db, s)
		{
			for (int icounter = 0; icounter < s.mWeightsData.Count; icounter++)
				mWeightsData.Add(new List<double>(s.mWeightsData[icounter].ToArray()));
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
		internal IfcRectangleHollowProfileDef(DatabaseIfc db, IfcRectangleHollowProfileDef p) : base(db, p) { mWallThickness = p.mWallThickness; mInnerFilletRadius = p.mInnerFilletRadius; mOuterFilletRadius = p.mOuterFilletRadius; }
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
		internal IfcRectangleProfileDef(DatabaseIfc db, IfcRectangleProfileDef p) : base(db, p) { mXDim = p.mXDim; mYDim = p.mYDim; }
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
		internal IfcRectangularPyramid(DatabaseIfc db, IfcRectangularPyramid p) : base(db, p) { mXLength = p.mXLength; mYLength = p.mYLength; mHeight = p.mHeight; }
	}
	[Serializable]
	public partial class IfcRectangularTrimmedSurface : IfcBoundedSurface
	{
		internal int mBasisSurface;// : IfcPlane;
		internal double mU1, mV1, mU2, mV2;// : IfcParameterValue;
		internal bool mUsense, mVsense;// : BOOLEAN; 

		public IfcPlane BasisSurface { get { return mDatabase[mBasisSurface] as IfcPlane; } set { mBasisSurface = value.mIndex; } }

		internal IfcRectangularTrimmedSurface() : base() { }
		internal IfcRectangularTrimmedSurface(DatabaseIfc db, IfcRectangularTrimmedSurface s) : base(db, s)
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
	//[Obsolete("DEPRECEATED IFC4", false)]
	//ENTITY IfcReferencesValueDocument; // DEPRECEATED IFC4
	//ENTITY IfcRegularTimeSeries
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
		internal IfcReinforcementDefinitionProperties(DatabaseIfc db, IfcReinforcementDefinitionProperties p, IfcOwnerHistory ownerHistory, bool downStream) : base(db, p, ownerHistory, downStream)
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
		internal IfcReinforcingBar(DatabaseIfc db, IfcReinforcingBar b, IfcOwnerHistory ownerHistory, bool downStream) : base(db, b, ownerHistory, downStream)
		{
			mNominalDiameter = b.mNominalDiameter;
			mCrossSectionArea = b.mCrossSectionArea;
			mBarLength = b.mBarLength;
			mPredefinedType = b.mPredefinedType;
			mBarSurface = b.mBarSurface;
		}
		public IfcReinforcingBar(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation) : base(host, placement, representation) { }
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
		internal IfcReinforcingBarType(DatabaseIfc db, IfcReinforcingBarType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream)
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
		private string mSteelGrade = "$";// : OPTIONAL IfcLabel; //IFC4 Depreceated 
		[Obsolete("DEPRECEATED IFC4", false)]
		public string SteelGrade { get { return (mSteelGrade == "$" ? "" : ParserIfc.Decode(mSteelGrade)); } set { mSteelGrade = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }

		protected IfcReinforcingElement() : base() { }
		protected IfcReinforcingElement(DatabaseIfc db, IfcReinforcingElement e, IfcOwnerHistory ownerHistory, bool downStream) : base(db, e, ownerHistory, downStream) { mSteelGrade = e.mSteelGrade; }
		public IfcReinforcingElement(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation) : base(host, placement, representation) { }
	}
	[Serializable]
	public abstract partial class IfcReinforcingElementType : IfcElementComponentType //IFC4 ABSTRACT SUPERTYPE OF(ONEOF(IfcReinforcingBarType, IfcReinforcingMeshType, IfcTendonAnchorType, IfcTendonType))
	{
		protected IfcReinforcingElementType() : base() { }
		protected IfcReinforcingElementType(DatabaseIfc db) : base(db) { }
		protected IfcReinforcingElementType(DatabaseIfc db, IfcReinforcingElementType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { }
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
		internal IfcReinforcingMesh(DatabaseIfc db, IfcReinforcingMesh m, IfcOwnerHistory ownerHistory, bool downStream) : base(db, m, ownerHistory, downStream)
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
		public IfcReinforcingMesh(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation) : base(host, placement, representation) { }
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
		internal IfcReinforcingMeshType(DatabaseIfc db, IfcReinforcingMeshType m, IfcOwnerHistory ownerHistory, bool downStream) : base(db, m, ownerHistory, downStream)
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
		internal int mRelatingObject;// : IfcObjectDefinition IFC4 IfcObject
		internal List<int> mRelatedObjects = new List<int>();// : SET [1:?] OF IfcObjectDefinition; 

		public IfcObjectDefinition RelatingObject { get { return mDatabase[mRelatingObject] as IfcObjectDefinition; } set { mRelatingObject = value.mIndex; value.mIsDecomposedBy.Add(this); } }
		public ReadOnlyCollection<IfcObjectDefinition> RelatedObjects { get { return new ReadOnlyCollection<IfcObjectDefinition>(mRelatedObjects.ConvertAll(x => mDatabase[x] as IfcObjectDefinition)); } }

		internal IfcRelAggregates() : base() { }
		internal IfcRelAggregates(DatabaseIfc db, IfcRelAggregates a, IfcOwnerHistory ownerHistory, bool downStream) : base(db, a, ownerHistory)
		{
			RelatingObject = db.Factory.Duplicate(a.RelatingObject, ownerHistory, downStream) as IfcObjectDefinition;
			if (downStream)
				a.RelatedObjects.ToList().ConvertAll(x => db.Factory.Duplicate(x, ownerHistory, downStream) as IfcObjectDefinition).ForEach(x => addObject(x));
		}
		internal IfcRelAggregates(IfcObjectDefinition relObject) : base(relObject.mDatabase)
		{
			mRelatingObject = relObject.mIndex;
			relObject.mIsDecomposedBy.Add(this);
		}
		internal IfcRelAggregates(IfcObjectDefinition relObject, IfcObjectDefinition relatedObject) : this(relObject, new List<IfcObjectDefinition>() { relatedObject }) { }
		internal IfcRelAggregates(IfcObjectDefinition relObject, IEnumerable<IfcObjectDefinition> relatedObjects) : this(relObject)
		{
			foreach (IfcObjectDefinition od in relatedObjects)
				od.Decomposes = this;
		}

		internal bool addObject(IfcObjectDefinition o)
		{
			if (o == null || mRelatedObjects.Contains(o.mIndex))
				return false;
			mRelatedObjects.Add(o.mIndex);
			o.Decomposes = this;
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
	[Serializable]
	public abstract partial class IfcRelAssigns : IfcRelationship //	ABSTRACT SUPERTYPE OF(ONEOF(IfcRelAssignsToActor, IfcRelAssignsToControl, IfcRelAssignsToGroup, IfcRelAssignsToProcess, IfcRelAssignsToProduct, IfcRelAssignsToResource))
	{
		internal SET<IfcObjectDefinition> mRelatedObjects = new SET<IfcObjectDefinition>();// : SET [1:?] OF IfcObjectDefinition;
		internal IfcObjectTypeEnum mRelatedObjectsType = IfcObjectTypeEnum.NOTDEFINED;// : OPTIONAL IfcObjectTypeEnum; IFC4 CHANGE  The attribute is deprecated and shall no longer be used. A NIL value should always be assigned.
		public SET<IfcObjectDefinition> RelatedObjects { get { return mRelatedObjects; } }

		public abstract NamedObjectIfc Relating();

		protected IfcRelAssigns() : base() { }
		protected IfcRelAssigns(DatabaseIfc db) : base(db) { }
		protected IfcRelAssigns(DatabaseIfc db, IfcRelAssigns a, IfcOwnerHistory ownerHistory, bool downStream) : base(db, a, ownerHistory)
		{
			if (downStream)
				RelatedObjects.AddRange(a.RelatedObjects.ConvertAll(x => db.Factory.Duplicate(x, ownerHistory, downStream) as IfcObjectDefinition));
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
	[Obsolete("DEPRECEATED IFC4", false)]
	public partial class IfcRelAssignsTasks : IfcRelAssignsToControl // IFC4 depreceated
	{
		private int mTimeForTask;// :  	OPTIONAL IfcScheduleTimeControl; 

		public IfcScheduleTimeControl TimeForTask
		{
			get { return mDatabase[mTimeForTask] as IfcScheduleTimeControl; }
			set { mTimeForTask = value == null ? 0 : value.mIndex; if (value != null) value.mScheduleTimeControlAssigned = this; }
		}
		internal IfcWorkControl WorkControl { get { return mDatabase[mRelatingControl] as IfcWorkControl; } }

		internal new ReadOnlyCollection<IfcTask> RelatedObjects { get { return new ReadOnlyCollection<IfcTask>(base.RelatedObjects.Cast<IfcTask>().ToList()); } }

		internal IfcRelAssignsTasks() : base() { }
		internal IfcRelAssignsTasks(DatabaseIfc db, IfcRelAssignsTasks r, IfcOwnerHistory ownerHistory, bool downStream) : base(db, r, ownerHistory, downStream) { if (r.mTimeForTask > 0) TimeForTask = db.Factory.Duplicate(r.TimeForTask, ownerHistory, downStream) as IfcScheduleTimeControl; }
		public IfcRelAssignsTasks(IfcWorkControl relating, IfcTask related ) : base(relating, related) { }
		public IfcRelAssignsTasks(IfcWorkControl relControl, IEnumerable<IfcTask> relObjects) : base(relControl, relObjects) { }
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
		internal IfcRelAssignsToActor(DatabaseIfc db, IfcRelAssignsToActor r, IfcOwnerHistory ownerHistory, bool downStream) : base(db, r, ownerHistory, downStream) { RelatingActor = db.Factory.Duplicate(r.RelatingActor, ownerHistory, false) as IfcActor; if (r.mActingRole != null) ActingRole = db.Factory.Duplicate(r.ActingRole, ownerHistory, downStream) as IfcActorRole; }
		public IfcRelAssignsToActor(IfcActor relActor) : base(relActor.mDatabase) { RelatingActor = relActor; }
		public IfcRelAssignsToActor(IfcActor relActor, IfcObjectDefinition relObject) : base(relObject) { RelatingActor = relActor; }
		public IfcRelAssignsToActor(IfcActor relActor, List<IfcObjectDefinition> relObjects) : base(relObjects) { RelatingActor = relActor; }
	}
	[Serializable]
	public partial class IfcRelAssignsToControl : IfcRelAssigns
	{
		internal int mRelatingControl;// : IfcControl; 
		public IfcControl RelatingControl { get { return mDatabase[mRelatingControl] as IfcControl; } set { mRelatingControl = value.mIndex; value.mControls.Add(this); } }

		public override NamedObjectIfc Relating() { return RelatingControl; } 

		internal IfcRelAssignsToControl() : base() { }
		internal IfcRelAssignsToControl(DatabaseIfc db, IfcRelAssignsToControl r, IfcOwnerHistory ownerHistory, bool downStream) : base(db, r, ownerHistory, downStream) { RelatingControl = db.Factory.Duplicate(r.RelatingControl, ownerHistory, false) as IfcControl; }
		public IfcRelAssignsToControl(IfcControl relControl) : base(relControl.mDatabase) { RelatingControl = relControl;  }
		public IfcRelAssignsToControl(IfcControl relControl, IfcObjectDefinition relObject) : base(relObject) { RelatingControl = relControl; }
		public IfcRelAssignsToControl(IfcControl relControl, IEnumerable<IfcObjectDefinition> relObjects) : base(relObjects) { RelatingControl = relControl; }
	}
	[Serializable]
	public partial class IfcRelAssignsToGroup : IfcRelAssigns   //SUPERTYPE OF(IfcRelAssignsToGroupByFactor)
	{
		private IfcGroup mRelatingGroup;// : IfcGroup; 
		public IfcGroup RelatingGroup { get { return mRelatingGroup; } set { mRelatingGroup = value; value.mIsGroupedBy.Add(this); } }

		public override NamedObjectIfc Relating() { return RelatingGroup; } 

		internal IfcRelAssignsToGroup() : base() { }
		internal IfcRelAssignsToGroup(DatabaseIfc db, IfcRelAssignsToGroup a, IfcOwnerHistory ownerHistory, bool downStream) : base(db, a, ownerHistory, downStream) { RelatingGroup = db.Factory.Duplicate(a.RelatingGroup, ownerHistory, false) as IfcGroup; }
		public IfcRelAssignsToGroup(IfcGroup relgroup) : base(relgroup.mDatabase) { RelatingGroup = relgroup; }
		public IfcRelAssignsToGroup(IfcObjectDefinition related, IfcGroup relating) : base(related) { RelatingGroup = relating; }
		public IfcRelAssignsToGroup(IEnumerable<IfcObjectDefinition> related, IfcGroup relating) : base(related) { RelatingGroup = relating; }
	}
	[Serializable]
	public partial class IfcRelAssignsToGroupByFactor : IfcRelAssignsToGroup //IFC4
	{
		internal override string KeyWord { get { return (mDatabase.mRelease < ReleaseVersion.IFC4 ? "IfcRelAssignsToGroup" : base.KeyWord); } }
		internal double mFactor = 1;//	 :	IfcRatioMeasure;
		public double Factor { get { return mFactor; } set { mFactor = value; } }

		internal IfcRelAssignsToGroupByFactor() : base() { }
		internal IfcRelAssignsToGroupByFactor(DatabaseIfc db, IfcRelAssignsToGroupByFactor a, IfcOwnerHistory ownerHistory, bool downStream) : base(db, a, ownerHistory, downStream) { mFactor = a.mFactor; }
		public IfcRelAssignsToGroupByFactor(IfcGroup relgroup, double factor) : base(relgroup) { mFactor = factor; }
		public IfcRelAssignsToGroupByFactor(IEnumerable<IfcObjectDefinition> relObjects, IfcGroup relgroup, double factor) : base(relObjects, relgroup) { mFactor = factor; }
	}
	[Serializable]
	public partial class IfcRelAssignsToProcess : IfcRelAssigns
	{
		internal int mRelatingProcess;// : IfcProcess; 
		internal IfcMeasureWithUnit mQuantityInProcess = null;//	 : 	OPTIONAL IfcMeasureWithUnit;
		public IfcProcess RelatingProcess { get { return mDatabase[mRelatingProcess] as IfcProcess; } set { mRelatingProcess = value.mIndex; } }
		public IfcMeasureWithUnit QuantityInProcess { get { return mQuantityInProcess; } set { mQuantityInProcess = value; } }

		public override NamedObjectIfc Relating() { return RelatingProcess; } 

		internal IfcRelAssignsToProcess() : base() { }
		internal IfcRelAssignsToProcess(DatabaseIfc db, IfcRelAssignsToProcess r, IfcOwnerHistory ownerHistory, bool downStream) : base(db, r, ownerHistory, downStream) { RelatingProcess = db.Factory.Duplicate(r.RelatingProcess, ownerHistory, false) as IfcProcess; }
		public IfcRelAssignsToProcess(IfcProcess relProcess) : base(relProcess.mDatabase) { RelatingProcess = relProcess; }
		public IfcRelAssignsToProcess(IfcProcess relProcess, IfcObjectDefinition related) : base(related) { RelatingProcess = relProcess; }
		public IfcRelAssignsToProcess(IfcProcess relProcess, IEnumerable<IfcObjectDefinition> related) : base(related) { RelatingProcess = relProcess; }
	}
	[Serializable]
	public partial class IfcRelAssignsToProduct : IfcRelAssigns
	{
		private IfcProductSelect mRelatingProduct = null;// : IFC4	IfcProductSelect; : IfcProduct; 
		public IfcProductSelect RelatingProduct { get { return mRelatingProduct; } set { mRelatingProduct = value; if(value != null && !value.ReferencedBy.Contains(this)) value.ReferencedBy.Add(this); } }

		public override NamedObjectIfc Relating() { return RelatingProduct as NamedObjectIfc; }

		internal IfcRelAssignsToProduct() : base() { }
		internal IfcRelAssignsToProduct(DatabaseIfc db, IfcRelAssignsToProduct r, IfcOwnerHistory ownerHistory, bool downStream) : base(db, r, ownerHistory, downStream) { }
		public IfcRelAssignsToProduct(IfcProductSelect relProduct) : base(relProduct.Database) { RelatingProduct = relProduct; }
		public IfcRelAssignsToProduct(IfcObjectDefinition relObject, IfcProductSelect relProduct) : base(relObject) { RelatingProduct = relProduct; }
		public IfcRelAssignsToProduct(IEnumerable<IfcObjectDefinition> relObjects, IfcProductSelect relProduct) : base(relObjects) { RelatingProduct = relProduct; }

		internal override void changeSchema(ReleaseVersion schema)
		{
			base.changeSchema(schema);
			mDatabase[mRelatingProduct.Index].changeSchema(schema);
			if (schema < ReleaseVersion.IFC4)
			{
				IfcProduct product = RelatingProduct as IfcProduct;
				if (product == null)
					mDatabase[mIndex] = null;
			}
		}
	}
	//[Obsolete("DEPRECEATED IFC4", false)]
	//ENTITY IfcRelAssignsToProjectOrder // DEPRECEATED IFC4 
	[Serializable]
	public partial class IfcRelAssignsToResource : IfcRelAssigns
	{
		internal int mRelatingResource;// : IfcResource; 
		public IfcResource RelatingResource { get { return mDatabase[mRelatingResource] as IfcResource; } set { mRelatingResource = value.mIndex; } }

		public override NamedObjectIfc Relating() { return RelatingResource; }

		internal IfcRelAssignsToResource() : base() { }
		internal IfcRelAssignsToResource(DatabaseIfc db, IfcRelAssignsToResource r, IfcOwnerHistory ownerHistory, bool downStream) : base(db, r, ownerHistory, downStream) { RelatingResource = db.Factory.Duplicate(r.RelatingResource, ownerHistory, false) as IfcResource; }
		public IfcRelAssignsToResource(IfcResource relResource) : base(relResource.mDatabase) { RelatingResource = relResource; }
		public IfcRelAssignsToResource(IfcResource relResource, IfcObjectDefinition relObject) : base(relObject) { RelatingResource = relResource; }
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
		protected IfcRelAssociates(DatabaseIfc db, IfcRelAssociates r, IfcOwnerHistory ownerHistory, bool downStream) : base(db, r, ownerHistory)
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
	//[Obsolete("DEPRECEATED IFC4", false)]
	//ENTITY IfcRelAssociatesAppliedValue // DEPRECEATED IFC4
	//ENTITY IfcRelAssociatesApproval
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
		internal IfcRelAssociatesClassification(DatabaseIfc db, IfcRelAssociatesClassification r, IfcOwnerHistory ownerHistory, bool downStream) : base(db, r, ownerHistory, downStream)
		{
			RelatingClassification = db.Factory.Duplicate(r.RelatingClassification) as IfcClassificationSelect;
		}
		public IfcRelAssociatesClassification(IfcClassificationSelect classification) : base(classification.Database) { RelatingClassification = classification; }
		public IfcRelAssociatesClassification(IfcClassificationSelect classification, IfcDefinitionSelect related) : base(related) { RelatingClassification = classification; }
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
		internal IfcRelAssociatesConstraint(DatabaseIfc db, IfcRelAssociatesConstraint c, IfcOwnerHistory ownerHistory, bool downStream) : base(db, c, ownerHistory, downStream) { RelatingConstraint = db.Factory.Duplicate(c.RelatingConstraint) as IfcConstraint; }
		public IfcRelAssociatesConstraint(IfcConstraint c) : base(c.mDatabase) { RelatingConstraint = c; }
		public IfcRelAssociatesConstraint(IfcDefinitionSelect related, IfcConstraint constraint) : base(related) { RelatingConstraint = constraint; }

		internal override void changeSchema(ReleaseVersion schema)
		{
			base.changeSchema(schema);
			if (schema < ReleaseVersion.IFC4)
			{
				IfcConstraint constraint = RelatingConstraint;
				if (constraint != null)
					constraint.Dispose(true);
				return;
#warning implement
				IfcMetric metric = RelatingConstraint as IfcMetric;
				// 	TYPE IfcMetricValueSelect = SELECT (IfcDateTimeSelect, IfcMeasureWithUnit, IfcTable, IfcText, IfcTimeSeries, IfcCostValue);
				if (metric != null)
				{
					IfcMetricValueSelect mv = metric.DataValue;
					IfcValue value = mv as IfcValue;
					if (value == null)
					{
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
	[Serializable]
	public partial class IfcRelAssociatesDocument : IfcRelAssociates
	{
		internal int mRelatingDocument;// : IfcDocumentSelect; 
		public IfcDocumentSelect RelatingDocument { get { return mDatabase[mRelatingDocument] as IfcDocumentSelect; } set { mRelatingDocument = value.Index; value.Associate(this); } }

		public override NamedObjectIfc Relating() { return RelatingDocument; } 

		internal IfcRelAssociatesDocument() : base() { }
		internal IfcRelAssociatesDocument(DatabaseIfc db, IfcRelAssociatesDocument r, IfcOwnerHistory ownerHistory, bool downStream) : base(db, r, ownerHistory, downStream) { RelatingDocument = db.Factory.Duplicate(r.mDatabase[r.mRelatingDocument], ownerHistory, downStream) as IfcDocumentSelect; }
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
		internal IfcRelAssociatesLibrary(DatabaseIfc db, IfcRelAssociatesLibrary r, IfcOwnerHistory ownerHistory, bool downStream) : base(db, r, ownerHistory, downStream) { RelatingLibrary = db.Factory.Duplicate(r.mDatabase[r.mRelatingLibrary], ownerHistory, downStream) as IfcLibrarySelect; }
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
		internal IfcRelAssociatesMaterial(DatabaseIfc db, IfcRelAssociatesMaterial r, IfcOwnerHistory ownerHistory, bool downStream) : base(db, r, ownerHistory, downStream)
		{
			RelatingMaterial = db.Factory.Duplicate(r.mDatabase[r.mRelatingMaterial], ownerHistory, downStream) as IfcMaterialSelect;
		}
		public IfcRelAssociatesMaterial(IfcMaterialSelect material) : base(material.Database) { RelatingMaterial = material; }
	}
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
		internal IfcRelAssociatesProfileProperties(DatabaseIfc db, IfcRelAssociatesProfileProperties r, IfcOwnerHistory ownerHistory, bool downStream) : base(db, r, ownerHistory, downStream)
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
		public IfcRelAssociatesProfileProperties(IfcProfileProperties pp) : base(pp.mDatabase) { if (pp.mDatabase.mRelease != ReleaseVersion.IFC2x3) throw new Exception(KeyWord + " Deleted in IFC4"); mRelatingProfileProperties = pp.mIndex; }
	}
	[Serializable]
	public abstract partial class IfcRelationship : IfcRoot  //ABSTRACT SUPERTYPE OF (ONEOF (IfcRelAssigns ,IfcRelAssociates ,IfcRelConnects ,IfcRelDecomposes ,IfcRelDefines))
	{
		protected IfcRelationship() : base() { }
		internal IfcRelationship(DatabaseIfc db) : base(db) { }
		protected IfcRelationship(DatabaseIfc db, IfcRelationship r, IfcOwnerHistory ownerHistory) : base(db, r, ownerHistory) { }
	}
	[Obsolete("DEPRECEATED IFC4", false)]
	[Serializable]
	public partial class IfcRelaxation : BaseClassIfc// DEPRECEATED IFC4
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
		protected IfcRelConnects(DatabaseIfc db, IfcRelConnects r, IfcOwnerHistory ownerHistory) : base(db, r, ownerHistory) { }
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
		internal IfcRelConnectsElements(DatabaseIfc db, IfcRelConnectsElements r, IfcOwnerHistory ownerHistory, bool downStream) : base(db, r, ownerHistory)
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
		internal IfcRelConnectsPathElements(DatabaseIfc db, IfcRelConnectsPathElements r, IfcOwnerHistory ownerHistory, bool downStream) : base(db, r, ownerHistory, downStream)
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

		public IfcPort RelatingPort { get { return mDatabase[mRelatingPort] as IfcPort; } set { mRelatingPort = value.mIndex; } }
		public IfcPort RelatedPort { get { return mDatabase[mRelatedPort] as IfcPort; } set { mRelatedPort = value.mIndex; } }
		public IfcElement RealizingElement { get { return mDatabase[mRealizingElement] as IfcElement; } set { mRealizingElement = value == null ? 0 : value.mIndex; } }

		internal IfcRelConnectsPorts() : base() { }
		internal IfcRelConnectsPorts(DatabaseIfc db, IfcRelConnectsPorts r, IfcOwnerHistory ownerHistory, bool downStream) : base(db, r, ownerHistory)
		{
			RelatingPort = db.Factory.Duplicate(r.RelatingPort, ownerHistory, downStream) as IfcPort;
			RelatedPort = db.Factory.Duplicate(r.RelatedPort, ownerHistory, downStream) as IfcPort;
			if (r.mRealizingElement > 0)
				RealizingElement = db.Factory.Duplicate(r.RealizingElement, ownerHistory, downStream) as IfcElement;
		}
		public IfcRelConnectsPorts(IfcPort relatingPort, IfcPort relatedPort) : base(relatingPort.mDatabase) { RelatingPort = relatingPort; RelatedPort = relatedPort; }

		internal IfcPort getOtherPort(IfcPort p) { return (mRelatedPort == p.mIndex ? mDatabase[mRelatingPort] as IfcPort : mDatabase[mRelatedPort] as IfcPort); }
	}
	[Serializable]
	public partial class IfcRelConnectsPortToElement : IfcRelConnects
	{
		internal int mRelatingPort;// : IfcPort;
		internal int mRelatedElement;// : IfcElement; 

		public IfcPort RelatingPort { get { return mDatabase[mRelatingPort] as IfcPort; } set { mRelatingPort = value.mIndex; } }
		public IfcElement RelatedElement { get { return mDatabase[mRelatedElement] as IfcElement; } set { mRelatedElement = value.mIndex; } }

		internal IfcRelConnectsPortToElement() : base() { }
		internal IfcRelConnectsPortToElement(DatabaseIfc db, IfcRelConnectsPortToElement r, IfcOwnerHistory ownerHistory, bool downStream) : base(db, r, ownerHistory) { RelatingPort = db.Factory.Duplicate(r.RelatingPort, ownerHistory, downStream) as IfcPort; RelatedElement = db.Factory.Duplicate(r.RelatedElement, ownerHistory, downStream) as IfcElement; }
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
		private int mRelatingElement;// : IfcStructuralActivityAssignmentSelect; SELECT(IfcStructuralItem,IfcElement);
		private int mRelatedStructuralActivity;// : IfcStructuralActivity; 

		public IfcStructuralActivityAssignmentSelect RelatingElement { get { return mDatabase[mRelatingElement] as IfcStructuralActivityAssignmentSelect; } set { mRelatingElement = value.Index; value.AssignStructuralActivity(this); } }
		public IfcStructuralActivity RelatedStructuralActivity { get { return mDatabase[mRelatedStructuralActivity] as IfcStructuralActivity; } set { mRelatedStructuralActivity = value.Index; } }

		internal IfcRelConnectsStructuralActivity() : base() { }
		internal IfcRelConnectsStructuralActivity(DatabaseIfc db, IfcRelConnectsStructuralActivity c, IfcOwnerHistory ownerHistory, bool downStream) : base(db, c, ownerHistory)
		{
			//mRelatingElement = c.mRelatingElement; 
			RelatedStructuralActivity = db.Factory.Duplicate(c.RelatedStructuralActivity, ownerHistory, downStream) as IfcStructuralActivity;
		}
		public IfcRelConnectsStructuralActivity(IfcStructuralActivityAssignmentSelect item, IfcStructuralActivity a)
			: base(a.mDatabase) { mRelatingElement = item.Index; mRelatedStructuralActivity = a.mIndex; }
	}
	[Serializable]
	public partial class IfcRelConnectsStructuralElement : IfcRelConnects //DELETED IFC4 Replaced by IfcRelAssignsToProduct
	{
		internal int mRelatingElement;// : IfcElement;
		internal int mRelatedStructuralMember;// : IfcStructuralMember; 

		public IfcElement RelatingElement { get { return mDatabase[mRelatingElement] as IfcElement; } set { mRelatingElement = value.mIndex; value.mHasStructuralMember.Add(this); } }
		public IfcStructuralMember RelatedStructuralMember { get { return mDatabase[mRelatedStructuralMember] as IfcStructuralMember; } set { mRelatedStructuralMember = value.mIndex; value.mStructuralMemberForGG = this; } }

		internal IfcRelConnectsStructuralElement() : base() { }
		internal IfcRelConnectsStructuralElement(DatabaseIfc db, IfcRelConnectsStructuralElement c, IfcOwnerHistory ownerHistory, bool downStream) : base(db, c, ownerHistory)
		{
			RelatingElement = db.Factory.Duplicate(c.RelatingElement, ownerHistory, downStream) as IfcElement;
			RelatedStructuralMember = db.Factory.Duplicate(c.RelatedStructuralMember, ownerHistory, downStream) as IfcStructuralMember;
		}
		public IfcRelConnectsStructuralElement(IfcElement elem, IfcStructuralMember memb) : base(elem.mDatabase)
		{
			if (elem.mDatabase.mRelease != ReleaseVersion.IFC2x3)
				throw new Exception(KeyWord + " Deleted IFC4!");
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
		internal IfcRelConnectsStructuralMember(DatabaseIfc db, IfcRelConnectsStructuralMember r, IfcOwnerHistory ownerHistory, bool downStream) : base(db, r, ownerHistory)
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
		internal IfcRelConnectsWithEccentricity(DatabaseIfc db, IfcRelConnectsWithEccentricity c, IfcOwnerHistory ownerHistory, bool downStream) : base(db, c, ownerHistory, downStream) { ConnectionConstraint = db.Factory.Duplicate(c.ConnectionConstraint) as IfcConnectionGeometry; }
		public IfcRelConnectsWithEccentricity(IfcStructuralMember memb, IfcStructuralConnection connection, IfcConnectionGeometry cg)
			: base(memb, connection) { mConnectionConstraint = cg.mIndex; }
	}
	public partial class IfcRelConnectsWithRealizingElements : IfcRelConnectsElements
	{
		internal SET<IfcElement> mRealizingElements = new SET<IfcElement>();// :	SET [1:?] OF IfcElement;
		internal string mConnectionType = "$";// : :	OPTIONAL IfcLabel; 

		public SET<IfcElement> RealizingElements { get { return mRealizingElements; } }

		internal IfcRelConnectsWithRealizingElements() : base() { }
		internal IfcRelConnectsWithRealizingElements(DatabaseIfc db, IfcRelConnectsWithRealizingElements r, IfcOwnerHistory ownerHistory, bool downStream) : base(db, r, ownerHistory, downStream) { RealizingElements.AddRange(r.RealizingElements.Select(x => db.Factory.Duplicate(x, ownerHistory, downStream) as IfcElement)); mConnectionType = r.mConnectionType; }
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
		//public class RelatedElementsCollection : ObservableCollection<IfcProduct>
		//{
		//	private IfcRelContainedInSpatialStructure mContainer = null;
		//	internal RelatedElementsCollection(IfcRelContainedInSpatialStructure container) { mContainer = container; }

		//	protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
		//	{
		//		base.OnCollectionChanged(e);
		//		if(e.Action == NotifyCollectionChangedAction.Add)
		//		{
		//			foreach (IfcProduct p in e.NewItems)
		//				mContainer.relate(p);
		//		}
		//		else if (e.Action == NotifyCollectionChangedAction.Remove)
		//		{
		//			foreach (IfcProduct p in e.NewItems)
		//				mContainer.relate(p);
		//		}
		//	}

		//}

		internal SET<IfcProduct> mRelatedElements = new SET<IfcProduct>();// : SET [1:?] OF IfcProduct;
		private IfcSpatialElement mRelatingStructure = null;//  IfcSpatialElement 

		//	public RelatedElementsCollection RelatedElements { get; } 
		public SET<IfcProduct> RelatedElements { get { return mRelatedElements; } }
		public IfcSpatialElement RelatingStructure
		{
			get { return mRelatingStructure; }
			set
			{
				if (mRelatingStructure != value)
				{
					mRelatingStructure = value;
					if (value != null && !value.mContainsElements.Contains(this))
						value.mContainsElements.Add(this);
				}
			}
		}

		internal IfcRelContainedInSpatialStructure() : base() { }// RelatedElements = new RelatedElementsCollection(this); }
		internal IfcRelContainedInSpatialStructure(DatabaseIfc db, IfcRelContainedInSpatialStructure r, IfcOwnerHistory ownerHistory, bool downstream) : base(db, r, ownerHistory)
		{
			RelatingStructure = db.Factory.Duplicate(r.RelatingStructure, ownerHistory, false) as IfcSpatialElement;
			if (downstream)
			{
				foreach(IfcProduct p  in r.RelatedElements)
					db.Factory.Duplicate(p, ownerHistory, downstream);
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
		internal IfcRelContainedInSpatialStructure(IfcProduct related, IfcSpatialElement host) : this(host) { RelatedElements.Add(related); }
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
				{
					relate(p);	
				}
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
			IfcElement element = product as IfcElement;
			if (element != null)
			{
				if (element.mContainedInStructure != this)
				{
					if (element.mContainedInStructure != null)
						element.mContainedInStructure.removeObject(element);
					element.mContainedInStructure = this;
				}
			}
			else
			{
				IfcGrid grid = product as IfcGrid;
				if (grid != null)
				{
					if (grid.mContainedInStructure != this)
					{
						if (grid.mContainedInStructure != null)
							grid.mContainedInStructure.removeObject(grid);
						grid.mContainedInStructure = this;
					}
				}
				else
				{
					IfcAnnotation annotation = product as IfcAnnotation;
					if (annotation != null)
					{
						if (annotation.mContainedInStructure != this)
						{
							if (annotation.mContainedInStructure != null)
								annotation.mContainedInStructure.removeObject(annotation);
							annotation.mContainedInStructure = this;
						}

					}
					else
					{
						IfcPositioningElement positioningElement = product as IfcPositioningElement;
						if(positioningElement  != null)
						{
							if (positioningElement.mContainedInStructure != this)
							{
								if (positioningElement.mContainedInStructure != null)
									positioningElement.mContainedInStructure.removeObject(positioningElement);
								positioningElement.mContainedInStructure = this;
							}
						}
					}
				}
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
		internal int mRelatingBuildingElement;// :	IfcElement;  
		private List<int> mRelatedCoverings = new List<int>();// : SET [1:?] OF IfcCovering;

		public IfcElement RelatingBuildingElement { get { return mDatabase[mRelatingBuildingElement] as IfcElement; } set { mRelatingBuildingElement = value.mIndex; } }
		public ReadOnlyCollection<IfcCovering> RelatedCoverings { get { return new ReadOnlyCollection<IfcCovering>(mRelatedCoverings.ConvertAll(x => mDatabase[x] as IfcCovering)); } }

		internal IfcRelCoversBldgElements() : base() { }
		internal IfcRelCoversBldgElements(DatabaseIfc db, IfcRelCoversBldgElements c, IfcOwnerHistory ownerHistory, bool downStream) : base(db, c, ownerHistory) { RelatingBuildingElement = db.Factory.Duplicate(c.RelatingBuildingElement, ownerHistory, downStream) as IfcElement; c.RelatedCoverings.ToList().ForEach(x => addCovering(db.Factory.Duplicate(x, ownerHistory, downStream) as IfcCovering)); }
		public IfcRelCoversBldgElements(IfcElement e, IfcCovering covering) : base(e.mDatabase)
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

		internal void Remove(IfcCovering c) { mRelatedCoverings.Remove(c.mIndex); c.mHasCoverings.Remove(this); }
		internal void addCovering(IfcCovering c) { c.mCoversElements = this; mRelatedCoverings.Add(c.mIndex); }
	}
	[Serializable]
	public partial class IfcRelCoversSpaces : IfcRelConnects //IFC4 DEPRECATION  The relationship IfcRelCoversSpace shall not be used anymore, use IfcRelContainedInSpatialStructure instead.
	{
		internal IfcSpace mRelatedSpace;// : IfcSpace;
		internal SET<IfcCovering> mRelatedCoverings = new SET<IfcCovering>();// SET [1:?] OF IfcCovering; 

		public IfcSpace RelatedSpace { get { return mRelatedSpace; } set { mRelatedSpace = value; value.mHasCoverings.Add(this); } }
		public SET<IfcCovering> RelatedCoverings { get { return mRelatedCoverings; } }

		internal IfcRelCoversSpaces() : base() { }
		internal IfcRelCoversSpaces(DatabaseIfc db, IfcRelCoversSpaces r, IfcOwnerHistory ownerHistory, bool downStream) : base(db, r, ownerHistory)
		{
			RelatedSpace = db.Factory.Duplicate(r.RelatedSpace, ownerHistory, downStream) as IfcSpace;
			RelatedCoverings.AddRange( r.RelatedCoverings.ConvertAll(x => db.Factory.Duplicate(x, ownerHistory, downStream) as IfcCovering));
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
		internal IfcRelDeclares(DatabaseIfc db, IfcRelDeclares r, IfcOwnerHistory ownerHistory, bool downStream) : base(db, r, ownerHistory)
		{
			RelatingContext = db.Factory.Duplicate(r.RelatingContext, ownerHistory, false) as IfcContext;
			if (downStream)
				RelatedDefinitions.AddRange(r.RelatedDefinitions.ConvertAll(x => db.Factory.Duplicate(r.mDatabase[x.Index], ownerHistory, downStream) as IfcDefinitionSelect));
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

		internal override void changeSchema(ReleaseVersion schema)
		{
			base.changeSchema(schema);
			for (int icounter = 0; icounter < mRelatedDefinitions.Count; icounter++)
				mDatabase[mRelatedDefinitions[icounter].Index].changeSchema(schema);
		}
	}
	[Serializable]
	public abstract partial class IfcRelDecomposes : IfcRelationship //ABSTACT  SUPERTYPE OF (ONEOF (IfcRelAggregates ,IfcRelNests ,IfcRelProjectsElement ,IfcRelVoidsElement))
	{
		protected IfcRelDecomposes() : base() { }
		protected IfcRelDecomposes(DatabaseIfc db) : base(db) { }
		protected IfcRelDecomposes(DatabaseIfc db, IfcRelDecomposes d, IfcOwnerHistory ownerHistory) : base(db, d, ownerHistory) { }
	}
	[Serializable]
	public abstract partial class IfcRelDefines : IfcRelationship // 	ABSTRACT SUPERTYPE OF(ONEOF(IfcRelDefinesByObject, IfcRelDefinesByProperties, IfcRelDefinesByTemplate, IfcRelDefinesByType))
	{
		public abstract IfcRoot Relating();

		protected IfcRelDefines() : base() { }
		protected IfcRelDefines(DatabaseIfc db) : base(db) { }
		protected IfcRelDefines(DatabaseIfc db, IfcRelDefines d, IfcOwnerHistory ownerHistory) : base(db, d, ownerHistory) { }
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
		internal IfcRelDefinesByObject(DatabaseIfc db, IfcRelDefinesByObject r, IfcOwnerHistory ownerHistory, bool downStream) : base(db, r, ownerHistory) { r.RelatedObjects.ToList().ForEach(x => addRelated(db.Factory.Duplicate(x, ownerHistory, downStream) as IfcObject)); RelatingObject = db.Factory.Duplicate(r.RelatingObject, ownerHistory, downStream) as IfcObject; }
		public IfcRelDefinesByObject(IfcObject relObj) : base(relObj.mDatabase) { mRelatingObject = relObj.mIndex; relObj.mIsDeclaredBy = this; }

		internal void addRelated(IfcObject obj) { mRelatedObjects.Add(obj.mIndex); obj.mIsDeclaredBy = this; }
	}
	[Serializable]
	public partial class IfcRelDefinesByProperties : IfcRelDefines
	{
		private SET<IfcObjectDefinition> mRelatedObjects = new SET<IfcObjectDefinition>();// IFC4 change	SET [1:1] OF IfcObjectDefinition; ifc2x3 : SET [1:?] OF IfcObject;  
		private IfcPropertySetDefinition mRelatingPropertyDefinition;// : IfcPropertySetDefinition; 

		public SET<IfcObjectDefinition> RelatedObjects { get { return mRelatedObjects; } }
		public IfcPropertySetDefinition RelatingPropertyDefinition { get { return mRelatingPropertyDefinition; } set { mRelatingPropertyDefinition = value; } }

		public override IfcRoot Relating() { return RelatingPropertyDefinition; } 

		internal IfcRelDefinesByProperties() : base() { }
		private IfcRelDefinesByProperties(DatabaseIfc db) : base(db) { Name = "NameRelDefinesByProperties"; Description = "DescriptionRelDefinesByProperties"; }
		internal IfcRelDefinesByProperties(DatabaseIfc db, IfcRelDefinesByProperties d, IfcOwnerHistory ownerHistory, bool downStream) : base(db, d, ownerHistory)
		{
			//RelatedObjects = d.RelatedObjects.ConvertAll(x=>db.Factory.Duplicate(x) as IfcObjectDefinition);
			RelatingPropertyDefinition = db.Factory.Duplicate(d.RelatingPropertyDefinition, ownerHistory, downStream) as IfcPropertySetDefinition;
		}
		public IfcRelDefinesByProperties(IfcPropertySetDefinition propertySet) : base(propertySet.mDatabase) { mRelatingPropertyDefinition = propertySet; }
		public IfcRelDefinesByProperties(IfcObjectDefinition relatedObject, IfcPropertySetDefinition propertySet) : this(propertySet) { RelatedObjects.Add(relatedObject); }
		public IfcRelDefinesByProperties(IEnumerable<IfcObjectDefinition> relatedObjects, IfcPropertySetDefinition propertySet) : this(propertySet) { RelatedObjects.AddRange(relatedObjects); }

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
		internal override void changeSchema(ReleaseVersion schema)
		{
			base.changeSchema(schema);
			RelatingPropertyDefinition.changeSchema(schema);
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
		internal IfcRelDefinesByTemplate(DatabaseIfc db, IfcRelDefinesByTemplate r, IfcOwnerHistory ownerHistory, bool downStream) : base(db, r, ownerHistory) { r.RelatedPropertySets.ToList().ForEach(x => AddRelated(db.Factory.Duplicate(x, ownerHistory, downStream) as IfcPropertySetDefinition)); RelatingTemplate = db.Factory.Duplicate(r.RelatingTemplate, ownerHistory, downStream) as IfcPropertySetTemplate; }
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

		internal SET<IfcObject> RelatedObjects { get { return mRelatedObjects; } set { mRelatedObjects.Clear(); if (value != null) { mRelatedObjects.CollectionChanged -= mRelatedObjects_CollectionChanged; mRelatedObjects = value; mRelatedObjects.CollectionChanged += mRelatedObjects_CollectionChanged; } } }
		public IfcTypeObject RelatingType { get { return mRelatingType; } set { mRelatingType = value; value.mObjectTypeOf = this; } }

		public override IfcRoot Relating() { return RelatingType; } 

		internal IfcRelDefinesByType() : base() { }
		internal IfcRelDefinesByType(DatabaseIfc db, IfcRelDefinesByType r, IfcOwnerHistory ownerHistory, bool downStream) : base(db, r, ownerHistory)
		{
			//mRelatedObjects = new List<int>(d.mRelatedObjects.ToArray()); 
			RelatingType = db.Factory.Duplicate(r.RelatingType, ownerHistory, downStream) as IfcTypeObject;
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
						o.IsTypedBy = this;
				}
					
			}
			if (e.OldItems != null)
			{
				foreach (IfcObject o in e.OldItems)
					o.IsTypedBy = null;
			}
		}
	}
	[Serializable]
	public partial class IfcRelFillsElement : IfcRelConnects
	{
		private int mRelatingOpeningElement;// : IfcOpeningElement;
		private int mRelatedBuildingElement;// :OPTIONAL IfcElement; 

		public IfcOpeningElement RelatingOpeningElement { get { return mDatabase[mRelatingOpeningElement] as IfcOpeningElement; } set { mRelatingOpeningElement = value.mIndex; } }
		public IfcElement RelatedBuildingElement { get { return mDatabase[mRelatedBuildingElement] as IfcElement; } set { mRelatedBuildingElement = value.mIndex; } }

		internal IfcRelFillsElement() : base() { }
		internal IfcRelFillsElement(DatabaseIfc db, IfcRelFillsElement r, IfcOwnerHistory ownerHistory, bool downStream) : base(db, r, ownerHistory) { RelatingOpeningElement = db.Factory.Duplicate(r.RelatingOpeningElement, ownerHistory, downStream) as IfcOpeningElement; RelatedBuildingElement = db.Factory.Duplicate(r.RelatedBuildingElement, ownerHistory, downStream) as IfcElement; }
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
		internal IfcRelFlowControlElements(DatabaseIfc db, IfcRelFlowControlElements r, IfcOwnerHistory ownerHistory, bool downStream) : base(db, r, ownerHistory) { RelatingPort = db.Factory.Duplicate(r.RelatingPort, ownerHistory, downStream) as IfcPort; RelatedElement = db.Factory.Duplicate(r.RelatedElement, ownerHistory, downStream) as IfcElement; }
	}
	//[Obsolete("DEPRECEATED IFC4", false)]
	//ENTITY IfcRelInteractionRequirements  // DEPRECEATED IFC4
	[Serializable]
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
		internal IfcRelInterferesElements(DatabaseIfc db, IfcRelInterferesElements r, IfcOwnerHistory ownerHistory, bool downStream) : base(db, r, ownerHistory)
		{
			RelatingElement = db.Factory.Duplicate(r.RelatingElement, ownerHistory, downStream) as IfcElement;
			RelatedElement = db.Factory.Duplicate(r.RelatedElement, ownerHistory, downStream) as IfcElement;
			if (r.mInterferenceGeometry > 0)
				InterferenceGeometry = db.Factory.Duplicate(r.InterferenceGeometry, ownerHistory, downStream) as IfcConnectionGeometry;
			mInterferenceType = r.mInterferenceType;
			mImpliedOrder = r.mImpliedOrder;
		}
		public IfcRelInterferesElements(IfcElement relatingElement, IfcElement relatedElement)
			: base(relatingElement.mDatabase) { RelatingElement = relatingElement; RelatedElement = relatedElement; }
	}
	[Serializable]
	public partial class IfcRelNests : IfcRelDecomposes
	{
		internal IfcObjectDefinition mRelatingObject;// : IfcObjectDefinition 
		internal List<int> mRelatedObjects = new List<int>();// : SET [1:?] OF IfcObjectDefinition; 

		public IfcObjectDefinition RelatingObject { get { return mRelatingObject; } set { mRelatingObject = value; if (value != null && !value.IsNestedBy.Contains(this)) value.IsNestedBy.Add(this); } }
		public ReadOnlyCollection<IfcObjectDefinition> RelatedObjects { get { return new ReadOnlyCollection<IfcObjectDefinition>(mRelatedObjects.ConvertAll(x => mDatabase[x] as IfcObjectDefinition)); } }

		internal IfcRelNests() : base() { }
		internal IfcRelNests(DatabaseIfc db, IfcRelNests n, IfcOwnerHistory ownerHistory, bool downStream) : base(db, n, ownerHistory) { RelatingObject = db.Factory.Duplicate(n.RelatingObject, ownerHistory, downStream) as IfcObjectDefinition; n.RelatedObjects.ToList().ForEach(x => addRelated(db.Factory.Duplicate(x, ownerHistory, downStream) as IfcObjectDefinition)); }
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

		internal void addRelated(IfcObjectDefinition o)
		{
			o.Nests = this;
			if (!mRelatedObjects.Contains(o.mIndex))
				mRelatedObjects.Add(o.mIndex);
		}
		internal bool removeObject(IfcObjectDefinition o)
		{
			o.mDecomposes = null;
			return mRelatedObjects.Remove(o.mIndex);
		}

		internal override void changeSchema(ReleaseVersion schema)
		{
			base.changeSchema(schema);
			ReadOnlyCollection<IfcObjectDefinition> ods = RelatedObjects;
			for (int icounter = 0; icounter < ods.Count; icounter++)
				ods[icounter].changeSchema(schema);
			if (schema < ReleaseVersion.IFC4)
			{
				string obj = RelatingObject.KeyWord;
				bool valid = true;
				foreach (IfcObjectDefinition od in RelatedObjects)
				{
					if (string.Compare(obj, od.KeyWord, true) != 0)
					{
						valid = false;
						break;
					}
				}
				if (!valid)
				{
					IfcTypeProduct typeProduct = RelatingObject as IfcTypeProduct;
					if (typeProduct != null)
					{
						List<IfcDistributionPort> ports = RelatedObjects.OfType<IfcDistributionPort>().ToList();
						if (ports.Count > 0)
						{
							new IfcRelAggregates(typeProduct, ports);
						}

					}
					else
					{
						foreach (IfcObjectDefinition od in RelatedObjects)
						{
							IfcDistributionPort port = od as IfcDistributionPort;
							if (port != null && port.mContainedIn == null)
								port.Dispose(true);
						}
						Dispose(false);
					}
				}
			}
		}
	}
	//[Obsolete("DEPRECEATED IFC4", false)]
	//ENTITY IfcRelOccupiesSpaces // DEPRECEATED IFC4
	[Obsolete("DEPRECEATED IFC4", false)]
	public partial class IfcRelOverridesProperties : IfcRelDefinesByProperties // DEPRECEATED IFC4
	{
		internal override string KeyWord { get { return (mDatabase.mRelease <= ReleaseVersion.IFC2x3 ? base.KeyWord : "IFCRELOVERRIDESPROPERTIES"); } }
		private List<int> mOverridingProperties = new List<int>();// : 	SET [1:?] OF IfcProperty;

		public ReadOnlyCollection<IfcProperty> OverridingProperties { get { return new ReadOnlyCollection<IfcProperty>(mOverridingProperties.ConvertAll(x => mDatabase[x] as IfcProperty)); } }

		internal IfcRelOverridesProperties() : base() { }
		internal IfcRelOverridesProperties(DatabaseIfc db, IfcRelOverridesProperties d, IfcOwnerHistory ownerHistory, bool downStream) : base(db, d, ownerHistory, downStream)
		{
			mOverridingProperties = d.OverridingProperties.ToList().ConvertAll(x => db.Factory.Duplicate(x, ownerHistory, downStream).mIndex);
		}
		internal IfcRelOverridesProperties(IfcPropertySetDefinition ifcproperty) : base(ifcproperty) { }
		public IfcRelOverridesProperties(IfcObjectDefinition od, IfcPropertySetDefinition ifcproperty) : base(od, ifcproperty) { }
		public IfcRelOverridesProperties(List<IfcObjectDefinition> objs, IfcPropertySetDefinition ifcproperty) : base(objs, ifcproperty) { }
	}
	[Serializable]
	public partial class IfcRelProjectsElement : IfcRelDecomposes // IFC2x3 IfcRelDecomposes
	{
		internal int mRelatingElement;// : IfcElement; 
		internal int mRelatedFeatureElement;// : IfcFeatureElementAddition

		public IfcElement RelatingElement { get { return mDatabase[mRelatingElement] as IfcElement; } set { mRelatingElement = value.mIndex; } }
		public IfcFeatureElementAddition RelatedFeatureElement { get { return mDatabase[mRelatedFeatureElement] as IfcFeatureElementAddition; } set { mRelatedFeatureElement = value.mIndex; } }

		protected IfcRelProjectsElement() : base() { }
		protected IfcRelProjectsElement(DatabaseIfc db, IfcRelProjectsElement p, IfcOwnerHistory ownerHistory, bool downStream) : base(db, p, ownerHistory) { RelatingElement = db.Factory.Duplicate(p.RelatingElement, ownerHistory, downStream) as IfcElement; RelatedFeatureElement = db.Factory.Duplicate(p.RelatedFeatureElement, ownerHistory, downStream) as IfcFeatureElementAddition; }
		protected IfcRelProjectsElement(IfcElement e, IfcFeatureElementAddition a) : base(e.mDatabase) { mRelatingElement = e.mIndex; mRelatedFeatureElement = a.mIndex; }
	}
	[Serializable]
	public partial class IfcRelReferencedInSpatialStructure : IfcRelConnects
	{
		internal SET<IfcProduct> mRelatedElements = new SET<IfcProduct>();// : SET [1:?] OF IfcProduct;
		private int mRelatingStructure;//  IfcSpatialElement 

		public SET<IfcProduct> RelatedElements { get { return mRelatedElements; } }
		public IfcSpatialElement RelatingStructure { get { return mDatabase[mRelatingStructure] as IfcSpatialElement; } set { mRelatingStructure = value.mIndex; value.mReferencesElements.Add(this); } }

		internal IfcRelReferencedInSpatialStructure() : base() { }
		internal IfcRelReferencedInSpatialStructure(DatabaseIfc db, IfcRelReferencedInSpatialStructure r, IfcOwnerHistory ownerHistory, bool downstream) : base(db, r, ownerHistory)
		{
			if (downstream)
				RelatedElements.AddRange(r.RelatedElements.Select(x => db.Factory.Duplicate(x, ownerHistory, downstream) as IfcProduct));
			RelatingStructure = db.Factory.Duplicate(r.RelatingStructure, ownerHistory, false) as IfcSpatialElement;
		}
		public IfcRelReferencedInSpatialStructure(IfcSpatialElement e) : base(e.mDatabase)
		{
			mRelatingStructure = e.mIndex;
			e.mReferencesElements.Add(this);
		}
		public IfcRelReferencedInSpatialStructure(IfcProduct related, IfcSpatialElement relating) : this(relating)
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
				foreach (IfcProduct p in e.NewItems)
				{
					p.mReferencedInStructures.Add(this);
					//if (p is IfcElement element)
					//	element.mReferencedInStructures.Add(this);
					//else if (p is IfcSpatialElement spatial)
					//	spatial.mReferencedInStructures.Add(this);

				}
			}
			if (e.OldItems != null)
			{
				foreach (IfcProduct p in e.OldItems)
				{
					p.mReferencedInStructures.Remove(this);
					//if (p is IfcElement element)
					//	element.mReferencedInStructures.Remove(this);
					//else if (p is IfcSpatialElement spatial)
					//	spatial.mReferencedInStructures.Remove(this);
				}	
			}
		}
	}
	//[Obsolete("DEPRECEATED IFC4", false)]
	//ENTITY IfcRelSchedulesCostItems // DEPRECEATED IFC4 
	[Serializable]
	public partial class IfcRelSequence : IfcRelConnects
	{
		internal int mRelatingProcess;// : IfcProcess;
		internal int mRelatedProcess;//  IfcProcess;
		private int mTimeLag;// : OPTIONAL IfcLagTime; IFC2x3 	IfcTimeMeasure
		private double mTimeLagSS = double.NaN;// : OPTIONAL IfcLagTime; IFC2x3 	IfcTimeMeasure
		internal IfcSequenceEnum mSequenceType = IfcSequenceEnum.NOTDEFINED;//	 :	OPTIONAL IfcSequenceEnum;
		internal string mUserDefinedSequenceType = "$";//	 :	OPTIONAL IfcLabel; 

		public IfcProcess RelatingProcess { get { return mDatabase[mRelatingProcess] as IfcProcess; } set { mRelatingProcess = value.mIndex; } }
		public IfcProcess RelatedProcess { get { return mDatabase[mRelatedProcess] as IfcProcess; } set { mRelatedProcess = value.mIndex; } }
		public IfcLagTime TimeLag { get { return mDatabase[mTimeLag] as IfcLagTime; } set { mTimeLag = (value == null ? 0 : value.mIndex); } }

		internal IfcRelSequence() : base() { }
		internal IfcRelSequence(DatabaseIfc db, IfcRelSequence s, IfcOwnerHistory ownerHistory, bool downStream) : base(db, s, ownerHistory)
		{
			RelatingProcess = db.Factory.Duplicate(s.RelatingProcess, ownerHistory, downStream) as IfcProcess;
			RelatedProcess = db.Factory.Duplicate(s.RelatedProcess, ownerHistory, downStream) as IfcProcess;
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
	public partial class IfcRelServicesBuildings : IfcRelConnects
	{
		internal int mRelatingSystem;// : IfcSystem;
		internal List<int> mRelatedBuildings = new List<int>();// : SET [1:?] OF IfcSpatialElement  ;

		public IfcSystem RelatingSystem { get { return mDatabase[mRelatingSystem] as IfcSystem; } set { mRelatingSystem = value.mIndex; value.ServicesBuildings = this; } }
		public ReadOnlyCollection<IfcSpatialElement> RelatedBuildings { get { return new ReadOnlyCollection<IfcSpatialElement>(mRelatedBuildings.ConvertAll(x => mDatabase[x] as IfcSpatialElement)); } }

		internal IfcRelServicesBuildings() : base() { }
		internal IfcRelServicesBuildings(DatabaseIfc db, IfcRelServicesBuildings s, IfcOwnerHistory ownerHistory, bool downStream) : base(db, s, ownerHistory)
		{
			RelatingSystem = db.Factory.Duplicate(s.RelatingSystem, ownerHistory, downStream) as IfcSystem;
			s.RelatedBuildings.ToList().ForEach(x => addRelated(db.Factory.Duplicate(x, false) as IfcSpatialElement));
		}
		public IfcRelServicesBuildings(IfcSystem system, IfcSpatialElement se)
			: base(system.mDatabase) { mRelatingSystem = system.mIndex; mRelatedBuildings.Add(se.mIndex); se.mServicedBySystems.Add(this); }

		internal void addRelated(IfcSpatialElement spatial)
		{
			mRelatedBuildings.Add(spatial.mIndex);
			spatial.mServicedBySystems.Add(this);
		}
	}
	[Serializable]
	public partial class IfcRelSpaceBoundary : IfcRelConnects
	{
		internal int mRelatingSpace;// :	IfcSpaceBoundarySelect; : IfcSpace;
		internal int mRelatedBuildingElement;// :OPTIONAL IfcElement; Mandatory in IFC4
		internal int mConnectionGeometry;// : OPTIONAL IfcConnectionGeometry;
		internal IfcPhysicalOrVirtualEnum mPhysicalOrVirtualBoundary = IfcPhysicalOrVirtualEnum.NOTDEFINED;// : IfcPhysicalOrVirtualEnum;
		internal IfcInternalOrExternalEnum mInternalOrExternalBoundary = IfcInternalOrExternalEnum.NOTDEFINED;// : IfcInternalOrExternalEnum; 

		public IfcSpaceBoundarySelect RelatingSpace { get { return mDatabase[mRelatingSpace] as IfcSpaceBoundarySelect; } set { mRelatingSpace = value.Index; } }
		public IfcElement RelatedBuildingElement { get { return mDatabase[mRelatedBuildingElement] as IfcElement; } set { mRelatedBuildingElement = value == null ? 0 : value.mIndex; } }
		public IfcConnectionGeometry ConnectionGeometry { get { return mDatabase[mConnectionGeometry] as IfcConnectionGeometry; } set { mConnectionGeometry = (value == null ? 0 : value.mIndex); } }

		internal IfcRelSpaceBoundary() : base() { }
		internal IfcRelSpaceBoundary(DatabaseIfc db, IfcRelSpaceBoundary b, IfcOwnerHistory ownerHistory, bool downStream) : base(db, b, ownerHistory)
		{
			RelatingSpace = db.Factory.Duplicate(b.mDatabase[b.mRelatingSpace], ownerHistory, downStream) as IfcSpaceBoundarySelect;
			if (b.mRelatedBuildingElement > 0)
				RelatedBuildingElement = db.Factory.Duplicate(b.RelatedBuildingElement, ownerHistory, downStream) as IfcElement;
			if (b.mConnectionGeometry > 0)
				ConnectionGeometry = db.Factory.Duplicate(b.ConnectionGeometry, ownerHistory, downStream) as IfcConnectionGeometry;
			mPhysicalOrVirtualBoundary = b.mPhysicalOrVirtualBoundary;
			mInternalOrExternalBoundary = b.mInternalOrExternalBoundary;
		}
		public IfcRelSpaceBoundary(IfcSpaceBoundarySelect s, IfcElement e, IfcPhysicalOrVirtualEnum virt, IfcInternalOrExternalEnum intern) : base(s.Database)
		{
			mRelatingSpace = s.Index;
			s.AddBoundary(this);
			mRelatedBuildingElement = e.mIndex;
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

		public IfcRelSpaceBoundary1stLevel ParentBoundary { get { return mDatabase[mParentBoundary] as IfcRelSpaceBoundary1stLevel; } set { mParentBoundary = value.mIndex; } }
		internal IfcRelSpaceBoundary1stLevel() : base() { }
		internal IfcRelSpaceBoundary1stLevel(DatabaseIfc db, IfcRelSpaceBoundary1stLevel r, IfcOwnerHistory ownerHistory, bool downStream) : base(db, r, ownerHistory, downStream) { ParentBoundary = db.Factory.Duplicate(r.ParentBoundary, ownerHistory, downStream) as IfcRelSpaceBoundary1stLevel; }
		public IfcRelSpaceBoundary1stLevel(IfcSpaceBoundarySelect s, IfcElement e, IfcPhysicalOrVirtualEnum virt, IfcInternalOrExternalEnum intern, IfcRelSpaceBoundary1stLevel parent)
			: base(s, e, virt, intern) { mParentBoundary = parent.mIndex; }
	}
	[Serializable]
	public partial class IfcRelSpaceBoundary2ndLevel : IfcRelSpaceBoundary1stLevel
	{
		internal int mCorrespondingBoundary;// :	IfcRelSpaceBoundary2ndLevel;
											//INVERSE	
		internal List<IfcRelSpaceBoundary2ndLevel> mCorresponds = new List<IfcRelSpaceBoundary2ndLevel>();//	:	SET OF IfcRelSpaceBoundary1stLevel FOR ParentBoundary;

		public IfcRelSpaceBoundary2ndLevel CorrespondingBoundary { get { return mDatabase[mCorrespondingBoundary] as IfcRelSpaceBoundary2ndLevel; } set { mCorrespondingBoundary = value.mIndex; } }

		internal IfcRelSpaceBoundary2ndLevel() : base() { }
		internal IfcRelSpaceBoundary2ndLevel(DatabaseIfc db, IfcRelSpaceBoundary2ndLevel r, IfcOwnerHistory ownerHistory, bool downStream) : base(db, r, ownerHistory, downStream) { CorrespondingBoundary = db.Factory.Duplicate(r.CorrespondingBoundary, ownerHistory, downStream) as IfcRelSpaceBoundary2ndLevel; }
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
		internal IfcRelVoidsElement(DatabaseIfc db, IfcRelVoidsElement v, IfcOwnerHistory ownerHistory, bool downStream) : base(db, v, ownerHistory)
		{
			RelatedOpeningElement = db.Factory.Duplicate(v.RelatedOpeningElement, ownerHistory, downStream) as IfcFeatureElementSubtraction;
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
		internal IfcReparametrisedCompositeCurveSegment(DatabaseIfc db, IfcReparametrisedCompositeCurveSegment s) : base(db, s) { mParamLength = s.mParamLength; }
		public IfcReparametrisedCompositeCurveSegment(IfcTransitionCode tc, bool sense, IfcBoundedCurve bc, double paramLength) : base(tc, sense, bc) { mParamLength = paramLength; }
	}
	[Serializable]
	public partial class IfcRepresentation : BaseClassIfc, IfcLayeredItem // Abstract IFC4 ,SUPERTYPE OF (ONEOF(IfcShapeModel,IfcStyleModel));
	{
		private IfcRepresentationContext mContextOfItems = null;// : IfcRepresentationContext;
		internal string mRepresentationIdentifier = "";//  : OPTIONAL IfcLabel; //RepresentationIdentifier: Name of the representation, e.g. 'Body' for 3D shape, 'FootPrint' for 2D ground view, 'Axis' for reference axis, 		
		private string mRepresentationType = "";//  : OPTIONAL IfcLabel;
		private SET<IfcRepresentationItem> mItems = new SET<IfcRepresentationItem>();//  : SET [1:?] OF IfcRepresentationItem; 
		//INVERSE 
		internal IfcRepresentationMap mRepresentationMap = null;//	 : 	SET [0:1] OF IfcRepresentationMap FOR MappedRepresentation;
		internal SET<IfcPresentationLayerAssignment> mLayerAssignments = new SET<IfcPresentationLayerAssignment>();// new List<>();//	IFC4 change : 	SET OF IfcPresentationLayerAssignment FOR AssignedItems;
		internal List<IfcProductRepresentation> mOfProductRepresentation = new List<IfcProductRepresentation>();/// IFC4 change	 : 	SET [0:n] OF IfcProductRepresentation FOR Representations;

		public IfcRepresentationContext ContextOfItems
		{
			get { return mContextOfItems; }
			set
			{
				mContextOfItems = value;
				if (value != null)
				{
					IfcRepresentation existing = value.RepresentationsInContext.Where(x => x.Index == mIndex).FirstOrDefault();
					if(existing == null)
						value.RepresentationsInContext.Add(this);
					mRepresentationIdentifier = value.ContextIdentifier;
				}
			}
		}
		public string RepresentationIdentifier { get { return mRepresentationIdentifier; } set { mRepresentationIdentifier = value; } }
		public string RepresentationType { get { return mRepresentationType; } set { mRepresentationType = value; } }
		public SET<IfcRepresentationItem> Items { get { return mItems; } set { mItems.Clear(); if (value != null) { mItems.CollectionChanged -= mItems_CollectionChanged; mItems = value; mItems.CollectionChanged += mItems_CollectionChanged; } } }

		public SET<IfcPresentationLayerAssignment> LayerAssignments { get { return mLayerAssignments; } }
		public ReadOnlyCollection<IfcProductRepresentation> OfProductRepresentation { get { return new ReadOnlyCollection<IfcProductRepresentation>(mOfProductRepresentation); } }

		protected IfcRepresentation() : base() { }
		protected IfcRepresentation(DatabaseIfc db, IfcRepresentation r) : base(db)
		{
			ContextOfItems = db.Factory.Duplicate(r.ContextOfItems) as IfcRepresentationContext;

			mRepresentationIdentifier = r.mRepresentationIdentifier;
			mRepresentationType = r.mRepresentationType;
			Items.AddRange(r.Items.ConvertAll(x => db.Factory.Duplicate(x) as IfcRepresentationItem));

			foreach (IfcPresentationLayerAssignment layerAssignment in r.mLayerAssignments)
			{
				IfcPresentationLayerAssignment la = db.Factory.Duplicate(layerAssignment, false) as IfcPresentationLayerAssignment;
				la.AssignedItems.Add(this);
			}
		}
		protected IfcRepresentation(IfcRepresentationContext context) : base(context.mDatabase) { ContextOfItems = context; RepresentationIdentifier = context.ContextIdentifier; }
		protected IfcRepresentation(IfcRepresentationContext context, IfcRepresentationItem ri) : this(context) { mItems.Add(ri); }
		protected IfcRepresentation(IfcRepresentationContext context, List<IfcRepresentationItem> reps) : this(context) { Items.AddRange(reps); }
		protected override void initialize()
		{
			base.initialize();
			mItems.CollectionChanged += mItems_CollectionChanged;
		}
		private void mItems_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			if (mDatabase != null && mDatabase.IsDisposed())
				return;
			if (e.NewItems != null)
			{
				foreach (IfcRepresentationItem r in e.NewItems)
					r.mRepresents.Add(this);
			}
			if (e.OldItems != null)
			{
				foreach (IfcRepresentationItem r in e.OldItems)
					r.mRepresents.Remove(this);
			}
		}
		protected override List<T> Extract<T>(Type type)
		{
			List<T> result = base.Extract<T>(type);
			foreach (IfcRepresentationItem item in Items)
				result.AddRange(item.Extract<T>());
			return result;
		}

		internal override void changeSchema(ReleaseVersion schema)
		{
			base.changeSchema(schema);
			foreach (IfcRepresentationItem item in Items)
				item.changeSchema(schema);
		}
		public void AssignLayer(IfcPresentationLayerAssignment layer) { mLayerAssignments.Add(layer); }
	}
	[Serializable]
	public abstract partial class IfcRepresentationContext : BaseClassIfc //ABSTRACT SUPERTYPE OF(IfcGeometricRepresentationContext);
	{
		internal string mContextIdentifier = "$";// : OPTIONAL IfcLabel;
		internal string mContextType = "$";// : OPTIONAL IfcLabel; 
										   //INVERSE
		[NonSerialized] private SET<IfcRepresentation> mRepresentationsInContext = new SET<IfcRepresentation>();// :	SET OF IfcRepresentation FOR ContextOfItems;

		public string ContextIdentifier { get { return (mContextIdentifier == "$" ? "" : ParserIfc.Decode(mContextIdentifier)); } set { mContextIdentifier = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public string ContextType { get { return (mContextType == "$" ? "" : ParserIfc.Decode(mContextType)); } set { mContextType = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public SET<IfcRepresentation> RepresentationsInContext { get { return mRepresentationsInContext; } private set { mRepresentationsInContext = value; } }

		protected IfcRepresentationContext() : base() { }
		protected IfcRepresentationContext(DatabaseIfc db) : base(db) { }
		protected IfcRepresentationContext(DatabaseIfc db, IfcRepresentationContext c) : base(db, c) { mContextIdentifier = c.mContextIdentifier; mContextType = c.mContextType; }
	}
	[Serializable]
	public abstract partial class IfcRepresentationItem : BaseClassIfc, IfcLayeredItem /*(IfcGeometricRepresentationItem,IfcMappedItem,IfcStyledItem,IfcTopologicalRepresentationItem));*/
	{ //INVERSE
		internal SET<IfcPresentationLayerAssignment> mLayerAssignments = new SET<IfcPresentationLayerAssignment>();// : SET [0:?] OF IfcPresentationLayerAssignment FOR AssignedItems;
		internal IfcStyledItem mStyledByItem = null;// : SET [0:1] OF IfcStyledItem FOR Item; 

		internal List<IfcRepresentation> mRepresents = new List<IfcRepresentation>();

		public SET<IfcPresentationLayerAssignment> LayerAssignments { get { return mLayerAssignments; } }
		public IfcStyledItem StyledByItem { get { return mStyledByItem; } set { if (value != null) value.Item = this; else mStyledByItem = null; } }


		protected IfcRepresentationItem() : base() { }
		protected IfcRepresentationItem(DatabaseIfc db, IfcRepresentationItem i) : base(db, i)
		{
			foreach (IfcPresentationLayerAssignment layerAssignment in i.mLayerAssignments)
			{
				IfcPresentationLayerAssignment la = db.Factory.Duplicate(layerAssignment, false) as IfcPresentationLayerAssignment;
				la.AssignedItems.Add(this);
			}

			if (i.mStyledByItem != null)
			{
				IfcStyledItem si = db.Factory.Duplicate(i.mStyledByItem) as IfcStyledItem;
				si.Item = this;
			}
		}
		protected IfcRepresentationItem(DatabaseIfc db) : base(db) { }

		public void AssignLayer(IfcPresentationLayerAssignment layer) { mLayerAssignments.Add(layer); }
	}
	[Serializable]
	public partial class IfcRepresentationMap : BaseClassIfc, IfcProductRepresentationSelect
	{
		internal int mMappingOrigin;// : IfcAxis2Placement;
		internal int mMappedRepresentation;// : IfcRepresentation;
										   //INVERSE
		internal List<IfcShapeAspect> mHasShapeAspects = new List<IfcShapeAspect>();//	:	SET [0:?] OF IfcShapeAspect FOR PartOfProductDefinitionShape;
		internal List<IfcMappedItem> mMapUsage = new List<IfcMappedItem>();//: 	SET OF IfcMappedItem FOR MappingSource;

		public IfcAxis2Placement MappingOrigin { get { return mDatabase[mMappingOrigin] as IfcAxis2Placement; } set { mMappingOrigin = value.Index; } }
		public IfcShapeModel MappedRepresentation { get { return mDatabase[mMappedRepresentation] as IfcShapeModel; } set { mMappedRepresentation = value.mIndex; value.mRepresentationMap = this; } }
		public ReadOnlyCollection<IfcShapeAspect> HasShapeAspects { get { return new ReadOnlyCollection<IfcShapeAspect>(mHasShapeAspects); } }
		public ReadOnlyCollection<IfcMappedItem> MapUsage { get { return new ReadOnlyCollection<IfcMappedItem>(mMapUsage); } }

		internal List<IfcTypeProduct> mRepresents = new List<IfcTypeProduct>();// GG

		internal IfcRepresentationMap() : base() { }
		internal IfcRepresentationMap(DatabaseIfc db, IfcRepresentationMap m) : base(db, m) { MappingOrigin = db.Factory.Duplicate(m.mDatabase[m.mMappingOrigin]) as IfcAxis2Placement; MappedRepresentation = db.Factory.Duplicate(m.MappedRepresentation) as IfcShapeModel; }
		public IfcRepresentationMap(IfcAxis2Placement placement, IfcShapeRepresentation representation) : base(representation.mDatabase) { mMappingOrigin = placement.Index; MappedRepresentation = representation; }
		public IfcRepresentationMap(IfcAxis2Placement placement, IfcTopologyRepresentation representation) : base(representation.mDatabase) { mMappingOrigin = placement.Index; MappedRepresentation = representation; }

		public void AddShapeAspect(IfcShapeAspect aspect) { mHasShapeAspects.Add(aspect); }

		internal override void changeSchema(ReleaseVersion schema)
		{
			MappedRepresentation.changeSchema(schema);
			foreach (IfcShapeAspect aspect in mHasShapeAspects)
			{
				IfcShapeAspect sa = mDatabase[aspect.mIndex] as IfcShapeAspect;
				if (sa != null)
					mDatabase[aspect.mIndex] = null;

			}
			base.changeSchema(schema);
		}
	}
	[Serializable]
	public abstract partial class IfcResource : IfcObject //ABSTRACT SUPERTYPE OF (ONEOF (IfcConstructionResource))
	{
		internal string mIdentification = "";// :OPTIONAL IfcIdentifier;
		internal string mLongDescription = "";//: OPTIONAL IfcText; 
		//INVERSE 
		internal List<IfcRelAssignsToResource> mResourceOf = new List<IfcRelAssignsToResource>();// : SET [0:?] OF IfcRelAssignsToResource FOR RelatingResource; 

		public string Identification { get { return mIdentification; } set { mIdentification = value; } }
		public string LongDescription { get { return mLongDescription; } set { mLongDescription = value; } }

		protected IfcResource() : base() { }
		protected IfcResource(DatabaseIfc db, IfcResource r, IfcOwnerHistory ownerHistory, bool downStream) : base(db, r, ownerHistory, downStream) { }
		protected IfcResource(DatabaseIfc db) : base(db) { }
	}
	[Serializable]
	public partial class IfcResourceConstraintRelationship : IfcResourceLevelRelationship  // IfcPropertyConstraintRelationship; // DEPRECEATED IFC4 renamed
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

		public override bool Dispose(bool children)
		{
			for (int icounter = 0; icounter < mRelatedResourceObjects.Count; icounter++)
			{
				BaseClassIfc bc = mDatabase[mRelatedResourceObjects[icounter]];
				if (bc != null)
					bc.Dispose(children);
			}
			return base.Dispose(children);
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
		SET<IfcExternalReferenceRelationship> HasExternalReferences { get; set; }
		void AddConstraintRelationShip(IfcResourceConstraintRelationship constraintRelationship);
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
		internal IfcRevolvedAreaSolid(DatabaseIfc db, IfcRevolvedAreaSolid s) : base(db, s) { Axis = db.Factory.Duplicate(s.Axis) as IfcAxis1Placement; mAngle = s.mAngle; }
		public IfcRevolvedAreaSolid(IfcProfileDef profile, IfcAxis2Placement3D position, IfcAxis1Placement axis, double angle) : base(profile, position) { Axis = axis; mAngle = angle; }
	}
	[Serializable]
	public partial class IfcRevolvedAreaSolidTapered : IfcRevolvedAreaSolid
	{
		private int mEndSweptArea;//: IfcProfileDef 
		public IfcProfileDef EndSweptArea { get { return mDatabase[mEndSweptArea] as IfcProfileDef; } set { mEndSweptArea = value.mIndex; } }

		internal IfcRevolvedAreaSolidTapered() : base() { }
		internal IfcRevolvedAreaSolidTapered(DatabaseIfc db, IfcRevolvedAreaSolidTapered e) : base(db, e) { EndSweptArea = db.Factory.Duplicate(e.EndSweptArea) as IfcProfileDef; }
		public IfcRevolvedAreaSolidTapered(IfcProfileDef start, IfcAxis2Placement3D placement, IfcAxis1Placement axis, double angle, IfcProfileDef end) : base(start, placement, axis, angle) { EndSweptArea = end; }
	}
	[Obsolete("DEPRECEATED IFC4", false)]
	[Serializable]
	public partial class IfcRibPlateProfileProperties : IfcProfileProperties // DEPRECEATED IFC4
	{
		internal double mThickness, mRibHeight, mRibWidth, mRibSpacing;// : OPTIONAL IfcPositiveLengthMeasure;
		internal IfcRibPlateDirectionEnum mDirection;// : IfcRibPlateDirectionEnum;*/
		internal IfcRibPlateProfileProperties() : base() { }
		internal IfcRibPlateProfileProperties(DatabaseIfc db, IfcRibPlateProfileProperties p) : base(db, p)
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
		internal IfcRightCircularCone(DatabaseIfc db, IfcRightCircularCone c) : base(db, c) { mHeight = c.mHeight; mBottomRadius = c.mBottomRadius; }
	}
	[Serializable]
	public partial class IfcRightCircularCylinder : IfcCsgPrimitive3D
	{
		internal double mHeight, mRadius;// : IfcPositiveLengthMeasure;
		internal IfcRightCircularCylinder() : base() { }
		internal IfcRightCircularCylinder(DatabaseIfc db, IfcRightCircularCylinder c) : base(db, c) { mHeight = c.mHeight; mRadius = c.mRadius; }
	}
	[Serializable]
	public partial class IfcRoof : IfcBuildingElement
	{
		internal IfcRoofTypeEnum mPredefinedType = IfcRoofTypeEnum.NOTDEFINED;// OPTIONAL : IfcRoofTypeEnum; 
		public IfcRoofTypeEnum PredefinedType { get { return mPredefinedType; } }

		internal IfcRoof() : base() { }
		internal IfcRoof(DatabaseIfc db, IfcRoof r, IfcOwnerHistory ownerHistory, bool downStream) : base(db, r, ownerHistory, downStream) { mPredefinedType = r.mPredefinedType; }
		public IfcRoof(IfcObjectDefinition host, IfcObjectPlacement placement, IfcProductRepresentation representation) : base(host, placement, representation) { }
	}
	[Serializable]
	public partial class IfcRoofType : IfcBuildingElementType //IFC4
	{
		internal IfcRoofTypeEnum mPredefinedType = IfcRoofTypeEnum.NOTDEFINED;
		public IfcRoofTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcRoofType() : base() { }
		internal IfcRoofType(DatabaseIfc db, IfcRoofType t, IfcOwnerHistory ownerHistory, bool downStream) : base(db, t, ownerHistory, downStream) { mPredefinedType = t.mPredefinedType; }
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
					if (mDatabase != null)
					{
						BaseClassIfc obj = null;
						mDatabase.mDictionary.TryRemove(mGlobalId, out obj);
						if (!mDatabase.mDictionary.ContainsKey(value))
							mDatabase.mDictionary.TryAdd(value, this);
					}
					mGlobalId = value;
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

		protected IfcRoot() : base() { mGlobalId = ParserIfc.EncodeGuid(Guid.NewGuid()); }
		protected IfcRoot(IfcRoot basis) : base(basis) { GlobalId = basis.mGlobalId; mOwnerHistory = basis.mOwnerHistory; mName = basis.mName; mDescription = basis.mDescription; }
		protected IfcRoot(DatabaseIfc db) : base(db)
		{
			mGlobalId = ParserIfc.EncodeGuid(Guid.NewGuid());
			//m.mGlobalIDs.Add(mGlobalId);
			if (db.Release < ReleaseVersion.IFC4 || (db.mModelView != ModelView.Ifc4Reference && db.Factory.Options.GenerateOwnerHistory))
				OwnerHistory = db.Factory.OwnerHistoryAdded;
		}
		protected IfcRoot(DatabaseIfc db, IfcRoot r, IfcOwnerHistory ownerHistory) : base(db, r)
		{
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
	//[Obsolete("DEPRECEATED IFC4", false)]
	//ENTITY IfcRoundedEdgeFeature // DEPRECEATED IFC4
	[Serializable]
	public partial class IfcRoundedRectangleProfileDef : IfcRectangleProfileDef
	{
		internal double mRoundingRadius;// : IfcPositiveLengthMeasure; 
		public double RoundingRadius { get { return mRoundingRadius; } set { mRoundingRadius = value; } }
		internal IfcRoundedRectangleProfileDef() : base() { }
		internal IfcRoundedRectangleProfileDef(DatabaseIfc db, IfcRoundedRectangleProfileDef p) : base(db, p) { mRoundingRadius = p.mRoundingRadius; }
		public IfcRoundedRectangleProfileDef(DatabaseIfc db, string name, double xDim, double yDim, double roundingRadius) : base(db, name, xDim, yDim) { mRoundingRadius = roundingRadius; }
	}
}
