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
    /// Guest Information
    /// </summary>
    [CurrentMenuItem(WeddingMenuItems.GuestInformation)]
    public class GuestInformationController : BaseController
    {
        /// <summary>
        /// Index
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View("GuestInformation");
        }
	}
}