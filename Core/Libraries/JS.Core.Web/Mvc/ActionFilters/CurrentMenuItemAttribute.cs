using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace JS.Core.Web.Mvc.ActionFilters
{
    /// <summary>
    /// Action Filter for Setting the Current Menu Item in the ViewBag
    /// </summary>
    public sealed class CurrentMenuItemAttribute : ActionFilterAttribute
    {
        private string CurrentMenuItem { get; set; }
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="currentMenuItem"></param>
        public CurrentMenuItemAttribute(string currentMenuItem)
        {
            CurrentMenuItem = currentMenuItem;
        }

        /// <summary>
        /// Sets the Current Menu Item in the View Bag
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.Controller.ViewBag.CurrentMenuItem = CurrentMenuItem;

            base.OnActionExecuting(filterContext);
        }
    }
}
