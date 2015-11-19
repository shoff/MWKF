using System;
using System.Globalization;
using System.Security.Claims;
using System.Security.Principal;
using System.Web.Http;
using AUSKF.Api.Data;
using AUSKF.Api.Entities.Identity;
using AUSKF.Api.Providers.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

namespace AUSKF.Api
{
    public static class IdentityExtensions
    {
        public static T GetUserId<T>(this IIdentity identity) 
        {
            if (identity == null)
            {
                throw new ArgumentNullException(nameof(identity));
            }
            ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
            string text = claimsIdentity?.FindFirstValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
            if (text != null)
            {
                return (T)((object)Convert.ChangeType(text, typeof(T), CultureInfo.InvariantCulture));
            }
            return default(T);
        }
    }

    public partial class Startup
    {

        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {

            app.UseWebApi(GlobalConfiguration.Configuration);
            // Configure the db context and user manager to use a single instance per request
            app.CreatePerOwinContext(DataContext.Create);
            
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            //app.CreatePerOwinContext<ApplicationRoleManager>(ApplicationRoleManager.Create);
            //IAuthenticationManager authenticationManager = Ioc.Instance.Resolve<IAuthenticationManager>();

            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),

                Provider = new CookieAuthenticationProvider
                {
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, User, Guid>
                    (TimeSpan.FromMinutes(30), (manager, user) => user.GenerateUserIdentityAsync(manager),ident => ident.GetUserId<Guid>()),

                    //**** This what I did ***//
                    OnException = context =>
                    {
                        Console.WriteLine(context.Exception.Message);
                        throw context.Exception;
                    }
                }
            });

            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");
           // app.UseGitHubAuthentication("1bfcefabf8915347782a", "db683ecf2f28f09f65432e9cff972673d9d3a522");

            //app.UseRedditAuthentication("t3xal_3BqrnFEg", "FcyBUZI88TkQe_ydgvy5fU1J3a8");

            app.UseFacebookAuthentication("1627454327496577", "1423363bfda5af73029608170b7b0e34");

            app.UseGoogleAuthentication("136994307495-35s7hbhm9jkn43mis7dnqnjl3ooapflu.apps.googleusercontent.com", "wZvoCl-d_AQ2MpzGd-3fxmCY");
        }
    }
}
