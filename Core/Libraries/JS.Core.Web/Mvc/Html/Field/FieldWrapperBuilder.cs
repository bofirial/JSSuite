using JS.Core.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using JS.Core.Web.ExtensionMethods;
using JS.Core.Web.Mvc.Html.Interface;

namespace JS.Core.Web.Mvc.Html
{
    /// <summary>
    /// Field Wrapper Builder
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <typeparam name="TProperty">The type of the property.</typeparam>
    public class FieldWrapperBuilder<TModel, TProperty> : HtmlWrapperBuilder<TModel, FieldWrapperBuilder<TModel, TProperty>>
    {
        /// <summary>
        /// The model expression
        /// </summary>
        protected Expression<Func<TModel, TProperty>> modelExpression;
        private TagBuilder tagBuilder = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="FieldWrapperBuilder{TModel, TProperty}" /> class.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="modelExpression">The model expression.</param>
        public FieldWrapperBuilder(HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> modelExpression)
            : base(helper)
        {
            this.modelExpression = modelExpression;
        }

        private string _OverrideTemplateName = null;
        /// <summary>
        /// Gets or sets the name of the override template.
        /// </summary>
        /// <value>
        /// The name of the override template.
        /// </value>
        internal string OverrideTemplateName
        {
            get
            {
                return _OverrideTemplateName;
            }
            set
            {
                _OverrideTemplateName = value;
            }
        }

        /// <summary>
        /// Adds custom layout classes to the builder.
        /// </summary>
        /// <param name="classes">The classes.</param>
        /// <returns></returns>
        internal new FieldWrapperBuilder<TModel, TProperty> WithCustomLayoutClasses(params string[] classes)
        {
            return base.WithCustomLayoutClasses(classes);
        }

        /// <summary>
        /// Renders the start tag.
        /// </summary>
        /// <returns></returns>
        protected override string RenderStartTag()
        {
            tagBuilder = new TagBuilder("div");

            string templateName = _OverrideTemplateName;

            if (String.IsNullOrEmpty(templateName))
            {
                templateName = MvcHelper.Current.GetTemplateName(helper, modelExpression);
            }
            
            tagBuilder.MergeAttributesAppendClasses(htmlAttributes);

            tagBuilder.AddCssClass("field");
            tagBuilder.AddCssClass("dataType-" + templateName);
            tagBuilder.AddCssClass("form-group");

            if (MvcHelper.Current.ExpressionHasErrors(helper, modelExpression))
            {
                tagBuilder.AddCssClass("has-error");
            }

            return tagBuilder.ToString(TagRenderMode.StartTag);
        }

        /// <summary>
        /// Renders the end tag.
        /// </summary>
        /// <returns></returns>
        protected override string RenderEndTag()
        {
            return tagBuilder.ToString(TagRenderMode.EndTag);
        }
    }
}
