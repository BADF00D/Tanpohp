using System.Collections.Generic;

namespace Tanpohp.Data.Collections
{
    /// <summary>
    /// Defines a basic interface for a ringbuffer.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRingBuffer<T> : IEnumerable<T>
    {
        /// <summary>
        /// Max amount of items this buffer can store.
        /// </summary>
        int Capacity { get; }

        /// <summary>
        /// Current amount of items in buffer.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Gets/sets element at specific position where index=0 is the newest element.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        /// <remarks>If there is no value at given index, default(T) is returned.</remarks>
        T this[int index] { get; set; }

        /// <summary>
        /// Adds a item to the ringbuffer and sets head to its position.
        /// </summary>
        /// <param name="item"></param>
        void Add(T item);
        /// <summary>
        /// Clears the buffer.
        /// </summary>
        void Clear();
        /// <summary>
        /// Determines whether given item is in buffer.
        /// </summary>
        /// <param name="item">Item to search for.</param>
        /// <returns>True if item is in buffer, false otherwise.</returns>
        bool Contains(T item);
    }
}