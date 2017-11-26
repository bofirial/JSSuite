using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using JS.Core.Foundation.Helpers;

namespace JS.Core.Foundation.ExtensionMethods
{
    /// <summary>
    /// Identity Extensions
    /// </summary>
    public static class IdentityExtensions
    {
        /// <summary>
        /// Gets the User Id and converts it to Type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="identity">The identity.</param>
        /// <returns></returns>
        public static T GetUserId<T>(this IIdentity identity) where T : IConvertible
        {
            return TypeHelper.Current.Convert<T>(identity.GetUserId());
        }
    }
}
