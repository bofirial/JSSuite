using JS.Core.Foundation.Helpers;
using JS.Suite.BusinessLogic.Security;
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
    /// Set Current Application Attribute
    /// </summary>
    public class SetCurrentApplicationAttribute : ActionFilterAttribute
    {
        private Applications _Application;

        /// <summary>
        /// Initializes a new instance of the <see cref="SetCurrentApplicationAttribute"/> class.
        /// </summary>
        /// <param name="applicationName">Name of the application.</param>
        public SetCurrentApplicationAttribute(string applicationName)
        {
            _Application = EnumHelper.Current.GetFromName<Applications>(applicationName);
        }

        /// <summary>
        /// Called by the ASP.NET MVC framework before the action method executes.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            SecurityManager.Current.UpdateCurrentApplication(_Application);

            base.OnActionExecuting(filterContext);
        }
    }
}
