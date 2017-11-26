using JS.Core.Foundation.BaseClasses;
using JS.Core.Web.Mvc.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace JS.Core.Web.ExtensionMethods
{
    /// <summary>
    /// Tag Builder Extensions
    /// </summary>
    public static class TagBuilderExtensions
    {
        /// <summary>
        /// Extends Merge Attributes to also intelligently handle clashing class attributes
        /// </summary>
        /// <param name="tagBuilder">The tag builder.</param>
        /// <param name="attributes">The attributes.</param>
        public static void MergeAttributesAppendClasses(this TagBuilder tagBuilder, HtmlAttributeDictionary attributes)
        {
            MergeAttributesAppendClasses(tagBuilder, attributes, false);
        }

        /// <summary>
        /// Extends Merge Attributes to also intelligently handle clashing class attributes
        /// </summary>
        /// <param name="tagBuilder">The tag builder.</param>
        /// <param name="attributes">The attributes.</param>
        /// <param name="replaceExisting">if set to <c>true</c> [replace existing].</param>
        public static void MergeAttributesAppendClasses(this TagBuilder tagBuilder, HtmlAttributeDictionary attributes, bool replaceExisting)
        {
            var attributesWithoutClass = attributes.Where(kvp => kvp.Key.ToString() != "class").ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            if (attributes.ContainsKey("class"))
            {
                tagBuilder.AddCssClass(attributes["class"].ToString());
            }

            tagBuilder.MergeAttributes(attributesWithoutClass, replaceExisting);
        }
    }
}
