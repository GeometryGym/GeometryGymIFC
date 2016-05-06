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
	public class IfcCableCarrierFitting : IfcFlowFitting //IFC4
	{
		internal IfcCableCarrierFittingTypeEnum mPredefinedType = IfcCableCarrierFittingTypeEnum.NOTDEFINED;// OPTIONAL : IfcCableCarrierFittingTypeEnum;
		public IfcCableCarrierFittingTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcCableCarrierFitting() : base() { }
		internal IfcCableCarrierFitting(IfcCableCarrierFitting f) : base(f) { mPredefinedType = f.mPredefinedType; }
		internal IfcCableCarrierFitting(IfcProduct host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }

		internal static void parseFields(IfcCableCarrierFitting s, List<string> arrFields, ref int ipos)
		{
			IfcEnergyConversionDevice.parseFields(s, arrFields, ref ipos);
			string str = arrFields[ipos++];
			if (str[0] == '.')
				s.mPredefinedType = (IfcCableCarrierFittingTypeEnum)Enum.Parse(typeof(IfcCableCarrierFittingTypeEnum), str);
		}
		internal new static IfcCableCarrierFitting Parse(string strDef) { IfcCableCarrierFitting s = new IfcCableCarrierFitting(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildString()
		{
			return base.BuildString() + (mDatabase.mSchema == Schema.IFC2x3 ? "" : (mPredefinedType == IfcCableCarrierFittingTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + "."));
		}
	}
	public class IfcCableCarrierFittingType : IfcFlowFittingType
	{
		internal IfcCableCarrierFittingTypeEnum mPredefinedType = IfcCableCarrierFittingTypeEnum.NOTDEFINED;// : IfcDuctFittingTypeEnum; 
		public IfcCableCarrierFittingTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcCableCarrierFittingType() : base() { }
		internal IfcCableCarrierFittingType(IfcCableCarrierFittingType t) : base(t) { mPredefinedType = t.mPredefinedType; }
		internal static void parseFields(IfcCableCarrierFittingType t, List<string> arrFields, ref int ipos) { IfcFlowFittingType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcCableCarrierFittingTypeEnum)Enum.Parse(typeof(IfcCableCarrierFittingTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcCableCarrierFittingType Parse(string strDef) { IfcCableCarrierFittingType t = new IfcCableCarrierFittingType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildString() { return base.BuildString() + ",." + mPredefinedType.ToString() + "."; }
	}
	public partial class IfcCableCarrierSegment : IfcFlowSegment //IFC4
	{
		internal IfcCableCarrierSegmentTypeEnum mPredefinedType = IfcCableCarrierSegmentTypeEnum.NOTDEFINED;// OPTIONAL : IfcCableCarrierSegmentTypeEnum;
		public IfcCableCarrierSegmentTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcCableCarrierSegment() : base() { }
		internal IfcCableCarrierSegment(IfcCableCarrierSegment s) : base(s) { mPredefinedType = s.mPredefinedType; }
 
		internal static void parseFields(IfcCableCarrierSegment s, List<string> arrFields, ref int ipos)
		{
			IfcFlowSegment.parseFields(s, arrFields, ref ipos);
			string str = arrFields[ipos++];
			if (str[0] == '.')
				s.mPredefinedType = (IfcCableCarrierSegmentTypeEnum)Enum.Parse(typeof(IfcCableCarrierSegmentTypeEnum), str);
		}
		internal new static IfcCableCarrierSegment Parse(string strDef) { IfcCableCarrierSegment s = new IfcCableCarrierSegment(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildString() { return base.BuildString() + (mDatabase.mSchema == Schema.IFC2x3 ? "" : (mPredefinedType == IfcCableCarrierSegmentTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
	}
	public partial class IfcCableCarrierSegmentType : IfcFlowSegmentType
	{
		internal IfcCableCarrierSegmentTypeEnum mPredefinedType = IfcCableCarrierSegmentTypeEnum.NOTDEFINED;// : IfcCableCarrierSegmentTypeEnum; 
		public IfcCableCarrierSegmentTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcCableCarrierSegmentType() : base() { }
		internal IfcCableCarrierSegmentType(IfcCableCarrierSegmentType t) : base(t) { mPredefinedType = t.mPredefinedType; }
		internal IfcCableCarrierSegmentType(DatabaseIfc m, string name, IfcCableCarrierSegmentTypeEnum t) : base(m) { Name = name; mPredefinedType = t; }
		internal static void parseFields(IfcCableCarrierSegmentType t, List<string> arrFields, ref int ipos) { IfcFlowSegmentType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcCableCarrierSegmentTypeEnum)Enum.Parse(typeof(IfcCableCarrierSegmentTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcCableCarrierSegmentType Parse(string strDef) { IfcCableCarrierSegmentType t = new IfcCableCarrierSegmentType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildString() { return base.BuildString() + ",." + mPredefinedType.ToString() + "."; }
	}
	public class IfcCableFitting : IfcFlowFitting //IFC4
	{
		internal IfcCableFittingTypeEnum mPredefinedType = IfcCableFittingTypeEnum.NOTDEFINED;// OPTIONAL : IfcCableFittingTypeEnum;
		public IfcCableFittingTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcCableFitting() : base() { }
		internal IfcCableFitting(IfcCableFitting b) : base(b) { mPredefinedType = b.mPredefinedType; }
		internal IfcCableFitting(IfcProduct host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }

		internal static void parseFields(IfcCableFitting s, List<string> arrFields, ref int ipos)
		{
			IfcEnergyConversionDevice.parseFields(s, arrFields, ref ipos);
			string str = arrFields[ipos++];
			if (str[0] == '.')
				s.mPredefinedType = (IfcCableFittingTypeEnum)Enum.Parse(typeof(IfcCableFittingTypeEnum), str);
		}
		internal new static IfcCableFitting Parse(string strDef) { IfcCableFitting s = new IfcCableFitting(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildString()
		{
			return base.BuildString() + (mDatabase.mSchema == Schema.IFC2x3 ? "" : (mPredefinedType == IfcCableFittingTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + "."));
		}
	}
	public class IfcCableFittingType : IfcFlowFittingType
	{
		internal IfcCableFittingTypeEnum mPredefinedType = IfcCableFittingTypeEnum.NOTDEFINED;// : IfcDuctFittingTypeEnum; 
		public IfcCableFittingTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcCableFittingType() : base() { }
		internal IfcCableFittingType(IfcCableFittingType t) : base(t) { mPredefinedType = t.mPredefinedType; }
		internal static void parseFields(IfcCableFittingType t, List<string> arrFields, ref int ipos) { IfcFlowFittingType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcCableFittingTypeEnum)Enum.Parse(typeof(IfcCableFittingTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcCableFittingType Parse(string strDef) { IfcCableFittingType t = new IfcCableFittingType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildString() { return base.BuildString() + ",." + mPredefinedType.ToString() + "."; }
	}
	public partial class IfcCableSegment : IfcFlowSegment //IFC4
	{
		internal IfcCableSegmentTypeEnum mPredefinedType = IfcCableSegmentTypeEnum.NOTDEFINED;// OPTIONAL : IfcCableSegmentTypeEnum;
		public IfcCableSegmentTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		
		internal IfcCableSegment() : base() { }
		internal IfcCableSegment(IfcCableSegment s) : base(s) { mPredefinedType = s.mPredefinedType; }
 
		internal static void parseFields(IfcCableSegment s, List<string> arrFields, ref int ipos)
		{
			IfcFlowSegment.parseFields(s, arrFields, ref ipos);
			string str = arrFields[ipos++];
			if (str[0] == '.')
				s.mPredefinedType = (IfcCableSegmentTypeEnum)Enum.Parse(typeof(IfcCableSegmentTypeEnum), str);
		}
		internal new static IfcCableSegment Parse(string strDef) { IfcCableSegment s = new IfcCableSegment(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildString() { return base.BuildString() + (mDatabase.mSchema == Schema.IFC2x3 ? "" : ",." + mPredefinedType.ToString() + "."); }
	}
	public class IfcCableSegmentType : IfcFlowSegmentType
	{
		internal IfcCableSegmentTypeEnum mPredefinedType = IfcCableSegmentTypeEnum.NOTDEFINED;// : IfcCableSegmentTypeEnum; 
		public IfcCableSegmentTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcCableSegmentType() : base() { }
		internal IfcCableSegmentType(IfcCableSegmentType t) : base(t) { mPredefinedType = t.mPredefinedType; }
		internal IfcCableSegmentType(DatabaseIfc m, string name, IfcCableSegmentTypeEnum t) : base(m) { Name = name; mPredefinedType = t; }
		internal IfcCableSegmentType(DatabaseIfc m, string name, IfcMaterialProfileSet mps, IfcCableSegmentTypeEnum t) : base(m) { Name = name; MaterialSelect = mps; PredefinedType = t; }
		internal static void parseFields(IfcCableSegmentType t, List<string> arrFields, ref int ipos) { IfcFlowSegmentType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcCableSegmentTypeEnum)Enum.Parse(typeof(IfcCableCarrierSegmentTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcCableSegmentType Parse(string strDef) { IfcCableSegmentType t = new IfcCableSegmentType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildString() { return base.BuildString() + ",." + mPredefinedType.ToString() + "."; }
	}
	public class IfcCalendarDate : BaseClassIfc, IfcDateTimeSelect //DEPRECEATED IFC4
	{
		internal int mDayComponent;//  : IfcDayInMonthNumber;
		internal int mMonthComponent;//  : IfcMonthInYearNumber;
		internal int mYearComponent;// : IfcYearNumber; 

		internal IfcCalendarDate() : base() { }
		internal IfcCalendarDate(IfcCalendarDate i) : base() { mDayComponent = i.mDayComponent; mMonthComponent = i.mMonthComponent; mYearComponent = i.mYearComponent; }
		internal IfcCalendarDate(DatabaseIfc m, int day, int month, int year) : base(m)
		{
			if (m.mSchema != Schema.IFC2x3) throw new Exception("IfcCalanderDate DEPRECEATED, use IfcDate");
			mDayComponent = day;
			mMonthComponent = month;
			mYearComponent = year;
		}
		internal static IfcCalendarDate Parse(string strDef) { IfcCalendarDate d = new IfcCalendarDate(); int ipos = 0; parseFields(d, ParserSTEP.SplitLineFields(strDef), ref ipos); return d; }
		internal static void parseFields(IfcCalendarDate c, List<string> arrFields, ref int ipos)
		{
			c.mDayComponent = ParserSTEP.ParseInt(arrFields[ipos++]);
			c.mMonthComponent = ParserSTEP.ParseInt(arrFields[ipos++]);
			c.mYearComponent = ParserSTEP.ParseInt(arrFields[ipos++]);
		}
		protected override string BuildString() { return base.BuildString() + "," + mDayComponent + "," + mMonthComponent + "," + mYearComponent; }
		public DateTime DateTime { get { return new DateTime(mYearComponent, mMonthComponent, mDayComponent); } }
	}
	public partial class IfcCartesianPoint : IfcPoint
	{
		private double mCoordinateX = 0, mCoordinateY = 0, mCoordinateZ = 0;

		public Tuple<double,double,double> Coordinates
		{
			get { return new Tuple<double, double, double>(mCoordinateX, mCoordinateY, double.IsNaN(mCoordinateZ) ? 0 : mCoordinateZ); }
			set { mCoordinateX = value.Item1; mCoordinateY = value.Item2; mCoordinateZ = value.Item3; }
		}
		internal IfcCartesianPoint() : base() { }
		internal IfcCartesianPoint(IfcCartesianPoint p) : base()
		{
			mCoordinateX = p.mCoordinateX;
			mCoordinateY = p.mCoordinateY;
			mCoordinateZ = p.mCoordinateZ;
		}
		public IfcCartesianPoint(DatabaseIfc m, double x, double y) : base(m) { mCoordinateX = x; mCoordinateY = y; mCoordinateZ = double.NaN; }
		public IfcCartesianPoint(DatabaseIfc m, double x, double y, double z) : base(m) { mCoordinateX = x; mCoordinateY = y; mCoordinateZ = z; }
		internal static void parseFields(IfcCartesianPoint p, List<string> arrFields, ref int ipos)
		{
			IfcPoint.parseFields(p, arrFields, ref ipos);
			string s = arrFields[ipos++];
			if (s != "$")
			{
				List<string> arrCoords = ParserSTEP.SplitLineFields(s.Substring(1, s.Length - 2));
				int count = arrCoords.Count;
				if (count > 0)
				{
					p.mCoordinateX = ParserSTEP.ParseDouble(arrCoords[0]);
					if (count > 1)
					{
						p.mCoordinateY = ParserSTEP.ParseDouble(arrCoords[1]);
						p.mCoordinateZ = (count > 2 ? ParserSTEP.ParseDouble(arrCoords[2]) : double.NaN);
					}
				}
			}
		}
		internal static IfcCartesianPoint Parse(string str)
		{
			IfcCartesianPoint p = new IfcCartesianPoint();
			if (str[0] == '(')
			{
				int pos = 1;
				string val = ParserSTEP.StripField(str, ref pos);

				if (!string.IsNullOrEmpty(val))
					p.mCoordinateX = ParserSTEP.ParseDouble(val);
				val = ParserSTEP.StripField(str, ref pos);
				if (!string.IsNullOrEmpty(val))
					p.mCoordinateY = ParserSTEP.ParseDouble(val);
				if (pos >= str.Length)
					p.mCoordinateZ = double.NaN;
				else
				{
					val = str.Substring(pos, str.Length - pos - 1);// ParserSTEP.StripField(str.Substring(ref pos);
					p.mCoordinateZ = (string.IsNullOrEmpty(val) ? double.NaN : ParserSTEP.ParseDouble(val));
				}
			}
			return p;
		}
		protected override string BuildString()
		{
			if (mDatabase.mOutputEssential)
				return "";
			double tol = (mDatabase.mGeomRepContxt != null ? mDatabase.mGeomRepContxt.mPrecision / 100 : 1e-6);
			double x = mCoordinateX, y = mCoordinateY;
			if (Math.Abs(x) < tol)
				x = 0;
			if (Math.Abs(y) < tol)
				y = 0;
			if (Is2D())
				return base.BuildString() + ",(" + ParserSTEP.DoubleToString(x) + "," + ParserSTEP.DoubleToString(y) + ")";
			double z = mCoordinateZ;
			if (Math.Abs(z) < tol)
				z = 0;
			return base.BuildString() + ",(" + ParserSTEP.DoubleToString(x) + "," +
					ParserSTEP.DoubleToString(y) + "," + ParserSTEP.DoubleToString(z) + ")";
		}
		internal bool Is2D() { return double.IsNaN(mCoordinateZ); }
	}
	public abstract partial class IfcCartesianPointList : IfcGeometricRepresentationItem //IFC4
	{
		protected IfcCartesianPointList() : base() { }
		protected IfcCartesianPointList(IfcCartesianPointList o) : base(o) { }
		protected IfcCartesianPointList(DatabaseIfc m) : base(m) { }
	}
	public partial class IfcCartesianPointList2D : IfcCartesianPointList //IFC4
	{ 
		private Tuple<double, double>[] mCoordList = new Tuple<double, double>[0];//	 :	LIST [1:?] OF LIST [2:2] OF IfcLengthMeasure; 

		internal IfcCartesianPointList2D() : base() { }
		internal IfcCartesianPointList2D(IfcCartesianPointList2D o) : base(o) { mCoordList = o.mCoordList; }
		public IfcCartesianPointList2D(DatabaseIfc m, IEnumerable<Tuple<double, double>> coordList) : base(m)
		{
			List<Tuple<double, double>> pts = new List<Tuple<double, double>>();
			foreach (Tuple<double, double> t in coordList)
				pts.Add(new Tuple<double, double>(t.Item1, t.Item2));
			mCoordList = pts.ToArray();
		}
		internal static IfcCartesianPointList2D Parse(string strDef)
		{
			IfcCartesianPointList2D l = new IfcCartesianPointList2D();
			l.mCoordList = ParserSTEP.SplitListDoubleTuple(strDef);
			return l;
		}
		protected override string BuildString()
		{
			StringBuilder sb = new StringBuilder();
			Tuple<double, double> p = mCoordList[0];
			sb.Append(",((" + ParserSTEP.DoubleToString(p.Item1) + "," + ParserSTEP.DoubleToString(p.Item2) + ")");
			for (int icounter = 1; icounter < mCoordList.Length; icounter++)
			{
				p = mCoordList[icounter];
				sb.Append(",(" + ParserSTEP.DoubleToString(p.Item1) + "," + ParserSTEP.DoubleToString(p.Item2) + ")");
			}
			return base.BuildString() + sb.ToString() + ")";
		}
	}
	public partial class IfcCartesianPointList3D : IfcCartesianPointList //IFC4
	{
		private Tuple<double, double, double>[] mCoordList = new Tuple<double, double, double>[0];//	 :	LIST [1:?] OF LIST [3:3] OF IfcLengthMeasure; 

		internal IfcCartesianPointList3D() : base() { }
		internal IfcCartesianPointList3D(IfcCartesianPointList3D o) : base(o) { mCoordList = o.mCoordList; }
		public IfcCartesianPointList3D(DatabaseIfc m, IEnumerable<Tuple<double, double, double>> coordList) : base(m)
		{
			List<Tuple<double, double, double>> pts = new List<Tuple<double, double, double>>();
			foreach (Tuple<double, double, double> t in coordList)
				pts.Add(new Tuple<double, double, double>(t.Item1, t.Item2, t.Item3));
			mCoordList = pts.ToArray();
		}
		internal static IfcCartesianPointList3D Parse(string strDef)
		{
			IfcCartesianPointList3D l = new IfcCartesianPointList3D();
			l.mCoordList = ParserSTEP.SplitListDoubleTriple(strDef);
			return l;
		}
		protected override string BuildString()
		{
			StringBuilder sb = new StringBuilder();
			Tuple<double, double, double> p = mCoordList[0];
			sb.Append(",((" + ParserSTEP.DoubleToString(p.Item1) + "," + ParserSTEP.DoubleToString(p.Item2) + "," + ParserSTEP.DoubleToString(p.Item3) + ")");
			for (int icounter = 1; icounter < mCoordList.Length; icounter++)
			{
				p = mCoordList[icounter];
				sb.Append(",(" + ParserSTEP.DoubleToString(p.Item1) + "," + ParserSTEP.DoubleToString(p.Item2) + "," + ParserSTEP.DoubleToString(p.Item3) + ")");
			}
			return base.BuildString() + sb.ToString() + ")";
		}
	}
	public abstract partial class IfcCartesianTransformationOperator : IfcGeometricRepresentationItem /*ABSTRACT SUPERTYPE OF (ONEOF (IfcCartesianTransformationOperator2D ,IfcCartesianTransformationOperator3D))*/
	{  //http://www.buildingsmart-tech.org/ifc/ifc2x3/tc1/html/ifcprofileresource/lexical/ifcderivedprofiledef.htm for examples
		private int mAxis1;// : OPTIONAL IfcDirection
		private int mAxis2;// : OPTIONAL IfcDirection;
		private int mLocalOrigin;// : IfcCartesianPoint;
		private double mScale = 1;// : OPTIONAL REAL;

		internal IfcDirection Axis1 { get { return mDatabase.mIfcObjects[mAxis1] as IfcDirection; } set { mAxis1 = (value == null ? 0 : value.mIndex); } }
		internal IfcDirection Axis2 { get { return mDatabase.mIfcObjects[mAxis2] as IfcDirection; } set { mAxis2 = (value == null ? 0 : value.mIndex); } }
		internal IfcCartesianPoint LocalOrigin { get { return mDatabase.mIfcObjects[mLocalOrigin] as IfcCartesianPoint; } set { mLocalOrigin = value.mIndex; } }
		internal double Scale { get { return mScale; } set { mScale = value; } }

		protected IfcCartesianTransformationOperator() { }
		protected IfcCartesianTransformationOperator(IfcCartesianTransformationOperator o) : base(o) { mAxis1 = o.mAxis1; mAxis2 = o.mAxis2; mLocalOrigin = o.mLocalOrigin; mScale = o.mScale; }
		protected IfcCartesianTransformationOperator(DatabaseIfc db) : base(db) { mLocalOrigin = db.WorldOrigin.mIndex; }
		protected IfcCartesianTransformationOperator(DatabaseIfc db, IfcDirection ax1, IfcDirection ax2, IfcCartesianPoint o, double scale)
			: base(db) { if (ax1 != null) mAxis1 = ax1.mIndex; if (ax2 != null) mAxis2 = ax2.mIndex; mLocalOrigin = o.mIndex; mScale = scale; }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.LinkToString(mAxis1) + "," + ParserSTEP.LinkToString(mAxis2) + "," + ParserSTEP.LinkToString(mLocalOrigin) + "," + ParserSTEP.DoubleOptionalToString(mScale); }
		internal static void parseFields(IfcCartesianTransformationOperator o, List<string> arrFields, ref int ipos)
		{
			IfcGeometricRepresentationItem.parseFields(o, arrFields, ref ipos);
			o.mAxis1 = ParserSTEP.ParseLink(arrFields[ipos++]);
			o.mAxis2 = ParserSTEP.ParseLink(arrFields[ipos++]);
			o.mLocalOrigin = ParserSTEP.ParseLink(arrFields[ipos++]);
			o.mScale = ParserSTEP.ParseDouble(arrFields[ipos++]);
			if (o.mScale == 0)
				o.mScale = 1;
		}
	}
	public partial class IfcCartesianTransformationOperator2D : IfcCartesianTransformationOperator
	{
		internal IfcCartesianTransformationOperator2D() : base() { }
		internal IfcCartesianTransformationOperator2D(IfcCartesianTransformationOperator2D i) : base(i) { }
		internal static IfcCartesianTransformationOperator2D Parse(string strDef) { IfcCartesianTransformationOperator2D o = new IfcCartesianTransformationOperator2D(); int ipos = 0; parseFields(o, ParserSTEP.SplitLineFields(strDef), ref ipos); return o; }
		internal static void parseFields(IfcCartesianTransformationOperator2D o, List<string> arrFields, ref int ipos) { IfcCartesianTransformationOperator.parseFields(o, arrFields, ref ipos); }
	}
	public partial class IfcCartesianTransformationOperator2DnonUniform : IfcCartesianTransformationOperator2D
	{
		private double mScale2; //OPTIONAL REAL;
		internal double Scale2 { get { return mScale2; } }

		internal IfcCartesianTransformationOperator2DnonUniform() : base() { }
		internal IfcCartesianTransformationOperator2DnonUniform(IfcCartesianTransformationOperator2DnonUniform i) : base(i) { mScale2 = i.mScale2; }
		internal new static IfcCartesianTransformationOperator2DnonUniform Parse(string strDef) { IfcCartesianTransformationOperator2DnonUniform o = new IfcCartesianTransformationOperator2DnonUniform(); int ipos = 0; parseFields(o, ParserSTEP.SplitLineFields(strDef), ref ipos); return o; }
		internal static void parseFields(IfcCartesianTransformationOperator2DnonUniform o, List<string> arrFields, ref int ipos) { IfcCartesianTransformationOperator2D.parseFields(o, arrFields, ref ipos); o.mScale2 = ParserSTEP.ParseDouble(arrFields[ipos++]); }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.DoubleOptionalToString(mScale2); }
	}
	public partial class IfcCartesianTransformationOperator3D : IfcCartesianTransformationOperator
	{
		private int mAxis3;// : OPTIONAL IfcDirection
		internal IfcDirection Axis3 { get { return mDatabase.mIfcObjects[mAxis3] as IfcDirection; } set { mAxis3 = (value == null ? 0 : value.mIndex); } }

		internal IfcCartesianTransformationOperator3D() { }
		internal IfcCartesianTransformationOperator3D(IfcCartesianTransformationOperator3D i) : base(i) { mAxis3 = i.mAxis3; }
		public IfcCartesianTransformationOperator3D(DatabaseIfc db) : base(db) { }
		internal static void parseFields(IfcCartesianTransformationOperator3D o, List<string> arrFields, ref int ipos) { IfcCartesianTransformationOperator.parseFields(o, arrFields, ref ipos); o.mAxis3 = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.LinkToString(mAxis3); }
		internal static IfcCartesianTransformationOperator3D Parse(string strDef) { IfcCartesianTransformationOperator3D o = new IfcCartesianTransformationOperator3D(); int ipos = 0; parseFields(o, ParserSTEP.SplitLineFields(strDef), ref ipos); return o; }

	}
	public partial class IfcCartesianTransformationOperator3DnonUniform : IfcCartesianTransformationOperator3D
	{
		private double mScale2 = 1;// : OPTIONAL REAL;
		private double mScale3 = 1;// : OPTIONAL REAL; 

		internal double Scale2 { get { return mScale2; } }
		internal double Scale3 { get { return mScale3; } }

		internal IfcCartesianTransformationOperator3DnonUniform() { }
		internal IfcCartesianTransformationOperator3DnonUniform(IfcCartesianTransformationOperator3DnonUniform i) : base(i) { mScale2 = i.mScale2; mScale3 = i.mScale3; }
		internal new static IfcCartesianTransformationOperator3DnonUniform Parse(string strDef) { IfcCartesianTransformationOperator3DnonUniform o = new IfcCartesianTransformationOperator3DnonUniform(); int ipos = 0; parseFields(o, ParserSTEP.SplitLineFields(strDef), ref ipos); return o; }
		internal static void parseFields(IfcCartesianTransformationOperator3DnonUniform o, List<string> arrFields, ref int ipos) { IfcCartesianTransformationOperator3D.parseFields(o, arrFields, ref ipos); o.mScale2 = ParserSTEP.ParseDouble(arrFields[ipos++]); o.mScale3 = ParserSTEP.ParseDouble(arrFields[ipos++]); }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.DoubleOptionalToString(mScale2) + "," + ParserSTEP.DoubleOptionalToString(mScale3); }

	}
	public class IfcCenterLineProfileDef : IfcArbitraryOpenProfileDef
	{
		internal double mThickness;// : IfcPositiveLengthMeasure;
		internal IfcCenterLineProfileDef() : base() { }
		internal IfcCenterLineProfileDef(IfcCenterLineProfileDef i) : base(i) { mThickness = i.mThickness; }
		internal new static IfcCenterLineProfileDef Parse(string strDef) { IfcCenterLineProfileDef p = new IfcCenterLineProfileDef(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos); return p; }
		internal static void parseFields(IfcCenterLineProfileDef p, List<string> arrFields, ref int ipos) { IfcArbitraryOpenProfileDef.parseFields(p, arrFields, ref ipos); p.mThickness = ParserSTEP.ParseDouble(arrFields[ipos++]); }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.DoubleToString(mThickness); }
	}
	public class IfcChamferEdgeFeature : IfcEdgeFeature //DEPRECEATED IFC4
	{
		internal double mWidth;// : OPTIONAL IfcPositiveLengthMeasure
		internal double mHeight;// : OPTIONAL IfcPositiveLengthMeasure;
		internal IfcChamferEdgeFeature() : base() { }
		internal IfcChamferEdgeFeature(IfcChamferEdgeFeature p) : base(p) { mWidth = p.mWidth; mHeight = p.mHeight; }
		internal static void parseFields(IfcChamferEdgeFeature f, List<string> arrFields, ref int ipos) { IfcEdgeFeature.parseFields(f, arrFields, ref ipos); f.mWidth = ParserSTEP.ParseDouble(arrFields[ipos++]); f.mHeight = ParserSTEP.ParseDouble(arrFields[ipos++]); }
		internal static IfcChamferEdgeFeature Parse(string strDef) { IfcChamferEdgeFeature f = new IfcChamferEdgeFeature(); int ipos = 0; parseFields(f, ParserSTEP.SplitLineFields(strDef), ref ipos); return f; }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.DoubleOptionalToString(mWidth) + "," + ParserSTEP.DoubleOptionalToString(mHeight); }
	}
	public class IfcChiller : IfcEnergyConversionDevice //IFC4
	{
		internal IfcChillerTypeEnum mPredefinedType = IfcChillerTypeEnum.NOTDEFINED;// OPTIONAL : IfcBurnerTypeEnum;
		public IfcChillerTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcChiller() : base() { }
		internal IfcChiller(IfcChiller c) : base(c) { mPredefinedType = c.mPredefinedType; }
		public IfcChiller(IfcProduct host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }

		internal static void parseFields(IfcChiller s, List<string> arrFields, ref int ipos)
		{
			IfcEnergyConversionDevice.parseFields(s, arrFields, ref ipos);
			string str = arrFields[ipos++];
			if (str[0] == '.')
				s.mPredefinedType = (IfcChillerTypeEnum)Enum.Parse(typeof(IfcChillerTypeEnum), str);
		}
		internal new static IfcBurner Parse(string strDef) { IfcBurner s = new IfcBurner(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildString()
		{
			return base.BuildString() + (mDatabase.mSchema == Schema.IFC2x3 ? "" : (mPredefinedType == IfcChillerTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + "."));
		}
	}
	public class IfcChillerType : IfcEnergyConversionDeviceType
	{
		internal IfcChillerTypeEnum mPredefinedType = IfcChillerTypeEnum.NOTDEFINED;// : IfcChillerTypeEnum
		public IfcChillerTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcChillerType() : base() { }
		internal IfcChillerType(IfcChillerType t) : base(t) { mPredefinedType = t.mPredefinedType; }
		public IfcChillerType(DatabaseIfc db, string name, IfcChillerTypeEnum type) : base(db) { Name = name; mPredefinedType = type; }

		internal static void parseFields(IfcChillerType t, List<string> arrFields, ref int ipos) { IfcEnergyConversionDeviceType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcChillerTypeEnum)Enum.Parse(typeof(IfcChillerTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcChillerType Parse(string strDef) { IfcChillerType t = new IfcChillerType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildString() { return base.BuildString() + ",." + mPredefinedType.ToString() + "."; }
	}
	public class IfcChimney : IfcBuildingElement
	{
		internal IfcChimneyTypeEnum mPredefinedType = IfcChimneyTypeEnum.NOTDEFINED;//: OPTIONAL IfcChimneyTypeEnum; 
		public IfcChimneyTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcChimney() : base() { }
		internal IfcChimney(IfcChimney c) : base(c) { mPredefinedType = c.mPredefinedType; }
		public IfcChimney(IfcProduct host, IfcObjectPlacement p, IfcProductRepresentation r) : base(host, p, r) { }
	
		internal static IfcChimney Parse(string strDef, Schema schema) { IfcChimney w = new IfcChimney(); int ipos = 0; parseFields(w, ParserSTEP.SplitLineFields(strDef), ref ipos, schema); return w; }
		internal static void parseFields(IfcChimney w, List<string> arrFields, ref int ipos, Schema schema)
		{
			IfcBuildingElement.parseFields(w, arrFields, ref ipos);
			if (schema != Schema.IFC2x3)
			{
				string str = arrFields[ipos++];
				if (str[0] == '.')
					w.mPredefinedType = (IfcChimneyTypeEnum)Enum.Parse(typeof(IfcChimneyTypeEnum), str.Substring(1, str.Length - 2));
			}
		}
		protected override string BuildString() { return base.BuildString() + (mDatabase.mSchema == Schema.IFC2x3 ? "" : (mPredefinedType == IfcChimneyTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
	}
	public partial class IfcChimneyType : IfcBuildingElementType
	{
		internal IfcChimneyTypeEnum mPredefinedType = IfcChimneyTypeEnum.NOTDEFINED;
		public IfcChimneyTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcChimneyType() : base() { } 
		internal IfcChimneyType(IfcChimneyType t) : base(t) { mPredefinedType = t.mPredefinedType; }
		internal IfcChimneyType(DatabaseIfc m, string name, IfcChimneyTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
		internal static void parseFields(IfcChimneyType t, List<string> arrFields, ref int ipos) { IfcBuildingElementType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcChimneyTypeEnum)Enum.Parse(typeof(IfcChimneyTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcChimneyType Parse(string strDef) { IfcChimneyType t = new IfcChimneyType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildString() { return base.BuildString() + ",." + mPredefinedType.ToString() + "."; }
	}
	public partial class IfcCircle : IfcConic
	{
		private double mRadius;// : IfcPositiveLengthMeasure;
		internal double Radius { get { return mRadius; } }

		internal IfcCircle() : base() { }
		internal IfcCircle(IfcCircle el) : base(el) { mRadius = el.mRadius; }
		internal IfcCircle(DatabaseIfc m, double radius) : base(m.m2DPlaceOrigin) { mRadius = radius; }
		internal IfcCircle(IfcAxis2Placement ap, double radius) : base(ap) { mRadius = radius; }
		internal static IfcCircle Parse(string strDef) { IfcCircle c = new IfcCircle(); int ipos = 0; parseFields(c, ParserSTEP.SplitLineFields(strDef), ref ipos); return c; }
		internal static void parseFields(IfcCircle c, List<string> arrFields, ref int ipos) { IfcConic.parseFields(c, arrFields, ref ipos); c.mRadius = ParserSTEP.ParseDouble(arrFields[ipos++]); }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.DoubleToString(mRadius); }
	}
	public partial class IfcCircleHollowProfileDef : IfcCircleProfileDef
	{
		public override string KeyWord { get { return (mWallThickness < mDatabase.Tolerance ? "IFCCIRCLEPROFILEDEF" : base.KeyWord); } }
		internal double mWallThickness;// : IfcPositiveLengthMeasure;
		internal IfcCircleHollowProfileDef() : base() { }
		internal IfcCircleHollowProfileDef(IfcCircleHollowProfileDef c) : base(c) { mWallThickness = c.mWallThickness; }
		public IfcCircleHollowProfileDef(DatabaseIfc m, string name, double radius, double wallThickness) : base(m, name, radius) { mWallThickness = wallThickness; }
		internal static void parseFields(IfcCircleHollowProfileDef p, List<string> arrFields, ref int ipos) { IfcCircleProfileDef.parseFields(p, arrFields, ref ipos); p.mWallThickness = ParserSTEP.ParseDouble(arrFields[ipos++]); }
		internal new static IfcCircleHollowProfileDef Parse(string strDef) { IfcCircleHollowProfileDef p = new IfcCircleHollowProfileDef(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos); return p; }
		protected override string BuildString() { return base.BuildString() + (mWallThickness > mDatabase.Tolerance ? "," + ParserSTEP.DoubleToString(mWallThickness) : ""); }
	}
	public partial class IfcCircleProfileDef : IfcParameterizedProfileDef //SUPERTYPE OF(IfcCircleHollowProfileDef)
	{
		internal double mRadius;// : IfcPositiveLengthMeasure;		
		internal IfcCircleProfileDef() : base() { }
		internal IfcCircleProfileDef(IfcCircleProfileDef c) : base(c) { mRadius = c.mRadius; }
		public IfcCircleProfileDef(DatabaseIfc db, string name, double radius) : base(db) { Name = name; mRadius = radius; }//if (string.Compare(getKW, mKW) == 0) mModel.mCircProfiles.Add(this); }
		internal static void parseFields(IfcCircleProfileDef p, List<string> arrFields, ref int ipos) { IfcParameterizedProfileDef.parseFields(p, arrFields, ref ipos); p.mRadius = ParserSTEP.ParseDouble(arrFields[ipos++]); }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.DoubleToString(mRadius); }
		internal new static IfcCircleProfileDef Parse(string strDef) { IfcCircleProfileDef p = new IfcCircleProfileDef(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos); return p; }
	}
	public partial class IfcCircularArcSegment2D : IfcCurveSegment2D  //IFC4x1
	{
		private double mRadius;// : IfcPositiveLengthMeasure;
		private bool mIsCCW;// : IfcBoolean;

		internal IfcCircularArcSegment2D() : base() { }
		internal IfcCircularArcSegment2D(IfcCircularArcSegment2D p) : base(p) { mRadius = p.mRadius; mIsCCW = p.mIsCCW; }
		internal IfcCircularArcSegment2D(IfcCartesianPoint start, double startDirection, double length, double radius, bool isCCW)
			: base(start, startDirection, length)
		{
			mRadius = radius;
			mIsCCW = isCCW;
		}
		internal static void parseFields(IfcCircularArcSegment2D c, List<string> arrFields, ref int ipos)
		{
			IfcCurveSegment2D.parseFields(c, arrFields, ref ipos);
			c.mRadius = ParserSTEP.ParseDouble(arrFields[ipos++]);
			c.mIsCCW = ParserSTEP.ParseBool(arrFields[ipos++]);
		}
		internal static IfcCircularArcSegment2D Parse(string strDef) { IfcCircularArcSegment2D c = new IfcCircularArcSegment2D(); int ipos = 0; parseFields(c, ParserSTEP.SplitLineFields(strDef), ref ipos); return c; }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.DoubleToString(mRadius) + "," + ParserSTEP.BoolToString(mIsCCW); }
	}
	public class IfcCivilElement : IfcElement  //IFC4
	{
		internal IfcCivilElement() : base() { }
		internal IfcCivilElement(IfcCivilElement e) : base(e) { }
		public IfcCivilElement(IfcProduct host, IfcObjectPlacement p, IfcProductRepresentation r) : base(host, p, r) { if (mDatabase.mSchema == Schema.IFC2x3) throw new Exception(KeyWord + " only supported in IFC4!"); }
		internal static IfcCivilElement Parse(string strDef) { IfcCivilElement e = new IfcCivilElement(); int ipos = 0; parseFields(e, ParserSTEP.SplitLineFields(strDef), ref ipos); return e; }
		internal static void parseFields(IfcCivilElement e, List<string> arrFields, ref int ipos) { IfcElement.parseFields(e, arrFields, ref ipos); }
	}
	public abstract class IfcCivilElementPart : IfcElementComponent //	ABSTRACT SUPERTYPE OF(ONEOF(IfcBridgeSegmentPart , IfcBridgeContactElement , IfcCivilSheath , IfcCivilVoid))
	{
		// INVERSE
		//ContainedInSegment : IfcBridgeSegment FOR SegmentParts;
		protected IfcCivilElementPart() : base() { }
		protected IfcCivilElementPart(IfcCivilElementPart e) : base(e) { }
		internal IfcCivilElementPart(IfcProduct host, IfcObjectPlacement placement, IfcProductRepresentation representation) : base(host, placement, representation) { }
		protected static void parseFields(IfcCivilElementPart e, List<string> arrFields, ref int ipos)
		{
			IfcElementComponent.parseFields(e, arrFields, ref ipos);
		}
	}
	public partial class IfcCivilElementType : IfcElementType //IFC4
	{
		internal IfcCivilElementType() : base() { }
		internal IfcCivilElementType(IfcCivilElementType t) : base(t) { }
		internal IfcCivilElementType(DatabaseIfc m, string name) : base(m) { Name = name; if (m.mSchema == Schema.IFC2x3) throw new Exception(KeyWord + " only supported in IFC4!"); }
		internal new static IfcCivilElementType Parse(string strDef) { IfcCivilElementType t = new IfcCivilElementType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		internal static void parseFields(IfcCivilElementType t, List<string> arrFields, ref int ipos) { IfcElementType.parseFields(t, arrFields, ref ipos); }
	}
	public partial class IfcCivilSheath : IfcCivilElementPart //IFC5
	{
		internal IfcCivilSheath() : base() { }
		internal IfcCivilSheath(IfcCivilSheath b) : base(b) { }
		internal IfcCivilSheath(IfcProduct host, IfcObjectPlacement placement, IfcProductRepresentation representation) : base(host, placement, representation) { }
		internal static IfcCivilSheath Parse(string strDef) { IfcCivilSheath p = new IfcCivilSheath(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos); return p; }
		internal static void parseFields(IfcCivilSheath a, List<string> arrFields, ref int ipos)
		{
			IfcCivilElementPart.parseFields(a, arrFields, ref ipos);
		}
	}
	public abstract partial class IfcCivilStructureElement : IfcSpatialStructureElement //IFC5
	{
		protected IfcCivilStructureElement() : base() { }
		protected IfcCivilStructureElement(IfcCivilStructureElement p) : base(p) { }
		protected IfcCivilStructureElement(IfcSpatialStructureElement host, string name) : base(host, name) { }
	}
	public partial class IfcCivilVoid : IfcCivilElementPart //IFC5
	{
		internal IfcCivilVoid() : base() { }
		internal IfcCivilVoid(IfcCivilVoid b) : base(b) { }
		internal IfcCivilVoid(IfcProduct host, IfcObjectPlacement placement, IfcProductRepresentation representation) : base(host, placement, representation) { }
		internal static IfcCivilVoid Parse(string strDef) { IfcCivilVoid p = new IfcCivilVoid(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos); return p; }
		internal static void parseFields(IfcCivilVoid a, List<string> arrFields, ref int ipos)
		{
			IfcCivilElementPart.parseFields(a, arrFields, ref ipos);
		}
	}
	public partial class IfcClassification : IfcExternalInformation, IfcClassificationSelect //	SUBTYPE OF IfcExternalInformation;
	{
		internal string mSource = "$";//  : OPTIONAL IfcLabel;
		internal string mEdition = "$";//  : OPTIONAL IfcLabel;
		internal string mEditionDate = "$";// OPTIONAL IfcDate IFC4 change : OPTIONAL IfcCalendarDate;
		internal string mName;//  : IfcLabel;
		internal string mDescription = "$";//	 :	OPTIONAL IfcText; IFC4 Addition
		internal string mLocation = "$";//	 :	OPTIONAL IfcURIReference; IFC4 Addtion
		internal List<string> mReferenceTokens = new List<string>();//	 :	OPTIONAL LIST [1:?] OF IfcIdentifier; IFC4 Addition
		//INVERSE 
		internal List<IfcRelAssociatesClassification> mClassificationForObjects = new List<IfcRelAssociatesClassification>();//	 :	SET OF IfcRelAssociatesclassification FOR Relatingclassification;
		internal List<IfcClassificationReference> mHasReferences = new List<IfcClassificationReference>();//	 :	SET OF IfcClassificationReference FOR ReferencedSource;

		public override string Name { get { return (mName == "$" ? "" : ParserIfc.Decode(mName)); } set { if (!string.IsNullOrEmpty(value)) mName = ParserIfc.Encode(value.Replace("'", "")); } }
		public List<IfcRelAssociatesClassification> ClassificationForObjects { get { return mClassificationForObjects; } }

		internal IfcClassification() : base() { }
		internal IfcClassification(IfcClassification c) : base(c) { mSource = c.mSource; mEdition = c.mEdition; mEditionDate = c.mEditionDate; mName = c.mName; }
		internal IfcClassification(DatabaseIfc m, string source, string edition, DateTime editionDate, string name, string desc, string location, List<string> references)
			: base(m)
		{
			if (!string.IsNullOrEmpty(source))
				mSource = source.Replace("'", "");
			if (!string.IsNullOrEmpty(edition))
				mEdition = edition.Replace("'", "");
			if (editionDate != DateTime.MinValue)
				mEditionDate = IfcDate.convert(editionDate);
			Name = name;
			if (!string.IsNullOrEmpty(desc))
				mDescription = desc.Replace("'", "");
			if (!string.IsNullOrEmpty(location))
				mLocation = location.Replace("'", "");
			if (references != null)
				mReferenceTokens = references.ConvertAll(x => x.Replace("'", ""));
			mClassificationForObjects.Add(new IfcRelAssociatesClassification(this));
		}
		internal static IfcClassification Parse(string strDef, Schema schema) { IfcClassification c = new IfcClassification(); int ipos = 0; parseFields(c, ParserSTEP.SplitLineFields(strDef), ref ipos, schema); return c; }
		internal static void parseFields(IfcClassification c, List<string> arrFields, ref int ipos, Schema schema)
		{
			IfcExternalInformation.parseFields(c, arrFields, ref ipos);
			c.mSource = arrFields[ipos++].Replace("'", "");
			c.mEdition = arrFields[ipos++].Replace("'", "");
			c.mEditionDate = arrFields[ipos++].Replace("'", "");
			c.mName = arrFields[ipos++].Replace("'", "");
			if (schema != Schema.IFC2x3)
			{
				c.mDescription = arrFields[ipos++].Replace("'", "");
				c.mLocation = arrFields[ipos++].Replace("'", "");
				c.mReferenceTokens = ParserSTEP.SplitListStrings(arrFields[ipos++]);
			}
		}
		protected override string BuildString()
		{
			string tokens = "$";
			if (mReferenceTokens.Count > 0)
			{
				tokens = "('" + mReferenceTokens;
				for (int icounter = 1; icounter < mReferenceTokens.Count; icounter++)
					tokens += "','" + mReferenceTokens;
				tokens += "')";
			}
			return base.BuildString() + (mSource == "$" ? (mDatabase.mSchema == Schema.IFC2x3 ? ",'Unknown'," : ",$,") : ",'" + mSource + "',") + (mEdition == "$" ? (mDatabase.mSchema == Schema.IFC2x3 ? "'Unknown'," : "$,") : "'" + mEdition + "',") + (mDatabase.mSchema == Schema.IFC2x3 ? mEditionDate : (mEdition == "$" ? "$" : "'" + mEditionDate + "'")) +
				",'" + mName + (mDatabase.mSchema == Schema.IFC2x3 ? "'" : (mDescription == "$" ? "',$," : "','" + mDescription + "',") + (mLocation == "$" ? "$," : "'" + mLocation + "',") + tokens);
		}
	}
	public class IfcClassificationItem : BaseClassIfc //DEPRECEATED IFC4
	{
		internal int mNotation;// : IfcClassificationNotationFacet;
		internal int mItemOf;//: OPTIONAL IfcClassification;
		internal string mTitle;// : IfcLabel; 

		internal IfcClassificationItem() : base() { }
		internal IfcClassificationItem(IfcClassificationItem i) : base() { mNotation = i.mNotation; mItemOf = i.mItemOf; mTitle = i.mTitle; }
		internal static IfcClassificationItem Parse(string strDef) { IfcClassificationItem c = new IfcClassificationItem(); int ipos = 0; parseFields(c, ParserSTEP.SplitLineFields(strDef), ref ipos); return c; }
		internal static void parseFields(IfcClassificationItem c, List<string> arrFields, ref int ipos) { c.mNotation = ParserSTEP.ParseLink(arrFields[ipos++]); c.mItemOf = ParserSTEP.ParseLink(arrFields[ipos++]); c.mTitle = arrFields[ipos++]; }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.LinkToString(mNotation) + "," + ParserSTEP.LinkToString(mItemOf) + "," + mTitle; }
	}
	public class IfcClassificationItemRelationship : BaseClassIfc //DEPRECEATED IFC4
	{
		internal string mSource;// : IfcLabel;
		internal string mEdition;// : IfcLabel;
		internal int mEditionDate;// : OPTIONAL IfcCalendarDate;
		internal string mName;// : IfcLabel;
		internal IfcClassificationItemRelationship() : base() { }
		internal IfcClassificationItemRelationship(IfcClassificationItemRelationship i)
			: base()
		{
			mSource = i.mSource;
			mEdition = i.mEdition;
			mEditionDate = i.mEditionDate;
			mName = i.mName;
		}
		internal static IfcClassificationItemRelationship Parse(string strDef) { IfcClassificationItemRelationship c = new IfcClassificationItemRelationship(); int ipos = 0; parseFields(c, ParserSTEP.SplitLineFields(strDef), ref ipos); return c; }
		internal static void parseFields(IfcClassificationItemRelationship c, List<string> arrFields, ref int ipos) { c.mSource = arrFields[ipos++]; c.mEdition = arrFields[ipos++]; c.mEditionDate = ParserSTEP.ParseLink(arrFields[ipos++]); c.mName = arrFields[ipos++]; }
		protected override string BuildString() { return base.BuildString() + "," + mSource + "," + mEdition + "," + ParserSTEP.LinkToString(mEditionDate) + "," + mName; }
	}
	public class IfcClassificationNotation : BaseClassIfc, IfcClassificationNotationSelect //DEPRECEATED IFC4
	{
		internal List<int> mNotationFacets = new List<int>();// : SET [1:?] OF IfcClassificationNotationFacet;

		internal IfcClassificationNotation() : base() { }
		internal IfcClassificationNotation(IfcClassificationNotation i) : base() { mNotationFacets = new List<int>(i.mNotationFacets.ToArray()); }
		internal static IfcClassificationNotation Parse(string strDef) { IfcClassificationNotation c = new IfcClassificationNotation(); int ipos = 0; parseFields(c, ParserSTEP.SplitLineFields(strDef), ref ipos); return c; }
		internal static void parseFields(IfcClassificationNotation c, List<string> arrFields, ref int ipos) { c.mNotationFacets = ParserSTEP.SplitListLinks(arrFields[ipos++]); }
		protected override string BuildString()
		{
			string str = base.BuildString() + ",(";
			if (mNotationFacets.Count > 0)
			{
				str += ParserSTEP.LinkToString(mNotationFacets[0]);
				for (int icounter = 1; icounter < mNotationFacets.Count; icounter++)
					str += "," + ParserSTEP.LinkToString(mNotationFacets[icounter]);
			}
			return str + ")";
		}
	}
	public class IfcClassificationNotationFacet : BaseClassIfc  //DEPRECEATED IFC4
	{
		internal string mNotationValue;//  : IfcLabel;
		internal IfcClassificationNotationFacet() : base() { }
		internal IfcClassificationNotationFacet(IfcClassification i) : base() { mNotationValue = i.mSource; }
		internal static IfcClassificationNotationFacet Parse(string strDef) { IfcClassificationNotationFacet c = new IfcClassificationNotationFacet(); int ipos = 0; parseFields(c, ParserSTEP.SplitLineFields(strDef), ref ipos); return c; }
		internal static void parseFields(IfcClassificationNotationFacet c, List<string> arrFields, ref int ipos) { c.mNotationValue = arrFields[ipos++]; }
		protected override string BuildString() { return base.BuildString() + "," + mNotationValue; }
	}
	public interface IfcClassificationNotationSelect : IfcInterface { } // List<IfcRelAssociatesclassification> classificationForObjects { get; } 	IfcClassificationNotation, IfcClassificationReference) 
	public class IfcClassificationReference : IfcExternalReference, IfcClassificationSelect, IfcClassificationNotationSelect
	{
		internal int mReferencedSource;// : OPTIONAL IfcClassificationReferenceSelect; //IFC2x3 : 	OPTIONAL IfcClassification;
		internal string mDescription = "$";// :	OPTIONAL IfcText; IFC4
		internal string mSort = "$";//	 :	OPTIONAL IfcIdentifier;
		//INVERSE 
		internal List<IfcRelAssociatesClassification> mClassificationRefForObjects = new List<IfcRelAssociatesClassification>();//	 :	SET [0:?] OF IfcRelAssociatesclassification FOR Relatingclassification;
		internal List<IfcClassificationReference> mHasReferences = new List<IfcClassificationReference>();//	 :	SET [0:?] OF IfcClassificationReference FOR ReferencedSource;
		public List<IfcRelAssociatesClassification> ClassificationForObjects { get { return mClassificationRefForObjects; } }

		internal IfcClassificationReference() : base() { }
		internal IfcClassificationReference(IfcClassificationReference r) : base(r) { mReferencedSource = r.mReferencedSource; mDescription = r.mDescription; mSort = r.mSort; }
		internal IfcClassificationReference(DatabaseIfc db) : base(db) {  }
		internal static IfcClassificationReference Parse(string strDef, Schema schema) { IfcClassificationReference r = new IfcClassificationReference(); int ipos = 0; parseFields(r, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return r; }
		internal static void parseFields(IfcClassificationReference r, List<string> arrFields, ref int ipos, Schema schema)
		{
			IfcExternalReference.parseFields(r, arrFields, ref ipos);
			r.mReferencedSource = ParserSTEP.ParseLink(arrFields[ipos++]);
			if (schema != Schema.IFC2x3)
			{
				r.mDescription = arrFields[ipos++].Replace("'", "");
				r.mSort = arrFields[ipos++].Replace("'", "");
			}
		}
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.LinkToString(mReferencedSource) + (mDatabase.mSchema == Schema.IFC2x3 ? "" : (mDescription == "$" ? ",$" : ",'" + mDescription + "'") + (mSort == "$" ? ",$" : ",'" + mSort + "'")); }
	}
	public interface IfcClassificationSelect : IfcInterface { List<IfcRelAssociatesClassification> ClassificationForObjects { get; } } // IFC4 rename IfcClassification,IfcClassificationReference 
	public partial class IfcClosedShell : IfcConnectedFaceSet, IfcShell
	{
		internal IfcClosedShell() : base() { }
		internal IfcClosedShell(IfcClosedShell c) : base(c) { }
		public IfcClosedShell(List<IfcFace> faces) : base(faces) { }
		internal new static IfcClosedShell Parse(string str) 
		{ 
			IfcClosedShell s = new IfcClosedShell(); 
			int pos = 0; 
			s.Parse(str, ref pos);
			return s; 
		}
	}
	public partial class IfcClothoidalArcSegment2D : IfcCurveSegment2D  //IFC4x1
	{
		private double mStartRadius;// : IfcPositiveLengthMeasure;
		private bool mIsCCW;// : IfcBoolean;
		private bool mIsEntry;// : IfcBoolean;
		private double mClothoidConstant;// : IfcReal;

		internal IfcClothoidalArcSegment2D() : base() { }
		internal IfcClothoidalArcSegment2D(IfcClothoidalArcSegment2D p) : base(p) { mStartRadius = p.mStartRadius; mIsCCW = p.mIsCCW; mIsEntry = p.mIsEntry; mClothoidConstant = p.mClothoidConstant; }
		internal IfcClothoidalArcSegment2D(IfcCartesianPoint start, double startDirection, double length, double radius, bool isCCW, bool isEntry, double clothoidConstant)
			: base(start, startDirection, length)
		{
			mStartRadius = radius;
			mIsCCW = isCCW;
			mIsEntry = isEntry;
			mClothoidConstant = clothoidConstant;
		}
		internal static void parseFields(IfcClothoidalArcSegment2D c, List<string> arrFields, ref int ipos)
		{
			IfcCurveSegment2D.parseFields(c, arrFields, ref ipos);
			c.mStartRadius = ParserSTEP.ParseDouble(arrFields[ipos++]);
			c.mIsCCW = ParserSTEP.ParseBool(arrFields[ipos++]);
			c.mIsEntry = ParserSTEP.ParseBool(arrFields[ipos++]);
			c.mClothoidConstant = ParserSTEP.ParseDouble(arrFields[ipos++]);
		}
		internal static IfcClothoidalArcSegment2D Parse(string strDef) { IfcClothoidalArcSegment2D c = new IfcClothoidalArcSegment2D(); int ipos = 0; parseFields(c, ParserSTEP.SplitLineFields(strDef), ref ipos); return c; }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.DoubleToString(mStartRadius) + "," + ParserSTEP.BoolToString(mIsCCW) + "," + ParserSTEP.BoolToString(mIsEntry) + "," + ParserSTEP.DoubleToString(mClothoidConstant); }
	}
	public class IfcCoil : IfcEnergyConversionDevice //IFC4
	{
		internal IfcCoilTypeEnum mPredefinedType = IfcCoilTypeEnum.NOTDEFINED;// OPTIONAL : IfcCoilTypeEnum;
		public IfcCoilTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcCoil() : base() { }
		internal IfcCoil(IfcCoil b) : base(b) { mPredefinedType = b.mPredefinedType; }
		internal IfcCoil(IfcProduct host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }

		internal static void parseFields(IfcCoil s, List<string> arrFields, ref int ipos)
		{
			IfcEnergyConversionDevice.parseFields(s, arrFields, ref ipos);
			string str = arrFields[ipos++];
			if (str[0] == '.')
				s.mPredefinedType = (IfcCoilTypeEnum)Enum.Parse(typeof(IfcCoilTypeEnum), str);
		}
		internal new static IfcCoil Parse(string strDef) { IfcCoil s = new IfcCoil(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildString()
		{
			return base.BuildString() + (mDatabase.mSchema == Schema.IFC2x3 ? "" : (mPredefinedType == IfcCoilTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + "."));
		}
	}
	public class IfcCoilType : IfcEnergyConversionDeviceType
	{
		internal IfcCoilTypeEnum mPredefinedType = IfcCoilTypeEnum.NOTDEFINED;// : IfcCoilTypeEnum;
		public IfcCoilTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcCoilType() : base() { }
		internal IfcCoilType(IfcCoilType t) : base(t) { mPredefinedType = t.mPredefinedType; }
		internal IfcCoilType(DatabaseIfc m, string name, IfcCoilTypeEnum t) : base(m) { Name = name; PredefinedType = t; }
		internal static void parseFields(IfcCoilType t,List<string> arrFields, ref int ipos) {  IfcEnergyConversionDeviceType.parseFields(t,arrFields, ref ipos); t.mPredefinedType = (IfcCoilTypeEnum)Enum.Parse(typeof(IfcCoilTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcCoilType Parse(string strDef) { IfcCoilType t = new IfcCoilType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildString() { return base.BuildString() + ",." + mPredefinedType.ToString() + "."; }
	}
	public interface IfcColour : IfcInterface { Color Colour { get; } }// = SELECT (IfcColourSpecification ,IfcPreDefinedColour); 
	public interface IfcColourOrFactor { }//IfcNormalisedRatioMeasure, IfcColourRgb);
	public partial class IfcColourRgb : IfcColourSpecification, IfcColourOrFactor
	{
		internal double mRed, mGreen, mBlue;// : IfcNormalisedRatioMeasure; 
		public override Color Colour { get { return Color.FromArgb((int)(mRed * 255), (int)(mGreen * 255), (int)(mBlue * 255)); } }
		
		internal IfcColourRgb() : base() { }
		internal IfcColourRgb(IfcColourRgb c) : base(c) { mRed = c.mRed; mGreen = c.mGreen; mBlue = c.mBlue; }
		internal IfcColourRgb(DatabaseIfc m, string name, Color col) : base(m, name) { mRed = col.R / 255.0; mGreen = col.G / 255.0; mBlue = col.B / 255.0; }
		internal static IfcColourRgb Parse(string strDef) { IfcColourRgb c = new IfcColourRgb(); int ipos = 0; parseFields(c, ParserSTEP.SplitLineFields(strDef), ref ipos); return c; }
		internal static void parseFields(IfcColourRgb c, List<string> arrFields, ref int ipos) { IfcColourSpecification.parseFields(c, arrFields, ref ipos); c.mRed = ParserSTEP.ParseDouble(arrFields[ipos++]); c.mGreen = ParserSTEP.ParseDouble(arrFields[ipos++]); c.mBlue = ParserSTEP.ParseDouble(arrFields[ipos++]); }
		protected override string BuildString() { return (mDatabase.mOutputEssential ? "" : base.BuildString() + "," + ParserSTEP.DoubleToString(mRed) + "," + ParserSTEP.DoubleToString(mGreen) + "," + ParserSTEP.DoubleToString(mBlue)); }
	}
	public class IfcColourRgbList : IfcPresentationItem
	{
		internal Tuple<double, double, double>[] mColourList = new Tuple<double, double, double>[0];//	: LIST [1:?] OF LIST [3:3] OF IfcNormalisedRatioMeasure; 
		internal IfcColourRgbList() : base() { }
		internal IfcColourRgbList(IfcColourRgbList i) : base() { mColourList = i.mColourList; }
		public IfcColourRgbList(DatabaseIfc m, IEnumerable<Color> colourList) : base(m)
		{
			mColourList = new Tuple<double, double, double>[colourList.Count()];
			int ilast = colourList.Count();
			for (int icounter = 0; icounter < ilast; icounter++)
			{
				Color c = colourList.ElementAt(icounter);
				mColourList[icounter] = new Tuple<double, double, double>(c.R / 255.0, c.G / 255.0, c.B / 255.0);
			}
		}
		internal static void parseFields(IfcColourRgbList s, List<string> arrFields, ref int ipos) { s.mColourList = ParserSTEP.SplitListDoubleTriple(arrFields[ipos++]); }
		internal static IfcColourRgbList Parse(string strDef) { IfcColourRgbList s = new IfcColourRgbList(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildString()
		{
			Tuple<double, double, double> t = mColourList[0];
			string result = base.BuildString() + ",((" + ParserSTEP.DoubleToString(t.Item1) + "," + ParserSTEP.DoubleToString(t.Item2) + "," + ParserSTEP.DoubleToString(t.Item3);
			for (int icounter = 1; icounter < mColourList.Length; icounter++)
			{
				t = mColourList[icounter];
				result += "),(" + ParserSTEP.DoubleToString(t.Item1) + "," + ParserSTEP.DoubleToString(t.Item2) + "," + ParserSTEP.DoubleToString(t.Item3);
			}

			return result + "))";
		}
		internal List<Color> ColorList
		{
			get
			{
				List<Color> result = new List<Color>();
				foreach (Tuple<double, double, double> c in mColourList)
					result.Add(Color.FromArgb((int)(c.Item1 * 255), (int)(c.Item2 * 255), (int)(c.Item3 * 255)));
				return result;
			}
		}
	}
	public abstract class IfcColourSpecification : IfcPresentationItem, IfcColour //	ABSTRACT SUPERTYPE OF(IfcColourRgb)
	{
		private string mName = "$";// : OPTIONAL IfcLabel
		public override string Name { get { return (mName == "$" ? "" : ParserIfc.Decode(mName)); } set { if (!string.IsNullOrEmpty(value)) mName = ParserIfc.Encode(value.Replace("'", "")); } }

		protected IfcColourSpecification() : base() { }
		protected IfcColourSpecification(IfcColourSpecification i) : base() { mName = i.mName; }
		protected IfcColourSpecification(DatabaseIfc m, string name) : base(m) { Name = name; }
		protected static void parseFields(IfcColourSpecification c, List<string> arrFields, ref int ipos) { c.mName = arrFields[ipos++].Replace("'", ""); }
		protected override string BuildString() { return base.BuildString() + (mName == "$" ? ",$" : ",'" + mName + "'"); }
		public abstract Color Colour { get; } 
	}
	public partial class IfcColumn : IfcBuildingElement
	{
		internal IfcColumnTypeEnum mPredefinedType = IfcColumnTypeEnum.NOTDEFINED;//: OPTIONAL IfcColumnTypeEnum; 
		public IfcColumnTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		
		internal IfcColumn() : base()  { }
		internal IfcColumn(IfcColumn c) : base(c) { mPredefinedType = c.mPredefinedType; }
		public IfcColumn(IfcProduct host, IfcObjectPlacement p, IfcProductRepresentation r) : base(host, p, r) { }
	 
		internal static IfcColumn Parse(string strDef, Schema schema) { IfcColumn col = new IfcColumn(); int ipos = 0; parseFields(col, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return col; }
		internal static void parseFields(IfcColumn c, List<string> arrFields, ref int ipos, Schema schema)
		{
			IfcBuildingElement.parseFields(c, arrFields, ref ipos);
			if (schema != Schema.IFC2x3)
			{
				string str = arrFields[ipos++];
				if (str[0] == '.')
					c.mPredefinedType = (IfcColumnTypeEnum)Enum.Parse(typeof(IfcColumnTypeEnum), str.Substring(1, str.Length - 2));
			
			}
		}
		protected override string BuildString() { return base.BuildString() + (mDatabase.mSchema == Schema.IFC2x3 ? "" : (mPredefinedType == IfcColumnTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
	}
	public partial class IfcColumnStandardCase : IfcColumn
	{
		public override string KeyWord { get { return (mDatabase.mSchema == Schema.IFC2x3 || mDatabase.mModelView == ModelView.Ifc4Reference ? "IFCCOLUMN" : base.KeyWord); } }
		internal IfcColumnStandardCase() : base() { }
		internal IfcColumnStandardCase(IfcColumnStandardCase o) : base(o) { }

		internal new static IfcColumnStandardCase Parse(string strDef, Schema schema) { IfcColumnStandardCase c = new IfcColumnStandardCase(); int ipos = 0; parseFields(c, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return c; }
		internal static void parseFields(IfcColumnStandardCase c, List<string> arrFields, ref int ipos, Schema schema) { IfcColumn.parseFields(c, arrFields, ref ipos,schema); }
	} 
	public partial class IfcColumnType : IfcBuildingElementType
	{
		internal IfcColumnTypeEnum mPredefinedType = IfcColumnTypeEnum.NOTDEFINED;
		public IfcColumnTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcColumnType() : base() { }
		internal IfcColumnType(IfcColumnType t) : base(t) { mPredefinedType = t.mPredefinedType; }
		internal IfcColumnType(DatabaseIfc m, string name, IfcColumnTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
		public IfcColumnType(string name, IfcMaterialProfile ps, IfcColumnTypeEnum type) : this(name,new IfcMaterialProfileSet(ps.Name, ps), type) { }
		internal IfcColumnType(string name, IfcMaterialProfileSet ps, IfcColumnTypeEnum type) : base(ps.mDatabase)
		{
			Name = name;
			mPredefinedType = type;
			if (ps.mTaperEnd != null)
				mTapering = ps;
			else
				MaterialSelect = ps;
		}
		internal static void parseFields(IfcColumnType t, List<string> arrFields, ref int ipos) { IfcBuildingElementType.parseFields(t,arrFields, ref ipos); t.mPredefinedType = (IfcColumnTypeEnum)Enum.Parse(typeof(IfcColumnTypeEnum), arrFields[ipos++].Replace(".", ""));  }
		internal new static IfcColumnType Parse(string strDef) { IfcColumnType t = new IfcColumnType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildString() { return base.BuildString() + ",." + mPredefinedType.ToString() + "."; }

	}
	public partial class IfcComplexProperty : IfcProperty
	{
		internal string mUsageName;// : IfcIdentifier;
		internal List<int> mHasProperties = new List<int>();// : SET [1:?] OF IfcProperty;

		public List<IfcProperty> HasProperties { get { return mHasProperties.ConvertAll(x => mDatabase.mIfcObjects[x] as IfcProperty); } }

		internal IfcComplexProperty(IfcComplexProperty p) : base(p) { mUsageName = p.mUsageName; mHasProperties = new List<int>(p.mHasProperties.ToArray()); }
		internal IfcComplexProperty() : base() { }
		internal IfcComplexProperty(DatabaseIfc m, string name, string desc) : base(m, name, desc) { }
		internal static void parseFields(IfcComplexProperty p, List<string> arrFields, ref int ipos) { IfcProperty.parseFields(p, arrFields, ref ipos); p.mUsageName = arrFields[ipos++]; p.mHasProperties = ParserSTEP.SplitListLinks(arrFields[ipos++]); }
		internal static IfcComplexProperty Parse(string strDef) { IfcComplexProperty p = new IfcComplexProperty(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos); return p; }
		protected override string BuildString()
		{
			string str = base.BuildString() + "," + mUsageName + ",(" + ParserSTEP.LinkToString(mHasProperties[0]);
			for (int icounter = 1; icounter < mHasProperties.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mHasProperties[icounter]);
			return str + ")";
		}
		internal void relate()
		{
			List<IfcProperty> props = HasProperties;
			for (int icounter = 0; icounter < props.Count; icounter++)
				props[icounter].mPartOfComplex.Add(this);
		}
	}
	public partial class IfcCompositeCurve : IfcBoundedCurve
	{
		private List<int> mSegments = new List<int>();// : LIST [1:?] OF IfcCompositeCurveSegment;
		private IfcLogicalEnum mSelfIntersect = IfcLogicalEnum.UNKNOWN;// : LOGICAL;

		internal List<IfcCompositeCurveSegment> Segments { get { return mSegments.ConvertAll(x => mDatabase.mIfcObjects[x] as IfcCompositeCurveSegment); } }
		internal IfcLogicalEnum SelfIntersect { get { return mSelfIntersect; } }

		internal IfcCompositeCurve() : base() { }
		internal IfcCompositeCurve(IfcCompositeCurve pl) : base(pl) { mSegments = new List<int>(pl.mSegments.ToArray()); mSelfIntersect = pl.mSelfIntersect; }
		public IfcCompositeCurve(List<IfcCompositeCurveSegment> segs) : base(segs[0].mDatabase) { mSegments = segs.ConvertAll(x => x.mIndex); }
		internal static void parseFields(IfcCompositeCurve c, List<string> arrFields, ref int ipos) { IfcBoundedCurve.parseFields(c, arrFields, ref ipos); c.mSegments = ParserSTEP.SplitListLinks(arrFields[ipos++]); c.mSelfIntersect = ParserIfc.ParseIFCLogical(arrFields[ipos++]); }
		internal static IfcCompositeCurve Parse(string strDef) { IfcCompositeCurve c = new IfcCompositeCurve(); int ipos = 0; parseFields(c, ParserSTEP.SplitLineFields(strDef), ref ipos); return c; }
		protected override string BuildString()
		{
			string str = base.BuildString() + ",(";
			if (mSegments.Count > 0)
				str += ParserSTEP.LinkToString(mSegments[0]);
			for (int icounter = 1; icounter < mSegments.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mSegments[icounter]);
			str += "),";
			str += ParserIfc.LogicalToString(mSelfIntersect);
			return base.BuildString() + str;
		}
	}
	public class Ifc2dCompositeCurve : IfcCompositeCurve
	{
		internal Ifc2dCompositeCurve() : base() { }
		internal Ifc2dCompositeCurve(Ifc2dCompositeCurve pl) : base(pl) { }
		internal static void parseFields(Ifc2dCompositeCurve c, List<string> arrFields, ref int ipos) { IfcCompositeCurve.parseFields(c, arrFields, ref ipos); }
		internal new static IfcCompositeCurve Parse(string strDef) { Ifc2dCompositeCurve c = new Ifc2dCompositeCurve(); int ipos = 0; parseFields(c, ParserSTEP.SplitLineFields(strDef), ref ipos); return c; }
	}
	public class IfcCompositeCurveOnSurface : IfcCompositeCurve
	{
		internal IfcCompositeCurveOnSurface() : base() { }
		internal IfcCompositeCurveOnSurface(IfcCompositeCurveOnSurface i) : base(i) { }
		internal IfcCompositeCurveOnSurface(List<IfcCompositeCurveSegment> segs) : base(segs) { }
		internal new static IfcCompositeCurveOnSurface Parse(string strDef) { IfcCompositeCurveOnSurface c = new IfcCompositeCurveOnSurface(); int ipos = 0; parseFields(c, ParserSTEP.SplitLineFields(strDef), ref ipos); return c; }
		internal static void parseFields(IfcCompositeCurveOnSurface c, List<string> arrFields, ref int ipos) { IfcCompositeCurve.parseFields(c, arrFields, ref ipos); }
	}
	public partial class IfcCompositeCurveSegment : IfcGeometricRepresentationItem
	{
		private IfcTransitionCode mTransition;// : IfcTransitionCode;
		private bool mSameSense;// : BOOLEAN;
		private int mParentCurve;// : IfcCurve;  Really IfcBoundedCurve WR1

		public IfcTransitionCode Transition { get { return mTransition; } }
		public bool SameSense { get { return mSameSense; } }
		public IfcBoundedCurve ParentCurve { get { return mDatabase.mIfcObjects[mParentCurve] as IfcBoundedCurve; } }

		internal IfcCompositeCurveSegment() : base() { }
		internal IfcCompositeCurveSegment(IfcCompositeCurveSegment el) : base(el) { mTransition = el.mTransition; mSameSense = el.mSameSense; mParentCurve = el.mParentCurve; }
		public IfcCompositeCurveSegment(IfcTransitionCode tc, bool sense, IfcBoundedCurve bc) : base(bc.mDatabase) { mSameSense = sense; mTransition = tc; }
		internal static IfcCompositeCurveSegment Parse(string strDef) { IfcCompositeCurveSegment s = new IfcCompositeCurveSegment(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		internal static void parseFields(IfcCompositeCurveSegment s, List<string> arrFields, ref int ipos) { IfcGeometricRepresentationItem.parseFields(s, arrFields, ref ipos); s.mTransition = (IfcTransitionCode)Enum.Parse(typeof(IfcTransitionCode), arrFields[ipos++].Replace(".", "")); s.mSameSense = ParserSTEP.ParseBool(arrFields[ipos++]); s.mParentCurve = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildString() { return base.BuildString() + ",." + mTransition.ToString() + ".," + ParserSTEP.BoolToString(mSameSense) + "," + ParserSTEP.LinkToString(mParentCurve); }

	}
	public partial class IfcCompositeProfileDef : IfcProfileDef
	{
		private List<int> mProfiles = new List<int>();// : SET [2:?] OF IfcProfileDef;
		private string mLabel = "$";// : OPTIONAL IfcLabel;

		internal List<IfcProfileDef> Profiles { get { return mProfiles.ConvertAll(x => mDatabase.mIfcObjects[x] as IfcProfileDef); } }
		public string Label { get { return (mLabel == "$" ? "" : ParserIfc.Decode(mLabel)); } set { mLabel = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }

		internal IfcCompositeProfileDef() : base() { }
		internal IfcCompositeProfileDef(IfcCompositeProfileDef i) : base(i) { mProfiles = new List<int>(i.mProfiles.ToArray()); mLabel = i.mLabel; }
		private IfcCompositeProfileDef(DatabaseIfc m, string name) : base(m)
		{
			Name = name;
			if (mDatabase.mModelView == ModelView.Ifc4Reference)
				throw new Exception("Invalid Model View for IfcCompositeProfileDef : " + m.ModelView.ToString());
		}
		public IfcCompositeProfileDef(string name, List<IfcProfileDef> defs) : this(defs[0].mDatabase, name) { mProfiles = defs.ConvertAll(x => x.mIndex); }
		public IfcCompositeProfileDef(string name, IfcProfileDef p1, IfcProfileDef p2) : this(p1.mDatabase, name) { mProfiles.Add(p1.mIndex); mProfiles.Add(p2.mIndex); }
		internal new static IfcCompositeProfileDef Parse(string strDef) { IfcCompositeProfileDef p = new IfcCompositeProfileDef(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos); return p; }
		internal static void parseFields(IfcCompositeProfileDef p, List<string> arrFields, ref int ipos) { IfcProfileDef.parseFields(p, arrFields, ref ipos); p.mProfiles = ParserSTEP.SplitListLinks(arrFields[ipos++]); p.mLabel = arrFields[ipos++].Replace("'", ""); }
		protected override string BuildString()
		{
			string str = base.BuildString() + ",(" + ParserSTEP.LinkToString(mProfiles[0]);
			for (int icounter = 1; icounter < mProfiles.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mProfiles[icounter]);
			return str + (mLabel == "$" ? "),$" : "),'" + mLabel + "'");
		}
	}
	public partial class IfcCompoundPlaneAngleMeasure
	{
		internal int mDegrees = 0, mMinutes = 0, mSeconds = 0, mMicroSeconds = 0;
		internal IfcCompoundPlaneAngleMeasure(double angleDegrees)
		{
			double ang = Math.Abs(angleDegrees);
			int sign = angleDegrees < 0 ? -1 : 1;

			mDegrees = sign * (int)Math.Floor(ang);
			mMinutes = sign * (int)Math.Floor((ang - mDegrees) * 60.0);
			mSeconds = sign * (int)Math.Floor(((ang - mDegrees) * 60 - mMinutes) * 60);
			mMicroSeconds = sign * (int)Math.Floor((((ang - mDegrees) * 60 - mMinutes) * 60 - mSeconds) * 1e6);

		}
		internal IfcCompoundPlaneAngleMeasure(int degrees, int minutes, int seconds, int microSeconds)
		{
			mDegrees = degrees;
			mMinutes = minutes;
			mSeconds = seconds;
			mMicroSeconds = microSeconds;
		}
		
		public override string ToString() { return "(" + mDegrees + "," + mMinutes + "," + mSeconds + "," + mMicroSeconds + ")"; }
		internal double computeAngle()
		{
			double compound = Math.Abs(mMinutes) / 60.0 + Math.Abs(mSeconds) / 3600.0 + Math.Abs(mMicroSeconds) / 3600 * 1e-6;
			return mDegrees + (mDegrees == 0 ? (mMinutes == 0 ? (mSeconds == 0 ? (mMicroSeconds > 0 ? 1 : -1) : (mSeconds > 0 ? 1 : -1)) : (mMinutes > 0 ? 1 : -1)) : (mDegrees > 0 ? 1 : -1)) * compound;
		}

	}
	public class IfcCompressor : IfcFlowMovingDevice //IFC4
	{
		internal IfcCompressorTypeEnum mPredefinedType = IfcCompressorTypeEnum.NOTDEFINED;// OPTIONAL : IfcCompressorTypeEnum;
		public IfcCompressorTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcCompressor() : base() { }
		internal IfcCompressor(IfcCompressor c) : base(c) { mPredefinedType = c.mPredefinedType; }
		internal IfcCompressor(IfcProduct host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }

		internal static void parseFields(IfcCompressor s, List<string> arrFields, ref int ipos)
		{
			IfcEnergyConversionDevice.parseFields(s, arrFields, ref ipos);
			string str = arrFields[ipos++];
			if (str[0] == '.')
				s.mPredefinedType = (IfcCompressorTypeEnum)Enum.Parse(typeof(IfcCompressorTypeEnum), str);
		}
		internal new static IfcCompressor Parse(string strDef) { IfcCompressor s = new IfcCompressor(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildString()
		{
			return base.BuildString() + (mDatabase.mSchema == Schema.IFC2x3 ? "" : (mPredefinedType == IfcCompressorTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + "."));
		}
	}
	public class IfcCompressorType : IfcFlowMovingDeviceType
	{
		internal IfcCompressorTypeEnum mPredefinedType = IfcCompressorTypeEnum.NOTDEFINED;// : IfcCompressorTypeEnum;
		public IfcCompressorTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcCompressorType() : base() { }
		internal IfcCompressorType(IfcCompressorType t) : base(t) { mPredefinedType = t.mPredefinedType; }
		internal IfcCompressorType(DatabaseIfc m, string name, IfcCompressorTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
		internal static void parseFields(IfcCompressorType t, List<string> arrFields, ref int ipos) { IfcFlowFittingType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcCompressorTypeEnum)Enum.Parse(typeof(IfcCompressorTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcCompressorType Parse(string strDef) { IfcCompressorType t = new IfcCompressorType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildString() { return base.BuildString() + ",." + mPredefinedType.ToString() + "."; }
	}
	public class IfcCommunicationsAppliance : IfcFlowTerminal //IFC4
	{
		internal IfcCommunicationsApplianceTypeEnum mPredefinedType = IfcCommunicationsApplianceTypeEnum.NOTDEFINED;// OPTIONAL : IfcCommunicationsApplianceTypeEnum;
		public IfcCommunicationsApplianceTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcCommunicationsAppliance(IfcCommunicationsAppliance a) : base(a) { mPredefinedType = a.mPredefinedType; }
		internal IfcCommunicationsAppliance() : base() { }
		internal static void parseFields(IfcCommunicationsAppliance s, List<string> arrFields, ref int ipos)
		{
			IfcFlowTerminal.parseFields(s, arrFields, ref ipos);
			string str = arrFields[ipos++];
			if (str[0] == '.')
				s.mPredefinedType = (IfcCommunicationsApplianceTypeEnum)Enum.Parse(typeof(IfcCommunicationsApplianceTypeEnum), str.Substring(1, str.Length - 2));
		}
		internal new static IfcCommunicationsAppliance Parse(string strDef) { IfcCommunicationsAppliance s = new IfcCommunicationsAppliance(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildString() { return base.BuildString() + (mDatabase.mSchema == Schema.IFC2x3 ? "" : (mPredefinedType == IfcCommunicationsApplianceTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + ".")); }
	}
	public partial class IfcCommunicationsApplianceType : IfcFlowTerminalType
	{
		internal IfcCommunicationsApplianceTypeEnum mPredefinedType = IfcCommunicationsApplianceTypeEnum.NOTDEFINED;// : IfcCommunicationsApplianceBoxTypeEnum; 
		public IfcCommunicationsApplianceTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcCommunicationsApplianceType(IfcCommunicationsApplianceType t) : base(t) { mPredefinedType = t.mPredefinedType; }
		internal IfcCommunicationsApplianceType() : base() { }
		internal IfcCommunicationsApplianceType(DatabaseIfc m, string name, IfcCommunicationsApplianceTypeEnum t) : base(m) { Name = name; mPredefinedType = t; }
		internal static void parseFields(IfcCommunicationsApplianceType t, List<string> arrFields, ref int ipos) { IfcFlowControllerType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcCommunicationsApplianceTypeEnum)Enum.Parse(typeof(IfcCommunicationsApplianceTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcCommunicationsApplianceType Parse(string strDef) { IfcCommunicationsApplianceType t = new IfcCommunicationsApplianceType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildString() { return base.BuildString() + ",." + mPredefinedType.ToString() + "."; }
	}
	public class IfcCondenser : IfcEnergyConversionDevice //IFC4
	{
		internal IfcCondenserTypeEnum mPredefinedType = IfcCondenserTypeEnum.NOTDEFINED;// OPTIONAL : IfcCCondenserTypeEnum;
		public IfcCondenserTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcCondenser() : base() { }
		internal IfcCondenser(IfcCondenser b) : base(b) { mPredefinedType = b.mPredefinedType; }
		internal IfcCondenser(IfcProduct host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }

		internal static void parseFields(IfcCondenser s, List<string> arrFields, ref int ipos)
		{
			IfcEnergyConversionDevice.parseFields(s, arrFields, ref ipos);
			string str = arrFields[ipos++];
			if (str[0] == '.')
				s.mPredefinedType = (IfcCondenserTypeEnum)Enum.Parse(typeof(IfcCondenserTypeEnum), str);
		}
		internal new static IfcCondenser Parse(string strDef) { IfcCondenser s = new IfcCondenser(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildString()
		{
			return base.BuildString() + (mDatabase.mSchema == Schema.IFC2x3 ? "" : (mPredefinedType == IfcCondenserTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + "."));
		}
	}
	public class IfcCondenserType : IfcEnergyConversionDeviceType
	{
		internal IfcCondenserTypeEnum mPredefinedType = IfcCondenserTypeEnum.NOTDEFINED;// : IfcCondenserTypeEnum;
		public IfcCondenserTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcCondenserType() : base() { }
		internal IfcCondenserType(IfcCondenserType t) : base(t) { mPredefinedType = t.mPredefinedType; }
		internal IfcCondenserType(DatabaseIfc m, string name, IfcCondenserTypeEnum t) : base(m) { Name = name; mPredefinedType = t; }
		internal static void parseFields(IfcCondenserType t, List<string> arrFields, ref int ipos) { IfcEnergyConversionDeviceType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcCondenserTypeEnum)Enum.Parse(typeof(IfcCondenserTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcCondenserType Parse(string strDef) { IfcCondenserType t = new IfcCondenserType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildString() { return base.BuildString() + ",." + mPredefinedType.ToString() + "."; }
	}
	public class IfcCondition : IfcGroup //DEPRECEATED IFC4
	{
		internal IfcCondition() : base() { }
		internal IfcCondition(IfcCondition c) : base(c) { }
		internal new static IfcCondition Parse(string strDef) { IfcCondition c = new IfcCondition(); int ipos = 0; parseFields(c, ParserSTEP.SplitLineFields(strDef), ref ipos); return c; }
		internal static void parseFields(IfcCondition c, List<string> arrFields, ref int ipos) { IfcGroup.parseFields(c, arrFields, ref ipos); }
	}
	public class IfcConditionCriterion : IfcControl //DEPRECEATED IFC4
	{
		internal int mCriterion;// : IfcConditionCriterionSelect;
		internal int mCriterionDateTime;// : IfcDateTimeSelect; 
		internal IfcConditionCriterion() : base() { }
		internal IfcConditionCriterion(IfcConditionCriterion c) : base(c) { mCriterion = c.mCriterion; mCriterionDateTime = c.mCriterionDateTime; }
		internal static IfcConditionCriterion Parse(string strDef, Schema schema) { IfcConditionCriterion c = new IfcConditionCriterion(); int ipos = 0; parseFields(c, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return c; }
		internal static void parseFields(IfcConditionCriterion c, List<string> arrFields, ref int ipos, Schema schema) { IfcControl.parseFields(c, arrFields, ref ipos,schema); c.mCriterion = ParserSTEP.ParseLink(arrFields[ipos++]); c.mCriterionDateTime = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.LinkToString(mCriterion) + "," + ParserSTEP.LinkToString(mCriterionDateTime); }
	}
	public abstract partial class IfcConic : IfcCurve /*ABSTRACT SUPERTYPE OF (ONEOF (IfcCircle ,IfcEllipse))*/
	{
		private int mPosition;// : IfcAxis2Placement;
		internal IfcAxis2Placement Position { get { return mDatabase.mIfcObjects[mPosition] as IfcAxis2Placement; } }

		protected IfcConic() : base() { }
		protected IfcConic(IfcConic el) : base(el) { mPosition = el.mPosition; }
		protected IfcConic(IfcAxis2Placement ap) : base(ap.Database) { mPosition = ap.Index; }
		protected static void parseFields(IfcConic c, List<string> arrFields, ref int ipos) { IfcCurve.parseFields(c, arrFields, ref ipos); c.mPosition = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.LinkToString(mPosition); }
	}
	public partial class IfcConnectedFaceSet : IfcTopologicalRepresentationItem //SUPERTYPE OF (ONEOF (IfcClosedShell ,IfcOpenShell))
	{
		private List<int> mCfsFaces = new List<int>();// : SET [1:?] OF IfcFace;
		public List<IfcFace> CfsFaces { get { return mCfsFaces.ConvertAll(x => mDatabase.mIfcObjects[x] as IfcFace); } }

		internal IfcConnectedFaceSet() : base() { }
		internal IfcConnectedFaceSet(IfcConnectedFaceSet c) : base(c) { mCfsFaces = new List<int>(c.mCfsFaces.ToArray()); }
		internal IfcConnectedFaceSet(List<IfcFace> faces) : base(faces[0].mDatabase) { mCfsFaces = faces.ConvertAll(x => x.mIndex); }
		protected override string BuildString()
		{
			if (mDatabase.mOutputEssential || mCfsFaces.Count == 0)
				return "";
			string str = base.BuildString() + ",(" + ParserSTEP.LinkToString(mCfsFaces[0]);
			if (mCfsFaces.Count > 100)
			{
				StringBuilder sb = new StringBuilder();
				for (int icounter = 1; icounter < mCfsFaces.Count; icounter++)
					sb.Append(",#" + mCfsFaces[icounter]);

				str += sb.ToString();
			}
			else
			{
				for (int icounter = 1; icounter < mCfsFaces.Count; icounter++)
					str += ",#" + mCfsFaces[icounter];
			}
			return str + ")";
		}
		protected override void Parse(string str, ref int ipos) { base.Parse(str, ref ipos); mCfsFaces = ParserSTEP.SplitListLinks(str.Substring(1, str.Length - 2)); }
		internal static IfcConnectedFaceSet Parse(string strDef) { IfcConnectedFaceSet s = new IfcConnectedFaceSet(); int ipos = 0; s.Parse(strDef, ref ipos); return s; }

	}
	public class IfcConnectionCurveGeometry : IfcConnectionGeometry
	{
		private int mCurveOnRelatingElement;// : IfcCurveOrEdgeCurve;
		private int mCurveOnRelatedElement;// : OPTIONAL IfcCurveOrEdgeCurve; 

		//internal IfcCurveOrEdgeCurve CurveOnRelatingElement { get { } }
		//internal IfcCurveOrEdgeCurve CurveOnRelatedElement { get { } }

		internal IfcConnectionCurveGeometry() : base() { }
		internal IfcConnectionCurveGeometry(IfcConnectionCurveGeometry g) : base(g) { mCurveOnRelatingElement = g.mCurveOnRelatingElement; mCurveOnRelatedElement = g.mCurveOnRelatedElement; }
		internal static IfcConnectionCurveGeometry Parse(string strDef) { IfcConnectionCurveGeometry c = new IfcConnectionCurveGeometry(); int ipos = 0; parseFields(c, ParserSTEP.SplitLineFields(strDef), ref ipos); return c; }
		internal static void parseFields(IfcConnectionCurveGeometry c, List<string> arrFields, ref int ipos) { IfcConnectionGeometry.parseFields(c, arrFields, ref ipos); c.mCurveOnRelatingElement = ParserSTEP.ParseLink(arrFields[ipos++]); c.mCurveOnRelatedElement = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.LinkToString(mCurveOnRelatingElement) + "," + ParserSTEP.LinkToString(mCurveOnRelatedElement); }
	}
	public abstract class IfcConnectionGeometry : BaseClassIfc /*ABSTRACT SUPERTYPE OF (ONEOF(IfcConnectionCurveGeometry,IfcConnectionPointGeometry,IfcConnectionPortGeometry,IfcConnectionSurfaceGeometry));*/
	{
		protected IfcConnectionGeometry() : base() { }
		protected IfcConnectionGeometry(IfcConnectionGeometry i) : base() { }
		protected IfcConnectionGeometry(DatabaseIfc m) : base(m) { }
		protected static void parseFields(IfcConnectionGeometry c, List<string> arrFields, ref int ipos) { }
	}
	public partial class IfcConnectionPointEccentricity : IfcConnectionPointGeometry
	{ 
		private double mEccentricityInX, mEccentricityInY, mEccentricityInZ;// : OPTIONAL IfcLengthMeasure;
		public double EccentricityInX { get { return mEccentricityInX; } set { mEccentricityInX = value; } }
		public double EccentricityInY { get { return mEccentricityInY; } set { mEccentricityInY = value; } }
		public double EccentricityInZ { get { return mEccentricityInZ; } set { mEccentricityInZ = value; } }

		internal IfcConnectionPointEccentricity() : base() { }
		internal IfcConnectionPointEccentricity(IfcConnectionPointEccentricity e) : base(e) { mEccentricityInX = e.mEccentricityInX; mEccentricityInY = e.mEccentricityInY; mEccentricityInZ = e.mEccentricityInZ; }
		public IfcConnectionPointEccentricity(IfcPointOrVertexPoint v, double x, double y, double z) : base(v) { mEccentricityInX = x; mEccentricityInY = y; mEccentricityInZ = z; }

		internal new static IfcConnectionPointEccentricity Parse(string strDef) { IfcConnectionPointEccentricity c = new IfcConnectionPointEccentricity(); int ipos = 0; parseFields(c, ParserSTEP.SplitLineFields(strDef), ref ipos); return c; }
		internal static void parseFields(IfcConnectionPointEccentricity c, List<string> arrFields, ref int ipos) { IfcConnectionPointGeometry.parseFields(c, arrFields, ref ipos); c.mEccentricityInX = ParserSTEP.ParseDouble(arrFields[ipos++]); c.mEccentricityInY = ParserSTEP.ParseDouble(arrFields[ipos++]); c.mEccentricityInZ = ParserSTEP.ParseDouble(arrFields[ipos++]); }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.DoubleOptionalToString(mEccentricityInX) + "," + ParserSTEP.DoubleOptionalToString(mEccentricityInY) + "," + ParserSTEP.DoubleOptionalToString(mEccentricityInZ); }
	}
	public class IfcConnectionPointGeometry : IfcConnectionGeometry
	{
		private int mPointOnRelatingElement;// : IfcPointOrVertexPoint;
		private int mPointOnRelatedElement;// : OPTIONAL IfcPointOrVertexPoint;

		public IfcPointOrVertexPoint PointOnRelatingElement { get { return mDatabase.mIfcObjects[mPointOnRelatingElement] as IfcPointOrVertexPoint; } }
		public IfcPointOrVertexPoint PointOnRelatedElement { get { return mDatabase.mIfcObjects[mPointOnRelatedElement] as IfcPointOrVertexPoint; } }

		internal IfcConnectionPointGeometry() : base() { }
		internal IfcConnectionPointGeometry(IfcConnectionPointGeometry g) : base(g) { mPointOnRelatingElement = g.mPointOnRelatingElement; mPointOnRelatedElement = g.mPointOnRelatedElement; }
		internal IfcConnectionPointGeometry(IfcPointOrVertexPoint v) : base(v.Database) { mPointOnRelatingElement = v.Index; }
		internal static IfcConnectionPointGeometry Parse(string strDef) { IfcConnectionPointGeometry c = new IfcConnectionPointGeometry(); int ipos = 0; parseFields(c, ParserSTEP.SplitLineFields(strDef), ref ipos); return c; }
		internal static void parseFields(IfcConnectionPointGeometry c, List<string> arrFields, ref int ipos) { IfcConnectionGeometry.parseFields(c, arrFields, ref ipos); c.mPointOnRelatingElement = ParserSTEP.ParseLink(arrFields[ipos++]); c.mPointOnRelatedElement = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.LinkToString(mPointOnRelatingElement) + "," + ParserSTEP.LinkToString(mPointOnRelatedElement); }
	}
	//ENTITY IfcConnectionPortGeometry  // DEPRECEATED IFC4
	public class IfcConnectionSurfaceGeometry : IfcConnectionGeometry
	{
		internal int mSurfaceOnRelatingElement;// : IfcSurfaceOrFaceSurface;
		internal int mSurfaceOnRelatedElement;// : OPTIONAL IfcSurfaceOrFaceSurface;

		//internal IfcSurfaceOrFaceSurface SurfaceOnRelatingElement { get { return mModel.mIFCobjs[mSurfaceOnRelatingElement]; } }
		//internal IfcSurfaceOrFaceSurface SurfaceOnRelatedElement { get { return mModel.mIFCobjs[mSurfaceOnRelatedElement]; } }

		internal IfcConnectionSurfaceGeometry() : base() { }
		internal IfcConnectionSurfaceGeometry(IfcConnectionSurfaceGeometry el) : base(el) { mSurfaceOnRelatingElement = el.mSurfaceOnRelatingElement; mSurfaceOnRelatedElement = el.mSurfaceOnRelatedElement; }
		internal static IfcConnectionSurfaceGeometry Parse(string strDef) { IfcConnectionSurfaceGeometry c = new IfcConnectionSurfaceGeometry(); int ipos = 0; parseFields(c, ParserSTEP.SplitLineFields(strDef), ref ipos); return c; }
		internal static void parseFields(IfcConnectionSurfaceGeometry c, List<string> arrFields, ref int ipos) { IfcConnectionGeometry.parseFields(c, arrFields, ref ipos); c.mSurfaceOnRelatingElement = ParserSTEP.ParseLink(arrFields[ipos++]); c.mSurfaceOnRelatedElement = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.LinkToString(mSurfaceOnRelatingElement) + "," + ParserSTEP.LinkToString(mSurfaceOnRelatedElement); }
	}
	public abstract class IfcConstraint : BaseClassIfc, IfcResourceObjectSelect //IFC4Change ABSTRACT SUPERTYPE OF(ONEOF(IfcMetric, IfcObjective));
	{
		internal string mName;// :  IfcLabel;
		internal string mDescription = "$";// : OPTIONAL IfcText;
		internal IfcConstraintEnum mConstraintGrade;// : IfcConstraintEnum
		internal string mConstraintSource = "$";// : OPTIONAL IfcLabel;
		internal int mCreatingActor;// : OPTIONAL IfcActorSelect;
		internal string mCreationTime = "$";// : OPTIONAL IfcDateTimeSelect; IFC4 IfcDateTime 
		internal int mSSCreationTime;// : OPTIONAL IfcDateTimeSelect; IFC4 IfcDateTime 
		internal string mUserDefinedGrade = "$";// : OPTIONAL IfcLabel

		public override string Name { get { return (mName == "$" ? "" : ParserIfc.Decode(mName)); } set { mName = (string.IsNullOrEmpty(value) ? "NOTDEFINED" : ParserIfc.Encode(value.Replace("'", ""))); } }
		public string Description { get { return (mDescription == "$" ? "" : ParserIfc.Decode(mDescription)); } set { mDescription = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }
		public IfcConstraintEnum ConstraintGrade { get { return mConstraintGrade; } set { mConstraintGrade = value; } }
		public string ConstraintSource { get { return (mConstraintSource == "$" ? "" : ParserIfc.Decode(mConstraintSource)); } set { mConstraintSource = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }
		public IfcActorSelect CreatingActor { get { return mDatabase.mIfcObjects[mCreatingActor] as IfcActorSelect; } set { mCreatingActor = (value == null ? 0 : value.Index); } }
		//creationtime
		public string UserDefinedGrade { get { return (mUserDefinedGrade == "$" ? "" : ParserIfc.Decode(mUserDefinedGrade)); } set { mUserDefinedGrade = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }

		//	INVERSE
		internal List<IfcExternalReferenceRelationship> mHasExternalReferences = new List<IfcExternalReferenceRelationship>(); //IFC4
		internal List<IfcResourceConstraintRelationship> mPropertiesForConstraint = new List<IfcResourceConstraintRelationship>();//	 :	SET OF IfcResourceConstraintRelationship FOR RelatingConstraint;
		internal List<IfcResourceConstraintRelationship> mHasConstraintRelationships = new List<IfcResourceConstraintRelationship>(); //gg
		public List<IfcExternalReferenceRelationship> HasExternalReferences { get { return mHasExternalReferences; } }
		public List<IfcResourceConstraintRelationship> HasConstraintRelationships { get { return mHasConstraintRelationships; } }

		protected IfcConstraint() : base() { }
		protected IfcConstraint(IfcConstraint c) : base()
		{
			mName = c.mName;
			mDescription = c.mDescription;
			mConstraintGrade = c.mConstraintGrade;
			mConstraintSource = c.mConstraintSource;
			mCreatingActor = c.mCreatingActor;
			mCreationTime = c.mCreationTime;
			mSSCreationTime = c.mSSCreationTime;
			mUserDefinedGrade = c.mUserDefinedGrade;
		}
		protected IfcConstraint(DatabaseIfc db, string name, IfcConstraintEnum constraint) : base(db) { Name = name; mConstraintGrade = constraint; }
		internal static void parseFields(IfcConstraint a, List<string> arrFields, ref int ipos, Schema schema)
		{
			a.mName = arrFields[ipos++].Replace("'", "");
			a.mDescription = arrFields[ipos++].Replace("'", "");
			a.mConstraintGrade = (IfcConstraintEnum)Enum.Parse(typeof(IfcConstraintEnum), arrFields[ipos++].Replace(".", ""));
			a.mConstraintSource = arrFields[ipos++].Replace("'", "");
			a.mCreatingActor = ParserSTEP.ParseLink(arrFields[ipos++]);
			if (schema == Schema.IFC2x3)
				a.mSSCreationTime = ParserSTEP.ParseLink(arrFields[ipos++]);
			else
				a.mCreationTime = arrFields[ipos++].Replace("'", "");
			a.mUserDefinedGrade = arrFields[ipos++].Replace("'", "");
		}
		protected override string BuildString() { return base.BuildString() + ",'" + mName + (mDescription == "$" ? "',$,." : "','" + mDescription + "',.") + mConstraintGrade.ToString() + (mConstraintSource == "$" ? ".,$," : ".,'" + mConstraintSource + "',") + ParserSTEP.LinkToString(mCreatingActor) + "," + (mDatabase.mSchema == Schema.IFC2x3 ? ParserSTEP.LinkToString(mSSCreationTime) : (mCreationTime == "$" ? "$" : "'" + mCreationTime + "'")) + (mUserDefinedGrade == "$" ? ",$" : ",'" + mUserDefinedGrade + "'"); }
	}
	//ENTITY IfcConstraintAggregationRelationship; // DEPRECEATED IFC4
	//ENTITY IfcConstraintclassificationRelationship; // DEPRECEATED IFC4
	//ENTITY IfcConstraintRelationship; // DEPRECEATED IFC4
	//ENTITY IfcConstructionResource
	public class IfcConstructionEquipmentResource : IfcConstructionResource
	{
		internal IfcConstructionEquipmentResourceTypeEnum mPredefinedType = IfcConstructionEquipmentResourceTypeEnum.NOTDEFINED;// OPTIONAL : IfcRoofTypeEnum; 
		public IfcConstructionEquipmentResourceTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcConstructionEquipmentResource() : base() { }
		internal IfcConstructionEquipmentResource(IfcConstructionEquipmentResource r) : base(r) { mPredefinedType = r.mPredefinedType; }
		internal IfcConstructionEquipmentResource(DatabaseIfc m) : base(m) { }
		internal static IfcConstructionEquipmentResource Parse(string strDef, Schema schema) { IfcConstructionEquipmentResource r = new IfcConstructionEquipmentResource(); int ipos = 0; parseFields(r, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return r; }
		internal static void parseFields(IfcConstructionEquipmentResource r, List<string> arrFields, ref int ipos, Schema schema)
		{
			IfcConstructionResource.parseFields(r, arrFields, ref ipos,schema);
			if (schema != Schema.IFC2x3)
			{
				string str = arrFields[ipos++];
				if (str[0] == '.')
					r.mPredefinedType = (IfcConstructionEquipmentResourceTypeEnum)Enum.Parse(typeof(IfcConstructionEquipmentResourceTypeEnum), str.Substring(1, str.Length - 2));
			}
		}
		protected override string BuildString() { return base.BuildString() + ",." + mPredefinedType.ToString() + "."; }
	}
	public class IfcConstructionEquipmentResourceType : IfcConstructionResourceType //IFC4
	{
		internal IfcConstructionEquipmentResourceTypeEnum mPredefinedType = IfcConstructionEquipmentResourceTypeEnum.NOTDEFINED;
		public IfcConstructionEquipmentResourceTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcConstructionEquipmentResourceType() : base() { }
		internal IfcConstructionEquipmentResourceType(IfcConstructionEquipmentResourceType t) : base(t) { mPredefinedType = t.mPredefinedType; }
		internal IfcConstructionEquipmentResourceType(DatabaseIfc m, string name, IfcConstructionEquipmentResourceTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
		internal static void parseFields(IfcConstructionEquipmentResourceType t, List<string> arrFields, ref int ipos) { IfcConstructionResourceType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcConstructionEquipmentResourceTypeEnum)Enum.Parse(typeof(IfcConstructionEquipmentResourceTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcConstructionEquipmentResourceType Parse(string strDef) { IfcConstructionEquipmentResourceType t = new IfcConstructionEquipmentResourceType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildString() { return base.BuildString() + ",." + mPredefinedType.ToString() + "."; }
	}
	public class IfcConstructionMaterialResource : IfcConstructionResource
	{
		internal IfcConstructionMaterialResourceTypeEnum mPredefinedType = IfcConstructionMaterialResourceTypeEnum.NOTDEFINED;// OPTIONAL : IfcRoofTypeEnum; 
		public IfcConstructionMaterialResourceTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcConstructionMaterialResource() : base() { }
		internal IfcConstructionMaterialResource(IfcConstructionMaterialResource o) : base(o) { mPredefinedType = o.mPredefinedType; }
		internal IfcConstructionMaterialResource(DatabaseIfc m) : base(m) { }
		internal static IfcConstructionMaterialResource Parse(string strDef, Schema schema) { IfcConstructionMaterialResource r = new IfcConstructionMaterialResource(); int ipos = 0; parseFields(r, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return r; }
		internal static void parseFields(IfcConstructionMaterialResource r, List<string> arrFields, ref int ipos, Schema schema)
		{
			IfcConstructionResource.parseFields(r, arrFields, ref ipos,schema);
			if (schema != Schema.IFC2x3)
			{
				string str = arrFields[ipos++];
				if (str[0] == '.')
					r.mPredefinedType = (IfcConstructionMaterialResourceTypeEnum)Enum.Parse(typeof(IfcConstructionMaterialResourceTypeEnum), str.Substring(1, str.Length - 2));
			}
		}
		protected override string BuildString() { return base.BuildString() + ",." + mPredefinedType.ToString() + "."; }
	}
	public class IfcConstructionMaterialResourceType : IfcConstructionResourceType //IFC4
	{
		internal IfcConstructionMaterialResourceTypeEnum mPredefinedType = IfcConstructionMaterialResourceTypeEnum.NOTDEFINED;
		public IfcConstructionMaterialResourceTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcConstructionMaterialResourceType() : base() { }
		internal IfcConstructionMaterialResourceType(IfcConstructionMaterialResourceType t) : base(t) { mPredefinedType = t.mPredefinedType; }
		internal IfcConstructionMaterialResourceType(DatabaseIfc m, string name, IfcConstructionMaterialResourceTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
		internal static void parseFields(IfcConstructionMaterialResourceType t, List<string> arrFields, ref int ipos) { IfcConstructionResourceType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcConstructionMaterialResourceTypeEnum)Enum.Parse(typeof(IfcConstructionMaterialResourceTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcConstructionMaterialResourceType Parse(string strDef) { IfcConstructionMaterialResourceType t = new IfcConstructionMaterialResourceType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildString() { return base.BuildString() + ",." + mPredefinedType.ToString() + "."; }
	}
	public class IfcConstructionProductResource : IfcConstructionResource
	{
		internal IfcConstructionProductResourceTypeEnum mPredefinedType = IfcConstructionProductResourceTypeEnum.NOTDEFINED;// OPTIONAL : IfcRoofTypeEnum; 
		public IfcConstructionProductResourceTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcConstructionProductResource() : base() { }
		internal IfcConstructionProductResource(IfcConstructionProductResource r) : base(r) { mPredefinedType = r.mPredefinedType; }
		internal IfcConstructionProductResource(DatabaseIfc m) : base(m) { }
		internal static IfcConstructionProductResource Parse(string strDef, Schema schema) { IfcConstructionProductResource r = new IfcConstructionProductResource(); int ipos = 0; parseFields(r, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return r; }
		internal static void parseFields(IfcConstructionProductResource r, List<string> arrFields, ref int ipos, Schema schema)
		{
			IfcConstructionResource.parseFields(r, arrFields, ref ipos,schema);
			if (schema != Schema.IFC2x3)
			{
				string str = arrFields[ipos++];
				if (str[0] == '.')
					r.mPredefinedType = (IfcConstructionProductResourceTypeEnum)Enum.Parse(typeof(IfcConstructionProductResourceTypeEnum), str.Substring(1, str.Length - 2));
			}
		}
		protected override string BuildString() { return base.BuildString() + ",." + mPredefinedType.ToString() + "."; }
	}
	internal class IfcConstructionProductResourceType : IfcConstructionResourceType //IFC4
	{
		internal IfcConstructionProductResourceTypeEnum mPredefinedType = IfcConstructionProductResourceTypeEnum.NOTDEFINED;
		public IfcConstructionProductResourceTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcConstructionProductResourceType() : base() { }
		internal IfcConstructionProductResourceType(IfcConstructionProductResourceType t) : base(t) { mPredefinedType = t.mPredefinedType; }
		internal IfcConstructionProductResourceType(DatabaseIfc m, string name, IfcConstructionProductResourceTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
		internal static void parseFields(IfcConstructionProductResourceType t, List<string> arrFields, ref int ipos) { IfcConstructionResourceType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcConstructionProductResourceTypeEnum)Enum.Parse(typeof(IfcConstructionProductResourceTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcConstructionProductResourceType Parse(string strDef) { IfcConstructionProductResourceType t = new IfcConstructionProductResourceType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildString() { return base.BuildString() + ",." + mPredefinedType.ToString() + "."; }
	}
	public abstract class IfcConstructionResource : IfcResource //ABSTRACT SUPERTYPE OF (ONEOF(IfcConstructionEquipmentResource, IfcConstructionMaterialResource, IfcConstructionProductResource, IfcCrewResource, IfcLaborResource, IfcSubContractResource))
	{
		internal int mUsage; //:	OPTIONAL IfcResourceTime; IFC4
		internal List<int> mBaseCosts = new List<int>();//	 :	OPTIONAL LIST [1:?] OF IfcAppliedValue; IFC4
		internal int mBaseQuantity;//	 :	OPTIONAL IfcPhysicalQuantity; IFC4 

		internal IfcResourceTime Usage { get { return mDatabase.mIfcObjects[mUsage] as IfcResourceTime; } set { mUsage = (value == null  ? 0 : value.mIndex); } }

		protected IfcConstructionResource() : base() { }
		protected IfcConstructionResource(IfcConstructionResource r) : base(r) { mUsage = r.mUsage; mBaseCosts.AddRange(r.mBaseCosts); mBaseQuantity = r.mBaseQuantity; }
		protected IfcConstructionResource(DatabaseIfc m) : base(m) { }
		protected IfcConstructionResource(DatabaseIfc m, IfcResourceTime usage, List<IfcAppliedValue> baseCosts, IfcPhysicalQuantity baseQuantity)
			: base(m) { if (usage != null) mUsage = usage.mIndex; if (baseCosts != null && baseCosts.Count > 0) mBaseCosts = baseCosts.ConvertAll(x => x.mIndex); if (baseQuantity != null) mBaseQuantity = baseQuantity.mIndex; }
		protected static void parseFields(IfcConstructionResource c, List<string> arrFields, ref int ipos, Schema schema)
		{
			IfcResource.parseFields(c, arrFields, ref ipos);
			if (schema != Schema.IFC2x3)
			{
				c.mUsage = ParserSTEP.ParseLink(arrFields[ipos++]);
				c.mBaseCosts = ParserSTEP.SplitListLinks(arrFields[ipos++]);
				c.mBaseQuantity = ParserSTEP.ParseLink(arrFields[ipos++]);
			}
		}
		protected override string BuildString() { return base.BuildString() + (mDatabase.mSchema == Schema.IFC2x3 ? "" : (mUsage == 0 ? ",$," : ",#" + mUsage + ",") + ParserSTEP.ListLinksToString(mBaseCosts) + (mBaseQuantity == 0 ? ",$" : ",#" + mBaseQuantity)); }
	}
	public abstract class IfcConstructionResourceType : IfcTypeResource //IFC4
	{
		internal List<int> mBaseCosts = new List<int>();//	 :	OPTIONAL LIST [1:?] OF IfcAppliedValue; 
		internal int mBaseQuantity;//	 :	OPTIONAL IfcPhysicalQuantity; 

		internal List<IfcAppliedValue> BaseCosts { get { return mBaseCosts.ConvertAll(x => mDatabase.mIfcObjects[x] as IfcAppliedValue); } set { mBaseCosts = (value == null ? new List<int>() : value.ConvertAll(x => x.mIndex)); } }
		internal IfcPhysicalQuantity BaseQuantity { get { return mDatabase.mIfcObjects[mBaseQuantity] as IfcPhysicalQuantity; } set { mBaseQuantity = (value == null ? 0 : value.mIndex); } }

		protected IfcConstructionResourceType() : base() { }
		protected IfcConstructionResourceType(IfcConstructionResourceType t) : base(t) { mBaseCosts.AddRange(t.mBaseCosts); mBaseQuantity = t.mBaseQuantity; }
		protected IfcConstructionResourceType(DatabaseIfc m) : base(m) { }
		internal static void parseFields(IfcConstructionResourceType t, List<string> arrFields, ref int ipos) { IfcTypeProcess.parseFields(t, arrFields, ref ipos); t.mBaseCosts = ParserSTEP.SplitListLinks(arrFields[ipos++]); t.mBaseQuantity = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildString() { return (mDatabase.mSchema == Schema.IFC2x3 ? "" : base.BuildString() + "," + ParserSTEP.ListLinksToString(mBaseCosts) + (mBaseQuantity == 0 ? ",$" : ",#" + mBaseQuantity)); }
	}
	public abstract partial class IfcContext : IfcObjectDefinition, IfcObjectDefinitionSelect//(IfcProject, IfcProjectLibrary)
	{
		internal string mObjectType = "$";//	 :	OPTIONAL IfcLabel;
		private string mLongName = "$";// : OPTIONAL IfcLabel;
		private string mPhase = "$";// : OPTIONAL IfcLabel;
		internal List<int> mRepresentationContexts = new List<int>();// : 	OPTIONAL SET [1:?] OF IfcRepresentationContext;
		private int mUnitsInContext;// : OPTIONAL IfcUnitAssignment; IFC2x3 not Optional
		//INVERSE
		internal List<IfcRelDefinesByProperties> mIsDefinedBy = new List<IfcRelDefinesByProperties>();
		internal List<IfcRelDeclares> mDeclares = new List<IfcRelDeclares>();

		public string ObjectType { get { return mObjectType == "$" ? "" : ParserIfc.Decode(mObjectType); } set { mObjectType = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value)); } }
		public string LongName { get { return (mLongName == "$" ? "" : ParserIfc.Decode(mLongName)); } set { mLongName = (string.IsNullOrEmpty(value) ? "" : ParserIfc.Encode(value.Replace("'", ""))); } }
		public string Phase { get { return (mPhase == "$" ? "" : ParserIfc.Decode(mPhase)); } set { mPhase = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }
		public List<IfcRepresentationContext> RepresentationContexts { get { return mRepresentationContexts.ConvertAll(x => mDatabase.mIfcObjects[x] as IfcRepresentationContext); } }
		public IfcUnitAssignment UnitsInContext { get { return mDatabase.mIfcObjects[mUnitsInContext] as IfcUnitAssignment; } set { mUnitsInContext = (value == null ? 0 : value.mIndex); } }

		public List<IfcRelDefinesByProperties> IsDefinedBy { get { return mIsDefinedBy; } }
		public List<IfcRelDeclares> Declares { get { return mDeclares; } }
	
		public DatabaseIfc Model { get { return mDatabase; } }

		protected IfcContext() : base() { }
		protected IfcContext(IfcContext o) : base(o) { mObjectType = o.mObjectType; mLongName = o.mLongName; mPhase = o.mPhase; mRepresentationContexts = new List<int>(o.mRepresentationContexts.ToArray()); mUnitsInContext = o.mUnitsInContext; }

		protected IfcContext(DatabaseIfc m, string name, IfcUnitAssignment.Length length) : base(m)
		{
			Name = name;
			if (m.mGeomRepContxt != null)
				mRepresentationContexts.Add(m.mGeomRepContxt.mIndex);
			IfcUnitAssignment u = new IfcUnitAssignment(m);
			u.SetUnits(length);
			UnitsInContext = u;
			mIsDecomposedBy.Clear(); //??? Jon
		}
		protected IfcContext(DatabaseIfc m, string name) : base(m)
		{
			Name = name;
			if (m.mGeomRepContxt != null)
				mRepresentationContexts.Add(m.mGeomRepContxt.mIndex);
			mIsDecomposedBy.Clear(); //??? Jon
		}
		internal static void parseFields(IfcContext p, List<string> arrFields, ref int ipos)
		{
			IfcObjectDefinition.parseFields(p, arrFields, ref ipos);
			p.mObjectType = arrFields[ipos++].Replace("'", "");
			p.mLongName = arrFields[ipos++].Replace("'", "");
			p.mPhase = arrFields[ipos++].Replace("'", "");
			string s = arrFields[ipos++];
			if (s != "$")
				p.mRepresentationContexts = ParserSTEP.SplitListLinks(s);
			p.mUnitsInContext = ParserSTEP.ParseLink(arrFields[ipos++]);

		}
		protected override string BuildString()
		{
			string str = base.BuildString() + (mObjectType == "$" ? ",$" : ",'" + mObjectType + "'") + (mLongName == "$" ? ",$" : ",'" + mLongName + "'") + (mPhase == "$" ? ",$" : ",'" + mPhase + "'");
			if (mRepresentationContexts.Count == 0)
				return str + ",$," + (mUnitsInContext == 0 ? "$" : ParserSTEP.LinkToString(mUnitsInContext));
			{
				str += ",(" + ParserSTEP.LinkToString(mRepresentationContexts[0]);
				for (int icounter = 1; icounter < mRepresentationContexts.Count; icounter++)
					str += "," + ParserSTEP.LinkToString(mRepresentationContexts[icounter]);
			}
			return str + ")," + (mUnitsInContext == 0 ? "$" : ParserSTEP.LinkToString(mUnitsInContext));
		}

		internal void setStructuralUnits()
		{
			IfcUnitAssignment ua = UnitsInContext;
			if (ua != null)
				ua.setStructuralUnits();
		}
		internal double setSIScale()
		{
			IfcUnitAssignment ua = UnitsInContext;
			if (ua != null)
				return ua.LengthScaleSI;
			return 1;
		}
		internal void initializeUnitsAndScales()
		{
			if (mRepresentationContexts.Count > 0)
			{
				for (int icounter = 0; icounter < mRepresentationContexts.Count; icounter++)
				{
					IfcGeometricRepresentationContext c = mDatabase.mIfcObjects[mRepresentationContexts[icounter]] as IfcGeometricRepresentationContext;
					if (c != null)
					{
						mDatabase.Tolerance = c.mPrecision;
						break;
					}
				}
			}
			setSIScale();
		}
		public void AddDeclared(IfcDefinitionSelect o)
		{
			if (mDeclares.Count == 0)
			{
				IfcRelDeclares d = new IfcRelDeclares(this, o);
			}
			else
				mDeclares[0].AddRelated(o);
		}
	}
	//ENTITY IfcContextDependentUnit, IfcResourceObjectSelect
	public abstract partial class IfcControl : IfcObject //ABSTRACT SUPERTYPE OF (ONEOF (IfcActionRequest ,IfcConditionCriterion ,IfcCostItem ,IfcCostSchedule,IfcEquipmentStandard ,IfcFurnitureStandard
	{ //  ,IfcPerformanceHistory ,IfcPermit ,IfcProjectOrder ,IfcProjectOrderRecord ,IfcScheduleTimeControl ,IfcServiceLife ,IfcSpaceProgram ,IfcTimeSeriesSchedule,IfcWorkControl))
		internal string mIdentification = "$"; // : OPTIONAL IfcIdentifier; IFC4
		//INVERSE
		internal List<IfcRelAssignsToControl> mControls = new List<IfcRelAssignsToControl>();/// : SET OF IfcRelAssignsToControl FOR RelatingControl;

		public string Identification { get { return (mIdentification == "$" ? "" : ParserIfc.Decode(mIdentification)); } set { mIdentification = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode(value.Replace("'", ""))); } }

		protected IfcControl() : base() { }
		protected IfcControl(IfcControl o) : base(o) { mIdentification = o.mIdentification; }
		protected IfcControl(DatabaseIfc m) : base(m)
		{
			if (mDatabase.mModelView != ModelView.Ifc4NotAssigned && mDatabase.mModelView != ModelView.If2x3NotAssigned)
				throw new Exception("Invalid Model View for IfcActor : " + m.ModelView.ToString());
		}
		protected static void parseFields(IfcControl c, List<string> arrFields, ref int ipos,Schema schema) { IfcObject.parseFields(c, arrFields, ref ipos); if (schema != Schema.IFC2x3) c.mIdentification = arrFields[ipos++].Replace("'", ""); }
		protected override string BuildString() { return base.BuildString() + (mDatabase.mSchema == Schema.IFC2x3 ? "" : (mIdentification == "$" ? ",$" : ",'" + mIdentification + "'")); }

		public void Assign(IfcObjectDefinition o) { if (mControls.Count == 0) mControls.Add(new IfcRelAssignsToControl(this, o)); else mControls[0].assign(o); }

	}
	public class IfcController : IfcDistributionControlElement //IFC4  
	{
		internal IfcControllerTypeEnum mPredefinedType = IfcControllerTypeEnum.NOTDEFINED;
		public IfcControllerTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcController(IfcController a) : base(a) { mPredefinedType = a.mPredefinedType; }
		internal IfcController() : base() { }
		internal IfcController(IfcProduct host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }
		internal static void parseFields(IfcController c, List<string> arrFields, ref int ipos) 
		{ 
			IfcDistributionControlElement.parseFields(c, arrFields, ref ipos);
			string s = arrFields[ipos++];
			if (s.StartsWith("."))
				c.mPredefinedType = (IfcControllerTypeEnum)Enum.Parse(typeof(IfcControllerTypeEnum), s.Replace(".", ""));
		}
		internal new static IfcController Parse(string strDef) { IfcController d = new IfcController(); int ipos = 0; parseFields(d, ParserSTEP.SplitLineFields(strDef), ref ipos); return d; }
		protected override string BuildString()
		{
			return base.BuildString() + (mDatabase.mSchema == Schema.IFC2x3 ? "" : (mPredefinedType == IfcControllerTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + "."));
		}
	}
	public class IfcControllerType : IfcDistributionControlElementType
	{
		internal IfcControllerTypeEnum mPredefinedType = IfcControllerTypeEnum.NOTDEFINED;// : IfcControllerTypeEnum;
		public IfcControllerTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcControllerType() : base() { }
		internal IfcControllerType(IfcControllerType t) : base(t) { mPredefinedType = t.mPredefinedType; }
		internal IfcControllerType(DatabaseIfc m, string name, IfcControllerTypeEnum t) : base(m) { Name = name; mPredefinedType = t; }
		internal static void parseFields(IfcControllerType t,List<string> arrFields, ref int ipos) { IfcDistributionControlElementType.parseFields(t,arrFields, ref ipos); t.mPredefinedType = (IfcControllerTypeEnum)Enum.Parse(typeof(IfcControllerTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcControllerType Parse(string strDef) { IfcControllerType t = new IfcControllerType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildString() { return base.BuildString() + ",." + mPredefinedType.ToString() + "."; }
	}
	public partial class IfcConversionBasedUnit : IfcNamedUnit, IfcResourceObjectSelect
	{
		private string mName = "";// : IfcLabel;
		private int mConversionFactor;// : IfcMeasureWithUnit; 
		//INVERSE
		internal List<IfcExternalReferenceRelationship> mHasExternalReferences = new List<IfcExternalReferenceRelationship>(); //IFC4
		internal List<IfcResourceConstraintRelationship> mHasConstraintRelationships = new List<IfcResourceConstraintRelationship>(); //gg
		public List<IfcExternalReferenceRelationship> HasExternalReferences { get { return mHasExternalReferences; } }
		public List<IfcResourceConstraintRelationship> HasConstraintRelationships { get { return mHasConstraintRelationships; } }

		public override string Name { get { return ParserIfc.Decode(mName); } set { mName = ParserIfc.Encode(value); } }
		internal IfcMeasureWithUnit ConversionFactor { get { return mDatabase.mIfcObjects[mConversionFactor] as IfcMeasureWithUnit; } }

		internal IfcConversionBasedUnit(IfcConversionBasedUnit el) : base(el) { mName = el.mName; mConversionFactor = el.mConversionFactor; }
		internal IfcConversionBasedUnit() : base() { }
		internal IfcConversionBasedUnit(IfcUnitEnum unit, string name, IfcMeasureWithUnit mu)
			: base(mu.mDatabase, unit, true) { Name = name.Replace("'", ""); mConversionFactor = mu.mIndex; }
		internal static IfcConversionBasedUnit Parse(string strDef) { IfcConversionBasedUnit u = new IfcConversionBasedUnit(); int ipos = 0; parseFields(u, ParserSTEP.SplitLineFields(strDef), ref ipos); return u; }
		internal static void parseFields(IfcConversionBasedUnit u, List<string> arrFields, ref int ipos) { IfcNamedUnit.parseFields(u, arrFields, ref ipos); u.mName = arrFields[ipos++].Replace("'", ""); u.mConversionFactor = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildString() { return base.BuildString() + ",'" + mName + "'," + ParserSTEP.LinkToString(mConversionFactor); }
		internal override double getSIFactor() { return ConversionFactor.getSIFactor(); }
	}
	public class IfcConversionBasedUnitWithOffset : IfcConversionBasedUnit //IFC4
	{
		internal double mConversionOffset = 0;//	 :	IfcReal
		internal IfcConversionBasedUnitWithOffset(IfcConversionBasedUnitWithOffset el) : base(el) { mConversionOffset = el.mConversionOffset; }
		internal IfcConversionBasedUnitWithOffset() : base() { }
		internal IfcConversionBasedUnitWithOffset(IfcUnitEnum unit, string name, IfcMeasureWithUnit mu, double offset)
			: base(unit, name, mu) { mConversionOffset = offset; }
		internal new static IfcConversionBasedUnitWithOffset Parse(string strDef) { IfcConversionBasedUnitWithOffset u = new IfcConversionBasedUnitWithOffset(); int ipos = 0; parseFields(u, ParserSTEP.SplitLineFields(strDef), ref ipos); return u; }
		internal static void parseFields(IfcConversionBasedUnitWithOffset u, List<string> arrFields, ref int ipos) { IfcConversionBasedUnit.parseFields(u, arrFields, ref ipos); u.mConversionOffset = ParserSTEP.ParseDouble(arrFields[ipos++]); }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.DoubleToString(mConversionOffset); }
	}
	public class IfcCooledBeam : IfcEnergyConversionDevice //IFC4
	{
		internal IfcCooledBeamTypeEnum mPredefinedType = IfcCooledBeamTypeEnum.NOTDEFINED;// OPTIONAL : IfcCooledBeamTypeEnum;
		public IfcCooledBeamTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcCooledBeam() : base() { }
		internal IfcCooledBeam(IfcCooledBeam b) : base(b) { mPredefinedType = b.mPredefinedType; }
		internal IfcCooledBeam(IfcProduct host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }

		internal static void parseFields(IfcCooledBeam s, List<string> arrFields, ref int ipos)
		{
			IfcEnergyConversionDevice.parseFields(s, arrFields, ref ipos);
			string str = arrFields[ipos++];
			if (str[0] == '.')
				s.mPredefinedType = (IfcCooledBeamTypeEnum)Enum.Parse(typeof(IfcCooledBeamTypeEnum), str);
		}
		internal new static IfcCooledBeam Parse(string strDef) { IfcCooledBeam s = new IfcCooledBeam(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildString()
		{
			return base.BuildString() + (mDatabase.mSchema == Schema.IFC2x3 ? "" : (mPredefinedType == IfcCooledBeamTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + "."));
		}
	}
	internal class IfcCooledBeamType : IfcEnergyConversionDeviceType
	{
		internal IfcCooledBeamTypeEnum mPredefinedType = IfcCooledBeamTypeEnum.NOTDEFINED;// : IfcCooledBeamTypeEnum
		public IfcCooledBeamTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcCooledBeamType() : base() { }
		internal IfcCooledBeamType(IfcCooledBeamType t) : base(t) { mPredefinedType = t.mPredefinedType; }
		internal IfcCooledBeamType(DatabaseIfc m, string name, IfcCooledBeamTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
		internal static void parseFields(IfcCooledBeamType t,List<string> arrFields, ref int ipos) { IfcEnergyConversionDeviceType.parseFields(t,arrFields, ref ipos); t.mPredefinedType = (IfcCooledBeamTypeEnum)Enum.Parse(typeof(IfcCooledBeamTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcCooledBeamType Parse(string strDef) { IfcCooledBeamType t = new IfcCooledBeamType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildString() { return base.BuildString() + ",." + mPredefinedType.ToString() + "."; }
	}
	public class IfcCoolingTower : IfcEnergyConversionDevice //IFC4
	{
		internal IfcCoolingTowerTypeEnum mPredefinedType = IfcCoolingTowerTypeEnum.NOTDEFINED;// OPTIONAL : IfcCoolingTowerTypeEnum;
		public IfcCoolingTowerTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcCoolingTower() : base() { }
		internal IfcCoolingTower(IfcCoolingTower t) : base(t) { mPredefinedType = t.mPredefinedType; }
		internal IfcCoolingTower(IfcProduct host, IfcObjectPlacement placement, IfcProductRepresentation representation, IfcDistributionSystem system) : base(host, placement, representation, system) { }

		internal static void parseFields(IfcCoolingTower s, List<string> arrFields, ref int ipos)
		{
			IfcEnergyConversionDevice.parseFields(s, arrFields, ref ipos);
			string str = arrFields[ipos++];
			if (str[0] == '.')
				s.mPredefinedType = (IfcCoolingTowerTypeEnum)Enum.Parse(typeof(IfcCoolingTowerTypeEnum), str);
		}
		internal new static IfcCoolingTower Parse(string strDef) { IfcCoolingTower s = new IfcCoolingTower(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildString()
		{
			return base.BuildString() + (mDatabase.mSchema == Schema.IFC2x3 ? "" : (mPredefinedType == IfcCoolingTowerTypeEnum.NOTDEFINED ? ",$" : ",." + mPredefinedType.ToString() + "."));
		}
	}
	public class IfcCoolingTowerType : IfcEnergyConversionDeviceType
	{
		internal IfcCoolingTowerTypeEnum mPredefinedType = IfcCoolingTowerTypeEnum.NOTDEFINED;// : IfcCoolingTowerTypeEnum
		public IfcCoolingTowerTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcCoolingTowerType() : base() { }
		internal IfcCoolingTowerType(IfcCoolingTowerType t) : base(t) { mPredefinedType = t.mPredefinedType; }
		internal IfcCoolingTowerType(DatabaseIfc m, string name, IfcCoolingTowerTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
		internal static void parseFields(IfcCoolingTowerType t, List<string> arrFields, ref int ipos) { IfcEnergyConversionDeviceType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcCoolingTowerTypeEnum)Enum.Parse(typeof(IfcCoolingTowerTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcCoolingTowerType Parse(string strDef) { IfcCoolingTowerType t = new IfcCoolingTowerType(); int ipos = 0; parseFields(t,ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildString() { return base.BuildString() + ",." + mPredefinedType.ToString() + "."; }
	}
	public class IfcCoordinatedUniversalTimeOffset : BaseClassIfc //DEPRECEATED IFC4
	{
		internal int mHourOffset;// : IfcHourInDay;
		internal int mMinuteOffset;// : OPTIONAL IfcMinuteInHour;
		internal IfcAheadOrBehind mSense = IfcAheadOrBehind.AHEAD;// : IfcAheadOrBehind; 
		internal IfcCoordinatedUniversalTimeOffset(IfcCoordinatedUniversalTimeOffset v) : base() { mHourOffset = v.mHourOffset; mMinuteOffset = v.mMinuteOffset; mSense = v.mSense; }
		internal IfcCoordinatedUniversalTimeOffset() : base() { }
		internal static void parseFields(IfcCoordinatedUniversalTimeOffset s,List<string> arrFields, ref int ipos) { s.mHourOffset = int.Parse(arrFields[ipos++]); s.mMinuteOffset = int.Parse(arrFields[ipos++]); s.mSense = (IfcAheadOrBehind)Enum.Parse(typeof(IfcAheadOrBehind),arrFields[ipos++].Replace(".",""));  }
		internal static IfcCoordinatedUniversalTimeOffset Parse(string strDef) { IfcCoordinatedUniversalTimeOffset t = new IfcCoordinatedUniversalTimeOffset(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildString() { return base.BuildString() + "," + mHourOffset + "," + mMinuteOffset + ",." + mSense.ToString() + "."; }
	}
	public abstract partial class IfcCoordinateOperation : BaseClassIfc // IFC4 	ABSTRACT SUPERTYPE OF(IfcMapConversion);
	{
		private int mSourceCRS;// :	IfcCoordinateReferenceSystemSelect;
		private int mTargetCRS;// :	IfcCoordinateReferenceSystem;

		public IfcCoordinateReferenceSystemSelect SourceCRS { get { return mDatabase.mIfcObjects[mSourceCRS] as IfcCoordinateReferenceSystemSelect; } }
		public IfcCoordinateReferenceSystem TargetCRS { get { return mDatabase.mIfcObjects[mTargetCRS] as IfcCoordinateReferenceSystem; } }

		protected IfcCoordinateOperation() : base() { }
		protected IfcCoordinateOperation(IfcCoordinateOperation p) : base() { mSourceCRS = p.mSourceCRS; mTargetCRS = p.mTargetCRS; }
		protected IfcCoordinateOperation(DatabaseIfc m, IfcCoordinateReferenceSystemSelect source, IfcCoordinateReferenceSystem target) : base(m) { mSourceCRS = source.Index; mTargetCRS = target.mIndex; }
		protected static void parseFields(IfcCoordinateOperation o, List<string> arrFields, ref int ipos)
		{
			o.mSourceCRS = ParserSTEP.ParseLink(arrFields[ipos++]);
			o.mTargetCRS = ParserSTEP.ParseLink(arrFields[ipos++]);
		}
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.LinkToString(mSourceCRS) + "," + ParserSTEP.LinkToString(mTargetCRS); }
		internal void Relate()
		{
			SourceCRS.HasCoordinateOperation = this;
		}
	}
	public abstract class IfcCoordinateReferenceSystem : BaseClassIfc, IfcCoordinateReferenceSystemSelect  // IFC4 	ABSTRACT SUPERTYPE OF(IfcProjectedCRS);
	{
		internal string mName = "$";//:	OPTIONAL IfcLabel;
		internal string mDescription = "$";//	:	OPTIONAL IfcText;
		internal string mGeodeticDatum; //	:	IfcIdentifier;
		internal string mVerticalDatum = "$";	//:	OPTIONAL IfcIdentifier;

		//INVERSE
		private IfcCoordinateOperation mHasCoordinateOperation = null;

		public override string Name { get { return (mName == "$" ? "" : ParserIfc.Decode(mName)); } set { if (!string.IsNullOrEmpty(value)) mName =  ParserIfc.Encode(value.Replace("'","")); } }
		public IfcCoordinateOperation HasCoordinateOperation { get { return mHasCoordinateOperation; } set { mHasCoordinateOperation = value; } }

		protected IfcCoordinateReferenceSystem() : base() { }
		protected IfcCoordinateReferenceSystem(IfcCoordinateReferenceSystem p) : base() { mName = p.mName; mDescription = p.mDescription; mGeodeticDatum = p.mGeodeticDatum; mVerticalDatum = p.mVerticalDatum; }
		protected IfcCoordinateReferenceSystem(DatabaseIfc m, string name, string desc, string geodeticDatum, string verticalDatum) : base(m)
		{
			if (!string.IsNullOrEmpty(name))
				mName = name.Replace("'", "");
			if (!string.IsNullOrEmpty(desc))
				mDescription = desc.Replace("'", "");
			mGeodeticDatum = geodeticDatum;
			if (!string.IsNullOrEmpty(verticalDatum))
				mVerticalDatum = verticalDatum;
		}
		protected static void parseFields(IfcCoordinateReferenceSystem o, List<string> arrFields, ref int ipos)
		{
			o.mName = arrFields[ipos++];
			o.mDescription = arrFields[ipos++];
			o.mGeodeticDatum = arrFields[ipos++];
			o.mVerticalDatum = arrFields[ipos++];
		}
		protected override string BuildString()
		{
			return base.BuildString() + (mName == "$" ? ",$," : ",'" + mName + "',") + (mDescription == "$" ? "$,'" : "'" + mDescription + "','") +
				mGeodeticDatum + (mVerticalDatum == "$" ? "',$" : "','" + mVerticalDatum + "'");
		}
	}
	public interface IfcCoordinateReferenceSystemSelect : IfcInterface { IfcCoordinateOperation HasCoordinateOperation { get; set; } } // IfcCoordinateReferenceSystem, IfcGeometricRepresentationContext
	public class IfcCostItem : IfcControl
	{
		internal IfcCostItemTypeEnum mPredefinedType = IfcCostItemTypeEnum.NOTDEFINED; // IFC4
		internal List<int> mCostValues = new List<int>();//	 : OPTIONAL LIST [1:?] OF IfcCostValue; IFC4
		internal List<int> mCostQuantities = new List<int>();//	 : OPTIONAL LIST [1:?] OF IfcPhysicalQuantity; IFC4

		public IfcCostItemTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcCostItem() : base() { } 
		internal IfcCostItem(IfcCostItem i) : base(i) { }
		internal IfcCostItem( IfcCostSchedule s, List<IfcCostValue> values, List<IfcPhysicalQuantity> quants)
			: this(s.mDatabase, values, quants) { s.AddAggregated(this); }
		internal IfcCostItem(IfcCostItem i, List<IfcCostValue> values, List<IfcPhysicalQuantity> quants)
			: this(i.mDatabase, values, quants) { i.AddNested(this); }
		internal IfcCostItem(DatabaseIfc m, List<IfcCostValue> values, List<IfcPhysicalQuantity> quants) : base(m)
		{ 
			if (values != null && values.Count > 0)
				mCostValues = values.ConvertAll(x => x.mIndex);
			if (quants != null && quants.Count > 0)
				mCostQuantities = quants.ConvertAll(x => x.mIndex);
		}
		internal static IfcCostItem Parse(string strDef, Schema schema) { IfcCostItem c = new IfcCostItem(); int ipos = 0; parseFields(c, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return c; }
		internal static void parseFields(IfcCostItem c, List<string> arrFields, ref int ipos, Schema schema)  
		{ 
			IfcControl.parseFields(c, arrFields, ref ipos,schema);
			if (schema != Schema.IFC2x3)
			{
				string s = arrFields[ipos++];
				if (s.StartsWith("."))
					c.mPredefinedType = (IfcCostItemTypeEnum)Enum.Parse(typeof(IfcCostItemTypeEnum), s.Replace(".", ""));
				s = arrFields[ipos++];
				if(s != "$")
					c.mCostValues = ParserSTEP.SplitListLinks(s);
				s = arrFields[ipos++];
				if(s != "$")
					c.mCostQuantities = ParserSTEP.SplitListLinks(s);
			}
		}
		protected override string BuildString()
		{
			string s = base.BuildString();
			if (mDatabase.mSchema != Schema.IFC2x3)
			{
				s += ",." + mPredefinedType.ToString();
				if (mCostValues.Count == 0)
					s += ".,$,";
				else
				{
					s += ".,(" + ParserSTEP.LinkToString(mCostValues[0]);
					for (int icounter = 1; icounter < mCostValues.Count; icounter++)
						s += "," + ParserSTEP.LinkToString(mCostValues[icounter]);
					s += "),";
				}
				if (mCostQuantities.Count == 0)
					s += "$";
				else
				{
					s += "(" + ParserSTEP.LinkToString(mCostQuantities[0]);
					for (int icounter = 1; icounter < mCostQuantities.Count; icounter++)
						s += "," + ParserSTEP.LinkToString(mCostQuantities[icounter]);
					s += ")";
				}
			}
			return s;
		}
	}
	public class IfcCostSchedule : IfcControl
	{
		internal IfcCostScheduleTypeEnum mPredefinedType = IfcCostScheduleTypeEnum.NOTDEFINED;// :	OPTIONAL IfcCostScheduleTypeEnum; IFC4 relocated
		internal string mStatus = "$";// : OPTIONAL IfcLabel; IFC4 relocated
		internal string mSubmittedOn = "$";// : OPTIONAL IfcDateTime; IFC4 relocated  : 	OPTIONAL IfcDateTimeSelect in control
		internal string mUpdateDate = "$";// : OPTIONAL IfcDateTime; IFC4 relocated 
		private int mSubmittedBy;// : OPTIONAL IfcActorSelect; IFC4 DELETED 
		private int mPreparedBy;// : OPTIONAL IfcActorSelect; IFC4 DELETED 
		private List< int> mTargetUsers = new List<int>();// : OPTIONAL SET [1:?] OF IfcActorSelect; //IFC4 DELETED 
		//internal string mID;// : IfcIdentifier; IFC4 relocated 
		public IfcCostScheduleTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		public string Staus { get { return (mStatus == "$" ? "" : ParserIfc.Decode( mStatus)); } set { mStatus = (string.IsNullOrEmpty(value) ? "$" : ParserIfc.Encode( value.Replace("'",""))); } }

		internal IfcCostSchedule() : base() { }
		internal IfcCostSchedule(IfcCostSchedule s) : base(s)
		{
			mSubmittedBy = s.mSubmittedBy;
			mPreparedBy = s.mPreparedBy;
			mSubmittedBy = s.mSubmittedBy;
			mStatus = s.mStatus;
			mTargetUsers = new List<int>( s.mTargetUsers.ToArray());
			mUpdateDate = s.mUpdateDate; 
			mPredefinedType = s.mPredefinedType;
		}
		internal IfcCostSchedule(DatabaseIfc m, IfcCostScheduleTypeEnum t, string status, DateTime submitted, IfcProject prj)
			: base(m) 
		{
			mPredefinedType = t;
			if (!string.IsNullOrEmpty(status)) 
				mStatus = status.Replace("'", "");
			if (submitted != DateTime.MinValue)
				mSubmittedOn = (m.mSchema == Schema.IFC2x3 ? "#" + new IfcDateAndTime(new IfcCalendarDate(m,submitted.Day,submitted.Month,submitted.Year),new IfcLocalTime(m,submitted.Hour,submitted.Minute,submitted.Second)).mIndex : IfcDateTime.Convert(  submitted));// IfcDate.convert(submitted); 
			mUpdateDate = IfcDate.convert(DateTime.Now); 
			if (prj != null) 
				prj.AddDeclared(this);
		}
		internal static IfcCostSchedule Parse(string strDef) { IfcCostSchedule c = new IfcCostSchedule(); int ipos = 0; parseFields(c, ParserSTEP.SplitLineFields(strDef), ref ipos); return c; }
		internal static void parseFields(IfcCostSchedule c,List<string> arrFields, ref int ipos,Schema schema)
		{  
			IfcControl.parseFields(c,arrFields, ref ipos,schema);
			if(schema == Schema.IFC2x3)
			{
				c.mSubmittedBy = ParserSTEP.ParseLink(arrFields[ipos++]); 
				c.mPreparedBy = ParserSTEP.ParseLink(arrFields[ipos++]);
				c.mSubmittedOn = arrFields[ipos++]; 
				c.mStatus = arrFields[ipos++].Replace("'",""); 
				c.mTargetUsers = ParserSTEP.SplitListLinks(arrFields[ipos++]); 
				c.mUpdateDate = arrFields[ipos++]; 
				c.mIdentification = arrFields[ipos++].Replace("'", "");
				c.mPredefinedType = (IfcCostScheduleTypeEnum)Enum.Parse(typeof(IfcCostScheduleTypeEnum), arrFields[ipos++].Replace(".", ""));
			}
			else
			{
				c.mPredefinedType = (IfcCostScheduleTypeEnum)Enum.Parse(typeof(IfcCostScheduleTypeEnum), arrFields[ipos++].Replace(".", ""));
				c.mStatus = arrFields[ipos++].Replace("'", "");
				c.mSubmittedOn = arrFields[ipos++];
				c.mUpdateDate = arrFields[ipos++]; 
			}
		}
		protected override string BuildString()
		{
			if (mDatabase.mSchema == Schema.IFC2x3)
			{
				string str = base.BuildString() + "," + ParserSTEP.LinkToString(mSubmittedBy) + "," + ParserSTEP.LinkToString(mPreparedBy) + "," + mSubmittedOn + "," + mStatus;
				if (mTargetUsers.Count > 0)
				{
					str += ",(" + ParserSTEP.LinkToString(mTargetUsers[0]);
					for (int icounter = 1; icounter < mTargetUsers.Count; icounter++)
						str += "," + ParserSTEP.LinkToString(mTargetUsers[icounter]);
					str += "),";
				}
				else
					str += ",$,";
				return str + mUpdateDate + (mIdentification == "$" ? ",$,." : ",'" + mIdentification + "',.") + mPredefinedType.ToString() + ".";
			}
			return base.BuildString() + ",." + mPredefinedType.ToString() + (mStatus == "$" ? ".,$," : ".,'" + mStatus + "',") + mSubmittedOn + "," + mUpdateDate;
		}
	}
	public partial class IfcCostValue : IfcAppliedValue
	{
		//internal string mCostType;// : IfcLabel;  IFC4 renamed to category
		//internal string mCondition = "$";//  : OPTIONAL IfcText; IFC4 moved to condition
		internal IfcCostValue() : base() { }
		internal IfcCostValue(IfcCostValue o) : base(o) { }
		internal new static IfcCostValue Parse(string strDef, Schema schema) { IfcCostValue v = new IfcCostValue(); int ipos = 0; parseFields(v, ParserSTEP.SplitLineFields(strDef), ref ipos, schema); return v; }
		internal static void parseFields(IfcCostValue v, List<string> arrFields, ref int ipos, Schema schema) { IfcAppliedValue.parseFields(v, arrFields, ref ipos, schema); if (schema == Schema.IFC2x3) { v.mCategory = arrFields[ipos++].Replace("'", ""); v.mCondition = arrFields[ipos++].Replace("'", ""); } }
		protected override string BuildString() { return base.BuildString() + (mDatabase.mSchema == Schema.IFC2x3 ? (mCategory == "$" ? ",$," : ",'" + mCategory + "',") + (mCondition == "$" ? "$" : "'" + mCondition + "'") : ""); }
	}
	public partial class IfcCovering : IfcBuildingElement
	{
		internal IfcCoveringTypeEnum mPredefinedType = IfcCoveringTypeEnum.NOTDEFINED;// : OPTIONAL IfcCoveringTypeEnum;
		//INVERSE
		internal IfcRelCoversSpaces mCoversSpaces = null;//	 : 	SET [0:1] OF IfcRelCoversSpaces FOR RelatedCoverings;
		internal IfcRelCoversBldgElements mCoversElements = null;//	 : 	SET [0:1] OF IfcRelCoversBldgElements FOR RelatedCoverings;

		public IfcCoveringTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		
		internal IfcCovering() : base() { }
		internal IfcCovering(IfcCovering o) : base(o) { mPredefinedType = o.mPredefinedType; }
		public IfcCovering(IfcProduct host, IfcObjectPlacement p, IfcProductRepresentation r) : base(host, p, r) { }
		 
		internal static IfcCovering Parse(string strDef) { IfcCovering c = new IfcCovering(); int ipos = 0; parseFields(c, ParserSTEP.SplitLineFields(strDef), ref ipos); return c; }
		internal static void parseFields(IfcCovering c, List<string> arrFields, ref int ipos)
		{
			IfcBuildingElement.parseFields(c, arrFields, ref ipos);
			string str = arrFields[ipos++];
			if (str != "$")
				c.mPredefinedType = (IfcCoveringTypeEnum)Enum.Parse(typeof(IfcCoveringTypeEnum), str.Replace(".", ""));
		}
		protected override string BuildString() { return base.BuildString() + "," + (mPredefinedType == IfcCoveringTypeEnum.NOTDEFINED ? "$" : "." + mPredefinedType.ToString() + "."); }
	}
	public partial class IfcCoveringType : IfcBuildingElementType
	{
		internal IfcCoveringTypeEnum mPredefinedType = IfcCoveringTypeEnum.NOTDEFINED;
		public IfcCoveringTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }

		internal IfcCoveringType() : base() { }
		internal IfcCoveringType(IfcCoveringType t) : base(t) { mPredefinedType = t.mPredefinedType; }
		public IfcCoveringType(DatabaseIfc m, string name, IfcCoveringTypeEnum type) : base(m) { Name = name; mPredefinedType = type; } 

		internal static void parseFields(IfcCoveringType e, List<string> arrFields, ref int ipos) { IfcBuildingElementType.parseFields(e, arrFields, ref ipos); try { e.mPredefinedType = (IfcCoveringTypeEnum)Enum.Parse(typeof(IfcCoveringTypeEnum), arrFields[ipos++].Replace(".", "")); } catch (Exception) { } }
		internal new static IfcCoveringType Parse(string strDef) { IfcCoveringType t = new IfcCoveringType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildString() { return base.BuildString() + ",." + mPredefinedType.ToString() + "."; }
	}
	public class IfcCraneRailAShapeProfileDef : IfcParameterizedProfileDef
	{
		internal double mOverallHeight, mBaseWidth2;// : IfcPositiveLengthMeasure;
		internal double mRadius;// : OPTIONAL IfcPositiveLengthMeasure;
		internal double mHeadWidth, mHeadDepth2, mHeadDepth3, mWebThickness, mBaseWidth4, mBaseDepth1, mBaseDepth2, mBaseDepth3;// : IfcPositiveLengthMeasure;
		internal double mCentreOfGravityInY;// : OPTIONAL IfcPositiveLengthMeasure; 
		internal IfcCraneRailAShapeProfileDef() : base() { }
		internal IfcCraneRailAShapeProfileDef(IfcCraneRailAShapeProfileDef i)
			: base(i)
		{
			mOverallHeight = i.mOverallHeight; mBaseWidth2 = i.mBaseWidth2; mRadius = i.mRadius; mHeadWidth = i.mHeadWidth; mHeadDepth2 = i.mHeadDepth2;
			mHeadDepth3 = i.mHeadDepth3; mWebThickness = i.mWebThickness; mBaseWidth4 = i.mBaseWidth4; mBaseDepth1 = i.mBaseDepth1;
			mBaseDepth2 = i.mBaseDepth2; mBaseDepth3 = i.mBaseDepth3; mCentreOfGravityInY = i.mCentreOfGravityInY;
		}
		internal static void parseFields(IfcCraneRailAShapeProfileDef p, List<string> arrFields, ref int ipos)
		{
			IfcParameterizedProfileDef.parseFields(p, arrFields, ref ipos); p.mOverallHeight = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mBaseWidth2 = ParserSTEP.ParseDouble(arrFields[ipos++]); p.mRadius = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mHeadWidth = ParserSTEP.ParseDouble(arrFields[ipos++]); p.mHeadDepth2 = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mHeadDepth3 = ParserSTEP.ParseDouble(arrFields[ipos++]); p.mWebThickness = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mBaseWidth4 = ParserSTEP.ParseDouble(arrFields[ipos++]); p.mBaseDepth1 = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mBaseDepth2 = ParserSTEP.ParseDouble(arrFields[ipos++]); p.mBaseDepth3 = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mCentreOfGravityInY = ParserSTEP.ParseDouble(arrFields[ipos++]);
		}
		internal new static IfcCraneRailAShapeProfileDef Parse(string strDef) { IfcCraneRailAShapeProfileDef p = new IfcCraneRailAShapeProfileDef(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos); return p; }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.DoubleToString(mOverallHeight) + "," + ParserSTEP.DoubleToString(mBaseWidth2) + "," + ParserSTEP.DoubleOptionalToString(mRadius) + "," + ParserSTEP.DoubleToString(mHeadWidth) + "," + ParserSTEP.DoubleToString(mHeadDepth2) + "," + ParserSTEP.DoubleToString(mHeadDepth3) + "," + ParserSTEP.DoubleToString(mWebThickness) + "," + ParserSTEP.DoubleToString(mBaseDepth1) + "," + ParserSTEP.DoubleToString(mBaseDepth2) + "," + ParserSTEP.DoubleToString(mBaseDepth3) + "," + ParserSTEP.DoubleOptionalToString(mCentreOfGravityInY); }
	}
	public class IfcCraneRailFShapeProfileDef : IfcParameterizedProfileDef
	{
		internal double mOverallHeight, mHeadWidth;// : IfcPositiveLengthMeasure;
		internal double mRadius;// : OPTIONAL IfcPositiveLengthMeasure;
		internal double mHeadDepth2, mHeadDepth3, mWebThickness, mBaseDepth1, mBaseDepth2;// : IfcPositiveLengthMeasure;
		internal double mCentreOfGravityInY;// : OPTIONAL IfcPositiveLengthMeasure; 
		internal IfcCraneRailFShapeProfileDef() : base() { }
		internal IfcCraneRailFShapeProfileDef(IfcCraneRailFShapeProfileDef i)
			: base(i)
		{
			mOverallHeight = i.mOverallHeight; mHeadWidth = i.mHeadWidth; mRadius = i.mRadius; mHeadDepth2 = i.mHeadDepth2; mHeadDepth3 = i.mHeadDepth3;
			mWebThickness = i.mWebThickness; mBaseDepth1 = i.mBaseDepth1; mBaseDepth2 = i.mBaseDepth2; mCentreOfGravityInY = i.mCentreOfGravityInY;
		}
		internal static void parseFields(IfcCraneRailFShapeProfileDef p, List<string> arrFields, ref int ipos)
		{
			IfcParameterizedProfileDef.parseFields(p, arrFields, ref ipos); p.mOverallHeight = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mHeadWidth = ParserSTEP.ParseDouble(arrFields[ipos++]); p.mRadius = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mHeadDepth2 = ParserSTEP.ParseDouble(arrFields[ipos++]); p.mHeadDepth3 = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mWebThickness = ParserSTEP.ParseDouble(arrFields[ipos++]); p.mBaseDepth1 = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mBaseDepth2 = ParserSTEP.ParseDouble(arrFields[ipos++]); p.mCentreOfGravityInY = ParserSTEP.ParseDouble(arrFields[ipos++]);
		}
		internal new static IfcCraneRailFShapeProfileDef Parse(string strDef) { IfcCraneRailFShapeProfileDef p = new IfcCraneRailFShapeProfileDef(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos); return p; }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.DoubleToString(mOverallHeight) + "," + ParserSTEP.DoubleToString(mHeadWidth) + "," + ParserSTEP.DoubleOptionalToString(mRadius) + "," + ParserSTEP.DoubleToString(mHeadDepth2) + "," + ParserSTEP.DoubleToString(mHeadDepth3) + "," + ParserSTEP.DoubleToString(mWebThickness) + "," + ParserSTEP.DoubleToString(mBaseDepth1) + "," + ParserSTEP.DoubleToString(mBaseDepth2) + "," + ParserSTEP.DoubleOptionalToString(mCentreOfGravityInY); }
	} 
	public class IfcCrewResource : IfcConstructionResource
	{
		internal IfcCrewResourceTypeEnum mPredefinedType = IfcCrewResourceTypeEnum.NOTDEFINED;// OPTIONAL : IfcCrewResourceTypeEnum; 
		public IfcCrewResourceTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcCrewResource() : base() { }
		internal IfcCrewResource(IfcCrewResource o) : base(o) { mPredefinedType = o.mPredefinedType; }
		internal IfcCrewResource(DatabaseIfc m) : base(m) { }
		internal static IfcCrewResource Parse(string strDef, Schema schema) { IfcCrewResource r = new IfcCrewResource(); int ipos = 0; parseFields(r, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return r; }
		internal static void parseFields(IfcCrewResource r, List<string> arrFields, ref int ipos,Schema schema)
		{
			IfcConstructionResource.parseFields(r, arrFields, ref ipos,schema);
			if (schema != Schema.IFC2x3)
			{
				string str = arrFields[ipos++];
				if (str[0] == '.')
					r.mPredefinedType = (IfcCrewResourceTypeEnum)Enum.Parse(typeof(IfcCrewResourceTypeEnum), str.Substring(1, str.Length - 2));
			}
		}
		protected override string BuildString() { return base.BuildString() + ",." + mPredefinedType.ToString() + "."; }
	}
	public class IfcCrewResourceType : IfcConstructionResourceType //IFC4
	{
		internal IfcCrewResourceTypeEnum mPredefinedType = IfcCrewResourceTypeEnum.NOTDEFINED;
		public IfcCrewResourceTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcCrewResourceType() : base() { }
		internal IfcCrewResourceType(IfcCrewResourceType t) : base(t) { mPredefinedType = t.mPredefinedType; }
		internal IfcCrewResourceType(DatabaseIfc m, string name, IfcCrewResourceTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
		internal static void parseFields(IfcCrewResourceType t, List<string> arrFields, ref int ipos) { IfcCrewResourceType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcCrewResourceTypeEnum)Enum.Parse(typeof(IfcCrewResourceTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcCrewResourceType Parse(string strDef) { IfcCrewResourceType t = new IfcCrewResourceType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildString() { return base.BuildString() + ",." + mPredefinedType.ToString() + "."; }
	}
	public abstract partial class IfcCsgPrimitive3D : IfcGeometricRepresentationItem, IfcBooleanOperand, IfcCsgSelect /*ABSTRACT SUPERTYPE OF (ONEOF (IfcBlock ,IfcRectangularPyramid ,IfcRightCircularCone ,IfcRightCircularCylinder ,IfcSphere))*/
	{
		private int mPosition;// : IfcAxis2Placement3D;

		internal IfcAxis2Placement3D Position { get { return mDatabase.mIfcObjects[mPosition] as IfcAxis2Placement3D; } }

		protected IfcCsgPrimitive3D() : base() { }
		protected IfcCsgPrimitive3D(IfcCsgPrimitive3D p) : base(p) { mPosition = p.mPosition; }
		
		protected static void parseFields(IfcCsgPrimitive3D g, List<string> arrFields, ref int ipos) { IfcGeometricRepresentationItem.parseFields(g, arrFields, ref ipos); g.mPosition = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.LinkToString(mPosition); }
	}
	public partial interface IfcCsgSelect : IfcInterface { } //	IfcBooleanResult, IfcCsgPrimitive3D
	public partial class IfcCsgSolid : IfcSolidModel
	{
		private int mTreeRootExpression;// : IfcCsgSelect

		internal IfcCsgSelect TreeRootExpression { get { return mDatabase.mIfcObjects[mTreeRootExpression] as IfcCsgSelect; } set { mTreeRootExpression = value.Index; } }

		internal IfcCsgSolid() : base() { }
		internal IfcCsgSolid(IfcCsgSolid p) : base(p) { mTreeRootExpression = p.mTreeRootExpression; }
		public IfcCsgSolid(IfcCsgSelect csg)
			: base(csg.Database)
		{
			if (mDatabase.mModelView != ModelView.Ifc4NotAssigned && mDatabase.mModelView != ModelView.If2x3NotAssigned && mDatabase.mModelView != ModelView.Ifc4DesignTransfer)
				throw new Exception("Invalid Model View for IfcCsgSolid : " + mDatabase.ModelView.ToString());
			TreeRootExpression = csg;
		}
		internal static IfcCsgSolid Parse(string strDef) { IfcCsgSolid s = new IfcCsgSolid(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		internal static void parseFields(IfcCsgSolid s, List<string> arrFields, ref int ipos) { IfcSolidModel.parseFields(s, arrFields, ref ipos); s.mTreeRootExpression = ParserSTEP.ParseLink(arrFields[ipos++]); }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.LinkToString(mTreeRootExpression); }
	}
	public partial class IfcCShapeProfileDef : IfcParameterizedProfileDef
	{
		internal double mDepth, mWidth, mWallThickness, mGirth;// : IfcPositiveLengthMeasure;
		internal double mInternalFilletRadius;// : OPTIONAL IfcPositiveLengthMeasure;
		//internal double mCentreOfGravityInX;// : OPTIONAL IfcPositiveLengthMeasure // DELETED IFC4 	Superseded by respective attribute of IfcStructuralProfileProperties 
		internal IfcCShapeProfileDef() : base() { }
		internal IfcCShapeProfileDef(IfcCShapeProfileDef c)
			: base(c)
		{
			mDepth = c.mDepth;
			mWidth = c.mWidth;
			mWallThickness = c.mWallThickness;
			mGirth = c.mGirth;
			mInternalFilletRadius = c.mInternalFilletRadius;
		}
		public IfcCShapeProfileDef(DatabaseIfc m, string name, double depth, double width, double wallThickness, double girth)
			: base(m) { Name = name; mDepth = depth; mWidth = width; mWallThickness = wallThickness; mGirth = girth; }
		
		internal static void parseFields(IfcCShapeProfileDef p, List<string> arrFields, ref int ipos, Schema schema)
		{
			IfcParameterizedProfileDef.parseFields(p, arrFields, ref ipos);
			p.mDepth = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mWidth = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mWallThickness = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mGirth = ParserSTEP.ParseDouble(arrFields[ipos++]);
			p.mInternalFilletRadius = ParserSTEP.ParseDouble(arrFields[ipos++]);
			if (schema == Schema.IFC2x3)
				ipos++;
		}
		internal static IfcCShapeProfileDef Parse(string strDef, Schema schema) { IfcCShapeProfileDef p = new IfcCShapeProfileDef(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos, schema); return p; }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.DoubleToString(mDepth) + "," + ParserSTEP.DoubleToString(mWidth) + "," + ParserSTEP.DoubleToString(mWallThickness) + "," + ParserSTEP.DoubleToString(mGirth) + "," + ParserSTEP.DoubleOptionalToString(mInternalFilletRadius) + (mDatabase.mSchema == Schema.IFC2x3 ? ",$" : ""); }

	}
	//ENTITY IfcCurrencyRelationship; 
	public partial class IfcCurtainWall : IfcBuildingElement
	{
		internal IfcCurtainWallTypeEnum mPredefinedType = IfcCurtainWallTypeEnum.NOTDEFINED;//: OPTIONAL IfcCurtainWallTypeEnum; 
		public IfcCurtainWallTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcCurtainWall() : base() { }
		internal IfcCurtainWall(IfcCurtainWall w) : base(w) { mPredefinedType = w.mPredefinedType; }
		public IfcCurtainWall(IfcProduct host, IfcObjectPlacement p, IfcProductRepresentation r) : base(host, p, r) { }
		 
		internal static IfcCurtainWall Parse(string strDef, Schema schema) { IfcCurtainWall w = new IfcCurtainWall(); int ipos = 0; parseFields(w, ParserSTEP.SplitLineFields(strDef), ref ipos,schema); return w; }
		internal static void parseFields(IfcCurtainWall w, List<string> arrFields, ref int ipos, Schema schema)
		{
			IfcBuildingElement.parseFields(w, arrFields, ref ipos);
			if (schema != Schema.IFC2x3)
			{
				string str = arrFields[ipos++];
				if (str[0] == '.')
					w.mPredefinedType = (IfcCurtainWallTypeEnum)Enum.Parse(typeof(IfcCurtainWallTypeEnum), str.Substring(1, str.Length - 2));
			}
		}
		protected override string BuildString() { return (mDatabase.mSchema == Schema.IFC2x3 ? base.BuildString() : base.BuildString() + ",." + mPredefinedType.ToString() + "."); }
	}
	public partial class IfcCurtainWallType : IfcBuildingElementType
	{
		internal IfcCurtainWallTypeEnum mPredefinedType = IfcCurtainWallTypeEnum.NOTDEFINED;
		public IfcCurtainWallTypeEnum PredefinedType { get { return mPredefinedType; } set { mPredefinedType = value; } }
		internal IfcCurtainWallType() : base() { }
		internal IfcCurtainWallType(IfcCurtainWallType t) : base(t) { mPredefinedType = t.mPredefinedType; }
		public IfcCurtainWallType(DatabaseIfc m, string name, IfcCurtainWallTypeEnum type) : base(m) { Name = name; mPredefinedType = type; }
		internal static void parseFields(IfcCurtainWallType t, List<string> arrFields, ref int ipos) { IfcBuildingElementType.parseFields(t, arrFields, ref ipos); t.mPredefinedType = (IfcCurtainWallTypeEnum)Enum.Parse(typeof(IfcCurtainWallTypeEnum), arrFields[ipos++].Replace(".", "")); }
		internal new static IfcCurtainWallType Parse(string strDef) { IfcCurtainWallType t = new IfcCurtainWallType(); int ipos = 0; parseFields(t, ParserSTEP.SplitLineFields(strDef), ref ipos); return t; }
		protected override string BuildString() { return base.BuildString() + ",." + mPredefinedType.ToString() + "."; }
	}
	public abstract partial class IfcCurve : IfcGeometricRepresentationItem, IfcGeometricSetSelect /*ABSTRACT SUPERTYPE OF (ONEOF (IfcBoundedCurve ,IfcConic ,IfcLine ,IfcOffsetCurve2D ,IfcOffsetCurve3D,IfcPcurve,IfcClothoid))*/
	{   //INVERSE GeomGym
		internal IfcEdgeCurve mEdge = null;

		protected IfcCurve() : base() { }
		protected IfcCurve(IfcCurve c) : base(c) { }
		protected IfcCurve(DatabaseIfc db) : base(db) { }
		protected static void parseFields(IfcCurve c, List<string> arrFields, ref int ipos) { IfcGeometricRepresentationItem.parseFields(c, arrFields, ref ipos); }
	}
	public partial class IfcCurveBoundedPlane : IfcBoundedSurface
	{
		internal int mBasisSurface;// : IfcPlane;
		internal int mOuterBoundary;// : IfcCurve;
		internal List<int> mInnerBoundaries = new List<int>();//: SET OF IfcCurve;

		internal IfcPlane BasisSurface { get { return mDatabase.mIfcObjects[mBasisSurface] as IfcPlane; } }
		internal IfcCurve OuterBoundary { get { return mDatabase.mIfcObjects[mOuterBoundary] as IfcCurve; } }
		internal List<IfcCurve> InnerBoundaries { get { return mInnerBoundaries.ConvertAll(x => mDatabase.mIfcObjects[x] as IfcCurve); } }

		internal IfcCurveBoundedPlane() : base() { }
		internal IfcCurveBoundedPlane(IfcCurveBoundedPlane p) : base(p) { mBasisSurface = p.mBasisSurface; mOuterBoundary = p.mOuterBoundary; mInnerBoundaries = new List<int>(p.mInnerBoundaries.ToArray()); }
		internal IfcCurveBoundedPlane(DatabaseIfc m, IfcPlane p, IfcCurve outer, List<IfcCurve> inner)
			: base(m) { mBasisSurface = p.mIndex; mOuterBoundary = outer.mIndex; mInnerBoundaries = inner.ConvertAll(x => x.mIndex); }
		internal static void parseFields(IfcCurveBoundedPlane p, List<string> arrFields, ref int ipos)
		{
			IfcBoundedSurface.parseFields(p, arrFields, ref ipos);
			p.mBasisSurface = ParserSTEP.ParseLink(arrFields[ipos++]);
			p.mOuterBoundary = ParserSTEP.ParseLink(arrFields[ipos++]);
			p.mInnerBoundaries = ParserSTEP.SplitListLinks(arrFields[ipos++]);
		}
		internal static IfcCurveBoundedPlane Parse(string strDef) { IfcCurveBoundedPlane p = new IfcCurveBoundedPlane(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos); return p; }
		protected override string BuildString()
		{
			string str = ",(";
			if (mInnerBoundaries.Count > 0)
			{
				str += "#" + mInnerBoundaries[0];
				for (int icounter = 1; icounter < mInnerBoundaries.Count; icounter++)
					str += ",#" + mInnerBoundaries[icounter];
			}
			return base.BuildString() + "," + ParserSTEP.LinkToString(mBasisSurface) + "," + ParserSTEP.LinkToString(mOuterBoundary) + str + ")";
		}
	}
	public partial class IfcCurveBoundedSurface : IfcBoundedSurface //IFC4
	{
		private int mBasisSurface;// : IfcSurface;; 
		private List<int> mBoundaries = new List<int>();//: SET [1:?] OF IfcBoundaryCurve;
		private bool mImplicitOuter = false;//	 :	BOOLEAN; 

		internal IfcSurface BasisSurface { get { return mDatabase.mIfcObjects[mBasisSurface] as IfcSurface; } }
		internal List<IfcBoundaryCurve> Boundaries { get { return mBoundaries.ConvertAll(x => mDatabase.mIfcObjects[x] as IfcBoundaryCurve); } }
		internal bool ImplicitOuter { get { return mImplicitOuter; } }

		internal IfcCurveBoundedSurface() : base() { }
		internal IfcCurveBoundedSurface(IfcCurveBoundedSurface p) : base(p) { mBasisSurface = p.mBasisSurface; mBoundaries.AddRange(p.mBoundaries); }
		internal IfcCurveBoundedSurface(DatabaseIfc m, IfcSurface s, List<IfcBoundaryCurve> bounds)
			: base(m) { mBasisSurface = s.mIndex; mBoundaries = bounds.ConvertAll(x => x.mIndex); mImplicitOuter = false; }

		internal static void parseFields(IfcCurveBoundedSurface p, List<string> arrFields, ref int ipos)
		{
			IfcBoundedSurface.parseFields(p, arrFields, ref ipos);
			p.mBasisSurface = ParserSTEP.ParseLink(arrFields[ipos++]);
			p.mBoundaries = ParserSTEP.SplitListLinks(arrFields[ipos++]);
			p.mImplicitOuter = ParserSTEP.ParseBool(arrFields[ipos++]);
		}
		internal static IfcCurveBoundedSurface Parse(string strDef) { IfcCurveBoundedSurface p = new IfcCurveBoundedSurface(); int ipos = 0; parseFields(p, ParserSTEP.SplitLineFields(strDef), ref ipos); return p; }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.LinkToString(mBasisSurface) + "," + ParserSTEP.ListLinksToString(mBoundaries) + (mImplicitOuter ? ",.T." : ",.F."); }
	}
	public abstract partial class IfcCurveSegment2D : IfcBoundedCurve
	{
		private int mStartPoint;// : IfcCartesianPoint;
		private double mStartDirection;// : IfcPlaneAngleMeasure;
		private double mSegmentLength;// : IfcPositiveLengthMeasure;

		internal IfcCartesianPoint StartPoint { get { return mDatabase.mIfcObjects[mStartPoint] as IfcCartesianPoint; } }

		protected IfcCurveSegment2D() : base() { }
		protected IfcCurveSegment2D(IfcCurveSegment2D p) : base(p) { mStartPoint = p.mStartPoint; mStartDirection = p.mStartDirection; mSegmentLength = p.mSegmentLength; }
		protected IfcCurveSegment2D(IfcCartesianPoint start, double startDirection, double length)
			: base(start.mDatabase)
		{
			mStartDirection = startDirection;
			mSegmentLength = length;
		}

		internal static void parseFields(IfcCurveSegment2D c, List<string> arrFields, ref int ipos)
		{
			IfcBoundedCurve.parseFields(c, arrFields, ref ipos);
			c.mStartPoint = ParserSTEP.ParseLink(arrFields[ipos++]);
			c.mStartDirection = ParserSTEP.ParseDouble(arrFields[ipos++]);
			c.mSegmentLength = ParserSTEP.ParseDouble(arrFields[ipos++]);
		}
		protected override string BuildString() { return base.BuildString() + ",#" + mStartPoint + "," + ParserSTEP.DoubleToString(mStartDirection) + "," + ParserSTEP.DoubleToString(mSegmentLength); }
	}
	public partial class IfcCurveStyle : IfcPresentationStyle, IfcPresentationStyleSelect
	{
		internal int mCurveFont;// : OPTIONAL IfcCurveFontOrScaledCurveFontSelect;
		internal string mCurveWidth = "$";// : OPTIONAL IfcSizeSelect; 
		internal int mCurveColour;// : OPTIONAL IfcColour;
		internal bool mModelOrDraughting = true;//	:	OPTIONAL BOOLEAN; IFC4 CHANGE

		internal IfcColour CurveColour { get { return mDatabase.mIfcObjects[mCurveColour] as IfcColour; } }

		internal IfcCurveStyle() : base() { }
		internal IfcCurveStyle(IfcCurveStyle v) : base(v) { mCurveFont = v.mCurveFont; mCurveWidth = v.mCurveWidth; mCurveColour = v.mCurveColour; mModelOrDraughting = v.mModelOrDraughting; }
		internal IfcCurveStyle(DatabaseIfc m, string name, IfcCurveFontOrScaledCurveFontSelect font, IfcSizeSelect width, IfcColour col)
			: base(m, name) { if (font != null) mCurveFont = font.Index; if (width != null) mCurveWidth = width.ToString(); if (col != null) mCurveColour = col.Index; }
		internal static void parseFields(IfcCurveStyle s, List<string> arrFields, ref int ipos, Schema schema)
		{
			IfcPresentationStyle.parseFields(s, arrFields, ref ipos);
			s.mCurveFont = ParserSTEP.ParseLink(arrFields[ipos++]);
			s.mCurveWidth = arrFields[ipos++];
			s.mCurveColour = ParserSTEP.ParseLink(arrFields[ipos++]);
			if (schema != Schema.IFC2x3)
				s.mModelOrDraughting = ParserSTEP.ParseBool(arrFields[ipos++]);
		}
		internal static IfcCurveStyle Parse(string strDef, Schema schema) { IfcCurveStyle s = new IfcCurveStyle(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos, schema); return s; }
		protected override string BuildString() { return (mDatabase.mOutputEssential ? "" : base.BuildString() + "," + ParserSTEP.LinkToString(mCurveFont) + "," + mCurveWidth + "," + ParserSTEP.LinkToString(mCurveColour)) + (mDatabase.mSchema != Schema.IFC2x3 ? "," + ParserSTEP.BoolToString(mModelOrDraughting) : ""); }

	}
	public class IfcCurveStyleFont : IfcPresentationItem, IfcCurveStyleFontSelect
	{
		internal string mName = "$";// : OPTIONAL IfcLabel;
		internal List<int> mPatternList = new List<int>();// :  LIST [1:?] OF IfcCurveStyleFontPattern;

		public override string Name { get { return (mName == "$" ? "" : ParserIfc.Decode(mName)); } set { if (!string.IsNullOrEmpty(value)) mName = ParserIfc.Encode(value.Replace("'", "")); } }

		internal IfcCurveStyleFont() : base() { }
		internal IfcCurveStyleFont(IfcCurveStyleFont v) : base() { mName = v.mName; mPatternList = new List<int>(v.mPatternList.ToArray()); }
		internal static void parseFields(IfcCurveStyleFont s, List<string> arrFields, ref int ipos) { s.mName = arrFields[ipos++]; s.mPatternList = ParserSTEP.SplitListLinks(arrFields[ipos++]); }
		internal static IfcCurveStyleFont Parse(string strDef) { IfcCurveStyleFont s = new IfcCurveStyleFont(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildString()
		{
			string str = base.BuildString() + "," + mName + ",(" + ParserSTEP.LinkToString(mPatternList[0]);
			for (int icounter = 0; icounter < mPatternList.Count; icounter++)
				str += "," + ParserSTEP.LinkToString(mPatternList[icounter]);
			return str + ")";
		}
	}
	public class IfcCurveStyleFontAndScaling : IfcPresentationItem, IfcCurveFontOrScaledCurveFontSelect
	{
		internal string mName; // : 	OPTIONAL IfcLabel;
		internal int mCurveFont;// : 	IfcCurveStyleFontSelect;
		internal IfcPositiveRatioMeasure mCurveFontScaling;//: 	IfcPositiveRatioMeasure;
		internal IfcCurveStyleFontAndScaling() : base() { }
		internal IfcCurveStyleFontAndScaling(IfcCurveStyleFontAndScaling i) : base() { mName = i.mName; mCurveFont = i.mCurveFont; mCurveFontScaling = i.mCurveFontScaling; }
		internal static IfcCurveStyleFontAndScaling Parse(string strDef) { IfcCurveStyleFontAndScaling s = new IfcCurveStyleFontAndScaling(); int ipos = 0; parseFields(s, ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		internal static void parseFields(IfcCurveStyleFontAndScaling s, List<string> arrFields, ref int ipos) { s.mName = arrFields[ipos++].Replace("'", ""); s.mCurveFont = ParserSTEP.ParseLink(arrFields[ipos++]); s.mCurveFontScaling = new IfcPositiveRatioMeasure(arrFields[ipos++]); }
		protected override string BuildString() { return ",'" + mName + "'," + ParserSTEP.LinkToString(mCurveFont) + "," + mCurveFontScaling.ToString(); }
	}
	public interface IfcCurveFontOrScaledCurveFontSelect : IfcInterface { } //SELECT (IfcCurveStyleFontAndScaling ,IfcCurveStyleFontSelect);
	public class IfcCurveStyleFontPattern : IfcPresentationItem
	{
		internal double mVisibleSegmentLength;// : IfcLengthMeasure;
		internal double mInvisibleSegmentLength;//: IfcPositiveLengthMeasure;	
		internal IfcCurveStyleFontPattern() : base() { }
		internal IfcCurveStyleFontPattern(IfcCurveStyleFontPattern i) : base() { mVisibleSegmentLength = i.mVisibleSegmentLength; mInvisibleSegmentLength = i.mInvisibleSegmentLength; }
		internal static IfcCurveStyleFontPattern Parse(string strDef) { IfcCurveStyleFontPattern c = new IfcCurveStyleFontPattern(); int ipos = 0; parseFields(c, ParserSTEP.SplitLineFields(strDef), ref ipos); return c; }
		internal static void parseFields(IfcCurveStyleFontPattern c, List<string> arrFields, ref int ipos) { c.mVisibleSegmentLength = ParserSTEP.ParseDouble(arrFields[ipos++]); c.mInvisibleSegmentLength = ParserSTEP.ParseDouble(arrFields[ipos++]); }
		protected override string BuildString() { return base.BuildString() + "," + ParserSTEP.DoubleToString(mVisibleSegmentLength) + "," + ParserSTEP.DoubleToString(mInvisibleSegmentLength); }
	}
	public interface IfcCurveStyleFontSelect : IfcCurveFontOrScaledCurveFontSelect { } //SELECT (IfcCurveStyleFont ,IfcPreDefinedCurveFont);
}
