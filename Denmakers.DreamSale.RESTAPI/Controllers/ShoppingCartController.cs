using Denmakers.DreamSale.Helpers;
using Denmakers.DreamSale.Model.Customers;
using Denmakers.DreamSale.Model.Orders;
using Denmakers.DreamSale.RESTAPI.Infrastructure;
using Denmakers.DreamSale.Services;
using Denmakers.DreamSale.Services.Catalog;
using Denmakers.DreamSale.Services.Customers;
using Denmakers.DreamSale.Services.Helpers;
using Denmakers.DreamSale.Services.Localization;
using Denmakers.DreamSale.Services.Logging;
using Denmakers.DreamSale.Services.Orders;
using Denmakers.DreamSale.Services.Products;
using Denmakers.DreamSale.Services.Security;
using Denmakers.DreamSale.Services.Stores;
using Denmakers.DreamSale.Services.Tax;
using Denmakers.DreamSale.ViewModels.AdminVM.ShoppingCart;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Denmakers.DreamSale.RESTAPI.Controllers
{
    [RoutePrefix("api/ShoppingCart")]
    //[Infrastructure.Securities.AdminAuthorize]
    public class ShoppingCartController : ApiControllerBase
    {
        #region Fields

        private readonly ICustomerService _customerService;
        private readonly IDateTimeHelper _dateTimeHelper;
        private readonly IPriceFormatter _priceFormatter;
        private readonly IStoreService _storeService;
        private readonly ITaxService _taxService;
        private readonly IPriceCalculationService _priceCalculationService;
        private readonly IPermissionService _permissionService;
        private readonly ILocalizationService _localizationService;
        private readonly IProductAttributeFormatter _productAttributeFormatter;

        #endregion

        #region Constructors

        public ShoppingCartController(IBaseService baseService, ILogger logger, IWebHelper webHelper,
            ICustomerService customerService,
            IDateTimeHelper dateTimeHelper,
            IPriceFormatter priceFormatter,
            IStoreService storeService,
            ITaxService taxService,
            IPriceCalculationService priceCalculationService,
            IPermissionService permissionService,
            ILocalizationService localizationService,
            IProductAttributeFormatter productAttributeFormatter)
            : base(baseService, logger, webHelper)
        {
            this._customerService = customerService;
            this._dateTimeHelper = dateTimeHelper;
            this._priceFormatter = priceFormatter;
            this._storeService = storeService;
            this._taxService = taxService;
            this._priceCalculationService = priceCalculationService;
            this._permissionService = permissionService;
            this._localizationService = localizationService;
            this._productAttributeFormatter = productAttributeFormatter;
        }

        #endregion

        #region Carts
        [HttpGet]
        [Route("Carts/{pageIndex:int=0}/{pageSize:int=2147483647}", Name = "CurrentCarts")]
        public HttpResponseMessage CurrentCarts(HttpRequestMessage request, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (true)
                {
                    var customers = _customerService.GetAllCustomers(
                                                                        loadOnlyWithShoppingCart: true,
                                                                        sct: ShoppingCartType.ShoppingCart,
                                                                        pageIndex: pageIndex,
                                                                        pageSize: pageSize
                                                                    );

                    var gridModel = new DataSourceResult
                    {
                        Data = customers.Select(x => new ShoppingCartVM
                        {
                            CustomerId = x.Id,
                            CustomerEmail = x.IsRegistered() ? x.Email : _localizationService.GetResource("Admin.Customers.Guest"),
                            TotalItems = x.ShoppingCartItems.Where(sci => sci.ShoppingCartType == ShoppingCartType.ShoppingCart).ToList().GetTotalProducts()
                        }),
                        Total = customers.TotalCount
                    };
                    response = request.CreateResponse<DataSourceResult>(HttpStatusCode.OK, gridModel);
                }
                else
                {
                    response = request.CreateResponse(HttpStatusCode.Unauthorized, "Unauthorized user");
                }
                return response;
            });
        }


        [HttpPost]
        [Route("GetCartDetails", Name = "GetCartDetails")]
        public HttpResponseMessage GetCartDetails(HttpRequestMessage request, int customerId)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (true)
                {
                    var customer = _customerService.GetCustomerById(customerId);
                    var cart = customer.ShoppingCartItems.Where(x => x.ShoppingCartType == ShoppingCartType.ShoppingCart).ToList();

                    var gridModel = new DataSourceResult
                    {
                        Data = cart.Select(sci =>
                        {
                            decimal taxRate;
                            var store = _storeService.GetStoreById(sci.StoreId);
                            var sciModel = new ShoppingCartItemVM
                            {
                                Id = sci.Id,
                                Store = store != null ? store.Name : "Unknown",
                                ProductId = sci.ProductId,
                                Quantity = sci.Quantity,
                                ProductName = sci.Product.Name,
                                AttributeInfo = _productAttributeFormatter.FormatAttributes(sci.Product, sci.AttributesXml, sci.Customer),
                                UnitPrice = _priceFormatter.FormatPrice(_taxService.GetProductPrice(sci.Product, _priceCalculationService.GetUnitPrice(sci), out taxRate)),
                                Total = _priceFormatter.FormatPrice(_taxService.GetProductPrice(sci.Product, _priceCalculationService.GetSubTotal(sci), out taxRate)),
                                UpdatedOn = _dateTimeHelper.ConvertToUserTime(sci.UpdatedOnUtc, DateTimeKind.Utc)
                            };
                            return sciModel;
                        }),
                        Total = cart.Count
                    };
                    response = request.CreateResponse<DataSourceResult>(HttpStatusCode.OK, gridModel);
                }
                else
                {
                    response = request.CreateResponse(HttpStatusCode.Unauthorized, "Unauthorized user");
                }
                return response;
            });
        }
        #endregion

        #region Wishlists
        [HttpGet]
        [Route("Wishlists/{pageIndex:int=0}/{pageSize:int=2147483647}", Name = "CurrentWishlists")]
        public HttpResponseMessage CurrentWishlists(HttpRequestMessage request, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (true)
                {
                    var customers = _customerService.GetAllCustomers(
                                                                        loadOnlyWithShoppingCart: true,
                                                                        sct: ShoppingCartType.Wishlist,
                                                                        pageIndex: pageIndex,
                                                                        pageSize: pageSize
                                                                    );

                    var gridModel = new DataSourceResult
                    {
                        Data = customers.Select(x => new ShoppingCartVM
                        {
                            CustomerId = x.Id,
                            CustomerEmail = x.IsRegistered() ? x.Email : _localizationService.GetResource("Admin.Customers.Guest"),
                            TotalItems = x.ShoppingCartItems.Where(sci => sci.ShoppingCartType == ShoppingCartType.Wishlist).ToList().GetTotalProducts()
                        }),
                        Total = customers.TotalCount
                    };
                    response = request.CreateResponse<DataSourceResult>(HttpStatusCode.OK, gridModel);
                }
                else
                {
                    response = request.CreateResponse(HttpStatusCode.Unauthorized, "Unauthorized user");
                }
                return response;
            });
        }


        [Route("GetWishlistDetails", Name = "GetWishlistDetails")]
        public HttpResponseMessage GetWishlistDetails(HttpRequestMessage request, int customerId)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (true)
                {
                    var customer = _customerService.GetCustomerById(customerId);
                    var cart = customer.ShoppingCartItems.Where(x => x.ShoppingCartType == ShoppingCartType.Wishlist).ToList();

                    var gridModel = new DataSourceResult
                    {
                        Data = cart.Select(sci =>
                        {
                            decimal taxRate;
                            var store = _storeService.GetStoreById(sci.StoreId);
                            var sciModel = new ShoppingCartItemVM
                            {
                                Id = sci.Id,
                                Store = store != null ? store.Name : "Unknown",
                                ProductId = sci.ProductId,
                                Quantity = sci.Quantity,
                                ProductName = sci.Product.Name,
                                AttributeInfo = _productAttributeFormatter.FormatAttributes(sci.Product, sci.AttributesXml, sci.Customer),
                                UnitPrice = _priceFormatter.FormatPrice(_taxService.GetProductPrice(sci.Product, _priceCalculationService.GetUnitPrice(sci), out taxRate)),
                                Total = _priceFormatter.FormatPrice(_taxService.GetProductPrice(sci.Product, _priceCalculationService.GetSubTotal(sci), out taxRate)),
                                UpdatedOn = _dateTimeHelper.ConvertToUserTime(sci.UpdatedOnUtc, DateTimeKind.Utc)
                            };
                            return sciModel;
                        }),
                        Total = cart.Count
                    };
                    response = request.CreateResponse<DataSourceResult>(HttpStatusCode.OK, gridModel);
                }
                else
                {
                    response = request.CreateResponse(HttpStatusCode.Unauthorized, "Unauthorized user");
                }
                return response;
            });
        }
        #endregion
    }
}
