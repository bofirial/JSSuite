using JS.Core.Foundation.BaseClasses;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JS.Core.Web.Mvc.Html
{
    /// <summary>
    /// Html Attribute Dictionary
    /// </summary>
    public class HtmlAttributeDictionary : BaseDictionary<string, object>
    {
        private const string classKey = "class";

        /// <summary>
        /// Adds the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public override void Add(string key, object value)
        {
            if (key == classKey)
            {
                AddClass(value);

                return;
            }

            base.Add(key, value);
        }

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        public override void Add(KeyValuePair<string, object> item)
        {
            Add(item);
        }

        /// <summary>
        /// Adds the specified class.
        /// </summary>
        /// <param name="value">The value.</param>
        public void AddClass(object value)
        {
            if (!ContainsKey(classKey))
            {
                base.Add(classKey, value); 
            }
            else
	        {
                base[classKey] = String.Join(" ", base[classKey].ToString(), value.ToString());
	        }
        }

        /// <summary>
        /// Removes the specified class.
        /// </summary>
        /// <param name="value">The value.</param>
        public void RemoveClass(object value)
        {
            if (!ContainsKey(classKey))
            {
                return;
            }
            else
            {
                string currentClass = base[classKey].ToString();
                string removeClass = value.ToString();
                int index = currentClass.IndexOf(removeClass);

                if (index > 0)
                {
                    string newClass = currentClass.Remove(index, removeClass.Length);

                    newClass = newClass.Replace("  ", " ").Trim();

                    if (!String.IsNullOrEmpty(newClass))
                    {
                        base[classKey] = newClass;
                    }
                    else
                    {
                        Remove(classKey);
                    }
                }
            }
        }
    }
}
