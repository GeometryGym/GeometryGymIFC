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
using System.Globalization;
using System.Threading.Tasks;
using System.Diagnostics;
using GeometryGym.Ifc;

namespace GeometryGym.STEP
{
	public static class ParserSTEP 
	{
		public static NumberFormatInfo NumberFormat = null;
		static ParserSTEP()
		{
			CultureInfo ci = new CultureInfo("en-us");
			NumberFormat = (NumberFormatInfo) ci.NumberFormat.Clone();
		}

		public static string Encode(string str)
		{
			if (string.IsNullOrEmpty(str))
				return "";
			string result = "";
			int length = str.Length;
			for (int icounter = 0; icounter < length; icounter++)
			{
				char c = str[icounter];
				if (c == '\r')
				{
					if (icounter + 1 < length)
					{
						if (str[icounter + 1] == '\n' && icounter + 2 == length)
							return result;
					}
					continue;
				}
				if (c == '\n')
				{
					if (icounter + 1 == length)
						return result;
				}
				if (c == '\'')
					result += "\\X\\27"; // Alternative result += "''";
				else if (c == '\\')
					result += "\\\\";
				else
				{
					int i = (int)c;
					if (i < 32 || i > 126)
						result += "\\X2\\" + string.Format("{0:x4}", i, CultureInfo.InvariantCulture).ToUpper() + "\\X0\\";
					else
						result += c;
				}
			}
			return result;
		}
		public static string Decode(string str) 
		{
			if (string.IsNullOrEmpty(str) || str == "$")
				return "";
			int ilast = str.Length - 4, icounter = 0, strLength = str.Length;
			string result = "";
			while (icounter < ilast)
			{
				char c = str[icounter];
				if (c == '\'')
				{
					if (icounter + 1 < ilast && str[icounter + 1] == '\'')
						icounter++;
				}
				if (c == '\\')
				{
					if (icounter + 1 < strLength && str[icounter + 1] == '\\')
					{
						result += c;
						icounter++;
					}
					else if (icounter + 3 < strLength)
					{
						char c3 = str[icounter + 3], c2 = str[icounter + 2], c1 = str[icounter + 1];
						if (c2 == '\\')
						{
							if (c1 == 'S')
							{
								result += (char)((int)c3 + 128);
								icounter += 3;
							}
							else if (c1 == 'X' && strLength > icounter + 4)
							{
								string s = str.Substring(icounter + 3, 2);
								Int32 i = Convert.ToInt32(s, 16);
								Byte[] bytes = BitConverter.GetBytes(i);
								c = Encoding.Unicode.GetChars(bytes)[0];
								result += c;
								icounter += 4;
							}
							else
								result += str[icounter];
						}
						else if (c3 == '\\' && c2 == '2' && c1 == 'X')
						{
							icounter += 4;
							while (str[icounter] != '\\')
							{
								string s = str.Substring(icounter, 4);
								Int32 i = Convert.ToInt32(s, 16);
								Byte[] bytes = BitConverter.GetBytes(i);
								c = Encoding.Unicode.GetChars(bytes)[0];
								result += c;
								icounter += 4;
							}
							icounter += 3;
						}
						else if (c1 == '\\' && c2 == 'X')
						{
							if (c3 == '2')
							{
								icounter += 6;
								while (str[icounter] != '\\')
								{
									string s = str.Substring(icounter, 4);
									c = System.Text.Encoding.Unicode.GetChars(BitConverter.GetBytes(Convert.ToInt32(s, 16)))[0];
									//result += (char)();
									result += c;
									icounter += 4;
								}
								icounter += 5;
							}
							else
								result += str[icounter];
						}
						else
							result += str[icounter];
					}
					else
						result += str[icounter];
				}
				else
					result += str[icounter];
				icounter++;
			}
			while (icounter < str.Length)
				result += str[icounter++];
			return result;
		}


		internal static void GetKeyWord(string line, out int stepID, out string keyword, out string def)
		{
			stepID = 0;
			keyword = "";
			def = "";
			if (string.IsNullOrEmpty(line))
				return;
			string strLine = line.Trim();
			if (strLine.Length < 5)
				return;
			int jlast = strLine.Length, jcounter = (strLine[0] == '#' ? 1 : 0);
			char c;
			for (; jcounter < jlast; jcounter++)
			{
				c = strLine[jcounter];
				if (char.IsDigit(c))
					def += c;
				else
					break;
			}
			if (!string.IsNullOrEmpty(def))
				stepID = int.Parse(def);
			c = strLine[jcounter];
			while (c == ' ')
				c = strLine[++jcounter];
			if (strLine[jcounter] == '=')
				jcounter++;
			c = strLine[jcounter];
			while (c == ' ')
				c = strLine[++jcounter];
			if (c == '(')
				c = strLine[++jcounter];
			for (; jcounter < jlast; jcounter++)
			{
				c = strLine[jcounter];
				if (c == '(')
					break;
				keyword += c;
			}
			keyword = keyword.Trim();
			keyword = keyword.ToUpper();
			int len = strLine.Length;
			int ilast = 1;
			while (strLine[len - ilast] != ')')
			{
				ilast++;
				if (len - ilast == jcounter)
				{
					ilast = 0;
					break;
				}
				if (ilast > len)
					return;
			}
			int length = len - jcounter - ilast - 1;
			if(length > 0)
				def = strLine.Substring(jcounter + 1, len - jcounter - ilast - 1);
		}

