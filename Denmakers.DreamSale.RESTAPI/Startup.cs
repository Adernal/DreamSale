using System;
using System.Web.Http;
using Autofac;
using Denmakers.DreamSale.RESTAPI.Infrastructure.OAuth;
using Denmakers.DreamSale.ViewModels.Mapper;

using Autofac.Integration.Owin;
using System.Web;
using Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.Twitter;

[assembly: OwinStartup(typeof(Denmakers.DreamSale.RESTAPI.Startup))]

namespace Denmakers.DreamSale.RESTAPI
{
    public class Startup
    {
        #region Fields
        private static IContainer _container;
        private HttpConfiguration _config = new HttpConfiguration();
        #endregion

        #region Properties
        public static OAuthBearerAuthenticationOptions OAuthBearerOptions { get; private set; }
        public static GoogleOAuth2AuthenticationOptions GoogleAuthOptions { get; private set; }
        public static FacebookAuthenticationOptions FacebookAuthOptions { get; private set; }
        public static TwitterAuthenticationOptions TwitterAuthOptions { get; private set; }
        #endregion

        #region Utilities
        public void ConfigureCors(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
            //enable cors origin requests
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
        }

        public void ConfigureWebAPIRouting(HttpConfiguration config)
        {
            // register api routing.
            WebApiConfig.Register(config);

            //System.Web.Mvc.AreaRegistration.RegisterAllAreas();

            //FilterConfig.RegisterGlobalFilters(System.Web.Mvc.GlobalFilters.Filters);
            //RouteConfig.RegisterRoutes(RouteTable.Routes);
            //BundleConfig.RegisterBundles(BundleTable.Bundles);
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            //GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            //GlobalConfiguration.Configuration.Formatters.Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);
        }

        public void ConfigureExternaleAuthentication(IAppBuilder app)
        {
            //Configure Google External Login
            GoogleAuthOptions = new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = "xxxxxx",
                ClientSecret = "xxxxxx",
                Provider = new GoogleAuthProvider()
            };
            app.UseGoogleAuthentication(GoogleAuthOptions);


            //Configure Facebook External Login
            FacebookAuthOptions = new FacebookAuthenticationOptions()
            {
                AppId = "xxxxxx",
                AppSecret = "xxxxxx",
                Provider = new FacebookAuthProvider()
            };
            app.UseFacebookAuthentication(FacebookAuthOptions);

            //Configure Microsoft Account External Login
            app.UseMicrosoftAccountAuthentication(
                clientId: "",
                clientSecret: "");

            //Configure Twitter External Login
            app.UseTwitterAuthentication(
               consumerKey: "",
               consumerSecret: "");

            //app.UseFacebookAuthentication(
            //   appId: "",
            //   appSecret: "");

            //app.UseGoogleAuthentication(
            //     clientId: "000-000.apps.googleusercontent.com",
            //     clientSecret: "00000000000");
        }

        public void ConfigureOAuth(HttpConfiguration config, IAppBuilder app)
        {
            //use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseExternalSignInCookie(Microsoft.AspNet.Identity.DefaultAuthenticationTypes.ExternalCookie);
            OAuthBearerOptions = new OAuthBearerAuthenticationOptions();

            var oAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(365 * 24),
                // RefreshTokenProvider = _tokenProvider,
                //Provider = _container.Resolve<IOAuthAuthorizationServerProvider>()
                Provider = new OwinAuthorizationServerProvider()
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(oAuthServerOptions);
            app.UseOAuthBearerAuthentication(OAuthBearerOptions);

            // Uncomment the following lines to enable logging in with third party login providers
            // ConfigureExternaleAuthentication(app);
        }

        public IContainer ConfigureAutofac(HttpConfiguration config, IAppBuilder app)
        {
            _container = AutofacWebapiConfig.Initialize(config);
            // Register the Autofac middleware FIRST. This also adds
            // Autofac-injected middleware registered with the container.
            app.UseAutofacMiddleware(_container);

            // http://stackoverflow.com/questions/37826551/authorization-with-asp-net-identity-autofac-owin-integration
            // https://github.com/autofac/Autofac.WebApi.Owin/issues/3
            // Configure Auth
            ConfigureOAuth(config, app);

            // To enable the lifetime scope created during the OWIN request to extend into the Web API dependency scope
            app.UseAutofacWebApi(config);
            app.UseWebApi(config);
            return _container;
        }
        
        #endregion

        public void Configuration(IAppBuilder app)
        {
            // Configure Autofac
            _container = ConfigureAutofac(_config, app);

            // Configure automapper
            MapperRegistration.RegisterMapperConfiguration();

            // Configure CORS
            ConfigureCors(app);

            // Configure Web Api
            ConfigureWebAPIRouting(_config);
        }

        
    }

    public class AutofacLifetimeScope
    {
        public static TService Resolve<TService>(IOwinContext context)
        {
            // autofacLifetimeScope is 'AutofacWebRequest' 
            // visit http://stackoverflow.com/questions/25871392/autofac-dependency-injection-in-implementation-of-oauthauthorizationserverprovid
            var autofacLifetimeScope = OwinContextExtensions.GetAutofacLifetimeScope(context);
            return autofacLifetimeScope.Resolve<TService>();
        }
        public static TService Resolve<TService>()
        {
            IOwinContext context = HttpContext.Current.GetOwinContext();
            var autofacLifetimeScope = OwinContextExtensions.GetAutofacLifetimeScope(context);
            return autofacLifetimeScope.Resolve<TService>();
        }
    }
}