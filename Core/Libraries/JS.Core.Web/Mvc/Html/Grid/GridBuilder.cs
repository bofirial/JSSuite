using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using JS.Core.Web.ExtensionMethods;
using System.Web.Routing;
using JS.Core.Foundation.BaseClasses.Interfaces;
using JS.Core.Foundation.Constants;
using JS.Core.Foundation.Data;
using JS.Core.Foundation.Helpers;
using System.Linq.Expressions;
using System.Data.SqlClient;

namespace JS.Core.Web.Mvc.Html
{
    /// <summary>
    /// Grid Builder
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <typeparam name="TProperty">The type of the property.</typeparam>
    public class GridBuilder<TModel, TProperty> : HtmlBuilder<TModel, GridBuilder<TModel, TProperty>>
            where TProperty : class, IEntity, IQueryContextContainer
    {
        private List<IGridColumnBuilder> gridColumns = null;

        private TProperty filter = null;

        private string defaultSortBy = null;

        private SortOrder defaultSortDirection = SortOrder.Ascending;

        /// <summary>
        /// Initializes a new instance of the <see cref="GridBuilder{TModel, TProperty}" /> class.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="filter">The filter.</param>
        public GridBuilder(HtmlHelper<TModel> helper, TProperty filter = null)
            : base(helper)
        {
            this.filter = filter;
        }

        /// <summary>
        /// Columnses the specified grid column factory function.
        /// </summary>
        /// <param name="gridColumnFactoryFunction">The grid column factory function.</param>
        /// <returns></returns>
        public GridBuilder<TModel, TProperty> Columns(Action<GridColumnFactory<TModel, TProperty>> gridColumnFactoryFunction)
        {
            GridColumnFactory<TModel, TProperty> gridColumnFactory = new GridColumnFactory<TModel,TProperty>(helper);

            gridColumnFactoryFunction(gridColumnFactory);
            
            gridColumns = gridColumnFactory.GridColumns;

            return this;
        }

        /// <summary>
        /// Defaults the sort.
        /// </summary>
        /// <typeparam name="TColumn">The type of the column.</typeparam>
        /// <param name="sortProperty">The sort property.</param>
        /// <param name="sortDirection">The sort direction.</param>
        /// <returns></returns>
        public GridBuilder<TModel, TProperty> DefaultSort<TColumn>(Expression<Func<TProperty, TColumn>> sortProperty, SortOrder sortDirection)
        {
            return DefaultSort(ExpressionHelper.GetExpressionText(sortProperty), sortDirection);
        }

        /// <summary>
        /// Defaults the sort.
        /// </summary>
        /// <param name="sortBy">The sort by.</param>
        /// <param name="sortDirection">The sort direction.</param>
        /// <returns></returns>
        public GridBuilder<TModel, TProperty> DefaultSort(string sortBy, SortOrder sortDirection)
        {
            this.defaultSortBy = sortBy;
            this.defaultSortDirection = sortDirection;

            return this;
        }

        /// <summary>
        /// Renders this instance.
        /// </summary>
        /// <returns></returns>
        protected override string Render()
        {
            TagBuilder wrapper = new TagBuilder("div");

            wrapper.AddCssClass("grid");

            WithDataAttribute("grid-initial-filter", GetInitialGridFilter());

            WithDataAttribute("default-sort-by", defaultSortBy);
            WithDataAttribute("default-sort-direction", defaultSortDirection);

            wrapper.MergeAttributesAppendClasses(htmlAttributes);

            TagBuilder grid = new TagBuilder("table");

            grid.AddCssClass("footable");
            grid.AddCssClass("toggle-arrow-tiny");
            grid.AddCssClass("table");

            TagBuilder tHead = new TagBuilder("thead");

            TagBuilder headRow = new TagBuilder("tr");

            StringBuilder thStringBuilder = new StringBuilder();

            foreach (IGridColumnBuilder gridColumn in gridColumns)
            {
                thStringBuilder.Append(gridColumn.ToHtmlString());
            }

            headRow.InnerHtml = thStringBuilder.ToString();

            tHead.InnerHtml = headRow.ToString();

            StringBuilder gridContentsStringBuilder = new StringBuilder();

            gridContentsStringBuilder.Append(tHead.ToString());

            TagBuilder tbody = new TagBuilder("tbody");

            gridContentsStringBuilder.Append(tbody.ToString());

            grid.InnerHtml = gridContentsStringBuilder.ToString();

            TagBuilder footer = new TagBuilder("div");

            footer.AddCssClass("footer");

            TagBuilder pager = new TagBuilder("ul");

            pager.AddCssClass("pagination");

            TagBuilder resultCount = new TagBuilder("span");

            resultCount.AddCssClass("resultCount");
            resultCount.AddCssClass("label");
            resultCount.AddCssClass("label-primary");

            resultCount.InnerHtml = "<span class=\"from\"></span> - <span class=\"to\"></span> of <span class=\"total\"></span> items";

            footer.InnerHtml = String.Format("<div>{0}</div><div>{1}</div>", pager.ToString(), resultCount.ToString());

            wrapper.InnerHtml = grid.ToString() + footer.ToString();

            return wrapper.ToString();
        }

        private string GetInitialGridFilter()
        {
            Dictionary<string, object> gridFilter = new Dictionary<string, object>();

            IEnumerable<string> modifiedColumns = filter.GetModifiedColumns();

            foreach (var columnName in modifiedColumns)
            {
                gridFilter.Add(columnName, ReflectionHelper.Current.GetPropertyValue(filter, columnName));
            }

            return SerializationHelper.Current.Serialize(gridFilter, SerializationTypes.Json);
        }
    }
}
