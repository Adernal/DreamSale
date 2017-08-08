using Denmakers.DreamSale.Data.Context;
using Denmakers.DreamSale.Helpers;
using Denmakers.DreamSale.Model.Catalog;
using Denmakers.DreamSale.Model.Common;
using Denmakers.DreamSale.Model.Customers;
using Denmakers.DreamSale.Model.Directory;
using Denmakers.DreamSale.Model.Orders;
using Denmakers.DreamSale.Model.Payments;
using Denmakers.DreamSale.Model.Shipping;
using Denmakers.DreamSale.Model.Tax;
using Denmakers.DreamSale.RESTAPI.Infrastructure;
using Denmakers.DreamSale.Services;
using Denmakers.DreamSale.Services.Addresses;
using Denmakers.DreamSale.Services.Affiliates;
using Denmakers.DreamSale.Services.Catalog;
using Denmakers.DreamSale.Services.Categories;
using Denmakers.DreamSale.Services.Configuration;
using Denmakers.DreamSale.Services.Directory;
using Denmakers.DreamSale.Services.Discounts;
using Denmakers.DreamSale.Services.Helpers;
using Denmakers.DreamSale.Services.Localization;
using Denmakers.DreamSale.Services.Logging;
using Denmakers.DreamSale.Services.Manufacturers;
using Denmakers.DreamSale.Services.Media;
using Denmakers.DreamSale.Services.Orders;
using Denmakers.DreamSale.Services.Payments;
using Denmakers.DreamSale.Services.Products;
using Denmakers.DreamSale.Services.Security;
using Denmakers.DreamSale.Services.Shipping;
using Denmakers.DreamSale.Services.Stores;
using Denmakers.DreamSale.Services.Tax;
using Denmakers.DreamSale.Services.Vendors;
using Denmakers.DreamSale.ViewModels.AdminVM.Orders;
using Denmakers.DreamSale.ViewModels.Mapper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;

namespace Denmakers.DreamSale.RESTAPI.Controllers
{
    [RoutePrefix("api/Orders")]
    public class OrderController : ApiControllerBase
    {
        #region Fields

        private readonly IOrderService _orderService;
        private readonly IOrderReportService _orderReportService;
        private readonly IOrderProcessingService _orderProcessingService;
        private readonly IReturnRequestService _returnRequestService;
        private readonly IPriceCalculationService _priceCalculationService;
        private readonly ITaxService _taxService;
        private readonly IDateTimeHelper _dateTimeHelper;
        private readonly IPriceFormatter _priceFormatter;
        private readonly IDiscountService _discountService;
        private readonly ILocalizationService _localizationService;
        private readonly IWorkContext _workContext;
        private readonly ICurrencyService _currencyService;
        private readonly IEncryptionService _encryptionService;
        private readonly IPaymentService _paymentService;
        private readonly IMeasureService _measureService;
        //private readonly IPdfService _pdfService;
        private readonly IAddressService _addressService;
        private readonly ICountryService _countryService;
        private readonly IStateProvinceService _stateProvinceService;
        private readonly IProductService _productService;
        //private readonly IExportManager _exportManager;
        private readonly IPermissionService _permissionService;
        //private readonly IWorkflowMessageService _workflowMessageService;
        private readonly ICategoryService _categoryService;
        private readonly IManufacturerService _manufacturerService;
        private readonly IProductAttributeService _productAttributeService;
        private readonly IProductAttributeParser _productAttributeParser;
        private readonly IProductAttributeFormatter _productAttributeFormatter;
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IGiftCardService _giftCardService;
        private readonly IDownloadService _downloadService;
        private readonly IShipmentService _shipmentService;
        private readonly IShippingService _shippingService;
        private readonly IStoreService _storeService;
        private readonly IVendorService _vendorService;
        private readonly IAddressAttributeParser _addressAttributeParser;
        private readonly IAddressAttributeService _addressAttributeService;
        private readonly IAddressAttributeFormatter _addressAttributeFormatter;
        private readonly IAffiliateService _affiliateService;
        private readonly IPictureService _pictureService;
        private readonly ICustomerActivityService _customerActivityService;
        //private readonly ICacheManager _cacheManager;

        private readonly ISettingService _settingService;
        private readonly OrderSettings _orderSettings;
        private readonly CurrencySettings _currencySettings;
        private readonly TaxSettings _taxSettings;
        private readonly MeasureSettings _measureSettings;
        private readonly AddressSettings _addressSettings;
        private readonly ShippingSettings _shippingSettings;

        #endregion

        #region Ctor

        public OrderController(IBaseService baseService, ILogger logger, IWebHelper webHelper,
            IOrderService orderService,
            IOrderReportService orderReportService,
            IOrderProcessingService orderProcessingService,
            IReturnRequestService returnRequestService,
            IPriceCalculationService priceCalculationService,
            ITaxService taxService,
            IDateTimeHelper dateTimeHelper,
            IPriceFormatter priceFormatter,
            IDiscountService discountService,
            ILocalizationService localizationService,
            ICurrencyService currencyService,
            IEncryptionService encryptionService,
            IPaymentService paymentService,
            IMeasureService measureService,
            //IPdfService pdfService,
            IAddressService addressService,
            ICountryService countryService,
            IStateProvinceService stateProvinceService,
            IProductService productService,
            //IExportManager exportManager,
            IPermissionService permissionService,
            //IWorkflowMessageService workflowMessageService,
            ICategoryService categoryService,
            IManufacturerService manufacturerService,
            IProductAttributeService productAttributeService,
            IProductAttributeParser productAttributeParser,
            IProductAttributeFormatter productAttributeFormatter,
            IShoppingCartService shoppingCartService,
            IGiftCardService giftCardService,
            IDownloadService downloadService,
            IShipmentService shipmentService,
            IShippingService shippingService,
            IStoreService storeService,
            IVendorService vendorService,
            IAddressAttributeParser addressAttributeParser,
            IAddressAttributeService addressAttributeService,
            IAddressAttributeFormatter addressAttributeFormatter,
            IAffiliateService affiliateService,
            IPictureService pictureService,
            ICustomerActivityService customerActivityService,
            ISettingService settingService
            //ICacheManager cacheManager
            )
            : base(baseService, logger, webHelper)
        {
            this._orderService = orderService;
            this._orderReportService = orderReportService;
            this._orderProcessingService = orderProcessingService;
            this._returnRequestService = returnRequestService;
            this._priceCalculationService = priceCalculationService;
            this._taxService = taxService;
            this._dateTimeHelper = dateTimeHelper;
            this._priceFormatter = priceFormatter;
            this._discountService = discountService;
            this._localizationService = localizationService;
            this._workContext = base._baseService.WorkContext;
            this._currencyService = currencyService;
            this._encryptionService = encryptionService;
            this._paymentService = paymentService;
            this._measureService = measureService;
            //this._pdfService = pdfService;
            this._addressService = addressService;
            this._countryService = countryService;
            this._stateProvinceService = stateProvinceService;
            this._productService = productService;
            //this._exportManager = exportManager;
            this._permissionService = permissionService;
            //this._workflowMessageService = workflowMessageService;
            this._categoryService = categoryService;
            this._manufacturerService = manufacturerService;
            this._productAttributeService = productAttributeService;
            this._productAttributeParser = productAttributeParser;
            this._productAttributeFormatter = productAttributeFormatter;
            this._shoppingCartService = shoppingCartService;
            this._giftCardService = giftCardService;
            this._downloadService = downloadService;
            this._shipmentService = shipmentService;
            this._shippingService = shippingService;
            this._storeService = storeService;
            this._vendorService = vendorService;
            this._addressAttributeParser = addressAttributeParser;
            this._addressAttributeService = addressAttributeService;
            this._addressAttributeFormatter = addressAttributeFormatter;
            this._affiliateService = affiliateService;
            this._pictureService = pictureService;
            this._customerActivityService = customerActivityService;
            //this._cacheManager = cacheManager;
            this._settingService = settingService;
            this._orderSettings = _settingService.LoadSetting<OrderSettings>();
            this._currencySettings = _settingService.LoadSetting<CurrencySettings>();
            this._taxSettings = _settingService.LoadSetting<TaxSettings>();
            this._measureSettings = _settingService.LoadSetting<MeasureSettings>();
            this._addressSettings = _settingService.LoadSetting<AddressSettings>();
            this._shippingSettings = _settingService.LoadSetting<ShippingSettings>();
        }

        #endregion

        #region Utilities
        #region Activity log
        [NonAction]
        protected virtual void LogEditOrder(int orderId)
        {
            var order = _orderService.GetOrderById(orderId);

            _customerActivityService.InsertActivity("EditOrder", _localizationService.GetResource("ActivityLog.EditOrder"), order.CustomOrderNumber);

            _baseService.Commit();
        }
        #endregion

        [NonAction]
        protected virtual DataSourceResult GetBestsellersBriefReportModel(int pageIndex, int pageSize, int orderBy)
        {
            //a vendor should have access only to his products
            int vendorId = 0;
            if (_workContext.CurrentVendor != null)
                vendorId = _workContext.CurrentVendor.Id;

            var items = _orderReportService.BestSellersReport(
                vendorId: vendorId,
                orderBy: orderBy,
                pageIndex: pageIndex,
                pageSize: pageSize,
                showHidden: true);
            var gridModel = new DataSourceResult
            {
                Data = items.Select(x =>
                {
                    var m = new BestsellersReportLineVM
                    {
                        ProductId = x.ProductId,
                        TotalAmount = _priceFormatter.FormatPrice(x.TotalAmount, true, false),
                        TotalQuantity = x.TotalQuantity,
                    };
                    var product = _productService.GetProductById(x.ProductId);
                    if (product != null)
                        m.ProductName = product.Name;
                    return m;
                }),
                Total = items.TotalCount
            };
            return gridModel;
        }


