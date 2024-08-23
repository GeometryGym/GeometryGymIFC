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
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections;
using System.Globalization;
using System.Text;
using System.Reflection;
using System.IO;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;

using GeometryGym.STEP;
using System.Runtime.CompilerServices;

namespace GeometryGym.Ifc
{
	public static class ParserIfc 
	{

		public static T ParseEnum<T>(string str, string enumName) where T : struct
		{
			int start = enumName.Length + 2, end = str.Length - start - 2;
			if (end < 1)
				return default(T);
			return ParseEnum<T>(str.Substring(start, end));
		}
		public static T ParseEnum<T>(string str) where T : struct
		{
			T result = default(T);
			if (string.IsNullOrEmpty(str))
				return result;
			Enum.TryParse<T>(str, out result);
			return result;
		}
		public static IfcLogicalEnum ParseIFCLogical(string str) 
		{
			string s = str.Trim();
			if (str == "$")
				return IfcLogicalEnum.UNKNOWN;
			Char c = char.ToUpper(s.Replace(".", "")[0]);
			if (c == 'T')
				return IfcLogicalEnum.TRUE;
			else if (c == 'F')
				return IfcLogicalEnum.FALSE;

			return IfcLogicalEnum.UNKNOWN;
		}
		public static IfcLogicalEnum StripLogical(string s, ref int pos, int len)
		{
			IfcLogicalEnum result = IfcLogicalEnum.UNKNOWN;
			int icounter = pos;
			while (char.IsWhiteSpace(s[icounter]))
			{
				icounter++;
				if (icounter == len)
					break;
			}
			if (s[icounter] == '$')
			{
				if (++icounter < len)
				{
					while (s[icounter++] != ',')
					{
						if (icounter == len)
							break;
					}
				}
				pos = icounter;
				return result;
			}
			if (s[icounter++] != '.')
				throw new Exception("Unrecognized format!");
			char c = char.ToUpper(s[icounter++]);
			if (c == 'T')
				result = IfcLogicalEnum.TRUE;
			else if (c == 'F')
				result = IfcLogicalEnum.FALSE;
			pos = icounter + 2;
			return result;
		}
		public static string LogicalToString(IfcLogicalEnum l)
		{
			if (l == IfcLogicalEnum.TRUE)
				return ".T.";
			else if (l == IfcLogicalEnum.FALSE)
				return ".F.";
			return ".U.";
		}
		
		internal static BaseClassIfc ParseLine(string line, ReleaseVersion schema, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			string kw = "", str = "";
			int stepID = 0;
			
			ParserSTEP.GetKeyWord(line, out stepID, out kw, out str);
			if (string.IsNullOrEmpty(kw) || !kw.ToUpper().StartsWith("IFC"))
				return null;
			str = str.Trim();
			ConcurrentDictionary<int, BaseClassIfc> dict = dictionary;
			if(dict == null)
			{
				dict = new ConcurrentDictionary<int, BaseClassIfc>();
				dict[0] = null;
			}
			BaseClassIfc result = BaseClassIfc.LineParser(kw, str, schema, dict);
			if (result == null)
				return null;
			result.mStepId = stepID;
			return result;
		}

		internal static IfcColour parseColour(string str)
		{
			string kw = "", def = "";
			int id = 0,pos = 0;
			ParserSTEP.GetKeyWord(str, out id, out kw, out def);
			if (string.IsNullOrEmpty(kw))
				return null;
			if (string.Compare(kw, "IFCCOLOURRGB", false) == 0)
			{
				IfcColourRgb color = new IfcColourRgb();
				color.parse(def, ref pos, ReleaseVersion.IFC2x3, def.Length, null);
				return color;
			}
			if (string.Compare(kw, "IFCDRAUGHTINGPREDEFINEDCOLOUR", false) == 0)
			{
				IfcDraughtingPreDefinedColour color = new IfcDraughtingPreDefinedColour();
				color.parse(def, ref pos, ReleaseVersion.IFC2x3, def.Length, null);
				return color;
			}
			return null;
		}
		internal static IfcColourOrFactor parseColourOrFactor(string str, ConcurrentDictionary<int, BaseClassIfc> dictionary)
		{
			if (string.IsNullOrEmpty(str) || str[0] == '$')
				return null;

			if(str[0] == '#')
			{
				int i = ParserSTEP.ParseLink(str);
				if(i > 0)
					return dictionary[i] as IfcColourOrFactor;
				return null;
			}
			string kw = "", def = "";
			int id = 0,pos = 0;
			ParserSTEP.GetKeyWord(str, out id, out kw, out def);
			if (string.IsNullOrEmpty(kw))
				return null;
			if (string.Compare(kw, "IFCCOLOURRGB", true) == 0)
			{
				IfcColourRgb color = new IfcColourRgb();
				color.parse(def, ref pos, ReleaseVersion.IFC2x3, def.Length, null);
				return color;
			}
			double ratio = 0;
			if(double.TryParse(def, System.Globalization.NumberStyles.Any, ParserSTEP.NumberFormat, out ratio)) 
				return new IfcNormalisedRatioMeasure(ratio);
			return null;
		}

