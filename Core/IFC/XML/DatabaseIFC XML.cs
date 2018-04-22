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
using System.Globalization;
using System.Threading;
using System.Xml;
//using System.Xml.Linq;


namespace GeometryGym.Ifc
{
	public partial class DatabaseIfc
	{
		internal bool XMLMandatoryId { get; set; } = false;
		public void ReadXMLFile(string filename)
		{
			FileName = filename;
			XmlDocument doc = new XmlDocument();
			doc.Load(filename);
			ReadXMLDoc(doc);
		}
		public void ReadXMLStream(TextReader stream)
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(stream);
			ReadXMLDoc(doc);
		}
		public void ReadXMLDoc(XmlDocument doc)
		{
			CultureInfo current = Thread.CurrentThread.CurrentCulture;
			Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
			Release = ReleaseVersion.IFC4;
			foreach(XmlNode node in doc.DocumentElement.ChildNodes)
			{
				ParseXml<IBaseClassIfc>( node as XmlElement);
			}
			postImport();
		}
		Dictionary<string, BaseClassIfc> mParsed = new Dictionary<string, BaseClassIfc>();
		//internal BaseClassIfc ParseXml(XmlElement xml)
		//{
		//	if (xml == null)
		//		return null;
		//	string keyword = xml.HasAttribute("xsi:type") ? xml.Attributes["xsi:type"].Value : xml.Name;
		//	Type type = Type.GetType("GeometryGym.Ifc." + keyword, false, true);
		//	if (type == null)
		//	{
		//		return null;
		//	}
		//	return ParseXml<type>(xml);
		//}
		internal List<T> ParseXMLList<T>(XmlNode xml) where T : IBaseClassIfc
		{
			List<T> result = new List<T>(xml.ChildNodes.Count);
			foreach (XmlNode cn in xml.ChildNodes)
			{
				T obj = ParseXml<T>(cn as XmlElement);
				if (obj != null)
					result.Add(obj);
			}
			return result;
		}
		internal T ParseXml<T>(XmlElement xml) where T : IBaseClassIfc
		{
			if (xml == null)
				return default(T);
			if(xml.HasAttribute("ref"))
			{
				string str = xml.Attributes["ref"].Value;
				if (mParsed.ContainsKey(str))
					return (T)(IBaseClassIfc)mParsed[str];
				if(xml.HasAttribute("xsi:nil"))
				{
					string query = string.Format("//*[@id='{0}']", str); 
					xml = (XmlElement)xml.OwnerDocument.SelectSingleNode(query);
					if (xml == null)
						return default(T);
				}
			}
			else if (xml.HasAttribute("href"))
			{
				string str = xml.Attributes["href"].Value;
				if (mParsed.ContainsKey(str))
					return (T)(IBaseClassIfc)mParsed[str];
				if (xml.HasAttribute("xsi:nil"))
				{
					string query = string.Format("//*[@id='{0}']", str); // or "//book[@id='{0}']"
					xml = (XmlElement)xml.OwnerDocument.SelectSingleNode(query);
					if (xml == null)
						return default(T);
				}
			}
			string id = (xml.HasAttribute("id") ? xml.Attributes["id"].Value : "");
			if (!string.IsNullOrEmpty(id) && mParsed.ContainsKey(id))
				return (T)(IBaseClassIfc)mParsed[id];

			string keyword = xml.HasAttribute("xsi:type") ? xml.Attributes["xsi:type"].Value : xml.Name;
			Type type = Type.GetType("GeometryGym.Ifc." + keyword, false, true);
			if (type == null)
			{
				type = typeof(T);
			}	
			if(type.IsAbstract)
			{
				IEnumerable<Type> types = type.Assembly.GetTypes().Where(x=> x != type && type.IsAssignableFrom(x));
				if (types != null && types.Count() == 1)
					type = types.First();
			}
			ConstructorInfo constructor = type.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
null, Type.EmptyTypes, null);
			if(type.IsAbstract && xml.HasChildNodes && xml.ChildNodes.Count == 1)
			{
				T result = ParseXml<T>(xml.ChildNodes[0] as XmlElement);
				if (result != null)
					return result;
			}
			if (!type.IsAbstract && constructor != null)
			{
				BaseClassIfc entity = constructor.Invoke(new object[] { }) as BaseClassIfc;
				if (entity != null)
				{
					int index = NextBlank;
					if(!string.IsNullOrEmpty(id))
					{
						int i = 0;
						if (id[0] == 'i')
						{
							if (int.TryParse(id.Substring(1), out i))
							{
								if (this[i] == null)
									index = i;
							}
						}
					}
					this[index] = entity;
					entity.ParseXml(xml);
					if (xml.HasAttribute("id"))
					{
						string str = xml.Attributes["id"].Value;
						if(!mParsed.ContainsKey(str))
							mParsed.Add(str, entity);
					}
					return (T)(IBaseClassIfc)entity;
				}
			}
			if (xml.HasChildNodes && xml.ChildNodes.Count == 1)
			{
				T result = ParseXml<T>(xml.ChildNodes[0] as XmlElement);
				if (result != null)
					return result;
			}
			foreach (XmlNode node in xml.ChildNodes)
			{
				ParseXml<IBaseClassIfc>(node as XmlElement);
			}
			
			return default(T);
		}
		internal string mXmlNamespace = "http://www.buildingsmart-tech.org/ifcXML/IFC4_ADD2";
		internal string mXmlSchema = "http://www.buildingsmart-tech.org/ifc/IFC4/Add2/IFC4_ADD2.xsd";
		internal string mXsiNamespace = "http://www.w3.org/2001/XMLSchema-instance";
		public bool WriteXMLFile(string filename)
		{
			CultureInfo current = Thread.CurrentThread.CurrentCulture;
			Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

			XmlDocument doc = new XmlDocument();
			XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", "UTF-8", "yes");
			doc.AppendChild(dec);
			
			XmlElement el = doc.CreateElement("ifc:ifcXML", mXmlNamespace);
			
			doc.AppendChild(el);
			XmlAttribute schemaLocation = doc.CreateAttribute("xsi", "schemaLocation",mXsiNamespace);
			schemaLocation.Value = mXmlSchema;
			el.SetAttributeNode(schemaLocation);

			XmlAttribute ns = doc.CreateAttribute("xlmns", "ifc", mXmlNamespace);
			ns.Value = mXmlNamespace;
			el.SetAttribute("xlmns", mXmlNamespace);

			Dictionary<int, XmlElement> processed = new Dictionary<int, XmlElement>();
			IfcContext context = Context;
			if(context != null)
				el.AppendChild(Context.GetXML(doc, "", null, processed));

			if (context == null || (context.mIsDecomposedBy.Count == 0 && context.Declares.Count == 0))
			{
				foreach (BaseClassIfc e in this)
				{
					if (!processed.ContainsKey(e.Index))
						el.AppendChild(e.GetXML(doc, "", null, processed));
				}
			}

			XmlTextWriter writer;
			try
			{
				writer = new XmlTextWriter(filename,Encoding.UTF8);
				writer.Formatting = Formatting.Indented;
				try
				{
					doc.WriteTo(writer);
				}
				finally
				{
					writer.Flush();
					writer.Close();
				}
			}
			catch(Exception ) { }
			Thread.CurrentThread.CurrentUICulture = current;
			return true;
		}
		
	}
}

 