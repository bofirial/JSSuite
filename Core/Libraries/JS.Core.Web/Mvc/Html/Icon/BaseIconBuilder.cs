using JS.Core.Foundation.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using JS.Core.Web.ExtensionMethods;

namespace JS.Core.Web.Mvc.Html
{
    /// <summary>
    /// Icon Builder
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <typeparam name="TBuilder">The type of the builder.</typeparam>
    public abstract class BaseIconBuilder<TModel, TBuilder> : HtmlBuilder<TModel, TBuilder>
         where TBuilder : BaseIconBuilder<TModel, TBuilder>
    {
        private FAIcons faIcon;
        private bool fixedWidth = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="IconBuilder{TModel}" /> class.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="faIcon">The fa icon.</param>
        public BaseIconBuilder(HtmlHelper<TModel> helper, FAIcons faIcon)
            : base(helper)
        {
            this.faIcon = faIcon;

            FixedWidth();
        }

        /// <summary>
        /// Sets the Icon to have a Fixed Width
        /// </summary>
        /// <param name="isFixedWidth">if set to <c>true</c> [is fixed width].</param>
        /// <returns></returns>
        public TBuilder FixedWidth(bool isFixedWidth = true)
        {
            fixedWidth = isFixedWidth;

            return (TBuilder)this;
        }

        /// <summary>
        /// Renders this instance.
        /// </summary>
        /// <returns></returns>
        protected override string Render()
        {
            TagBuilder icon = new TagBuilder("i");

            icon.AddCssClass("fa");

            string faClass = String.Format("fa-{0}", StringHelper.Current.CamelCaseToSeperated(EnumHelper.Current.GetName(faIcon), "-").ToLower());

            icon.AddCssClass(faClass);

            icon.AddCssClass("fa-lg");

            icon.MergeAttributesAppendClasses(htmlAttributes);

            if (fixedWidth)
            {
                icon.AddCssClass("fa-fw");
            }

            return icon.ToString();
        }
    }
}
