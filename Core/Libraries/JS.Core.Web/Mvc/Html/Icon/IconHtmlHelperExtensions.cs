using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace JS.Core.Web.Mvc.Html
{
    /// <summary>
    /// Icon Html Helper Extensions
    /// </summary>
    public static class IconHtmlHelperExtensions
    {
        /// <summary>
        /// Html Helper for Creating Icons
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <param name="helper">The helper.</param>
        /// <param name="icon">The icon.</param>
        /// <returns></returns>
        public static IconBuilder<TModel> Icon<TModel>(this HtmlHelper<TModel> helper, FAIcons icon)
        {
            return new IconBuilder<TModel>(helper, icon);
        }
    }
}
