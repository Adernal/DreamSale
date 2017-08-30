using Denmakers.DreamSale.Data.Context;
using Denmakers.DreamSale.Helpers;
using Denmakers.DreamSale.Model;
using Denmakers.DreamSale.Model.Common;
using Denmakers.DreamSale.Model.Customers;
using Denmakers.DreamSale.Model.Localization;
using Denmakers.DreamSale.Model.Media;
using Denmakers.DreamSale.RESTAPI.Infrastructure;
using Denmakers.DreamSale.RESTAPI.Results;
using Denmakers.DreamSale.Services;
using Denmakers.DreamSale.Services.Addresses;
using Denmakers.DreamSale.Services.Attributes;
using Denmakers.DreamSale.Services.Configuration;
using Denmakers.DreamSale.Services.Customers;
using Denmakers.DreamSale.Services.Localization;
using Denmakers.DreamSale.Services.Logging;
using Denmakers.DreamSale.Services.Media;
using Denmakers.DreamSale.Services.Messages;
using Denmakers.DreamSale.Services.Orders;
using Denmakers.DreamSale.Services.Stores;
using Denmakers.DreamSale.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;

namespace Denmakers.DreamSale.RESTAPI.Controllers
{
    [RoutePrefix("api/CustomerAuthentication")]
    public class CustomerAuthenticationController : ApiControllerBase
    {
        #region Fields
        //private Data.Repositories.IRepository<Model.Clients.Client> _clientRepository;
        private readonly ICustomerRegistrationService _customerRegistrationService;
        private readonly ICustomerService _customerService;
        private readonly ICustomerAttributeParser _customerAttributeParser;
        private readonly ICustomerAttributeService _customerAttributeService;
        private readonly ICustomerActivityService _customerActivityService;
        private readonly IAddressService _addressService;
        private readonly IAddressAttributeParser _addressAttributeParser;
        private readonly IAddressAttributeService _addressAttributeService;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly ISettingService _settingService;
        private readonly CustomerSettings _customerSettings;
        private readonly AddressSettings _addressSettings;

        private readonly ILocalizationService _localizationService;
        private readonly IStoreContext _storeContext;
        private readonly IOrderService _orderService;
        private readonly IPictureService _pictureService;
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IStoreService _storeService;
        private readonly IWorkflowMessageService _workflowMessageService;

        private readonly MediaSettings _mediaSettings;
        private readonly LocalizationSettings _localizationSettings;
        //private readonly CaptchaSettings _captchaSettings;
        private readonly StoreInformationSettings _storeInformationSettings;

        private IAuthenticationManager Authentication
        {
            get { return Request.GetOwinContext().Authentication; }
        }

        #endregion

        #region Ctor
        public CustomerAuthenticationController(IBaseService baseService, ILogger logger, IWebHelper webHelper,
            //Data.Repositories.IRepository<Model.Clients.Client> clientRepository,
            ICustomerRegistrationService customerRegistrationService,
            ICustomerService customerService,
            ICustomerAttributeParser customerAttributeParser,
            ICustomerAttributeService customerAttributeService,
            ICustomerActivityService customerActivityService,
            IAddressService addressService,
            IAddressAttributeParser addressAttributeParser,
            IAddressAttributeService addressAttributeService,
            IGenericAttributeService genericAttributeService,
            ISettingService settingService,

            ILocalizationService localizationService,
            IStoreContext storeContext,
            IOrderService orderService,
            IPictureService pictureService,
            IShoppingCartService shoppingCartService,
            IStoreService storeService,
            IWorkflowMessageService workflowMessageService
            )
            : base(baseService, logger, webHelper)
        {
            //this._clientRepository = clientRepository;
            this._customerRegistrationService = customerRegistrationService;
            this._customerService = customerService;
            this._customerAttributeParser = customerAttributeParser;
            this._customerAttributeService = customerAttributeService;
            this._customerActivityService = customerActivityService;
            this._addressService = addressService;
            this._addressAttributeParser = addressAttributeParser;
            this._addressAttributeService = addressAttributeService;
            this._genericAttributeService = genericAttributeService;
            this._settingService = settingService;

            this._customerSettings = _settingService.LoadSetting<CustomerSettings>();
            this._addressSettings = _settingService.LoadSetting<AddressSettings>();

            this._localizationService = localizationService;
            this._storeContext = storeContext;
            this._orderService = orderService;
            this._pictureService = pictureService;
            this._shoppingCartService = shoppingCartService;
            this._workflowMessageService = workflowMessageService;

            this._mediaSettings = _settingService.LoadSetting<MediaSettings>();
            this._localizationSettings = _settingService.LoadSetting<LocalizationSettings>();
            this._storeInformationSettings = _settingService.LoadSetting<StoreInformationSettings>();
        }
        #endregion

