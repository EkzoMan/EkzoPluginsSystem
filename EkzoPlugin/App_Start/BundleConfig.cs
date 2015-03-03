using System.Web;
using System.Web.Optimization;

namespace EkzoPlugin.Web
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = false;
            BundleTable.Bundles.IgnoreList.Clear();

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/Scripts/uikit").Include("~/Scripts/uikit.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/themes/default/site.css"));

            bundles.Add(new StyleBundle("~/bundles/uikit").Include(
                "~/Content/uikit.almost-flat.min.css"
                ));

        }
    }
}