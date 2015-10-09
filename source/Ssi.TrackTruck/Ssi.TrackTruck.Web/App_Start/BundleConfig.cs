using System.Web.Optimization;

namespace Ssi.TrackTruck.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            var lib = new ScriptBundle("~/js/lib")
                .Include("~/Scripts/Lib/angular.js")
                .Include("~/Scripts/Lib/angular-route.js")
                .Include("~/Scripts/Lib/ui-bootstrap-tpls-0.13.4.min.js")
                .Include("~/Scripts/Lib/underscore-min.js")
                .Include("~/Scripts/Lib/angular-tablesort.js")
                .Include("~/Scripts/Lib/moment.min.js");

            var signIn = new ScriptBundle("~/js/signin")
                .IncludeDirectory("~/Scripts/SignIn", "*.js");

            var util = new ScriptBundle("~/js/util").IncludeDirectory("~/Scripts/Util", "*.js");

            var app = new ScriptBundle("~/js/app").IncludeDirectory("~/Scripts/App", "*.js", true);

            bundles.Add(lib);
            bundles.Add(signIn);
            bundles.Add(util);
            bundles.Add(app);
        }
    }
}