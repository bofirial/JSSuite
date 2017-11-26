using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JS.Core.Web.ExtensionMethods;
using System.Web.Mvc;

namespace JS.Core.Web.Mvc.Html
{
    /// <summary>
    /// Button Wrapper Builder
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    public class ButtonWrapperBuilder<TModel> : HtmlWrapperBuilder<TModel, ButtonWrapperBuilder<TModel>>
    {
        private TagBuilder tagBuilder = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="ButtonWrapperBuilder{TModel}"/> class.
        /// </summary>
        /// <param name="helper">The helper.</param>
        public ButtonWrapperBuilder(HtmlHelper<TModel> helper) : base(helper)
        {
        }

        /// <summary>
        /// Adds custom layout classes to the builder.
        /// </summary>
        /// <param name="classes">The classes.</param>
        /// <returns></returns>
        internal new ButtonWrapperBuilder<TModel> WithCustomLayoutClasses(params string[] classes)
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

            tagBuilder.MergeAttributesAppendClasses(htmlAttributes);

            tagBuilder.AddCssClass("btn-wrapper");

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