        #region Nested Class
        private class ExternalLoginData
        {
            public string LoginProvider { get; set; }
            public string ProviderKey { get; set; }
            public string UserName { get; set; }
            public string ExternalAccessToken { get; set; }

            public static ExternalLoginData FromIdentity(ClaimsIdentity identity)
            {
                if (identity == null)
                {
                    return null;
                }

                Claim providerKeyClaim = identity.FindFirst(ClaimTypes.NameIdentifier);

                if (providerKeyClaim == null || String.IsNullOrEmpty(providerKeyClaim.Issuer) || String.IsNullOrEmpty(providerKeyClaim.Value))
                {
                    return null;
                }

                if (providerKeyClaim.Issuer == ClaimsIdentity.DefaultIssuer)
                {
                    return null;
                }

                return new ExternalLoginData
                {
                    LoginProvider = providerKeyClaim.Issuer,
                    ProviderKey = providerKeyClaim.Value,
                    UserName = identity.FindFirstValue(ClaimTypes.Name),
                    ExternalAccessToken = identity.FindFirstValue("ExternalAccessToken"),
                };
            }
        }
        #endregion

        //#region Utilities
        //private string GetQueryString(HttpRequestMessage request, string key)
        //{
        //    var queryStrings = request.GetQueryNameValuePairs();

        //    if (queryStrings == null)
        //        return null;

        //    var match = queryStrings.FirstOrDefault(keyValue => string.Compare(keyValue.Key, key, true) == 0);

        //    if (string.IsNullOrEmpty(match.Value))
        //        return null;

        //    return match.Value;
        //}

        //private string ValidateClientAndRedirectUri(HttpRequestMessage request, ref string redirectUriOutput)
        //{
        //    Uri redirectUri;

        //    var redirectUriString = GetQueryString(Request, "redirect_uri");

        //    if (string.IsNullOrWhiteSpace(redirectUriString))
        //    {
        //        return "redirect_uri is required";
        //    }

        //    bool validUri = Uri.TryCreate(redirectUriString, UriKind.Absolute, out redirectUri);

        //    if (!validUri)
        //    {
        //        return "redirect_uri is invalid";
        //    }

        //    var clientId = GetQueryString(Request, "client_id");

        //    if (string.IsNullOrWhiteSpace(clientId))
        //    {
        //        return "client_Id is required";
        //    }

        //    var client = _clientRepository.FindBy(c => c.Name.Equals(clientId, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();// _repo.FindClient(clientId);

        //    if (client == null)
        //    {
        //        return string.Format("Client_id '{0}' is not registered in the system.", clientId);
        //    }

        //    if (!string.Equals(client.AllowedOrigin, redirectUri.GetLeftPart(UriPartial.Authority), StringComparison.OrdinalIgnoreCase))
        //    {
        //        return string.Format("The given URL is not allowed by Client_id '{0}' configuration.", clientId);
        //    }

        //    redirectUriOutput = redirectUri.AbsoluteUri;

        //    return string.Empty;

        //}


        //private async Task<ParsedExternalAccessToken> VerifyExternalAccessToken(string provider, string accessToken)
        //{
        //    ParsedExternalAccessToken parsedToken = null;

        //    var verifyTokenEndPoint = "";

        //    if (provider == "Facebook")
        //    {
        //        //You can get it from here: https://developers.facebook.com/tools/accesstoken/
        //        //More about debug_tokn here: http://stackoverflow.com/questions/16641083/how-does-one-get-the-app-access-token-for-debug-token-inspection-on-facebook
        //        var appToken = "xxxxxx";
        //        verifyTokenEndPoint = string.Format("https://graph.facebook.com/debug_token?input_token={0}&access_token={1}", accessToken, appToken);
        //    }
        //    else if (provider == "Google")
        //    {
        //        verifyTokenEndPoint = string.Format("https://www.googleapis.com/oauth2/v1/tokeninfo?access_token={0}", accessToken);
        //    }
        //    else
        //    {
        //        return null;
        //    }

