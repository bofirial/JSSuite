using JS.Core.Web;
using JS.Core.Web.Mvc.ActionFilters;
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
using System.Web.Routing;

namespace JS.Suite.JustinAndNicoleWedding.Controllers
{
    /// <summary>
    /// Wedding Party Controller
    /// </summary>
    [CurrentMenuItem(WeddingMenuItems.WeddingParty)]
    public class WeddingPartyController : BaseController
    {
        /// <summary>
        /// Wedding Party Index Screen
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Index()
        {
            List<List<WeddingPartyMember>> weddingPartyMembers = await WeddingPartyMemberBusinessManager.Current.SelectWithSummaryPartnersAndGroupByType(SecurityManager.Current.ConnectionInfo, new WeddingPartyMember()
            {
                WeddingSiteId = (int)WeddingSites.NicoleAndJustinWedding
            });


            return View("WeddingParty", weddingPartyMembers);
        }

        /// <summary>
        /// Wedding Party Member
        /// </summary>
        /// <param name="weddingPartyMemberId">The wedding party member identifier.</param>
        /// <returns></returns>
        public async Task<ActionResult> WeddingPartyMember(int weddingPartyMemberId)
        {
            WeddingPartyMember model = (await WeddingPartyMemberBusinessManager.Current.SelectAsync(SecurityManager.Current.ConnectionInfo, new WeddingPartyMember()
            {
                WeddingPartyMemberId = weddingPartyMemberId
            })).FirstOrDefault();

            if (model.SummaryPartnerId != default(int))
            {
                model.SummaryPartner = (await WeddingPartyMemberBusinessManager.Current.SelectAsync(SecurityManager.Current.ConnectionInfo, new WeddingPartyMember()
                {
                    WeddingPartyMemberId = model.SummaryPartnerId
                })).FirstOrDefault();
            }

            return View("WeddingPartyMemberDetail", model);
        }
    }
}