		public static string STEPString(IfcColourOrFactor colourOrFactor)
		{
			if (colourOrFactor == null)
				return "$";
			IfcColourRgb colourRgb = colourOrFactor as IfcColourRgb;
			if (colourRgb != null)
				return "#" + colourRgb.StepId;
			IfcNormalisedRatioMeasure normalisedRatioMeasure = colourOrFactor as IfcNormalisedRatioMeasure;
			if (normalisedRatioMeasure != null)
				return normalisedRatioMeasure.ToString();
			return "$";
		}
		private static Dictionary<string, Type> mDerivedMeasureValueTypes = null;
		private static object mDerivedMeasureValueTypesLock = new object();
		private static Dictionary<string, Type> DerivedMeasureValueTypes
		{
			get
			{
				lock (mDerivedMeasureValueTypesLock)
					if (mDerivedMeasureValueTypes == null)
					{
						mDerivedMeasureValueTypes = new Dictionary<string, Type>();
						IEnumerable<Type> types = from type in Assembly.GetCallingAssembly().GetTypes()
												  where typeof(IfcDerivedMeasureValue).IsAssignableFrom(type)
												  select type;
						foreach (Type t in types)
							mDerivedMeasureValueTypes[t.Name.ToLower()] = t;
					}
				return mDerivedMeasureValueTypes;
			}
		}
		internal static IfcDerivedMeasureValue parseDerivedMeasureValue(string str)
		{
			try
			{
				int len = str.Length;
				if (str.EndsWith(")"))
					len--;
				int icounter = 0;
				char c = str[icounter];
				while (!char.IsDigit(c) && icounter < str.Length)
					c = str[icounter++];
				if (icounter == str.Length)
					return null;
				icounter--;
				if (icounter > 1)
				{
					string kw = str.Substring(0, icounter - 1).ToLower();
					Dictionary<string, Type> dmvtypes = DerivedMeasureValueTypes;
					Type type = null;
					if(dmvtypes.TryGetValue(kw, out type))
					{
						double val = 0;
						string measure = str.Substring(icounter, len - icounter);
						if (double.TryParse(measure, System.Globalization.NumberStyles.Any, ParserSTEP.NumberFormat, out val))
						{
							Type[] types = new Type[] { typeof(double) };
							ConstructorInfo constructor = type.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
				null, types, null);
							if (constructor != null)
								return constructor.Invoke(new object[] { val }) as IfcDerivedMeasureValue;
							else
								return null;
						}
					}
				}
			}
			catch(Exception) { }
			return null;
		}

