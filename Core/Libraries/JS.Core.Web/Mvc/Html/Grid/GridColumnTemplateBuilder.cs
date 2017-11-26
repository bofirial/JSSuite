using JS.Core.Foundation.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JS.Core.Web.Mvc.Html
{
    /// <summary>
    /// Grid Column Template Builder
    /// </summary>
    /// <typeparam name="TGrid">The type of the grid.</typeparam>
    public class GridColumnTemplateBuilder<TGrid>
    {
        /// <summary>
        /// Adds a Property to the Grid Template
        /// </summary>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="modelExpression">The model expression.</param>
        /// <returns></returns>
        public string Property<TProperty>(Expression<Func<TGrid, TProperty>> modelExpression)
        {
            return String.Format("-~{0}~-", ((MemberExpression)modelExpression.Body).Member.Name);
        }

        /// <summary>
        /// Filters the specified model expression.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="modelExpression">The model expression.</param>
        /// <returns></returns>
        public string Filter<TProperty>(Expression<Func<TGrid, TProperty>> modelExpression)
        {
            return String.Format(".~{0}~.", ((MemberExpression)modelExpression.Body).Member.Name);
        }
    }
}
