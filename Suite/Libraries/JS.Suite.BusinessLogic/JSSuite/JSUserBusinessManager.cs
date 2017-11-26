using JS.Suite.BusinessLogic.JSSuite.Generated;
using JS.Suite.BusinessLogic.Security;
using JS.Suite.DataAbstraction.JSSuite;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Owin.Security;
using System.Web.Mvc;
using Owin;
using Microsoft.Owin.Security.DataProtection;
using JS.Core.Foundation.Helpers;
using JS.Suite.BusinessLogic.Security.DataProtectorToken;
using JS.Suite.BusinessLogic.Messaging.Email;

namespace JS.Suite.BusinessLogic.JSSuite
{
    /// <summary>
    /// JSUser Business Manager
    /// </summary>
    public class JSUserBusinessManager : JSUserBusinessManager_Generated<JSUserBusinessManager>
    {
        private JSUserBusinessManager()
        {
            UserManager = CreateUserManager();
        }

        private UserManager<JSUser, int> CreateUserManager()
        {
            UserManager<JSUser, int> userManager = new UserManager<JSUser, int>(new JSUserStore());

            userManager.UserValidator = new UserValidator<JSUser, int>(userManager)
            {
                AllowOnlyAlphanumericUserNames = true,
                RequireUniqueEmail = false
            };
            
            userManager.MaxFailedAccessAttemptsBeforeLockout = ConfigurationHelper.Current.GetSetting<int>("MaxFailedAccessAttemptsBeforeLockout");

            userManager.DefaultAccountLockoutTimeSpan = ConfigurationHelper.Current.GetSetting<TimeSpan>("DefaultAccountLockoutTimeSpan");

            userManager.UserTokenProvider = new DataProtectorTokenProvider<JSUser, int>(DataProtectorTokenManager.Current.Provider.Create("UserToken"));
            
            return userManager;
        }

        /// <summary>
        /// Gets the user manager.
        /// </summary>
        /// <value>
        /// The user manager.
        /// </value>
        public UserManager<JSUser, int> UserManager { get; private set; }
    }
}
