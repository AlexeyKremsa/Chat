using System;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using Chat.Domain.Repositories.Implementations;
using Chat.Domain.Repositories.Interfaces;
using Chat.Services.Security;
using Chat.Web.Controllers;
using Chat.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Practices.Unity;

namespace Chat.Web.App_Start
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterService(container);
            RegisterDomain(container);
            RegisterMapper(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        private static void RegisterService(IUnityContainer container)
        {
            container.RegisterType<DbContext, ApplicationDbContext>(new HierarchicalLifetimeManager());
            container.RegisterType<UserManager<ApplicationUser>>(new HierarchicalLifetimeManager());
            container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>(new HierarchicalLifetimeManager());

            container.RegisterType<AccountController>(new InjectionConstructor());
            container.RegisterType<IPasswordHelper, PasswordHelper>();
        }


        private static void RegisterDomain(IUnityContainer container)
        {
            container.RegisterType<IUserRepository, UserRepository>();
        }

        private static void RegisterMapper(IUnityContainer container)
        {
            var profiles =
                typeof(UnityConfig).Assembly
                .GetTypes()
                .Where(x => typeof(Profile).IsAssignableFrom(x))
                .Select(x => (Profile)Activator.CreateInstance(x))
                .ToArray();

            var config = new MapperConfiguration(cfg =>
            {
                foreach (var profile in profiles)
                {
                    cfg.AddProfile(profile);
                }
            });

            container.RegisterInstance(config);
            container.RegisterInstance(config.CreateMapper());
        }
    }
}
