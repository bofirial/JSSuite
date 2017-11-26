using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JS.Core.Foundation.Configuration
{
    /// <summary>
    /// Email Account Collection
    /// </summary>
    public class EmailAccountCollection : ConfigurationElementCollection
    {
        /// <summary>
        /// Gets the <see cref="EmailAccountElement"/> with the specified name.
        /// </summary>
        /// <value>
        /// The <see cref="EmailAccountElement"/>.
        /// </value>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public new EmailAccountElement this[string name]
        {
            get { return (EmailAccountElement)base.BaseGet(name); }
        }

        /// <summary>
        /// When overridden in a derived class, creates a new <see cref="T:System.Configuration.ConfigurationElement" />.
        /// </summary>
        /// <returns>
        /// A newly created <see cref="T:System.Configuration.ConfigurationElement" />.
        /// </returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new EmailAccountElement();
        }

        /// <summary>
        /// Gets the element key for a specified configuration element when overridden in a derived class.
        /// </summary>
        /// <param name="element">The <see cref="T:System.Configuration.ConfigurationElement" /> to return the key for.</param>
        /// <returns>
        /// An <see cref="T:System.Object" /> that acts as the key for the specified <see cref="T:System.Configuration.ConfigurationElement" />.
        /// </returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((EmailAccountElement)element).Name;
        }
    }
}
