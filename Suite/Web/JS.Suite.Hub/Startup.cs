using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.DataProtection;
using JS.Suite.BusinessLogic.Security.DataProtectorToken;
using JS.Suite.BusinessLogic.Web.BaseClasses;

[assembly: OwinStartupAttribute(typeof(JS.Suite.Hub.Startup))]
namespace JS.Suite.Hub
{
    /// <summary>
    /// Startup
    /// </summary>
    public partial class Startup : StartupBase
    {
        /// <summary>
        /// Configurations the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        public override void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            base.Configuration(app);
        }
    }
}