        [NonAction]
        protected virtual bool HasAccessToOrder(Order order)
        {
            if (order == null)
                throw new ArgumentNullException("order");

            if (_workContext.CurrentVendor == null)
                //not a vendor; has access
                return true;

            var vendorId = _workContext.CurrentVendor.Id;
            var hasVendorProducts = order.OrderItems.Any(orderItem => orderItem.Product.VendorId == vendorId);
            return hasVendorProducts;
        }

        [NonAction]
        protected virtual bool HasAccessToOrderItem(OrderItem orderItem)
        {
            if (orderItem == null)
                throw new ArgumentNullException("orderItem");

            if (_workContext.CurrentVendor == null)
                //not a vendor; has access
                return true;

            var vendorId = _workContext.CurrentVendor.Id;
            return orderItem.Product.VendorId == vendorId;
        }

        [NonAction]
        protected virtual bool HasAccessToProduct(Product product)
        {
            if (product == null)
                throw new ArgumentNullException("product");

            if (_workContext.CurrentVendor == null)
                //not a vendor; has access
                return true;

            var vendorId = _workContext.CurrentVendor.Id;
            return product.VendorId == vendorId;
        }

        [NonAction]
        protected virtual bool HasAccessToShipment(Shipment shipment)
        {
            if (shipment == null)
                throw new ArgumentNullException("shipment");

            if (_workContext.CurrentVendor == null)
                //not a vendor; has access
                return true;

            var hasVendorProducts = false;
            var vendorId = _workContext.CurrentVendor.Id;
            foreach (var shipmentItem in shipment.ShipmentItems)
            {
                var orderItem = _orderService.GetOrderItemById(shipmentItem.OrderItemId);
                if (orderItem != null)
                {
                    if (orderItem.Product.VendorId == vendorId)
                    {
                        hasVendorProducts = true;
                        break;
                    }
                }
            }
            return hasVendorProducts;
        }

