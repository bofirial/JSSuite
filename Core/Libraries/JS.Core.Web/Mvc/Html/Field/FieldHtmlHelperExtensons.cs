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
    /// Field Html Helper Extensions
    /// </summary>
    public static class FieldHtmlHelperExtensons
    {
        /// <summary>
        /// Creates a Field
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="helper">The helper.</param>
        /// <param name="modelExpression">The model expression.</param>
        public static FieldBuilder<TModel, TProperty> FieldFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> modelExpression)
        {
            return new FieldBuilder<TModel, TProperty>(helper, modelExpression);
        }
    }
}
