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
using System.Collections;
using System.Text;
using System.Reflection;
using System.IO;
using System.ComponentModel;
using System.Linq;
using System.Xml;
//using System.Xml.Linq;


using GeometryGym.STEP;

namespace GeometryGym.Ifc
{
	public partial class BaseClassIfc : STEPEntity, IBaseClassIfc
	{
		internal virtual void ParseXml(XmlElement xml) { }
		internal XmlElement GetXML(XmlDocument doc, string name, BaseClassIfc host, Dictionary<string,XmlElement> processed)
		{
			string type = StepClassName;
			if (string.IsNullOrEmpty(name))
				name = type;

			XmlElement existing = null;
			string id = xmlId();
			if (processed.TryGetValue(id, out existing))
			{
				if (!mDatabase.XMLMandatoryId)
				{
					if (!existing.HasAttribute("id"))
					{
						XmlAttribute attribute = doc.CreateAttribute("id");
						attribute.Value = id;
						existing.Attributes.Prepend(attribute);
					}
				}
				XmlElement xelement = doc.CreateElement(name, mDatabase.mXmlNamespace);
				XmlAttribute nil = doc.CreateAttribute("xsi", "nil", mDatabase.mXsiNamespace);
				nil.Value = "true";
				xelement.SetAttributeNode(nil);
				
				xelement.SetAttribute("href", id);
				return xelement;
			}
			
			XmlElement element = doc.CreateElement(name, mDatabase.mXmlNamespace);
			processed[id] = element;
			SetXML(element, host, processed);
			
			if(mDatabase.XMLMandatoryId)
				element.SetAttribute("id", "i" + mIndex);
			if (string.Compare(name, type) != 0)
			{
				XmlAttribute typeAttribute = doc.CreateAttribute("type", mDatabase.mXsiNamespace);
				typeAttribute.Value = type;
				element.Attributes.Prepend(typeAttribute);
			}
			return element;
		}
		internal virtual void SetXML(XmlElement xml, BaseClassIfc host, Dictionary<string, XmlElement> processed)
		{
			foreach (string str in mComments)
				xml.AppendChild(xml.OwnerDocument.CreateComment(str));
		}
		
		protected void setAttribute(XmlElement xml, string name, string value)
		{
			if (!string.IsNullOrEmpty(value))
				xml.SetAttribute(name, value);	
		}
		protected void setAttribute(XmlElement xml, string name, double val)
		{
			if (!double.IsNaN(val))
				xml.SetAttribute(name, val.ToString());
		}
		protected string extractString(XmlElement xml, string name) { return (xml.HasAttribute(name) ? xml.Attributes[name].Value : ""); }
		protected void setChild(XmlElement xml, string name, IEnumerable<IBaseClassIfc> objects, Dictionary<string, XmlElement> processed)
		{
			if (objects == null || objects.Count() == 0)
				return;
			XmlElement element = xml.OwnerDocument.CreateElement(name, mDatabase.mXmlNamespace);
			xml.AppendChild(element);
			foreach (IBaseClassIfc o in objects)
				element.AppendChild(mDatabase[o.Index].GetXML(xml.OwnerDocument, "", this, processed));
		}
		internal static XmlNode convert(XmlDocument doc, IfcValue value, string name, string ifcnamespace)
		{
			string keyword = value.GetType().Name;
			XmlElement v = doc.CreateElement(keyword + "-wrapper", ifcnamespace);
			v.InnerText = value.ValueString;
			if (string.IsNullOrEmpty(name))
				return v;
			XmlElement element = doc.CreateElement(name, ifcnamespace);
			element.AppendChild(v);
			return element;
		}
		internal static IfcValue extractValue(XmlNode node)
		{
			string name = node.Name;
			if(name.EndsWith("-wrapper"))
			{
				name = name.Substring(0, name.Length - 8);
				return ParserIfc.extractValue(name, node.InnerText);
			}
			
			return null;
		}

		internal string xmlId()
		{
			string id = "";
			IfcClassificationReference classificationReference = this as IfcClassificationReference;
			if (classificationReference != null && !string.IsNullOrEmpty(classificationReference.Identification))
				id = classificationReference.Identification;
			if(string.IsNullOrEmpty(id))
				return "i" + StepId;
			if (char.IsDigit(id[0]))
				return "_" + id;
			return id;
		}
	}
}