        //    var client = new HttpClient();
        //    var uri = new Uri(verifyTokenEndPoint);
        //    var response = await client.GetAsync(uri);

        //    if (response.IsSuccessStatusCode)
        //    {
        //        var content = await response.Content.ReadAsStringAsync();

        //        dynamic jObj = (JObject)Newtonsoft.Json.JsonConvert.DeserializeObject(content);

        //        parsedToken = new ParsedExternalAccessToken();

        //        if (provider == "Facebook")
        //        {
        //            parsedToken.user_id = jObj["data"]["user_id"];
        //            parsedToken.app_id = jObj["data"]["app_id"];

        //            if (!string.Equals(Startup.FacebookAuthOptions.AppId, parsedToken.app_id, StringComparison.OrdinalIgnoreCase))
        //            {
        //                return null;
        //            }
        //        }
        //        else if (provider == "Google")
        //        {
        //            parsedToken.user_id = jObj["user_id"];
        //            parsedToken.app_id = jObj["audience"];

        //            if (!string.Equals(Startup.GoogleAuthOptions.ClientId, parsedToken.app_id, StringComparison.OrdinalIgnoreCase))
        //            {
        //                return null;
        //            }

        //        }

        //    }

        //    return parsedToken;
        //}

        //#endregion

        #region Methods
        //[OverrideAuthentication]
        //[HostAuthentication(DefaultAuthenticationTypes.ExternalCookie)]
        //[AllowAnonymous]
        //[Route("ExternalLogin", Name = "ExternalLogin")]
        //public async Task<IHttpActionResult> GetExternalLogin(string provider, string error = null)
        //{
        //    string redirectUri = string.Empty;

        //    if (error != null)
        //    {
        //        return BadRequest(Uri.EscapeDataString(error));
        //    }

        //    if (!User.Identity.IsAuthenticated)
        //    {
        //        return new ChallengeResult(provider, this);
        //    }

        //    var redirectUriValidationResult = ValidateClientAndRedirectUri(this.Request, ref redirectUri);

        //    if (!string.IsNullOrWhiteSpace(redirectUriValidationResult))
        //    {
        //        return BadRequest(redirectUriValidationResult);
        //    }

        //    ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);

        //    if (externalLogin == null)
        //    {
        //        return InternalServerError();
        //    }

        //    if (externalLogin.LoginProvider != provider)
        //    {
        //        Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);
        //        return new ChallengeResult(provider, this);
        //    }

        //    IdentityUser user = await _repo.FindAsync(new UserLoginInfo(externalLogin.LoginProvider, externalLogin.ProviderKey));

        //    bool hasRegistered = user != null;

        //    redirectUri = string.Format("{0}#external_access_token={1}&provider={2}&haslocalaccount={3}&external_user_name={4}",
        //                                    redirectUri,
        //                                    externalLogin.ExternalAccessToken,
        //                                    externalLogin.LoginProvider,
        //                                    hasRegistered.ToString(),
        //                                    externalLogin.UserName);

        //    return Redirect(redirectUri);

        //}

        //// POST api/CustomerAuthentication/RegisterExternal
        //[AllowAnonymous]
        //[Route("RegisterExternal")]
        //public async Task<IHttpActionResult> RegisterExternal(RegisterExternalBindingModel model)
        //{

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var verifiedAccessToken = await VerifyExternalAccessToken(model.Provider, model.ExternalAccessToken);
        //    if (verifiedAccessToken == null)
        //    {
        //        return BadRequest("Invalid Provider or External Access Token");
        //    }

        //    IdentityUser user = await _repo.FindAsync(new UserLoginInfo(model.Provider, verifiedAccessToken.user_id));

        //    bool hasRegistered = user != null;

        //    if (hasRegistered)
        //    {
        //        return BadRequest("External user is already registered");
        //    }

        //    user = new IdentityUser() { UserName = model.UserName };

        //    IdentityResult result = await _repo.CreateAsync(user);
        //    if (!result.Succeeded)
        //    {
        //        return GetErrorResult(result);
        //    }

        //    var info = new ExternalLoginInfo()
        //    {
        //        DefaultUserName = model.UserName,
        //        Login = new UserLoginInfo(model.Provider, verifiedAccessToken.user_id)
        //    };

