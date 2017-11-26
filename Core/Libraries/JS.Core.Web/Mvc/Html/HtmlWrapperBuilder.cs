using JS.Core.Foundation.ErrorHandling;
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
    /// Html Wrapper Builder
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <typeparam name="TBuilder">The type of the builder.</typeparam>
    public abstract class HtmlWrapperBuilder<TModel, TBuilder> : BaseHtmlBuilder<TModel, TBuilder>
        where TBuilder : HtmlWrapperBuilder<TModel, TBuilder>
    {
        /// <summary>
        /// The before render has been called
        /// </summary>
        protected bool beforeRenderHasBeenCalled = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="InputBuilder{TModel, TProperty, TBuilder}"/> class.
        /// </summary>
        /// <param name="helper">The helper.</param>
        public HtmlWrapperBuilder(HtmlHelper<TModel> helper)
            : base(helper)
        {

        }

        /// <summary>
        /// Renders the start tag.
        /// </summary>
        /// <returns></returns>
        protected abstract string RenderStartTag();

        /// <summary>
        /// Renders the end tag.
        /// </summary>
        /// <returns></returns>
        protected abstract string RenderEndTag();

        /// <summary>
        /// Renders this instance.
        /// </summary>
        /// <returns></returns>
        protected override string Render()
        {
            return RenderStartTag() + RenderEndTag();
        }

        /// <summary>
        /// To the HTML string.
        /// </summary>
        /// <returns></returns>
        public sealed override string ToHtmlString()
        {
            return ToHtmlString(TagRenderMode.Normal);
        }

        /// <summary>
        /// To the HTML string.
        /// </summary>
        /// <param name="renderMode">The render mode.</param>
        /// <returns></returns>
        public string ToHtmlString(TagRenderMode renderMode)
        {
            if (!beforeRenderHasBeenCalled)
	        {
		        BeforeRender(); 
                
                beforeRenderHasBeenCalled = true;
	        }

            switch (renderMode)
            {
                case TagRenderMode.StartTag:

                    return RenderStartTag();

                case TagRenderMode.EndTag:

                    return RenderEndTag();

                case TagRenderMode.SelfClosing:

                    throw new CoreException("Self Closing Tags are not supported by Html Wrapper Builder", "Self Closing Tags are not supported by Html Wrapper Builder");

                case TagRenderMode.Normal:
                default:

                    return Render();
            }
        }

        /// <summary>
        /// Converts the Builder into a Disposable Control
        /// </summary>
        /// <returns></returns>
        internal DisposableControl<TModel> ToDisposableControl()
        {
            return new DisposableControl<TModel>(helper, ToHtmlString(TagRenderMode.StartTag), ToHtmlString(TagRenderMode.EndTag));
        }
    }
}
