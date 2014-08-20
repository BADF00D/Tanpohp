#region usings

using System.Collections.Generic;

#endregion

namespace Tanpohp.Extensions
{
    public static class ListExtension
    {
        public static void AddRange<T>(this IList<T> list, IEnumerable<T> values)
        {
            if (list is List<T>) (list as List<T>).AddRange(values);
            else values.ForEach(list.Add);
        }
    }
}
