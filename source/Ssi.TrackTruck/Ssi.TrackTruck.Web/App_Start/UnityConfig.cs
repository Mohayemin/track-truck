using System;
using System.Configuration;
using Microsoft.Practices.Unity;
using MongoDB.Driver;
using Ssi.TrackTruck.Bussiness.Auth;
using Ssi.TrackTruck.Bussiness.DAL;

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
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            container.RegisterType<IHasher, Pbkdf2Hasher>();
            RegisterRepo(container);
        }

        private static void RegisterRepo(IUnityContainer container)
        {
            var connectionString = ConfigurationManager.AppSettings["MONGOLAB_URI"];
            var mongoUrl = new MongoUrl(connectionString);
            var mongoClient = new MongoClient(mongoUrl.Url);
            var mongoDb = mongoClient.GetServer().GetDatabase(mongoUrl.DatabaseName);
            var collectionMapper = new CollectionMapper();

            container.RegisterType<IRepository, MongoRepository>(new InjectionFactory(c => new MongoRepository(mongoDb, collectionMapper.Map)));

            //container.RegisterType<IRepository, DummyRepository>();
        }
    }
}
