using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace JS.Suite.BusinessLogic.Templating
{
    /// <summary>
    /// Templating View Engine
    /// </summary>
    public class TemplatingViewEngine : RazorViewEngine
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TemplatingViewEngine"/> class.
        /// </summary>
        public TemplatingViewEngine()
        {
            ViewLocationFormats = new[] {
                "~/Views/Shared/Common/Templates/Email/{0}.cshtml"
            };
            PartialViewLocationFormats = ViewLocationFormats;
        }
    }
}
