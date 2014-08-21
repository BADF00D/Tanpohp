#region usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

#endregion

namespace Tanpohp.Extensions
{
    public static class TypeExtension
    {
        /// <summary>
        /// Returns a reader friendly name of normal an generic types.
        /// </summary>
        /// <param name="type">Type to get readable name from.</param>
        /// <returns></returns>
        /// <remarks>For buildin types as int, and string this method use always the names of the CLR types.</remarks>
        public static string GetReadableName(this Type type)
        {
            var typeParameter = type.GetGenericArguments();
            var typeName = type.Name;
            if (typeParameter.Length > 0)
            {
                var sb = new StringBuilder(typeName.Length + typeParameter.Length * 10);
                sb.Append(typeName.Substring(0, typeName.Length - 2));
                sb.Append("<");
                for (var i = 0; i < typeParameter.Length; i++)
                {
                    if (i > 0)
                    {
                        sb.Append(",");
                    }
                    sb.Append(typeParameter[i].Name);
                }
                sb.Append(">");
                typeName = sb.ToString();
            }
            return typeName;
        }

        /// <summary>
        /// Gets all pulic properties of given Type.
        /// </summary>
        /// <param name="type">Type that should be evaluated.</param>
        /// <returns></returns>
        /// <remarks>Stolen from "http://stackoverflow.com/questions/358835/getproperties-to-return-all-properties-for-an-interface-inheritance-hierarchy."</remarks>
        public static PropertyInfo[] GetPublicProperties(this Type type)
        {
            return type.IsInterface
                       ? GetPropertiesOfInterface(type)
                       : type.GetProperties(BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.Instance);
        }

        private static PropertyInfo[] GetPropertiesOfInterface(Type type)
        {
            var propertyInfos = new List<PropertyInfo>();

            var considered = new List<Type>();
            var queue = new Queue<Type>();
            considered.Add(type);
            queue.Enqueue(type);
            while (queue.Count > 0)
            {
                var subType = queue.Dequeue();
                foreach (var subInterface in
                    subType.GetInterfaces().Where(subInterface => !considered.Contains(subInterface)))
                {
                    considered.Add(subInterface);
                    queue.Enqueue(subInterface);
                }

                var typeProperties =
                    subType.GetProperties(BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.Instance);

                var newPropertyInfos = typeProperties.Where(x => !propertyInfos.Contains(x));

                propertyInfos.InsertRange(0, newPropertyInfos);
            }

            return propertyInfos.ToArray();
        }
    }
}
