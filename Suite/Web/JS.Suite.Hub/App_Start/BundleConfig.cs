using JS.Core.Web.Optimization;
using System.Web;
using System.Web.Optimization;

namespace JS.Suite.Hub
{
    /// <summary>
    /// Bundle Config
    /// </summary>
    public class BundleConfig : BaseBundleConfig
    {
        /// <summary>
        /// Registers the bundles.
        /// </summary>
        /// <param name="bundles">The bundles.</param>
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(BaseSiteStyleBundle("~/Content/css")
                .Include("~/Content/Generated/Site.*")
                .Include("~/Content/Generated/Account.css"));

            bundles.Add(new StyleBundle("~/Content/login")
                .Include("~/Content/Generated/Login.*"));

            bundles.Add(BaseSiteJavascriptBundle("~/bundles/core")
                .Include("~/Scripts/Common/Validation/js.validation.js"));

            RegisterSupportSiteBundles(bundles);

            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.validate*"));

            //// Use the development version of Modernizr to develop with and learn from. Then, when you're
            //// ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            //bundles.Add(new StyleBundle("~/Content/css").Include(
            //          "~/Content/bootstrap.css",
            //          "~/Content/site.css"));
        }

        private static void RegisterSupportSiteBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/ApplicationLog").Include("~/Content/Generated/Support.ApplicationLog.*"));
        }
    }
}
