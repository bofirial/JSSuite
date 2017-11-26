using JS.Core.Foundation.Data;
using JS.Core.Web;
using JS.Core.Web.Mvc.ActionFilters;
using JS.Suite.BusinessLogic.JSWedding;
using JS.Suite.BusinessLogic.Messaging;
using JS.Suite.BusinessLogic.Security;
using JS.Suite.DataAbstraction.JSWedding;
using JS.Suite.Foundation.Constants;
using JS.Suite.JustinAndNicoleWedding.Models.GuestBook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace JS.Suite.JustinAndNicoleWedding.Controllers
{
    /// <summary>
    /// Guest Book Controller
    /// </summary>
    [CurrentMenuItem(WeddingMenuItems.GuestBook)]
    public class GuestBookController : BaseController
    {
        private WeddingSites weddingSite = WeddingSites.NicoleAndJustinWedding;

        /// <summary>
        /// Index Get
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            GuestBookViewModel model = new GuestBookViewModel();

            model.WeddingComments = await WeddingCommentBusinessManager.Current.SelectAsync(SecurityManager.Current.ConnectionInfo, new WeddingComment()
            {
                WebSiteId = (int)weddingSite,
                HiddenFlag = false
            });

            model.WeddingComments = model.WeddingComments.OrderByDescending(wc => wc.InsertedOn);

            return View("GuestBook", model);
        }

        /// <summary>
        /// Index Post
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(GuestBookViewModel model)
        {
            if (ModelState.IsValid)
            {
                ModelState.Clear();

                Task<IProcessResult> appMessageResult;

                if (WeddingCommentBusinessManager.Current.IsSpam(model.NewWeddingComment))
                {
                    model.NewWeddingComment.HiddenFlag = true;

                    appMessageResult = AppMessenger.Current.Send(MessageTypes.SpamWeddingComment,
                        String.Format("<h4>From: {0}</h4><p>{1}</p><br />", model.NewWeddingComment.UserName, model.NewWeddingComment.Comment),
                        "Hidden Comment(s)");

                    ModelState.AddModelError("NewWeddingComment.Comment", "Your comment has been hidden until we can verify it is not an advertisement. Thank you for visiting our website.");
                }
                else
                {
                    appMessageResult = AppMessenger.Current.Send(MessageTypes.NicoleAndJustinWeddingComment,
                        String.Format("<h3>{0}</h3><h4>{1}</h4><p>{2}</p>", model.NewWeddingComment.UserName, model.NewWeddingComment.Email, model.NewWeddingComment.Comment),
                        String.Format("New Wedding Comment from {0}", model.NewWeddingComment.UserName));
                }

                model.NewWeddingComment.WebSiteId = (int)weddingSite;

                await WeddingCommentBusinessManager.Current.InsertAsync(SecurityManager.Current.ConnectionInfo, model.NewWeddingComment);
                await appMessageResult;
            }

            return await Index();
        }
	}
}