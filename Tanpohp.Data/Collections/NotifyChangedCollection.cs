using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Tanpohp.Data.Collections
{
    public class NotifyChangedCollection<T> : INotifyChangedCollection<T>
    {
        private readonly IList<T> _items;

        private readonly object _lockKey = new object();

        public NotifyChangedCollection(int intialSize = 4)
        {
            _items = new List<T>(intialSize);
        }

        public NotifyChangedCollection(IEnumerable<T> sequence)
        {
            _items = new List<T>(sequence);
        }

        public event EventHandler<ListChangedEventArgs<T>> ItemAdded;
        
        public event EventHandler<ListChangedEventArgs<T>> ItemRemoved;


        public IEnumerator<T> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T item)
        {
            lock (_lockKey)
            {
                _items.Add(item);
            }
            InvokeHandler(ItemAdded, item);
        }

        private void InvokeHandler(EventHandler<ListChangedEventArgs<T>> eventHandler, T item)
        {
            if (eventHandler == null) return;

            eventHandler(this, new ListChangedEventArgs<T>(item));
        }

        public void Clear()
        {
            List<T> copy;
            lock (_lockKey)
            {
                copy = _items.ToList();
                _items.Clear();
            }
            copy.ForEach(i => InvokeHandler(ItemRemoved, i));
        }

        public bool Contains(T item)
        {
            lock (_lockKey)
            {
                return _items.Contains(item);
            }
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            lock (_lockKey)
            {
                _items.CopyTo(array, arrayIndex);
            }
        }

        public bool Remove(T item)
        {
            bool success;
            lock (_lockKey)
            {
                success = _items.Remove(item);
            }
            InvokeHandler(ItemRemoved, item);

            return success;
        }

        public int Count
        {
            get
            {
                lock (_lockKey)
                {
                    return _items.Count;
                }
            }
        }

        public bool IsReadOnly
        {
            get
            {
                lock (_lockKey)
                {
                    return _items.IsReadOnly;
                }
            }
        }
    }
}