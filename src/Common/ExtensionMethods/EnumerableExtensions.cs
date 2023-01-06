using System;
using System.Collections.Generic;
using System.Text;

namespace Zhai.Famil.Common.ExtensionMethods
{
    public static class EnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
        {
            foreach (var item in enumeration)
                action(item);
        }
    }
}
