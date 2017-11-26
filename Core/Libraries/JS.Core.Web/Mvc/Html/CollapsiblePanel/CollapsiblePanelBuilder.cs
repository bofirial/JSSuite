using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace JS.Core.Web.Mvc.Html
{
    /// <summary>
    /// Collapsible Panel Builder
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    public class CollapsiblePanelBuilder<TModel> : HtmlWrapperBuilder<TModel, CollapsiblePanelBuilder<TModel>>
    {
        private TagBuilder panel;
        private TagBuilder panelCollapseContainer;
        private TagBuilder panelBody;

        private string collapsiblePanelText = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="CollapsiblePanelBuilder{TModel}" /> class.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="collapsiblePanelText">The collapsible panel text.</param>
        public CollapsiblePanelBuilder(HtmlHelper<TModel> helper, string collapsiblePanelText)
            : base(helper)
        {
            this.collapsiblePanelText = collapsiblePanelText;
        }

        /// <summary>
        /// Renders the start tag.
        /// </summary>
        /// <returns></returns>
        protected override string RenderStartTag()
        {
            StringBuilder panelStartHtml = new StringBuilder();

            panel = new TagBuilder("div");

            panel.AddCssClass("panel");
            panel.AddCssClass("collapsiblePanel");
            panel.AddCssClass("panel-default");

            panelStartHtml.Append(panel.ToString(TagRenderMode.StartTag));

            TagBuilder heading = new TagBuilder("div");

            heading.AddCssClass("panel-heading");

            TagBuilder title = new TagBuilder("a");

            title.AddCssClass("panel-title");
            title.MergeAttribute("data-toggle", "collapse");
            title.MergeAttribute("href", "#collapsibleId");

            title.SetInnerText(collapsiblePanelText);

            heading.InnerHtml = title.ToString();

            panelStartHtml.Append(heading.ToString());

            panelCollapseContainer = new TagBuilder("div");
            panelCollapseContainer.AddCssClass("panel-collapse");
            panelCollapseContainer.AddCssClass("collapse");
            panelCollapseContainer.AddCssClass("in");

            panelCollapseContainer.MergeAttribute("id", "collapsibleId");

            panelStartHtml.Append(panelCollapseContainer.ToString(TagRenderMode.StartTag));

            panelBody = new TagBuilder("div");

            panelBody.AddCssClass("panel-body");
            
            panelStartHtml.Append(panelBody.ToString(TagRenderMode.StartTag));

            return panelStartHtml.ToString();
        }

        /// <summary>
        /// Renders the end tag.
        /// </summary>
        /// <returns></returns>
        protected override string RenderEndTag()
        {
            return panelBody.ToString(TagRenderMode.EndTag) + panelCollapseContainer.ToString(TagRenderMode.EndTag) + panel.ToString(TagRenderMode.EndTag);
        }
    }
}
