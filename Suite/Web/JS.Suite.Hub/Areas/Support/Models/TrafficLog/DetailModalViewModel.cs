using JS.Suite.DataAbstraction.JSSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JSSupport = JS.Suite.DataAbstraction.JSSupport;

namespace JS.Suite.Hub.Areas.Support.Models.TrafficLog
{
    /// <summary>
    /// Detail Modal View Model
    /// </summary>
    public class DetailModalViewModel
    {
        /// <summary>
        /// Gets or sets the traffic log request.
        /// </summary>
        /// <value>
        /// The traffic log request.
        /// </value>
        public TrafficLogRequest TrafficLogRequest { get; set; }

        /// <summary>
        /// Gets or sets the traffic log response.
        /// </summary>
        /// <value>
        /// The traffic log response.
        /// </value>
        public TrafficLogResponse TrafficLogResponse { get; set; }

        /// <summary>
        /// Gets or sets the application logs.
        /// </summary>
        /// <value>
        /// The application logs.
        /// </value>
        public List<JSSupport.ApplicationLog> ApplicationLogs { get; set; }
    }
}