﻿using System;
using System.Data.Entity.Utilities;
using System.Security.Claims;
using System.Threading.Tasks;
using AUSKF.Api.Entities.Identity;
using AUSKF.Api.Providers.Interfaces;
using AUSKF.Api.Repositories.Interfaces;
using AUSKF.Api.Services;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;

namespace AUSKF.Api.Providers.Identity
{
    /// <summary>
    /// </summary>
    public sealed class ApplicationSignInManager : SignInManager<User, Guid>, IApplicationSignInManager
    {
        private readonly IEntityRepository<User, Guid> userRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationSignInManager" /> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="authenticationManager">The authentication manager.</param>
        /// <param name="userRepository">The user repository.</param>
        public ApplicationSignInManager(IApplicationUserManager userManager,
            IAuthenticationManager authenticationManager, IEntityRepository<User, Guid> userRepository)
            : base((ApplicationUserManager)userManager, authenticationManager)
        {
            this.userRepository = userRepository;
        }

        /// <summary>
        /// Creates the user identity asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">user</exception>
        /// <exception cref="ArgumentNullException">The value of 'user' cannot be null.</exception>
        public override Task<ClaimsIdentity> CreateUserIdentityAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return user.GenerateUserIdentityAsync((ApplicationUserManager)this.UserManager);
        }

        /// <summary>
        /// Passwords the sign in asynchronous.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <param name="isPersistent">if set to <c>true</c> [is persistent].</param>
        /// <param name="shouldLockout">if set to <c>true</c> [should lockout].</param>
        /// <returns></returns>
        public override async Task<SignInStatus> PasswordSignInAsync(string userName, string password, bool isPersistent, bool shouldLockout)
        {
            SignInStatus result;
            if (this.UserManager == null)
            {
                result = SignInStatus.Failure;
            }
            else
            {
                User user = await this.UserManager.FindByNameAsync(userName).WithCurrentCulture();
                if (user == null)
                {
                    result = SignInStatus.Failure;
                }
                else if (await this.UserManager.IsLockedOutAsync(user.Id).WithCurrentCulture())
                {
                    result = SignInStatus.LockedOut;
                }
                else if (await this.UserManager.CheckPasswordAsync(user, password).WithCurrentCulture())
                {
                    await this.UserManager.ResetAccessFailedCountAsync(user.Id).WithCurrentCulture();
                    result = await this.SignInOrTwoFactor(user, isPersistent).WithCurrentCulture();
                }
                else
                {
                    if (shouldLockout)
                    {
                        await this.UserManager.AccessFailedAsync(user.Id).WithCurrentCulture();
                        if (await this.UserManager.IsLockedOutAsync(user.Id).WithCurrentCulture())
                        {
                            result = SignInStatus.LockedOut;
                            return result;
                        }
                    }
                    result = SignInStatus.Failure;
                }
            }
            return result;
        }

        private async Task<SignInStatus> SignInOrTwoFactor(User user, bool isPersistent)
        {
            string text = Convert.ToString(user.Id);
            SignInStatus result;
            if (await this.UserManager.GetTwoFactorEnabledAsync(user.Id).WithCurrentCulture()
                && (await this.UserManager.GetValidTwoFactorProvidersAsync(user.Id).WithCurrentCulture()).Count > 0
                && !(await this.AuthenticationManager.TwoFactorBrowserRememberedAsync(text).WithCurrentCulture()))
            {
                ClaimsIdentity claimsIdentity = new ClaimsIdentity("TwoFactorCookie");
                claimsIdentity.AddClaim(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", text));
                this.AuthenticationManager.SignIn(claimsIdentity);
                result = SignInStatus.RequiresVerification;
            }
            else
            {
                await this.SignInAsync(user, isPersistent, false).WithCurrentCulture();
                result = SignInStatus.Success;
            }
            return result;
        }

        /// <summary>
        /// Signs the in asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="isPersistent">if set to <c>true</c> [is persistent].</param>
        /// <param name="rememberBrowser">if set to <c>true</c> [remember browser].</param>
        /// <returns></returns>
        public override async Task SignInAsync(User user, bool isPersistent, bool rememberBrowser)
        {
            ClaimsIdentity claimsIdentity = await this.CreateUserIdentityAsync(user).WithCurrentCulture();
            this.AuthenticationManager.SignOut("ExternalCookie", "TwoFactorCookie");
            if (rememberBrowser)
            {
                ClaimsIdentity claimsIdentity2 = this.AuthenticationManager.CreateTwoFactorRememberBrowserIdentity(this.ConvertIdToString(user.Id));
                this.AuthenticationManager.SignIn(new AuthenticationProperties
                {
                    IsPersistent = isPersistent
                }, claimsIdentity, claimsIdentity2);
            }
            else
            {
                this.AuthenticationManager.SignIn(new AuthenticationProperties
                {
                    IsPersistent = isPersistent
                }, claimsIdentity);
            }
            user.LastLogin = DateTime.Now;
            this.userRepository.Update(user, user.Id);
        }

        /// <summary>
        /// Creates the specified options.
        /// </summary>
        /// <param name="options">
        /// The options.
        /// </param>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <returns>
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// The value of 'options' cannot be null. 
        /// </exception>
        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var applicationUserManager = context.GetUserManager<ApplicationUserManager>();
            // should we try to create it from our container if MS screws the pooch here?

            if (applicationUserManager == null)
            {
                applicationUserManager = ((ApplicationUserManager)Ioc.Instance.Resolve<IApplicationUserManager>());
            }

            var auth = context.Authentication;

            return new ApplicationSignInManager(applicationUserManager, auth, Ioc.Instance.Resolve<IEntityRepository<User, Guid>>());
        }
    }
}