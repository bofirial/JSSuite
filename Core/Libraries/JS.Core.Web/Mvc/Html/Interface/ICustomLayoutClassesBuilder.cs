using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JS.Core.Web.Mvc.Html.Interface
{
    /// <summary>
    /// Interface for Custom Layout Classes Builders
    /// </summary>
    public interface ICustomLayoutClassesBuilder<TModel, TBuilder> 
        where TBuilder : BaseHtmlBuilder<TModel, TBuilder>
    {

        /// <summary>
        /// Adds custom layout classes to the builder.
        /// </summary>
        /// <param name="classes">The classes.</param>
        /// <returns></returns>
        TBuilder WithCustomLayoutClasses(params string[] classes);
    }
}
