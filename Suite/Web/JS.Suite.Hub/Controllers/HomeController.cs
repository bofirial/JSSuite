using JS.Core.Foundation.Helpers;
using JS.Core.Web;
using JS.Suite.BusinessLogic.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JS.Suite.Hub.Controllers
{
    /// <summary>
    /// Home Controller
    /// </summary>
    public class HomeController : BaseController
    {
        /// <summary>
        /// View for Selecting an Application
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            if (SecurityManager.Current.AvailableApplications.Count < 2)
            {
                string appName = EnumHelper.Current.GetName(SecurityManager.Current.AvailableApplications.FirstOrDefault());

                return RedirectToAction("Index", appName, new { area = appName });
            }

            return View();
        }
    }
}