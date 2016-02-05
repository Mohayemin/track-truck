using System;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace Ssi.TrackTruck.Web.Utils
{
    public class JsonNetResult : JsonResult
    {
        public JsonNetResult(object data)
        {
            Data = data;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            var response = context.HttpContext.Response;

            response.ContentType = !string.IsNullOrEmpty(ContentType)
                ? ContentType
                : "application/json";

            if (ContentEncoding != null)
                response.ContentEncoding = ContentEncoding;
            
            var serializedObject = JsonConvert.SerializeObject(Data, Formatting.Indented);
            response.Write(serializedObject);
            JsonRequestBehavior = JsonRequestBehavior.AllowGet;
        }

        internal static ActionResult JsonNet(string v, JsonRequestBehavior allowGet)
        {
            throw new NotImplementedException();
        }

        public static JsonNetResult JsonNet(object data)
        {
            return new JsonNetResult(data);
        }
    }
}