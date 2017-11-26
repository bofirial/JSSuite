using JS.Core.Foundation.Data;
using JS.Core.Foundation.Helpers;
using JS.Core.Web.Mvc.ActionFilters;
using JS.Suite.BusinessLogic.JSSuite;
using JS.Suite.BusinessLogic.JSSupport;
using JS.Suite.BusinessLogic.Security;
using JS.Suite.DataAbstraction.JSSuite;
using JS.Suite.DataAbstraction.JSSupport;
using JS.Suite.Foundation.Constants;
using JS.Suite.Hub.Areas.Support.Models.TrafficLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using JS.Core.Foundation.ExtensionMethods;

namespace JS.Suite.Hub.Areas.Support.Controllers
{
    /// <summary>
    /// Traffic Log Controller
    /// </summary>
    [CurrentMenuItem(SuiteMenuItems.TrafficLog)]
    public class TrafficLogController : SupportBaseController
    {
        /// <summary>
        /// Index
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(TrafficLogRequest filter)
        {
            TrafficLogRequest model = new TrafficLogRequest();

            if (!filter.PublicPropertiesEquals(model))
            {
                ModelState.Clear();

                if (!String.IsNullOrEmpty(filter.TrackingGuid))
                {
                    model.TrackingGuid = filter.TrackingGuid;
                }

                if (!String.IsNullOrEmpty(filter.Location))
                {
                    model.Location = filter.Location;
                }

                if (filter.InsertedOn_From != null)
                {
                    model.InsertedOn_From = filter.InsertedOn_From;
                }

                if (filter.InsertedOn_To != null)
                {
                    model.InsertedOn_To = filter.InsertedOn_To;
                }

                if (filter.ApplicationId != default(int))
                {
                    model.ApplicationId = filter.ApplicationId;
                }
            }
            else
            {
                model.InsertedOn_From = DateTime.Today;
                model.InsertedOn_To = DateTime.Today.AddDays(1);
            }

            ViewBag.AvailableApplications = EnumHelper.Current.GetSelectList<Applications>();

            return View(model);
        }

        /// <summary>
        /// Traffics the log grid data.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public async Task<JsonResult> TrafficLogGridData(TrafficLogRequest filter)
        {
            filter.ClearEmptyModifiedColumns();

            PagedResult<TrafficLogRequest> pagedResult = await TrafficLogRequestBusinessManager.Current.PagingSelectAsync(SecurityManager.Current.ConnectionInfo, filter);

            foreach (TrafficLogRequest trafficLog in pagedResult.Results)
            {
                List<JSUser> jsUsers = await JSUserBusinessManager.Current.SelectAsync(SecurityManager.Current.ConnectionInfo, new JSUser()
                {
                    JSUserId = trafficLog.JSUserId
                });

                if (jsUsers.Any())
                {
                    trafficLog.JSUserName = jsUsers.First().Name; 
                }

                trafficLog.ApplicationName = EnumHelper.Current.GetNameWithSpaces((Applications)trafficLog.ApplicationId);
            }

            return Json(pagedResult);
        }

        /// <summary>
        /// Details the modal.
        /// </summary>
        /// <param name="trafficLogRequestId">The traffic log request identifier.</param>
        /// <param name="trackingGuid">The tracking unique identifier.</param>
        /// <returns></returns>
        public async Task<ActionResult> DetailModal(int trafficLogRequestId, string trackingGuid)
        {
            DetailModalViewModel model = new DetailModalViewModel();

            var trafficLogRequest = TrafficLogRequestBusinessManager.Current.SelectAsync(SecurityManager.Current.ConnectionInfo,
                new TrafficLogRequest() { TrafficLogRequestId = trafficLogRequestId });

            var trafficLogResponse = TrafficLogResponseBusinessManager.Current.SelectAsync(SecurityManager.Current.ConnectionInfo,
                new TrafficLogResponse() { TrackingGuid = trackingGuid });

            var applicationLogs = ApplicationLogBusinessManager.Current.SelectAsync(SecurityManager.Current.ConnectionInfo,
                new ApplicationLog() { TrackingGuid = trackingGuid });

            model.TrafficLogRequest = (await trafficLogRequest).FirstOrDefault();

            List<JSUser> jsUsers = await JSUserBusinessManager.Current.SelectAsync(SecurityManager.Current.ConnectionInfo, new JSUser()
            {
                JSUserId = model.TrafficLogRequest.JSUserId
            });

            if (jsUsers.Any())
            {
                model.TrafficLogRequest.JSUserName = jsUsers.First().Name;
            }

            model.TrafficLogRequest.ApplicationName = EnumHelper.Current.GetNameWithSpaces((Applications)model.TrafficLogRequest.ApplicationId);

            model.ApplicationLogs = await applicationLogs;

            model.TrafficLogResponse = (await trafficLogResponse).FirstOrDefault();

            return View(model);
        }

        /// <summary>
        /// Traffic Log Location Summary.
        /// </summary>
        /// <returns></returns>
        [CurrentMenuItem(SuiteMenuItems.TrafficLogLocationSummary)]
        public ActionResult TrafficLogLocationSummary()
        {
            TrafficLogSummary model = new TrafficLogSummary();

            model.InsertedOn_From = DateTime.Today;
            model.InsertedOn_To = DateTime.Today.AddDays(1);

            ViewBag.AvailableApplications = EnumHelper.Current.GetSelectList<Applications>();

            return View(model);
        }

        /// <summary>
        /// Traffics the log summary grid data.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public async Task<JsonResult> TrafficLogSummaryGridData(TrafficLogSummary filter)
        {
            filter.ClearEmptyModifiedColumns();

            PagedResult<TrafficLogSummary> pagedResult = await TrafficLogRequestBusinessManager.Current.TrafficLogSummaryPagingSelectAsync(SecurityManager.Current.ConnectionInfo, filter);
            
            return Json(pagedResult);
        }
    }
}