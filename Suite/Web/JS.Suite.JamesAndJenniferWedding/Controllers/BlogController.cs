using JS.Core.Foundation.Data;
using JS.Core.Web;
using JS.Core.Web.Mvc.ActionFilters;
using JS.Core.Web.Mvc.Html;
using JS.Suite.BusinessLogic.JSWedding;
using JS.Suite.BusinessLogic.Security;
using JS.Suite.DataAbstraction.JSWedding;
using JS.Suite.Foundation.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace JSchafer.Controllers
{
    /// <summary>
    /// Blog Controller
    /// </summary>
    [CurrentMenuItem(WeddingMenuItems.Blog)]
    public class BlogController : BaseController
    {
        /// <summary>
        /// Blog Action
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Index()
        {
            List<WeddingBlogPost> posts = await WeddingBlogPostBusinessManager.Current.SelectAsync(SecurityManager.Current.ConnectionInfo, new WeddingBlogPost());

            return View("Blog", posts.OrderByDescending(p => p.PublishedDate).ToList());
        }

    }
}
