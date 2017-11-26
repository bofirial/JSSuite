using JS.Core.Foundation.Constants;
using JS.Core.Web;
using JS.Suite.BusinessLogic.Messaging;
using JS.Suite.Foundation.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace JS.Suite.BusinessLogic.Web.BaseClasses
{
    /// <summary>
    /// Base Error Controller
    /// </summary>
    [AllowAnonymous]
    public class BaseErrorController : BaseController
    {
        /// <summary>
        /// Error Action.
        /// </summary>
        /// <returns></returns>
        public virtual ActionResult Index()
        {
            return View("Common/Errors/Error");
        }

        /// <summary>
        /// Error Action for 404s.
        /// </summary>
        /// <param name="aspxerrorpath">The aspxerrorpath.</param>
        /// <returns></returns>
        public virtual async Task<ActionResult> NotFound(string aspxerrorpath = null)
        {
            string errorPath = aspxerrorpath;

            if (String.IsNullOrEmpty(errorPath))
            {
                errorPath = Request.RawUrl;
            }

            await AppMessenger.Current.Send(MessageTypes.NotFound404, String.Format("Url Not Found: {0}", errorPath), "404 Not Found Error", TraceLevels.Error);

            HttpContext.Response.StatusCode = 404;

            return View("Common/Errors/NotFound");
        }

        /// <summary>
        /// Returns the Robots.txt File
        /// </summary>
        /// <returns></returns>
        public FileContentResult Robots()
        {
            var content = "User-agent: *" + Environment.NewLine;
            
            //content += "Sitemap: https://jumpingsalamander.com/sitemap.xml";

            return File(
                    Encoding.UTF8.GetBytes(content),
                    "text/plain");
        }
    }
}
