using System;
using System.Collections.Generic;
using Tanpohp.Extensions;

namespace Tanpohp.Data
{
    /// <summary>
    /// Defines a threadsave pool for items of type T.
    /// </summary>
    /// <typeparam name="T">Generic type that has to be a class.</typeparam>
    public abstract class AbstractPool<T> : IPool<T> where T : class
    {
        private readonly Stack<T> _availableItems;

        private readonly object _lockKey = new object();

        /// <summary>
        /// Initial size of the pool.
        /// </summary>
        public uint InitialPoolSize { get; private set; }

        /// <summary>
        /// Number of items in pool.
        /// </summary>
        public uint Count
        {
            get
            {
                lock (_lockKey)
                {
                    return (uint)_availableItems.Count;
                }
            }
        }

        protected AbstractPool(uint initialPoolSize = (uint)16)
        {
            _availableItems = new Stack<T>((int)initialPoolSize);
            InitialPoolSize = initialPoolSize;
            FillPoolWith(InitialPoolSize);
        }

        ~AbstractPool()
        {
            Clear(0);
        }

        /// <summary>
        /// Used to create new isntances of type T.
        /// </summary>
        /// <returns></returns>
        protected abstract T CreateInstance();

        /// <summary>
        /// Used to reset a instance to its default values.
        /// </summary>
        /// <returns></returns>
        protected abstract void ClearInstance(T instance);

        /// <summary>
        /// Gets a instance from pool.
        /// </summary>
        /// <returns>Instance of T that might already has been used.</returns>
        public T RetrieveInstance()
        {
            lock (_lockKey)
            {
                if (_availableItems.Count > 0) return _availableItems.Pop();
            }

            PoolEmpty.CheckedInvoke(this);//here subclass might decide to add new instances.

            lock (_lockKey)
            {
                if (_availableItems.Count > 0) return _availableItems.Pop();
            }
            return CreateInstance();
        }

        /// <summary>
        /// Occures when pool has no more items.
        /// </summary>
        public event EventHandler PoolEmpty;

        /// <summary>
        /// Fills the pool with the number of instances given.
        /// </summary>
        /// <param name="newInstanceCount">Number of instances the that should be added to the pool.</param>
        public void FillPoolWith(uint newInstanceCount)
        {
            lock (_lockKey)
            {
                for (var i = 0; i < newInstanceCount; i++)
                {
                    _availableItems.Push(CreateInstance());
                }
            }
        }

        /// <summary>
        /// Gets a instance from pool that is cleared to its defaults.
        /// </summary>
        /// <returns></returns>
        public T RetrieveClearInstance()
        {
            var instance = RetrieveInstance();
            ClearInstance(instance);

            return instance;
        }

        /// <summary>
        /// Releases a instance to the pool.
        /// </summary>
        /// <param name="instance"></param>
        public void ReleaseInstance(T instance)
        {
            lock (_lockKey)
            {
                _availableItems.Push(instance);
            }
        }

        /// <summary>
        /// Removed all items from pool besides remainingItemCount. If items are IDisposable, they get disposed.
        /// </summary>
        /// <param name="remainingItemCount">Number of items in pool after clear.</param>
        public void Clear(int remainingItemCount)
        {
            lock (_lockKey)
            {
                var upper = Math.Max(_availableItems.Count-remainingItemCount, 0);
                for (var i = 0; i < upper; i++)
                {
                    var itemToDispose = _availableItems.Pop();
                    if (itemToDispose is IDisposable)
                        (itemToDispose as IDisposable).Dispose();
                }
            }
        }
    }
}
