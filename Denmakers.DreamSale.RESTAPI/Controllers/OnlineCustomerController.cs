using Denmakers.DreamSale.Data.Context;
using Denmakers.DreamSale.Data.Infrastructure;
using Denmakers.DreamSale.Data.Repositories;
using Denmakers.DreamSale.Helpers;
using Denmakers.DreamSale.Model.Customers;
using Denmakers.DreamSale.Model.Logging;
using Denmakers.DreamSale.RESTAPI.Infrastructure;
using Denmakers.DreamSale.Services;
using Denmakers.DreamSale.Services.Attributes;
using Denmakers.DreamSale.Services.Configuration;
using Denmakers.DreamSale.Services.Customers;
using Denmakers.DreamSale.Services.Directory;
using Denmakers.DreamSale.Services.Helpers;
using Denmakers.DreamSale.Services.Localization;
using Denmakers.DreamSale.Services.Logging;
using Denmakers.DreamSale.Services.Security;
using Denmakers.DreamSale.ViewModels.AdminVM.Customers;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Denmakers.DreamSale.RESTAPI.Controllers
{
    [RoutePrefix("api/onlineCustomers")]
    public class OnlineCustomerController : ApiControllerBase
    {
        #region Fields

        private readonly ICustomerService _customerService;
        private readonly IGeoLookupService _geoLookupService;
        private readonly IDateTimeHelper _dateTimeHelper;
        private readonly IPermissionService _permissionService;
        private readonly ILocalizationService _localizationService;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly ISettingService _settingService;
        private readonly CustomerSettings _customerSettings;
        #endregion

        #region Constructors

        public OnlineCustomerController(IBaseService baseService, ILogger logger, IWebHelper webHelper,
            ICustomerService customerService,
            IGeoLookupService geoLookupService, 
            IDateTimeHelper dateTimeHelper,
            IPermissionService permissionService, 
            ILocalizationService localizationService,
            IGenericAttributeService genericAttributeService,
            ISettingService settingService)
            : base(baseService, logger, webHelper)
        {
            this._customerService = customerService;
            this._geoLookupService = geoLookupService;
            this._dateTimeHelper = dateTimeHelper;
            this._permissionService = permissionService;
            this._localizationService = localizationService;
            this._genericAttributeService = genericAttributeService;
            this._settingService = settingService;
            this._customerSettings = _settingService.LoadSetting<CustomerSettings>();
        }

        #endregion

        [HttpGet]
        [Route("{pageIndex:int=0}/{pageSize:int=2147483640}")]
        public HttpResponseMessage List(HttpRequestMessage request, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageCustomers))
                {
                    var customers = _customerService.GetOnlineCustomers(DateTime.UtcNow.AddMinutes(_customerSettings.OnlineCustomerMinutes),
                null, pageIndex, pageSize);
                    var gridModel = new DataSourceResult
                    {
                        Data = customers.Select(x => new OnlineCustomerVM
                        {
                            Id = x.Id,
                            CustomerInfo = x.IsRegistered() ? x.Email : _localizationService.GetResource("Admin.Customers.Guest"),
                            LastIpAddress = x.LastIpAddress,
                            Location = _geoLookupService.LookupCountryName(x.LastIpAddress),
                            LastActivityDate = _dateTimeHelper.ConvertToUserTime(x.LastActivityDateUtc, DateTimeKind.Utc),
                            LastVisitedPage = _customerSettings.StoreLastVisitedPage ?
                                x.GetAttribute<string>(SystemCustomerAttributeNames.LastVisitedPage, _genericAttributeService) :
                                _localizationService.GetResource("Admin.Customers.OnlineCustomers.Fields.LastVisitedPage.Disabled")
                        }),
                        Total = customers.TotalCount
                    };

                    response = request.CreateResponse<DataSourceResult>(HttpStatusCode.OK, gridModel);
                }
                return response;

            });
        }
    }
}
