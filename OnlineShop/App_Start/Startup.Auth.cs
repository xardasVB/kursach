﻿using Autofac;
using Autofac.Integration.Owin;
using Autofac.Integration.Mvc;
using BLL.Models;
using BLL.Services;
using BLL.Services.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.Google;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

[assembly: OwinStartup(typeof(OnlineShop.Startup))]

namespace OnlineShop
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit https://go.microsoft.com/fwlink/?LinkId=301864
        public void Configuration(IAppBuilder app)
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new DataModule("OnlineShopDB", app));
            // REGISTER CONTROLLERS SO DEPENDENCIES ARE CONSTRUCTOR INJECTED

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // BUILD THE CONTAINER
            var container = builder.Build();

            // REPLACE THE MVC DEPENDENCY RESOLVER WITH AUTOFAC
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            // REGISTER WITH OWIN
            app.UseAutofacMiddleware(container);
            app.UseAutofacMvc();

            //Configuration(app);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(MyOptions.OptionCookies());
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Enables the application to remember the second login verification factor such as phone or email.
            // Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
            // This is similar to the RememberMe option when you log in.
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");
            
            var facebookOptions = new FacebookAuthenticationOptions
            {
                AppId = "1686325828115193",
                AppSecret = "2a53ed8bd5a2f23446fbb08434e7f547"
            };

            facebookOptions.Scope.Add("email");
            facebookOptions.Fields.Add("name");
            facebookOptions.Fields.Add("email");

            app.UseFacebookAuthentication(facebookOptions);

            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = "667803798618-2a0pns2qjj8ocm4kqg1jampadddk0bdk.apps.googleusercontent.com",
                ClientSecret = "y2WDSRN3YJCaJt_u41FLN0mk"
            });
        }
    }
}