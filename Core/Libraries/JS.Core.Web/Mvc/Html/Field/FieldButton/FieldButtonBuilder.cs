using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace JS.Core.Web.Mvc.Html
{
    /// <summary>
    /// Field Button Builder
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    public class FieldButtonBuilder<TModel> : BaseButtonBuilder<TModel, FieldButtonBuilder<TModel>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FieldButtonBuilder{TModel}"/> class.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="buttonText">The button text.</param>
        public FieldButtonBuilder(HtmlHelper<TModel> helper, string buttonText)
            : base(helper, buttonText)
        {

        }
    }
}
