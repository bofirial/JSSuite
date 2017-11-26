using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JS.Core.Foundation.Configuration
{
    /// <summary>
    /// Email Account Element
    /// </summary>
    public class EmailAccountElement : ConfigurationElement
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [ConfigurationProperty("name")]
        public string Name
        {
            get { return (string)(base["name"]); }
            set { base["name"] = value; }
        }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        [ConfigurationProperty("userName")]
        public string UserName
        {
            get { return (string)(base["userName"]); }
            set { base["userName"] = value; }
        }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        [ConfigurationProperty("password")]
        public string Password
        {
            get { return (string)(base["password"]); }
            set { base["password"] = value; }
        }

        /// <summary>
        /// Gets or sets from address.
        /// </summary>
        /// <value>
        /// From address.
        /// </value>
        [ConfigurationProperty("fromAddress")]
        public string FromAddress
        {
            get { return (string)(base["fromAddress"]); }
            set { base["fromAddress"] = value; }
        }

        /// <summary>
        /// Gets or sets from display name.
        /// </summary>
        /// <value>
        /// From display name.
        /// </value>
        [ConfigurationProperty("fromDisplayName")]
        public string FromDisplayName
        {
            get { return (string)(base["fromDisplayName"]); }
            set { base["fromDisplayName"] = value; }
        }

        /// <summary>
        /// Gets or sets the SMTP address.
        /// </summary>
        /// <value>
        /// The SMTP address.
        /// </value>
        [ConfigurationProperty("smtpAddress")]
        public string SmtpAddress
        {
            get { return (string)(base["smtpAddress"]); }
            set { base["smtpAddress"] = value; }
        }

        /// <summary>
        /// Gets or sets the SMTP port.
        /// </summary>
        /// <value>
        /// The SMTP port.
        /// </value>
        [ConfigurationProperty("smtpPort")]
        public int SmtpPort
        {
            get { return Int32.Parse(base["smtpPort"].ToString()); }
        }

        /// <summary>
        /// Gets or sets the use SSL.
        /// </summary>
        /// <value>
        /// The use SSL.
        /// </value>
        [ConfigurationProperty("useSSL", DefaultValue="true")]
        public bool UseSSL
        {
            get { return base["useSSL"].ToString().ToLower() == "true"; }
        }
    }
}
