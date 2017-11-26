using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace JS.Core.Web.Mvc.Html
{
    /// <summary>
    /// Button Html Helper Extensions
    /// </summary>
    public static class ButtonHtmlHelperExtensions
    {
        /// <summary>
        /// Html Helper Extension for Creating an Html Button
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <param name="helper">The helper.</param>
        /// <param name="buttonText">The button text.</param>
        /// <returns></returns>
        public static WrappedButtonBuilder<TModel> Button<TModel>(this HtmlHelper<TModel> helper, string buttonText)
        {
            return new WrappedButtonBuilder<TModel>(helper, buttonText);
        }
    }
}
