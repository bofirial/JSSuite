using JS.Core.Foundation.Constants;
using JS.Core.Foundation.Data;
using JS.Suite.BusinessLogic.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using Microsoft.AspNet.Identity;
using System.Globalization;
using System.Security.Principal;
using JS.Suite.BusinessLogic.JSSuite;
using JS.Suite.DataAbstraction.JSSuite;
using System.Security.Claims;

namespace JS.Suite.BusinessLogic.Web.Mvc.ActionFilters
{
    /// <summary>
    /// Populate Security Context Attribute
    /// </summary>
    public class PopulateSecurityContextAttribute : ActionFilterAttribute, IAuthenticationFilter
    {
        /// <summary>
        /// Authenticates the request.
        /// </summary>
        /// <param name="filterContext">The context to use for authentication.</param>
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            if (filterContext.Principal.Identity.IsAuthenticated)
            {
                SecurityManager.Current.SetSecurityContextFromIdentity((ClaimsIdentity)filterContext.Principal.Identity);
            }
            else
            {
                ClaimsIdentity identity = JSUserBusinessManager.Current.UserManager.CreateIdentity(
                    JSUserBusinessManager.Current.UserManager.FindById((int)SystemUsers.UnknownWebUser),
                    DefaultAuthenticationTypes.ApplicationCookie);

                SecurityManager.Current.SetSecurityContextFromIdentity(identity);
            }
        }

        /// <summary>
        /// Adds an authentication challenge to the current <see cref="T:System.Web.Mvc.ActionResult" />.
        /// </summary>
        /// <param name="filterContext">The context to use for the authentication challenge.</param>
        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
        }
    }
}
