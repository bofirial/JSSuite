using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace JS.Core.Web.Mvc.Html
{
    /// <summary>
    /// Button Group Html Helper Extensions
    /// </summary>
    public static class ButtonGroupHtmlHelperExtensions
    {
        /// <summary>
        /// Button Group Html Helper
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <param name="helper">The helper.</param>
        /// <param name="buttonGroupFactory">The button group factory.</param>
        /// <returns></returns>
        public static DisposableControl<TModel> ButtonGroup<TModel>(this HtmlHelper<TModel> helper, Func<ButtonGroupBuilder<TModel>, ButtonGroupBuilder<TModel>> buttonGroupFactory)
        {
            ButtonGroupBuilder<TModel> buttonGroupBuilder = new ButtonGroupBuilder<TModel>(helper);

            buttonGroupFactory(buttonGroupBuilder);

            return buttonGroupBuilder.ToDisposableControl();
        }
    }
}
