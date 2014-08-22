#region usings

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Tanpohp.Extensions;

#endregion

namespace Tanpohp.Data.Caching
{
    /// <summary>
    /// Threadsafe implementation for ICache interface.
    /// </summary>
    /// <remarks>The access to the inner values is thread save, but the events get invokes outside the lock 
    /// statements.</remarks>
    public class Cache<TIdentifier, TValue> : ICache<TIdentifier, TValue>
    {
        #region Fields

        private readonly Dictionary<TIdentifier, TValue> _cache;

        private readonly object _lockKey = new object();
        private const string ItemAlreadyExistsMessage = "Item with identifier '{0}' already exists.";
        private const string ItemDoesNotExsistsMessage = "Item with identifier '{0}' does not exists.";

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Create a cache.
        /// </summary>
        /// <param name="probableCacheSize">Expected final size of cache.</param>
        [DebuggerStepThrough]
        public Cache(int probableCacheSize)
        {
            _cache = new Dictionary<TIdentifier, TValue>(probableCacheSize);
        }

        #endregion

        #region Public Events

        /// <summary>
        /// Invoked from extern if a batch of updates was completed.
        /// </summary>
        public event EventHandler BatchUpdateCompleted;

        /// <summary>
        /// Invoked if Clear() was called.
        /// </summary>
        public event EventHandler Cleared;

        /// <summary>
        ///     Invoked if new element was added.
        /// </summary>
        public event EventHandler<CacheChangedEventArgs<TIdentifier, TValue>> ItemAdded;

        /// <summary>
        ///     Invoked if element was removed.
        /// </summary>
        public event EventHandler<CacheChangedEventArgs<TIdentifier, TValue>> ItemRemoved;

        /// <summary>
        ///     Invoked if element was updated.
        /// </summary>
        public event EventHandler<CacheUpdatedEventArgs<TIdentifier, TValue>> ItemUpdated;

        #endregion

        #region Public Properties

        public int Count
        {
            [DebuggerStepThrough]
            get
            {
                lock (_lockKey)
                {
                    return _cache.Count;
                }
            }
        }

        #endregion

        #region Public Indexers

        public TValue this[TIdentifier identifier]
        {
            [DebuggerStepThrough]
            get
            {
                lock (_lockKey)
                {
                    return _cache[identifier];
                }
            }
        }

        #endregion

        #region Public Methods and Operators

        #region ICache<TIdentifier,TValue> Members

        [DebuggerStepThrough]
        public void Add(TIdentifier identifier, TValue value)
        {
            if (_cache.ContainsKey(identifier))
                throw new Exception(string.Format(ItemAlreadyExistsMessage, identifier));

            lock (_lockKey)
            {
                _cache.Add(identifier, value);
            }
            InvokeEventHandler(ItemAdded, identifier, value);
        }


        [DebuggerStepThrough]
        public void Clear(bool invokeItemRemoved = true)
        {
            if (!invokeItemRemoved)
            {
                lock (_lockKey)
                {
                    _cache.Clear();
                }
            }
            else
            {
                var items = _cache.ToArray();
                lock (_lockKey)
                {
                    _cache.Clear();
                }
                foreach (var item in items)
                {
                    InvokeEventHandler(ItemRemoved, item.Key, item.Value);
                }
            }
            Cleared.CheckedInvoke(this);
        }

        [DebuggerStepThrough]
        public bool Contains(TIdentifier identifier)
        {
            lock (_lockKey)
            {
                return _cache.ContainsKey(identifier);
            }
        }

        public void InvokeUpdateBatchCompleted()
        {
            var handler = BatchUpdateCompleted;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        [DebuggerStepThrough]
        public bool IsEmpty()
        {
            return _cache.Count == 0;
        }

        [DebuggerStepThrough]
        public bool Remove(TIdentifier identifier)
        {
            TValue item;
            lock (_lockKey)
            {
                if (!_cache.ContainsKey(identifier)) return false;
                item = _cache[identifier];
                _cache.Remove(identifier);
            }
            InvokeEventHandler(ItemRemoved, identifier, item);

            return true;
        }

        public bool TryGetKey(TValue value, out TIdentifier key)
        {
            key = default(TIdentifier);
            var pair = _cache.Where(kvp => ReferenceEquals(value, kvp.Value)).Select(kvp => kvp.Key).ToArray();
            if (pair.Length == 0) return false;
            key = pair.First();

            return true;
        }

        [DebuggerStepThrough]
        public bool TryGet(TIdentifier identifier, out TValue value)
        {
            value = default(TValue);
            lock (_lockKey)
            {
                if (_cache.ContainsKey(identifier))
                {
                    value = _cache[identifier];
                    return true;
                }
            }
            return false;
        }

        [DebuggerStepThrough]
        public void Update(TIdentifier identifier, TValue item)
        {
            if (!_cache.ContainsKey(identifier))
                throw new ArgumentException(string.Format(ItemDoesNotExsistsMessage, identifier));

            TValue oldValue;
            lock (_lockKey)
            {
                oldValue = _cache[identifier];
                _cache[identifier] = item;
            }

            if (ItemUpdated == null) return;

            ItemUpdated(this, new CacheUpdatedEventArgs<TIdentifier, TValue>(identifier, oldValue, item));
        }

        #endregion

        #region IEnumerable<TValue> Members

        [DebuggerStepThrough]
        public IEnumerator<TValue> GetEnumerator()
        {
            List<TValue> array;
            lock (_lockKey)
            {
                array = _cache.Values.ToList();
            }
            return array.GetEnumerator();
        }

        #endregion

        #endregion

        #region Explicit Interface Methods

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Methods

        private void InvokeEventHandler(
            EventHandler<CacheChangedEventArgs<TIdentifier, TValue>> eventHandler, TIdentifier identifier, TValue value)
        {
            if (eventHandler == null) return;

            eventHandler.Invoke(this, new CacheChangedEventArgs<TIdentifier, TValue>(identifier, value));
        }

        #endregion
    }
}