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
		private Dictionary<string, T> mDictionary = new Dictionary<string, T>();

		public event NotifyCollectionChangedEventHandler CollectionChanged;

		public SET() : base() { }
		public SET(T item) : base() { Add(item); }
		public SET(IEnumerable<T> collection) { Clear();  AddRange(collection); }

		public int Count { get { return mDictionary.Count; } }
		public bool IsReadOnly { get { return false; } }

		private string getKey(T item)
		{
			if (item is IfcRoot root)
				return root.GlobalId;
			return item.StepId > 0 ? item.StepId.ToString() : item.GetHashCode().ToString();
		}
		private bool add(T item)
		{
			string key = getKey(item);
			if (mDictionary.ContainsKey(key))
				return false;
			mDictionary[key] = item;
			return true;
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
			if (mDictionary.Count > 0)
			{
				List<T> values = mDictionary.Values.ToList();
				mDictionary.Clear();
				if(CollectionChanged != null)
					CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, values));
			}
		} 
		public bool Contains(T item) { return mDictionary.ContainsKey(getKey(item)); }
		public List<TOutput> ConvertAll<TOutput>(Converter<T, TOutput> converter) { return this.ToList().ConvertAll(converter); }
		public void CopyTo(T[] array, int arrayIndex) { mDictionary.Values.CopyTo(array, arrayIndex); }
		public IEnumerator<T> GetEnumerator() { return mDictionary.Values.GetEnumerator(); }
		public bool Remove(T item) 
		{
			if (mDictionary.Remove(getKey(item)))
			{
				if(CollectionChanged != null)
					CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item));
				return true;
			}
			return false;
		} 
		IEnumerator IEnumerable.GetEnumerator() { return mDictionary.Values.GetEnumerator(); }

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
