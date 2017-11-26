using JSSupport = JS.Suite.DataAbstraction.JSSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JS.Suite.DataAbstraction.JSSupport;

namespace JS.Suite.Hub.Areas.Support.Models.ApplicationLog
{
    /// <summary>
    /// Detail Modal View Model
    /// </summary>
    public class DetailModalViewModel
    {
        /// <summary>
        /// Gets or sets the application log.
        /// </summary>
        /// <value>
        /// The application log.
        /// </value>
        public JSSupport.ApplicationLog ApplicationLog { get; set; }

        /// <summary>
        /// Gets or sets the additional application logs.
        /// </summary>
        /// <value>
        /// The additional application logs.
        /// </value>
        public List<JSSupport.ApplicationLog> AdditionalApplicationLogs { get; set; }

        /// <summary>
        /// Gets or sets the traffic log request.
        /// </summary>
        /// <value>
        /// The traffic log request.
        /// </value>
        public TrafficLogRequest TrafficLogRequest { get; set; }
    }
}