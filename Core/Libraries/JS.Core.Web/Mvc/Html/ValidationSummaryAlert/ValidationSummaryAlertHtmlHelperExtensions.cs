using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace JS.Core.Web.Mvc.Html
{
    /// <summary>
    /// Validation Summary Alert Html Extensions
    /// </summary>
    public static class ValidationSummaryAlertHtmlHelperExtensions
    {
        /// <summary>
        /// Html Helper for Rendering a Validation Summary Alert Control
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <param name="helper">The helper.</param>
        /// <returns></returns>
        public static ValidationSummaryAlertBuilder<TModel> ValidationSummaryAlert<TModel>(this HtmlHelper<TModel> helper)
        {
            return new ValidationSummaryAlertBuilder<TModel>(helper);
        }
    }
}
