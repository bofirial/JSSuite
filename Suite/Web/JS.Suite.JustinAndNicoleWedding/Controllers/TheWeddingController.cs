using JS.Core.Foundation.ErrorHandling;
using JS.Core.Foundation.Helpers;
using JS.Core.Web;
using JS.Core.Web.Mvc.ActionFilters;
using JS.Suite.Foundation.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JS.Suite.JustinAndNicoleWedding.Controllers
{
    /// <summary>
    /// The Wedding Controller
    /// </summary>
    [CurrentMenuItem(WeddingMenuItems.TheWedding)]
    public class TheWeddingController : BaseController
    {
        /// <summary>
        /// Index
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            DateTime WeddingDate = DateTime.Parse(ConfigurationHelper.Current.GetSetting("WeddingDateTime"));

            return View("TheWedding", WeddingDate);
        }

        /// <summary>
        /// The Church
        /// </summary>
        /// <returns></returns>
        public ActionResult TheChurch()
        {
            return View();
        }

        /// <summary>
        /// The Reception
        /// </summary>
        /// <returns></returns>
        public ActionResult TheReception()
        {
            return View();
        }
	}
}