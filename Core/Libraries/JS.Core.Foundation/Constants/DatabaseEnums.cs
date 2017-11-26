using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JS.Core.Foundation.Constants
{
    /// <summary>
    /// System Users
    /// </summary>
    public enum SystemUsers
    {
        /// <summary> Admin </summary>
        Admin = 1,
        /// <summary> Unit Test User </summary>
        UnitTestUser = 2,
        /// <summary> Unknown Web User </summary>
        UnknownWebUser = 3,
        /// <summary> Unknown System User </summary>
        UnknownSystemUser = 4
    }

    /// <summary>
    /// Trace Levels
    /// </summary>
    public enum TraceLevels
    {
        /// <summary> Fatal </summary>
        Fatal = 1,
        /// <summary> Error </summary>
        Error = 2,
        /// <summary> Warning </summary>
        Warning = 3,
        /// <summary> Information </summary>
        Information = 4,
        /// <summary> Debug </summary>
        Debug = 5
    }
}
