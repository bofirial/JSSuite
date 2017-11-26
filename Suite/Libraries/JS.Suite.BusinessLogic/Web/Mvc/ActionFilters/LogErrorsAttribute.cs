using JS.Core.Foundation.Constants;
using JS.Core.Foundation.ErrorHandling;
using JS.Suite.BusinessLogic.Helpers;
using JS.Suite.BusinessLogic.Messaging;
using JS.Suite.Foundation.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace JS.Suite.BusinessLogic.Web.Mvc.ActionFilters
{
    /// <summary>
    /// Logs Application Errors to the Application Log
    /// </summary>
    public class LogErrorsAttribute : ActionFilterAttribute, IExceptionFilter
    {
        /// <summary>
        /// Called when an exception occurs.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        public void OnException(ExceptionContext filterContext)
        {
            ExceptionHelper.Current.SendAppMessageForException(filterContext.Exception);
        }
    }
}
