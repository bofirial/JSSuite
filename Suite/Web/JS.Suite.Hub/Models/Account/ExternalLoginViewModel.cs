using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JS.Suite.Hub.Models.Account
{
    /// <summary>
    /// External Login View Model
    /// </summary>
    public class ExternalLoginViewModel
    {
        /// <summary>
        /// Gets or sets the action.
        /// </summary>
        /// <value>
        /// The action.
        /// </value>
        public string Action { get; set; }

        /// <summary>
        /// Gets or sets the route values.
        /// </summary>
        /// <value>
        /// The route values.
        /// </value>
        public object RouteValues { get; set; }

        /// <summary>
        /// Gets or sets the button text format.
        /// </summary>
        /// <value>
        /// The button text format.
        /// </value>
        public string ButtonTextFormat { get; set; }

        /// <summary>
        /// Gets or sets the button hover format.
        /// </summary>
        /// <value>
        /// The button hover format.
        /// </value>
        public string ButtonHoverFormat { get; set; }

        /// <summary>
        /// Gets or sets the login providers.
        /// </summary>
        /// <value>
        /// The login providers.
        /// </value>
        public IList<UserLoginInfo> LoginProviders { get; set; }
    }
}