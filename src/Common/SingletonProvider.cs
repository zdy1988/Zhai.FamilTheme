using System;

namespace Zhai.Famil.Common
{
    public static class SingletonProvider<T> 
        where T : class, new()
    {
        #region Fields

        /// <summary>
        /// Gets the singleton of the given type.
        /// </summary>
        private static readonly Lazy<T> lazy = new Lazy<T>(() => new T());

        #endregion

        #region Properties

        /// <summary>
        /// Gets the singleton of the given type.
        /// </summary>
        public static T Instance => lazy.Value;

        #endregion
    }

    public static class SingletonProvider<T, TBase>
        where T : TBase, new()
        where TBase : class
    {
        #region Fields

        /// <summary>
        /// Gets the singleton of the given type.
        /// </summary>
        private static readonly Lazy<T> lazy = new Lazy<T>(() => new T());

        #endregion

        #region Properties

        /// <summary>
        /// Gets the singleton of the given type.
        /// </summary>
        public static T Instance => lazy.Value;

        #endregion
    }

    public static class SingletonProvider
    {
        #region Methods

        /// <summary>
        /// Gets the singleton of the given type.
        /// </summary>
        /// <typeparam name="TParameter">Given singleton type.</typeparam>
        /// <returns>The <see cref="TParameter" />Singleton object.</returns>
        public static TParameter Get<TParameter>() 
            where TParameter : class, new()
        {
            return SingletonProvider<TParameter>.Instance;
        }

        public static TParameter Get<TParameter, TParameterBase>()
            where TParameter : TParameterBase, new()
            where TParameterBase : class
        {
            return SingletonProvider<TParameter, TParameterBase>.Instance;
        }

        #endregion
    }

    internal sealed class Singleton
    {
        #region Fields

        /// <summary>
        /// Defines the _lazy
        /// </summary>
        private static readonly Lazy<Singleton> _lazy = new Lazy<Singleton>(() => new Singleton());

        #endregion

        #region Properties

        /// <summary>
        /// Gets the Instance
        /// </summary>
        public static Singleton Instance => _lazy.Value;

        #endregion

        #region Constructors

        /// <summary>
        /// Prevents a default instance of the <see cref="Singleton"/> class from being created.
        /// </summary>
        private Singleton()
        {
        }

        #endregion
    }
}