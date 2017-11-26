using JS.Core.Foundation.BaseClasses;
using JS.Core.Foundation.Constants;
using JS.Core.Foundation.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JS.Core.Foundation.Helpers
{
    /// <summary>
    /// Unit Test Helper
    /// </summary>
    public class UnitTestHelper : SingletonBase<UnitTestHelper>
    {
        /// <summary>
        /// Gets the test connection information.
        /// </summary>
        /// <returns></returns>
        public IConnectionInfo GetTestConnectionInfo()
        {
            ConnectionInfo connectionInfo = new ConnectionInfo();

            connectionInfo.UserId = (int)SystemUsers.UnitTestUser;

            return connectionInfo;
        }
    }
}
