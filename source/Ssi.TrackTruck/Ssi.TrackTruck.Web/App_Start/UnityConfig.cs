using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using Ssi.TrackTruck.Bussiness.Auth;
using Ssi.TrackTruck.Bussiness.DAL;
using Ssi.TrackTruck.Bussiness.DAL.Entities;

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

            RegisterRepository(container);
            container.RegisterType<IHasher, Pbkdf2Hasher>();
        }

        private static void RegisterRepository(IUnityContainer container)
        {
            var data = new Dictionary<Type, IList>();
            data[typeof (User)] = new List<User>
            {
                new User
                {
                    Id = "1",
                    Username = "Mohayemin",
                    UsernameLowerCase = "mohayemin",
                    PasswordHash = "g+S4Aydl1ZTXWYxO8IdfJWVUJVCpeTc7D09FOEFfPT/rvjDhVFVe9pqfIFS8HfU36AMAAA=="
                }
            };
            container.RegisterType<IRepository, ListStorageRepository>(new InjectionConstructor(data));
        }
    }
}
