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
			string s = str.Trim();
			if (s == "$")
				return double.NaN;
			if (s == "*")
				return 0;
			return double.Parse(s, NumberFormat);
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

		public static string BoolToString(bool b) { return (b ? ".T." : ".F."); }
		public static string DoubleToString(double d) { return String.Format("{0:0.0################}", d); }
		public static string DoubleOptionalToString(double i) { return (double.IsNaN(i) ? "$" : String.Format("{0:0.0################}", i)); }
		public static string IntToString(int i)
		{
			if (i == 0)
				return "*";
			return i.ToString();
		}
		public static string IntOptionalToString(int i)
		{
			if (i == 0)
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
		public static Tuple<double, double>[] SplitListDoubleTuple(string s)
		{
			List<Tuple<double, double>> tss = new List<Tuple<double, double>>(100);
			if (s == "$")
				return new Tuple<double, double>[0];
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
				return new Tuple<double, double>[0];
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
			return tss.ToArray();
		}
		public static Tuple<double, double, double>[] SplitListDoubleTriple(string s)
		{
			List<Tuple<double, double, double>> tss = new List<Tuple<double, double, double>>(100);
			if (s == "$")
				return new Tuple<double, double, double>[0];
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
				return new Tuple<double, double, double>[0];
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
			return tss.ToArray();
		}	
		public static Tuple<int, int, int>[] SplitListSTPIntTriple(string s)
		{
			List<Tuple<int, int, int>> tss = new List<Tuple<int, int, int>>(100);
			if (s == "$")
				return new Tuple<int, int, int>[0];
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
				return new Tuple<int, int, int>[0];
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

			return tss.ToArray();
		}

		public static string StripField(string s, ref int pos)
		{
			int icounter = pos, len = s.Length;
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
				result = striplist(s, ref icounter);
				icounter++;
				return result;
			}
			while (c != ',' && c != ')')
			{
				if (c == '\'')
				{
					--icounter;
					result += stripstring(s, ref icounter);
				}
				else if (c == '(')
				{
					--icounter;
					result += striplist(s, ref icounter);
				}
				else
					result += c;
				if (icounter == len)
					break;
				c = s[icounter++];
				if (icounter == len)
					break;
			}
			pos = icounter;
			return result;
		}
		public static bool StripBool(string s, ref int pos)
		{
			int icounter = pos, len = s.Length;
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
				return false;
			}
			if (s[icounter++] != '.')
				throw new Exception("Unrecognized format!");
			char c = char.ToUpper(s[icounter++]);
			pos = icounter + 2;
			return c == 'T';
		}
		
		public static double StripDouble(string s, ref int pos)
		{
			int icounter = pos, len = s.Length;
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
				return double.NaN;
			}


			string str = "";
			char c = s[icounter];
			while (char.IsDigit(c) || c == '.' || char.ToLower(c) == 'e' || c == '-')
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
			return double.Parse(str, NumberFormat);
		}
		public static int StripInt(string s, ref int pos)
		{
			int icounter = pos, len = s.Length;
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
				return int.MinValue;
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
				while (s[icounter++] != ',')
				{
					if (icounter == len)
						break;
				}
			}
			pos = icounter;
			return int.Parse(str);
		}
		public static int StripLink(string s, ref int pos)
		{
			int icounter = pos, len = s.Length;
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
				return 0;
			}

			if (s[icounter++] != '#')
				throw new Exception("Unrecognized format!");
			string str = "";
			while (char.IsDigit(s[icounter]))
			{
				str += s[icounter++];
				if (icounter == len)
					break;
			}
			if (icounter < len)
			{
				while (s[icounter++] != ',')
				{
					if (icounter == len)
						break;
				}
			}
			pos = icounter;
			return int.Parse(str);
		}
		public static List<int> StripListLink(string s, ref int pos)
		{
			int icounter = pos, len = s.Length;
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
				return new List<int>();
			}
			while (char.IsWhiteSpace(s[icounter]))
				icounter++;
			if (s[icounter++] != '(')
				throw new Exception("Unrecognized format!");
			while (char.IsWhiteSpace(s[icounter]))
				icounter++;
			if (s[icounter] == ')')
				return new List<int>();
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
			pos = icounter + 2;
			return result;
		}
		public static List<List<int>> StripListListLink(string s, ref int pos)
		{
			int icounter = pos, len = s.Length;
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
				if (++icounter < len)
				{
					while (s[icounter++] != ',')
					{
						if (icounter == len)
							break;
					}
				}
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
			pos = icounter + 2;
			return result;
		}
		public static List<int> StripListInt(string s, ref int pos)
		{
			int icounter = pos, len = s.Length;
			if (string.IsNullOrEmpty(s) || pos == len)
				return new List<int>();
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
				}
			}
			pos = icounter + 2;
			return result;
		}
		public static List<List<int>> StripListListInt(string s, ref int pos)
		{
			int icounter = pos, len = s.Length;
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
				if (++icounter < len)
				{
					while (s[icounter++] != ',')
					{
						if (icounter == len)
							break;
					}
				}
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
			pos = icounter + 2;
			return result;
		}
		public static List<double> StripListDouble(string s, ref int pos)
		{
			int icounter = pos, len = s.Length;
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
				if (++icounter < len)
				{
					while (s[icounter++] != ',')
					{
						if (icounter == len)
							break;
					}
				}
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
				double d = StripDouble(s, ref icounter);
				icounter--;
				if (double.IsNaN(d))
					break;
				result.Add(d);
			}
			pos = icounter + 2;
			return result;
		}
		public static List<List<double>> StripListListDouble(string s, ref int pos)
		{
			int icounter = pos, len = s.Length;
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
				if (++icounter < len)
				{
					while (s[icounter++] != ',')
					{
						if (icounter == len)
							break;
					}
				}
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
					while (char.IsDigit(c) || c == '.' || c == '-')
					{
						str += c;
						c = s[++icounter];
						if (icounter == len)
							break;
					}
					doubles.Add(double.Parse(str));
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
			pos = icounter + 2;
			return result;
		}
		public static string StripString(string s, ref int pos)
		{
			int icounter = pos, len = s.Length;
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
				return "$";
			}
			string result = stripstring(s,ref icounter);
			pos = icounter + 1;
			return result;
		}
		private static string stripstring(string s, ref int pos)
		{
			int icounter = pos, len = s.Length;
			string result = "";
			if (s[icounter++] != '\'')
				throw new Exception("Unrecognized format!");
			while (icounter < len)
			{
				char c = s[icounter];
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
		private static string striplist(string s, ref int pos)
		{
			int icounter = pos, len = s.Length;
			string result = "(";
			if (s[icounter++] != '(')
				throw new Exception("Unrecognized format!");
			while (icounter < len)
			{
				char c = s[icounter];
				if (c == '\'')
					result += "'" + stripstring(s, ref icounter) + "'";
				else if (c == '(')
					result += striplist(s, ref icounter);
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
