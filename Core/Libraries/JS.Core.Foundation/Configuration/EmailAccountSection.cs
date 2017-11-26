using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JS.Core.Foundation.Configuration
{
    /// <summary>
    /// Email Account Section
    /// </summary>
    public class EmailAccountSection : ConfigurationSection
    {
        /// <summary>
        /// List of Email Accounts.
        /// </summary>
        [ConfigurationProperty("", IsRequired=true, IsDefaultCollection=true)]
        public EmailAccountCollection EmailAccounts
        {
            get { return this[""] as EmailAccountCollection; }
        }
    }
}
