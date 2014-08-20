using System;

namespace Tanpohp.Data.ValueProvider
{
    public interface IValueProvider<T>
    {
        T Value { get; set; }

        /// <summary>
        /// Invoked when value was changed.
        /// </summary>
        event EventHandler Changed;

        /// <summary>
        /// Invoked before value gets changed;
        /// </summary>
        event EventHandler Changing;
    }
}