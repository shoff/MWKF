using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AUSKF.Api.Entities.Identity;
using Microsoft.AspNet.Identity;

namespace AUSKF.Api.Providers.Interfaces
{
    /// <summary>
    /// </summary>
    public interface IApplicationUserManager
    {
        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        void Dispose();

        /// <summary>
        /// Creates the identity asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="authenticationType">Type of the authentication.</param>
        /// <returns></returns>
        Task<ClaimsIdentity> CreateIdentityAsync(User user, string authenticationType);

        /// <summary>
        /// Creates the asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        Task<IdentityResult> CreateAsync(User user);

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        Task<IdentityResult> UpdateAsync(User user);

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        Task<IdentityResult> DeleteAsync(User user);

        /// <summary>
        /// Finds the by identifier asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        Task<User> FindByIdAsync(Guid userId);

        /// <summary>
        /// Finds the by name asynchronous.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        Task<User> FindByNameAsync(string userName);

        /// <summary>
        /// Creates the asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        Task<IdentityResult> CreateAsync(User user, string password);

        /// <summary>
        /// Finds the asynchronous.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        Task<User> FindAsync(string userName, string password);

        /// <summary>
        /// Checks the password asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        Task<bool> CheckPasswordAsync(User user, string password);

        /// <summary>
        /// Determines whether [has password asynchronous] [the specified user identifier].
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        Task<bool> HasPasswordAsync(Guid userId);

        /// <summary>
        /// Adds the password asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        Task<IdentityResult> AddPasswordAsync(Guid userId, string password);

        /// <summary>
        /// Changes the password asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="currentPassword">The current password.</param>
        /// <param name="newPassword">The new password.</param>
        /// <returns></returns>
        Task<IdentityResult> ChangePasswordAsync(Guid userId, string currentPassword, string newPassword);

        /// <summary>
        /// Removes the password asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        Task<IdentityResult> RemovePasswordAsync(Guid userId);

        /// <summary>
        /// Gets the security stamp asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        Task<string> GetSecurityStampAsync(Guid userId);

        /// <summary>
        /// Updates the security stamp asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        Task<IdentityResult> UpdateSecurityStampAsync(Guid userId);

        /// <summary>
        /// Generates the password reset token asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        Task<string> GeneratePasswordResetTokenAsync(Guid userId);

        /// <summary>
        /// Resets the password asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="token">The token.</param>
        /// <param name="newPassword">The new password.</param>
        /// <returns></returns>
        Task<IdentityResult> ResetPasswordAsync(Guid userId, string token, string newPassword);

        /// <summary>
        /// Finds the asynchronous.
        /// </summary>
        /// <param name="login">The login.</param>
        /// <returns></returns>
        Task<User> FindAsync(UserLoginInfo login);

        /// <summary>
        /// Removes the login asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="login">The login.</param>
        /// <returns></returns>
        Task<IdentityResult> RemoveLoginAsync(Guid userId, UserLoginInfo login);

        /// <summary>
        /// Adds the login asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="login">The login.</param>
        /// <returns></returns>
        Task<IdentityResult> AddLoginAsync(Guid userId, UserLoginInfo login);

        /// <summary>
        /// Gets the logins asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        Task<IList<UserLoginInfo>> GetLoginsAsync(Guid userId);

        /// <summary>
        /// Adds the claim asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="claim">The claim.</param>
        /// <returns></returns>
        Task<IdentityResult> AddClaimAsync(Guid userId, Claim claim);

        /// <summary>
        /// Removes the claim asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="claim">The claim.</param>
        /// <returns></returns>
        Task<IdentityResult> RemoveClaimAsync(Guid userId, Claim claim);

        /// <summary>
        /// Gets the claims asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        Task<IList<Claim>> GetClaimsAsync(Guid userId);

        /// <summary>
        /// Adds to role asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="role">The role.</param>
        /// <returns></returns>
        Task<IdentityResult> AddToRoleAsync(Guid userId, string role);

        /// <summary>
        /// Adds to roles asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="roles">The roles.</param>
        /// <returns></returns>
        Task<IdentityResult> AddToRolesAsync(Guid userId, params string[] roles);

        /// <summary>
        /// Removes from roles asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="roles">The roles.</param>
        /// <returns></returns>
        Task<IdentityResult> RemoveFromRolesAsync(Guid userId, params string[] roles);

        /// <summary>
        /// Removes from role asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="role">The role.</param>
        /// <returns></returns>
        Task<IdentityResult> RemoveFromRoleAsync(Guid userId, string role);

        /// <summary>
        /// Gets the roles asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        Task<IList<string>> GetRolesAsync(Guid userId);

        /// <summary>
        /// Determines whether [is in role asynchronous] [the specified user identifier].
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="role">The role.</param>
        /// <returns></returns>
        Task<bool> IsInRoleAsync(Guid userId, string role);

        /// <summary>
        /// Gets the email asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        Task<string> GetEmailAsync(Guid userId);

        /// <summary>
        /// Sets the email asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        Task<IdentityResult> SetEmailAsync(Guid userId, string email);