		public static string ParseString(string str)
		{
			return (str.Length > 2 ? str.Substring(1, str.Length - 2) : (str == "''" ? "" : str));
		}	
		public static bool ParseBool(string str)
		{
			string s = str.Trim();
			if (char.ToUpper(s.Replace(".", "")[0]) == 'T')
				return true;
			return false;
		}
		public static double ParseDouble(string str)
		{
			if (string.IsNullOrEmpty(str))
				return double.NaN;
			string s = str.Trim();
			if (s[0] == '$')
				return double.NaN;
			if (s == "*")
				return 0;
			try
			{
				return double.Parse(s, NumberFormat);
			}
			catch { return double.NaN; }
		}
		public static int ParseInt(string str)
		{
			string s = str.Trim();
			if (s == "$")
				return 0;
			if (s == "*")
				return 0;
			return int.Parse(s);
		}
		public static int ParseLink(string str)
		{
			string s = str.Trim();
			if (s == "$")
				return 0;
			if (s == "*")
				return 0;
			return int.Parse(s.Substring(1));
		}

		private static char[] HexChars = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };
		public static string BinaryToString(Byte[] binary)
		{
			StringBuilder sb = new StringBuilder(binary.Length * 2 + 1);
			sb.Append("\"");
			byte b;
			int start;

			// only 8-byte multiples supported
			sb.Append("0");
			start = 0;

			for (int i = start; i < binary.Length; i++)
			{
				b = binary[i];
				sb.Append(HexChars[b / 0x10]);
				sb.Append(HexChars[b % 0x10]);
			}

			sb.Append("\"");
			return sb.ToString();
		}
		public static IfcBinary ParseBinaryString(string str)
		{
			int len = (str.Length - 3) / 2; // subtract surrounding quotes and modulus character
			byte[] array = new byte[len];
			int modulo = 0; // not used for IFC -- always byte-aligned

			int offset;
			if (str.Length % 2 == 0)
			{
				modulo = Convert.ToInt32(str[1]) + 4;
				offset = 1;

				char ch = str[2];
				array[0] = (ch >= 'A' ? (byte)(ch - 'A' + 10) : (byte)ch);
			}
			else
			{
				modulo = Convert.ToInt32((str[1] - '0')); // [0] is quote; [1] is modulo
				offset = 0;
			}

			for (int i = offset; i < len; i++)
			{
				char hi = str[i * 2 + 2 - offset];
				char lo = str[i * 2 + 3 - offset];

				byte val = (byte)(
					((hi >= 'A' ? +(int)(hi - 'A' + 10) : (int)(hi - '0')) << 4) +
					((lo >= 'A' ? +(int)(lo - 'A' + 10) : (int)(lo - '0'))));

				array[i] = val;
			}

			return new IfcBinary(array);
		}
		public static string BoolToString(bool b) { return (b ? ".T." : ".F."); }
		public static string DoubleToString(double d)
		{
			return DoubleToString(d, "{0:0.0################}");
		}
		public static string DoubleToString(double d, string format)
		{ 
			if (double.IsNaN(d) || double.IsInfinity(d))
				return "0.0";
			return String.Format(CultureInfo.InvariantCulture, format, d);
		}
		public static string DoubleExponentialString(double d) 
		{
			double abs = Math.Abs(d);
			if (abs < 1000000 && (abs < double.Epsilon || abs > 1e-7))
				return DoubleToString(d);

			return String.Format(CultureInfo.InvariantCulture, "{0:0.0################E+00}", d);
		}
		public static string DoubleOptionalToString(double d) { return (double.IsNaN(d) || double.IsInfinity(d) ? "$" : String.Format(CultureInfo.InvariantCulture, "{0:0.0################}", d)); }
		public static string IntToString(int i)
		{
			if (i == 0)
				return "*";
			if (i == int.MinValue)
				return "$";
			return i.ToString();
		}
		public static string IntOptionalToString(int i)
		{
			if (i == int.MinValue)
				return "$";
			return i.ToString();
		}
		public static string LinkToString(int link)
		{
			if (link == 0)
				return "$";
			else
				return "#" + link;
		}
		public static string ObjToLinkString(ISTEPEntity obj) { return obj == null ? "$" : "#" + obj.StepId; }
		public static string ListLinksToString(List<int> links)
		{
			if (links.Count == 0)
				return "$";
			if (links.Count > 50)
			{
				StringBuilder sb = new StringBuilder();
				sb.Append("(#" + links[0]);
				for (int icounter = 1; icounter < links.Count; icounter++)
				{
					sb.Append(",#" + links[icounter]);
				}
				return sb.ToString() + ")";
			}
			string result = "(#" + links[0];
			for (int icounter = 1; icounter < links.Count; icounter++)
				result += ",#" + links[icounter];
			return result + ")";
		}
		
