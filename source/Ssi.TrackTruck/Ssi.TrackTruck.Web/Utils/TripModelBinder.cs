using System.Web.Mvc;

namespace Ssi.TrackTruck.Web.Utils
{
    public class TripModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            return base.BindModel(controllerContext, bindingContext);
        }
    }
}