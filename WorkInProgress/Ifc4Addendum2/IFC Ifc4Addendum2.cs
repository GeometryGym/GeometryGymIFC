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
	public partial class IfcPolygonalFaceSet : IfcTessellatedFaceSet //IFC4A2
	{
		internal bool mClosed; // 	OPTIONAL BOOLEAN;
		internal List<int> mFaces = new List<int>(); // : SET [1:?] OF IfcIndexedPolygonalFace;

		public bool Closed { get { return mClosed; } set { mClosed = value; } }
		public IEnumerable<IfcIndexedPolygonalFace> Faces { get { return mFaces.ConvertAll(x=>mDatabase[x] as IfcIndexedPolygonalFace); } set { mFaces = value.ToList().ConvertAll(x=>x.mIndex); } }

		internal IfcPolygonalFaceSet() : base() { }
		internal IfcPolygonalFaceSet(DatabaseIfc db, IfcPolygonalFaceSet s) : base(db,s) { Faces = s.Faces.ToList().ConvertAll(x => db.Duplicate(x) as IfcIndexedPolygonalFace); }
		public IfcPolygonalFaceSet(IfcCartesianPointList3D pl, bool closed, IEnumerable<IfcIndexedPolygonalFace> faces)
			: base(pl) { Faces = faces; mClosed = closed; }
		internal static IfcPolygonalFaceSet Parse(string str)
		{
			IfcPolygonalFaceSet t = new IfcPolygonalFaceSet();
			int pos = 0;
			t.Parse(str, ref pos);
			return t;
		}
		protected override void Parse(string str, ref int pos)
		{
			base.Parse(str, ref pos);
			mClosed = ParserSTEP.StripBool(str, ref pos);
			mFaces = ParserSTEP.StripListLink(str, ref pos);
		}
		protected override string BuildStringSTEP() 
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("," + ParserSTEP.BoolToString(mClosed) + ",(#" + mFaces[0]);
			for (int icounter = 1; icounter < mFaces.Count; icounter++)
				sb.Append(",#" + mFaces[icounter]);
			return base.BuildStringSTEP() + sb.ToString() + ")";
		}
	}
	public partial class IfcIndexedPolygonalFace : IfcTessellatedItem
	{
		internal List<int> mCoordIndex = new List<int>();// : LIST [3:?] OF IfcPositiveInteger;
		//INVERSE
		internal IfcPolygonalFaceSet mToFaceSet = null;

		public IEnumerable<int> CoordIndex { get { return mCoordIndex; } set { mCoordIndex = value.ToList(); } }
		public IfcPolygonalFaceSet ToFaceSet { get { return mToFaceSet; } set { mToFaceSet = value; } }

		internal IfcIndexedPolygonalFace() : base() { }
		internal IfcIndexedPolygonalFace(DatabaseIfc db, IfcIndexedPolygonalFace f) : base(db,f) { mCoordIndex.AddRange(f.mCoordIndex); }
		public IfcIndexedPolygonalFace(DatabaseIfc db, IEnumerable<int> coords) : base(db) { CoordIndex = coords; }
		protected override void parseFields(List<string> arrFields, ref int ipos)
		{
			base.parseFields(arrFields, ref ipos);
			mCoordIndex = ParserSTEP.SplitListSTPIntegers(arrFields[ipos++]);
		}
		internal static IfcIndexedPolygonalFace Parse(string strDef) { IfcIndexedPolygonalFace s = new IfcIndexedPolygonalFace(); int ipos = 0; s.parseFields(ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildStringSTEP()
		{
			string result = base.BuildStringSTEP() + ",(" + mCoordIndex[0];
			for (int icounter = 1; icounter < mCoordIndex.Count; icounter++)
				result += "," + mCoordIndex[icounter];
			return result + ")";
		}
	}
	public partial class IfcIndexedPolygonalFaceWithVoids : IfcIndexedPolygonalFace
	{
		internal List<List<int>> mInnerCoordIndices = new List<List<int>>();// : List[1:?] LIST [3:?] OF IfcPositiveInteger;
		public IEnumerable<IEnumerable<int>> InnerCoordIndices { get { return mInnerCoordIndices; } set { mInnerCoordIndices = value.ToList().ConvertAll(x => x.ToList()); } }
		internal IfcIndexedPolygonalFaceWithVoids() : base() { }
		internal IfcIndexedPolygonalFaceWithVoids(DatabaseIfc db, IfcIndexedPolygonalFaceWithVoids f) : base(db,f) { mInnerCoordIndices.AddRange(f.mInnerCoordIndices); }
		public IfcIndexedPolygonalFaceWithVoids(DatabaseIfc db, IEnumerable<int> coords, IEnumerable<IEnumerable<int>> inners) : base(db,coords)
		{ InnerCoordIndices = inners; }
		protected override void parseFields(List<string> arrFields, ref int ipos)
		{
			base.parseFields(arrFields, ref ipos);
			string str = arrFields[ipos++];
			List<string> fields = ParserSTEP.SplitLineFields(str.Substring(1,str.Length-2));
			mInnerCoordIndices = fields.ConvertAll(x=>ParserSTEP.SplitListSTPIntegers(x));
		}
		internal new static IfcIndexedPolygonalFaceWithVoids Parse(string strDef) { IfcIndexedPolygonalFaceWithVoids s = new IfcIndexedPolygonalFaceWithVoids(); int ipos = 0; s.parseFields(ParserSTEP.SplitLineFields(strDef), ref ipos); return s; }
		protected override string BuildStringSTEP()
		{
			List<int> inner = mInnerCoordIndices[0];
			string result = inner[0].ToString();
			for (int icounter = 1; icounter < inner.Count; icounter++)
				result += "," + inner[icounter];
			for(int jcounter = 1; jcounter < mInnerCoordIndices.Count; jcounter++)
			{
				inner = mInnerCoordIndices[jcounter];
				result += "),(" + inner[0];
				for (int icounter = 1; icounter < inner.Count; icounter++)
					result += "," + inner[icounter];
			}
			return base.BuildStringSTEP() + ",((" + result + "))";
		}
	}

}
