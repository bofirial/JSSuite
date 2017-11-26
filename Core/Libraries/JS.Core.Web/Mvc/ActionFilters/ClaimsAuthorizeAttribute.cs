using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace JS.Core.Web.Mvc.ActionFilters
{
    /// <summary>
    /// Claims Authorization Attribute
    /// </summary>
    public class ClaimsAuthorizeAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// Gets or sets the type of the claim.
        /// </summary>
        /// <value>
        /// The type of the claim.
        /// </value>
        private string _ClaimType { get; set; }

        /// <summary>
        /// Gets or sets the claim value.
        /// </summary>
        /// <value>
        /// The claim value.
        /// </value>
        private string _ClaimValue { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClaimsAuthorizeAttribute"/> class.
        /// </summary>
        /// <param name="claimType">Type of the claim.</param>
        /// <param name="claimValue">The claim value.</param>
        public ClaimsAuthorizeAttribute(string claimType, string claimValue)
        {
            _ClaimType = claimType;
            _ClaimValue = claimValue;
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
            if (!base.AuthorizeCore(httpContext))
	        {
		        return false;
	        }

            ClaimsIdentity identity = (ClaimsIdentity)httpContext.User.Identity;

            foreach (Claim claim in identity.Claims)
            {
                if (claim.Type == _ClaimType && claim.Value == _ClaimValue)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
