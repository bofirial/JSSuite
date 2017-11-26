using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace JS.Core.Web.Mvc.Html
{
    /// <summary>
    /// Field Icon Factory
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    public class FieldIconFactory<TModel>
    {
        private HtmlHelper<TModel> helper;

        /// <summary>
        /// Initializes a new instance of the <see cref="FieldIconFactory{TModel}"/> class.
        /// </summary>
        /// <param name="helper">The helper.</param>
        public FieldIconFactory(HtmlHelper<TModel> helper)
        {
            this.helper = helper;
        }

        /// <summary>
        /// Creates the Field Icon
        /// </summary>
        /// <param name="faIcon">The fa icon.</param>
        /// <returns></returns>
        public FieldIconBuilder<TModel> Icon(FAIcons faIcon)
        {
            return new FieldIconBuilder<TModel>(helper, faIcon);
        }
    }
}
