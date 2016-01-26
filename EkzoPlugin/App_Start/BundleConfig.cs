using System.Web;
using System.Web.Optimization;

namespace EkzoPlugin.CoreSite.Web
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = false;
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                        "~/Content/materialize/css/materialize.min.css", 
                        "~/Content/Site.css"
                        ));

            bundles.Add(new ScriptBundle("~/Scripts/default").Include("~/Scripts/jquery-2.2.0.min.js",
                                                                      "~/Scripts/materialize/materialize.min.js",
                                                                      "~/Scripts/Global.js"));
        }
    }
}