using JS.Core.Foundation.Helpers;
using JS.Suite.BusinessLogic.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace JS.Suite.JustinAndNicoleWedding.ActionFilters
{
    /// <summary>
    /// Action Filter for Setting a Flag in the ViewBag if the Wedding is finished
    /// </summary>
    public sealed class PostWeddingMessageFlagAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Sets the Post Wedding Message Flag in the View Bag
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            DateTime weddingDate = DateTime.Parse(ConfigurationHelper.Current.GetSetting("WeddingDateTime"));

            if (DateTimeHelper.Current.GetLocalNow(SecurityManager.Current.ConnectionInfo) > weddingDate)
            {
                filterContext.Controller.ViewBag.PostWeddingMessageFlag = true;
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