        /// <summary>
        /// Finds the by email asynchronous.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        Task<User> FindByEmailAsync(string email);

        /// <summary>
        /// Generates the email confirmation token asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        Task<string> GenerateEmailConfirmationTokenAsync(Guid userId);

        /// <summary>
        /// Confirms the email asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        Task<IdentityResult> ConfirmEmailAsync(Guid userId, string token);

        /// <summary>
        /// Determines whether [is email confirmed asynchronous] [the specified user identifier].
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        Task<bool> IsEmailConfirmedAsync(Guid userId);

        /// <summary>
        /// Gets the phone number asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        Task<string> GetPhoneNumberAsync(Guid userId);

        /// <summary>
        /// Sets the phone number asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="phoneNumber">The phone number.</param>
        /// <returns></returns>
        Task<IdentityResult> SetPhoneNumberAsync(Guid userId, string phoneNumber);

        /// <summary>
        /// Changes the phone number asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="phoneNumber">The phone number.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        Task<IdentityResult> ChangePhoneNumberAsync(Guid userId, string phoneNumber, string token);

        /// <summary>
        /// Determines whether [is phone number confirmed asynchronous] [the specified user identifier].
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        Task<bool> IsPhoneNumberConfirmedAsync(Guid userId);

        /// <summary>
        /// Generates the change phone number token asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="phoneNumber">The phone number.</param>
        /// <returns></returns>
        Task<string> GenerateChangePhoneNumberTokenAsync(Guid userId, string phoneNumber);

        /// <summary>
        /// Verifies the change phone number token asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="token">The token.</param>
        /// <param name="phoneNumber">The phone number.</param>
        /// <returns></returns>
        Task<bool> VerifyChangePhoneNumberTokenAsync(Guid userId, string token, string phoneNumber);

        /// <summary>
        /// Verifies the user token asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="purpose">The purpose.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        Task<bool> VerifyUserTokenAsync(Guid userId, string purpose, string token);

        /// <summary>
        /// Generates the user token asynchronous.
        /// </summary>
        /// <param name="purpose">The purpose.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        Task<string> GenerateUserTokenAsync(string purpose, Guid userId);

        /// <summary>
        /// Registers the two factor provider.
        /// </summary>
        /// <param name="twoFactorProvider">The two factor provider.</param>
        /// <param name="provider">The provider.</param>
        void RegisterTwoFactorProvider(string twoFactorProvider, IUserTokenProvider<User, Guid> provider);

        /// <summary>
        /// Gets the valid two factor providers asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        Task<IList<string>> GetValidTwoFactorProvidersAsync(Guid userId);

        /// <summary>
        /// Verifies the two factor token asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="twoFactorProvider">The two factor provider.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        Task<bool> VerifyTwoFactorTokenAsync(Guid userId, string twoFactorProvider, string token);

        /// <summary>
        /// Generates the two factor token asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="twoFactorProvider">The two factor provider.</param>
        /// <returns></returns>
        Task<string> GenerateTwoFactorTokenAsync(Guid userId, string twoFactorProvider);

        /// <summary>
        /// Notifies the two factor token asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="twoFactorProvider">The two factor provider.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        Task<IdentityResult> NotifyTwoFactorTokenAsync(Guid userId, string twoFactorProvider, string token);

        /// <summary>
        /// Gets the two factor enabled asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        Task<bool> GetTwoFactorEnabledAsync(Guid userId);

        /// <summary>
        /// Sets the two factor enabled asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="enabled">if set to <c>true</c> [enabled].</param>
        /// <returns></returns>
        Task<IdentityResult> SetTwoFactorEnabledAsync(Guid userId, bool enabled);

        /// <summary>
        /// Sends the email asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The body.</param>
        /// <returns></returns>
        Task SendEmailAsync(Guid userId, string subject, string body);

        /// <summary>
        /// Sends the SMS asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        Task SendSmsAsync(Guid userId, string message);

        /// <summary>
        /// Determines whether [is locked out asynchronous] [the specified user identifier].
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        Task<bool> IsLockedOutAsync(Guid userId);

        /// <summary>
        /// Sets the lockout enabled asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="enabled">if set to <c>true</c> [enabled].</param>
        /// <returns></returns>
        Task<IdentityResult> SetLockoutEnabledAsync(Guid userId, bool enabled);

        /// <summary>
        /// Gets the lockout enabled asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        Task<bool> GetLockoutEnabledAsync(Guid userId);

        /// <summary>
        /// Gets the lockout end date asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        Task<DateTimeOffset> GetLockoutEndDateAsync(Guid userId);

        /// <summary>
        /// Sets the lockout end date asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="lockoutEnd">The lockout end.</param>
        /// <returns></returns>
        Task<IdentityResult> SetLockoutEndDateAsync(Guid userId, DateTimeOffset lockoutEnd);

        /// <summary>
        /// Accesses the failed asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        Task<IdentityResult> AccessFailedAsync(Guid userId);

