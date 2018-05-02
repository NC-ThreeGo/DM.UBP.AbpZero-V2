using System;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Abp.Extensions;
using Abp.Hangfire;
using Abp.Owin;
using Abp.Runtime.Security;
using Hangfire;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.OpenIdConnect;
using Microsoft.Owin.Security.Twitter;
using Microsoft.Owin.Security.WsFederation;
using DM.UBP.Web;
using DM.UBP.Web.Auth;
using DM.UBP.WebApi.Controllers;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace DM.UBP.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseAbp();

            app.RegisterDataProtectionProvider();

            app.UseOAuthBearerAuthentication(AccountController.OAuthBearerOptions);

            //使应用程序可以使用 Cookie 来存储已登录用户的信息
            //UseCookieAuthentication方法告诉ASP.NET Identity如何用Cookie去标识已认证的用户，
            //  以及通过CookieAuthenticationOptions类指定的选项在哪儿。
            //  这里重要的部分是LoginPath属性，它指定了一个URL，这是未认证客户端请求内容时要重定向的地址。
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login")
            });

            //Use a cookie to temporarily store information about a user logging in with a third party login provider
            //使应用程序可以使用cookie方式来存储第三方认证的信息
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            //土牛：You can remove these lines if you don't like to use two factor auth (while it has no problem if you don't remove)
            //使应用程序能够临时存储时，他们正在核实在双因素身份验证过程的第二个因素用户信息。
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));
            //使应用程序能够记住第二次登录验证的因素，如电话或电子邮件。
            //一旦选中此选项，在登录过程中验证的第二个步骤将是你从登录的设备记住。
            //这是类似的，当你登录了rememberMe选项。
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            if (IsTrue("ExternalAuth.Facebook.IsEnabled"))
            {
                app.UseFacebookAuthentication(CreateFacebookAuthOptions());
            }

            if (IsTrue("ExternalAuth.Twitter.IsEnabled"))
            {
                app.UseTwitterAuthentication(CreateTwitterAuthOptions());
            }

            if (IsTrue("ExternalAuth.Google.IsEnabled"))
            {
                app.UseGoogleAuthentication(CreateGoogleAuthOptions());
            }

            if (IsTrue("ExternalAuth.WsFederation.IsEnabled"))
            {
                app.UseWsFederationAuthentication(CreateWsFederationAuthOptions());
            }

            if (IsTrue("ExternalAuth.OpenId.IsEnabled"))
            {
                app.UseOpenIdConnectAuthentication(CreateOpenIdOptions());
            }

            app.MapSignalR();

            //app.QuartzServerStartUp();

            //Enable it to use HangFire dashboard (uncomment only if it's enabled in UBPWebModule)
            //app.UseHangfireDashboard("/hangfire", new DashboardOptions
            //{
            //    Authorization = new[] { new AbpHangfireAuthorizationFilter(AppPermissions.Pages_Administration_HangfireDashboard) }
            //});

            //开启Hangfire服务，UBPHangfireModule中配置SQL链接
            //app.UseHangfireServer();
            //app.UseHangfireDashboard();
            //app.UseHangfireDashboard("/hangfire", new DashboardOptions
            //{
            //    Authorization = new[] { new AbpHangfireAuthorizationFilter() }
            //});
        }

        private static OpenIdConnectAuthenticationOptions CreateOpenIdOptions()
        {
            var options = new OpenIdConnectAuthenticationOptions
            {
                Authority = ConfigurationManager.AppSettings["ExternalAuth.OpenId.Authority"],
                ClientId = ConfigurationManager.AppSettings["ExternalAuth.OpenId.ClientId"],
                PostLogoutRedirectUri = WebUrlService.WebSiteRootAddress + "Account/Logout",
                Notifications = new OpenIdConnectAuthenticationNotifications
                {
                    SecurityTokenValidated = notification =>
                    {
                        var email = notification.AuthenticationTicket.Identity.Name;
                        notification.AuthenticationTicket.Identity.AddClaim(new Claim(ClaimTypes.Email, email));
                        return Task.FromResult(0);
                    }
                }
            };

            var clientSecret = ConfigurationManager.AppSettings["ExternalAuth.OpenId.ClientSecret"];
            if (!clientSecret.IsNullOrEmpty())
            {
                options.ClientSecret = clientSecret;
            }

            return options;
        }

        private static FacebookAuthenticationOptions CreateFacebookAuthOptions()
        {
            var options = new FacebookAuthenticationOptions
            {
                AppId = ConfigurationManager.AppSettings["ExternalAuth.Facebook.AppId"],
                AppSecret = ConfigurationManager.AppSettings["ExternalAuth.Facebook.AppSecret"]
            };

            options.Scope.Add("email");
            options.Scope.Add("public_profile");

            return options;
        }

        private static TwitterAuthenticationOptions CreateTwitterAuthOptions()
        {
            var consumerKey = ConfigurationManager.AppSettings["ExternalAuth.Twitter.ConsumerKey"];
            var consumerSecret = ConfigurationManager.AppSettings["ExternalAuth.Twitter.ConsumerSecret"];

            return new TwitterAuthenticationOptions
            {
                ConsumerKey = consumerKey,
                ConsumerSecret = consumerSecret,
                Provider = new TwitterAuthenticationProvider
                {
                    OnAuthenticated = (context) =>
                    {
                        context.Identity.AddClaim(new Claim("urn:twitter:access_token", context.AccessToken));
                        context.Identity.AddClaim(new Claim("urn:twitter:access_secret", context.AccessTokenSecret));

                        var emailRetriever = new TwitterEmailRetriever();
                        var email = emailRetriever.Get(context.AccessToken, context.AccessTokenSecret, consumerKey, consumerSecret);
                        context.Identity.AddClaim(new Claim(ClaimTypes.Email, email));

                        return Task.FromResult(0);
                    }
                },
                BackchannelCertificateValidator = new Microsoft.Owin.Security.CertificateSubjectKeyIdentifierValidator(new[]
                {
                    "A5EF0B11CEC04103A34A659048B21CE0572D7D47", // VeriSign Class 3 Secure Server CA - G2
                    "0D445C165344C1827E1D20AB25F40163D8BE79A5", // VeriSign Class 3 Secure Server CA - G3
                    "7FD365A7C2DDECBBF03009F34339FA02AF333133", // VeriSign Class 3 Public Primary Certification Authority - G5
                    "39A55D933676616E73A761DFA16A7E59CDE66FAD", // Symantec Class 3 Secure Server CA - G4
                    "‎add53f6680fe66e383cbac3e60922e3b4c412bed", // Symantec Class 3 EV SSL CA - G3
                    "4eb6d578499b1ccf5f581ead56be3d9b6744a5e5", // VeriSign Class 3 Primary CA - G5
                    "5168FF90AF0207753CCCD9656462A212B859723B", // DigiCert SHA2 High Assurance Server C‎A 
                    "B13EC36903F8BF4701D498261A0802EF63642BC3" // DigiCert High Assurance EV Root CA
                })
            };
        }

        private static GoogleOAuth2AuthenticationOptions CreateGoogleAuthOptions()
        {
            return new GoogleOAuth2AuthenticationOptions
            {
                ClientId = ConfigurationManager.AppSettings["ExternalAuth.Google.ClientId"],
                ClientSecret = ConfigurationManager.AppSettings["ExternalAuth.Google.ClientSecret"]
            };
        }

        private static WsFederationAuthenticationOptions CreateWsFederationAuthOptions()
        {
            var wtrealm = ConfigurationManager.AppSettings["ExternalAuth.WsFederation.Wtrealm"];
            var metaDataAddress = ConfigurationManager.AppSettings["ExternalAuth.WsFederation.MetaDataAddress"];

            return new WsFederationAuthenticationOptions
            {
                Wtrealm = wtrealm,
                MetadataAddress = metaDataAddress,
                AuthenticationType = "adfs",
                Notifications = new WsFederationAuthenticationNotifications
                {
                    RedirectToIdentityProvider = notification =>
                    {
                        if (notification.ProtocolMessage.IsSignOutMessage)
                        {
                            notification.HandleResponse();
                        }

                        notification.ProtocolMessage.Wreply = WebUrlService.WebSiteRootAddress + "Account/ExternalLoginCallback";

                        return Task.FromResult(0);
                    },
                    SecurityTokenValidated = notification =>
                    {
                        var emailClaim = notification.AuthenticationTicket.Identity.Claims.ToList().FirstOrDefault(c => c.Type == ClaimTypes.Email);

                        if (emailClaim != null)
                        {
                            notification.AuthenticationTicket.Identity.AddClaim(new Claim(ClaimTypes.Email, emailClaim.Value));
                        }

                        return Task.FromResult(0);
                    }
                }
            };
        }

        private static bool IsTrue(string appSettingName)
        {
            return string.Equals(
                ConfigurationManager.AppSettings[appSettingName],
                "true",
                StringComparison.InvariantCultureIgnoreCase);
        }
    }
}