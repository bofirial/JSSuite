using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Optimization;

namespace JS.Core.Web.Optimization
{
    /// <summary>
    /// Base Bundle Config
    /// </summary>
    public abstract class BaseBundleConfig
    {
        /// <summary>
        /// Gets a new Base Site Style Bundle
        /// </summary>
        /// <param name="bundleName">Name of the bundle.</param>
        /// <returns></returns>
        public static Bundle BaseSiteStyleBundle(string bundleName)
        {
            return new StyleBundle(bundleName)
                .Include("~/Content/Generated/bootstrap.css")
                .Include("~/Content/Generated/footable.core.css")
                .Include("~/Content/Generated/Controls.*");
        }

        /// <summary>
        /// Gets a new Base Javascript Style Bundle
        /// </summary>
        /// <param name="bundleName">Name of the bundle.</param>
        /// <returns></returns>
        public static Bundle BaseSiteJavascriptBundle(string bundleName)
        {
            return new ScriptBundle(bundleName)
                .Include("~/Scripts/Common/Externals/JQuery/jquery-{version}.js")
                .Include("~/Scripts/Common/Externals/Bootstrap/bootstrap.js")
                .Include("~/Scripts/Common/Externals/FooTable/footable.js")
                .Include("~/Scripts/Common/Externals/JQueryValidate/jquery.validate.js")
                .Include("~/Scripts/Common/Externals/JQueryValidate/jquery.validate.unobtrusive.js")
                .Include("~/Scripts/Common/Core/js.core.js")
                .IncludeDirectory("~/Scripts/Common/Core", "*.js")
                .IncludeDirectory("~/Scripts/Common/Controls", "*.js");
        }
    }
}
