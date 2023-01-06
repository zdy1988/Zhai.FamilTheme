using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;

namespace Zhai.Famil.Common
{
    public abstract class SingletonBase<T> where T : class
    {
        public static T Instance => SingletonFactory.Instance;

        private class SingletonFactory
        {
            private static WeakReference instance;

            internal static T Instance
            {
                get
                {
                    if (!(instance?.Target is T comparer))
                    {
                        comparer = GetInstance();
                        instance = new WeakReference(comparer);
                    }

                    return comparer;
                }
            }

            private SingletonFactory()
            {
            }

            static SingletonFactory()
            {
            }

            private static T GetInstance()
            {
                var theType = typeof(T);

                T inst;

                try
                {
                    inst = (T) theType.InvokeMember(theType.Name,
                            BindingFlags.CreateInstance | BindingFlags.Instance | BindingFlags.NonPublic,
                            null, null, null,
                            CultureInfo.InvariantCulture);
                }
                catch (MissingMethodException ex)
                {
                    throw new TypeLoadException(
                            string.Format(CultureInfo.CurrentCulture,
                                    "The type '{0}' must have a private constructor to be used in the Singleton pattern.",
                                    theType.FullName), ex);
                }

                return inst;
            }
        }
    }
}