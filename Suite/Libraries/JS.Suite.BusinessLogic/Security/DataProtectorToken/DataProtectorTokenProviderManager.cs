using JS.Core.Foundation.BaseClasses;
using Microsoft.Owin.Security.DataProtection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JS.Suite.BusinessLogic.Security.DataProtectorToken
{
    /// <summary>
    /// Data Protector Token Manager
    /// </summary>
    public class DataProtectorTokenManager : SingletonBase<DataProtectorTokenManager>
    {
        private IDataProtectionProvider _Provider;

        /// <summary>
        /// Gets or sets the provider.
        /// </summary>
        /// <value>
        /// The provider.
        /// </value>
        public IDataProtectionProvider Provider
        {
            get { return _Provider; }
            set { _Provider = value; }
        }

    }
}
