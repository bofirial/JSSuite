using JS.Suite.BusinessLogic.Web.BaseClasses;
using JS.Suite.BusinessLogic.Web.Mvc;
using System.Web;
using System.Web.Mvc;

namespace JS.Suite.Hub
{
    /// <summary>
    /// Filter Config
    /// </summary>
    public class FilterConfig : BaseFilterConfig
    {
        /// <summary>
        /// Registers the global filters.
        /// </summary>
        /// <param name="filters">The filters.</param>
        public new static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            BaseFilterConfig.RegisterGlobalFilters(filters);

            filters.Add(new AuthorizeAttribute());
        }
    }
}
