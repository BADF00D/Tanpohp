#region usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Tanpohp.Annotations.Resharper;
using Tanpohp.Extensions;

#endregion

namespace Tanpohp.Data
{
    /// <summary>
    /// This class allows to keep track of properties that got changed. This is done by using the 
    /// AssignTrackt methods, instead of the Assign methods from base class.
    /// </summary>
    public abstract class ChangeTrackingNotificationObject : NotificationObject
    {
        private readonly HashSet<string> _changedProperties = new HashSet<string>();
        private readonly string _containsChangesPropertyName;

        protected ChangeTrackingNotificationObject()
        {
            _containsChangesPropertyName = this.GetPropertyName(() => ContainsChanges);
        }

        /// <summary>
        /// Assigns a given field with a new value, if the value changes the field. Tracks the changed field. 
        /// </summary>
        /// <typeparam name="T">Generic parameter.</typeparam>
        /// <param name="backingField">Backing field of the property.</param>
        /// <param name="newValue">New value to assign.</param>
        /// <param name="selectorExpression">Expression that selects the propery name of the property to change.</param>
        /// <returns>True, if value was changed. False otherwise.</returns>
        protected bool AssignTracked<T>(ref T backingField, T newValue, [NotNull]Expression<Func<T>> selectorExpression)
        {
            return AssignTracked(ref backingField, newValue, this.GetPropertyName(selectorExpression));
        }

        /// <summary>
        /// Assigns a given field with a new value, if the value changes the field. Tracks the changed field. 
        /// </summary>
        /// <typeparam name="T">Generic parameter.</typeparam>
        /// <param name="backingField">Backing field of the property.</param>
        /// <param name="newValue">New value to assign.</param>
        /// <param name="propertyName">Name of the property to change.</param>
        /// <returns>True, if value was changed. False otherwise.</returns>
        protected bool AssignTracked<T>(ref T backingField, T newValue, string propertyName)
        {
            if (EqualityComparer<T>.Default.Equals(backingField, newValue)) return false;

            var wasThisPropertyAlreadyChanged = _changedProperties.Contains(propertyName);
            if(wasThisPropertyAlreadyChanged)
            {
                InvokePropertyChanging(propertyName);
                backingField = newValue;
                InvokePropertyChanged(propertyName);
            }else
            {
                InvokePropertyChanging(propertyName);
                backingField = newValue;
                InvokePropertyChanged(propertyName);

                
                InvokePropertyChanging(_containsChangesPropertyName);
                _changedProperties.Add(propertyName);
                InvokePropertyChanged(_containsChangesPropertyName);
            }
            return true;
        }

        /// <summary>
        /// Determines whether there is a property changed, that was assigned via AssignTracked.
        /// </summary>
        public bool ContainsChanges { get { return GetAmountOfChangedProperties() > 0; } }

        /// <summary>
        /// Gets a list of properties that changed. This information is retrieved via using the 
        /// AssignTracked methods.
        /// </summary>
        /// <returns></returns>
        /// <remarks>This method should be overwritten when subclass contains other NotificationObject instances. 
        /// This methods never contains the property ContainsChanges.</remarks>
        public virtual IList<string> GetChangedProperties()
        {
            return _changedProperties.ToList();
        }

        /// <summary>
        /// Gets the amount of properties that have changed. This information is retrieved 
        /// by using the AssignTracked methods to assign properties.
        /// </summary>
        /// <returns></returns>
        /// <remarks>This method should be overwritten when subclass contains other NotificationObject instances.</remarks>
        public virtual int GetAmountOfChangedProperties()
        {
            return _changedProperties.Count;
        }

        /// <summary>
        /// Clears set of changed properties and sets ContainsChanges to false.
        /// </summary>
        public virtual void ClearChanges()
        {
            var changedPropertyCount = _changedProperties.Count;
            _changedProperties.Clear();
            if (changedPropertyCount > 0) InvokePropertyChanged(() => ContainsChanges);
        }

        /// <summary>
        /// Build a unique name for ChangeTrackingNotificationObjects that contains other 
        /// ChangeTrackingNotificationObjects. 
        /// </summary>
        /// <param name="property">Name of the property in current object.</param>
        /// <param name="subProperty">Name of the property in the object of first property.</param>
        /// <returns></returns>
        public string BuildNestedName(string property, string subProperty)
        {
            return "{0}.{1}".FormatWith(property, subProperty);
        }
    }
}
