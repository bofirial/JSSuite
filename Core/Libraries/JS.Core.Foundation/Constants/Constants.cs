using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JS.Core.Foundation.Constants
{
    /// <summary>
    /// Default Values
    /// </summary>
    public static class Defaults
    {
        /// <summary>
        /// Time Zone
        /// </summary>
        public static string TimeZone = TimeZoneInfo.Local.Id;
        /// <summary>
        /// Default Locale
        /// </summary>
        public const string Locale = "en-US";
    }

    /// <summary>
    /// JS Claim Types
    /// </summary>
    public static class JSClaimTypes
    {
        /// <summary>
        /// User Type
        /// </summary>
        public const string UserType = "UserType";
        /// <summary>
        /// Application
        /// </summary>
        public const string Application = "Application";
    }
}