        [NonAction]
        protected virtual void PrepareOrderDetailsModel(OrderVM model, Order order)
        {
            if (order == null)
                throw new ArgumentNullException("order");

            if (model == null)
                throw new ArgumentNullException("model");

            model.Id = order.Id;
            model.OrderStatus = order.OrderStatus.GetLocalizedEnum(_localizationService, _workContext);
            model.OrderStatusId = order.OrderStatusId;
            model.OrderGuid = order.OrderGuid;
            model.CustomOrderNumber = order.CustomOrderNumber;
            var store = _storeService.GetStoreById(order.StoreId);
            model.StoreName = store != null ? store.Name : "Unknown";
            model.CustomerId = order.CustomerId;
            var customer = order.Customer;
            model.CustomerInfo = customer.IsRegistered() ? customer.Email : _localizationService.GetResource("Admin.Customers.Guest");
            model.CustomerIp = order.CustomerIp;
            model.VatNumber = order.VatNumber;
            model.CreatedOn = _dateTimeHelper.ConvertToUserTime(order.CreatedOnUtc, DateTimeKind.Utc);
            model.AllowCustomersToSelectTaxDisplayType = _taxSettings.AllowCustomersToSelectTaxDisplayType;
            model.TaxDisplayType = _taxSettings.TaxDisplayType;

            var affiliate = _affiliateService.GetAffiliateById(order.AffiliateId);
            if (affiliate != null)
            {
                model.AffiliateId = affiliate.Id;
                model.AffiliateName = affiliate.GetFullName();
            }

            //a vendor should have access only to his products
            model.IsLoggedInAsVendor = _workContext.CurrentVendor != null;
            //custom values
            model.CustomValues = order.DeserializeCustomValues();

            #region Order totals

            var primaryStoreCurrency = _currencyService.GetCurrencyById(_currencySettings.PrimaryStoreCurrencyId);
            if (primaryStoreCurrency == null)
                throw new Exception("Cannot load primary store currency");

            //subtotal
            model.OrderSubtotalInclTax = _priceFormatter.FormatPrice(order.OrderSubtotalInclTax, true, primaryStoreCurrency, _workContext.WorkingLanguage, true);
            model.OrderSubtotalExclTax = _priceFormatter.FormatPrice(order.OrderSubtotalExclTax, true, primaryStoreCurrency, _workContext.WorkingLanguage, false);
            model.OrderSubtotalInclTaxValue = order.OrderSubtotalInclTax;
            model.OrderSubtotalExclTaxValue = order.OrderSubtotalExclTax;
            //discount (applied to order subtotal)
            string orderSubtotalDiscountInclTaxStr = _priceFormatter.FormatPrice(order.OrderSubTotalDiscountInclTax, true, primaryStoreCurrency, _workContext.WorkingLanguage, true);
            string orderSubtotalDiscountExclTaxStr = _priceFormatter.FormatPrice(order.OrderSubTotalDiscountExclTax, true, primaryStoreCurrency, _workContext.WorkingLanguage, false);
            if (order.OrderSubTotalDiscountInclTax > decimal.Zero)
                model.OrderSubTotalDiscountInclTax = orderSubtotalDiscountInclTaxStr;
            if (order.OrderSubTotalDiscountExclTax > decimal.Zero)
                model.OrderSubTotalDiscountExclTax = orderSubtotalDiscountExclTaxStr;
            model.OrderSubTotalDiscountInclTaxValue = order.OrderSubTotalDiscountInclTax;
            model.OrderSubTotalDiscountExclTaxValue = order.OrderSubTotalDiscountExclTax;

            //shipping
            model.OrderShippingInclTax = _priceFormatter.FormatShippingPrice(order.OrderShippingInclTax, true, primaryStoreCurrency, _workContext.WorkingLanguage, true);
            model.OrderShippingExclTax = _priceFormatter.FormatShippingPrice(order.OrderShippingExclTax, true, primaryStoreCurrency, _workContext.WorkingLanguage, false);
            model.OrderShippingInclTaxValue = order.OrderShippingInclTax;
            model.OrderShippingExclTaxValue = order.OrderShippingExclTax;

            //payment method additional fee
            if (order.PaymentMethodAdditionalFeeInclTax > decimal.Zero)
            {
                model.PaymentMethodAdditionalFeeInclTax = _priceFormatter.FormatPaymentMethodAdditionalFee(order.PaymentMethodAdditionalFeeInclTax, true, primaryStoreCurrency, _workContext.WorkingLanguage, true);
                model.PaymentMethodAdditionalFeeExclTax = _priceFormatter.FormatPaymentMethodAdditionalFee(order.PaymentMethodAdditionalFeeExclTax, true, primaryStoreCurrency, _workContext.WorkingLanguage, false);
            }
            model.PaymentMethodAdditionalFeeInclTaxValue = order.PaymentMethodAdditionalFeeInclTax;
            model.PaymentMethodAdditionalFeeExclTaxValue = order.PaymentMethodAdditionalFeeExclTax;


            //tax
            model.Tax = _priceFormatter.FormatPrice(order.OrderTax, true, false);
            SortedDictionary<decimal, decimal> taxRates = order.TaxRatesDictionary;
            bool displayTaxRates = _taxSettings.DisplayTaxRates && taxRates.Any();
            bool displayTax = !displayTaxRates;
            foreach (var tr in order.TaxRatesDictionary)
            {
                model.TaxRates.Add(new OrderVM.TaxRate
                {
                    Rate = _priceFormatter.FormatTaxRate(tr.Key),
                    Value = _priceFormatter.FormatPrice(tr.Value, true, false),
                });
            }
            model.DisplayTaxRates = displayTaxRates;
            model.DisplayTax = displayTax;
            model.TaxValue = order.OrderTax;
            model.TaxRatesValue = order.TaxRates;

            //discount
            if (order.OrderDiscount > 0)
                model.OrderTotalDiscount = _priceFormatter.FormatPrice(-order.OrderDiscount, true, false);
            model.OrderTotalDiscountValue = order.OrderDiscount;

            //gift cards
            foreach (var gcuh in order.GiftCardUsageHistory)
            {
                model.GiftCards.Add(new OrderVM.GiftCard
                {
                    CouponCode = gcuh.GiftCard.GiftCardCouponCode,
                    Amount = _priceFormatter.FormatPrice(-gcuh.UsedValue, true, false),
                });
            }

            //reward points
            if (order.RedeemedRewardPointsEntry != null)
            {
                model.RedeemedRewardPoints = -order.RedeemedRewardPointsEntry.Points;
                model.RedeemedRewardPointsAmount = _priceFormatter.FormatPrice(-order.RedeemedRewardPointsEntry.UsedAmount, true, false);
            }

            //total
            model.OrderTotal = _priceFormatter.FormatPrice(order.OrderTotal, true, false);
            model.OrderTotalValue = order.OrderTotal;

            //refunded amount
            if (order.RefundedAmount > decimal.Zero)
                model.RefundedAmount = _priceFormatter.FormatPrice(order.RefundedAmount, true, false);

            //used discounts
            var duh = _discountService.GetAllDiscountUsageHistory(orderId: order.Id);
            foreach (var d in duh)
            {
                model.UsedDiscounts.Add(new OrderVM.UsedDiscountVM
                {
                    DiscountId = d.DiscountId,
                    DiscountName = d.Discount.Name
                });
            }

            //profit (hide for vendors)
            if (_workContext.CurrentVendor == null)
            {
                var profit = _orderReportService.ProfitReport(orderId: order.Id);
                model.Profit = _priceFormatter.FormatPrice(profit, true, false);
            }

            #endregion

            #region Payment info

            if (order.AllowStoringCreditCardNumber)
            {
                //card type
                model.CardType = _encryptionService.DecryptText(order.CardType);
                //cardholder name
                model.CardName = _encryptionService.DecryptText(order.CardName);
                //card number
                model.CardNumber = _encryptionService.DecryptText(order.CardNumber);
                //cvv
                model.CardCvv2 = _encryptionService.DecryptText(order.CardCvv2);
                //expiry date
                string cardExpirationMonthDecrypted = _encryptionService.DecryptText(order.CardExpirationMonth);
                if (!String.IsNullOrEmpty(cardExpirationMonthDecrypted) && cardExpirationMonthDecrypted != "0")
                    model.CardExpirationMonth = cardExpirationMonthDecrypted;
                string cardExpirationYearDecrypted = _encryptionService.DecryptText(order.CardExpirationYear);
                if (!String.IsNullOrEmpty(cardExpirationYearDecrypted) && cardExpirationYearDecrypted != "0")
                    model.CardExpirationYear = cardExpirationYearDecrypted;

                model.AllowStoringCreditCardNumber = true;
            }
            else
            {
                string maskedCreditCardNumberDecrypted = _encryptionService.DecryptText(order.MaskedCreditCardNumber);
                if (!String.IsNullOrEmpty(maskedCreditCardNumberDecrypted))
                    model.CardNumber = maskedCreditCardNumberDecrypted;
            }


            //payment transaction info
            model.AuthorizationTransactionId = order.AuthorizationTransactionId;
            model.CaptureTransactionId = order.CaptureTransactionId;
            model.SubscriptionTransactionId = order.SubscriptionTransactionId;

            ////payment method info
            //var pm = _paymentService.LoadPaymentMethodBySystemName(order.PaymentMethodSystemName);
            //model.PaymentMethod = pm != null ? pm.PluginDescriptor.FriendlyName : order.PaymentMethodSystemName;
            model.PaymentStatus = order.PaymentStatus.GetLocalizedEnum(_localizationService, _workContext);

            //payment method buttons
            model.CanCancelOrder = _orderProcessingService.CanCancelOrder(order);
            model.CanCapture = _orderProcessingService.CanCapture(order);
            model.CanMarkOrderAsPaid = _orderProcessingService.CanMarkOrderAsPaid(order);
            model.CanRefund = _orderProcessingService.CanRefund(order);
            model.CanRefundOffline = _orderProcessingService.CanRefundOffline(order);
            model.CanPartiallyRefund = _orderProcessingService.CanPartiallyRefund(order, decimal.Zero);
            model.CanPartiallyRefundOffline = _orderProcessingService.CanPartiallyRefundOffline(order, decimal.Zero);
            model.CanVoid = _orderProcessingService.CanVoid(order);
            model.CanVoidOffline = _orderProcessingService.CanVoidOffline(order);

            model.PrimaryStoreCurrencyCode = _currencyService.GetCurrencyById(_currencySettings.PrimaryStoreCurrencyId).CurrencyCode;
            model.MaxAmountToRefund = order.OrderTotal - order.RefundedAmount;

            //recurring payment record
            var recurringPayment = _orderService.SearchRecurringPayments(initialOrderId: order.Id, showHidden: true).FirstOrDefault();
            if (recurringPayment != null)
            {
                model.RecurringPaymentId = recurringPayment.Id;
            }
            #endregion

            #region Billing & shipping info

            model.BillingAddress = order.BillingAddress.ToModel();
            model.BillingAddress.FormattedCustomAddressAttributes = _addressAttributeFormatter.FormatAttributes(order.BillingAddress.CustomAttributes);
            model.BillingAddress.FirstNameEnabled = true;
            model.BillingAddress.FirstNameRequired = true;
            model.BillingAddress.LastNameEnabled = true;
            model.BillingAddress.LastNameRequired = true;
            model.BillingAddress.EmailEnabled = true;
            model.BillingAddress.EmailRequired = true;
            model.BillingAddress.CompanyEnabled = _addressSettings.CompanyEnabled;
            model.BillingAddress.CompanyRequired = _addressSettings.CompanyRequired;
            model.BillingAddress.CountryEnabled = _addressSettings.CountryEnabled;
            model.BillingAddress.CountryRequired = _addressSettings.CountryEnabled; //country is required when enabled
            model.BillingAddress.StateProvinceEnabled = _addressSettings.StateProvinceEnabled;
            model.BillingAddress.CityEnabled = _addressSettings.CityEnabled;
            model.BillingAddress.CityRequired = _addressSettings.CityRequired;
            model.BillingAddress.StreetAddressEnabled = _addressSettings.StreetAddressEnabled;
            model.BillingAddress.StreetAddressRequired = _addressSettings.StreetAddressRequired;
            model.BillingAddress.StreetAddress2Enabled = _addressSettings.StreetAddress2Enabled;
            model.BillingAddress.StreetAddress2Required = _addressSettings.StreetAddress2Required;
            model.BillingAddress.ZipPostalCodeEnabled = _addressSettings.ZipPostalCodeEnabled;
            model.BillingAddress.ZipPostalCodeRequired = _addressSettings.ZipPostalCodeRequired;
            model.BillingAddress.PhoneEnabled = _addressSettings.PhoneEnabled;
            model.BillingAddress.PhoneRequired = _addressSettings.PhoneRequired;
            model.BillingAddress.FaxEnabled = _addressSettings.FaxEnabled;
            model.BillingAddress.FaxRequired = _addressSettings.FaxRequired;

            model.ShippingStatus = order.ShippingStatus.GetLocalizedEnum(_localizationService, _workContext); ;
            if (order.ShippingStatus != ShippingStatus.ShippingNotRequired)
            {
                model.IsShippable = true;

                model.PickUpInStore = order.PickUpInStore;
                if (!order.PickUpInStore)
                {
                    model.ShippingAddress = order.ShippingAddress.ToModel();
                    model.ShippingAddress.FormattedCustomAddressAttributes = _addressAttributeFormatter.FormatAttributes(order.ShippingAddress.CustomAttributes);
                    model.ShippingAddress.FirstNameEnabled = true;
                    model.ShippingAddress.FirstNameRequired = true;
                    model.ShippingAddress.LastNameEnabled = true;
                    model.ShippingAddress.LastNameRequired = true;
                    model.ShippingAddress.EmailEnabled = true;
                    model.ShippingAddress.EmailRequired = true;
                    model.ShippingAddress.CompanyEnabled = _addressSettings.CompanyEnabled;
                    model.ShippingAddress.CompanyRequired = _addressSettings.CompanyRequired;
                    model.ShippingAddress.CountryEnabled = _addressSettings.CountryEnabled;
                    model.ShippingAddress.CountryRequired = _addressSettings.CountryEnabled; //country is required when enabled
                    model.ShippingAddress.StateProvinceEnabled = _addressSettings.StateProvinceEnabled;
                    model.ShippingAddress.CityEnabled = _addressSettings.CityEnabled;
                    model.ShippingAddress.CityRequired = _addressSettings.CityRequired;
                    model.ShippingAddress.StreetAddressEnabled = _addressSettings.StreetAddressEnabled;
                    model.ShippingAddress.StreetAddressRequired = _addressSettings.StreetAddressRequired;
                    model.ShippingAddress.StreetAddress2Enabled = _addressSettings.StreetAddress2Enabled;
                    model.ShippingAddress.StreetAddress2Required = _addressSettings.StreetAddress2Required;
                    model.ShippingAddress.ZipPostalCodeEnabled = _addressSettings.ZipPostalCodeEnabled;
                    model.ShippingAddress.ZipPostalCodeRequired = _addressSettings.ZipPostalCodeRequired;
                    model.ShippingAddress.PhoneEnabled = _addressSettings.PhoneEnabled;
                    model.ShippingAddress.PhoneRequired = _addressSettings.PhoneRequired;
                    model.ShippingAddress.FaxEnabled = _addressSettings.FaxEnabled;
                    model.ShippingAddress.FaxRequired = _addressSettings.FaxRequired;

                    model.ShippingAddressGoogleMapsUrl = string.Format("http://maps.google.com/maps?f=q&hl=en&ie=UTF8&oe=UTF8&geocode=&q={0}", HttpUtility.UrlEncode(order.ShippingAddress.Address1 + " " + order.ShippingAddress.ZipPostalCode + " " + order.ShippingAddress.City + " " + (order.ShippingAddress.Country != null ? order.ShippingAddress.Country.Name : "")));
                }
                else
                {
                    if (order.PickupAddress != null)
                    {
                        model.PickupAddress = order.PickupAddress.ToModel();
                        model.PickupAddressGoogleMapsUrl = string.Format("http://maps.google.com/maps?f=q&hl=en&ie=UTF8&oe=UTF8&geocode=&q={0}",
                            HttpUtility.UrlEncode(string.Format("{0} {1} {2} {3}", order.PickupAddress.Address1, order.PickupAddress.ZipPostalCode, order.PickupAddress.City,
                                order.PickupAddress.Country != null ? order.PickupAddress.Country.Name : string.Empty)));
                    }
                }
                model.ShippingMethod = order.ShippingMethod;

                model.CanAddNewShipments = order.HasItemsToAddToShipment();
            }

            #endregion

            #region Products

            model.CheckoutAttributeInfo = order.CheckoutAttributeDescription;
            bool hasDownloadableItems = false;
            var products = order.OrderItems;
            //a vendor should have access only to his products
            if (_workContext.CurrentVendor != null)
            {
                products = products
                    .Where(orderItem => orderItem.Product.VendorId == _workContext.CurrentVendor.Id)
                    .ToList();
            }
            foreach (var orderItem in products)
            {
                if (orderItem.Product.IsDownload)
                    hasDownloadableItems = true;

                var orderItemModel = new OrderVM.OrderItemVM
                {
                    Id = orderItem.Id,
                    ProductId = orderItem.ProductId,
                    ProductName = orderItem.Product.Name,
                    Sku = orderItem.Product.FormatSku(orderItem.AttributesXml, _productAttributeParser),
                    Quantity = orderItem.Quantity,
                    IsDownload = orderItem.Product.IsDownload,
                    DownloadCount = orderItem.DownloadCount,
                    DownloadActivationType = orderItem.Product.DownloadActivationType,
                    IsDownloadActivated = orderItem.IsDownloadActivated
                };
                //picture
                var orderItemPicture = orderItem.Product.GetProductPicture(orderItem.AttributesXml, _pictureService, _productAttributeParser);
                orderItemModel.PictureThumbnailUrl = _pictureService.GetPictureUrl(orderItemPicture, 75, true);

                //license file
                if (orderItem.LicenseDownloadId.HasValue)
                {
                    var licenseDownload = _downloadService.GetDownloadById(orderItem.LicenseDownloadId.Value);
                    if (licenseDownload != null)
                    {
                        orderItemModel.LicenseDownloadGuid = licenseDownload.DownloadGuid;
                    }
                }
                //vendor
                var vendor = _vendorService.GetVendorById(orderItem.Product.VendorId);
                orderItemModel.VendorName = vendor != null ? vendor.Name : "";

                //unit price
                orderItemModel.UnitPriceInclTaxValue = orderItem.UnitPriceInclTax;
                orderItemModel.UnitPriceExclTaxValue = orderItem.UnitPriceExclTax;
                orderItemModel.UnitPriceInclTax = _priceFormatter.FormatPrice(orderItem.UnitPriceInclTax, true, primaryStoreCurrency, _workContext.WorkingLanguage, true, true);
                orderItemModel.UnitPriceExclTax = _priceFormatter.FormatPrice(orderItem.UnitPriceExclTax, true, primaryStoreCurrency, _workContext.WorkingLanguage, false, true);
                //discounts
                orderItemModel.DiscountInclTaxValue = orderItem.DiscountAmountInclTax;
                orderItemModel.DiscountExclTaxValue = orderItem.DiscountAmountExclTax;
                orderItemModel.DiscountInclTax = _priceFormatter.FormatPrice(orderItem.DiscountAmountInclTax, true, primaryStoreCurrency, _workContext.WorkingLanguage, true, true);
                orderItemModel.DiscountExclTax = _priceFormatter.FormatPrice(orderItem.DiscountAmountExclTax, true, primaryStoreCurrency, _workContext.WorkingLanguage, false, true);
                //subtotal
                orderItemModel.SubTotalInclTaxValue = orderItem.PriceInclTax;
                orderItemModel.SubTotalExclTaxValue = orderItem.PriceExclTax;
                orderItemModel.SubTotalInclTax = _priceFormatter.FormatPrice(orderItem.PriceInclTax, true, primaryStoreCurrency, _workContext.WorkingLanguage, true, true);
                orderItemModel.SubTotalExclTax = _priceFormatter.FormatPrice(orderItem.PriceExclTax, true, primaryStoreCurrency, _workContext.WorkingLanguage, false, true);

                orderItemModel.AttributeInfo = orderItem.AttributeDescription;
                if (orderItem.Product.IsRecurring)
                    orderItemModel.RecurringInfo = string.Format(_localizationService.GetResource("Admin.Orders.Products.RecurringPeriod"), orderItem.Product.RecurringCycleLength, orderItem.Product.RecurringCyclePeriod.GetLocalizedEnum(_localizationService, _workContext));
                //rental info
                if (orderItem.Product.IsRental)
                {
                    var rentalStartDate = orderItem.RentalStartDateUtc.HasValue ? orderItem.Product.FormatRentalDate(orderItem.RentalStartDateUtc.Value) : "";
                    var rentalEndDate = orderItem.RentalEndDateUtc.HasValue ? orderItem.Product.FormatRentalDate(orderItem.RentalEndDateUtc.Value) : "";
                    orderItemModel.RentalInfo = string.Format(_localizationService.GetResource("Order.Rental.FormattedDate"),
                        rentalStartDate, rentalEndDate);
                }

                //return requests
                orderItemModel.ReturnRequests = _returnRequestService
                    .SearchReturnRequests(orderItemId: orderItem.Id)
                    .Select(item => new OrderVM.OrderItemVM.ReturnRequestBriefVM
                    {
                        CustomNumber = item.CustomNumber,
                        Id = item.Id
                    }).ToList();

                //gift cards
                orderItemModel.PurchasedGiftCardIds = _giftCardService.GetGiftCardsByPurchasedWithOrderItemId(orderItem.Id)
                    .Select(gc => gc.Id).ToList();

                model.Items.Add(orderItemModel);
            }
            model.HasDownloadableProducts = hasDownloadableItems;
            #endregion
        }
        #endregion

