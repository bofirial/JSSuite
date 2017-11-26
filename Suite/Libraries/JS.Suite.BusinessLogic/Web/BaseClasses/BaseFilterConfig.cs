using JS.Suite.BusinessLogic.Web.Mvc.ActionFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace JS.Suite.BusinessLogic.Web.BaseClasses
{
    /// <summary>
    /// Base Filter Config
    /// </summary>
    public class BaseFilterConfig
    {
        /// <summary>
        /// Registers the global filters.
        /// </summary>
        /// <param name="filters">The filters.</param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new TrafficLogAttribute()
            {
                Order = 20
            });
            filters.Add(new PopulateSecurityContextAttribute()
            {
                Order = 10
            });
            filters.Add(new LogErrorsAttribute()
            {
                Order = 30
            });
            filters.Add(new HandleErrorAttribute()
            {
                Order = 50,
                View = "Common/Errors/Error"
            });
        }
    }
}
