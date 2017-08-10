using System;
using Owin;
using Microsoft.Owin.Security.OAuth;
using System.Web.Http;
using Autofac;
using Denmakers.DreamSale.RESTAPI.Infrastructure.OAuth;
using Denmakers.DreamSale.ViewModels.Mapper;
using Microsoft.Owin;
using Autofac.Integration.Owin;
using System.Web;

[assembly: OwinStartup(typeof(Denmakers.DreamSale.RESTAPI.Startup))]

namespace Denmakers.DreamSale.RESTAPI
{
    public class Startup
    {
        private static IContainer _container;

        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            // Configure Autofac
            _container = ConfigureAutofac(config, app);

            // Configure automapper
            //Configure AutoMapper
            MapperRegistration.RegisterMapperConfiguration();

            // Configure CORS
            ConfigureCors(app);

            // Configure Web Api
            ConfigureWebAPIRouting(config);
        }

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

            //AreaRegistration.RegisterAllAreas();
            //RouteConfig.RegisterRoutes(RouteTable.Routes);

            //FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            //RouteConfig.RegisterRoutes(RouteTable.Routes);
            //BundleConfig.RegisterBundles(BundleTable.Bundles);
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            //GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            //GlobalConfiguration.Configuration.Formatters.Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);
        }

        public void ConfigureOAuth(HttpConfiguration config, IAppBuilder app)
        {
            var oAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(365 * 24),
                // RefreshTokenProvider = _tokenProvider,
                //Provider = _container.Resolve<IOAuthAuthorizationServerProvider>()
                Provider = new OwinAuthorizationServerProvider()
                //Provider = config.DependencyResolver.GetService(typeof(MyAuthorizationServerProvider)) as MyAuthorizationServerProvider
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(oAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            //app.UseFacebookAuthentication(
            //   appId: "",
            //   appSecret: "");

            //app.UseGoogleAuthentication(
            //     clientId: "000-000.apps.googleusercontent.com",
            //     clientSecret: "00000000000");
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