using Autofac;
using Autofac.Features.ResolveAnything;
using Autofac.Integration.WebApi;
using Denmakers.DreamSale.Data.Context;
using Denmakers.DreamSale.Data.Infrastructure;
using Denmakers.DreamSale.Data.Repositories;
using System.Reflection;
using System.Web.Http;
using Denmakers.DreamSale.Helpers;
using Denmakers.DreamSale.RESTAPI.Infrastructure.WebContext;
using System.Web;
using Denmakers.DreamSale.Services.Logging;
using Denmakers.DreamSale.Services.Helpers;
using Denmakers.DreamSale.Common;
using Denmakers.DreamSale.Services.Messages;

namespace Denmakers.DreamSale.RESTAPI
{
    public class AutofacWebapiConfig
    {
        public static IContainer Container;

        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            //DependencyResolver.SetResolver(new AutofacDependencyResolver(container)); //Set the MVC DependencyResolver
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

        }

        public static IContainer Initialize(HttpConfiguration config)
        {
            Container = RegisterServices(new ContainerBuilder());
            Initialize(config, Container);
            return Container;
        }

        private static IContainer RegisterServices(ContainerBuilder builder)
        {
            //builder.RegisterControllers(Assembly.GetExecutingAssembly()); //Register MVC Controllers
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly()); //Register WebApi Controllers

            //type finder
            var typeFinder = new WebAppTypeFinder();
            builder.RegisterInstance(typeFinder).As<ITypeFinder>().SingleInstance();

            // EF DbContext
            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
            //builder.RegisterType<DreamSaleObjectContext>()
            //       .As<IDbContext>()
            //       .InstancePerLifetimeScope();
            //var conString = ConfigurationManager.AppSettings["DreamSaleConString"].ToString();
            builder.Register<IDbContext>(c => new DreamSaleObjectContext("DreamSaleConString")).InstancePerLifetimeScope();
            
            // Web context
            builder.RegisterType<WebWorkContext>().As<IWorkContext>().InstancePerLifetimeScope();
            ////store context
            builder.RegisterType<WebStoreContext>().As<IStoreContext>().InstancePerLifetimeScope();

            // Db Factory
            builder.RegisterType<DbFactory>()
                .As<IDbFactory>()
                .InstancePerLifetimeScope();

            // Unit Of work
            builder.RegisterType<UnitOfWork>()
                .As<IUnitOfWork>()
                .InstancePerLifetimeScope();
            
            //HTTP context and other related stuff
            builder.Register(c =>
                (new HttpContextWrapper(HttpContext.Current) as HttpContextBase))
                .As<HttpContextBase>()
                .InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<HttpContextBase>().Request)
                .As<HttpRequestBase>()
                .InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<HttpContextBase>().Response)
                .As<HttpResponseBase>()
                .InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<HttpContextBase>().Server)
                .As<HttpServerUtilityBase>()
                .InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<HttpContextBase>().Session)
                .As<HttpSessionStateBase>()
                .InstancePerLifetimeScope();

            //builder.RegisterType<CustomerController>().InstancePerLifetimeScope();//.WithParameter(ResolvedParameter.ForNamed<ICacheManager>("nop_cache_static"));

            //web helper
            builder.RegisterType<WebHelper>().As<IWebHelper>().InstancePerLifetimeScope();

            // settings
            builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());

            //services
            builder.RegisterAssemblyTypes(Assembly.Load("Denmakers.DreamSale.Services"))
                    .Where(t => t.Name.EndsWith("Service"))
                    .AsImplementedInterfaces()
                    //.AsSelf()
                    .InstancePerLifetimeScope();

            //builder.RegisterType<MyAuthorizationServerProvider>()
            //    .As<IOAuthAuthorizationServerProvider>()
            //    .PropertiesAutowired() // to automatically resolve IUserService
            //    .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(Assembly.Load("Denmakers.DreamSale.Services"))
                    .Where(t => t.Name.EndsWith("Formatter"))
                    .AsImplementedInterfaces()
                    //.AsSelf()
                    .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(Assembly.Load("Denmakers.DreamSale.Services"))
                    .Where(t => t.Name.EndsWith("Parser"))
                    .AsImplementedInterfaces()
                    //.AsSelf()
                    .InstancePerLifetimeScope();

            //builder.RegisterType<AddressAttributeFormatter>().As<IAddressAttributeFormatter>().InstancePerLifetimeScope();
            //builder.RegisterType<AddressAttributeParser>().As<IAddressAttributeParser>().InstancePerLifetimeScope();

            //builder.RegisterType<ProductAttributeParser>().As<IProductAttributeParser>().InstancePerLifetimeScope();
            //builder.RegisterType<ProductAttributeFormatter>().As<IProductAttributeFormatter>().InstancePerLifetimeScope();
            //builder.RegisterType<CustomerAttributeFormatter>().As<ICustomerAttributeFormatter>().InstancePerLifetimeScope();
            //builder.RegisterType<CustomerAttributeParser>().As<ICustomerAttributeParser>().InstancePerLifetimeScope();

            // logger
            builder.RegisterType<DefaultLogger>().As<ILogger>().InstancePerLifetimeScope();

            //Misc
            builder.RegisterType<DateTimeHelper>().As<IDateTimeHelper>().InstancePerLifetimeScope();
            builder.RegisterType<DateTimeSettings>().AsSelf();
            builder.RegisterType<Tokenizer>().As<ITokenizer>().InstancePerLifetimeScope();
            builder.RegisterType<EmailSender>().As<IEmailSender>().InstancePerLifetimeScope();
            builder.RegisterType<MessageTokenProvider>().As<IMessageTokenProvider>().InstancePerLifetimeScope();

            Container = builder.Build();

            return Container;
        }
    }
}