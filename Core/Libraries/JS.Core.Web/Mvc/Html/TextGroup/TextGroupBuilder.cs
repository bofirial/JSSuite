using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace JS.Core.Web.Mvc.Html
{
    /// <summary>
    /// Text Group Builder
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    public class TextGroupBuilder<TModel> : BaseGroupBuilder<TModel, TextGroupBuilder<TModel>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TextGroupBuilder{TModel}"/> class.
        /// </summary>
        /// <param name="helper">The helper.</param>
        public TextGroupBuilder(HtmlHelper<TModel> helper)
            : base(helper)
        {
        }

        /// <summary>
        /// Renders the start tag.
        /// </summary>
        /// <returns></returns>
        protected override string RenderStartTag()
        {
            tagBuilder.AddCssClass("textGroup");

            return base.RenderStartTag();
        }
    }
}
