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
    /// Grid Column Factory
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <typeparam name="TProperty">The type of the property.</typeparam>
    public class GridColumnFactory<TModel, TProperty>
    {
        private HtmlHelper<TModel> helper;

        /// <summary>
        /// Initializes a new instance of the <see cref="GridColumnFactory{TModel, TProperty}"/> class.
        /// </summary>
        /// <param name="helper">The helper.</param>
        public GridColumnFactory(HtmlHelper<TModel> helper)
        {
            this.helper = helper;

            GridColumns = new List<IGridColumnBuilder>();
        }

        /// <summary>
        /// Gets or sets the grid columns.
        /// </summary>
        /// <value>
        /// The grid columns.
        /// </value>
        internal List<IGridColumnBuilder> GridColumns { get; set; }

        /// <summary>
        /// Adds the specified column expression.
        /// </summary>
        /// <typeparam name="TColumn">The type of the column.</typeparam>
        /// <param name="columnExpression">The column expression.</param>
        /// <returns></returns>
        public GridColumnBuilder<TModel, TProperty, TColumn> Add<TColumn>(Expression<Func<TProperty, TColumn>> columnExpression)
        {
            GridColumnBuilder<TModel, TProperty, TColumn> gridColumnBuilder = new GridColumnBuilder<TModel, TProperty, TColumn>(helper, columnExpression);

            GridColumns.Add(gridColumnBuilder);

            return gridColumnBuilder;
        }
    }
}
