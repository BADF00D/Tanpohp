#region usings

using System;
using System.Diagnostics;
using Tanpohp.Annotations.Resharper;
using Tanpohp.Extensions;

#endregion

namespace Tanpohp.Data.ValueProvider
{
    /// <summary>
    /// This class provides access to a shared value. It informs about changes on this item.
    /// </summary>
    /// <typeparam name="T">Generic parameter.</typeparam>
    public abstract class AbstractValueProvider<T> : IValueProvider<T>
    {
        private readonly Func<T, T, bool> _equals;

        /// <summary>
        /// Create a new instance.
        /// </summary>
        /// <param name="equals">Compares two (maybe not initialized) instances adn returns if the are equals.</param>
        protected AbstractValueProvider([NotNull]Func<T,T, bool> equals)
        {
            _equals = equals;
        }

        private T _value;
        public T Value
        {
            get { return _value; }
            set
            {
                if (_equals(_value, value)) return;

                PerformSet(ref _value, value);
            }
        }

        public event EventHandler Changed;
        public event EventHandler Changing;

        /// <summary>
        /// This method should be overwritten in order to specify behaviour during setting the value.
        /// </summary>
        /// <param name="backingField">Backing field of the value.</param>
        /// <param name="value">New value that should be set.</param>
        protected abstract void PerformSet(ref T backingField, T value);

        /// <summary>
        /// Should be overwritten in order to check if given value is valid. If value is not valid, 
        /// value is not set and no events where fired.
        /// </summary>
        /// <param name="newValue"></param>
        /// <returns></returns>
        protected virtual bool IsValid(T newValue)
        {
            return true;
        }

        /// <summary>
        /// Invokes Changed event.
        /// </summary>
        [DebuggerStepThrough]
        protected void InvokeChanged()
        {
            Changed.CheckedInvoke(this);
        }

        /// <summary>
        /// Invokes Changing event.
        /// </summary>
        [DebuggerStepThrough]
        protected void InvokeChanging()
        {
            Changing.CheckedInvoke(this);
        }
    }
}
