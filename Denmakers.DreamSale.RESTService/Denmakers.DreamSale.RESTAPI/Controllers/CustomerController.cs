using Denmakers.DreamSale.Data.Infrastructure;
using Denmakers.DreamSale.Data.Repositories;
using Denmakers.DreamSale.Model.Customers;
using Denmakers.DreamSale.Model.Logging;
using Denmakers.DreamSale.Model.Orders;
using Denmakers.DreamSale.RESTAPI.Infrastructure;
using Denmakers.DreamSale.Services.Categories;
using Denmakers.DreamSale.Services.Configuration;
using Denmakers.DreamSale.Services.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Denmakers.DreamSale.RESTAPI.Controllers
{
    [RoutePrefix("api/Customer")]
    public class CustomerController : ApiControllerBase
    {
        #region Fields
        private readonly ICustomerService _customerService;
        private readonly ICategoryService _categoryService;
        private readonly CustomerSettings _customerSettings;
        private readonly ISettingService _settingService;
        #endregion

        #region Ctor
        public CustomerController(IRepository<Log> log, IUnitOfWork unitOfWork, ICustomerService customerService, ICategoryService categoryService, ISettingService settingService)
            : base(log, unitOfWork)
        {
            this._customerService = customerService;
            this._categoryService = categoryService;
            this._settingService = settingService;
            this._customerSettings = _settingService.LoadSetting<CustomerSettings>();
        }
        #endregion

        #region Methods
        [HttpGet]
        [Route("")]
        public HttpResponseMessage GetAllCustomers(HttpRequestMessage request, int[] customerRoleIds, DateTime? createdFromUtc = null,
            DateTime? createdToUtc = null, int affiliateId = 0, int vendorId = 0,
            string email = null, string username = null,
            string firstName = null, string lastName = null,
            int dayOfBirth = 0, int monthOfBirth = 0,
            string company = null, string phone = null, string zipPostalCode = null,
            string ipAddress = null, bool loadOnlyWithShoppingCart = false, ShoppingCartType? sct = null,
            int pageIndex = 0, int pageSize = int.MaxValue)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var customers = _customerService.GetAllCustomers(createdFromUtc, createdToUtc, affiliateId, vendorId, customerRoleIds, email, username, firstName, lastName, dayOfBirth, monthOfBirth, company, phone, zipPostalCode, ipAddress, loadOnlyWithShoppingCart, sct, pageIndex, pageSize);

                response = request.CreateResponse<List<Customer>>(HttpStatusCode.OK, customers.ToList());

                return response;
            });
        }

        [Route("GetById/{customerId}")]
        public HttpResponseMessage GetCustomerById(HttpRequestMessage request, int customerId)
        {
            if (customerId == 0)
            {
                var sortedCategories = _categoryService.GetAllCategories();
                var alreadyProcessedCategoryIds = new List<string>();
                foreach (var cat in sortedCategories)
                {
                    alreadyProcessedCategoryIds.Add(cat.GetFormattedBreadCrumb(sortedCategories));
                }
                return null;
            }

            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var customer = _customerService.GetCustomerById(customerId);

                response = request.CreateResponse<Customer>(HttpStatusCode.OK, customer);

                return response;
            });
        }
        #endregion
    }
}
