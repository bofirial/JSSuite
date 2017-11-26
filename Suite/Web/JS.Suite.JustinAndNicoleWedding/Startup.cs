using JS.Suite.BusinessLogic.Web.BaseClasses;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(JS.Suite.JustinAndNicoleWedding.Startup))]
namespace JS.Suite.JustinAndNicoleWedding
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
