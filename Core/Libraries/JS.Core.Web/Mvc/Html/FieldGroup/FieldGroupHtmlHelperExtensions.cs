using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace JS.Core.Web.Mvc.Html
{
    /// <summary>
    /// Field Group Html Helper Extensions
    /// </summary>
    public static class FieldGroupHtmlHelperExtensions
    {
        /// <summary>
        /// Creates a Field Group.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <param name="helper">The helper.</param>
        /// <param name="fieldGroupFactory">The field group factory.</param>
        /// <returns></returns>
        public static DisposableControl<TModel> FieldGroup<TModel>(this HtmlHelper<TModel> helper, Func<FieldGroupBuilder<TModel>, FieldGroupBuilder<TModel>> fieldGroupFactory)
        {
            FieldGroupBuilder<TModel> fieldGroupBuilder = new FieldGroupBuilder<TModel>(helper);

            fieldGroupFactory(fieldGroupBuilder);

            return fieldGroupBuilder.ToDisposableControl();
        }
    }
}
