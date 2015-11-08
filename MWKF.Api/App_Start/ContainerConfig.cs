using System;
using AUSKF.Api.Data;
using AUSKF.Api.Entities;
using AUSKF.Api.Entities.Identity;
using AUSKF.Api.Interfaces;
using AUSKF.Api.Providers;
using AUSKF.Api.Providers.Identity;
using AUSKF.Api.Providers.Interfaces;
using AUSKF.Api.Repositories;
using AUSKF.Api.Repositories.Interfaces;
using AUSKF.Api.Services;
using AUSKF.Api.Services.Interfaces;
using Castle.Core;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace AUSKF.Api
{
    public class ContainerConfig
    {
        public static void RegisterComponents()
        {
            // this will register all of our controllers
            var assemblyDiscoveryService = Ioc.Instance.Resolve<IAssemblyDiscoveryService>();
            assemblyDiscoveryService.GenerateDependencyList();
            var controllerRegistrationService = Ioc.Instance.Resolve<IControllerRegistrationService>();
            controllerRegistrationService.RegisterControllers();

            // Data
            Ioc.Instance.AddComponentWithLifestyle("IDataContext", typeof(IDataContext), typeof(DataContext), LifestyleType.PerWebRequest);

            // Repositories
            RegisterRepositories();

            // Identity
            //Ioc.Instance.AddComponentWithLifestyle("GroupManager", typeof(IGroupManager), typeof(ApplicationRoleManager), LifestyleType.PerWebRequest);
            Ioc.Instance.AddComponentWithLifestyle("ApplicationSignInManager", typeof(IApplicationSignInManager), typeof(ApplicationSignInManager), LifestyleType.PerWebRequest);
            Ioc.Instance.AddComponentWithLifestyle("ApplicationUserManager", typeof(IApplicationUserManager), typeof(ApplicationUserManager), LifestyleType.PerWebRequest);
            Ioc.Instance.AddComponentWithLifestyle("AuthenticationManager", typeof(IAuthenticationManager), typeof(ApplicationAuthenticationManager), LifestyleType.PerWebRequest);
            Ioc.Instance.AddComponentWithLifestyle("IUserStore", typeof(IUserStore<User, Guid>), typeof(UserStoreProvider<User, Guid>), LifestyleType.Transient);
            Ioc.Instance.AddComponentWithLifestyle("ClaimsIdentityFactory", typeof(IClaimsIdentityFactory<User, Guid>), typeof(ApplicationClaimsIdentityFactory), LifestyleType.PerWebRequest);


        }


        // TODO auto repository registration
        private static void RegisterRepositories()
        {
            Ioc.Instance.AddComponentWithLifestyle("IUserRepository", typeof(IEntityRepository<User, Guid>), typeof(EntityRepository<User, Guid>), LifestyleType.PerWebRequest);
            Ioc.Instance.AddComponentWithLifestyle("IUserClaimRepository", typeof(IEntityRepository<UserClaim, Guid>), typeof(EntityRepository<UserClaim, Guid>), LifestyleType.PerWebRequest);
            Ioc.Instance.AddComponentWithLifestyle("IUserLoginRepository", typeof(IEntityRepository<UserLogin, Guid>), typeof(EntityRepository<UserLogin, Guid>), LifestyleType.PerWebRequest);
            Ioc.Instance.AddComponentWithLifestyle("IUserRoleRepository", typeof(IEntityRepository<UserRole, Guid>), typeof(EntityRepository<UserRole, Guid>), LifestyleType.PerWebRequest);
            Ioc.Instance.AddComponentWithLifestyle("ILogRepository", typeof(IEntityRepository<Log, Guid>), typeof(EntityRepository<Log, Guid>), LifestyleType.PerWebRequest);
            Ioc.Instance.AddComponentWithLifestyle("IKendoRankRepository", typeof(IEntityRepository<KendoRank, Guid>), typeof(EntityRepository<KendoRank, Guid>), LifestyleType.PerWebRequest);
            Ioc.Instance.AddComponentWithLifestyle("IDojoRepository", typeof(IEntityRepository<Dojo, Guid>), typeof(EntityRepository<Dojo, Guid>), LifestyleType.PerWebRequest);
            Ioc.Instance.AddComponentWithLifestyle("IFederationRepositor", typeof(IEntityRepository<Federation, Guid>), typeof(EntityRepository<Federation, Guid>), LifestyleType.PerWebRequest);

        }
    }
}