using System;
using System.Diagnostics;

namespace Tanpohp.Data.Caching
{
    /// <summary>
    /// EventArgs for ItemAdded and ItemRemoved event.
    /// </summary>
    public class CacheUpdatedEventArgs<TIdentifier, TValue> : EventArgs
    {
        [DebuggerStepThrough]
        public CacheUpdatedEventArgs(TIdentifier identifier, TValue oldItem, TValue newItem)
        {
            Identifier = identifier;
            OldItem = oldItem;
            NewItem = newItem;
        }

        public TIdentifier Identifier { get; private set; }

        public TValue OldItem { get; private set; }

        public TValue NewItem { get; private set; }
    }
}