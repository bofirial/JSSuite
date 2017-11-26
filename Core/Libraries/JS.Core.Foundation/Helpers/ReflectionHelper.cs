using JS.Core.Foundation.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JS.Core.Foundation.Helpers
{
    /// <summary>
    /// Reflection Helper
    /// </summary>
    public class ReflectionHelper : SingletonBase<ReflectionHelper>
    {
        /// <summary>
        /// Determines whether the specified type is nullable.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public bool IsNullableType(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        /// <summary>
        /// Gets the name of the base type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public string GetUnderlyingTypeName(Type type)
        {
            string dataType = type.Name;

            if (IsNullableType(type))
            {
                dataType = Nullable.GetUnderlyingType(type).Name;
            }

            return dataType;
        }

        /// <summary>
        /// Returns All Properties in the Provided Object
        /// </summary>
        /// <typeparam name="TObject"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public IEnumerable<PropertyInfo> GetAllProperties<TObject>(TObject obj)
        {
            return obj.GetType().GetProperties();
        }

        /// <summary>
        /// Gets the property.
        /// </summary>
        /// <typeparam name="TObject">The type of the object.</typeparam>
        /// <param name="obj">The object.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        public PropertyInfo GetProperty<TObject>(TObject obj, string propertyName)
        {
            return obj.GetType().GetProperty(propertyName);
        }

        /// <summary>
        /// Gets the Value of a Property using Reflection
        /// </summary>
        /// <typeparam name="TObject"></typeparam>
        /// <param name="obj"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public object GetPropertyValue<TObject>(TObject obj, string propertyName)
        {
            return GetProperty(obj, propertyName).GetValue(obj, null);
        }

        /// <summary>
        /// Returns the Type of a Property
        /// </summary>
        /// <typeparam name="TObject"></typeparam>
        /// <param name="obj"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public Type GetPropertyType<TObject>(TObject obj, string propertyName)
        {
            return obj.GetType().GetProperty(propertyName).PropertyType;
        }

        /// <summary>
        /// Sets the Value of a Property using Reflection
        /// </summary>
        /// <typeparam name="TObject"></typeparam>
        /// <param name="obj"></param>
        /// <param name="propertyName"></param>
        /// <param name="propertyValue"></param>
        public void SetPropertyValue<TObject>(TObject obj, string propertyName, object propertyValue)
        {
            List<PropertyInfo> properties = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.Name == propertyName).ToList();

            foreach (PropertyInfo prop in properties)
            {
                try
                {
                    if (null != prop && prop.CanWrite)
                    {
                        prop.SetValue(obj, propertyValue, null);
                        break;
                    }
                }
                catch (ArgumentException)
                {
                }
            }
        }

        /// <summary>
        /// Returns True if the Property has the Provided Attribute
        /// </summary>
        /// <typeparam name="TAttribute"></typeparam>
        /// <param name="prop"></param>
        /// <returns></returns>
        public bool HasAttribute<TAttribute>(PropertyInfo prop)
            where TAttribute : Attribute
        {
            return GetAttributes<TAttribute>(prop).Count() > 0;
        }

        /// <summary>
        /// Returns the first instance of the provided attribute on the property
        /// </summary>
        /// <typeparam name="TAttribute"></typeparam>
        /// <param name="prop"></param>
        /// <returns></returns>
        public TAttribute GetAttribute<TAttribute>(PropertyInfo prop)
            where TAttribute : Attribute
        {
            return GetAttributes<TAttribute>(prop).FirstOrDefault();
        }

        /// <summary>
        /// Returns all instances of the provided attribute on the provided property
        /// </summary>
        /// <typeparam name="TAttribute"></typeparam>
        /// <param name="prop"></param>
        /// <returns></returns>
        public IEnumerable<TAttribute> GetAttributes<TAttribute>(PropertyInfo prop)
            where TAttribute : Attribute
        {
            object[] attrs = prop.GetCustomAttributes(true);
            foreach (object attr in attrs)
            {
                TAttribute attribute = attr as TAttribute;

                if (attribute != null)
                {
                    yield return attribute;
                }
            }

            yield break ;
        }
    }
}
