using System;
using System.Collections.Generic;

namespace Tanpohp.Data.Collections
{
    /// <summary>
    ///     Defines basic methods and properties for cache with generic identifier and values.
    /// </summary>
    public interface ICache<TIdentifier, TValue> : IEnumerable<TValue>
    {
        #region Public Events

        /// <summary>
        /// Occures when InvokebatchUpdateCompleted() was called.
        /// </summary>
        event EventHandler BatchUpdateCompleted;

        /// <summary>
        /// Occures when Clear() was called.
        /// </summary>
        event EventHandler Cleared;

        /// <summary>
        /// Occures after item was added.
        /// </summary>
        event EventHandler<CacheChangedEventArgs<TIdentifier, TValue>> ItemAdded;

        /// <summary>
        /// Occures after item was removed.
        /// </summary>
        event EventHandler<CacheChangedEventArgs<TIdentifier, TValue>> ItemRemoved;

        /// <summary>
        /// Occures after item was updated.
        /// </summary>
        event EventHandler<CacheUpdatedEventArgs<TIdentifier, TValue>> ItemUpdated;

        #endregion

        #region Public Properties

        /// <summary>
        /// Count the number of elements in cache.
        /// </summary>
        int Count { get; }

        #endregion

        #region Public Indexers

        /// <summary>
        /// Gets the element with given identifier.
        /// </summary>
        /// <param name="identifier">Unique identifier.</param>
        /// <returns>Element with given identifier.</returns>
        TValue this[TIdentifier identifier] { get; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Adds item to cache.
        /// </summary>
        /// <param name="identifier">Unique identifier.</param>
        /// <param name="value">Value.</param>
        void Add(TIdentifier identifier, TValue value);

        /// <summary>
        /// Removes all items from cache.
        /// </summary>
        /// <param name="invokeItemRemoved">If true, ItemRemoved event is invoked for each.</param>
        void Clear(bool invokeItemRemoved = true);

        /// <summary>
        /// Determines whether element with identifier is in cache.
        /// </summary>
        /// <param name="identifier">Unique identifier.</param>
        /// <returns>True is key exits, false otherwise.</returns>
        bool Contains(TIdentifier identifier);

        /// <summary>
        /// Invoked from extern to inform listerens that multiple updates (batch update) occured and
        /// is completed.
        /// </summary>
        void InvokeUpdateBatchCompleted();

        /// <summary>
        /// Determines that Count is Zero.
        /// </summary>
        /// <returns>True if no elements in cache. False otherwise.</returns>
        bool IsEmpty();

        /// <summary>
        /// Removes element with given identifier.
        /// </summary>
        /// <param name="identifier">Unique identifier.</param>
        /// <returns>If item was in cache.</returns>
        bool Remove(TIdentifier identifier);

        /// <summary>
        /// Tries to get the key for the first object with reference equality.
        /// </summary>
        /// <param name="value">Value to get key for.</param>
        /// <param name="key">Key to find.</param>
        /// <returns>True - key was found and stored in key, false key wasn't found and key is set to default(TIdentifier).</returns>
        bool TryGetKey(TValue value, out TIdentifier key);

        /// <summary>
        /// Tries to get item from cache.
        /// </summary>
        /// <param name="identifier">Item identifier.</param>
        /// <param name="value">Value that should be assigned.</param>
        /// <returns>True if item was in cache, false otherwise.</returns>
        /// <remarks>This method is faster then checking if item exists and return it, because it uses one lock instead of two.</remarks>
        bool TryGet(TIdentifier identifier, out TValue value);

        /// <summary>
        ///     Replaces element with given identifier by new value.
        /// </summary>
        /// <param name="identifier">Unique identifier.</param>
        /// <param name="item">New value that should replace old value with same identifier.</param>
        void Update(TIdentifier identifier, TValue item);

        #endregion
    }
}
