using JS.Core.Foundation.BaseClasses;
using JS.Core.Foundation.ErrorHandling;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JS.Core.Foundation.Helpers
{
    /// <summary>
    /// Type Helper
    /// </summary>
    public class TypeHelper : SingletonBase<TypeHelper>
    {
        /// <summary>
        /// Converts the specified value to Type T.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public T Convert<T>(string value)
        {
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));

            if (!converter.CanConvertFrom(typeof(string)))
	        {
                throw new CoreException("Invalid Conversion", String.Format("Failed to convert string \"{0}\" to type {1}.", value, typeof(T).Name));
	        }

            if (!ReflectionHelper.Current.IsNullableType(typeof(T)) && value == null)
            {
                return default(T);
            }

            return (T)converter.ConvertFromInvariantString(value);
        }
    }
}
