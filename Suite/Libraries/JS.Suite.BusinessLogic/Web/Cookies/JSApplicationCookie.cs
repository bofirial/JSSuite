using JS.Core.Foundation.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JS.Suite.BusinessLogic.Web.Cookies
{
    /// <summary>
    /// Application Cookie for JS
    /// </summary>
    public class JSApplicationCookie : SerializationBase<JSApplicationCookie>
    {
        /// <summary>
        /// Gets or sets the tracking unique identifier.
        /// </summary>
        /// <value>
        /// The tracking unique identifier.
        /// </value>
        public string TrafficLogGuid { get; set; }
    }
}
