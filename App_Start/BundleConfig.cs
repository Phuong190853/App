using System.Web;
using System.Web.Optimization;

namespace Asm
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrapfile").Include(
                      "~/Scripts/js/bootstrap.js",
                      "~/Scripts/js/custom.js",
                      "~/Scripts/js/jquery-1.11.1.min.js",
                      "~/Scripts/js/modernizr.custom.js",
                      "~/Scripts/js/metisMenu.min.js"

                      ));

            bundles.Add(new StyleBundle("~/Content/cssfile").Include(
                      "~/Content/css/bootstrap.css",
                      "~/Content/css/style.css",
                      "~/Content/css/font-awesome.css",
                      "~/Content/css/SidebarNav.min.css",
                      "~/Content/css/custom.css"
                      ));

            BundleTable.EnableOptimizations = true;
        }
    }
}
