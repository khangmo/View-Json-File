using System.Web;
using System.Web.Optimization;

namespace SynchronousHttp
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/main.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery/Mobile").Include(
                        "~/Scripts/Mobile/jquery.mobile-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/respond.js",
                        "~/Scripts/bootbox.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/bootstrap.min.css",
                        "~/Content/bootstrap-theme.min.css",
                        "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/Mobile/css").Include(
                        "~/Content/Mobile/collapsible.css",
                        "~/Content/Mobile/jquery.mobile-1.4.2.min.css",
                        "~/Content/Mobile/jquery.mobile.theme-1.4.2.min.css"));
        }
    }
}
