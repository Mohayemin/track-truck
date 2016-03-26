using System.Web.Optimization;

namespace Ssi.TrackTruck.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            var signIn = new ScriptBundle("~/js/signin")
                .IncludeDirectory("~/Scripts/SignIn", "*.js");

            var util = new ScriptBundle("~/js/util").IncludeDirectory("~/Scripts/Util", "*.js");

            var app = new ScriptBundle("~/js/app").IncludeDirectory("~/Scripts/App", "*.js", true);

            bundles.Add(signIn);
            bundles.Add(util);
            bundles.Add(app);

            var css = new StyleBundle("~/Content/css/all")
                .Include("~/Content/css/bootstrap-superhero.min.css")
                .Include("~/Content/css/bootstrap-superhero-override.css")
                .Include("~/Content/css/font-awesome.min.css")
                .Include("~/Content/css/tablesort.css")
                .Include("~/Content/css/animate.css")
                .Include("~/Content/css/ng-tags-input.css")
                .Include("~/Content/css/ng-tags-input.bootstrap.css")
                .Include("~/Content/css/track-truck.css");

            bundles.Add(css);
        }
    }
}