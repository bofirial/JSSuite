using JS.Core.Foundation.Data;
using JS.Suite.Foundation.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JS.Suite.BusinessLogic.Security
{
    /// <summary>
    /// Security Context Interface
    /// </summary>
    public interface ISecurityContext
    {
        /// <summary>
        /// Gets or sets the connection information.
        /// </summary>
        /// <value>
        /// The connection information.
        /// </value>
        IConnectionInfo ConnectionInfo { get; set; }

        /// <summary>
        /// Gets or sets the tracking unique identifier.
        /// </summary>
        /// <value>
        /// The tracking unique identifier.
        /// </value>
        string TrackingGuid { get; set; }

        /// <summary>
        /// Gets or sets the type of the user.
        /// </summary>
        /// <value>
        /// The type of the user.
        /// </value>
        JSUserTypes UserType { get; set; }

        /// <summary>
        /// Gets or sets the current application.
        /// </summary>
        /// <value>
        /// The current application.
        /// </value>
        Applications CurrentApplication { get; set; }

        /// <summary>
        /// Gets or sets the available applications.
        /// </summary>
        /// <value>
        /// The available applications.
        /// </value>
        List<Applications> AvailableApplications { get; set; }
    }
}
