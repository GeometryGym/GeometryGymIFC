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
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GeometryGym.Ifc;

namespace GeometryGym.STEP
{
	[Serializable]
	public class SET<T> : ICollection<T>, IEnumerable<T>, IEnumerable, INotifyCollectionChanged where T : ISTEPEntity //ICollection, 
	{
		private HashSet<T> mSet = new HashSet<T>();

		public event NotifyCollectionChangedEventHandler CollectionChanged;

		public SET() : base() { }
		public SET(T item) : base() { Add(item); }
		public SET(IEnumerable<T> collection) { Clear();  AddRange(collection); }

		public int Count { get { return mSet.Count; } }
		public bool IsReadOnly { get { return false; } }

		private bool add(T item)
		{
			return mSet.Add(item);
		}
		public void Add(T item)
		{
			if(add(item) && CollectionChanged != null)
				CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
		}
		public void AddRange(IEnumerable<T> collection)
		{
			List<T> changed = new List<T>();
			if (collection != null)
			{
				foreach (T t in collection)
				{
					if (t != null)
					{
						if (this.add(t))
							changed.Add(t);
					}
				}
			}
			if (changed.Count > 0 && CollectionChanged != null)
				CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, changed));
		}
		public void Clear() 
		{
			if(mSet.Count > 0)
			{
				List<T> values = mSet.ToList();
				mSet.Clear();
				if(CollectionChanged != null)
					CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, values));
			}
		} 
		public bool Contains(T item) { return mSet.Contains(item); }
		public List<TOutput> ConvertAll<TOutput>(Converter<T, TOutput> converter) { return this.ToList().ConvertAll(converter); }
		public void CopyTo(T[] array, int arrayIndex) { mSet.CopyTo(array, arrayIndex); }
		public IEnumerator<T> GetEnumerator() { return mSet.GetEnumerator(); }
		public bool Remove(T item) 
		{
			return mSet.Remove(item);
		} 
		IEnumerator IEnumerable.GetEnumerator() { return mSet.GetEnumerator(); }

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