		public static List<string> SplitLineFields(string s)
		{
			//string s = str.Trim(); 
			string field = "";
			List<string> fields = new List<string>();
			if (string.IsNullOrEmpty(s))
				return fields;
			int ilast = s.Length;
			int icounter = 0;
			char c = s[icounter];
			while (char.IsWhiteSpace(c))
			{
				if (++icounter >= ilast)
					break;
				c = s[icounter];
			}
			/*if (c == '(')
			{
				icounter++;
				c = s[ilast - 1];
				while (char.IsWhiteSpace(c))
				{
					ilast--;
					c = s[ilast - 1];
				}
				if (s[ilast - 1] == ')')
					ilast--;
			}*/
			for (; icounter < ilast; icounter++)
			{
				c = s[icounter];
				while (char.IsWhiteSpace(c))
				{
					if (++icounter >= ilast)
						break;
					c = s[icounter];
				}
				if (c == '\'')
				{
					field += "'";
					for (icounter++; icounter < ilast; icounter++)
					{
						c = s[icounter];
						/*	if (c == '\\')
							{
								//field += '\\';
								int incr = 0;
								decode(s.Substring(icounter), ref c, ref incr);
								//field += c;
								icounter += incr;
								/*c = s[++icounter];
								if (c == '\'')
								{
									if (s.IndexOf('\'', icounter + 1) < icounter)
										break;
								}*/
						//}
						//else 
						if (c == '\'')
						{
							if (icounter + 1 < ilast && s[icounter + 1] == '\'')
							{
								field += "'";
								icounter++;
							}
							else
								break;
						}
						field += c;
					}
					field += "'";
					if (++icounter < ilast)
						c = s[icounter];
				}
				else if (c == '(')
				{
					icounter++;
					field += "(";
					int bCounter = 0;
					for (; icounter < ilast; icounter++)
					{
						c = s[icounter];
						if (c == '\'')
						{
							field += "'";
							for (icounter++; icounter < ilast; icounter++)
							{
								c = s[icounter];
								if (c == '\'')
								{
									if (icounter + 1 < ilast && s[icounter + 1] == '\'')
									{
										field += "'";
										icounter++;
									}
									else
										break;
								}
								field += c;
							}
						}
						if (c == '(')
							bCounter++;
						if (c == ')')
						{
							if (bCounter == 0)
							{
								break;
							}
							else
								bCounter--;
						}
						field += c;
					}
					field += ")";
					if (++icounter < ilast)
						c = s[icounter];
				}
				if (icounter < ilast)
				{
					if (c == ',')
					{
						fields.Add(field);
						field = "";
					}
					else
						field += c;
				}
			}
			if (!string.IsNullOrEmpty(field))
				fields.Add(field);
			return fields;
		}
		public static List<int> SplitListSTPIntegers(string s)
		{
			List<int> result = new List<int>();
			if (s == "$")
				return result;
			int ilast = s.Length, i = 0;
			int icounter = 0;
			while (char.IsWhiteSpace(s[icounter]))
			{
				if (++icounter >= ilast)
					break;
			}
			while (char.IsWhiteSpace(s[ilast - 1]))
				ilast--;
			char c = s[icounter];
			if (c != '(')
				return result;
			for (icounter++; icounter < ilast; icounter++)
			{
				c = s[icounter];
				if (c == ',')
					continue;
				string str = "";
				str += c;
				for (icounter++; icounter < ilast; icounter++)
				{
					c = s[icounter];
					if (!char.IsDigit(c))
					{
						if (int.TryParse(str, out i))
							result.Add(i);
						break;
					}
					str += c;
				}
			}
			return result;
		}	
		public static List<int> SplitListLinks(string s)
		{
			List<int> links = new List<int>();
			//string s = str.Trim();
			if (string.IsNullOrEmpty(s) || s == "$")
				return links;

			string field = "";
			int ilast = s.Length;
			int icounter = 0;
			while (char.IsWhiteSpace(s[icounter]))
			{
				if (++icounter >= ilast)
					break;
			}
			while (char.IsWhiteSpace(s[ilast - 1]))
				ilast--;
			if (s.StartsWith("("))
			{
				icounter = 1;
				if (s[ilast - 1] == ')')
					ilast--;
			}
			char c;
			for (; icounter < ilast; icounter++)
			{
				c = s[icounter];
				if (c == '#')
				{
					for (icounter++; icounter < ilast; icounter++)
					{
						c = s[icounter];
						if (c == ',')
						{
							int i = int.Parse(field);
							links.Add(i);
							field = "";
							break;
						}
						field += c;
					}
					if (field != "")
					{
						int i = int.Parse(field);
						links.Add(i);
					}
				}
			}
			return links;
		}
		public static List<string> SplitListStrings(string s)
		{
			List<string> result = new List<string>();
			if (s == "$")
				return result;
			int ilast = s.Length;
			int icounter = 0;
			while (char.IsWhiteSpace(s[icounter]))
			{
				if (++icounter >= ilast)
					break;
			}
			while (char.IsWhiteSpace(s[ilast - 1]))
				ilast--;
			char c = s[icounter];
			if (c != '(')
				return result;
			for (icounter++; icounter < ilast; icounter++)
			{
				c = s[icounter];
				if (c == '\'')
				{
					string str = "";
					for (icounter++; icounter < ilast; icounter++)
					{
						c = s[icounter];
						if (c == '\'')
						{
							result.Add(str);
							str = "";
							break;
						}
						str += c;
					}
					if (!string.IsNullOrEmpty(str))
						result.Add(str);
				}
			}
			return result;
		}
		public static List<Tuple<double,double>> SplitListDoubleTuple(string s)
		{
			List<Tuple<double, double>> tss = new List<Tuple<double, double>>(s.Length / 10);
			if (s == "$")
				return new List<Tuple<double, double>>();
			string field = "";
			int ilast = s.Length;
			int icounter = 0;
			while (char.IsWhiteSpace(s[icounter]))
			{
				if (++icounter >= ilast)
					break;
			}
			while (char.IsWhiteSpace(s[ilast - 1]))
				ilast--;

			char c = s[icounter];
			if (c != '(')
				return new List<Tuple<double, double>>();
			for (icounter++; icounter < ilast; icounter++)
			{
				c = s[icounter];
				if (c == '(')
				{
					double i = 0, j = 0;
					for (icounter++; icounter < ilast; icounter++)
					{
						c = s[icounter];
						if (c == ',')
						{
							i = double.Parse(field, NumberFormat);
							field = "";
							break;
						}
						field += c;
					}
					for (icounter++; icounter < ilast; icounter++)
					{
						c = s[icounter];
						if (c == ')')
						{
							j = double.Parse(field, NumberFormat);
							field = "";

							tss.Add(new Tuple<double, double>(i, j));
							break;
						}
						field += c;
					}
				}
			}
			return tss;
		}
		public static List<Tuple<double, double, double>> SplitListDoubleTriple(string s)
		{
			List<Tuple<double, double ,double>> tss = new List<Tuple<double, double, double>>(s.Length / 20);
			if (s == "$")
				return new List<Tuple<double, double, double>>();
			string field = "";
			int ilast = s.Length;
			int icounter = 0;
			while (char.IsWhiteSpace(s[icounter]))
			{
				if (++icounter >= ilast)
					break;
			}
			while (char.IsWhiteSpace(s[ilast - 1]))
				ilast--;

			char c = s[icounter];
			if (c != '(')
				return new List<Tuple<double, double, double>>();
			for (icounter++; icounter < ilast; icounter++)
			{
				c = s[icounter];
				if (c == '(')
				{
					double i = 0, j = 0, k = 0;
					for (icounter++; icounter < ilast; icounter++)
					{
						c = s[icounter];
						if (c == ',')
						{
							i = double.Parse(field, NumberFormat);
							field = "";
							break;
						}
						field += c;
					}
					for (icounter++; icounter < ilast; icounter++)
					{
						c = s[icounter];
						if (c == ',')
						{
							j = double.Parse(field, NumberFormat);
							field = "";
							break;
						}
						field += c;
					}
					for (icounter++; icounter < ilast; icounter++)
					{
						c = s[icounter];
						if (c == ')')
						{
							k = double.Parse(field, NumberFormat);
							field = "";

							tss.Add(new Tuple<double, double, double>(i, j, k));
							break;
						}
						field += c;
					}
				}
			}
			return tss;
		}	
		public static List<Tuple<int, int, int>> SplitListSTPIntTriple(string s)
		{
			List<Tuple<int, int, int>> tss = new List<Tuple<int, int, int>>(100);
			if (s == "$")
				return new List<Tuple<int, int, int>>();
			string field = "";
			int ilast = s.Length;
			int icounter = 0;
			while (char.IsWhiteSpace(s[icounter]))
			{
				if (++icounter >= ilast)
					break;
			}
			while (char.IsWhiteSpace(s[ilast - 1]))
				ilast--;

			char c = s[icounter];
			if (c != '(')
				return new List<Tuple<int, int, int>>();
			for (icounter++; icounter < ilast; icounter++)
			{
				c = s[icounter];
				if (c == '(')
				{
					int i = 0, j = 0, k = 0;
					for (icounter++; icounter < ilast; icounter++)
					{
						c = s[icounter];
						if (c == ',')
						{
							i = int.Parse(field);
							field = "";
							break;
						}
						field += c;
					}
					for (icounter++; icounter < ilast; icounter++)
					{
						c = s[icounter];
						if (c == ',')
						{
							j = int.Parse(field);
							field = "";
							break;
						}
						field += c;
					}
					for (icounter++; icounter < ilast; icounter++)
					{
						c = s[icounter];
						if (c == ')')
						{
							k = int.Parse(field);
							field = "";

							tss.Add(new Tuple<int, int, int>(i, j, k));
							break;
						}
						field += c;
					}
				}
			}

			return tss;
		}

