using Denmakers.DreamSale.ViewModels.Mapper;
using System.Web.Http;

namespace Denmakers.DreamSale.RESTAPI
{
    public class Bootstrapper
    {
        public static void Run()
        {
            // Configure Autofac
            AutofacWebapiConfig.Initialize(GlobalConfiguration.Configuration);

            //Configure AutoMapper
            MapperRegistration.RegisterMapperConfiguration();
        }
    }
}