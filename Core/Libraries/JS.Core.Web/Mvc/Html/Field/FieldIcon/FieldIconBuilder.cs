using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace JS.Core.Web.Mvc.Html
{
    /// <summary>
    /// Field Icon Builder
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    public class FieldIconBuilder<TModel> : BaseIconBuilder<TModel, FieldIconBuilder<TModel>>
    {
        private AddOnWrapperBuilder<TModel> addOnWrapperBuilder;

        /// <summary>
        /// Initializes a new instance of the <see cref="FieldIconBuilder{TModel}"/> class.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="faIcon">The fa icon.</param>
        public FieldIconBuilder(HtmlHelper<TModel> helper, FAIcons faIcon)
            : base(helper, faIcon)
        {
            addOnWrapperBuilder = new AddOnWrapperBuilder<TModel>(helper, AddOnTypes.Default);
        }

        /// <summary>
        /// Gives Access to the Add On Wrapper
        /// </summary>
        /// <param name="addOnWrapperFactoryFunction">The add on wrapper factory function.</param>
        /// <returns></returns>
        public FieldIconBuilder<TModel> Wrapper(Action<AddOnWrapperBuilder<TModel>> addOnWrapperFactoryFunction)
        {
            addOnWrapperFactoryFunction(addOnWrapperBuilder);

            return this;
        }

        /// <summary>
        /// Renders this instance.
        /// </summary>
        /// <returns></returns>
        protected override string Render()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(addOnWrapperBuilder.ToHtmlString(TagRenderMode.StartTag));
            sb.Append(base.Render());
            sb.Append(addOnWrapperBuilder.ToHtmlString(TagRenderMode.EndTag));

            return sb.ToString();
        }
    }
}
