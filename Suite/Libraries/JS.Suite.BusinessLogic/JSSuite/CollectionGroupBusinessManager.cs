using JS.Suite.BusinessLogic.JSSuite.Generated;
using JS.Suite.BusinessLogic.Security;
using JS.Suite.DataAbstraction.JSSuite;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Owin.Security;
using System.Web.Mvc;
using Owin;
using Microsoft.Owin.Security.DataProtection;
using JS.Core.Foundation.Helpers;
using JS.Suite.BusinessLogic.Security.DataProtectorToken;
using JS.Suite.BusinessLogic.Messaging.Email;
using JS.Core.Foundation.Data;

namespace JS.Suite.BusinessLogic.JSSuite
{
    /// <summary>
    /// CollectionGroup Business Manager
    /// </summary>
    public class CollectionGroupBusinessManager : CollectionGroupBusinessManager_Generated<CollectionGroupBusinessManager>
    {
        /// <summary>
        /// Pagings the select asynchronous.
        /// </summary>
        /// <param name="connectionInfo">The connection information.</param>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public Task<PagedResult<CollectionGroup>> PagingSelectAsync(IConnectionInfo connectionInfo, CollectionGroup filter)
        {
            return CollectionGroupManager.Current.PagingSelectAsync(connectionInfo, filter);
        }

        /// <summary>
        /// Inserts the asynchronous with collection group js user.
        /// </summary>
        /// <param name="connectionInfo">The connection information.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public async Task<IProcessResult> InsertAsyncWithCollectionGroupJSUser(IConnectionInfo connectionInfo, CollectionGroup model)
        {
            IProcessResult result = await InsertAsync(connectionInfo, model);

            if (result.IsSuccess())
            {
                result = await CollectionGroupJSUserBusinessManager.Current.InsertAsync(connectionInfo, new CollectionGroupJSUser()
                {
                    CollectionGroupId = model.CollectionGroupId,
                    JSUserId = connectionInfo.UserId
                });
            }

            return result;
        }
    }
}
