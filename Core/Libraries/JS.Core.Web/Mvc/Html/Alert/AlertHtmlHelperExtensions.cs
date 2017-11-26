using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace JS.Core.Web.Mvc.Html
{
    /// <summary>
    /// Alert Html Helper Extensions
    /// </summary>
    public static class AlertHtmlHelperExtensions
    {
        /// <summary>
        /// Html Helper for Creating Alerts
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <param name="helper">The helper.</param>
        /// <param name="alertText">The alert text.</param>
        /// <param name="alertType">Type of the alert.</param>
        /// <returns></returns>
        public static AlertBuilder<TModel> Alert<TModel>(this HtmlHelper<TModel> helper, string alertText, AlertTypes alertType = AlertTypes.Info)
        {
            return new AlertBuilder<TModel>(helper, alertText, alertType);
        }
    }
}
