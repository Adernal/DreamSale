using System.Web.Http;
using System.Web.Http.Cors;

namespace Denmakers.DreamSale.RESTAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            //enable cors
            EnableCorsAttribute cors = new EnableCorsAttribute(origins: "*", headers: "*", methods: "GET,POST");
            config.EnableCors(cors);


            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // to ignore An error has occurred.","ExceptionMessage":"Self referencing loop detected with  
            // https://stackoverflow.com/questions/19664257/why-in-web-api-returning-an-entity-that-has-a-one-to-many-relationship-causes-an

            //config.Formatters.Remove(config.Formatters.XmlFormatter);

            //var json = config.Formatters.JsonFormatter;
            //json.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
            //json.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            //json.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Serialize;
        }
    }
}
