using Autofac;
using Autofac.Core;
using Autofac.Features.ResolveAnything;
using Autofac.Integration.WebApi;
using Denmakers.DreamSale.Data.Context;
using Denmakers.DreamSale.Data.Infrastructure;
using Denmakers.DreamSale.Data.Repositories;
using Denmakers.DreamSale.Model.Customers;
using Denmakers.DreamSale.RESTAPI.Controllers;
using Denmakers.DreamSale.Services.Attributes;
using Denmakers.DreamSale.Services.Categories;
using Denmakers.DreamSale.Services.Configuration;
using Denmakers.DreamSale.Services.Customers;
using Denmakers.DreamSale.Services.Localization;
using Denmakers.DreamSale.Services.Stores;
using Denmakers.DreamSale.Services.Vendors;
using System.Reflection;
using System.Web.Http;

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

            // EF DbContext
            //builder.RegisterType<DreamSaleObjectContext>()
            //       .As<IDbContext>()
            //       .InstancePerLifetimeScope();

            builder.Register<IDbContext>(c => new DreamSaleObjectContext("DreamSaleConString")).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();

            builder.RegisterType<DbFactory>()
                .As<IDbFactory>()
                .InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWork>()
                .As<IUnitOfWork>()
                .InstancePerLifetimeScope();

            //builder.RegisterType<WebWorkContext>().As<IWorkContext>().InstancePerLifetimeScope();
            ////store context
            //builder.RegisterType<WebStoreContext>().As<IStoreContext>().InstancePerLifetimeScope();

            //builder.RegisterType<CustomerController>().InstancePerLifetimeScope();//.WithParameter(ResolvedParameter.ForNamed<ICacheManager>("nop_cache_static"));

            // settings
            //builder.RegisterType<CustomerSettings>().AsSelf();
            builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());

            //services
            builder.RegisterAssemblyTypes(Assembly.Load("Denmakers.DreamSale.Services"))
                    .Where(t => t.Name.EndsWith("Service"))
                    .AsImplementedInterfaces()
                    //.AsSelf()
                    .InstancePerLifetimeScope();

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

            Container = builder.Build();

            return Container;
        }
    }
}