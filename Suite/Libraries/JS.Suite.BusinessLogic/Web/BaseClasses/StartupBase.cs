using JS.Suite.BusinessLogic.Security.DataProtectorToken;
using Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.DataProtection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JS.Suite.BusinessLogic.Web.BaseClasses
{
    /// <summary>
    /// Startup Base
    /// </summary>
    public abstract class StartupBase
    {
        /// <summary>
        /// Configurations the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        public virtual void Configuration(IAppBuilder app)
        {
            DataProtectorTokenManager.Current.Provider = app.GetDataProtectionProvider();
        }
    }
}
