using System;

namespace Zhai.Famil.Common.Helpers
{
    public static class AssemblyHelper
    {
        public static object CreateInstance(string className)
        {
            try
            {
                var type = Type.GetType($"{className}");

                return type == null ? null : Activator.CreateInstance(type);
            }
            catch
            {
                return null;
            }
        }
    }
}
