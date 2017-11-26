using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace JS.Core.Web.Mvc.Html
{
    /// <summary>
    /// Label Builder
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <typeparam name="TProperty">The type of the property.</typeparam>
    public class LabelBuilder<TModel, TProperty> : ModelExpressionHtmlBuilder<TModel, TProperty, LabelBuilder<TModel, TProperty>>
    {
        /// <summary>
        /// The override text
        /// </summary>
        protected string overrideText = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="LabelBuilder{TModel, TProperty}"/> class.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="modelExpression">The model expression.</param>
        public LabelBuilder(HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> modelExpression)
            : base(helper, modelExpression)
        {
        }

        /// <summary>
        /// Overrides the Model Metadata Label Text with the Provided Text
        /// </summary>
        /// <param name="labelText">The label text.</param>
        /// <returns></returns>
        public LabelBuilder<TModel, TProperty> OverrideText(string labelText)
        {
            overrideText = labelText;

            return this;
        }

        /// <summary>
        /// Renders this instance.
        /// </summary>
        /// <returns></returns>
        protected override string Render()
        {
            htmlAttributes.AddClass("control-label");

            return helper.LabelFor(modelExpression, overrideText, htmlAttributes).ToString();
        }
    }
}
