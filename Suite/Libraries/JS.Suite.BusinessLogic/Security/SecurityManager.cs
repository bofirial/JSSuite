using JS.Core.Foundation.BaseClasses;
using JS.Core.Foundation.Data;
using JS.Core.Foundation.Helpers;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using JS.Suite.BusinessLogic.JSSuite;
using JS.Suite.DataAbstraction.JSSuite;
using System.Threading;
using System.Security.Claims;
using JS.Core.Foundation.Constants;
using System.Globalization;
using System.Security.Principal;
using JS.Core.Foundation.ExtensionMethods;
using JS.Suite.Foundation.Constants;
using JS.Suite.BusinessLogic.Configuration;
using System.Configuration;

namespace JS.Suite.BusinessLogic.Security
{
    /// <summary>
    /// Security Manager
    /// </summary>
    public class SecurityManager : SingletonBase<SecurityManager>
    {
        private const string _ThreadKey = "securityContext";

        private void SetSecurityContextOnThreadContext(ISecurityContext securityContext)
        {
            ThreadContextHelper.Current.SetValue(_ThreadKey, securityContext);
        }

        /// <summary>
        /// Gets the security context.
        /// </summary>
        /// <value>
        /// The security context.
        /// </value>
        private ISecurityContext SecurityContext
        {
            get
            {
                if (!ThreadContextHelper.Current.ContainsKeyOfType<ISecurityContext>(_ThreadKey))
                {
                    SetSecurityContextOnThreadContext(BuildSecurityContextForUnknownSystemUser());
                }

                return ThreadContextHelper.Current.GetValue<ISecurityContext>(_ThreadKey);
            }
        }

        private ISecurityContext BuildSecurityContextForUnknownSystemUser()
        {
            //This function can't call the database or SecurityContext because it is used to build the default ConnectionInfo

            ISecurityContext securityContext = new SecurityContext();

            securityContext.ConnectionInfo = new ConnectionInfo()
            {
                UserId = (int)SystemUsers.UnknownSystemUser,
                Locale = Defaults.Locale
            };

            securityContext.TrackingGuid = Guid.NewGuid().ToString();

            SecurityManagerSection securityManagerSection = (SecurityManagerSection)ConfigurationManager.GetSection("securityManager");

            securityContext.CurrentApplication = securityManagerSection.DefaultCurrentApplication;

            securityContext.UserType = JSUserTypes.User;

            securityContext.AvailableApplications = new List<Applications>() { securityContext.CurrentApplication };

            return securityContext;
        }

        /// <summary>
        /// Sets the security context from identity.
        /// </summary>
        /// <param name="identity">The identity.</param>
        internal void SetSecurityContextFromIdentity(ClaimsIdentity identity)
        {
            ISecurityContext securityContext = new SecurityContext();

            securityContext.ConnectionInfo = new ConnectionInfo()
            {
                UserId = identity.GetUserId<int>(),
                Locale = CultureInfo.CurrentCulture.Name
            };

            if (!String.IsNullOrEmpty(SecurityContext.TrackingGuid))
            {
                securityContext.TrackingGuid = SecurityContext.TrackingGuid;
            }
            else
            {
                securityContext.TrackingGuid = Guid.NewGuid().ToString();
            }

            securityContext.CurrentApplication = SecurityContext.CurrentApplication;

            securityContext.AvailableApplications = new List<Applications>();

            foreach (Claim claim in identity.Claims)
            {
                if (claim.Type == JSClaimTypes.UserType)
                {
                    securityContext.UserType = EnumHelper.Current.GetFromName<JSUserTypes>(claim.Value);
                }
                else if (claim.Type == JSClaimTypes.Application)
                {
                    securityContext.AvailableApplications.Add(EnumHelper.Current.GetFromName<Applications>(claim.Value));
                }
            }

            SetSecurityContextOnThreadContext(securityContext);
        }

        /// <summary>
        /// Updates the current application.
        /// </summary>
        /// <param name="application">The application.</param>
        internal void UpdateCurrentApplication(Applications application)
        {
            ISecurityContext securityContext = SecurityContext;

            securityContext.CurrentApplication = application;

            SetSecurityContextOnThreadContext(securityContext);
        }

        /// <summary>
        /// Gets the connection information.
        /// </summary>
        /// <value>
        /// The connection information.
        /// </value>
        public IConnectionInfo ConnectionInfo
        {
            get { return SecurityContext.ConnectionInfo; }
        }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int UserId
        {
            get { return SecurityContext.ConnectionInfo.UserId; }
        }

        /// <summary>
        /// Gets the tracking unique identifier.
        /// </summary>
        /// <value>
        /// The tracking unique identifier.
        /// </value>
        public string TrackingGuid
        {
            get { return SecurityContext.TrackingGuid; }
        }

        /// <summary>
        /// Gets or sets the type of the user.
        /// </summary>
        /// <value>
        /// The type of the user.
        /// </value>
        public JSUserTypes UserType 
        { 
            get { return SecurityContext.UserType; }
        }

        /// <summary>
        /// Gets or sets the current application.
        /// </summary>
        /// <value>
        /// The current application.
        /// </value>
        public Applications CurrentApplication
        {
            get { return SecurityContext.CurrentApplication; }
        }

        /// <summary>
        /// Gets or sets the available applications.
        /// </summary>
        /// <value>
        /// The available applications.
        /// </value>
        public List<Applications> AvailableApplications
        {
            get { return SecurityContext.AvailableApplications; }
        }

        /// <summary>
        /// Signs a User into the Application
        /// </summary>
        /// <param name="systemUser">The system user.</param>
        public void SignIn(SystemUsers systemUser)
        {
            SignIn(JSUserBusinessManager.Current.UserManager.FindById((int)systemUser));
        }

        /// <summary>
        /// Signs a User into the Application
        /// </summary>
        /// <param name="user">The user.</param>
        public void SignIn(JSUser user)
        {
            if (ThreadContextHelper.Current.InWebContext())
            {
                HttpContext.Current.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            }
            else
            {
                Thread.CurrentPrincipal = null;
            }

            var identity = JSUserBusinessManager.Current.UserManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);

            if (ThreadContextHelper.Current.InWebContext())
            {
                HttpContext.Current.GetOwinContext().Authentication.SignIn(new AuthenticationProperties() { IsPersistent = true }, identity);
            }
            else
            {
                Thread.CurrentPrincipal = new ClaimsPrincipal(identity);
            }

            SetSecurityContextFromIdentity(identity);
        }
    }
}
