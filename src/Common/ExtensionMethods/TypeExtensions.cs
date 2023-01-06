using System;
using System.Collections.Generic;
using System.Text;

namespace Zhai.Famil.Common.ExtensionMethods
{
    public static class TypeExtensions
    {
        public static T CreateInstance<T>(this Type t)
        {
            return (T)Activator.CreateInstance(t);
        }

        public static T CreateInstance<T>(this Type t, params object[] paramArray)
        {
            return (T)Activator.CreateInstance(t, paramArray);
        }
    }
}
