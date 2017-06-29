using System.Web;
using System.Web.Mvc;

namespace Denmakers.DreamSale.RESTAPI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
