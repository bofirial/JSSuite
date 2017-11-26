using JS.Core.Foundation.Data;
using JS.Suite.BusinessLogic.JSSupport.Generated;
using JS.Suite.DataAbstraction.JSSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JS.Suite.BusinessLogic.JSSupport
{
    /// <summary>
    /// TrafficLogRequest Business Manager
    /// </summary>
    public class TrafficLogRequestBusinessManager : TrafficLogRequestBusinessManager_Generated<TrafficLogRequestBusinessManager>
    {
        /// <summary>
        /// Pagings the select asynchronous.
        /// </summary>
        /// <param name="connectionInfo">The connection information.</param>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public async Task<PagedResult<TrafficLogRequest>> PagingSelectAsync(IConnectionInfo connectionInfo, TrafficLogRequest filter)
        {
            return await TrafficLogRequestManager.Current.PagingSelectAsync(connectionInfo, filter);
        }

        /// <summary>
        /// Traffics the log summary paging select asynchronous.
        /// </summary>
        /// <param name="connectionInfo">The connection information.</param>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public Task<PagedResult<TrafficLogSummary>> TrafficLogSummaryPagingSelectAsync(IConnectionInfo connectionInfo, TrafficLogSummary filter)
        {
            return TrafficLogRequestManager.Current.TrafficLogSummaryPagingSelectAsync(connectionInfo, filter);
        }
    }
}
