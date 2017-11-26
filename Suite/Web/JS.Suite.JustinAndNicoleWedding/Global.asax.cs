using JS.Suite.BusinessLogic.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace JS.Suite.JustinAndNicoleWedding
{
    /// <summary>
    /// Mvc Application
    /// </summary>
    public class MvcApplication : JSBaseApplication
    {
        /// <summary>
        /// Gets the name of the application.
        /// </summary>
        /// <value>
        /// The name of the application.
        /// </value>
        public override string ApplicationName
        {
            get { return "Justin and Nicole Wedding Site"; }
        }

        /// <summary>
        /// Application Start
        /// </summary>
        protected override void Application_Start()
        {
            base.Application_Start();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
