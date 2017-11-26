using JS.Core.Foundation.BaseClasses;
using JS.Core.Foundation.ErrorHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace JS.Core.Foundation.Helpers
{
    /// <summary>
    /// Thread Context Helper
    /// </summary>
    public class ThreadContextHelper : SingletonBase<ThreadContextHelper>
    {
        /// <summary>
        /// Get an un-typed value from the thread context.
        /// Returns Null if not found.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public object GetValue(string key)
        {
            if (InWebContext())
            {
                if (HttpContext.Current.Items.Contains(key))
                {
                    return HttpContext.Current.Items[key];
                }

                return null;
            }

            return CallContext.LogicalGetData(key);
        }

        /// <summary>
        /// Sets an untyped value on the thread context.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public void SetValue(string key, object value)
        {
            if (InWebContext())
            {
                if (HttpContext.Current.Items.Contains(key))
                {
                    HttpContext.Current.Items[key] = value;
                }
                else
                {
                    HttpContext.Current.Items.Add(key, value);
                }
            }
            else
            {
                CallContext.LogicalSetData(key, value);
            }
        }

        /// <summary>
        /// Gets a typed value from the thread context.
        /// </summary>
        /// <typeparam name="T">The type of object to retrieve.</typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        /// <exception cref="CoreException">Invalid type in ThreadContext</exception>
        public T GetValue<T>(string key)
        {
            object result = GetValue(key);

            if (result is T)
            {
                return (T)result;
            }

            throw new CoreException("Invalid type in ThreadContext", String.Format("Invalid type in ThreadContext. Expected: {0}, found: {1}.", typeof(T).Name, result.GetType().Name));
        }

        /// <summary>
        /// Checks if the ThreadContext contains a value for the key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public bool ContainsKey(string key)
        {
            if (InWebContext())
            {
                return (HttpContext.Current.Items.Contains(key));
            }

            return (CallContext.LogicalGetData(key) != null);
        }

        /// <summary>
        /// Checks if the ThreadContext contains a value for the key that matches the Generic Type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public bool ContainsKeyOfType<T>(string key)
        {
            if (ContainsKey(key))
            {
                return GetValue(key) is T;
            }

            return false;
        }

        /// <summary>
        /// Checks if there is a HttpContext
        /// </summary>
        /// <returns></returns>
        public bool InWebContext()
        {
            return (HttpContext.Current != null);
        }
    }
}
