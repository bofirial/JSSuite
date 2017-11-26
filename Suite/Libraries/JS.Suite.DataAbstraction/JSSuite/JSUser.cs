using JS.Core.Foundation.Data;
using JS.Suite.DataAbstraction.JSSuite.Generated;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JS.Suite.DataAbstraction.JSSuite
{
    /// <summary>
    /// Editable Class for the JSUser Table
    /// </summary>
    public class JSUser : JSUser_Generated, IUser<int>
    {
        /// <summary>
        /// Id
        /// </summary>
        [DBIgnore]
        public int Id
        {
            get { return JSUserId; }
        }

        /// <summary>
        /// User Name
        /// </summary>
        [DBIgnore]
        public string UserName
        {
            get
            {
                return Name;
            }
            set
            {
                Name = value;
            }
        }
    }
}
