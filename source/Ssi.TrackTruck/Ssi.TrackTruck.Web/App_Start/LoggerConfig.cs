using System;
using System.ComponentModel.Design;
using System.Configuration;
using System.Web;
using System.Web.Mvc;
using Elmah;
using Ssi.TrackTruck.Web;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(LoggerConfig), "RegisterLogger")]
namespace Ssi.TrackTruck.Web
{
    //http://blog.travisgosselin.com/elmah-with-programmatic-configuration/
    public class LoggerConfig
    {
        public static void RegisterLogger()
        {
            HttpApplication.RegisterModule(typeof(ErrorLogModule));
            HttpApplication.RegisterModule(typeof(ErrorFilterModule));
            ServiceCenter.Current = ElmahServiceProvider;
        }

        private static IServiceProvider ElmahServiceProvider(object context)
        {
            var container = new ServiceContainer(context as IServiceProvider);
            var log = new XmlFileErrorLog(HttpContext.Current.Server.MapPath("~/Errors/"))
            {
                ApplicationName = "SSI_Logistics"
            };
            container.AddService(typeof(ErrorLog), log);
            return container;
        }
    }
}