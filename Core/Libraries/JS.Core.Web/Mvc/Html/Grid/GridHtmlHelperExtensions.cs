using JS.Core.Foundation.BaseClasses.Interfaces;
using JS.Core.Foundation.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace JS.Core.Web.Mvc.Html
{
    /// <summary>
    /// Grid Html Helper Extensions
    /// </summary>
    public static class GridHtmlHelperExtensions
    {
        /// <summary>
        /// Grids the specified helper.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="helper">The helper.</param>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public static GridBuilder<TModel, TProperty> Grid<TModel, TProperty>(this HtmlHelper<TModel> helper, TProperty filter = null)
            where TProperty : class, IEntity, IQueryContextContainer
        {
            return new GridBuilder<TModel, TProperty>(helper, filter);
        }

        ///// <summary>
        ///// Fields for base grid filter.
        ///// </summary>
        ///// <typeparam name="TModel">The type of the model.</typeparam>
        ///// <typeparam name="TFilter">The type of the filter.</typeparam>
        ///// <param name="helper">The helper.</param>
        ///// <param name="modelExpression">The model expression.</param>
        ///// <returns></returns>
        //public static IHtmlString FieldForBaseGridFilter<TModel, TFilter>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TFilter>> modelExpression)
        //    where TFilter : class, IEntity, IQueryContextContainer
        //{
        //    StringBuilder sb = new StringBuilder();

        //    sb.Append(helper.FieldFor(m => modelExpression.Compile()(m).QueryContext.PageNumber).AsHidden().ToHtmlString());
        //    sb.Append(helper.FieldFor(m => modelExpression.Compile()(m).QueryContext.PageSize).AsHidden().ToHtmlString());
        //    sb.Append(helper.FieldFor(m => modelExpression.Compile()(m).QueryContext.SortBy).AsHidden().ToHtmlString());
        //    sb.Append(helper.FieldFor(m => modelExpression.Compile()(m).QueryContext.SortDirection).AsHidden().ToHtmlString());

        //    return new HtmlString(sb.ToString());
        //}
    }
}
