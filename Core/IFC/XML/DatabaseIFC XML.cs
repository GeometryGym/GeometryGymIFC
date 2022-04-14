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
using System.Xml.Serialization;
//using System.Xml.Linq;


namespace GeometryGym.Ifc
{
	public partial class DatabaseIfc
	{
		internal bool XMLMandatoryId { get; set; }
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
			foreach (XmlNode node in doc.DocumentElement.ChildNodes)
			{
				ParseXml<IBaseClassIfc>(node as XmlElement);
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
			if (xml.HasAttribute("ref"))
			{
				string str = xml.Attributes["ref"].Value;
				if (mParsed.ContainsKey(str))
					return (T)(IBaseClassIfc)mParsed[str];
				if (xml.HasAttribute("xsi:nil"))
				{
					string query = string.Format("//*[@id='{0}']", str, CultureInfo.InvariantCulture);
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
					string query = string.Format("//*[@id='{0}']", str, CultureInfo.InvariantCulture); // or "//book[@id='{0}']"
					xml = (XmlElement)xml.OwnerDocument.SelectSingleNode(query);
					if (xml == null)
						return default(T);
				}
			}
			string id = (xml.HasAttribute("id") ? xml.Attributes["id"].Value : "");
			if (!string.IsNullOrEmpty(id) && mParsed.ContainsKey(id))
				return (T)(IBaseClassIfc)mParsed[id];

			string keyword = xml.HasAttribute("xsi:type") ? xml.Attributes["xsi:type"].Value : xml.Name;
			Type type = BaseClassIfc.GetType(keyword);
			if (type == null)
			{
				type = typeof(T);
				if (xml.Attributes.Count == 0 && xml.ChildNodes.Count == 1)
				{
					XmlNode node = xml.ChildNodes[0];
					Type childType = BaseClassIfc.GetType(node.Name);
					if (string.Compare(node.Name, type.Name, true) == 0 || (childType != null && childType.IsSubclassOf(type)))
					{
						return ParseXml<T>(node as XmlElement);
					}
				}
			}
			if (type.IsAbstract)
			{
				IEnumerable<Type> types = type.Assembly.GetTypes().Where(x => x != type && type.IsAssignableFrom(x));
				if (types != null && types.Count() == 1)
					type = types.First();
			}
			ConstructorInfo constructor = type.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
null, Type.EmptyTypes, null);
			if (type.IsAbstract && xml.HasChildNodes && xml.ChildNodes.Count == 1)
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
					int index = NextBlank();
					if (!string.IsNullOrEmpty(id))
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
						if (!mParsed.ContainsKey(str))
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
		internal string mXmlNamespace = @"http://www.buildingsmart-tech.org/ifc/IFC4x1/final";
		internal string mXmlSchema = @"http://standards.buildingsmart.org/IFC/RELEASE/IFC4_1/FINAL/XML/IFC4x1.xsd";

		internal string mXsiNamespace = @"http://www.w3.org/2001/XMLSchema-instance";
		public XmlDocument XmlDocument()
		{
			XmlDocument doc = new XmlDocument();
			XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", "UTF-8", "yes");
			doc.AppendChild(dec);

			XmlElement el = doc.CreateElement("ifcXML", mXmlNamespace);
			doc.AppendChild(el);

			XmlAttribute schemaLocation = doc.CreateAttribute("xsi", "schemaLocation", mXsiNamespace);
			schemaLocation.Value = mXmlSchema;
			el.SetAttributeNode(schemaLocation);

			Dictionary<string, XmlElement> processed = new Dictionary<string, XmlElement>();
			IfcContext context = Context;
			if (context != null)
				el.AppendChild(Context.GetXML(doc, "", null, processed));

			List<BaseClassIfc> toProcess = new List<BaseClassIfc>();
			foreach (BaseClassIfc e in this)
			{
				string id = e.xmlId();
				if (!processed.ContainsKey(id))
				{
					if (e is IfcRelationship)
						el.AppendChild(e.GetXML(doc, "", null, processed));
					else
						toProcess.Add(e);
				}
			}
			foreach (BaseClassIfc e in toProcess)
			{
				string id = e.xmlId();
				if(!processed.ContainsKey(id))
					el.AppendChild(e.GetXML(doc, "", null, processed));
			}

			return doc;
		}
		public bool WriteXml(XmlTextWriter writer)
		{
			CultureInfo current = Thread.CurrentThread.CurrentCulture;
			Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
			XmlDocument doc = XmlDocument();
			try
			{
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
			catch (Exception) { }
			Thread.CurrentThread.CurrentUICulture = current;
			return true;
		}
		public string XmlString()
		{
			StringWriter sw = new StringWriter();
			XmlTextWriter writer = new XmlTextWriter(sw);
			if (WriteXml(writer))
				return sw.ToString();
			return null;

		}
		public bool WriteXmlFile(string filename)
		{
			XmlTextWriter writer = new XmlTextWriter(filename, Encoding.UTF8);
			return WriteXml(writer);
		}

	}
}

