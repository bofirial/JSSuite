using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace JS.Suite.Hub.Areas.Support.Controllers
{
    /// <summary>
    /// Support Controller
    /// </summary>
    public class SupportController : SupportBaseController
    {
        /// <summary>
        /// Index Application
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return RedirectToAction("Index", "ApplicationLog");
        }
    }
}