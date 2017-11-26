using JS.Core.Web.Mvc.ActionFilters;
using JS.Suite.Foundation.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JS.Suite.Hub.Areas.Games.Controllers
{
    /// <summary>
    /// Games Controller
    /// </summary>
    [CurrentMenuItem(SuiteMenuItems.MyGames)]
    public class GamesController : GamesBaseController
    {
        /// <summary>
        /// Index
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }
    }
}