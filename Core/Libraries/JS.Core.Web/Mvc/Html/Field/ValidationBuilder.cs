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
    /// Validation Builder
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <typeparam name="TProperty">The type of the property.</typeparam>
    public class ValidationBuilder<TModel, TProperty> : ModelExpressionHtmlBuilder<TModel, TProperty, ValidationBuilder<TModel, TProperty>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationBuilder{TModel, TProperty}"/> class.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="modelExpression">The model expression.</param>
        public ValidationBuilder(HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> modelExpression)
            : base(helper, modelExpression)
        {

        }

        /// <summary>
        /// Renders this instance.
        /// </summary>
        /// <returns></returns>
        protected override string Render()
        {
            htmlAttributes.AddClass("help-block");

            return helper.ValidationMessageFor(modelExpression, null, htmlAttributes).ToString();
        }
    }
}
