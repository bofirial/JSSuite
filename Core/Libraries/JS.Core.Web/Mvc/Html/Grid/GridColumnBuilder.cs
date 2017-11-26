using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using JS.Core.Web.ExtensionMethods;

namespace JS.Core.Web.Mvc.Html
{
    /// <summary>
    /// Grid Column Builder
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <typeparam name="TProperty">The type of the property.</typeparam>
    /// <typeparam name="TColumn">The type of the column.</typeparam>
    public class GridColumnBuilder<TModel, TProperty, TColumn> : HtmlBuilder<TModel, GridColumnBuilder<TModel, TProperty, TColumn>>, IGridColumnBuilder
    {
        private Expression<Func<TProperty, TColumn>> columnExpression;

        private string overrideLabelText = null;

        private GridColumnCollapseLevels gridColumnCollapseLevel = GridColumnCollapseLevels.None;

        private bool hideInDetailView = false;

        private string detailViewName = null;

        private bool isSortable = true;

        private string sortName = null;

        private string gridColumnTemplate = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="GridColumnBuilder{TModel, TProperty, TColumn}" /> class.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="columnExpression">The column expression.</param>
        public GridColumnBuilder(HtmlHelper<TModel> helper, Expression<Func<TProperty, TColumn>> columnExpression) : base(helper)
        {
            this.columnExpression = columnExpression;
        }

        /// <summary>
        /// Labels the specified column label.
        /// </summary>
        /// <param name="columnLabel">The column label.</param>
        /// <returns></returns>
        public GridColumnBuilder<TModel, TProperty, TColumn> Label(string columnLabel)
        {
            overrideLabelText = columnLabel;

            return this;
        }

        /// <summary>
        /// Collapses the level.
        /// </summary>
        /// <param name="gridColumnCollapseLevel">The grid column collapse level.</param>
        /// <returns></returns>
        public GridColumnBuilder<TModel, TProperty, TColumn> CollapseLevel(GridColumnCollapseLevels gridColumnCollapseLevel)
        {
            this.gridColumnCollapseLevel = gridColumnCollapseLevel;

            return this;
        }

        /// <summary>
        /// Details the view hide.
        /// </summary>
        /// <param name="hideInDetailView">if set to <c>true</c> [hide in detail view].</param>
        /// <returns></returns>
        public GridColumnBuilder<TModel, TProperty, TColumn> HideInDetailView(bool hideInDetailView = true)
        {
            this.hideInDetailView = hideInDetailView;

            return this;
        }

        /// <summary>
        /// Details the name of the view.
        /// </summary>
        /// <param name="detailViewName">Name of the detail view.</param>
        /// <returns></returns>
        public GridColumnBuilder<TModel, TProperty, TColumn> DetailViewName(string detailViewName)
        {
            this.detailViewName = detailViewName;

            return this;
        }

        /// <summary>
        /// Withes the column class.
        /// </summary>
        /// <param name="columnClass">The column class.</param>
        /// <returns></returns>
        public GridColumnBuilder<TModel, TProperty, TColumn> WithColumnClass(string columnClass)
        {
            const string columnClassKey = "data-class";

            if (htmlAttributes.ContainsKey(columnClassKey))
            {
                columnClass = String.Join(" ", htmlAttributes["data-class"], columnClass);

                htmlAttributes.Remove(columnClassKey);
            }

            WithDataAttribute(columnClassKey, columnClass);

            return this;
        }

        /// <summary>
        /// Withes the name of the sort.
        /// </summary>
        /// <param name="sortName">Name of the sort.</param>
        /// <returns></returns>
        public GridColumnBuilder<TModel, TProperty, TColumn> WithSortName(string sortName)
        {
            this.sortName = sortName;

            return this;
        }

        /// <summary>
        /// Sortables the specified is sortable.
        /// </summary>
        /// <param name="isSortable">if set to <c>true</c> [is sortable].</param>
        /// <returns></returns>
        public GridColumnBuilder<TModel, TProperty, TColumn> Sortable(bool isSortable = true)
        {
            this.isSortable = isSortable;

            return this;
        }

        /// <summary>
        /// Withes the template.
        /// </summary>
        /// <param name="gridColumnTemplateFunction">The grid column template function.</param>
        /// <returns></returns>
        public GridColumnBuilder<TModel, TProperty, TColumn> WithTemplate(Func<GridColumnTemplateBuilder<TProperty>, string> gridColumnTemplateFunction)
        {
            this.gridColumnTemplate = gridColumnTemplateFunction(new GridColumnTemplateBuilder<TProperty>());

            return this;
        }

        /// <summary>
        /// Gets the column label.
        /// </summary>
        /// <returns></returns>
        private string GetColumnLabel()
        {
            if (overrideLabelText != null)
            {
                return overrideLabelText;
            }

            return ExpressionHelper.GetExpressionText(columnExpression);
        }

        /// <summary>
        /// Renders this instance.
        /// </summary>
        /// <returns></returns>
        protected override string Render()
        {
            TagBuilder columnHeader = new TagBuilder("th");

            columnHeader.InnerHtml = GetColumnLabel();

            if (isSortable)
            {
                columnHeader.InnerHtml = columnHeader.InnerHtml + "<span class=\"footable-sort-indicator\"></span>";

                columnHeader.AddCssClass("footable-sortable");

                if (String.IsNullOrEmpty(sortName))
                {
                    sortName = ExpressionHelper.GetExpressionText(columnExpression);
                }

                WithDataAttribute("sort-name", sortName);
            }

            WithDataAttribute("property-name", ExpressionHelper.GetExpressionText(columnExpression));

            SetCollapseLevelDataAttribute();

            if (hideInDetailView)
            {
                WithDataAttribute("ignore", "true");
            }

            if (!String.IsNullOrEmpty(gridColumnTemplate))
            {
                WithDataAttribute("column-template", gridColumnTemplate);
            }

            if (!String.IsNullOrEmpty(detailViewName))
            {
                WithDataAttribute("name", detailViewName);
            }

            columnHeader.MergeAttributesAppendClasses(htmlAttributes);

            return columnHeader.ToString();
        }

        private void SetCollapseLevelDataAttribute()
        {
            string hideValue = String.Empty;

            switch (gridColumnCollapseLevel)
            {
                case GridColumnCollapseLevels.Phone:
                    hideValue = "phone";
                    break;
                case GridColumnCollapseLevels.Tablet:
                    hideValue = "phone,tablet";
                    break;
                case GridColumnCollapseLevels.All:
                    hideValue = "all";
                    break;
                case GridColumnCollapseLevels.None:
                default:
                    return;
            }

            WithDataAttribute("hide", hideValue);
        }
    }
}
