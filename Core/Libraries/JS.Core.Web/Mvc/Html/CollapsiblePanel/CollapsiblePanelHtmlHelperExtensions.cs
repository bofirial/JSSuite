using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace JS.Core.Web.Mvc.Html
{
    /// <summary>
    /// Collapsible Panel Html Helper Extensions
    /// </summary>
    public static class CollapsiblePanelHtmlHelperExtensions
    {
        /// <summary>
        /// Collapsibles the panel.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <param name="helper">The helper.</param>
        /// <param name="collapsiblePanelText">The collapsible panel text.</param>
        /// <param name="collapsiblePanelFactory">The collapsible panel factory.</param>
        /// <returns></returns>
        public static DisposableControl<TModel> CollapsiblePanel<TModel>(this HtmlHelper<TModel> helper, string collapsiblePanelText, Func<CollapsiblePanelBuilder<TModel>, CollapsiblePanelBuilder<TModel>> collapsiblePanelFactory)
        {
            CollapsiblePanelBuilder<TModel> collapsiblePanelBuilder = new CollapsiblePanelBuilder<TModel>(helper, collapsiblePanelText);

            collapsiblePanelFactory(collapsiblePanelBuilder);

            return collapsiblePanelBuilder.ToDisposableControl();
        }
    }
}
