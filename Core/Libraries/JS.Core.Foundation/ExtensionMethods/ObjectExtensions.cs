using JS.Core.Foundation.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JS.Core.Foundation.ExtensionMethods
{
    /// <summary>
    /// Object Extension Methods
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Returns the String Representation of an object if the object is not null
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToSafeString(this Object obj)
        {
            if (obj == null)
	        {
		        return null;
	        }

            return obj.ToString();
        }

        /// <summary>
        /// Publics the properties equals.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self">The self.</param>
        /// <param name="to">To.</param>
        /// <returns></returns>
        public static bool PublicPropertiesEquals<T>(this T self, T to) where T : class
        {
            if (self != null && to != null && !typeof(IEquatable<T>).IsAssignableFrom(typeof(T)))
            {
                Type type = typeof(T);

                foreach (PropertyInfo pi in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    object selfValue = ReflectionHelper.Current.GetPropertyValue(self, pi.Name);
                    object toValue = ReflectionHelper.Current.GetPropertyValue(to, pi.Name);

                    if (!selfValue.PublicPropertiesEquals(toValue))
                    {
                        return false;
                    }
                }
                return true;
            }
            return self == to;
        }
    }
}
