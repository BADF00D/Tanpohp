﻿using System;
using System.Collections.Generic;

namespace Tanpohp.Annotations.Resharper
{
    /// <summary>
    /// When applied to target attribute, specifies a requirement for any type which is marked with 
    /// target attribute to implement or inherit specific type or types
    /// </summary>
    /// <example>
    /// <code>
    /// [BaseTypeRequired(typeof(IComponent)] // Specify requirement
    /// public class ComponentAttribute : Attribute 
    /// {}
    /// 
    /// [Component] // ComponentAttribute requires implementing IComponent interface
    /// public class MyComponent : IComponent
    /// {}
    /// </code>
    /// </example>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    [BaseTypeRequired(typeof(Attribute))]
    public sealed class BaseTypeRequiredAttribute : Attribute
    {
        private readonly Type[] myBaseTypes;

        /// <summary>
        /// Initializes new instance of BaseTypeRequiredAttribute
        /// </summary>
        /// <param name="baseTypes">Specifies which types are required</param>
        public BaseTypeRequiredAttribute(params Type[] baseTypes)
        {
            myBaseTypes = baseTypes;
        }

        /// <summary>
        /// Gets enumerations of specified base types
        /// </summary>
        public IEnumerable<Type> BaseTypes
        {
            get { return myBaseTypes; }
        }
    }
}