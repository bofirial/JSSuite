using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using JS.Core.Web.ExtensionMethods;

namespace JS.Core.Web.Mvc.Html
{
    /// <summary>
    /// Alert Builder
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    public class AlertBuilder<TModel> : HtmlBuilder<TModel, AlertBuilder<TModel>>
    {
        private AlertTypes alertType;

        private string alertText;

        /// <summary>
        /// Initializes a new instance of the <see cref="AlertBuilder{TModel}" /> class.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="alertText">The alert text.</param>
        /// <param name="alertType">Type of the alert.</param>
        public AlertBuilder(HtmlHelper<TModel> helper, string alertText, AlertTypes alertType = AlertTypes.Info)
            : base(helper)
        {
            this.alertType = alertType;
            this.alertText = alertText;
        }
        
        /// <summary>
        /// Renders this instance.
        /// </summary>
        /// <returns></returns>
        protected override string Render()
        {
            if (String.IsNullOrEmpty(alertText))
            {
                return String.Empty;
            }

            TagBuilder alert = new TagBuilder("div");

            alert.AddCssClass("alert alert-dismissable fade in");

            switch (alertType)
            {
                case AlertTypes.Success:

                    alert.AddCssClass("alert-success");
                    break;
                case AlertTypes.Info:

                    alert.AddCssClass("alert-info");
                    break;
                case AlertTypes.Warning:

                    alert.AddCssClass("alert-warning");
                    break;
                case AlertTypes.Danger:

                    alert.AddCssClass("alert-danger");
                    break;
                default:
                    break;
            }

            alert.MergeAttributesAppendClasses(htmlAttributes);

            alert.InnerHtml = String.Format("<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\">&times;</button>{0}", alertText);

            return alert.ToString();
        }
    }
}
