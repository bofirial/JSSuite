using JS.Core.Web.Helpers;
using JS.Core.Web.Mvc.Html.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace JS.Core.Web.Mvc.Html
{
    /// <summary>
    /// Field Builder
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <typeparam name="TProperty">The type of the property.</typeparam>
    public class FieldBuilder<TModel, TProperty> : InputBuilder<TModel, TProperty, FieldBuilder<TModel, TProperty>>, 
        ICustomLayoutClassesBuilder<TModel, FieldBuilder<TModel, TProperty>>
    {
        private LabelBuilder<TModel, TProperty> labelBuilder = null;
        private FieldWrapperBuilder<TModel, TProperty> fieldWrapperBuilder = null;
        private ValidationBuilder<TModel, TProperty> validationBuilder = null;

        private string beforeAddOnHtml = String.Empty;
        private string afterAddOnHtml = String.Empty;

        private IEnumerable<SelectListItem> selectList = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="FieldBuilder{TModel, TProperty}"/> class.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="modelExpression">The model expression.</param>
        public FieldBuilder(HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> modelExpression) : base(helper, modelExpression)
        {
            labelBuilder = new LabelBuilder<TModel, TProperty>(helper, modelExpression);
            fieldWrapperBuilder = new FieldWrapperBuilder<TModel, TProperty>(helper, modelExpression);
            validationBuilder = new ValidationBuilder<TModel, TProperty>(helper, modelExpression);

            SetFieldWidth(FieldSpans.Single);
        }

        /// <summary>
        /// Adds a Field Icon to the Field
        /// </summary>
        /// <param name="faIcon">The fa icon.</param>
        /// <returns></returns>
        public FieldBuilder<TModel, TProperty> WithFieldIcon(FAIcons faIcon)
        {
            return WithFieldIcon(i => i.Icon(faIcon));
        }

        /// <summary>
        /// Adds a Field Icon to the Field
        /// </summary>
        /// <param name="fieldIconFactoryFunction">The field icon builder.</param>
        /// <returns></returns>
        public FieldBuilder<TModel, TProperty> WithFieldIcon(Func<FieldIconFactory<TModel>, FieldIconBuilder<TModel>> fieldIconFactoryFunction)
        {
            FieldIconFactory<TModel> fieldIconFactory = new FieldIconFactory<TModel>(helper);

            beforeAddOnHtml = fieldIconFactoryFunction(fieldIconFactory).ToHtmlString();

            return this;
        }

        /// <summary>
        /// Adds a Field Buttons to the Field
        /// </summary>
        /// <returns></returns>
        public FieldBuilder<TModel, TProperty> WithFieldButtons(Action<FieldButtonFactory<TModel>> fieldIconFactoryFunction)
        {
            FieldButtonFactory<TModel> factory = new FieldButtonFactory<TModel>(helper);

            fieldIconFactoryFunction(factory);

            afterAddOnHtml = factory.ToHtmlString();

            return this;
        }

        /// <summary>
        /// Adds a Field Check Box to the Field
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public FieldBuilder<TModel, TProperty> WithFieldCheckBox()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Adds a Field Radio Button to the Field
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public FieldBuilder<TModel, TProperty> WithFieldRadioButton()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Adds custom layout classes to the builder.
        /// </summary>
        /// <param name="classes">The classes.</param>
        /// <returns></returns>
        public new FieldBuilder<TModel, TProperty> WithCustomLayoutClasses(params string[] classes)
        {
            fieldWrapperBuilder.WithCustomLayoutClasses(classes);

            return this;
        }

        /// <summary>
        /// Sets the width of the field.
        /// </summary>
        /// <param name="fieldSpan">The field span.</param>
        /// <returns></returns>
        public FieldBuilder<TModel, TProperty> SetFieldWidth(FieldSpans fieldSpan)
        {

            switch (fieldSpan)
            {
                case FieldSpans.Double:
                    WithCustomLayoutClasses("col-sm-12", "col-md-8", "col-lg-6");
                    break;
                case FieldSpans.Triple:
                    WithCustomLayoutClasses("col-md-12", "col-lg-9");
                    break;
                case FieldSpans.Full:
                    WithCustomLayoutClasses("col-xs-12");
                    break;
                case FieldSpans.Single:
                default:
                    WithCustomLayoutClasses("col-sm-6", "col-md-4", "col-lg-3");
                    break;
            }

            return this;
        }

        /// <summary>
        /// Renders the Field as a Dropdown
        /// </summary>
        /// <returns></returns>
        public FieldBuilder<TModel, TProperty> AsDropDown(IEnumerable<SelectListItem> selectList)
        {
            OverrideTemplateName = "DropDown";

            this.selectList = selectList;

            return this;
        }

        /// <summary>
        /// Renders the Form Field as a Hidden Input
        /// </summary>
        /// <returns></returns>
        public override FieldBuilder<TModel, TProperty> AsHidden(bool isHidden = true)
        {
            if (isHidden)
            {
                OverrideTemplateName = "HiddenInput"; 
            }

            return this;
        }

        /// <summary>
        /// Allows Customization of the Fields Wrapper
        /// </summary>
        /// <param name="fieldWrapperFactory">The field wrapper factory.</param>
        /// <returns></returns>
        public FieldBuilder<TModel, TProperty> Wrapper(Action<FieldWrapperBuilder<TModel, TProperty>> fieldWrapperFactory)
        {
            fieldWrapperFactory(fieldWrapperBuilder);

            return this;
        }

        /// <summary>
        /// Allows Customization of the Fields Label
        /// </summary>
        /// <param name="labelFactory">The label factory.</param>
        /// <returns></returns>
        public FieldBuilder<TModel, TProperty> Label(Action<LabelBuilder<TModel, TProperty>> labelFactory)
        {
            labelFactory(labelBuilder);

            return this;
        }

        /// <summary>
        /// Allows Customization of the Fields Validation
        /// </summary>
        /// <param name="validationFactory">The validation factory.</param>
        /// <returns></returns>
        public FieldBuilder<TModel, TProperty> Validation(Action<ValidationBuilder<TModel, TProperty>> validationFactory)
        {
            validationFactory(validationBuilder);

            return this;
        }

        /// <summary>
        /// Gets or sets the name of the override template.
        /// </summary>
        /// <value>
        /// The name of the override template.
        /// </value>
        internal string OverrideTemplateName
        {
            get
            {
                return fieldWrapperBuilder.OverrideTemplateName;
            }
            set
            {
                fieldWrapperBuilder.OverrideTemplateName = value;
            }
        }

        /// <summary>
        /// Renders the Builder Item
        /// </summary>
        /// <returns></returns>
        protected override string Render()
        {
            StringBuilder output = new StringBuilder();

            string templateName = fieldWrapperBuilder.OverrideTemplateName;

            if (String.IsNullOrEmpty(templateName))
            {
                templateName = MvcHelper.Current.GetTemplateName(helper, modelExpression);
            }

            output.Append(fieldWrapperBuilder.ToHtmlString(TagRenderMode.StartTag));

            if (!IsInputInLabelTemplate(templateName))
            {
                output.Append(RenderLabel(labelBuilder));
                output.Append(RenderField(templateName)); 
            }
            else
            {
                TagBuilder inputWrapper = new TagBuilder("div");

                if (templateName == "Boolean")
                {
                    inputWrapper.AddCssClass("checkbox");
                }
                else
                {
                    inputWrapper.AddCssClass("radio");
                }

                output.Append(inputWrapper.ToString(TagRenderMode.StartTag));
                output.Append(RenderField(templateName)); 
                output.Append(RenderLabel(labelBuilder));
                output.Append(inputWrapper.ToString(TagRenderMode.EndTag));
            }

            output.Append(RenderValidation(validationBuilder));

            output.Append(fieldWrapperBuilder.ToHtmlString(TagRenderMode.EndTag));

            return output.ToString();
        }

        private bool IsInputInLabelTemplate(string templateName)
        {
            return templateName == "Boolean";
        }

        private string RenderValidation(ValidationBuilder<TModel, TProperty> validationBuilder)
        {
            return validationBuilder.ToHtmlString();
        }

        private string RenderField(string templateName)
        {
            if (!IsInputInLabelTemplate(templateName))
            {
                htmlAttributes.AddClass("form-control"); 
            }

            FieldDataWrapper fieldDataWrapper = new FieldDataWrapper()
            {
                FieldData = new FieldData()
                {
                    HtmlAttributes = htmlAttributes
                }
            };

            if (selectList != null)
            {
                fieldDataWrapper.DropDownFieldData = new DropDownFieldData()
                {
                    SelectList = selectList
                };
            }

            string beforeInputHtml = String.Empty;
            string afterInputHtml = String.Empty;

            if (!string.IsNullOrEmpty(beforeAddOnHtml) || !string.IsNullOrEmpty(afterAddOnHtml))
            {
                TagBuilder inputGroup = new TagBuilder("div");

                inputGroup.AddCssClass("input-group");

                beforeInputHtml += inputGroup.ToString(TagRenderMode.StartTag);
                beforeInputHtml += beforeAddOnHtml;

                afterInputHtml += afterAddOnHtml;
                afterInputHtml += inputGroup.ToString(TagRenderMode.EndTag);
            }
            
            return String.Format("{1}{0}{2}",
                helper.EditorFor(modelExpression, OverrideTemplateName, fieldDataWrapper).ToHtmlString(),
                beforeInputHtml,
                afterInputHtml);
        }

        private string RenderLabel(LabelBuilder<TModel, TProperty> labelBuilder)
        {
            return labelBuilder.ToHtmlString();
        }
    }
}
