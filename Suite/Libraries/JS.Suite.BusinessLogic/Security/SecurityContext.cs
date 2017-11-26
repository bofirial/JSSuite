using JS.Core.Foundation.Data;
using JS.Suite.Foundation.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JS.Suite.BusinessLogic.Security
{
    /// <summary>
    /// Security Context
    /// </summary>
    public class SecurityContext : ISecurityContext
    {
        private IConnectionInfo _ConnectionInfo;

        /// <summary>
        /// Gets or sets the connection information.
        /// </summary>
        /// <value>
        /// The connection information.
        /// </value>
        public IConnectionInfo ConnectionInfo
        {
            get
            {
                if (_ConnectionInfo == null)
                {
                    _ConnectionInfo = new ConnectionInfo();
                }

                return _ConnectionInfo.Clone();
            }
            set { _ConnectionInfo = value; }
        }

        /// <summary>
        /// Gets or sets the tracking unique identifier.
        /// </summary>
        /// <value>
        /// The tracking unique identifier.
        /// </value>
        public string TrackingGuid { get; set; }
        
        /// <summary>
        /// Gets or sets the type of the user.
        /// </summary>
        /// <value>
        /// The type of the user.
        /// </value>
        public JSUserTypes UserType { get; set; }

        /// <summary>
        /// Gets or sets the current application.
        /// </summary>
        /// <value>
        /// The current application.
        /// </value>
        public Applications CurrentApplication { get; set; }

        /// <summary>
        /// Gets or sets the available applications.
        /// </summary>
        /// <value>
        /// The available applications.
        /// </value>
        public List<Applications> AvailableApplications { get; set; }
    }
}
