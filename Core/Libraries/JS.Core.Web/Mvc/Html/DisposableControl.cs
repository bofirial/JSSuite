using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace JS.Core.Web.Mvc.Html
{
    /// <summary>
    /// Disposable Control for Creating in Using Blocks
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    public class DisposableControl<TModel> : IDisposable
    {
        private HtmlHelper<TModel> helper;
        private string endTag;

        /// <summary>
        /// Initializes a new instance of the <see cref="DisposableControl{TModel}"/> class.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="startTag">The start tag.</param>
        /// <param name="endTag">The end tag.</param>
        public DisposableControl(HtmlHelper<TModel> helper, string startTag, string endTag)
        {
            this.helper = helper;
            this.endTag = endTag;

            RenderTag(startTag);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DisposableControl{TModel}"/> class.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="tagBuilder">The tag builder.</param>
        public DisposableControl(HtmlHelper<TModel> helper, TagBuilder tagBuilder) 
            : this(helper, tagBuilder.ToString(TagRenderMode.StartTag), tagBuilder.ToString(TagRenderMode.EndTag))
        {
        }

        private void RenderTag(string tag)
        {
            helper.ViewContext.Writer.Write(tag);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            RenderTag(endTag);
        }
    }
}
