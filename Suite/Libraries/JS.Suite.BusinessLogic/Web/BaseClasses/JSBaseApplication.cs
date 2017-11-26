using JS.Core.Web;
using JS.Suite.BusinessLogic.Messaging;
using JS.Suite.BusinessLogic.Templating;
using JS.Suite.Foundation.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace JS.Suite.BusinessLogic.Web
{
    /// <summary>
    /// JS Base Application
    /// </summary>
    public abstract class JSBaseApplication : BaseApplication
    {
        /// <summary>
        /// Gets the name of the application.
        /// </summary>
        /// <value>
        /// The name of the application.
        /// </value>
        public abstract string ApplicationName {get;}

        /// <summary>
        /// Application Start
        /// </summary>
        protected async override void Application_Start()
        {
            base.Application_Start();

            ViewEngines.Engines.Add(new TemplatingViewEngine());

            DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;

            await AppMessenger.Current.Send(MessageTypes.ApplicationStartUp, String.Format("{0} is starting up.", ApplicationName), "Application Startup");
        }

        /// <summary>
        /// Application End
        /// </summary>
        protected async void Application_End()
        {
            await AppMessenger.Current.Send(MessageTypes.ApplicationShutdown, String.Format("{0} is shutting down.", ApplicationName), "Application Shutdown");
        }
    }
}
