#region usings

using System;
using System.Collections.Generic;

#endregion

namespace Tanpohp.Data.Collections
{
    public interface INotifyChangedCollection<T> : ICollection<T>
    {
        /// <summary>
        /// If true, ItemRemoved event is called for each item during clear and Cleared is called once. If false only Cleared is called once.
        /// </summary>
        bool InvokeItemRemovedOnClear { get; set; }

        /// <summary>
        /// Occures after item was added.
        /// </summary>
        event EventHandler<ListChangedEventArgs<T>> ItemAdded;

        /// <summary>
        /// Occures after item was removed.
        /// </summary>
        event EventHandler<ListChangedEventArgs<T>> ItemRemoved;

        /// <summary>
        /// Invoked if clear was called.
        /// </summary>
        event EventHandler Cleared;
    }
}