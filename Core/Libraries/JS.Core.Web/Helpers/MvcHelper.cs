using JS.Core.Foundation.BaseClasses;
using JS.Core.Foundation.ErrorHandling;
using JS.Core.Foundation.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace JS.Core.Web.Helpers
{
    /// <summary>
    /// Mvc Helper
    /// </summary>
    public class MvcHelper : SingletonBase<MvcHelper>
    {
        /// <summary>
        /// Gets the name of the template.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="helper">The helper.</param>
        /// <param name="modelExpression">The model expression.</param>
        /// <returns></returns>
        public string GetTemplateName<TModel, TProperty>(HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> modelExpression)
        {
            ModelMetadata modelMetadata = ModelMetadata.FromLambdaExpression(modelExpression, helper.ViewData);

            string dataType = modelMetadata.DataTypeName;

            if (String.IsNullOrEmpty(dataType))
            {
                Type type = modelMetadata.ModelType;

                dataType = ReflectionHelper.Current.GetUnderlyingTypeName(type);
            }

            return dataType;
        }

        /// <summary>
        /// Gets the name of the property.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="helper">The helper.</param>
        /// <param name="modelExpression">The model expression.</param>
        /// <returns></returns>
        public string GetPropertyName<TModel, TProperty>(HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> modelExpression)
        {
            return helper.ViewData.TemplateInfo.GetFullHtmlFieldName(ExpressionHelper.GetExpressionText(modelExpression));
        }

        /// <summary>
        /// Gets the property identifier.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="helper">The helper.</param>
        /// <param name="modelExpression">The model expression.</param>
        /// <returns></returns>
        public string GetPropertyId<TModel, TProperty>(HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> modelExpression)
        {
            return helper.ViewData.TemplateInfo.GetFullHtmlFieldId(ExpressionHelper.GetExpressionText(modelExpression));
        }

        /// <summary>
        /// Gets the property value.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="helper">The helper.</param>
        /// <param name="modelExpression">The model expression.</param>
        /// <returns></returns>
        public TProperty GetPropertyValue<TModel, TProperty>(HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> modelExpression)
        {
            return (TProperty)ModelMetadata.FromLambdaExpression(modelExpression, helper.ViewData).Model;
        }

        /// <summary>
        /// Returns True if the Model Expression Contains any Errors
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="helper">The helper.</param>
        /// <param name="modelExpression">The model expression.</param>
        /// <returns></returns>
        public bool ExpressionHasErrors<TModel, TProperty>(HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> modelExpression)
        {
            string modelName = helper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(ExpressionHelper.GetExpressionText(modelExpression));
            if (!helper.ViewData.ModelState.ContainsKey(modelName))
                return false;

            ModelState modelState = helper.ViewData.ModelState[modelName];
            return modelState.Errors.Count > 0;
        }
    }
}
