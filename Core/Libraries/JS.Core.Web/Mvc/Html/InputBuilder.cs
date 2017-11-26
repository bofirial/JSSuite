using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using JS.Core.Web.Mvc.Html.Interface;

namespace JS.Core.Web.Mvc.Html
{
    /// <summary>
    /// Html Builder Base Class for all Html Helpers that Create HTML
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <typeparam name="TProperty">The type of the property.</typeparam>
    /// <typeparam name="TBuilder">The type of the builder.</typeparam>
    public abstract class InputBuilder<TModel, TProperty, TBuilder> : ModelExpressionHtmlBuilder<TModel, TProperty, TBuilder>, IDisableableBuilder<TModel, TBuilder>
        where TBuilder : InputBuilder<TModel, TProperty, TBuilder>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InputBuilder{TModel, TProperty, TBuilder}"/> class.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="modelExpression">The model expression.</param>
        public InputBuilder(HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> modelExpression) : base(helper, modelExpression)
        {

        }

        /// <summary>
        /// Sets the Builder to Disabled
        /// </summary>
        /// <param name="isDisabled">if set to <c>true</c> [is disabled].</param>
        /// <returns></returns>
        public new TBuilder AsDisabled(bool isDisabled = true)
        {
            return base.AsDisabled(isDisabled);
        }

        /// <summary>
        /// Renders the Button as Disabled if a Condition Is Met
        /// </summary>
        /// <param name="modelExpression">The model expression.</param>
        /// <param name="targetValue">The target value.</param>
        /// <param name="conditionType">Type of the comparison.</param>
        /// <returns></returns>
        public new TBuilder AsDisabledIf<TTarget>(Expression<Func<TModel, TTarget>> modelExpression,
            object targetValue,
            JSConditionTypes conditionType = JSConditionTypes.IsEqualToValue)
        {
            return base.AsDisabledIf(modelExpression, targetValue, conditionType);
        }
    }
}
