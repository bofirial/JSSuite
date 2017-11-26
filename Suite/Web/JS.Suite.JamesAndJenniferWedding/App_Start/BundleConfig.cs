using JS.Core.Web.Optimization;
using System.Web;
using System.Web.Optimization;

namespace JS.Suite.JamesAndJenniferWedding
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
            bundles.Add(new StyleBundle("~/Content/css") //BaseSiteStyleBundle("~/Content/css")
                .Include("~/Content/Generated/Site.css")
                .Include("~/Content/Generated/Site.Header.css")
                .Include("~/Content/Generated/Site.Article.css")
                .Include("~/Content/Generated/Site.FieldGroup.css")
                .Include("~/Content/Generated/Site.Registries.css")
                .Include("~/Content/Generated/Site.SummaryCard.css"));
        }
    }
}
