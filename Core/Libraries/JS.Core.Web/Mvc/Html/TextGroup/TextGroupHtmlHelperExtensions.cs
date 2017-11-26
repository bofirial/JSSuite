using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace JS.Core.Web.Mvc.Html
{
    /// <summary>
    /// Text Group Html Helper Extensions
    /// </summary>
    public static class TextGroupHtmlHelperExtensions
    {
        /// <summary>
        /// Text Group Control Html Helper Function
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <param name="helper">The helper.</param>
        /// <param name="textGroupFactory">The text group factory.</param>
        /// <returns></returns>
        public static DisposableControl<TModel> TextGroup<TModel>(this HtmlHelper<TModel> helper, Func<TextGroupBuilder<TModel>, TextGroupBuilder<TModel>> textGroupFactory)
        {
            TextGroupBuilder<TModel> textGroupBuilder = new TextGroupBuilder<TModel>(helper);

            textGroupFactory(textGroupBuilder);

            return textGroupBuilder.ToDisposableControl();
        }
    }
}