        //    result = await _repo.AddLoginAsync(user.Id, info.Login);
        //    if (!result.Succeeded)
        //    {
        //        return GetErrorResult(result);
        //    }

        //    //generate access token response
        //    var accessTokenResponse = GenerateLocalAccessTokenResponse(model.UserName);

        //    return Ok(accessTokenResponse);
        //}

        [HttpPost]
        [Route("RegisterCustomer", Name = "RegisterCustomer")]
        public HttpResponseMessage RegisterCustomer(HttpRequestMessage request, RegistrationVM model)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.NotFound, "No items found");
                //response = request.CreateResponse<BulkEditListVM>(HttpStatusCode.OK, model);
                if (_baseService.WorkContext.CurrentCustomer.IsRegistered())
                {
                    //Save a new record
                    _baseService.WorkContext.CurrentCustomer = _customerService.InsertGuestCustomer();
                }
                var customer = _baseService.WorkContext.CurrentCustomer;
                customer.RegisteredInStoreId = _storeContext.CurrentStore.Id;
                if (ModelState.IsValid)
                {
                    if (_customerSettings.UsernamesEnabled && model.UserName != null)
                    {
                        model.UserName = model.UserName.Trim();
                    }

                    bool isApproved = _customerSettings.UserRegistrationType == UserRegistrationType.Standard;
                    var registrationRequest = new CustomerRegistrationRequest(customer,
                        model.UserName,
                        _customerSettings.UsernamesEnabled ? model.UserName : "",
                        model.Password,
                        _customerSettings.DefaultPasswordFormat,
                        _storeContext.CurrentStore.Id,
                        isApproved);
                    var registrationResult = _customerRegistrationService.RegisterCustomer(registrationRequest);

                    if (registrationResult.Success)
                    {

                        //insert default address (if possible)
                        var defaultAddress = new Address
                        {
                            FirstName = customer.GetAttribute<string>(SystemCustomerAttributeNames.FirstName, _genericAttributeService),
                            LastName = customer.GetAttribute<string>(SystemCustomerAttributeNames.LastName, _genericAttributeService),
                            Email = customer.Email,
                            Company = customer.GetAttribute<string>(SystemCustomerAttributeNames.Company, _genericAttributeService),
                            CountryId = customer.GetAttribute<int>(SystemCustomerAttributeNames.CountryId, _genericAttributeService) > 0
                                ? (int?)customer.GetAttribute<int>(SystemCustomerAttributeNames.CountryId, _genericAttributeService)
                                : null,
                            StateProvinceId = customer.GetAttribute<int>(SystemCustomerAttributeNames.StateProvinceId, _genericAttributeService) > 0
                                ? (int?)customer.GetAttribute<int>(SystemCustomerAttributeNames.StateProvinceId, _genericAttributeService)
                                : null,
                            City = customer.GetAttribute<string>(SystemCustomerAttributeNames.City, _genericAttributeService),
                            Address1 = customer.GetAttribute<string>(SystemCustomerAttributeNames.StreetAddress, _genericAttributeService),
                            Address2 = customer.GetAttribute<string>(SystemCustomerAttributeNames.StreetAddress2, _genericAttributeService),
                            ZipPostalCode = customer.GetAttribute<string>(SystemCustomerAttributeNames.ZipPostalCode, _genericAttributeService),
                            PhoneNumber = customer.GetAttribute<string>(SystemCustomerAttributeNames.Phone, _genericAttributeService),
                            FaxNumber = customer.GetAttribute<string>(SystemCustomerAttributeNames.Fax, _genericAttributeService),
                            CreatedOnUtc = customer.CreatedOnUtc
                        };

                        //some validation
                        if (defaultAddress.CountryId == 0)
                            defaultAddress.CountryId = null;
                        if (defaultAddress.StateProvinceId == 0)
                            defaultAddress.StateProvinceId = null;
                        //set default address
                        customer.Addresses.Add(defaultAddress);
                        customer.BillingAddress = defaultAddress;
                        customer.ShippingAddress = defaultAddress;
                        _customerService.UpdateCustomer(customer);

                        _baseService.Commit();
                        response = request.CreateResponse<Customer>(HttpStatusCode.OK, customer);
                    }
                }
                return response;

            });
        }
        #endregion
    }
}