        #region Orders List
        [HttpGet]
        [Route("", Name = "OrderDefaultModel")]
        public HttpResponseMessage OrderDeafultModel(HttpRequestMessage request, List<string> orderStatusIds = null, List<string> paymentStatusIds = null, List<string> shippingStatusIds = null)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (true)
                {
                    //order statuses
                    var model = new OrderListVM();
                    string allText = _localizationService.GetResource("Admin.Common.All");
                    model.AvailableOrderStatuses = OrderStatus.Pending.ToSelectList(_localizationService, _baseService.WorkContext, false).ToList();
                    model.AvailableOrderStatuses.Insert(0, new System.Web.Mvc.SelectListItem
                    { Text = allText, Value = "0", Selected = true });
                    if (orderStatusIds != null && orderStatusIds.Any())
                    {
                        foreach (var item in model.AvailableOrderStatuses.Where(os => orderStatusIds.Contains(os.Value)))
                            item.Selected = true;
                        model.AvailableOrderStatuses.First().Selected = false;
                    }
                    //payment statuses
                    model.AvailablePaymentStatuses = PaymentStatus.Pending.ToSelectList(_localizationService, _baseService.WorkContext, false).ToList();
                    model.AvailablePaymentStatuses.Insert(0, new System.Web.Mvc.SelectListItem
                    { Text = allText, Value = "0", Selected = true });
                    if (paymentStatusIds != null && paymentStatusIds.Any())
                    {
                        foreach (var item in model.AvailablePaymentStatuses.Where(ps => paymentStatusIds.Contains(ps.Value)))
                            item.Selected = true;
                        model.AvailablePaymentStatuses.First().Selected = false;
                    }

                    //shipping statuses
                    model.AvailableShippingStatuses = ShippingStatus.NotYetShipped.ToSelectList(_localizationService, _baseService.WorkContext, false).ToList();
                    model.AvailableShippingStatuses.Insert(0, new System.Web.Mvc.SelectListItem
                    { Text = allText, Value = "0", Selected = true });
                    if (shippingStatusIds != null && shippingStatusIds.Any())
                    {
                        foreach (var item in model.AvailableShippingStatuses.Where(ss => shippingStatusIds.Contains(ss.Value)))
                            item.Selected = true;
                        model.AvailableShippingStatuses.First().Selected = false;
                    }

                    //stores
                    model.AvailableStores.Add(new System.Web.Mvc.SelectListItem { Text = allText, Value = "0" });
                    foreach (var s in _storeService.GetAllStores())
                        model.AvailableStores.Add(new System.Web.Mvc.SelectListItem { Text = s.Name, Value = s.Id.ToString() });

                    //vendors
                    model.AvailableVendors.Add(new System.Web.Mvc.SelectListItem { Text = allText, Value = "0" });
                    var vendors = SelectListHelper.GetVendorList(_vendorService, true);
                    foreach (var v in vendors)
                        model.AvailableVendors.Add(v);

                    //warehouses
                    model.AvailableWarehouses.Add(new System.Web.Mvc.SelectListItem { Text = allText, Value = "0" });
                    foreach (var w in _shippingService.GetAllWarehouses())
                        model.AvailableWarehouses.Add(new System.Web.Mvc.SelectListItem { Text = w.Name, Value = w.Id.ToString() });

                    //payment methods
                    //model.AvailablePaymentMethods.Add(new System.Web.Mvc.SelectListItem { Text = allText, Value = "" });
                    //foreach (var pm in _paymentService.LoadAllPaymentMethods())
                    //    model.AvailablePaymentMethods.Add(new System.Web.Mvc.SelectListItem { Text = pm.PluginDescriptor.FriendlyName, Value = pm.PluginDescriptor.SystemName });

                    //billing countries
                    foreach (var c in _countryService.GetAllCountriesForBilling(showHidden: true))
                    {
                        model.AvailableCountries.Add(new System.Web.Mvc.SelectListItem { Text = c.Name, Value = c.Id.ToString() });
                    }
                    model.AvailableCountries.Insert(0, new System.Web.Mvc.SelectListItem { Text = allText, Value = "0" });

                    //a vendor should have access only to orders with his products
                    model.IsLoggedInAsVendor = _workContext.CurrentVendor != null;

                    response = request.CreateResponse<OrderListVM>(HttpStatusCode.OK, model);
                }
                else
                {
                    response = request.CreateResponse(HttpStatusCode.Unauthorized, "Unauthorized user");
                }
                return response;
            });
        }

        [HttpPost]
        [Route("OrderList", Name = "OrderList")]
        public HttpResponseMessage OrderList(HttpRequestMessage request, OrderListVM model, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (true)
                {
                    //a vendor should have access only to his products
                    if (_workContext.CurrentVendor != null)
                    {
                        model.VendorId = _workContext.CurrentVendor.Id;
                    }

                    DateTime? startDateValue = (model.StartDate == null) ? null
                                    : (DateTime?)_dateTimeHelper.ConvertToUtcTime(model.StartDate.Value, _dateTimeHelper.CurrentTimeZone);

                    DateTime? endDateValue = (model.EndDate == null) ? null
                                    : (DateTime?)_dateTimeHelper.ConvertToUtcTime(model.EndDate.Value, _dateTimeHelper.CurrentTimeZone).AddDays(1);

                    var orderStatusIds = !model.OrderStatusIds.Contains(0) ? model.OrderStatusIds : null;
                    var paymentStatusIds = !model.PaymentStatusIds.Contains(0) ? model.PaymentStatusIds : null;
                    var shippingStatusIds = !model.ShippingStatusIds.Contains(0) ? model.ShippingStatusIds : null;

                    var filterByProductId = 0;
                    var product = _productService.GetProductById(model.ProductId);
                    if (product != null && HasAccessToProduct(product))
                        filterByProductId = model.ProductId;

                    //load orders
                    var orders = _orderService.SearchOrders(storeId: model.StoreId,
                        vendorId: model.VendorId,
                        productId: filterByProductId,
                        warehouseId: model.WarehouseId,
                        paymentMethodSystemName: model.PaymentMethodSystemName,
                        createdFromUtc: startDateValue,
                        createdToUtc: endDateValue,
                        osIds: orderStatusIds,
                        psIds: paymentStatusIds,
                        ssIds: shippingStatusIds,
                        billingEmail: model.BillingEmail,
                        billingLastName: model.BillingLastName,
                        billingCountryId: model.BillingCountryId,
                        orderNotes: model.OrderNotes,
                        pageIndex: pageIndex,
                        pageSize: pageSize);
                    var gridModel = new DataSourceResult
                    {
                        Data = orders.Select(x =>
                        {
                            var store = _storeService.GetStoreById(x.StoreId);
                            return new OrderVM
                            {
                                Id = x.Id,
                                StoreName = store != null ? store.Name : "Unknown",
                                OrderTotal = _priceFormatter.FormatPrice(x.OrderTotal, true, false),
                                OrderStatus = x.OrderStatus.GetLocalizedEnum(_localizationService, _workContext),
                                OrderStatusId = x.OrderStatusId,
                                PaymentStatus = x.PaymentStatus.GetLocalizedEnum(_localizationService, _workContext),
                                PaymentStatusId = x.PaymentStatusId,
                                ShippingStatus = x.ShippingStatus.GetLocalizedEnum(_localizationService, _workContext),
                                ShippingStatusId = x.ShippingStatusId,
                                CustomerEmail = x.BillingAddress.Email,
                                CustomerFullName = string.Format("{0} {1}", x.BillingAddress.FirstName, x.BillingAddress.LastName),
                                CreatedOn = _dateTimeHelper.ConvertToUserTime(x.CreatedOnUtc, DateTimeKind.Utc),
                                CustomOrderNumber = x.CustomOrderNumber
                            };
                        }),
                        Total = orders.TotalCount
                    };

                    //summary report
                    //currently we do not support productId and warehouseId parameters for this report
                    var reportSummary = _orderReportService.GetOrderAverageReportLine(
                        storeId: model.StoreId,
                        vendorId: model.VendorId,
                        orderId: 0,
                        paymentMethodSystemName: model.PaymentMethodSystemName,
                        osIds: orderStatusIds,
                        psIds: paymentStatusIds,
                        ssIds: shippingStatusIds,
                        startTimeUtc: startDateValue,
                        endTimeUtc: endDateValue,
                        billingEmail: model.BillingEmail,
                        billingLastName: model.BillingLastName,
                        billingCountryId: model.BillingCountryId,
                        orderNotes: model.OrderNotes);

                    var profit = _orderReportService.ProfitReport(
                        storeId: model.StoreId,
                        vendorId: model.VendorId,
                        paymentMethodSystemName: model.PaymentMethodSystemName,
                        osIds: orderStatusIds,
                        psIds: paymentStatusIds,
                        ssIds: shippingStatusIds,
                        startTimeUtc: startDateValue,
                        endTimeUtc: endDateValue,
                        billingEmail: model.BillingEmail,
                        billingLastName: model.BillingLastName,
                        billingCountryId: model.BillingCountryId,
                        orderNotes: model.OrderNotes);
                    var primaryStoreCurrency = _currencyService.GetCurrencyById(_currencySettings.PrimaryStoreCurrencyId);
                    if (primaryStoreCurrency == null)
                        throw new Exception("Cannot load primary store currency");

                    gridModel.ExtraData = new OrderAggreratorVM
                    {
                        aggregatorprofit = _priceFormatter.FormatPrice(profit, true, false),
                        aggregatorshipping = _priceFormatter.FormatShippingPrice(reportSummary.SumShippingExclTax, true, primaryStoreCurrency, _workContext.WorkingLanguage, false),
                        aggregatortax = _priceFormatter.FormatPrice(reportSummary.SumTax, true, false),
                        aggregatortotal = _priceFormatter.FormatPrice(reportSummary.SumOrders, true, false)
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

        [HttpGet]
        [Route("{Id:int}", Name = "GetOrderById")]
        public HttpResponseMessage GetOrderById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No item found");
                if (true)
                {
                    var category = _categoryService.GetCategoryById(id);
                    if (category != null && !category.Deleted)
                    {
                        var order = _orderService.GetOrderById(id);
                        if (order == null || order.Deleted)
                        {
                            //No order found with the specified id
                            Url.Route("OrderDefaultModel", null);
                            string uri = uri = Url.Link("OrderDefaultModel", null);
                            response.Headers.Location = new Uri(uri);
                            return response;
                        }

                        //a vendor does not have access to this functionality
                        if (_workContext.CurrentVendor != null && !HasAccessToOrder(order))
                        {
                            Url.Route("OrderDefaultModel", null);
                            string uri = uri = Url.Link("OrderDefaultModel", null);
                            response.Headers.Location = new Uri(uri);
                            return response;
                        }

                        var model = new OrderVM();
                        PrepareOrderDetailsModel(model, order);

                        //var warnings = TempData["nop.admin.order.warnings"] as List<string>;
                        //if (warnings != null)
                        //    model.Warnings = warnings;
                        response = request.CreateResponse<OrderVM>(HttpStatusCode.OK, model);
                    }
                }
                else
                {
                    response = request.CreateResponse(HttpStatusCode.Unauthorized, "Unauthorized user");
                }
                return response;
            });
        }

        [HttpPost]
        [Route("GoToOrderId", Name = "GoToOrderId")]
        public HttpResponseMessage GoToOrderId(HttpRequestMessage request, OrderListVM model)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (true)
                {
                    var order = _orderService.GetOrderByCustomOrderNumber(model.GoDirectlyToCustomOrderNumber);
                    string uri = uri = Url.Link("OrderDefaultModel", null);
                    if (order != null)
                    {
                        uri = Url.Link("GetOrderById", new { id = order.Id });
                        Url.Route("GetOrderById", new { id = order.Id });
                    }

                    response.Headers.Location = new Uri(uri);
                }
                else
                {
                    response = request.CreateResponse(HttpStatusCode.Unauthorized, "Unauthorized user");
                }
                return response;
            });
        }

        [HttpGet]
        [Route("ProductSearchAutoComplete/{term}", Name = "ProductSearchAutoComplete")]
        public HttpResponseMessage ProductSearchAutoComplete(HttpRequestMessage request, string term)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No item found");
                const int searchTermMinimumLength = 3;
                if (String.IsNullOrWhiteSpace(term) || term.Length < searchTermMinimumLength)
                    return null;

                //a vendor should have access only to his products
                var vendorId = 0;
                if (_workContext.CurrentVendor != null)
                {
                    vendorId = _workContext.CurrentVendor.Id;
                }

                //products
                const int productNumber = 15;
                var products = _productService.SearchProducts(
                    vendorId: vendorId,
                    keywords: term,
                    pageSize: productNumber,
                    showHidden: true);

                var result = (from p in products
                              select new
                              {
                                  label = p.Name,
                                  productid = p.Id
                              })
                              .ToList();

                response = request.CreateResponse(HttpStatusCode.OK, result);
                return response;
            });
        }
        #endregion

        #region Order notes
        [HttpGet]
        [Route("{orderId:int}/orderNote", Name = "GetOrderNote")]
        public HttpResponseMessage GetOrderNote(HttpRequestMessage request, int orderId, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (true)
                {
                    var order = _orderService.GetOrderById(orderId);
                    if (order == null)
                        throw new ArgumentException("No order found with the specified id");

                    //a vendor does not have access to this functionality
                    if (_workContext.CurrentVendor != null)
                        return null;

                    //order notes
                    var orderNoteModels = new List<OrderVM.OrderNote>();
                    foreach (var orderNote in order.OrderNotes
                        .OrderByDescending(on => on.CreatedOnUtc))
                    {
                        var download = _downloadService.GetDownloadById(orderNote.DownloadId);
                        orderNoteModels.Add(new OrderVM.OrderNote
                        {
                            Id = orderNote.Id,
                            OrderId = orderNote.OrderId,
                            DownloadId = orderNote.DownloadId,
                            DownloadGuid = download != null ? download.DownloadGuid : Guid.Empty,
                            DisplayToCustomer = orderNote.DisplayToCustomer,
                            Note = orderNote.FormatOrderNoteText(),
                            CreatedOn = _dateTimeHelper.ConvertToUserTime(orderNote.CreatedOnUtc, DateTimeKind.Utc)
                        });
                    }

                    var gridModel = new DataSourceResult
                    {
                        Data = orderNoteModels,
                        Total = orderNoteModels.Count
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
        [Route("AddOrderNote", Name = "AddOrderNote")]
        public HttpResponseMessage AddOrderNote(HttpRequestMessage request, int orderId, int downloadId, bool displayToCustomer, string message)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, new { Result = false });
                if (true)
                {
                    var order = _orderService.GetOrderById(orderId);
                    if (order == null)
                    {
                        return response;
                    }

                    //a vendor does not have access to this functionality
                    if (_workContext.CurrentVendor != null)
                        return response;

                    var orderNote = new OrderNote
                    {
                        DisplayToCustomer = displayToCustomer,
                        Note = message,
                        DownloadId = downloadId,
                        CreatedOnUtc = DateTime.UtcNow,
                    };
                    order.OrderNotes.Add(orderNote);
                    _orderService.UpdateOrder(order);

                    //new order notification
                    if (displayToCustomer)
                    {
                        //email
                       // _workflowMessageService.SendNewOrderNoteAddedCustomerNotification(orderNote, _workContext.WorkingLanguage.Id);

                    }
                    _baseService.Commit();
                    response = response = request.CreateResponse(HttpStatusCode.OK, new { Result = true });
                }
                else
                {
                    response = request.CreateResponse(HttpStatusCode.Unauthorized, "Unauthorized user");
                }
                return response;

            });
        }

        [HttpPost]
        [Route("DeleteOrderNote", Name = "DeleteOrderNote")]
        public HttpResponseMessage DeleteOrderNote(HttpRequestMessage request, int id, int orderId)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (true)
                {
                    var order = _orderService.GetOrderById(orderId);
                    if (order == null)
                        throw new ArgumentException("No order found with the specified id");

                    //a vendor does not have access to this functionality
                    if (_workContext.CurrentVendor != null)
                    {
                        var uri = Url.Link("GetOrderById", new { id = orderId });
                        Url.Route("GetOrderById", new { id = orderId });
                        response.Headers.Location = new Uri(uri);
                        return response;
                    }

                    var orderNote = order.OrderNotes.FirstOrDefault(on => on.Id == id);
                    if (orderNote == null)
                        throw new ArgumentException("No order note found with the specified id");
                    _orderService.DeleteOrderNote(orderNote);

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    response = request.CreateResponse(HttpStatusCode.Unauthorized, "Unauthorized user");
                }
                return response;

            });
        }
        #endregion

        #region Reports
        [HttpGet]
        [Route("Reports/ByQuantity/{pageIndex:int=0}/{pageSize:int=2147483647}", Name = "BestsellersBriefReportByQuantity")]
        public HttpResponseMessage BestsellersBriefReportByQuantity(HttpRequestMessage request, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (true)
                {
                    var gridModel = GetBestsellersBriefReportModel(pageIndex, pageSize, 1);
                    response = request.CreateResponse<DataSourceResult>(HttpStatusCode.OK, gridModel);
                }
                else
                {
                    response = request.CreateResponse(HttpStatusCode.Unauthorized, "Unauthorized user");
                }
                return response;
            });
        }

        [HttpGet]
        [Route("Reports/ByAmount/{pageIndex:int=0}/{pageSize:int=2147483647}", Name = "BestsellersBriefReportByAmount")]
        public HttpResponseMessage BestsellersBriefReportByAmount(HttpRequestMessage request, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (true)
                {
                    var gridModel = GetBestsellersBriefReportModel(pageIndex, pageSize, 2);
                    response = request.CreateResponse<DataSourceResult>(HttpStatusCode.OK, gridModel);
                }
                else
                {
                    response = request.CreateResponse(HttpStatusCode.Unauthorized, "Unauthorized user");
                }
                return response;
            });
        }

        [HttpGet]
        [Route("BestsellersReportSearchModal", Name = "BestsellersReportSearchModal")]
        public HttpResponseMessage BestsellersReport(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (true)
                {
                    var model = new BestsellersReportVM();
                    //vendor
                    model.IsLoggedInAsVendor = _workContext.CurrentVendor != null;
                    string allText = _localizationService.GetResource("Admin.Common.All");
                    //stores
                    model.AvailableStores.Add(new System.Web.Mvc.SelectListItem { Text = allText, Value = "0" });
                    foreach (var s in _storeService.GetAllStores())
                        model.AvailableStores.Add(new System.Web.Mvc.SelectListItem { Text = s.Name, Value = s.Id.ToString() });

                    //order statuses
                    model.AvailableOrderStatuses = OrderStatus.Pending.ToSelectList(_localizationService, _baseService.WorkContext, false).ToList();
                    model.AvailableOrderStatuses.Insert(0, new System.Web.Mvc.SelectListItem { Text = allText, Value = "0" });

                    //payment statuses
                    model.AvailablePaymentStatuses = PaymentStatus.Pending.ToSelectList(_localizationService, _baseService.WorkContext, false).ToList();
                    model.AvailablePaymentStatuses.Insert(0, new System.Web.Mvc.SelectListItem { Text = allText, Value = "0" });

                    //categories
                    model.AvailableCategories.Add(new System.Web.Mvc.SelectListItem { Text = allText, Value = "0" });
                    var categories = SelectListHelper.GetCategoryList(_categoryService, true);
                    foreach (var c in categories)
                        model.AvailableCategories.Add(c);

                    //manufacturers
                    model.AvailableManufacturers.Add(new System.Web.Mvc.SelectListItem { Text = allText, Value = "0" });
                    var manufacturers = SelectListHelper.GetManufacturerList(_manufacturerService, true);
                    foreach (var m in manufacturers)
                        model.AvailableManufacturers.Add(m);

                    //billing countries
                    foreach (var c in _countryService.GetAllCountriesForBilling(showHidden: true))
                        model.AvailableCountries.Add(new System.Web.Mvc.SelectListItem { Text = c.Name, Value = c.Id.ToString() });
                    model.AvailableCountries.Insert(0, new System.Web.Mvc.SelectListItem { Text = allText, Value = "0" });

                    //vendors
                    model.AvailableVendors.Add(new System.Web.Mvc.SelectListItem { Text = allText, Value = "0" });
                    var vendors = SelectListHelper.GetVendorList(_vendorService, true);

                    foreach (var v in vendors)
                        model.AvailableVendors.Add(v);

                    response = request.CreateResponse<BestsellersReportVM>(HttpStatusCode.OK, model);
                }
                else
                {
                    response = request.CreateResponse(HttpStatusCode.Unauthorized, "Unauthorized user");
                }
                return response;
            });
        }

        [HttpPost]
        [Route("BestsellersReportList", Name = "BestsellersReportList")]
        public HttpResponseMessage BestsellersReportList(HttpRequestMessage request, BestsellersReportVM model, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (true)
                {
                    //a vendor should have access only to his products
                    if (_workContext.CurrentVendor != null)
                    {
                        model.VendorId = _workContext.CurrentVendor.Id;
                    }

                    DateTime? startDateValue = (model.StartDate == null) ? null
                                    : (DateTime?)_dateTimeHelper.ConvertToUtcTime(model.StartDate.Value, _dateTimeHelper.CurrentTimeZone);

                    DateTime? endDateValue = (model.EndDate == null) ? null
                                    : (DateTime?)_dateTimeHelper.ConvertToUtcTime(model.EndDate.Value, _dateTimeHelper.CurrentTimeZone).AddDays(1);

                    OrderStatus? orderStatus = model.OrderStatusId > 0 ? (OrderStatus?)(model.OrderStatusId) : null;
                    PaymentStatus? paymentStatus = model.PaymentStatusId > 0 ? (PaymentStatus?)(model.PaymentStatusId) : null;

                    var items = _orderReportService.BestSellersReport(
                        createdFromUtc: startDateValue,
                        createdToUtc: endDateValue,
                        os: orderStatus,
                        ps: paymentStatus,
                        billingCountryId: model.BillingCountryId,
                        orderBy: 2,
                        vendorId: model.VendorId,
                        categoryId: model.CategoryId,
                        manufacturerId: model.ManufacturerId,
                        storeId: model.StoreId,
                        pageIndex: pageIndex,
                        pageSize: pageSize,
                        showHidden: true);
                    var gridModel = new DataSourceResult
                    {
                        Data = items.Select(x =>
                        {
                            var m = new BestsellersReportLineVM
                            {
                                ProductId = x.ProductId,
                                TotalAmount = _priceFormatter.FormatPrice(x.TotalAmount, true, false),
                                TotalQuantity = x.TotalQuantity,
                            };
                            var product = _productService.GetProductById(x.ProductId);
                            if (product != null)
                                m.ProductName = product.Name;
                            return m;
                        }),
                        Total = items.TotalCount
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

        [HttpGet]
        [Route("NeverSoldReportSearchModel", Name = "NeverSoldReportSearchModel")]
        public HttpResponseMessage NeverSoldReport(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (true)
                {
                    var model = new NeverSoldReportVM();
                    string allText = _localizationService.GetResource("Admin.Common.All");

                    //a vendor should have access only to his products
                    model.IsLoggedInAsVendor = _workContext.CurrentVendor != null;

                    //categories
                    model.AvailableCategories.Add(new System.Web.Mvc.SelectListItem { Text = allText, Value = "0" });
                    var categories = SelectListHelper.GetCategoryList(_categoryService, true);
                    foreach (var c in categories)
                        model.AvailableCategories.Add(c);

                    //manufacturers
                    model.AvailableManufacturers.Add(new System.Web.Mvc.SelectListItem { Text = allText, Value = "0" });
                    var manufacturers = SelectListHelper.GetManufacturerList(_manufacturerService, true);
                    foreach (var m in manufacturers)
                        model.AvailableManufacturers.Add(m);

                    //stores
                    model.AvailableStores.Add(new System.Web.Mvc.SelectListItem { Text = allText, Value = "0" });
                    foreach (var s in _storeService.GetAllStores())
                        model.AvailableStores.Add(new System.Web.Mvc.SelectListItem { Text = s.Name, Value = s.Id.ToString() });

                    //vendors
                    model.AvailableVendors.Add(new System.Web.Mvc.SelectListItem { Text = allText, Value = "0" });
                    var vendors = SelectListHelper.GetVendorList(_vendorService, true);

                    foreach (var v in vendors)
                        model.AvailableVendors.Add(v);

                    response = request.CreateResponse<NeverSoldReportVM>(HttpStatusCode.OK, model);
                }
                else
                {
                    response = request.CreateResponse(HttpStatusCode.Unauthorized, "Unauthorized user");
                }
                return response;
            });
        }

        [HttpPost]
        [Route("NeverSoldReportList", Name = "NeverSoldReportList")]
        public HttpResponseMessage NeverSoldReportList(HttpRequestMessage request, NeverSoldReportVM model, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (true)
                {
                    //a vendor should have access only to his products
                    if (_workContext.CurrentVendor != null)
                    {
                        model.SearchVendorId = _workContext.CurrentVendor.Id;
                    }

                    DateTime? startDateValue = (model.StartDate == null) ? null
                                    : (DateTime?)_dateTimeHelper.ConvertToUtcTime(model.StartDate.Value, _dateTimeHelper.CurrentTimeZone);

                    DateTime? endDateValue = (model.EndDate == null) ? null
                                    : (DateTime?)_dateTimeHelper.ConvertToUtcTime(model.EndDate.Value, _dateTimeHelper.CurrentTimeZone).AddDays(1);

                    var items = _orderReportService.ProductsNeverSold(vendorId: model.SearchVendorId,
                        storeId: model.SearchStoreId,
                        categoryId: model.SearchCategoryId,
                        manufacturerId: model.SearchManufacturerId,
                        createdFromUtc: startDateValue,
                        createdToUtc: endDateValue,
                        pageIndex: pageIndex,
                        pageSize: pageSize,
                        showHidden: true);
                    var gridModel = new DataSourceResult
                    {
                        Data = items.Select(x =>
                            new NeverSoldReportLineVM
                            {
                                ProductId = x.Id,
                                ProductName = x.Name,
                            }),
                        Total = items.TotalCount
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

        [HttpGet]
        [Route("Reports/OrderAverageReportList", Name = "OrderAverageReportList")]
        public HttpResponseMessage OrderAverageReportList(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (true)
                {
                    //a vendor doesn't have access to this report
                    if (_workContext.CurrentVendor != null)
                        return null;

                    var report = new List<OrderAverageReportLineSummary>();
                    report.Add(_orderReportService.OrderAverageReport(0, OrderStatus.Pending));
                    report.Add(_orderReportService.OrderAverageReport(0, OrderStatus.Processing));
                    report.Add(_orderReportService.OrderAverageReport(0, OrderStatus.Complete));
                    report.Add(_orderReportService.OrderAverageReport(0, OrderStatus.Cancelled));
                    var model = report.Select(x => new OrderAverageReportLineSummaryVM
                    {
                        OrderStatus = x.OrderStatus.GetLocalizedEnum(_localizationService, _workContext),
                        SumTodayOrders = _priceFormatter.FormatPrice(x.SumTodayOrders, true, false),
                        SumThisWeekOrders = _priceFormatter.FormatPrice(x.SumThisWeekOrders, true, false),
                        SumThisMonthOrders = _priceFormatter.FormatPrice(x.SumThisMonthOrders, true, false),
                        SumThisYearOrders = _priceFormatter.FormatPrice(x.SumThisYearOrders, true, false),
                        SumAllTimeOrders = _priceFormatter.FormatPrice(x.SumAllTimeOrders, true, false),
                    }).ToList();

                    var gridModel = new DataSourceResult
                    {
                        Data = model,
                        Total = model.Count
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

        [HttpGet]
        [Route("Reports/OrderIncompleteReportList", Name = "OrderIncompleteReportList")]
        public HttpResponseMessage OrderIncompleteReportList(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (true)
                {
                    //a vendor doesn't have access to this report
                    if (_workContext.CurrentVendor != null)
                        return null;

                    var model = new List<OrderIncompleteReportLineVM>();
                    //not paid
                    var orderStatuses = Enum.GetValues(typeof(OrderStatus)).Cast<int>().Where(os => os != (int)OrderStatus.Cancelled).ToList();
                    var paymentStatuses = new List<int>() { (int)PaymentStatus.Pending };
                    var psPending = _orderReportService.GetOrderAverageReportLine(psIds: paymentStatuses, osIds: orderStatuses);
                    model.Add(new OrderIncompleteReportLineVM
                    {
                        Item = _localizationService.GetResource("Admin.SalesReport.Incomplete.TotalUnpaidOrders"),
                        Count = psPending.CountOrders,
                        Total = _priceFormatter.FormatPrice(psPending.SumOrders, true, false),
                        ViewLink = Url.Link("OrderDefaultModel", new
                        {
                            orderStatusIds = string.Join(",", orderStatuses),
                            paymentStatusIds = string.Join(",", paymentStatuses)
                        })
                    });
                    //not shipped
                    var shippingStatuses = new List<int>() { (int)ShippingStatus.NotYetShipped };
                    var ssPending = _orderReportService.GetOrderAverageReportLine(osIds: orderStatuses, ssIds: shippingStatuses);
                    model.Add(new OrderIncompleteReportLineVM
                    {
                        Item = _localizationService.GetResource("Admin.SalesReport.Incomplete.TotalNotShippedOrders"),
                        Count = ssPending.CountOrders,
                        Total = _priceFormatter.FormatPrice(ssPending.SumOrders, true, false),
                        ViewLink = Url.Link("OrderDefaultModel", new
                        {
                            orderStatusIds = string.Join(",", orderStatuses),
                            shippingStatusIds = string.Join(",", shippingStatuses)
                        })
                    });
                    //pending
                    orderStatuses = new List<int>() { (int)OrderStatus.Pending };
                    var osPending = _orderReportService.GetOrderAverageReportLine(osIds: orderStatuses);
                    model.Add(new OrderIncompleteReportLineVM
                    {
                        Item = _localizationService.GetResource("Admin.SalesReport.Incomplete.TotalIncompleteOrders"),
                        Count = osPending.CountOrders,
                        Total = _priceFormatter.FormatPrice(osPending.SumOrders, true, false),
                        ViewLink = Url.Link("OrderDefaultModel", new { orderStatusIds = string.Join(",", orderStatuses) })
                    });

                    var gridModel = new DataSourceResult
                    {
                        Data = model,
                        Total = model.Count
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


        [HttpGet]
        [Route("LoadOrderStatistics/{period}", Name = "LoadOrderStatistics")]
        public HttpResponseMessage LoadOrderStatistics(HttpRequestMessage request, string period)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (true)
                {
                    //a vendor doesn't have access to this report
                    if (_workContext.CurrentVendor != null)
                        return null;

                    var result = new List<object>();

                    var nowDt = _dateTimeHelper.ConvertToUserTime(DateTime.Now);
                    var timeZone = _dateTimeHelper.CurrentTimeZone;

                    var culture = new CultureInfo(_workContext.WorkingLanguage.LanguageCulture);

                    switch (period)
                    {
                        case "year":
                            //year statistics
                            var yearAgoDt = nowDt.AddYears(-1).AddMonths(1);
                            var searchYearDateUser = new DateTime(yearAgoDt.Year, yearAgoDt.Month, 1);
                            if (!timeZone.IsInvalidTime(searchYearDateUser))
                            {
                                for (int i = 0; i <= 12; i++)
                                {
                                    result.Add(new
                                    {
                                        date = searchYearDateUser.Date.ToString("Y", culture),
                                        value = _orderService.SearchOrders(
                                            createdFromUtc: _dateTimeHelper.ConvertToUtcTime(searchYearDateUser, timeZone),
                                            createdToUtc: _dateTimeHelper.ConvertToUtcTime(searchYearDateUser.AddMonths(1), timeZone),
                                            pageIndex: 0,
                                            pageSize: 1).TotalCount.ToString()
                                    });

                                    searchYearDateUser = searchYearDateUser.AddMonths(1);
                                }
                            }
                            break;

                        case "month":
                            //month statistics
                            var monthAgoDt = nowDt.AddDays(-30);
                            var searchMonthDateUser = new DateTime(monthAgoDt.Year, monthAgoDt.Month, monthAgoDt.Day);
                            if (!timeZone.IsInvalidTime(searchMonthDateUser))
                            {
                                for (int i = 0; i <= 30; i++)
                                {
                                    result.Add(new
                                    {
                                        date = searchMonthDateUser.Date.ToString("M", culture),
                                        value = _orderService.SearchOrders(
                                            createdFromUtc: _dateTimeHelper.ConvertToUtcTime(searchMonthDateUser, timeZone),
                                            createdToUtc: _dateTimeHelper.ConvertToUtcTime(searchMonthDateUser.AddDays(1), timeZone),
                                            pageIndex: 0,
                                            pageSize: 1).TotalCount.ToString()
                                    });

                                    searchMonthDateUser = searchMonthDateUser.AddDays(1);
                                }
                            }
                            break;

                        case "week":
                        default:
                            //week statistics
                            var weekAgoDt = nowDt.AddDays(-7);
                            var searchWeekDateUser = new DateTime(weekAgoDt.Year, weekAgoDt.Month, weekAgoDt.Day);
                            if (!timeZone.IsInvalidTime(searchWeekDateUser))
                            {
                                for (int i = 0; i <= 7; i++)
                                {
                                    result.Add(new
                                    {
                                        date = searchWeekDateUser.Date.ToString("d dddd", culture),
                                        value = _orderService.SearchOrders(
                                            createdFromUtc: _dateTimeHelper.ConvertToUtcTime(searchWeekDateUser, timeZone),
                                            createdToUtc: _dateTimeHelper.ConvertToUtcTime(searchWeekDateUser.AddDays(1), timeZone),
                                            pageIndex: 0,
                                            pageSize: 1).TotalCount.ToString()
                                    });

                                    searchWeekDateUser = searchWeekDateUser.AddDays(1);
                                }
                            }
                            break;
                    }

                    response = request.CreateResponse<List<object>>(HttpStatusCode.OK, result);
                }
                else
                {
                    response = request.CreateResponse(HttpStatusCode.Unauthorized, "Unauthorized user");
                }
                return response;
            });
        }
        #endregion

        #region Addresses
        [HttpGet]
        [Route("{orderId:int}/address/{addressId:int}", Name = "GetOrderAddress")]
        public HttpResponseMessage GetAddress(HttpRequestMessage request, int addressId, int orderId)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (true)
                {
                    var order = _orderService.GetOrderById(orderId);
                    if (order == null)
                    {
                        //No order found with the specified id
                        string uri = uri = Url.Link("OrderDefaultModel", null);

                        response.Headers.Location = new Uri(uri);
                        return response;
                    }

                    //a vendor does not have access to this functionality
                    if (_workContext.CurrentVendor != null)
                    {
                        string uri = uri = Url.Link("GetOrderById", new { id = orderId });
                        Url.Route("GetOrderById", new { id = orderId });
                        response.Headers.Location = new Uri(uri);
                        return response;
                    }

                    var address = _addressService.GetAddressById(addressId);
                    if (address == null)
                        throw new ArgumentException("No address found with the specified id", "addressId");

                    var model = new OrderAddressVM();
                    model.OrderId = orderId;
                    model.Address = address.ToModel();
                    model.Address.FirstNameEnabled = true;
                    model.Address.FirstNameRequired = true;
                    model.Address.LastNameEnabled = true;
                    model.Address.LastNameRequired = true;
                    model.Address.EmailEnabled = true;
                    model.Address.EmailRequired = true;
                    model.Address.CompanyEnabled = _addressSettings.CompanyEnabled;
                    model.Address.CompanyRequired = _addressSettings.CompanyRequired;
                    model.Address.CountryEnabled = _addressSettings.CountryEnabled;
                    model.Address.CountryRequired = _addressSettings.CountryEnabled; //country is required when enabled
                    model.Address.StateProvinceEnabled = _addressSettings.StateProvinceEnabled;
                    model.Address.CityEnabled = _addressSettings.CityEnabled;
                    model.Address.CityRequired = _addressSettings.CityRequired;
                    model.Address.StreetAddressEnabled = _addressSettings.StreetAddressEnabled;
                    model.Address.StreetAddressRequired = _addressSettings.StreetAddressRequired;
                    model.Address.StreetAddress2Enabled = _addressSettings.StreetAddress2Enabled;
                    model.Address.StreetAddress2Required = _addressSettings.StreetAddress2Required;
                    model.Address.ZipPostalCodeEnabled = _addressSettings.ZipPostalCodeEnabled;
                    model.Address.ZipPostalCodeRequired = _addressSettings.ZipPostalCodeRequired;
                    model.Address.PhoneEnabled = _addressSettings.PhoneEnabled;
                    model.Address.PhoneRequired = _addressSettings.PhoneRequired;
                    model.Address.FaxEnabled = _addressSettings.FaxEnabled;
                    model.Address.FaxRequired = _addressSettings.FaxRequired;

                    //countries
                    model.Address.AvailableCountries.Add(new System.Web.Mvc.SelectListItem{ Text = _localizationService.GetResource("Admin.Address.SelectCountry"), Value = "0" });
                    foreach (var c in _countryService.GetAllCountries(showHidden: true))
                        model.Address.AvailableCountries.Add(new System.Web.Mvc.SelectListItem{ Text = c.Name, Value = c.Id.ToString(), Selected = (c.Id == address.CountryId) });
                    //states
                    var states = address.Country != null ? _stateProvinceService.GetStateProvincesByCountryId(address.Country.Id, showHidden: true).ToList() : new List<StateProvince>();
                    if (states.Any())
                    {
                        foreach (var s in states)
                            model.Address.AvailableStates.Add(new System.Web.Mvc.SelectListItem{ Text = s.Name, Value = s.Id.ToString(), Selected = (s.Id == address.StateProvinceId) });
                    }
                    else
                        model.Address.AvailableStates.Add(new System.Web.Mvc.SelectListItem{ Text = _localizationService.GetResource("Admin.Address.OtherNonUS"), Value = "0" });
                    //customer attribute services
                    model.Address.PrepareCustomAddressAttributes(address, _addressAttributeService, _addressAttributeParser);

                    response = request.CreateResponse<OrderAddressVM>(HttpStatusCode.OK, model);
                }
                else
                {
                    response = request.CreateResponse(HttpStatusCode.Unauthorized, "Unauthorized user");
                }
                return response;

            });
        }

        [HttpPost]
        [Route("EditAddress")]
        public HttpResponseMessage AddressEdit(HttpRequestMessage request, OrderAddressVM model)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (true)
                {
                    var order = _orderService.GetOrderById(model.OrderId);
                    if (order == null)
                    {
                        //No order found with the specified id
                        string uri = Url.Link("OrderDefaultModel", null);

                        response.Headers.Location = new Uri(uri);
                        return response;
                    }

                    //a vendor does not have access to this functionality
                    if (_workContext.CurrentVendor != null)
                    {
                        string uri = uri = Url.Link("GetOrderById", new { id = order.Id });
                        Url.Route("GetOrderById", new { id = order.Id });
                        response.Headers.Location = new Uri(uri);
                        return response;
                    }

                    var address = _addressService.GetAddressById(model.Address.Id);
                    if (address == null)
                        throw new ArgumentException("No address found with the specified id");

                    //custom address attributes
                    //var customAttributes = form.ParseCustomAddressAttributes(_addressAttributeParser, _addressAttributeService);
                    //var customAttributeWarnings = _addressAttributeParser.GetAttributeWarnings(customAttributes);
                    //foreach (var error in customAttributeWarnings)
                    //{
                    //    ModelState.AddModelError("", error);
                    //}

                    if (ModelState.IsValid)
                    {
                        address = model.Address.ToEntity(address);
                        //address.CustomAttributes = customAttributes;
                        _addressService.UpdateAddress(address);

                        //add a note
                        order.OrderNotes.Add(new OrderNote
                        {
                            Note = "Address has been edited",
                            DisplayToCustomer = false,
                            CreatedOnUtc = DateTime.UtcNow
                        });
                        _orderService.UpdateOrder(order);
                        LogEditOrder(order.Id);

                        string uri = uri = Url.Link("GetOrderAddress", new { addressId = model.Address.Id, orderId = model.OrderId });
                        Url.Route("GetOrderAddress", new { addressId = model.Address.Id, orderId = model.OrderId });
                        response.Headers.Location = new Uri(uri);
                        return response;
                    }

                    //If we got this far, something failed, redisplay form
                    model.OrderId = order.Id;
                    model.Address = address.ToModel();
                    model.Address.FirstNameEnabled = true;
                    model.Address.FirstNameRequired = true;
                    model.Address.LastNameEnabled = true;
                    model.Address.LastNameRequired = true;
                    model.Address.EmailEnabled = true;
                    model.Address.EmailRequired = true;
                    model.Address.CompanyEnabled = _addressSettings.CompanyEnabled;
                    model.Address.CompanyRequired = _addressSettings.CompanyRequired;
                    model.Address.CountryEnabled = _addressSettings.CountryEnabled;
                    model.Address.CountryRequired = _addressSettings.CountryEnabled; //country is required when enabled
                    model.Address.StateProvinceEnabled = _addressSettings.StateProvinceEnabled;
                    model.Address.CityEnabled = _addressSettings.CityEnabled;
                    model.Address.CityRequired = _addressSettings.CityRequired;
                    model.Address.StreetAddressEnabled = _addressSettings.StreetAddressEnabled;
                    model.Address.StreetAddressRequired = _addressSettings.StreetAddressRequired;
                    model.Address.StreetAddress2Enabled = _addressSettings.StreetAddress2Enabled;
                    model.Address.StreetAddress2Required = _addressSettings.StreetAddress2Required;
                    model.Address.ZipPostalCodeEnabled = _addressSettings.ZipPostalCodeEnabled;
                    model.Address.ZipPostalCodeRequired = _addressSettings.ZipPostalCodeRequired;
                    model.Address.PhoneEnabled = _addressSettings.PhoneEnabled;
                    model.Address.PhoneRequired = _addressSettings.PhoneRequired;
                    model.Address.FaxEnabled = _addressSettings.FaxEnabled;
                    model.Address.FaxRequired = _addressSettings.FaxRequired;
                    //countries
                    model.Address.AvailableCountries.Add(new System.Web.Mvc.SelectListItem { Text = _localizationService.GetResource("Admin.Address.SelectCountry"), Value = "0" });
                    foreach (var c in _countryService.GetAllCountries(showHidden: true))
                        model.Address.AvailableCountries.Add(new System.Web.Mvc.SelectListItem { Text = c.Name, Value = c.Id.ToString(), Selected = (c.Id == address.CountryId) });
                    //states
                    var states = address.Country != null ? _stateProvinceService.GetStateProvincesByCountryId(address.Country.Id, showHidden: true).ToList() : new List<StateProvince>();
                    if (states.Any())
                    {
                        foreach (var s in states)
                            model.Address.AvailableStates.Add(new System.Web.Mvc.SelectListItem { Text = s.Name, Value = s.Id.ToString(), Selected = (s.Id == address.StateProvinceId) });
                    }
                    else
                        model.Address.AvailableStates.Add(new System.Web.Mvc.SelectListItem { Text = _localizationService.GetResource("Admin.Address.OtherNonUS"), Value = "0" });
                    //customer attribute services
                    model.Address.PrepareCustomAddressAttributes(address, _addressAttributeService, _addressAttributeParser);

                    response = request.CreateResponse<OrderAddressVM>(HttpStatusCode.OK, model);
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
