using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JS.Suite.Hub.Areas.Collections.Controllers
{
    /// <summary>
    /// Collections Controller
    /// </summary>
    public class CollectionsController : CollectionsBaseController
    {
        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return RedirectToAction("List", "CollectionGroup");
        }
    }
}