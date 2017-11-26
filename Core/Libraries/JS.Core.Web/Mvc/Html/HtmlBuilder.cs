using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace JS.Core.Web.Mvc.Html
{
    /// <summary>
    /// Html Builder Base Class for all Html Helpers that Create HTML
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <typeparam name="TBuilder">The type of the builder.</typeparam>
    public abstract class HtmlBuilder<TModel, TBuilder> : BaseHtmlBuilder<TModel, TBuilder>
         where TBuilder : HtmlBuilder<TModel, TBuilder>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlBuilder{TModel, TBuilder}"/> class.
        /// </summary>
        /// <param name="helper">The helper.</param>
        public HtmlBuilder(HtmlHelper<TModel> helper) : base(helper)
        {

        }

        /// <summary>
        /// To the HTML string.
        /// </summary>
        /// <returns></returns>
        public sealed override string ToHtmlString()
        {
            BeforeRender();

            return Render();
        }
    }
}
