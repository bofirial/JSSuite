using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using JS.Core.Web.ExtensionMethods;
using JS.Core.Web.Mvc.Html.Interface;

namespace JS.Core.Web.Mvc.Html
{
    /// <summary>
    /// Base Group Builder
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <typeparam name="TBuilder">The type of the builder.</typeparam>
    public abstract class BaseGroupBuilder<TModel, TBuilder> : HtmlWrapperBuilder<TModel, TBuilder>,
        ICustomLayoutClassesBuilder<TModel, TBuilder>
        where TBuilder : BaseGroupBuilder<TModel, TBuilder>
    {
        /// <summary>
        /// The tag builder
        /// </summary>
        protected TagBuilder tagBuilder = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseGroupBuilder{TModel, TBuilder}"/> class.
        /// </summary>
        /// <param name="helper">The helper.</param>
        public BaseGroupBuilder(HtmlHelper<TModel> helper)
            : base(helper)
        {
            tagBuilder = new TagBuilder("div");

            base.WithCustomLayoutClasses("row");
        }

        /// <summary>
        /// Adds custom layout classes to the builder.
        /// </summary>
        /// <param name="classes">The classes.</param>
        /// <returns></returns>
        public new TBuilder WithCustomLayoutClasses(params string[] classes)
        {
            return base.WithCustomLayoutClasses(classes);
        }

        /// <summary>
        /// Renders the start tag.
        /// </summary>
        /// <returns></returns>
        protected override string RenderStartTag()
        {
            if (tagBuilder == null)
            {
                tagBuilder = new TagBuilder("div");
            }

            tagBuilder.MergeAttributesAppendClasses(htmlAttributes);

            //tagBuilder.AddCssClass("row");

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
