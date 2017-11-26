using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace JS.Core.Web.Mvc.Html
{
    /// <summary>
    /// Button Group Builder
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    public class ButtonGroupBuilder<TModel> : BaseGroupBuilder<TModel, ButtonGroupBuilder<TModel>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ButtonGroupBuilder{TModel}"/> class.
        /// </summary>
        /// <param name="helper">The helper.</param>
        public ButtonGroupBuilder(HtmlHelper<TModel> helper) : base(helper)
        {
        }

        /// <summary>
        /// Renders the start tag.
        /// </summary>
        /// <returns></returns>
        protected override string RenderStartTag()
        {
            tagBuilder.AddCssClass("buttonGroup");

            return base.RenderStartTag();
        }
    }
}
