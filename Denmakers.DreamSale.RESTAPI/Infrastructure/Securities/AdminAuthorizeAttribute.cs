using Denmakers.DreamSale.Services.Customers;
using Denmakers.DreamSale.Services.Security;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace Denmakers.DreamSale.RESTAPI.Infrastructure.Securities
{
    public class AdminAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            string userName = HttpContext.Current.User.Identity.Name;
            string requiredPermission = string.Format("{0}/{1}", actionContext.ActionDescriptor.ControllerDescriptor.ControllerName, actionContext.ActionDescriptor.ActionName);

            if (!this.HasAdminAccess())
                base.HandleUnauthorizedRequest(actionContext);


            base.OnAuthorization(actionContext);
        }

        //protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        //{
        //    if (!HttpContext.Current.User.Identity.IsAuthenticated)
        //    {
        //        base.HandleUnauthorizedRequest(actionContext);
        //    }
        //    else
        //    {
        //        actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
        //    }
        //}

        public virtual bool HasAdminAccess()
        {
            var permissionService = AutofacLifetimeScope.Resolve<IPermissionService>();
            var customerService = AutofacLifetimeScope.Resolve<ICustomerService>();
            string userName = HttpContext.Current.User.Identity.Name;
            var customer = customerService.GetCustomerByUsername(userName);
            bool result = permissionService.Authorize(StandardPermissionProvider.AccessAdminPanel, customer);
            return result;
        }
    }
}