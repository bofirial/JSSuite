using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace JS.Core.Web.Mvc.Html
{
    /// <summary>
    /// Model Expression Html Builder
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <typeparam name="TProperty">The type of the property.</typeparam>
    /// <typeparam name="TBuilder">The type of the builder.</typeparam>
    public abstract class ModelExpressionHtmlBuilder<TModel, TProperty, TBuilder> : HtmlBuilder<TModel, TBuilder>
        where TBuilder : ModelExpressionHtmlBuilder<TModel, TProperty, TBuilder>
    {
        /// <summary>
        /// The model expression
        /// </summary>
        protected Expression<Func<TModel, TProperty>> modelExpression;

        /// <summary>
        /// Initializes a new instance of the <see cref="InputBuilder{TModel, TProperty, TBuilder}"/> class.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="modelExpression">The model expression.</param>
        public ModelExpressionHtmlBuilder(HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> modelExpression) : base(helper)
        {
            this.modelExpression = modelExpression;
        }
    }
}
