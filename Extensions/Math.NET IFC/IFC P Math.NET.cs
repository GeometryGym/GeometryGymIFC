using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Spatial.Euclidean;

namespace GeometryGym.Ifc
{
	public abstract partial class IfcPoint : IfcGeometricRepresentationItem, IfcGeometricSetSelect, IfcPointOrVertexPoint /*ABSTRACT SUPERTYPE OF (ONEOF (IfcCartesianPoint ,IfcPointOnCurve ,IfcPointOnSurface))*/
	{
		public virtual Point3D Coordinates { get { return Point3D.NaN; } }
	}
	public partial class IfcPlacement : IfcGeometricRepresentationItem /*ABSTRACT SUPERTYPE OF (ONEOF (IfcAxis1Placement ,IfcAxis2Placement2D ,IfcAxis2Placement3D))*/
	{
		internal Point3D LocationPoint { get { return Location.Coordinates; } }

		public abstract CoordinateSystem CoordinateSystem { get; }

		protected IfcPlacement(DatabaseIfc db, Point2D position) : base(db) { Location = new IfcCartesianPoint(db, position); }
	}
}	
