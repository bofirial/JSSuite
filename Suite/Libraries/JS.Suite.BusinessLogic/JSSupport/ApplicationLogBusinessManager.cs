using JS.Core.Foundation.Constants;
using JS.Core.Foundation.Data;
using JS.Core.Foundation.Helpers;
using JS.Core.Web.Mvc.Html;
using JS.Suite.BusinessLogic.Helpers;
using JS.Suite.BusinessLogic.JSSupport.Generated;
using JS.Suite.BusinessLogic.Security;
using JS.Suite.DataAbstraction.JSSupport;
using JS.Suite.Foundation.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JS.Suite.BusinessLogic.JSSupport
{
    /// <summary>
    /// ApplicationLog Business Manager
    /// </summary>
    public class ApplicationLogBusinessManager : ApplicationLogBusinessManager_Generated<ApplicationLogBusinessManager>
    {
        /// <summary>
        /// Pagings the select asynchronous.
        /// </summary>
        /// <param name="connectionInfo">The connection information.</param>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public async Task<PagedResult<ApplicationLog>> PagingSelectAsync(IConnectionInfo connectionInfo, ApplicationLog filter)
        {
            return await ApplicationLogManager.Current.PagingSelectAsync(connectionInfo, filter);
        }

        /// <summary>
        /// Applications the log summary paging select asynchronous.
        /// </summary>
        /// <param name="connectionInfo">The connection information.</param>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public async Task<PagedResult<ApplicationLogSummary>> ApplicationLogSummaryPagingSelectAsync(IConnectionInfo connectionInfo, ApplicationLogSummary filter)
        {
            return await ApplicationLogManager.Current.ApplicationLogSummaryPagingSelectAsync(connectionInfo, filter);
        }

        /// <summary>
        /// Logs the specified trace levels.
        /// </summary>
        /// <param name="traceLevel">The trace levels.</param>
        /// <param name="message">The message.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="messageType">Type of the message.</param>
        /// <param name="stackTrace">The stack trace.</param>
        /// <returns></returns>
        public IProcessResult Log(TraceLevels traceLevel, string message, string subject, MessageTypes messageType, string stackTrace)
        {
            ApplicationLog applicationLog = new ApplicationLog()
                {
                    JSUserId = SecurityManager.Current.UserId,
                    Message = message,
                    Subject = subject,
                    MessageTypeId = (int)messageType,
                    TrackingGuid = SecurityManager.Current.TrackingGuid,
                    ApplicationId = (int)SecurityManager.Current.CurrentApplication,
                    StackTrace = stackTrace,
                    TraceLevelId = (int)traceLevel
                };

            BackgroundProcessHelper.Current.Trigger(async (cancelationToken) =>
            {
                await InsertAsync(new ConnectionInfo(SystemUsers.Admin), applicationLog);
            });

            return new ProcessResult(ResultCodes.Success);
        }
    }
}
