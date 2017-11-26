using JS.Core.Web.Mvc.ActionFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace JS.Suite.BusinessLogic.Web.Mvc.ActionFilters
{
    /// <summary>
    /// Application Authorization Attribute
    /// </summary>
    public class ApplicationAuthorizeAttribute : ClaimsAuthorizeAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationAuthorizeAttribute" /> class.
        /// </summary>
        /// <param name="applicationName">Name of the application.</param>
        public ApplicationAuthorizeAttribute(string applicationName)
            :base("Application", applicationName)
        {

        }

        /// <summary>
        /// When overridden, provides an entry point for custom authorization checks.
        /// </summary>
        /// <param name="httpContext">The HTTP context, which encapsulates all HTTP-specific information about an individual HTTP request.</param>
        /// <returns>
        /// true if the user is authorized; otherwise, false.
        /// </returns>
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return base.AuthorizeCore(httpContext);
        }
    }
}
