using Autofac;
using Autofac.Core;
using Autofac.Integration.WebApi;
using Denmakers.DreamSale.Data.Context;
using Denmakers.DreamSale.Data.Infrastructure;
using Denmakers.DreamSale.Data.Repositories;
using Denmakers.DreamSale.RESTAPI.Controllers;
using Denmakers.DreamSale.Services.Attributes;
using Denmakers.DreamSale.Services.Customers;
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

            builder.RegisterType<CustomerController>().InstancePerLifetimeScope();//.WithParameter(ResolvedParameter.ForNamed<ICacheManager>("nop_cache_static"));
            builder.RegisterType<GenericAttributeService>().As<IGenericAttributeService>().InstancePerLifetimeScope();
            builder.RegisterType<CustomerService>().As<ICustomerService>().InstancePerLifetimeScope();

            //builder.RegisterType<MyAuthorizationServerProvider>()
            //    .As<IOAuthAuthorizationServerProvider>()
            //    .PropertiesAutowired() // to automatically resolve IUserService
            //    .InstancePerLifetimeScope();

            Container = builder.Build();

            return Container;
        }
    }
}