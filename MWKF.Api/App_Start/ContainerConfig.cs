namespace MWKF.Api
{
    using System;
    using Castle.Core;
    using Microsoft.AspNet.Identity;
    using Microsoft.Owin.Security;
    using MWKF.Api.Data;
    using MWKF.Api.Entities;
    using MWKF.Api.Entities.Identity;
    using MWKF.Api.Interfaces;
    using MWKF.Api.Providers;
    using MWKF.Api.Providers.Identity;
    using MWKF.Api.Providers.Interfaces;
    using MWKF.Api.Repositories;
    using MWKF.Api.Repositories.Interfaces;
    using MWKF.Api.Services;

    public class ContainerConfig
    {
        public static void RegisterComponents()
        {
            // Data
            Ioc.Instance.AddComponentWithLifestyle("IDataContext", typeof(IDataContext), typeof(DataContext), LifestyleType.PerWebRequest);

            // Repositories
            Ioc.Instance.AddComponentWithLifestyle("IUserRepository", typeof(IEntityRepository<User, Guid>), typeof(EntityRepository<User, Guid>), LifestyleType.PerWebRequest);
            Ioc.Instance.AddComponentWithLifestyle("IUserClaimRepository", typeof(IEntityRepository<UserClaim, Guid>), typeof(EntityRepository<UserClaim, Guid>), LifestyleType.PerWebRequest);
            Ioc.Instance.AddComponentWithLifestyle("IUserLoginRepository", typeof(IEntityRepository<UserLogin, Guid>), typeof(EntityRepository<UserLogin, Guid>), LifestyleType.PerWebRequest);
            Ioc.Instance.AddComponentWithLifestyle("ILogRepository", typeof(IEntityRepository<Log, Guid>), typeof(EntityRepository<Log, Guid>), LifestyleType.PerWebRequest);
            // Identity
            //Ioc.Instance.AddComponentWithLifestyle("GroupManager", typeof(IGroupManager), typeof(ApplicationRoleManager), LifestyleType.PerWebRequest);
            Ioc.Instance.AddComponentWithLifestyle("ApplicationSignInManager", typeof(IApplicationSignInManager), typeof(ApplicationSignInManager), LifestyleType.PerWebRequest);
            Ioc.Instance.AddComponentWithLifestyle("ApplicationUserManager", typeof(IApplicationUserManager), typeof(ApplicationUserManager), LifestyleType.PerWebRequest);
            Ioc.Instance.AddComponentWithLifestyle("AuthenticationManager", typeof(IAuthenticationManager), typeof(ApplicationAuthenticationManager), LifestyleType.PerWebRequest);
            Ioc.Instance.AddComponentWithLifestyle("IUserStore", typeof(IUserStore<User, Guid>), typeof(UserStoreProvider<User, Guid>), LifestyleType.Transient);
            Ioc.Instance.AddComponentWithLifestyle("ClaimsIdentityFactory", typeof(IClaimsIdentityFactory<User, Guid>), typeof(ApplicationClaimsIdentityFactory), LifestyleType.PerWebRequest);


        }
    }
}