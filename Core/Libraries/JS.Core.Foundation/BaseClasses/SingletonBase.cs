using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JS.Core.Foundation.BaseClasses
{
    /// <summary>
    /// Singleton Base
    /// </summary>
    /// <typeparam name="TClass">The type of the class.</typeparam>
    public class SingletonBase<TClass> where TClass : class
    {
        #region Members

        /// <summary>
        /// Static instance. Needs to use lambda expression
        /// to construct an instance (since constructor is private).
        /// </summary>
        private static readonly Lazy<TClass> sInstance = new Lazy<TClass>(() => CreateInstanceOfT());

        #endregion

        #region Properties

        /// <summary>
        /// Gets the instance of this singleton.
        /// </summary>
        public static TClass Current { get { return sInstance.Value; } }

        #endregion

        #region Methods

        /// <summary>
        /// Creates an instance of TClass via reflection since TClass's constructor is expected to be private.
        /// </summary>
        /// <returns></returns>
        private static TClass CreateInstanceOfT()
        {
            return Activator.CreateInstance(typeof(TClass), true) as TClass;
        }

        #endregion
    }
}
