using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using JS.Suite.Foundation.Constants;
using JS.Core.Foundation.Helpers;

namespace JS.Suite.BusinessLogic.Configuration
{
    /// <summary>
    /// Security Manager Section
    /// </summary>
    public class SecurityManagerSection : ConfigurationSection
    {
        /// <summary>
        /// Gets or sets the default current application.
        /// </summary>
        /// <value>
        /// The default current application.
        /// </value>
        [ConfigurationProperty("defaultCurrentApplication")]
        public Applications DefaultCurrentApplication
        {
            get { 
                return EnumHelper.Current.GetFromName<Applications>(this["defaultCurrentApplication"].ToString()); 
            }
            set { 
                this["defaultCurrentApplication"] = EnumHelper.Current.GetName(value); 
            }
        }
    }
}
