using System;
using System.Configuration;
using System.Runtime.CompilerServices;
using System.Web;
using Microsoft.Practices.Unity;
using MongoDB.Driver;
using Ssi.TrackTruck.Bussiness.Auth;
using Ssi.TrackTruck.Bussiness.DAL;
using Ssi.TrackTruck.Bussiness.Trips;
using Ssi.TrackTruck.Web.Utils;

namespace Ssi.TrackTruck.Web
{
    public class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> _container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        public static IUnityContainer GetConfiguredContainer()
        {
            return _container.Value;
        }
        #endregion

        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<HttpContextBase>(new InjectionFactory(_ =>
                new HttpContextWrapper(HttpContext.Current)));

            container.RegisterType<IHasher, Pbkdf2Hasher>();
            container.RegisterType<ISignedInUser, SignedInUser>();
            RegisterRepo(container);
        }

        private static void RegisterRepo(IUnityContainer container)
        {
            var connectionString = ConfigurationManager.AppSettings["MONGOLAB_URI"];
            var mongoUrl = new MongoUrl(connectionString);
            var mongoClient = new MongoClient(mongoUrl.Url);
            var mongoDb = mongoClient.GetServer().GetDatabase(mongoUrl.DatabaseName);

            container.RegisterType<MongoDatabase>(new InjectionFactory(_ => mongoDb));
            //container.RegisterType<IRepository, MongoRepository>();
            container.RegisterType<ITripRepository, TripMongoRepository>();
            container.RegisterType<IRepository, MongoRepository>();
        }
    }
}
