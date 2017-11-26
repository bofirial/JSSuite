using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using JS.Core.Web.ExtensionMethods;

namespace JS.Core.Web.Mvc.Html
{
    /// <summary>
    /// Add On Wrapper Builder
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    public class AddOnWrapperBuilder<TModel> : HtmlWrapperBuilder<TModel, AddOnWrapperBuilder<TModel>>
    {
        private TagBuilder tagBuilder = null;
        private AddOnTypes addOnType;


        /// <summary>
        /// Initializes a new instance of the <see cref="AddOnWrapperBuilder{TModel}"/> class.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="addOnType">Type of the add on.</param>
        public AddOnWrapperBuilder(HtmlHelper<TModel> helper, AddOnTypes addOnType)
            : base(helper)
        {
            this.addOnType = addOnType;
        }

        /// <summary>
        /// Renders the start tag.
        /// </summary>
        /// <returns></returns>
        protected override string RenderStartTag()
        {
            tagBuilder = new TagBuilder("div");

            switch (addOnType)
            {
                case AddOnTypes.Default:
                    tagBuilder.AddCssClass("input-group-addon");
                    break;
                case AddOnTypes.Button:
                    tagBuilder.AddCssClass("input-group-btn");
                    break;
            }

            tagBuilder.MergeAttributesAppendClasses(htmlAttributes);

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
