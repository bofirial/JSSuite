using JS.Suite.DataAbstraction.JSSuite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JS.Suite.BusinessLogic.Templating.Models.Email
{
    /// <summary>
    /// Confirm Email Model
    /// </summary>
    public class ConfirmationTokenModel
    {
        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public JSUser User { get; set; }
        /// <summary>
        /// Gets or sets the confirmation token.
        /// </summary>
        /// <value>
        /// The confirmation token.
        /// </value>
        public string ConfirmationToken { get; set; }
    }
}
