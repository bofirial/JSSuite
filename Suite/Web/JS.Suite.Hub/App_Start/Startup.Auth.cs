using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.Twitter;
using Owin;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.Owin;
using System;
using JS.Suite.BusinessLogic.JSSuite;
using JS.Suite.DataAbstraction.JSSuite;
using JS.Core.Foundation.ExtensionMethods;
using JS.Suite.BusinessLogic.Web.BaseClasses;

namespace JS.Suite.Hub
{
    /// <summary>
    /// Startup
    /// </summary>
    public partial class Startup : StartupBase
    {
        /// <summary>
        /// Configures the authentication.
        /// </summary>
        /// <param name="app">The application.</param>
        public void ConfigureAuth(IAppBuilder app)
        {
            // Enable the application to use a cookie to store information for the signed in user
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<UserManager<JSUser, int>, JSUser, int>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentityCallback: (manager, user) => JSUserBusinessManager.Current.UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie),
                        getUserIdCallback: (identity) => identity.GetUserId<int>())
                }
            });
            // Use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            app.UseFacebookAuthentication(new FacebookAuthenticationOptions()
            {
                AppId = "147066325435182",
                AppSecret = "c2539fcd1d6b593ad76426d8bd4111d7",
                //Scope = new List() {"email", "user_birthday"},
                SignInAsAuthenticationType = app.GetDefaultSignInAsAuthenticationType()
            });

            app.UseTwitterAuthentication(new TwitterAuthenticationOptions()
            {
                ConsumerKey = "0P7e7ZPdYauA1pQdeQHkomfy7",
                ConsumerSecret = "DTXCCC25xAcAqM3cn14AXqc6zeFira4gEopt2hMALQXJpAdOov",
                SignInAsAuthenticationType = app.GetDefaultSignInAsAuthenticationType()
            });

            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = "1089135435768.apps.googleusercontent.com",
                ClientSecret = "SPh1akml61UxCKtwEkCywTNR",
                //Scope = new List() {"email", "user_birthday"},
                SignInAsAuthenticationType = app.GetDefaultSignInAsAuthenticationType()
            });
        }
    }
}