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
    /// Create New Game Controller
    /// </summary>
    [CurrentMenuItem(SuiteMenuItems.NewGame)]
    public class CreateNewGameController : GamesBaseController
    {
        /// <summary>
        /// Index
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
    }
}