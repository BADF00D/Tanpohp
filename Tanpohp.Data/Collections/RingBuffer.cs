using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Tanpohp.Data.Collections
{
    public class RingBuffer<T> : IRingBuffer<T>
    {
        #region Fields

        private T[] _buffer;

        private int _headPosition = -1;

        #endregion

        #region Constructors and Destructors

        public RingBuffer(uint size)
        {
            if (size < 1) throw new ArgumentException("Size must be bigger then 0.");

            _buffer = new T[size];
        }

        #endregion

        #region Public Properties

        public int Capacity
        {
            get { return _buffer.Length; }
        }

        public int Count { get; private set; }

        #endregion

        #region Public Indexers

        public T this[int index]
        {
            get
            {
                if (index < 0 || index > Capacity -1) throw new ArgumentOutOfRangeException("Parameter index must be [0,Capacity[.");
                return _buffer[(_buffer.Length + _headPosition - index) % _buffer.Length];
            }
            set
            {
                if (index < 0 || index > Capacity - 1) throw new ArgumentOutOfRangeException("Parameter index must be [0,Capacity[.");
                _buffer[(_buffer.Length + _headPosition - index) % _buffer.Length] = value;
            }
        }

        #endregion

        #region Public Methods and Operators

        public void Add(T item)
        {
            ++_headPosition;
            if (_headPosition == _buffer.Length)
                _headPosition = 0;
            _buffer[_headPosition] = item;

            if (Count < _buffer.Length) Count++;
        }

        public void Clear()
        {
            for (var i = 0; i < _buffer.Length; i++ )
            {
                _buffer[i] = default(T);
            }
            _headPosition = 0;
            Count = 0;
        }

        public bool Contains(T item)
        {
            return _buffer.Contains(item);
        }

        /// <summary>
        /// Retruns enumerable of all item from newest to oldest.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            for (var i = 0; i < Count; i++)
            {
                yield return this[i];
            }
        }

        #endregion

        #region Explicit Interface Methods

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
