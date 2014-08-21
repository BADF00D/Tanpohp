#region usings

using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Tanpohp.Annotations.Resharper;

#endregion

namespace Tanpohp.Utils
{
    /// <summary>
    /// Base class for implementing Builder Pattern.
    /// </summary>
    /// <typeparam name="T">Type of the sub class.</typeparam>
    /// <remarks>This class use reflection during Validate to ensure that all backing fields are set.</remarks>
    public abstract class AbstractBuilder<T>
    {
        #region Public Methods and Operators

        /// <summary>
        /// Create new instance of subclass.
        /// </summary>
        /// <returns></returns>
        public abstract T Build();

        #endregion

        #region Methods

        /// <summary>
        /// Throw an expection with given parameter.
        /// </summary>
        /// <param name="parameterName"></param>
        [DebuggerStepThrough]
        protected void ThrowException([NotNull] string parameterName)
        {
            throw new Exception(parameterName + " is not set.");
        }

        /// <summary>
        ///     Checks if each private field that is not a value type, it this is not null.
        /// </summary>
        /// <param name="item"></param>
        
        protected virtual void Validate([NotNull] AbstractBuilder<T> item)
        {
            var fields = GetType()
                             .GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
            foreach (var fieldInfo in fields.Where(f => !f.FieldType.IsValueType))
            {
                var value = fieldInfo.GetValue(this);
                var typeName = fieldInfo.FieldType.Name;
                ValidateNotNull(value, typeName);
            }
        }

        /// <summary>
        /// Resets the builder, so that it can be used as it was recreated. This methods sets all reference types
        /// to null.
        /// </summary>
        /// <remarks>If subclass contains value types, these have to be manually set to their default value, by overwriting this method.</remarks>
        public virtual void Reset()
        {
            var fields = GetType()
                             .GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
            foreach (var fieldInfo in fields.Where(f => !f.FieldType.IsValueType))
            {
                fieldInfo.SetValue(this, null);
            }
        }

        [DebuggerStepThrough]
        protected void ValidateNotNull<TValue>(TValue field) where TValue : class
        {
            if (field == null) ThrowException(typeof(TValue).Name);
        }

        [DebuggerStepThrough]
        private void ValidateNotNull(object field, string typeName)
        {
            if (field == null) ThrowException(typeName);
        }

        #endregion
    }
}
