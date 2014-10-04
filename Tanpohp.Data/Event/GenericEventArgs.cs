#region usings

using System;

#endregion

namespace Tanpohp.Data.Event
{
    /// <summary>
    /// EventArgs with generic type.
    /// </summary>
    /// <typeparam name="T">Generic Type.</typeparam>
    public class GenericEventArgs<T> : EventArgs
    {
        public GenericEventArgs(T value)
        {
            Value = value;
        }

        public T Value { get; private set; }
    }
}