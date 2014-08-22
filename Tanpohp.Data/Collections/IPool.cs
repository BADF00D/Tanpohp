using System;

namespace Tanpohp.Data.Collections
{
    public interface IPool<T> where T : class
    {
        /// <summary>
        /// Number of items in pool.
        /// </summary>
        uint Count { get; }

        /// <summary>
        /// Gets a instance from pool.
        /// </summary>
        /// <returns>Instance of T that might already has been used.</returns>
        T RetrieveInstance();

        /// <summary>
        /// Occures when pool has no more items.
        /// </summary>
        event EventHandler PoolEmpty;

        /// <summary>
        /// Fills the pool with the number of instances given.
        /// </summary>
        /// <param name="newInstanceCount">Number of instances the that should be added to the pool.</param>
        void FillPoolWith(uint newInstanceCount);

        /// <summary>
        /// Gets a instance from pool that is cleared to its defaults.
        /// </summary>
        /// <returns></returns>
        T RetrieveClearInstance();

        /// <summary>
        /// Releases a instance to the pool.
        /// </summary>
        /// <param name="instance"></param>
        void ReleaseInstance(T instance);

        /// <summary>
        /// Removed all items from pool besides remainingItemCount. If items are IDisposable, they get disposed.
        /// </summary>
        /// <param name="remainingItemCount">Number of items in pool after clear.</param>
        void Clear(int remainingItemCount);
    }
}