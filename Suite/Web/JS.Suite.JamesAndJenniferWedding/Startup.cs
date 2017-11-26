using JS.Suite.BusinessLogic.Web.BaseClasses;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(JS.Suite.JamesAndJenniferWedding.Startup))]
namespace JS.Suite.JamesAndJenniferWedding
{
    /// <summary>
    /// Startup
    /// </summary>
    public partial class Startup : StartupBase
    {
        /// <summary>
        /// Configures the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        public override void Configuration(IAppBuilder app)
        {
            base.Configuration(app);
        }
    }
}
