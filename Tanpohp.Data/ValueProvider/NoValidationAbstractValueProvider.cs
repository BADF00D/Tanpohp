using System;
using ScreenDimmer.Annotations;

namespace Tanpohp.Data.ValueProvider
{
    /// <summary>
    /// Does not valid the given value. Only set it (if not equals to existing) and throws Changing and Changed event.
    /// </summary>
    /// <typeparam name="T">Generic parameter.</typeparam>
    public class NoValidationAbstractValueProvider<T> : AbstractValueProvider<T>
    {
        public NoValidationAbstractValueProvider([NotNull] Func<T, T, bool> equals) : base(equals)
        {
        }

        protected override void PerformSet(ref T backingField, T value)
        {
            InvokeChanging();
            backingField = value;
            InvokeChanged();
        }
    }
}