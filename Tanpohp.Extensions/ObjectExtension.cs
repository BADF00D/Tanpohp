#region usings

using System;
using System.Linq.Expressions;

#endregion

namespace Tanpohp.Extensions
{

    public static class ObjectExtension
    {
        /// <summary>
        /// Gets the name of the property given by selectorExpression.
        /// </summary>
        /// <typeparam name="T">Generic parameter.</typeparam>
        /// <param name="object">Any object.</param>
        /// <param name="selectorExpression">Expression that sepcifies the property.</param>
        /// <returns></returns>
        public static string GetPropertyName<T>(this Object @object, Expression<Func<T>> selectorExpression)
        {
            var body = selectorExpression.Body as MemberExpression;
            if (body == null) throw new ArgumentException("The body of the selectorExpression has to be a member expression.");
            return body.Member.Name;
        }
    }
}
