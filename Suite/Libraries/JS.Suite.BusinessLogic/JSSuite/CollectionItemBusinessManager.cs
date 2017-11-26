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
    /// CollectionItem Business Manager
    /// </summary>
    public class CollectionItemBusinessManager : CollectionItemBusinessManager_Generated<CollectionItemBusinessManager>
    {
        /// <summary>
        /// Pagings the select asynchronous.
        /// </summary>
        /// <param name="connectionInfo">The connection information.</param>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public Task<PagedResult<CollectionItem>> PagingSelectAsync(IConnectionInfo connectionInfo, CollectionItem filter)
        {
            return CollectionItemManager.Current.PagingSelectAsync(connectionInfo, filter);
        }
    }
}
