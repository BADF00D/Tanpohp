#region usings

using System;
using System.Collections.Generic;

#endregion

namespace Tanpohp.Data.Collections
{
    public interface INotifyChangedCollection<T> : ICollection<T>
    {
        /// <summary>
        /// Occures after item was added.
        /// </summary>
        event EventHandler<ListChangedEventArgs<T>> ItemAdded;

        /// <summary>
        /// Occures after item was removed.
        /// </summary>
        event EventHandler<ListChangedEventArgs<T>> ItemRemoved;
    }
}