        /// <summary>
        /// Resets the access failed count asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        Task<IdentityResult> ResetAccessFailedCountAsync(Guid userId);

        /// <summary>
        /// Gets the access failed count asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        Task<int> GetAccessFailedCountAsync(Guid userId);

        /// <summary>
        /// Gets or sets the password hasher.
        /// </summary>
        /// <value>
        /// The password hasher.
        /// </value>
        IPasswordHasher PasswordHasher { get; set; }
        /// <summary>
        /// Gets or sets the user validator.
        /// </summary>
        /// <value>
        /// The user validator.
        /// </value>
        IIdentityValidator<User> UserValidator { get; set; }
        /// <summary>
        /// Gets or sets the password validator.
        /// </summary>
        /// <value>
        /// The password validator.
        /// </value>
        IIdentityValidator<string> PasswordValidator { get; set; }
        /// <summary>
        /// Gets or sets the claims identity factory.
        /// </summary>
        /// <value>
        /// The claims identity factory.
        /// </value>
        IClaimsIdentityFactory<User, Guid> ClaimsIdentityFactory { get; set; }
        /// <summary>
        /// Gets or sets the email service.
        /// </summary>
        /// <value>
        /// The email service.
        /// </value>
        IIdentityMessageService EmailService { get; set; }
        /// <summary>
        /// Gets or sets the SMS service.
        /// </summary>
        /// <value>
        /// The SMS service.
        /// </value>
        IIdentityMessageService SmsService { get; set; }
        /// <summary>
        /// Gets or sets the user token provider.
        /// </summary>
        /// <value>
        /// The user token provider.
        /// </value>
        IUserTokenProvider<User, Guid> UserTokenProvider { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [user lockout enabled by default].
        /// </summary>
        /// <value>
        /// <c>true</c> if [user lockout enabled by default]; otherwise, <c>false</c>.
        /// </value>
        bool UserLockoutEnabledByDefault { get; set; }
        /// <summary>
        /// Gets or sets the maximum failed access attempts before lockout.
        /// </summary>
        /// <value>
        /// The maximum failed access attempts before lockout.
        /// </value>
        int MaxFailedAccessAttemptsBeforeLockout { get; set; }
        /// <summary>
        /// Gets or sets the default account lockout time span.
        /// </summary>
        /// <value>
        /// The default account lockout time span.
        /// </value>
        TimeSpan DefaultAccountLockoutTimeSpan { get; set; }
        /// <summary>
        /// Gets a value indicating whether [supports user two factor].
        /// </summary>
        /// <value>
        /// <c>true</c> if [supports user two factor]; otherwise, <c>false</c>.
        /// </value>
        bool SupportsUserTwoFactor { get; }
        /// <summary>
        /// Gets a value indicating whether [supports user password].
        /// </summary>
        /// <value>
        /// <c>true</c> if [supports user password]; otherwise, <c>false</c>.
        /// </value>
        bool SupportsUserPassword { get; }
        /// <summary>
        /// Gets a value indicating whether [supports user security stamp].
        /// </summary>
        /// <value>
        /// <c>true</c> if [supports user security stamp]; otherwise, <c>false</c>.
        /// </value>
        bool SupportsUserSecurityStamp { get; }
        /// <summary>
        /// Gets a value indicating whether [supports user role].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [supports user role]; otherwise, <c>false</c>.
        /// </value>
        bool SupportsUserRole { get; }
        /// <summary>
        /// Gets a value indicating whether [supports user login].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [supports user login]; otherwise, <c>false</c>.
        /// </value>
        bool SupportsUserLogin { get; }
        /// <summary>
        /// Gets a value indicating whether [supports user email].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [supports user email]; otherwise, <c>false</c>.
        /// </value>
        bool SupportsUserEmail { get; }
        /// <summary>
        /// Gets a value indicating whether [supports user phone number].
        /// </summary>
        /// <value>
        /// <c>true</c> if [supports user phone number]; otherwise, <c>false</c>.
        /// </value>
        bool SupportsUserPhoneNumber { get; }
        /// <summary>
        /// Gets a value indicating whether [supports user claim].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [supports user claim]; otherwise, <c>false</c>.
        /// </value>
        bool SupportsUserClaim { get; }
        /// <summary>
        /// Gets a value indicating whether [supports user lockout].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [supports user lockout]; otherwise, <c>false</c>.
        /// </value>
        bool SupportsUserLockout { get; }
        /// <summary>
        /// Gets a value indicating whether [supports queryable users].
        /// </summary>
        /// <value>
        /// <c>true</c> if [supports queryable users]; otherwise, <c>false</c>.
        /// </value>
        bool SupportsQueryableUsers { get; }
        /// <summary>
        /// Gets the users.
        /// </summary>
        /// <value>
        /// The users.
        /// </value>
        IQueryable<User> Users { get; }
        /// <summary>
        /// Gets the two factor providers.
        /// </summary>
        /// <value>
        /// The two factor providers.
        /// </value>
        IDictionary<string, IUserTokenProvider<User, Guid>> TwoFactorProviders { get; }
    }
}