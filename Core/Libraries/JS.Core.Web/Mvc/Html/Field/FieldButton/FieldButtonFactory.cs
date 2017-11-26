using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace JS.Core.Web.Mvc.Html
{
    /// <summary>
    /// Field Button Factory
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    public class FieldButtonFactory<TModel>
    {
        private HtmlHelper<TModel> helper;
        private List<FieldButtonBuilder<TModel>> fieldButtonBuilders;
        private AddOnWrapperBuilder<TModel> addOnWrapperBuilder;

        /// <summary>
        /// Initializes a new instance of the <see cref="FieldButtonFactory{TModel}"/> class.
        /// </summary>
        /// <param name="helper">The helper.</param>
        public FieldButtonFactory(HtmlHelper<TModel> helper)
        {
            this.helper = helper;

            fieldButtonBuilders = new List<FieldButtonBuilder<TModel>>();
            addOnWrapperBuilder = new AddOnWrapperBuilder<TModel>(helper, AddOnTypes.Button);
        }

        /// <summary>
        /// Adds a Field Button to the Field
        /// </summary>
        /// <param name="buttonText">The button text.</param>
        /// <returns></returns>
        public FieldButtonBuilder<TModel> Button(string buttonText)
        {
            FieldButtonBuilder<TModel> fieldButtonBuilder = new FieldButtonBuilder<TModel>(helper, buttonText);

            fieldButtonBuilders.Add(fieldButtonBuilder);

            return fieldButtonBuilder;
        }

        /// <summary>
        /// Gives Access to the Add On Wrapper
        /// </summary>
        /// <param name="addOnWrapperFactoryFunction">The add on wrapper factory function.</param>
        /// <returns></returns>
        public void Wrapper(Action<AddOnWrapperBuilder<TModel>> addOnWrapperFactoryFunction)
        {
            addOnWrapperFactoryFunction(addOnWrapperBuilder);
        }

        /// <summary>
        /// To the HTML string.
        /// </summary>
        /// <returns></returns>
        internal string ToHtmlString()
        {
            if (!fieldButtonBuilders.Any())
            {
                return String.Empty;
            }

            StringBuilder sb = new StringBuilder();

            sb.Append(addOnWrapperBuilder.ToHtmlString(TagRenderMode.StartTag));

            foreach (FieldButtonBuilder<TModel> fieldButtonBuilder in fieldButtonBuilders)
            {
                sb.Append(fieldButtonBuilder.ToHtmlString());
            }

            sb.Append(addOnWrapperBuilder.ToHtmlString(TagRenderMode.EndTag));

            return sb.ToString();
        }
    }
}
