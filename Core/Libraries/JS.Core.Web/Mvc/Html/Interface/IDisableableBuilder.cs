using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JS.Core.Web.Mvc.Html.Interface
{
    /// <summary>
    /// Disableable Builder Interface
    /// </summary>
    public interface IDisableableBuilder<TModel, TBuilder>
        where TBuilder : BaseHtmlBuilder<TModel, TBuilder>
    {
        /// <summary>
        /// Sets the Builder to Disabled
        /// </summary>
        /// <param name="isDisabled">if set to <c>true</c> [is disabled].</param>
        /// <returns></returns>
        TBuilder AsDisabled(bool isDisabled = true);
    }
}
