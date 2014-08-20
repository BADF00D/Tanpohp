using System;
using ScreenDimmer.Annotations;

namespace Tanpohp.Data.ValueProvider
{
    /// <summary>
    /// ValueProvider with following characteristics:
    /// If try to set an invalid value: throw an ArgumentException.
    /// Else: Invokes Changing event; Set Value; Invokes Changed event.
    /// </summary>
    /// <typeparam name="T">Generic parameter.</typeparam>
    public abstract class ExceptionThrowingValueProvider<T> : AbstractValueProvider<T>
    {
        protected ExceptionThrowingValueProvider([NotNull] Func<T, T, bool> equals) : base(equals)
        {
        }

        protected new abstract bool IsValid(T value);

        protected override void PerformSet(ref T backingField, T value)
        {
            if (!IsValid(value)) throw new ArgumentException("Invalid value");
            
            InvokeChanging();
            backingField = value;
            InvokeChanged();
        }
    }
}