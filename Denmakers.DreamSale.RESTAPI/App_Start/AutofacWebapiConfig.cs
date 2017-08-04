using Autofac;
using Autofac.Core;
using Autofac.Features.ResolveAnything;
using Autofac.Integration.WebApi;
using Denmakers.DreamSale.Data.Context;
using Denmakers.DreamSale.Data.Infrastructure;
using Denmakers.DreamSale.Data.Repositories;
//using Denmakers.DreamSale.Model.Customers;
//using Denmakers.DreamSale.RESTAPI.Controllers;
//using Denmakers.DreamSale.Services.Attributes;
//using Denmakers.DreamSale.Services.Categories;
//using Denmakers.DreamSale.Services.Configuration;
//using Denmakers.DreamSale.Services.Customers;
//using Denmakers.DreamSale.Services.Localization;
//using Denmakers.DreamSale.Services.Stores;
//using Denmakers.DreamSale.Services.Vendors;
using System.Reflection;
using System.Web.Http;
using System.Configuration;
using Denmakers.DreamSale.Helpers;
using Denmakers.DreamSale.RESTAPI.Infrastructure.WebContext;
using System.Web;
using Denmakers.DreamSale.Services.Addresses;
using Denmakers.DreamSale.Services.Logging;
using Denmakers.DreamSale.Services.Products;
using Denmakers.DreamSale.Services.Helpers;
using Denmakers.DreamSale.Common;

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

            //helpers
            builder.RegisterType<DateTimeHelper>().As<IDateTimeHelper>().InstancePerLifetimeScope();
            builder.RegisterType<DateTimeSettings>().AsSelf();
            //builder.RegisterType<GenericAttributeService>().As<IGenericAttributeService>().InstancePerLifetimeScope();
            //builder.RegisterType<CustomerService>().As<ICustomerService>().InstancePerLifetimeScope();
            //builder.RegisterType<CategoryService>().As<ICategoryService>().InstancePerLifetimeScope();
            //builder.RegisterType<SettingService>().As<ISettingService>().InstancePerLifetimeScope();
            //builder.RegisterType<LocalizationService>().As<ILocalizationService>().InstancePerLifetimeScope();
            //builder.RegisterType<LocalizedEntityService>().As<ILocalizedEntityService>().InstancePerLifetimeScope();
            //builder.RegisterType<LanguageService>().As<ILanguageService>().InstancePerLifetimeScope();
            //builder.RegisterType<StoreMappingService>().As<IStoreMappingService>().InstancePerLifetimeScope();
            //builder.RegisterType<VendorService>().As<IVendorService>().InstancePerLifetimeScope();

            //builder.RegisterType<MyAuthorizationServerProvider>()
            //    .As<IOAuthAuthorizationServerProvider>()
            //    .PropertiesAutowired() // to automatically resolve IUserService
            //    .InstancePerLifetimeScope();

            // formatter and parser
            //builder.RegisterType<CustomerAttributeFormatter>().As<ICustomerAttributeFormatter>().InstancePerLifetimeScope();
            //builder.RegisterType<CustomerAttributeParser>().As<ICustomerAttributeParser>().InstancePerLifetimeScope();

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

            // logger
            builder.RegisterType<DefaultLogger>().As<ILogger>().InstancePerLifetimeScope();

            Container = builder.Build();

            return Container;
        }
    }
}