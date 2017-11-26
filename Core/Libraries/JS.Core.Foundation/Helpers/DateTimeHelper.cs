using JS.Core.Foundation.BaseClasses;
using JS.Core.Foundation.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JS.Core.Foundation.Helpers
{
    /// <summary>
    /// Date Helper
    /// </summary>
    public class DateTimeHelper : SingletonBase<DateTimeHelper>
    {
        /// <summary>
        /// Gets the local now.
        /// </summary>
        /// <param name="connectionInfo">The connection information.</param>
        /// <returns></returns>
        public DateTime GetLocalNow(IConnectionInfo connectionInfo)
        {
            return ConvertUtcToLocal(connectionInfo, DateTime.UtcNow);
        }

        /// <summary>
        /// Converts the UTC to local.
        /// </summary>
        /// <param name="connectionInfo">The connection information.</param>
        /// <param name="utcDateTime">The UTC date time.</param>
        /// <returns></returns>
        public DateTime ConvertUtcToLocal(IConnectionInfo connectionInfo, DateTime utcDateTime)
        {
            TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(connectionInfo.TimeZone);

            DateTime dateTime = DateTime.SpecifyKind(utcDateTime, DateTimeKind.Utc);

            return TimeZoneInfo.ConvertTimeFromUtc(dateTime, timeZoneInfo);
        }

        /// <summary>
        /// Converts the local to UTC.
        /// </summary>
        /// <param name="connectionInfo">The connection information.</param>
        /// <param name="localDateTime">The local date time.</param>
        /// <returns></returns>
        public DateTime ConvertLocalToUtc(IConnectionInfo connectionInfo, DateTime localDateTime)
        {
            TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(connectionInfo.TimeZone);
            
            DateTime dateTime = DateTime.SpecifyKind(localDateTime, DateTimeKind.Unspecified);

            return TimeZoneInfo.ConvertTimeToUtc(dateTime, timeZoneInfo);
        }
    }
}
