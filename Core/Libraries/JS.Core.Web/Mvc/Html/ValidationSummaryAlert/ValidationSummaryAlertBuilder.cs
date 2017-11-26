using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace JS.Core.Web.Mvc.Html
{
    /// <summary>
    /// Validation Summary Alert Builder
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    public class ValidationSummaryAlertBuilder<TModel> : HtmlBuilder<TModel, ValidationSummaryAlertBuilder<TModel>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationSummaryAlertBuilder{TModel}"/> class.
        /// </summary>
        /// <param name="helper">The helper.</param>
        public ValidationSummaryAlertBuilder(HtmlHelper<TModel> helper) : base(helper)
        {

        }

        /// <summary>
        /// Renders this instance.
        /// </summary>
        /// <returns></returns>
        protected override string Render()
        {
            ModelState modelState = helper.ViewData.ModelState[""];

            string output = String.Empty;

            if (modelState != null && modelState.Errors.Count() > 0)
            {
                foreach (var error in modelState.Errors)
                {
                    output += helper.Alert(error.ErrorMessage, AlertTypes.Danger).ImportAttributes(htmlAttributes).ToHtmlString();
                }
            }

            return output;
        }
    }
}
