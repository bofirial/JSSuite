using JS.Core.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace JS.Core.Web.Mvc.Html
{
    /// <summary>
    /// Base Html Builder
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <typeparam name="TBuilder">The type of the builder.</typeparam>
    public abstract class BaseHtmlBuilder<TModel, TBuilder> : IHtmlString where TBuilder : BaseHtmlBuilder<TModel, TBuilder>
    {
        /// <summary>
        /// The helper
        /// </summary>
        protected HtmlHelper<TModel> helper;
        /// <summary>
        /// The HTML attributes
        /// </summary>
        protected HtmlAttributeDictionary htmlAttributes;

        /// <summary>
        /// The custom layout class
        /// </summary>
        protected string customLayoutClass;

        /// <summary>
        /// The hidden if condition keys
        /// </summary>
        protected List<string> hiddenIfConditionKeys;

        /// <summary>
        /// The disabled if condition keys
        /// </summary>
        protected List<string> disabledIfConditionKeys;
        
        /// <summary>
        /// The column span
        /// </summary>
        protected FieldSpans columnSpan;

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlBuilder{TModel, TProperty}" /> class.
        /// </summary>
        /// <param name="helper">The helper.</param>
        public BaseHtmlBuilder(HtmlHelper<TModel> helper)
        {
            this.helper = helper;
            this.htmlAttributes = new HtmlAttributeDictionary();

            this.hiddenIfConditionKeys = new List<string>();
            this.disabledIfConditionKeys = new List<string>();
        }

        /// <summary>
        /// Adds a Class to the Builder
        /// </summary>
        /// <param name="class">The class.</param>
        /// <returns></returns>
        public TBuilder WithClass(string @class)
        {
            htmlAttributes.AddClass(@class);

            return (TBuilder)this;
        }

        /// <summary>
        /// Adds a Class to the Builder
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public TBuilder WithDataAttribute(string key, object value)
        {
            if (!key.ToLower().StartsWith("data-"))
            {
                key = "data-" + key;
            }

            if (!htmlAttributes.ContainsKey(key))
            {
                htmlAttributes.Add(key, value);
            }

            return (TBuilder)this;
        }

        /// <summary>
        /// Adds Title Text to the Builder
        /// </summary>
        /// <param name="titleText">The title text.</param>
        /// <returns></returns>
        public TBuilder WithTitle(string titleText)
        {
            if (!htmlAttributes.ContainsKey("title"))
            {
                htmlAttributes.Add("title", titleText);
            }
            else
            {
                htmlAttributes["title"] = titleText;
            }

            return (TBuilder)this;
        }

        /// <summary>
        /// Hides the Element
        /// </summary>
        /// <param name="isHidden">if set to <c>true</c> [is hidden].</param>
        /// <returns></returns>
        public virtual TBuilder AsHidden(bool isHidden = true)
        {
            if (isHidden)
            {
                htmlAttributes.AddClass("hide");
            }
            else
            {
                htmlAttributes.RemoveClass("hide");
            }

            return (TBuilder)this;
        }

        private bool CheckCondition<TProperty>(Expression<Func<TModel, TProperty>> modelExpression,
            object targetValue,
            JSConditionTypes comparisonType)
        {
            switch (comparisonType)
            {
                case JSConditionTypes.IsNotEqualToValue:

                    return (MvcHelper.Current.GetPropertyValue(helper, modelExpression).ToString() != targetValue.ToString());
                case JSConditionTypes.ValidationEqualsValue:

                    return (MvcHelper.Current.ExpressionHasErrors(helper, modelExpression) != (bool)targetValue);
                case JSConditionTypes.IsEqualToValue:
                default:

                    return (MvcHelper.Current.GetPropertyValue(helper, modelExpression).ToString() == targetValue.ToString());
            }
        }

        private string GenerateConditionKey()
        {
            return Guid.NewGuid().ToString().Substring(0, 5).ToLower();
        }

        private string AddConditionDataAttributes<TProperty>(Expression<Func<TModel, TProperty>> modelExpression,
            object targetValue,
            JSConditionTypes conditionType)
        {
            string conditionKey = GenerateConditionKey();

            WithDataAttribute(String.Format("condition-target-name-k{0}", conditionKey), MvcHelper.Current.GetPropertyName(helper, modelExpression));
            WithDataAttribute(String.Format("condition-target-value-k{0}", conditionKey), targetValue);
            WithDataAttribute(String.Format("condition-type-k{0}", conditionKey), (int)conditionType);

            return conditionKey;
        }

        /// <summary>
        /// Renders the Element as a Hidden If Control
        /// </summary>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="modelExpression">The model expression.</param>
        /// <param name="targetValue">The target value.</param>
        /// <param name="conditionType">Type of the condition.</param>
        /// <returns></returns>
        public TBuilder AsHiddenIf<TProperty>(Expression<Func<TModel, TProperty>> modelExpression,
            object targetValue,
            JSConditionTypes conditionType = JSConditionTypes.IsEqualToValue)
        {
            AsHidden(CheckCondition(modelExpression, targetValue, conditionType));

            string hiddenIfKey = AddConditionDataAttributes(modelExpression, targetValue, conditionType);

            hiddenIfConditionKeys.Add(hiddenIfKey);

            return (TBuilder)this;
        }

        #region Protected Implementations Optionally Exposed in Child Classes

        /// <summary>
        /// Sets the Builder to Disabled
        /// </summary>
        /// <param name="isDisabled">if set to <c>true</c> [is disabled].</param>
        /// <returns></returns>
        protected TBuilder AsDisabled(bool isDisabled = true)
        {
            if (isDisabled)
            {
                if (!htmlAttributes.ContainsKey("disabled"))
                {
                    htmlAttributes.Add("disabled", true);
                }
            }
            else
            {
                htmlAttributes.Remove("disabled");
            }

            return (TBuilder)this;
        }


        /// <summary>
        /// Renders the Element as a Disabled If Control
        /// </summary>
        /// <param name="modelExpression">The model expression.</param>
        /// <param name="targetValue">The target value.</param>
        /// <param name="conditionType">Type of the comparison.</param>
        /// <returns></returns>
        protected TBuilder AsDisabledIf<TProperty>(Expression<Func<TModel, TProperty>> modelExpression,
            object targetValue, 
            JSConditionTypes conditionType = JSConditionTypes.IsEqualToValue)
        {
            AsDisabled(CheckCondition(modelExpression, targetValue, conditionType));

            string disabledIfKey = AddConditionDataAttributes(modelExpression, targetValue, conditionType);

            disabledIfConditionKeys.Add(disabledIfKey);

            return (TBuilder)this;
        }
        /// <summary>
        /// Adds custom layout classes to the builder.
        /// </summary>
        /// <param name="classes">The classes.</param>
        /// <returns></returns>
        protected TBuilder WithCustomLayoutClasses(params string[] classes)
        {
            customLayoutClass = String.Join(" ", classes);

            return (TBuilder)this;
        }

        #endregion

        /// <summary>
        /// Imports the attributes.
        /// </summary>
        /// <param name="attributes">The attributes.</param>
        /// <returns></returns>
        internal TBuilder ImportAttributes(HtmlAttributeDictionary attributes)
        {
            this.htmlAttributes = attributes;

            return (TBuilder)this;
        }

        /// <summary>
        /// This function is run right before rendering the control.
        /// It can be used to convert stored variables into html attributes that need to be rendered.
        /// </summary>
        protected virtual void BeforeRender()
        {
            if (!String.IsNullOrEmpty(customLayoutClass))
            {
                htmlAttributes.AddClass(customLayoutClass);
            }

            if (hiddenIfConditionKeys.Any())
            {
                WithDataAttribute("hidden-if-keys", hiddenIfConditionKeys.Aggregate((key1, key2) => key1 + ',' + key2));
            }

            if (disabledIfConditionKeys.Any())
            {
                WithDataAttribute("disabled-if-keys", disabledIfConditionKeys.Aggregate((key1, key2) => key1 + ',' + key2));
            }
        }

        /// <summary>
        /// Renders the Builder Item
        /// </summary>
        /// <returns></returns>
        protected abstract string Render();

        /// <summary>
        /// Returns an HTML-encoded string.
        /// </summary>
        /// <returns>
        /// An HTML-encoded string.
        /// </returns>
        public abstract string ToHtmlString();
    }
}
