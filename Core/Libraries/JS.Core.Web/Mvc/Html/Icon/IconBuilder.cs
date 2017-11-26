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
    public class IconBuilder<TModel> : BaseIconBuilder<TModel, IconBuilder<TModel>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IconBuilder{TModel}" /> class.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="faIcon">The fa icon.</param>
        public IconBuilder(HtmlHelper<TModel> helper, FAIcons faIcon)
            : base(helper, faIcon)
        {
            //Fixed Width by Default
            this.FixedWidth();
        }
    }
}
