using JS.Core.Foundation.BaseClasses;
using JS.Core.Foundation.ErrorHandling;
using JS.Core.Web;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace JS.Suite.BusinessLogic.Templating
{
    /// <summary>
    /// Email Template Engine
    /// </summary>
    public class EmailTemplateEngine : SingletonBase<EmailTemplateEngine>
    {
        /// <summary>
        /// Generates an email from a Template
        /// </summary>
        /// <param name="emailViewName">Type of the email.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public string Generate(string emailViewName, object model = null)
        {
            BaseController controller = new BaseController();

            controller.ControllerContext = new ControllerContext(HttpContext.Current.Request.RequestContext, controller);

            if (String.IsNullOrEmpty(System.Web.HttpRuntime.AppDomainAppId))
            {
                throw new CoreException("Cannot send email from non-http application.", "HttpContext is required to send email in JS.");
            }

            using (StringWriter sw = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindView(controller.ControllerContext, emailViewName, null); ;

                if (viewResult.View == null)
                {
                    throw new CoreException("No view found for email template.", 
                        String.Format("No view could be found for email template: {0}.  The following locations were searched: {1}",
                        emailViewName,
                        String.Join("\n", viewResult.SearchedLocations)));
                }

                controller.ViewData.Model = model;

                ViewContext viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(controller.ControllerContext, viewResult.View);

                return sw.ToString().Trim();
            }
        }
    }
}
