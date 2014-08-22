#region usings

using System;
using System.Diagnostics;

#endregion

namespace Tanpohp.Data.Collections
{
    /// <summary>
    /// EventArgs for ItemAdded and ItemRemoved event.
    /// </summary>
    public class CacheChangedEventArgs<TIdentifier, TValue> : EventArgs
    {
        [DebuggerStepThrough]
        public CacheChangedEventArgs(TIdentifier identifier, TValue item)
        {
            Identifier = identifier;
            Item = item;
        }

        public TIdentifier Identifier { get; private set; }

        public TValue Item { get; private set; }
    }
}