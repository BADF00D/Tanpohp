#region usings

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using Tanpohp.Annotations.Resharper;
using Tanpohp.Extensions;

#endregion

namespace Tanpohp.Data
{
    public abstract class NotificationObject : INotifyPropertyChanged, INotifyPropertyChanging
    {
        /// <summary>
        /// Assigns newValue to backing field it it changed. Invokes Changing and Changed events.
        /// </summary>
        /// <typeparam name="T">Generic parameter.</typeparam>
        /// <param name="backingField">Field in subclass is used as backing field.</param>
        /// <param name="newValue">New value to assign.</param>
        /// <param name="selectorExpression">Expression that selects the property name.</param>
        /// <returns>True, if value was changed. False otherwise.</returns>
        protected bool Assign<T>(ref T backingField, T newValue, [NotNull]Expression<Func<T>> selectorExpression)
        {
            return Assign(ref backingField, newValue, this.GetPropertyName(selectorExpression));
        }

        /// <summary>
        /// Assigns newValue to backing field it it changed. Invokes Changing and Changed events.
        /// </summary>
        /// <typeparam name="T">Generic parameter.</typeparam>
        /// <param name="backingField">Field in subclass is used as backing field.</param>
        /// <param name="newValue">New value to assign.</param>
        /// <param name="propertyName">Name of the property that might get changed.</param>
        /// <returns>True, if value was changed. False otherwise.</returns>
        protected bool Assign<T>(ref T backingField, T newValue, [NotNull]string propertyName)
        {
            if (EqualityComparer<T>.Default.Equals(backingField, newValue)) return false;

            InvokePropertyChanging(propertyName);
            backingField = newValue;
            InvokePropertyChanged(propertyName);

            return true;
        }

        /// <summary>
        /// Invokes PropertyChanged event.
        /// </summary>
        /// <typeparam name="T">Generic parameter.</typeparam>
        /// <param name="selectorExpression">Expression that selects the property name.</param>
        /// <returns>True, if value was changed. False otherwise.</returns>
        protected void InvokePropertyChanging<T>([NotNull]Expression<Func<T>> selectorExpression)
        {
            InvokePropertyChanged(this.GetPropertyName(selectorExpression));
        }

        /// <summary>
        /// Invokes PropertyChanging event.
        /// </summary>
        /// <param name="propertyName">Name of the property that might get changed.</param>
        /// <returns>True, if value was changed. False otherwise.</returns>
        protected void InvokePropertyChanging([NotNull]string propertyName)
        {
            if (PropertyChanging == null) return;

            PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
        }

        /// <summary>
        /// Invokes PropertyChanged event.
        /// </summary>
        /// <typeparam name="T">Generic parameter.</typeparam>
        /// <param name="selectorExpression">Expression that selects the property name.</param>
        /// <returns>True, if value was changed. False otherwise.</returns>
        protected void InvokePropertyChanged<T>([NotNull]Expression<Func<T>> selectorExpression)
        {
            InvokePropertyChanged(this.GetPropertyName(selectorExpression));
        }

        /// <summary>
        /// Invokes PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected void InvokePropertyChanged([NotNull]string propertyName)
        {
            if (PropertyChanged == null) return;

            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Occurs when a property value is changing.
        /// </summary>
        public event PropertyChangingEventHandler PropertyChanging;
    }
}
