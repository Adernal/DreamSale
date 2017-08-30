using Denmakers.DreamSale.Model.Customers;
using Denmakers.DreamSale.Services;
using Denmakers.DreamSale.Services.Customers;
using Denmakers.DreamSale.Services.Logging;
using Denmakers.DreamSale.Services.Orders;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Denmakers.DreamSale.RESTAPI.Infrastructure.OAuth
{
    public class OwinAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        #region Properties
        public ICustomerRegistrationService CustomerRegistrationService { get; set; }
        public ICustomerService CustomerService { get; set; }
        public IShoppingCartService ShoppingCartService { get; set; }
        public ICustomerActivityService CustomerActivityService { get; set; }
        public IBaseService BaseService { get; set; }
        #endregion

        //public override Task MatchEndpoint(OAuthMatchEndpointContext context)
        //{
        //    if (context.IsTokenEndpoint && context.Request.Method == "OPTIONS")
        //    {
        //        context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
        //        context.OwinContext.Response.Headers.Add("Access-Control-Allow-Headers", new[] { "authorization" });
        //        context.RequestCompleted();
        //        return Task.FromResult(0);
        //    }

        //    return base.MatchEndpoint(context);
        //}
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string clientId;
            string clientSecret;

            if (context.TryGetBasicCredentials(out clientId, out clientSecret))
            {
                // validate the client Id and secret against database or from configuration file.  
                context.Validated();
            }
            //else
            //{
            //    context.SetError("invalid_client", "Client credentials could not be retrieved from the Authorization header");
            //    context.Rejected();
            //}
            context.OwinContext.Set<string>("as:clientAllowedOrigin", "*");
            context.Validated();
            return Task.FromResult<object>(null);
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var allowedOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin");

            if (allowedOrigin == null) allowedOrigin = "*";

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });


            CustomerRegistrationService = CustomerRegistrationService ?? AutofacLifetimeScope.Resolve<ICustomerRegistrationService>(context.OwinContext);
            CustomerService = CustomerService ?? AutofacLifetimeScope.Resolve<ICustomerService>(context.OwinContext);

            Customer customer = CustomerService.GetCustomerByEmail(context.UserName);
            var validationResult = CustomerRegistrationService.ValidateCustomer(context.UserName, context.Password);
            if (validationResult != CustomerLoginResults.Successful)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }
            //var unitofWork = AutofacLifetimeScope.Resolve<Data.Infrastructure.IUnitOfWork>(context.OwinContext);
            BaseService = BaseService ?? AutofacLifetimeScope.Resolve<IBaseService>(context.OwinContext);
            ShoppingCartService = ShoppingCartService ?? AutofacLifetimeScope.Resolve<IShoppingCartService>(context.OwinContext);
            CustomerActivityService = CustomerActivityService ?? AutofacLifetimeScope.Resolve<ICustomerActivityService>(context.OwinContext);            

            //migrate shopping cart
            ShoppingCartService.MigrateShoppingCart(BaseService.WorkContext.CurrentCustomer, customer, true);
            
            //activity log
            CustomerActivityService.InsertActivity(customer, "PublicStore.Login", "Login");

            BaseService.Commit();
            //unitofWork.Commit();

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Role, string.Join(",", customer.CustomerRoles.Select(r => r.Name))));
            identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
            identity.AddClaim(new Claim("sub", context.UserName));

            //context.Validated(identity);

            //new code
            var props = new AuthenticationProperties(new Dictionary<string, string>
                {
                    {
                        "as:client_id", (context.ClientId == null) ? string.Empty : context.ClientId
                    },
                    {
                        "userName", context.UserName
                    }
                });

            var ticket = new AuthenticationTicket(identity, props);

            
            context.Validated(ticket);

            //context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" }); // To allow CORS on the token middleware provider
        }

        public override Task TokenEndpointResponse(OAuthTokenEndpointResponseContext context)
        {
            var accessToken = context.AccessToken;
            var response = base.TokenEndpointResponse(context);
            //return response;

            foreach (System.Collections.Generic.KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }
    }
}