using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using JS.Core.Web.ExtensionMethods;
using JS.Core.Foundation.Helpers;
using System.Linq.Expressions;
using System.Web.Routing;

namespace JS.Core.Web.Mvc.Html
{
    /// <summary>
    /// Base Button Builder
    /// </summary>
    public abstract class BaseButtonBuilder<TModel, TBuilder> : HtmlBuilder<TModel, TBuilder>
         where TBuilder : BaseButtonBuilder<TModel, TBuilder>
    {
        private string buttonText = null;
        private string name = null;
        private object value = null;
        private string submitAction = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseButtonBuilder{TModel, TBuilder}"/> class.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="buttonText">The button text.</param>
        public BaseButtonBuilder(HtmlHelper<TModel> helper, string buttonText) : base(helper)
        {
            this.buttonText = buttonText;
        }

        private ButtonTypes _buttonType = ButtonTypes.Button;

        /// <summary>
        /// Gets or sets the type of the button.
        /// </summary>
        /// <value>
        /// The type of the button.
        /// </value>
        protected ButtonTypes ButtonType
        {
            get { return _buttonType; }
            set { _buttonType = value; }
        }

        private ButtonStyles _buttonStyle = ButtonStyles.Default;

        /// <summary>
        /// Gets or sets the button style.
        /// </summary>
        /// <value>
        /// The button style.
        /// </value>
        protected ButtonStyles ButtonStyle
        {
            get { return _buttonStyle; }
            set { _buttonStyle = value; }
        }

        private BrandColors _brandColor = BrandColors.Default;

        /// <summary>
        /// Gets or sets the color of the brand.
        /// </summary>
        /// <value>
        /// The color of the brand.
        /// </value>
        protected BrandColors BrandColor
        {
            get { return _brandColor; }
            set { _brandColor = value; }
        }

        /// <summary>
        /// Sets the Builder to Disabled
        /// </summary>
        /// <param name="isDisabled">if set to <c>true</c> [is disabled].</param>
        /// <returns></returns>
        public new TBuilder AsDisabled(bool isDisabled = true)
        {
            return base.AsDisabled(isDisabled);
        }

        /// <summary>
        /// Renders the Button as Disabled if a Condition Is Met
        /// </summary>
        /// <param name="modelExpression">The model expression.</param>
        /// <param name="targetValue">The target value.</param>
        /// <param name="conditionType">Type of the comparison.</param>
        /// <returns></returns>
        public new TBuilder AsDisabledIf<TProperty>(Expression<Func<TModel, TProperty>> modelExpression,
            object targetValue,
            JSConditionTypes conditionType = JSConditionTypes.IsEqualToValue)
        {
            return base.AsDisabledIf(modelExpression, targetValue, conditionType);
        }

        /// <summary>
        /// Adds a Name and Value to the Button
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public TBuilder WithNameValue(string name, object value)
        {
            if (!String.IsNullOrEmpty(name) && value != null)
            {
                this.name = name;
                this.value = value;
            }

            return (TBuilder)this;
        }

        /// <summary>
        /// Makes the Builder a Submit Button
        /// </summary>
        /// <returns></returns>
        public TBuilder AsSubmit()
        {
            ButtonType = ButtonTypes.Submit;

            return (TBuilder)this;
        }

        /// <summary>
        /// Makes the Button Submit to the Specified Action
        /// </summary>
        /// <param name="actionName">Name of the action.</param>
        /// <param name="controllerName">Name of the controller.</param>
        /// <param name="routeValues">The route values.</param>
        /// <returns></returns>
        public TBuilder AsSubmitToAction(string actionName, string controllerName = null, RouteValueDictionary routeValues = null)
        {
            AsSubmit();

            UrlHelper urlHelper = new UrlHelper(helper.ViewContext.RequestContext);

            submitAction = urlHelper.Action(actionName, controllerName, routeValues);

            return (TBuilder)this;
        }

        /// <summary>
        /// Sets the Button's Color
        /// </summary>
        /// <param name="color">The color.</param>
        /// <returns></returns>
        public TBuilder Color(BrandColors color)
        {
            BrandColor = color;

            return (TBuilder)this;
        }

        /// <summary>
        /// Renders this instance.
        /// </summary>
        /// <returns></returns>
        protected override string Render()
        {
            TagBuilder button = new TagBuilder("button");

            button.AddCssClass("btn");

            button.AddCssClass(String.Format("btn-{0}", EnumHelper.Current.GetName(BrandColor).ToLower()));

            switch (ButtonStyle)
            {
                case ButtonStyles.Default:
                    break;
                case ButtonStyles.Block:

                    button.AddCssClass("btn-block");
                    button.AddCssClass("center-block");
                    break;
            }

            switch (ButtonType)
            {
                case ButtonTypes.Button:
                    htmlAttributes.Add("type", "button");
                    break;
                case ButtonTypes.Submit:
                    htmlAttributes.Add("type", "submit");
                    break;
            }

            if (!String.IsNullOrEmpty(name) && value != null)
            {
                htmlAttributes.Add("name", name);
                htmlAttributes.Add("value", value.ToString());
            }

            if (!String.IsNullOrEmpty(submitAction))
            {
                WithDataAttribute("submit-to-action", submitAction);
            }

            button.MergeAttributesAppendClasses(htmlAttributes);

            button.InnerHtml = buttonText;

            return button.ToString(TagRenderMode.Normal);
        }
    }
}
