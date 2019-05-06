using System.Web;
using System.Web.Optimization;

namespace AHD
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //add all css
            bundles.Add(new StyleBundle("~/Rebone/css").Include(
                      "~/Rebone/public/vendors/iconfonts/mdi/css/materialdesignicons.min.css",
                       "~/Rebone/public/vendors/iconfonts/puse-icons-feather/feather.css",
                       "~/Rebone/public/vendors/css/vendor.bundle.base.css",
                       "~/Rebone/public/vendors/css/vendor.bundle.addons.css",
                       "~/Rebone/public/css/style.css"));

            bundles.Add(new ScriptBundle("~/Rebone/js").Include(
                      "~/Rebone/public/vendors/js/vendor.bundle.base.js",
                       "~/Rebone/public/vendors/js/vendor.bundle.addons.js",
                       "~/Rebone/public/js/off-canvas.js",
                       "~/Rebone/public/js/hoverable-collapse.js",
                       "~/Rebone/public/js/misc.js",
                       "~/Rebone/public/js/settings.js",
                       "~/Rebone/public/js/todolist.js"));

            /*bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));*/



        }
    }
}
