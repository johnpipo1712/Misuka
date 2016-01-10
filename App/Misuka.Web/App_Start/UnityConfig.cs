using System;
using Microsoft.Practices.Unity;
using Misuka.Domain.Context;
using Misuka.Infrastructure.Data.ADO;
using Misuka.Infrastructure.EntityFramework.DContext;
using Misuka.Infrastructure.EntityFramework.Factories;
using Misuka.Infrastructure.EntityFramework.UnitOfWork;
using Misuka.Services;

namespace Misuka.Web.App_Start
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container
        private static readonly Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
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

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
         // container
         //   .RegisterType<IDataContextAsync, MisukaDBContext>(new PerRequestLifetimeManager())
         //   .RegisterType<IRepositoryProvider, RepositoryProvider>(
         //     new PerRequestLifetimeManager(),
         //     new InjectionConstructor(new object[] {new RepositoryFactories()})
         //   )
         //   .RegisterType<IUnitOfWorkAsync,UnitOfWork>(new PerRequestLifetimeManager());
         // new ServiceRegister(container).Register();
         //var ado = new ADOHelper();
         //ADO.AdoHelper = ado;
        }
    }
}
