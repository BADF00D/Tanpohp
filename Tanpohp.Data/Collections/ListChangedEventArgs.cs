#region usings

using System;

#endregion

namespace Tanpohp.Data.Collections
{
    public class ListChangedEventArgs<T> : EventArgs
    {
        public ListChangedEventArgs(T item)
        {
            Item = item;
        }

        public T Item { get; private set; }
    }
}