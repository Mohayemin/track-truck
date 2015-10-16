using System.Linq;
using System.Web.Mvc;
using Ssi.TrackTruck.Bussiness.Models;

namespace Ssi.TrackTruck.Web.Utils
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var modelState = filterContext.Controller.ViewData.ModelState;

            if (!modelState.IsValid)
            {
                var allErrorMessages = modelState.Values.SelectMany(v => v.Errors).Select(error => error.ErrorMessage);
                filterContext.Result = new JsonNetResult(Response.ValidationError(allErrorMessages.FirstOrDefault()));
            }
        }
    }
}