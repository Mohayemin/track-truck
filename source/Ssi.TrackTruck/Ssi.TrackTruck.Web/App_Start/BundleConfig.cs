using System.Web.Optimization;

namespace Ssi.TrackTruck.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
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