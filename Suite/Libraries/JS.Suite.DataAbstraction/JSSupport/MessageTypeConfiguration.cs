using JS.Core.Foundation.Data;
using JS.Suite.DataAbstraction.JSSupport.Generated;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JS.Suite.DataAbstraction.JSSupport
{
    /// <summary>
    /// Editable Class for the MessageTypeConfiguration Table
    /// </summary>
    public class MessageTypeConfiguration : MessageTypeConfiguration_Generated
    {
        /// <summary>
        /// Email Addresses
        /// </summary>
        public new IEnumerable<string> EmailAddresses
        {
            get
            {
                return base.EmailAddresses.Split(',');
            }
            set
            {
                base.EmailAddresses = String.Join(",", value);
            }
        }
    }
}
