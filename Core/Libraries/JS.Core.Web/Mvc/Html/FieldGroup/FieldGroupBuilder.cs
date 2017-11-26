using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace JS.Core.Web.Mvc.Html
{
    /// <summary>
    /// Field Group Builder
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    public class FieldGroupBuilder<TModel> : BaseGroupBuilder<TModel, FieldGroupBuilder<TModel>>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="FieldGroupBuilder{TModel}"/> class.
        /// </summary>
        /// <param name="helper">The helper.</param>
        public FieldGroupBuilder(HtmlHelper<TModel> helper) : base(helper)
        {
        }

        /// <summary>
        /// Renders the start tag.
        /// </summary>
        /// <returns></returns>
        protected override string RenderStartTag()
        {
            tagBuilder.AddCssClass("fieldGroup");

            return base.RenderStartTag();
        }
    }
}
