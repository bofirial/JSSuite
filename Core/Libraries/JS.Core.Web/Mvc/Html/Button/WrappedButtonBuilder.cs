using JS.Core.Web.Mvc.Html.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace JS.Core.Web.Mvc.Html
{
    /// <summary>
    /// Wrapped Button Builder
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    public class WrappedButtonBuilder<TModel> : BaseButtonBuilder<TModel, WrappedButtonBuilder<TModel>>,
        ICustomLayoutClassesBuilder<TModel, WrappedButtonBuilder<TModel>>
    {
        private ButtonWrapperBuilder<TModel> buttonWrapperBuilder = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="WrappedButtonBuilder{TModel}"/> class.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="buttonText">The button text.</param>
        public WrappedButtonBuilder(HtmlHelper<TModel> helper, string buttonText)
            : base(helper, buttonText)
        {
            buttonWrapperBuilder = new ButtonWrapperBuilder<TModel>(helper);

            WithCustomLayoutClasses("col-sm-4", "col-md-3", "col-lg-2");

            ButtonStyle = ButtonStyles.Block;
        }

        /// <summary>
        /// Adds custom layout classes to the builder.
        /// </summary>
        /// <param name="classes">The classes.</param>
        /// <returns></returns>
        public new WrappedButtonBuilder<TModel> WithCustomLayoutClasses(params string[] classes)
        {
            buttonWrapperBuilder.WithCustomLayoutClasses(classes);

            return this;
        }

        /// <summary>
        /// Exposes the Button Wrapper Builder to the user
        /// </summary>
        /// <param name="buttonWrapperFactory">The button wrapper factory.</param>
        /// <returns></returns>
        public WrappedButtonBuilder<TModel> Wrapper(Action<ButtonWrapperBuilder<TModel>> buttonWrapperFactory)
        {
            buttonWrapperFactory(buttonWrapperBuilder);

            return this;
        }

        /// <summary>
        /// Renders this instance.
        /// </summary>
        /// <returns></returns>
        protected override string Render()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(buttonWrapperBuilder.ToHtmlString(TagRenderMode.StartTag));

            TagBuilder pTag = new TagBuilder("p");

            sb.Append(pTag.ToString(TagRenderMode.StartTag));

            sb.Append(base.Render());

            sb.Append(pTag.ToString(TagRenderMode.EndTag));

            sb.Append(buttonWrapperBuilder.ToHtmlString(TagRenderMode.EndTag));

            return sb.ToString();
        }
    }
}
