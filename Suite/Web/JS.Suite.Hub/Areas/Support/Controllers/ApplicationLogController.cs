using JS.Core.Foundation.Constants;
using JS.Core.Foundation.Data;
using JS.Core.Foundation.Helpers;
using JS.Core.Web.Mvc.ActionFilters;
using JS.Core.Web.Mvc.Html;
using JS.Suite.BusinessLogic.JSSuite;
using JS.Suite.BusinessLogic.JSSupport;
using JS.Suite.BusinessLogic.Security;
using JS.Suite.DataAbstraction.JSSuite;
using JS.Suite.DataAbstraction.JSSupport;
using JS.Suite.Foundation.Constants;
using JS.Suite.Hub.Areas.Support.Models.ApplicationLog;
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
    /// Application Log Controller
    /// </summary>
    [CurrentMenuItem(SuiteMenuItems.ApplicationLog)]
    public class ApplicationLogController : SupportBaseController
    {
        /// <summary>
        /// Index
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(ApplicationLog filter)
        {
            ApplicationLog model = new ApplicationLog();

            if (!filter.PublicPropertiesEquals(model))
            {
                ModelState.Clear();

                if (!String.IsNullOrEmpty(filter.TrackingGuid))
                {
                    model.TrackingGuid = filter.TrackingGuid;
                }

                if (!String.IsNullOrEmpty(filter.Subject))
                {
                    model.Subject = filter.Subject;
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

                if (filter.TraceLevelId != default(int))
                {
                    model.TraceLevelId = filter.TraceLevelId;
                }
            }
            else
            {
                model.InsertedOn_From = DateTime.Today;
                model.InsertedOn_To = DateTime.Today.AddDays(1); 
            }

            ViewBag.AvailableTraceLevels = EnumHelper.Current.GetSelectList<TraceLevels>();

            ViewBag.AvailableMessageTypes = EnumHelper.Current.GetSelectList<MessageTypes>();

            ViewBag.AvailableApplications = EnumHelper.Current.GetSelectList<Applications>();

            return View(model);
        }

        /// <summary>
        /// Applications the log grid data.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public async Task<JsonResult> ApplicationLogGridData(ApplicationLog filter)
        {
            filter.ClearEmptyModifiedColumns();

            PagedResult<ApplicationLog> pagedResult = await ApplicationLogBusinessManager.Current.PagingSelectAsync(SecurityManager.Current.ConnectionInfo, filter);

            foreach (ApplicationLog applicationLog in pagedResult.Results)
            {
                List<JSUser> jsUsers = await JSUserBusinessManager.Current.SelectAsync(SecurityManager.Current.ConnectionInfo, new JSUser()
                {
                    JSUserId = applicationLog.JSUserId
                });

                if (jsUsers.Any())
                {
                    applicationLog.JSUserName = jsUsers.First().Name;
                }

                applicationLog.MessageTypeText = EnumHelper.Current.GetNameWithSpaces((MessageTypes)applicationLog.MessageTypeId);

                applicationLog.ApplicationName = EnumHelper.Current.GetNameWithSpaces((Applications)applicationLog.ApplicationId);
            }

            return Json(pagedResult);
        }

        /// <summary>
        /// Details the modal.
        /// </summary>
        /// <param name="applicationLogId">The application log identifier.</param>
        /// <returns></returns>
        public async Task<ActionResult> DetailModal(int applicationLogId, string trackingGuid)
        {
            DetailModalViewModel model = new DetailModalViewModel();

            var applicationLog = ApplicationLogBusinessManager.Current.SelectAsync(SecurityManager.Current.ConnectionInfo, 
                new ApplicationLog() { ApplicationLogId = applicationLogId });

            var otherApplicationLogs = ApplicationLogBusinessManager.Current.SelectAsync(SecurityManager.Current.ConnectionInfo,
                new ApplicationLog() { TrackingGuid = trackingGuid });

            var trafficLogRequest = TrafficLogRequestBusinessManager.Current.SelectAsync(SecurityManager.Current.ConnectionInfo,
                new TrafficLogRequest() { TrackingGuid = trackingGuid });

            model.ApplicationLog = (await applicationLog).FirstOrDefault();

            List<JSUser> jsUsers = await JSUserBusinessManager.Current.SelectAsync(SecurityManager.Current.ConnectionInfo, new JSUser()
            {
                JSUserId = model.ApplicationLog.JSUserId
            });

            if (jsUsers.Any())
            {
                model.ApplicationLog.JSUserName = jsUsers.First().Name;
            }

            model.ApplicationLog.MessageTypeText = EnumHelper.Current.GetNameWithSpaces((MessageTypes)model.ApplicationLog.MessageTypeId);

            model.ApplicationLog.ApplicationName = EnumHelper.Current.GetNameWithSpaces((Applications)model.ApplicationLog.ApplicationId);

            model.AdditionalApplicationLogs = await otherApplicationLogs;

            model.AdditionalApplicationLogs.RemoveAll(al => al.ApplicationLogId == applicationLogId);

            model.TrafficLogRequest = (await trafficLogRequest).FirstOrDefault();

            return View(model);
        }

        /// <summary>
        /// Application Log Summary.
        /// </summary>
        /// <returns></returns>
        [CurrentMenuItem(SuiteMenuItems.ApplicationLogSummary)]
        public ActionResult ApplicationLogSummary()
        {
            ApplicationLogSummary model = new ApplicationLogSummary();

            model.InsertedOn_From = DateTime.Today;
            model.InsertedOn_To = DateTime.Today.AddDays(1);

            ViewBag.AvailableTraceLevels = EnumHelper.Current.GetSelectList<TraceLevels>();

            ViewBag.AvailableApplications = EnumHelper.Current.GetSelectList<Applications>();

            return View(model);
        }

        /// <summary>
        /// Applications the log grid data.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public async Task<JsonResult> ApplicationLogSummaryGridData(ApplicationLogSummary filter)
        {
            filter.ClearEmptyModifiedColumns();

            PagedResult<ApplicationLogSummary> pagedResult = await ApplicationLogBusinessManager.Current.ApplicationLogSummaryPagingSelectAsync(SecurityManager.Current.ConnectionInfo, filter);

            foreach (ApplicationLog applicationLog in pagedResult.Results)
            {
                applicationLog.MessageTypeText = EnumHelper.Current.GetNameWithSpaces((MessageTypes)applicationLog.MessageTypeId);
            }

            return Json(pagedResult);
        }
    }
}