		private class DictionaryMeasureValueTypes : Dictionary<string,Type>
		{
			internal DictionaryMeasureValueTypes()
			{
				IEnumerable<Type> types = from type in Assembly.GetCallingAssembly().GetTypes()
										  where typeof(IfcMeasureValue).IsAssignableFrom(type)
										  select type;
				foreach (Type t in types)
					base[t.Name.ToLower()] = t;
			}
		}
		private static readonly DictionaryMeasureValueTypes mMeasureValueTypes = new DictionaryMeasureValueTypes();
		internal static IfcMeasureValue parseMeasureValue(string str)
		{
			try
			{
				if (string.IsNullOrEmpty(str))
					return null;
				int len = str.Length;
				if (str.EndsWith(")"))
					len--;
				int icounter = 0;
				char c = str[icounter];
				while (!char.IsDigit(c) && c != '-' && c != '.' && icounter < str.Length)
					c = str[icounter++];
				if (icounter == str.Length)
					return null;
				icounter--;
				if (icounter > 1)
				{
					string kw = str.Substring(0, icounter - 1).ToLower();
					if (kw.All(Char.IsLetter))
					{
						Type type = null;
						if(mMeasureValueTypes.TryGetValue(kw, out type))
							return extractMeasureValue(type, str.Substring(icounter, len - icounter));
					}
				}
			}
			catch(Exception) { }
			return null;
		}
		internal static IfcMeasureValue extractMeasureValue(Type type, string value)
		{
			if (type.IsSubclassOf(typeof(IfcMeasureValue)))
			{
				if (string.Compare(type.Name,"IfcDescriptiveMeasure",true) == 0)
					return new IfcDescriptiveMeasure(value);
				double val = 0;
				if(double.TryParse(value, System.Globalization.NumberStyles.Any, ParserSTEP.NumberFormat, out val))
				{
					Type[] types = new Type[] { typeof(double) };
					ConstructorInfo constructor = type.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
	null, types, null);
					if (constructor != null)
						return constructor.Invoke(new object[] { val }) as IfcMeasureValue;
				}
			}
			return null;
		}
		internal static IfcDerivedMeasureValue extractDerivedMeasureValue(Type type, string value)
		{
			if (type.IsSubclassOf(typeof(IfcDerivedMeasureValue)))
			{
				double val = 0;
				if (double.TryParse(value, System.Globalization.NumberStyles.Any, ParserSTEP.NumberFormat, out val))
				{
					Type[] types = new Type[] { typeof(double) };
					ConstructorInfo constructor = type.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
	null, types, null);
					if (constructor != null)
						return constructor.Invoke(new object[] { val }) as IfcDerivedMeasureValue;
				}
			}
			return null;
		}
		internal static IfcSimpleValue parseSimpleValue(string str)
		{
			if (str.StartsWith("IFCBOOLEAN("))
				return new IfcBoolean(string.Compare(str.Substring(11, str.Length - 12), ".T.") == 0);
			if (str.StartsWith("IFCIDENTIFIER("))
				return new IfcIdentifier(ParserSTEP.Decode(str.Substring(15, str.Length - 17)));
			if (str.StartsWith("IFCINTEGER("))
				return new IfcInteger(long.Parse(str.Substring(11, str.Length - 12)));
			if (str.StartsWith("IFCLABEL("))
			{
				if (str.Length <= 12)
					return new IfcLabel("");
				string s = str.Substring(10, str.Length - 12);
				return new IfcLabel((str[10] == '$' || s == null ? "" : ParserSTEP.Decode(s)));
			}
			if (str.StartsWith("IFCLOGICAL("))
			{
				string s = str.Substring(11, str.Length - 12);
				IfcLogicalEnum l = IfcLogicalEnum.UNKNOWN;
				if (s == ".T.")
					l = IfcLogicalEnum.TRUE;
				else if (s == ".F.")
					l = IfcLogicalEnum.FALSE;
				return new IfcLogical(l);
			}
			if (str.StartsWith("IFCREAL("))
				return new IfcReal(ParserSTEP.ParseDouble(str.Substring(8, str.Length - 9)));
			if (str.StartsWith("IFCTEXT("))
			{
				string s = str.Substring(9, str.Length - 11);
				return new IfcText((str[9] == '$' || s == null ? "" : ParserSTEP.Decode(s)));
			}
			if (str.StartsWith("IFCURIREFERENCE("))
				return new IfcURIReference(str[16] == '$' ? "" : ParserSTEP.Decode(str.Substring(17, str.Length - 19)));
			if(str.StartsWith("IFCDATE("))
			{
				string valueString = str.Substring(9, str.Length - 11);
				if (DateTime.TryParse(valueString, out DateTime dateTime))
					return new IfcDate(dateTime);
				return new IfcLabel(valueString);
			}
			if(str.StartsWith("IFCDATETIME("))
			{
				string valueString = str.Substring(13, str.Length - 15);
				if (DateTime.TryParse(valueString, out DateTime dateTime))
					return new IfcDateTime(dateTime);
				return new IfcLabel(valueString);
			}
			if(str.StartsWith("IFCDURATION("))
				return IfcDuration.Convert(str.Substring(13, str.Length - 15));
			int i = 0;
			if (int.TryParse(str, out i))
				return new IfcInteger(i);
			double d = 0;
			if (double.TryParse(str, System.Globalization.NumberStyles.Any, ParserSTEP.NumberFormat, out d))
				return new IfcReal(d);
			if (str == ".T.")
				return new IfcBoolean(true);
			if (str == ".F.")
				return new IfcBoolean(false);
			if (str == ".U.")
				return new IfcLogical(IfcLogicalEnum.UNKNOWN);
			return null;
		}
		internal static IfcSimpleValue extractSimpleValue(Type type, string value)
		{
			if(type.IsSubclassOf(typeof(IfcSimpleValue)))
			{
				string name = type.Name.ToUpper();
                     //  ifcbinary
				if(string.Compare(name,"IFCBOOLEAN") == 0)
				{
					bool result = false;
					if (bool.TryParse(value, out result))
						return new IfcBoolean(result);
					return new IfcBoolean( value.Contains("T"));
				}
				if (string.Compare(name, "IFCDATE") == 0)
					return new IfcDate(DateTime.Parse(value));
				if (string.Compare(name, "IFCDATETIME") == 0)
					return new IfcDateTime(DateTime.Parse(value));
				if (string.Compare(name, "IFCIDENTIFIER") == 0)
					return new IfcIdentifier(value);
				if (string.Compare(name, "IFCINTEGER") == 0)
					return new IfcInteger(int.Parse(value));
				if (string.Compare(name, "IFCLABEL") == 0)
					return new IfcLabel(value);
				if(string.Compare(name, "IFCLOGICAL") == 0)
				{
					bool result = false;
					if (bool.TryParse(value, out result))
						return new IfcLogical(result);
					return new IfcLogical(IfcLogicalEnum.UNKNOWN);
				}
				if (string.Compare(name, "IFCREAL") == 0)
					return new IfcReal(double.Parse(value, ParserSTEP.NumberFormat));
				if (string.Compare(name, "IFCTEXT") == 0)
					return new IfcText(value);
				if (string.Compare(name, "IFCURIREFERENCE") == 0)
					return new IfcURIReference(value);
			}
			return null;
		}
		internal static IfcValue parseValue(string str)
		{
			if (string.IsNullOrEmpty(str) || string.Compare(str, "$", true) == 0)
				return null;
			IfcSimpleValue sv = parseSimpleValue(str);
			if (sv != null)
				return sv;
			IfcMeasureValue mv = parseMeasureValue(str);
			if (mv != null)
				return mv;
			
			return parseDerivedMeasureValue(str);
		}
		internal static IfcValue extractValue(string keyword, string value)
		{
            if (string.IsNullOrEmpty(value))
                return null;
			Type type = BaseClassIfc.GetType(keyword);
			if (type != null)
			{
			    if(type.IsSubclassOf(typeof(IfcSimpleValue)))	
					return extractSimpleValue(type, value);
			    if(type.IsSubclassOf(typeof(IfcMeasureValue)))	
					return extractMeasureValue(type, value);
			    if(type.IsSubclassOf(typeof(IfcDerivedMeasureValue)))	
					return extractDerivedMeasureValue(type, value);
				if (type == typeof(IfcSpecularExponent))
					return new IfcSpecularExponent(double.Parse(value, ParserSTEP.NumberFormat));
				if (type == typeof(IfcSpecularRoughness))
					return new IfcSpecularRoughness(double.Parse(value, ParserSTEP.NumberFormat));
			}
			return null;
		}
		internal static bool TryGetDouble(IfcValue v, out double val)
		{
			IfcReal r = v as IfcReal;
			if (r != null)
			{
				val = r.Magnitude;
				return true;
			}
			IfcInteger i = v as IfcInteger;
			if (i != null)
			{
				val = i.Magnitude;
				return true;
			}
			IfcPositiveLengthMeasure plm = v as IfcPositiveLengthMeasure;
			if (plm != null)
			{
				val = plm.mValue;
				return true;
			}
			IfcDynamicViscosityMeasure dvm = v as IfcDynamicViscosityMeasure;
			if (dvm != null)
			{
				val = dvm.mValue;
				return true;
			}
			IfcMassDensityMeasure mdm = v as IfcMassDensityMeasure;
			if (mdm != null)
			{
				val = mdm.mValue;
				return true;
			}
			IfcModulusOfElasticityMeasure mem = v as IfcModulusOfElasticityMeasure;
			if (mem != null)
			{
				val = mem.mValue;
				return true;
			}
			IfcPositiveRatioMeasure prm = v as IfcPositiveRatioMeasure;
			if (prm != null)
			{
				val = prm.mValue;
				return true;
			}
			IfcThermalExpansionCoefficientMeasure tec = v as IfcThermalExpansionCoefficientMeasure;
			if (tec != null)
			{
				val = tec.mValue;
				return true;
			}
			val = 0;
			return false;
		}

		public static string IdentifyIfcClass(string className, out string predefinedConstant)
		{
			predefinedConstant = "";
			if (string.IsNullOrEmpty(className))
				return "";
			int indexOf = className.IndexOf('.');
			int	index = className.IndexOf('\\');
			if (indexOf < 0)
				indexOf = index;
			else if (index > 0)
				indexOf = Math.Min(indexOf, index);
			index = className.IndexOf('/');
			if (indexOf < 0)
				indexOf = index;
			else if (index > 0)
				indexOf = Math.Min(indexOf, index);
			index = className.IndexOf(":");
			if (indexOf < 0)
				indexOf = index;
			else if (index > 0)
				indexOf = Math.Min(indexOf, index);

			string result = className;
			if (indexOf > 0)
			{
				result = className.Substring(0, indexOf).Trim();
				string remainder = predefinedConstant = className.Substring(indexOf + 1);
				indexOf = remainder.IndexOf('(');
				if (indexOf > 0)
				{
					int startIndex = remainder.IndexOf('.'), endIndex = remainder.IndexOf(')');
					if (startIndex < indexOf)
						startIndex = indexOf;
					else
						endIndex = remainder.IndexOf('.', startIndex + 1);

					predefinedConstant = remainder.Substring(startIndex + 1, endIndex - startIndex - 1).Trim();
				}
			}

			if (result.EndsWith("Type"))
				result = result.Substring(0, result.Length - 4);
			else if (result.EndsWith("TypeEnum"))
				result = result.Substring(0, result.Length - 8);

			return result;
		}


		//http://madskristensen.net/post/A-shorter-and-URL-friendly-GUID.aspx
		/// <summary>
		/// Conversion methods between an IFC 
		/// encoded GUID string and a .NET GUID.
		/// This is a translation of the C code 
		/// found here: 
		/// http://www.iai-tech.org/ifc/IFC2x3/TC1/html/index.htm
		/// </summary>
		/// 

		#region Private Members
		/// <summary>
		/// The replacement table
		/// </summary>
		private static readonly char[] base64Chars = new char[]
    { '0','1','2','3','4','5','6','7','8','9'
    , 'A','B','C','D','E','F','G','H','I','J'
    , 'K','L','M','N','O','P','Q','R','S','T'
    , 'U','V','W','X','Y','Z','a','b','c','d'
    , 'e','f','g','h','i','j','k','l','m','n'
    , 'o','p','q','r','s','t','u','v','w','x'
    , 'y','z','_','$' };

		/// <summary>
		/// Conversion of an integer into characters 
		/// with base 64 using the table base64Chars
		/// </summary>
		/// <param name="number">The number to convert</param>
		/// <param name="result">The result char array to write to</param>
		/// <param name="start">The position in the char array to start writing</param>
		/// <param name="len">The length to write</param>
		/// <returns></returns>
		static void cv_to_64(uint number, ref char[] result, int start, int len)
		{
			uint act;
			int iDigit, nDigits;

			Debug.Assert(len <= 4);
			act = number;
			nDigits = len;

			for (iDigit = 0; iDigit < nDigits; iDigit++)
			{
				result[start + len - iDigit - 1] = base64Chars[(int)(act % 64)];
				act /= 64;
			}
			Debug.Assert(act == 0, "Logic failed, act was not null: " + act.ToString());
			return;
		}

		/// <summary>
		/// The reverse function to calculate 
		/// the number from the characters
		/// </summary>
		/// <param name="str">The char array to convert from</param>
		/// <param name="start">Position in array to start read</param>
		/// <param name="len">The length to read</param>
		/// <returns>The calculated nuber</returns>
		static uint cv_from_64(char[] str, int start, int len)
		{
			int i, j, index;
			uint res = 0;
			Debug.Assert(len <= 4);

			for (i = 0; i < len; i++)
			{
				index = -1;
				for (j = 0; j < 64; j++)
				{
					if (base64Chars[j] == str[start + i])
					{
						index = j;
						break;
					}
				}
				Debug.Assert(index >= 0);
				res = res * 64 + ((uint)index);
			}
			return res;
		}
		#endregion // Private Members

		#region Conversion Methods
		/// <summary>
		/// Reconstruction of the GUID 
		/// from an IFC GUID string (base64)
		/// </summary>
		/// <param name="guid">The GUID string to convert. Must be 22 characters int</param>
		/// <returns>GUID correspondig to the string</returns>
		public static Guid DecodeGlobalID(string guid)
		{
			if (string.IsNullOrEmpty(guid))
				return Guid.Empty;
			try
			{
				if (guid.Length == 22)
				{
					uint[] num = new uint[6];
					char[] str = guid.ToCharArray();
					int n = 2, pos = 0, i;
					for (i = 0; i < 6; i++)
					{
						num[i] = cv_from_64(str, pos, n);
						pos += n; n = 4;
					}
					int a = (int)((num[0] * 16777216 + num[1]));
					short b = (short)(num[2] / 256);
					short c = (short)((num[2] % 256) * 256 + num[3] / 65536);
					byte[] d = new byte[8];
					d[0] = Convert.ToByte((num[3] / 256) % 256);
					d[1] = Convert.ToByte(num[3] % 256);
					d[2] = Convert.ToByte(num[4] / 65536);
					d[3] = Convert.ToByte((num[4] / 256) % 256);
					d[4] = Convert.ToByte(num[4] % 256);
					d[5] = Convert.ToByte(num[5] / 65536);
					d[6] = Convert.ToByte((num[5] / 256) % 256);
					d[7] = Convert.ToByte(num[5] % 256);

					return new Guid(a, b, c, d);
				}
			}
			catch (Exception) { }
			return Guid.Empty;
		}

		/// <summary>
		/// Conversion of a GUID to a string 
		/// representing the GUID 
		/// </summary>
		/// <param name="guid">The GUID to convert</param>
		/// <returns>IFC (base64) encoded GUID string</returns>
		public static string EncodeGuid(Guid guid)
		{
			uint[] num = new uint[6];
			char[] str = new char[22];
			int i, n;
			byte[] b = guid.ToByteArray();

			// Creation of six 32 Bit integers from the components of the GUID structure
			num[0] = (uint)(BitConverter.ToUInt32(b, 0) / 16777216);
			num[1] = (uint)(BitConverter.ToUInt32(b, 0) % 16777216);
			num[2] = (uint)(BitConverter.ToUInt16(b, 4) * 256 + BitConverter.ToUInt16(b, 6) / 256);
			num[3] = (uint)((BitConverter.ToUInt16(b, 6) % 256) * 65536 + b[8] * 256 + b[9]);
			num[4] = (uint)(b[10] * 65536 + b[11] * 256 + b[12]);
			num[5] = (uint)(b[13] * 65536 + b[14] * 256 + b[15]);

			// Conversion of the numbers into a system using a base of 64
			n = 2;
			int pos = 0;
			for (i = 0; i < 6; i++)
			{
				cv_to_64(num[i], ref str, pos, n);
				pos += n; n = 4;
			}
			return new String(str);
		}
		#endregion // Conversion Methods

		public static string HashGlobalID(string uniqueString)
		{
			System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
			byte[] hash = md5.ComputeHash(Encoding.Default.GetBytes(uniqueString));
			Guid guid = new Guid(hash);
			return EncodeGuid(guid);
		}
	 
	}
}