		private static void progressToNext(string s, ref int pos, int len)
		{
			if (pos < len)
			{
				char c = s[pos++];
				while (c != ',' && c != ')')
				{
					if (pos == len)
						return;
					c = s[pos++];
				}
			}
		}

		public static string StripField(string s, ref int pos, int len)
		{
			if (pos >= len)
				return "";
			int icounter = pos;
			while (char.IsWhiteSpace(s[icounter]))
			{
				icounter++;
				if (icounter == len)
					break;
			}
			string result = "";
			char c = s[icounter++];
			if(c == '(')
			{
				--icounter;
				result = striplist(s, ref icounter, len);
				pos = ++icounter;
				return result;
			}
			while (c != ',' && c != ')')
			{
				if (c == '\'')
				{
					--icounter;
					result += stripstring(s, ref icounter, len);
				}
				else if (c == '(')
				{
					--icounter;
					result += striplist(s, ref icounter, len);
				}
				else
					result += c;
				if (icounter >= len)
					break;
				c = s[icounter++];
				if (icounter == len)
				{
					result += c;
					break;
				}
			}
			pos = icounter-1;
			progressToNext(s, ref pos, len);
			return result;
		}
		public static bool StripBool(string s, ref int pos, int len)
		{
			int icounter = pos;
			while (char.IsWhiteSpace(s[icounter]))
			{
				icounter++;
				if (icounter == len) 
					break;
			}
			if (s[icounter] == '$')
			{
				pos = icounter + 1;
				progressToNext(s, ref pos, len);
				return false;
			}
			if (s[icounter++] != '.')
				throw new Exception("Unrecognized format!");
			char c = char.ToUpper(s[icounter]);
			pos = icounter + 2;
			progressToNext(s, ref pos, len);
			return c == 'T';
		}
		private static bool isDoubleChar(char c)
		{
			return char.IsDigit(c) || c == '.' || c == 'e' || c == 'E' || c == '-' || c == '+';
		}
		public static double StripDouble(string s, ref int pos, int len)
		{
			if (pos >= len)
				return double.NaN;
			int icounter = pos;
			while (char.IsWhiteSpace(s[icounter]))
			{
				icounter++;
				if (icounter == len)
					break;
			}
			if (s[icounter] == '$')
			{
				pos = icounter + 1;
				progressToNext(s, ref pos, len);
				return double.NaN;
			}

			string str = "";
			char c = s[icounter];
			while (isDoubleChar(c))
			{
				str += c;
				if (++icounter == len)
					break;
				c = s[icounter];
			}
			if (icounter < len)
			{
				c = s[icounter++];
				while (c != ',' && c != ')')
				{
					if (icounter == len)
						break;
					c = s[icounter++];
				}
			}
			pos = icounter;
			double result = 0;
			if (double.TryParse(str, NumberStyles.Any, NumberFormat, out result))
				return result;
			return double.NaN;
		}
		public static int StripInt(string s, ref int pos, int len)
		{
			int icounter = pos;
			while (char.IsWhiteSpace(s[icounter]))
			{
				icounter++;
				if (icounter == len)
					break;
			}
			if (s[icounter] == '*')
			{
				pos = icounter+1;
				progressToNext(s, ref pos, len);
				return 0;
			}
			if (s[icounter] == '$')
			{
				pos = icounter + 1;
				progressToNext(s, ref pos, len);
				return int.MinValue;
			}

			string str = "";
			while (char.IsDigit(s[icounter]) || s[icounter] == '-')
			{
				str += s[icounter++];
				if (icounter == len)
					break;
			}
			if (icounter < len)
			{
				char c = s[icounter++];
				while (c != ',' && c != ')')
				{
					if (icounter == len)
						break;
					c = s[icounter++];
				}
			}
			pos = icounter;
			return int.Parse(str);
		}
		public static int StripLink(string s, ref int pos, int len)
		{
			if (pos >= len)
				return 0;
			int icounter = pos;
			while (char.IsWhiteSpace(s[icounter]))
			{
				icounter++;
				if (icounter == len)
					break;
			}
			if (s[icounter] == '$' || s[icounter] == '*')
			{
				pos = icounter + 1;
				progressToNext(s, ref pos, len);
				return 0;
			}

			if (s[icounter++] != '#')
			{
				progressToNext(s, ref pos, len);
				return 0;	
			}
			string str = "";
			while (char.IsDigit(s[icounter]))
			{
				str += s[icounter++];
				if (icounter == len)
					break;
			}
			if (icounter < len)
			{
				char c = s[icounter++];
				while (c != ',' && c != ')')
				{
					if (icounter == len)
						break;
					c = s[icounter++];
				}
			}
			pos = icounter;
			return int.Parse(str);
		}
		public static List<int> StripListLink(string s, ref int pos, int len)
		{
			int icounter = pos;
			while (char.IsWhiteSpace(s[icounter]))
			{
				icounter++;
				if (icounter == len)
					break;
			}
			if (s[icounter] == '$')
			{
				pos = icounter + 1;
				progressToNext(s, ref pos, len);
				return new List<int>();
			}
			while (char.IsWhiteSpace(s[icounter]))
				icounter++;
			if (s[icounter++] != '(')
				throw new Exception("Unrecognized format!");
			while (char.IsWhiteSpace(s[icounter]))
				icounter++;
			if (s[icounter] == ')')
			{
				pos = icounter + 1;
				progressToNext(s, ref pos, len);
				return new List<int>();
			}
			if (s[icounter++] != '#')
				throw new Exception("Unrecognized format!");
			List<int> result = new List<int>();
			while (s[icounter] != ')')
			{
				string str = "";
				while (char.IsDigit(s[icounter]))
				{
					str += s[icounter++];
					if (icounter == len)
						break;
				}
				result.Add(int.Parse(str));
				if (icounter == len)
					break;
				while (char.IsWhiteSpace(s[icounter]))
					icounter++;
				if (s[icounter] != ')')
				{
					if (s[icounter++] != ',')
						throw new Exception("Unrecognized format!");
					while (char.IsWhiteSpace(s[icounter]))
						icounter++;
					if (s[icounter++] != '#')
						throw new Exception("Unrecognized format!");
				}
			}
			pos = icounter + 1;
			progressToNext(s, ref pos, len);
			return result;
		}
		public static List<List<int>> StripListListLink(string s, ref int pos, int len)
		{
			int icounter = pos;
			if (string.IsNullOrEmpty(s) || pos == len)
				return new List<List<int>>();
			while (char.IsWhiteSpace(s[icounter]))
			{
				icounter++;
				if (icounter == len)
					break;
			}
			if (s[icounter] == '$')
			{
				pos = icounter + 1;
				progressToNext(s, ref pos, len);
				return new List<List<int>>();
			}
			while (char.IsWhiteSpace(s[icounter]))
				icounter++;
			if (s[icounter++] != '(')
				throw new Exception("Unrecognized format!");
			while (char.IsWhiteSpace(s[icounter]))
				icounter++;

			List<List<int>> result = new List<List<int>>();
			while (s[icounter] != ')')
			{
				if (s[icounter++] != '(')
					throw new Exception("Unrecognized format!");
				while (char.IsWhiteSpace(s[icounter]))
					icounter++;
				List<int> ints = new List<int>();

				while (s[icounter] != ')')
				{
					string str = "";
					char c = s[icounter++];
					if(c != '#')
						throw new Exception("Unrecognized format!");
					c = s[icounter];
					while (char.IsDigit(c))
					{
						str += c;
						c = s[++icounter];
						if (icounter == len)
							break;
					}
					ints.Add(int.Parse(str));
					if (icounter == len)
						break;
					while (char.IsWhiteSpace(s[icounter]))
						icounter++;
					if (s[icounter] != ')')
					{
						if (s[icounter++] != ',')
							throw new Exception("Unrecognized format!");
						while (char.IsWhiteSpace(s[icounter]))
							icounter++;
					}
				}
				result.Add(ints);
				if (icounter == len)
					break;
				while (char.IsWhiteSpace(s[icounter]))
					icounter++;
				if (s[icounter++] != ')')
						throw new Exception("Unrecognized format!");
				while (char.IsWhiteSpace(s[icounter]))
					icounter++;
				if (s[icounter] != ')')
				{
					if (s[icounter++] != ',')
						throw new Exception("Unrecognized format!");
					while (char.IsWhiteSpace(s[icounter]))
						icounter++;
				}
			}
			pos = icounter + 1;
			progressToNext(s, ref pos, len);
			return result;
		}
		public static List<int> StripListInt(string s, ref int pos, int len)
		{
			int icounter = pos;
			if (string.IsNullOrEmpty(s) || pos == len)
				return new List<int>();
			while (char.IsWhiteSpace(s[icounter]))
			{
				icounter++;
				if (icounter == len)
					break;
			}
			char c = s[icounter];
			if (c == '$')
			{
				pos = icounter + 1;
				progressToNext(s, ref pos, len);
				return new List<int>();
			}
			while (char.IsWhiteSpace(s[icounter]))
				icounter++;
			if (s[icounter++] != '(')
				throw new Exception("Unrecognized format!");
			while (char.IsWhiteSpace(s[icounter]))
				icounter++;
			
			List<int> result = new List<int>();
			while (s[icounter] != ')')
			{
				string str = "";
				c = s[icounter];
				while (c == '-' || char.IsDigit(c))
				{
					str += c;
					if (++icounter == len)
						break;
					c = s[icounter];
				}
				result.Add(int.Parse(str));
				if (icounter == len)
					break;
				while (char.IsWhiteSpace(s[icounter]))
					icounter++;
				if (s[icounter] != ')')
				{
					if (s[icounter++] != ',')
						throw new Exception("Unrecognized format!");
					while (char.IsWhiteSpace(s[icounter]))
						icounter++;
				}
			}
			pos = icounter + 1;
			progressToNext(s, ref pos, len);
			return result;
		}
		public static List<List<int>> StripListListInt(string s, ref int pos, int len)
		{
			int icounter = pos;
			if (string.IsNullOrEmpty(s) || pos == len)
				return new List<List<int>>();
			while (char.IsWhiteSpace(s[icounter]))
			{
				icounter++;
				if (icounter == len)
					break;
			}
			if (s[icounter] == '$')
			{
				pos = icounter+ 1;
				progressToNext(s, ref pos, len);
				return new List<List<int>>();
			}
			while (char.IsWhiteSpace(s[icounter]))
				icounter++;
			if (s[icounter++] != '(')
				throw new Exception("Unrecognized format!");
			while (char.IsWhiteSpace(s[icounter]))
				icounter++;

			List<List<int>> result = new List<List<int>>();
			while (s[icounter] != ')')
			{
				if (s[icounter++] != '(')
					throw new Exception("Unrecognized format!");
				while (char.IsWhiteSpace(s[icounter]))
					icounter++;
				List<int> ints = new List<int>();

				while (s[icounter] != ')')
				{
					string str = "";
					char c = s[icounter];
					while (char.IsDigit(c))
					{
						str += c;
						c = s[++icounter];
						if (icounter == len)
							break;
					}
					ints.Add(int.Parse(str));
					if (icounter == len)
						break;
					while (char.IsWhiteSpace(s[icounter]))
						icounter++;
					if (s[icounter] != ')')
					{
						if (s[icounter++] != ',')
							throw new Exception("Unrecognized format!");
						while (char.IsWhiteSpace(s[icounter]))
							icounter++;
					}
				}
				result.Add(ints);
				if (icounter == len)
					break;
				while (char.IsWhiteSpace(s[icounter]))
					icounter++;
				if (s[icounter++] != ')')
						throw new Exception("Unrecognized format!");
				while (char.IsWhiteSpace(s[icounter]))
					icounter++;
				if (s[icounter] != ')')
				{
					if (s[icounter++] != ',')
						throw new Exception("Unrecognized format!");
					while (char.IsWhiteSpace(s[icounter]))
						icounter++;
				}
			}
			pos = icounter + 1;
			progressToNext(s, ref pos, len);
			return result;
		}
		public static Tuple<int, int, int>[] StripListSTPIntTriple(string s, ref int pos, int len)
		{
			int icounter = pos;
			if (string.IsNullOrEmpty(s) || pos == len)
				return new Tuple<int, int, int>[0];
			while (char.IsWhiteSpace(s[icounter]))
			{
				icounter++;
				if (icounter == len)
					break;
			}
			if (s[icounter] == '$')
			{
				pos = icounter + 1;
				progressToNext(s, ref pos, len);
				return new Tuple<int, int, int>[0];
			}
			while (char.IsWhiteSpace(s[icounter]))
			{
				if (++icounter >= len)
					break;
			}
			char c = s[icounter++];
			if (c != '(')
				throw new Exception("Unrecognized format!");
			while (char.IsWhiteSpace(s[icounter]))
				icounter++;
			
			List<Tuple<string, string, string>> tripleStrings = new List<Tuple<string, string, string>>(100);
			while (s[icounter] != ')')
			{
				while (char.IsWhiteSpace(s[icounter]))
					icounter++;
				if (s[icounter++] != '(')
					throw new Exception("Unrecognized format!");
				while (char.IsWhiteSpace(s[icounter]))
					icounter++;

				int fieldPos = icounter;
				while (s[icounter] != ',')
					icounter++;
				string field1 = s.Substring(fieldPos, icounter - fieldPos);
				icounter++;
				while (char.IsWhiteSpace(s[icounter]))
					icounter++;

				fieldPos = icounter;
				while (s[icounter] != ',')
					icounter++;
				string field2 = s.Substring(fieldPos, icounter - fieldPos);
				icounter++;
				while (char.IsWhiteSpace(s[icounter]))
					icounter++;

				fieldPos = icounter;
				while (s[icounter] != ')')
					icounter++;
				string field3 = s.Substring(fieldPos, icounter - fieldPos);
				tripleStrings.Add(new Tuple<string, string, string>(field1, field2, field3));
				icounter++;
				while (char.IsWhiteSpace(s[icounter]))
					icounter++;
				if (s[icounter] == ')')
					break;
				if(s[icounter] != ',')
					throw new Exception("Unrecognized format!");
				icounter++;
				while (char.IsWhiteSpace(s[icounter]))
					icounter++;
				if (s[icounter] != '(')
					throw new Exception("Unrecognized format!");
			}
			pos = icounter + 1;
			progressToNext(s, ref pos, len);

			int upperBound = tripleStrings.Count;

			Tuple<int, int, int>[] result = new Tuple<int, int, int>[upperBound];
			Parallel.For(0, upperBound, x =>
			{
				try
				{
					Tuple<string, string, string> triple = tripleStrings[x];
					result[x] = new Tuple<int, int, int>(int.Parse(triple.Item1), int.Parse(triple.Item2), int.Parse(triple.Item3));
				}
				catch (Exception) { }
			});
			return result;
		}
		public static List<double> StripListDouble(string s, ref int pos, int len)
		{
			int icounter = pos;
			if (string.IsNullOrEmpty(s) || pos == len)
				return new List<double>();
			while (char.IsWhiteSpace(s[icounter]))
			{
				icounter++;
				if (icounter == len)
					break;
			}
			if (s[icounter] == '$')
			{
				pos = icounter + 1;
				progressToNext(s, ref pos, len);
				return new List<double>();
			}
			while (char.IsWhiteSpace(s[icounter]))
				icounter++;
			if (s[icounter++] != '(')
				throw new Exception("Unrecognized format!");
			while (char.IsWhiteSpace(s[icounter]))
				icounter++;

			List<double> result = new List<double>();
			while (s[icounter] != ')')
			{
				if (s[icounter] == ',')
					icounter++;
				double d = StripDouble(s, ref icounter,len);
				icounter--;
				if (double.IsNaN(d))
					break;
				result.Add(d);
			}
			pos = icounter + 1;
			progressToNext(s, ref pos, len);
			return result;
		}
		public static List<List<double>> StripListListDouble(string s, ref int pos, int len)
		{
			int icounter = pos;
			if (string.IsNullOrEmpty(s) || pos == len)
				return new List<List<double>>();
			while (char.IsWhiteSpace(s[icounter]))
			{
				icounter++;
				if (icounter == len)
					break;
			}
			if (s[icounter] == '$')
			{
				pos = icounter+1;
				progressToNext(s, ref pos, len);
				return new List<List<double>>();
			}
			while (char.IsWhiteSpace(s[icounter]))
				icounter++;
			if (s[icounter++] != '(')
				throw new Exception("Unrecognized format!");
			while (char.IsWhiteSpace(s[icounter]))
				icounter++;

			List<List<double>> result = new List<List<double>>();
			while (s[icounter] != ')')
			{
				if (s[icounter++] != '(')
					throw new Exception("Unrecognized format!");
				while (char.IsWhiteSpace(s[icounter]))
					icounter++;
				List<double> doubles = new List<double>();

				while (s[icounter] != ')')
				{
					string str = "";
					char c = s[icounter];
					while (isDoubleChar(c))
					{
						str += c;
						c = s[++icounter];
						if (icounter == len)
							break;
					}
					doubles.Add(double.Parse(str, NumberFormat));
					if (icounter == len)
						break;
					while (char.IsWhiteSpace(s[icounter]))
						icounter++;
					if (s[icounter] != ')')
					{
						if (s[icounter++] != ',')
							throw new Exception("Unrecognized format!");
						while (char.IsWhiteSpace(s[icounter]))
							icounter++;
					}
				}
				result.Add(doubles);
				if (icounter == len)
					break;
				while (char.IsWhiteSpace(s[icounter]))
					icounter++;
				if (s[icounter++] != ')')
						throw new Exception("Unrecognized format!");
				if (icounter == len)
					break;
				while (char.IsWhiteSpace(s[icounter]))
					icounter++;
				if (s[icounter] != ')')
				{
					if (s[icounter++] != ',')
						throw new Exception("Unrecognized format!");
					while (char.IsWhiteSpace(s[icounter]))
						icounter++;
					
				}
			}
			pos = icounter + 1;
			progressToNext(s, ref pos, len);
			return result;
		}
		public static List<Tuple<double,double,double>> StripListTripleDouble(string s, ref int pos, int len)
		{
			int icounter = pos;
			if (string.IsNullOrEmpty(s) || pos == len)
				return new List<Tuple<double,double,double>>();
			while (char.IsWhiteSpace(s[icounter]))
			{
				icounter++;
				if (icounter == len)
					break;
			}
			if (s[icounter] == '$')
			{
				pos = icounter + 1;
				progressToNext(s, ref pos, len);
				return new List<Tuple<double, double, double>>();
			}
			while (char.IsWhiteSpace(s[icounter]))
				icounter++;
			if (s[icounter++] != '(')
				throw new Exception("Unrecognized format!");
			while (char.IsWhiteSpace(s[icounter]))
				icounter++;

			NumberFormatInfo numberFormat = NumberFormat;
			List<Tuple<double, double, double>> result = new List<Tuple<double, double, double>>();
			while (s[icounter] != ')')
			{
				if (s[icounter++] != '(')
					throw new Exception("Unrecognized format!");
				while (char.IsWhiteSpace(s[icounter]))
					icounter++;

				while (s[icounter] != ')')
				{
					string str = "";
					char c = s[icounter];
					while (isDoubleChar(c))
					{
						str += c;
						c = s[++icounter];
						if (icounter == len)
							break;
					}
					double x  = double.Parse(str, numberFormat);
					if (icounter == len)
						break;
					while (char.IsWhiteSpace(s[icounter]))
						icounter++;
					if (s[icounter++] != ',')
						throw new Exception("Unrecognized format!");
					while (char.IsWhiteSpace(s[icounter]))
						icounter++;
					str = "";
					c = s[icounter];
					while (isDoubleChar(c))
					{
						str += c;
						c = s[++icounter];
						if (icounter == len)
							break;
					}
					double y = double.Parse(str, numberFormat);
					while (char.IsWhiteSpace(s[icounter]))
						icounter++;
					if (s[icounter++] != ',')
						throw new Exception("Unrecognized format!");
					while (char.IsWhiteSpace(s[icounter]))
						icounter++;
					str = "";
					c = s[icounter];
					while (isDoubleChar(c))
					{
						str += c;
						c = s[++icounter];
						if (icounter == len)
							break;
					}
					double z = double.Parse(str, numberFormat);
					result.Add(new Tuple<double,double,double>(x, y, z));
				}
				if (icounter == len)
					break;
				while (char.IsWhiteSpace(s[icounter]))
					icounter++;
				if (s[icounter++] != ')')
					throw new Exception("Unrecognized format!");
				if (icounter == len)
					break;
				while (char.IsWhiteSpace(s[icounter]))
					icounter++;
				if (s[icounter] != ')')
				{
					if (s[icounter++] != ',')
						throw new Exception("Unrecognized format!");
					while (char.IsWhiteSpace(s[icounter]))
						icounter++;

				}
			}
			pos = icounter + 1;
			progressToNext(s, ref pos, len);
			return result;
		}
		public static string StripString(string s, ref int pos, int len)
		{
			if (pos >= len)
				return "";
			int icounter = pos;
			while (char.IsWhiteSpace(s[icounter]))
			{
				icounter++;
				if (icounter == len)
					break;
			}
			if (s[icounter] == '$' || s[icounter] == '*')
			{
				pos = icounter + 1;
				progressToNext(s, ref pos, len);
				return "";
			}
			string result = stripstring(s, ref icounter, len);
			pos = icounter;
			progressToNext(s, ref pos, len);
			return result;
		}
		private static string stripstring(string s, ref int pos, int len)
		{
			int icounter = pos;
			string result = "";
			char c = s[icounter++];
			if (c != '\'')
			{
				if(c == 'I')
				{
					icounter++;
					while(s[icounter] != '\'' && icounter + 1 < len)
						icounter++;
					result = stripstring(s, ref icounter, len);
					while (s[icounter] != ')' && icounter + 1 < len)
						icounter++;
					pos = icounter + 1;
					return result;
				}
				throw new Exception("Unrecognized format!");
			}
			while (icounter < len)
			{
				c = s[icounter];
				if (c == '\'')
				{
					if (icounter + 1 < len)
					{
						if (s[icounter + 1] != '\'')
							break;
						result += '\'';
						icounter++;
					}
					else
						break;
				}
				result += c;
				icounter++;
			}
			pos = icounter + 1;
			return result;
		}
		private static string striplist(string s, ref int pos, int len)
		{
			int icounter = pos;
			string result = "(";
			if (s[icounter++] != '(')
				throw new Exception("Unrecognized format!");
			while (icounter < len)
			{
				char c = s[icounter];
				if (c == '\'')
					result += "'" + stripstring(s, ref icounter, len) + "'";
				else if (c == '(')
					result += striplist(s, ref icounter, len);
				else if (c == ')')
				{
					result += c;
					break;
				}
				else
				{
					result += c;
					icounter++;
				}
			}
			pos = icounter + 1;
			return result;
		}

		public static string StripComments(string s)
		{
			string result = System.Text.RegularExpressions.Regex.Replace(s, "/\\*.*?\\*/", "");
			return result;
		}

		internal static string offsetSTEPRecords(string line, int offset)
		{
			string newline = "";
			int ilast = line.Length - 1;
			for (int icounter = 0; icounter < line.Length; icounter++)
			{
				char c = line[icounter];
				if (c == '\'')
				{
					newline += "'";
					while (icounter < ilast)
					{
						c = line[icounter + 1];
						newline += c;
						icounter++;
						if (c == '\'')
							break;
					}

				}
				else if (line[icounter] == '#')
				{
					newline += "#";
					string str = "";
					while (icounter < ilast)
					{
						c = line[icounter + 1];
						if (char.IsDigit(c))
							str += c;
						else
							break;
						icounter++;
					}
					int record = int.Parse(str) + offset;
					newline += record.ToString();
				}
				else
					newline += c;
			}
			return newline;
		}
	}
}
