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
using System.Collections.ObjectModel;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GeometryGym.STEP
{
	[Serializable]
	public class SET<T> : ObservableCollection<T> where T : ISTEPEntity
	{
		public SET() : base() { }
		public SET(T item) : base() { Add(item); }
		public SET(IEnumerable<T> collection) { Clear();  AddRange(collection); }
		public void AddRange(IEnumerable<T> collection)
		{
			if (collection != null)
			{
				foreach (T t in collection)
				{
					if (t != null)
						this.Add(t);
				}
			}
		}
		
		public List<TOutput> ConvertAll<TOutput>(Converter<T, TOutput> converter) { return this.ToList().ConvertAll(converter); }
	}
	[Serializable]
	public class LIST<T> : ObservableCollection<T>
	{
		public LIST() : base() { }
		public LIST(T item) : base() { Add(item); }
		public LIST(IEnumerable<T> collection) { Clear();  AddRange(collection); }
		public void AddRange(IEnumerable<T> collection)
		{
			if (collection != null)
			{
				foreach (T t in collection)
				{
					if(t != null)
						this.Add(t);
				}
			}
		}
		public List<TOutput> ConvertAll<TOutput>(Converter<T, TOutput> converter) { return this.ToList().ConvertAll(converter); }
	}
}
