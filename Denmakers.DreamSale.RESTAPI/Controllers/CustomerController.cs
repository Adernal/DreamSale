using Denmakers.DreamSale.Common;
using Denmakers.DreamSale.Data.Context;
using Denmakers.DreamSale.Data.Infrastructure;
using Denmakers.DreamSale.Data.Repositories;
using Denmakers.DreamSale.Helpers;
using Denmakers.DreamSale.Model.Catalog;
using Denmakers.DreamSale.Model.Common;
using Denmakers.DreamSale.Model.Customers;
using Denmakers.DreamSale.Model.Directory;
using Denmakers.DreamSale.Model.Forums;
using Denmakers.DreamSale.Model.Logging;
using Denmakers.DreamSale.Model.Messages;
using Denmakers.DreamSale.Model.Orders;
using Denmakers.DreamSale.Model.Payments;
using Denmakers.DreamSale.Model.Shipping;
using Denmakers.DreamSale.Model.Tax;
using Denmakers.DreamSale.RESTAPI.Infrastructure;
using Denmakers.DreamSale.Services;
using Denmakers.DreamSale.Services.Addresses;
using Denmakers.DreamSale.Services.Affiliates;
using Denmakers.DreamSale.Services.Attributes;
using Denmakers.DreamSale.Services.Catalog;
using Denmakers.DreamSale.Services.Categories;
using Denmakers.DreamSale.Services.Configuration;
using Denmakers.DreamSale.Services.Customers;
using Denmakers.DreamSale.Services.Directory;
using Denmakers.DreamSale.Services.Helpers;
using Denmakers.DreamSale.Services.Localization;
using Denmakers.DreamSale.Services.Logging;
using Denmakers.DreamSale.Services.Manufacturers;
using Denmakers.DreamSale.Services.Messages;
using Denmakers.DreamSale.Services.Orders;
using Denmakers.DreamSale.Services.Products;
using Denmakers.DreamSale.Services.Security;
using Denmakers.DreamSale.Services.Stores;
using Denmakers.DreamSale.Services.Tax;
using Denmakers.DreamSale.Services.Vendors;
using Denmakers.DreamSale.ViewModels.AdminVM.Addresses;
using Denmakers.DreamSale.ViewModels.AdminVM.Customers;
using Denmakers.DreamSale.ViewModels.AdminVM.ShoppingCart;
using Denmakers.DreamSale.ViewModels.Mapper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;

namespace Denmakers.DreamSale.RESTAPI.Controllers
{
    [RoutePrefix("api/Customers")]
    public class CustomerController : ApiControllerBase
    {
        #region Fields
        private readonly IManufacturerService _manufacturerService;
        private readonly ICategoryService _categoryService;
        private readonly ICustomerService _customerService;
        //private readonly INewsLetterSubscriptionService _newsLetterSubscriptionService;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly ICustomerRegistrationService _customerRegistrationService;
        private readonly ICustomerReportService _customerReportService;
        private readonly IDateTimeHelper _dateTimeHelper;
        private readonly ILocalizationService _localizationService;

        private readonly ICountryService _countryService;
        private readonly IStateProvinceService _stateProvinceService;
        private readonly IAddressService _addressService;

        private readonly ITaxService _taxService;
        private readonly IVendorService _vendorService;
        private readonly IStoreContext _storeContext;
        private readonly IPriceFormatter _priceFormatter;
        private readonly IOrderService _orderService;
        //private readonly IExportManager _exportManager;
        private readonly ICustomerActivityService _customerActivityService;
        private readonly IBackInStockSubscriptionService _backInStockSubscriptionService;
        private readonly IPriceCalculationService _priceCalculationService;
        private readonly IProductAttributeFormatter _productAttributeFormatter;
        private readonly IPermissionService _permissionService;
        private readonly IQueuedEmailService _queuedEmailService;
        private readonly IEmailAccountService _emailAccountService;
        //private readonly IForumService _forumService;
        //private readonly IOpenAuthenticationService _openAuthenticationService;

        private readonly IStoreService _storeService;
        private readonly ICustomerAttributeParser _customerAttributeParser;
        private readonly ICustomerAttributeService _customerAttributeService;
        private readonly IAddressAttributeParser _addressAttributeParser;
        private readonly IAddressAttributeService _addressAttributeService;
        private readonly IAddressAttributeFormatter _addressAttributeFormatter;
        private readonly IAffiliateService _affiliateService;
        //private readonly IWorkflowMessageService _workflowMessageService;
        //private readonly IRewardPointService _rewardPointService;
        //private readonly ICacheManager _cacheManager;
        private readonly IProductService _productService;
        private readonly ISettingService _settingService;

        private readonly DateTimeSettings _dateTimeSettings;
        private readonly TaxSettings _taxSettings;
        private readonly RewardPointsSettings _rewardPointsSettings;
        private readonly CustomerSettings _customerSettings;
        private readonly EmailAccountSettings _emailAccountSettings;
        private readonly ForumSettings _forumSettings;
        private readonly AddressSettings _addressSettings;
        #endregion

        #region Constructors

        public CustomerController(IBaseService baseService, ILogger logger, IWebHelper webHelper,
            ICustomerService customerService,
            //INewsLetterSubscriptionService newsLetterSubscriptionService,
            IGenericAttributeService genericAttributeService,
            ICustomerRegistrationService customerRegistrationService,
            ICustomerReportService customerReportService,
            IDateTimeHelper dateTimeHelper,
            ILocalizationService localizationService,
            DateTimeSettings dateTimeSettings,
            ICountryService countryService,
            IStateProvinceService stateProvinceService,
            IAddressService addressService,
            ITaxService taxService,
            IVendorService vendorService,
            IStoreContext storeContext,
            IPriceFormatter priceFormatter,
            IOrderService orderService,
            //IExportManager exportManager,
            ICustomerActivityService customerActivityService,
            IBackInStockSubscriptionService backInStockSubscriptionService,
            IPriceCalculationService priceCalculationService,
            IProductAttributeFormatter productAttributeFormatter,
            IPermissionService permissionService,
            IQueuedEmailService queuedEmailService,
            IEmailAccountService emailAccountService,
            //IForumService forumService,
            //IOpenAuthenticationService openAuthenticationService,
            IStoreService storeService,
            ICustomerAttributeParser customerAttributeParser,
            ICustomerAttributeService customerAttributeService,
            IAddressAttributeParser addressAttributeParser,
            IAddressAttributeService addressAttributeService,
            IAddressAttributeFormatter addressAttributeFormatter,
            IAffiliateService affiliateService,
            IProductService productService,
            //IWorkflowMessageService workflowMessageService,
            //IRewardPointService rewardPointService,
            //ICacheManager cacheManager
            ICategoryService categoryService,
            IManufacturerService manufacturerService,
            ISettingService settingService
            )
            : base(baseService, logger, webHelper)
        {
            this._customerService = customerService;
            //this._newsLetterSubscriptionService = newsLetterSubscriptionService;
            this._genericAttributeService = genericAttributeService;
            this._customerRegistrationService = customerRegistrationService;
            this._customerReportService = customerReportService;
            this._dateTimeHelper = dateTimeHelper;
            this._localizationService = localizationService;
            this._dateTimeSettings = dateTimeSettings;
            this._countryService = countryService;
            this._stateProvinceService = stateProvinceService;
            this._addressService = addressService;
            this._taxService = taxService;
            this._vendorService = vendorService;
            this._storeContext = storeContext;
            this._priceFormatter = priceFormatter;
            this._orderService = orderService;
            //this._exportManager = exportManager;
            this._customerActivityService = customerActivityService;
            this._backInStockSubscriptionService = backInStockSubscriptionService;
            this._priceCalculationService = priceCalculationService;
            this._productAttributeFormatter = productAttributeFormatter;
            this._permissionService = permissionService;
            this._queuedEmailService = queuedEmailService;
            this._emailAccountService = emailAccountService;
            //this._forumService = forumService;
            //this._openAuthenticationService = openAuthenticationService;
            this._storeService = storeService;
            this._customerAttributeParser = customerAttributeParser;
            this._customerAttributeService = customerAttributeService;
            this._addressAttributeParser = addressAttributeParser;
            this._addressAttributeService = addressAttributeService;
            this._addressAttributeFormatter = addressAttributeFormatter;
            this._affiliateService = affiliateService;
            this._productService = productService;
            this._categoryService = categoryService;
            this._manufacturerService = manufacturerService;

            //this._workflowMessageService = workflowMessageService;
            //this._rewardPointService = rewardPointService;
            //this._cacheManager = cacheManager;
            this._settingService = settingService;
            this._taxSettings = _settingService.LoadSetting<TaxSettings>();
            this._rewardPointsSettings = _settingService.LoadSetting<RewardPointsSettings>();
            this._customerSettings = _settingService.LoadSetting<CustomerSettings>();
            this._emailAccountSettings = _settingService.LoadSetting<EmailAccountSettings>();
            this._forumSettings = _settingService.LoadSetting<ForumSettings>();
            this._addressSettings = _settingService.LoadSetting<AddressSettings>();
        }

        #endregion 

        #region Utilities

        [NonAction]
        protected virtual string GetCustomerRolesNames(IList<CustomerRole> customerRoles, string separator = ",")
        {
            var sb = new StringBuilder();
            for (int i = 0; i < customerRoles.Count; i++)
            {
                sb.Append(customerRoles[i].Name);
                if (i != customerRoles.Count - 1)
                {
                    sb.Append(separator);
                    sb.Append(" ");
                }
            }
            return sb.ToString();
        }

        [NonAction]
        protected virtual IList<RegisteredCustomerReportLineVM> GetReportRegisteredCustomersModel()
        {
            var report = new List<RegisteredCustomerReportLineVM>();
            report.Add(new RegisteredCustomerReportLineVM
            {
                Period = _localizationService.GetResource("Admin.Customers.Reports.RegisteredCustomers.Fields.Period.7days"),
                Customers = _customerReportService.GetRegisteredCustomersReport(7)
            });

            report.Add(new RegisteredCustomerReportLineVM
            {
                Period = _localizationService.GetResource("Admin.Customers.Reports.RegisteredCustomers.Fields.Period.14days"),
                Customers = _customerReportService.GetRegisteredCustomersReport(14)
            });
            report.Add(new RegisteredCustomerReportLineVM
            {
                Period = _localizationService.GetResource("Admin.Customers.Reports.RegisteredCustomers.Fields.Period.month"),
                Customers = _customerReportService.GetRegisteredCustomersReport(30)
            });
            report.Add(new RegisteredCustomerReportLineVM
            {
                Period = _localizationService.GetResource("Admin.Customers.Reports.RegisteredCustomers.Fields.Period.year"),
                Customers = _customerReportService.GetRegisteredCustomersReport(365)
            });

            return report;
        }

        //[NonAction]
        //protected virtual IList<CustomerVM.AssociatedExternalAuthVM> GetAssociatedExternalAuthRecords(Customer customer)
        //{
        //    if (customer == null)
        //        throw new ArgumentNullException("customer");

        //    var result = new List<CustomerVM.AssociatedExternalAuthVM>();
        //    foreach (var record in _openAuthenticationService.GetExternalIdentifiersFor(customer))
        //    {
        //        var method = _openAuthenticationService.LoadExternalAuthenticationMethodBySystemName(record.ProviderSystemName);
        //        if (method == null)
        //            continue;

        //        result.Add(new CustomerVM.AssociatedExternalAuthVM
        //        {
        //            Id = record.Id,
        //            Email = record.Email,
        //            ExternalIdentifier = record.ExternalIdentifier,
        //            AuthMethodName = method.PluginDescriptor.FriendlyName
        //        });
        //    }

        //    return result;
        //}

        [NonAction]
        protected virtual CustomerVM PrepareCustomerModelForList(Customer customer)
        {
            return new CustomerVM
            {
                Id = customer.Id,
                Email = customer.IsRegistered() ? customer.Email : _localizationService.GetResource("Admin.Customers.Guest"),
                Username = customer.Username,
                FullName = customer.GetFullName(_genericAttributeService),
                Company = customer.GetAttribute<string>(SystemCustomerAttributeNames.Company, _genericAttributeService),
                Phone = customer.GetAttribute<string>(SystemCustomerAttributeNames.Phone, _genericAttributeService),
                ZipPostalCode = customer.GetAttribute<string>(SystemCustomerAttributeNames.ZipPostalCode, _genericAttributeService),
                CustomerRoleNames = GetCustomerRolesNames(customer.CustomerRoles.ToList()),
                Active = customer.Active,
                CreatedOn = _dateTimeHelper.ConvertToUserTime(customer.CreatedOnUtc, DateTimeKind.Utc),
                LastActivityDate = _dateTimeHelper.ConvertToUserTime(customer.LastActivityDateUtc, DateTimeKind.Utc),
            };
        }

        [NonAction]
        protected virtual string ValidateCustomerRoles(IList<CustomerRole> customerRoles)
        {
            if (customerRoles == null)
                throw new ArgumentNullException("customerRoles");

            //ensure a customer is not added to both 'Guests' and 'Registered' customer roles
            //ensure that a customer is in at least one required role ('Guests' and 'Registered')
            bool isInGuestsRole = customerRoles.FirstOrDefault(cr => cr.SystemName == SystemCustomerRoleNames.Guests) != null;
            bool isInRegisteredRole = customerRoles.FirstOrDefault(cr => cr.SystemName == SystemCustomerRoleNames.Registered) != null;
            if (isInGuestsRole && isInRegisteredRole)
                return _localizationService.GetResource("Admin.Customers.Customers.GuestsAndRegisteredRolesError");
            if (!isInGuestsRole && !isInRegisteredRole)
                return _localizationService.GetResource("Admin.Customers.Customers.AddCustomerToGuestsOrRegisteredRoleError");

            //no errors
            return "";
        }

        [NonAction]
        protected virtual void PrepareVendorsModel(CustomerVM model)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            model.AvailableVendors.Add(new System.Web.Mvc.SelectListItem
            {
                Text = _localizationService.GetResource("Admin.Customers.Customers.Fields.Vendor.None"),
                Value = "0"
            });
            var vendors = SelectListHelper.GetVendorList(_vendorService, true);
            foreach (var v in vendors)
                model.AvailableVendors.Add(v);
        }

        [NonAction]
        protected virtual void PrepareCustomerAttributeModel(CustomerVM model, Customer customer)
        {
            var customerAttributes = _customerAttributeService.GetAllCustomerAttributes();
            foreach (var attribute in customerAttributes)
            {
                var attributeModel = new CustomerVM.CustomerAttributeVM
                {
                    Id = attribute.Id,
                    Name = attribute.Name,
                    IsRequired = attribute.IsRequired,
                    AttributeControlType = attribute.AttributeControlType,
                };

                if (attribute.ShouldHaveValues())
                {
                    //values
                    var attributeValues = _customerAttributeService.GetCustomerAttributeValues(attribute.Id);
                    foreach (var attributeValue in attributeValues)
                    {
                        var attributeValueModel = new CustomerVM.CustomerAttributeValueVM
                        {
                            Id = attributeValue.Id,
                            Name = attributeValue.Name,
                            IsPreSelected = attributeValue.IsPreSelected
                        };
                        attributeModel.Values.Add(attributeValueModel);
                    }
                }


                //set already selected attributes
                if (customer != null)
                {
                    var selectedCustomerAttributes = customer.GetAttribute<string>(SystemCustomerAttributeNames.CustomCustomerAttributes, _genericAttributeService);
                    switch (attribute.AttributeControlType)
                    {
                        case AttributeControlType.DropdownList:
                        case AttributeControlType.RadioList:
                        case AttributeControlType.Checkboxes:
                            {
                                if (!String.IsNullOrEmpty(selectedCustomerAttributes))
                                {
                                    //clear default selection
                                    foreach (var item in attributeModel.Values)
                                        item.IsPreSelected = false;

                                    //select new values
                                    var selectedValues = _customerAttributeParser.ParseCustomerAttributeValues(selectedCustomerAttributes);
                                    foreach (var attributeValue in selectedValues)
                                        foreach (var item in attributeModel.Values)
                                            if (attributeValue.Id == item.Id)
                                                item.IsPreSelected = true;
                                }
                            }
                            break;
                        case AttributeControlType.ReadonlyCheckboxes:
                            {
                                //do nothing
                                //values are already pre-set
                            }
                            break;
                        case AttributeControlType.TextBox:
                        case AttributeControlType.MultilineTextbox:
                            {
                                if (!String.IsNullOrEmpty(selectedCustomerAttributes))
                                {
                                    var enteredText = _customerAttributeParser.ParseValues(selectedCustomerAttributes, attribute.Id);
                                    if (enteredText.Any())
                                        attributeModel.DefaultValue = enteredText[0];
                                }
                            }
                            break;
                        case AttributeControlType.Datepicker:
                        case AttributeControlType.ColorSquares:
                        case AttributeControlType.ImageSquares:
                        case AttributeControlType.FileUpload:
                        default:
                            //not supported attribute control types
                            break;
                    }
                }

                model.CustomerAttributes.Add(attributeModel);
            }
        }

        [NonAction]
        protected virtual string ParseCustomCustomerAttributes(System.Web.Mvc.FormCollection form)
        {
            if (form == null)
                throw new ArgumentNullException("form");

            string attributesXml = "";
            var customerAttributes = _customerAttributeService.GetAllCustomerAttributes();
            foreach (var attribute in customerAttributes)
            {
                string controlId = string.Format("customer_attribute_{0}", attribute.Id);
                switch (attribute.AttributeControlType)
                {
                    case AttributeControlType.DropdownList:
                    case AttributeControlType.RadioList:
                        {
                            var ctrlAttributes = form[controlId];
                            if (!String.IsNullOrEmpty(ctrlAttributes))
                            {
                                int selectedAttributeId = int.Parse(ctrlAttributes);
                                if (selectedAttributeId > 0)
                                    attributesXml = _customerAttributeParser.AddCustomerAttribute(attributesXml,
                                        attribute, selectedAttributeId.ToString());
                            }
                        }
                        break;
                    case AttributeControlType.Checkboxes:
                        {
                            var cblAttributes = form[controlId];
                            if (!String.IsNullOrEmpty(cblAttributes))
                            {
                                foreach (var item in cblAttributes.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                                {
                                    int selectedAttributeId = int.Parse(item);
                                    if (selectedAttributeId > 0)
                                        attributesXml = _customerAttributeParser.AddCustomerAttribute(attributesXml,
                                            attribute, selectedAttributeId.ToString());
                                }
                            }
                        }
                        break;
                    case AttributeControlType.ReadonlyCheckboxes:
                        {
                            //load read-only (already server-side selected) values
                            var attributeValues = _customerAttributeService.GetCustomerAttributeValues(attribute.Id);
                            foreach (var selectedAttributeId in attributeValues
                                .Where(v => v.IsPreSelected)
                                .Select(v => v.Id)
                                .ToList())
                            {
                                attributesXml = _customerAttributeParser.AddCustomerAttribute(attributesXml,
                                            attribute, selectedAttributeId.ToString());
                            }
                        }
                        break;
                    case AttributeControlType.TextBox:
                    case AttributeControlType.MultilineTextbox:
                        {
                            var ctrlAttributes = form[controlId];
                            if (!String.IsNullOrEmpty(ctrlAttributes))
                            {
                                string enteredText = ctrlAttributes.Trim();
                                attributesXml = _customerAttributeParser.AddCustomerAttribute(attributesXml,
                                    attribute, enteredText);
                            }
                        }
                        break;
                    case AttributeControlType.Datepicker:
                    case AttributeControlType.ColorSquares:
                    case AttributeControlType.ImageSquares:
                    case AttributeControlType.FileUpload:
                    //not supported customer attributes
                    default:
                        break;
                }
            }

            return attributesXml;
        }

        [NonAction]
        protected virtual void PrepareCustomerModel(CustomerVM model, Customer customer, bool excludeProperties)
        {
            var allStores = _storeService.GetAllStores();
            if (customer != null)
            {
                model.Id = customer.Id;
                if (!excludeProperties)
                {
                    model.Email = customer.Email;
                    model.Username = customer.Username;
                    model.VendorId = customer.VendorId;
                    model.AdminComment = customer.AdminComment;
                    model.IsTaxExempt = customer.IsTaxExempt;
                    model.Active = customer.Active;

                    if (customer.RegisteredInStoreId == 0 || allStores.All(s => s.Id != customer.RegisteredInStoreId))
                        model.RegisteredInStore = string.Empty;
                    else
                        model.RegisteredInStore = allStores.First(s => s.Id == customer.RegisteredInStoreId).Name;

                    var affiliate = _affiliateService.GetAffiliateById(customer.AffiliateId);
                    if (affiliate != null)
                    {
                        model.AffiliateId = affiliate.Id;
                        model.AffiliateName = affiliate.GetFullName();
                    }

                    model.TimeZoneId = customer.GetAttribute<string>(SystemCustomerAttributeNames.TimeZoneId, _genericAttributeService);
                    model.VatNumber = customer.GetAttribute<string>(SystemCustomerAttributeNames.VatNumber, _genericAttributeService);
                    model.VatNumberStatusNote = ((VatNumberStatus)customer.GetAttribute<int>(SystemCustomerAttributeNames.VatNumberStatusId, _genericAttributeService))
                        .GetLocalizedEnum(_localizationService, _baseService.WorkContext);
                    model.CreatedOn = _dateTimeHelper.ConvertToUserTime(customer.CreatedOnUtc, DateTimeKind.Utc);
                    model.LastActivityDate = _dateTimeHelper.ConvertToUserTime(customer.LastActivityDateUtc, DateTimeKind.Utc);
                    model.LastIpAddress = customer.LastIpAddress;
                    model.LastVisitedPage = customer.GetAttribute<string>(SystemCustomerAttributeNames.LastVisitedPage, _genericAttributeService);

                    model.SelectedCustomerRoleIds = customer.CustomerRoles.Select(cr => cr.Id).ToList();

                    ////newsletter subscriptions
                    //if (!String.IsNullOrEmpty(customer.Email))
                    //{
                    //    var newsletterSubscriptionStoreIds = new List<int>();
                    //    foreach (var store in allStores)
                    //    {
                    //        var newsletterSubscription = _newsLetterSubscriptionService
                    //            .GetNewsLetterSubscriptionByEmailAndStoreId(customer.Email, store.Id);
                    //        if (newsletterSubscription != null)
                    //            newsletterSubscriptionStoreIds.Add(store.Id);
                    //        model.SelectedNewsletterSubscriptionStoreIds = newsletterSubscriptionStoreIds.ToArray();
                    //    }
                    //}

                    //form fields
                    model.FirstName = customer.GetAttribute<string>(SystemCustomerAttributeNames.FirstName, _genericAttributeService);
                    model.LastName = customer.GetAttribute<string>(SystemCustomerAttributeNames.LastName, _genericAttributeService);
                    model.Gender = customer.GetAttribute<string>(SystemCustomerAttributeNames.Gender, _genericAttributeService);
                    model.DateOfBirth = customer.GetAttribute<DateTime?>(SystemCustomerAttributeNames.DateOfBirth, _genericAttributeService);
                    model.Company = customer.GetAttribute<string>(SystemCustomerAttributeNames.Company, _genericAttributeService);
                    model.StreetAddress = customer.GetAttribute<string>(SystemCustomerAttributeNames.StreetAddress, _genericAttributeService);
                    model.StreetAddress2 = customer.GetAttribute<string>(SystemCustomerAttributeNames.StreetAddress2, _genericAttributeService);
                    model.ZipPostalCode = customer.GetAttribute<string>(SystemCustomerAttributeNames.ZipPostalCode, _genericAttributeService);
                    model.City = customer.GetAttribute<string>(SystemCustomerAttributeNames.City, _genericAttributeService);
                    model.CountryId = customer.GetAttribute<int>(SystemCustomerAttributeNames.CountryId, _genericAttributeService);
                    model.StateProvinceId = customer.GetAttribute<int>(SystemCustomerAttributeNames.StateProvinceId, _genericAttributeService);
                    model.Phone = customer.GetAttribute<string>(SystemCustomerAttributeNames.Phone, _genericAttributeService);
                    model.Fax = customer.GetAttribute<string>(SystemCustomerAttributeNames.Fax, _genericAttributeService);
                }
            }

            model.UsernamesEnabled = _customerSettings.UsernamesEnabled;
            model.AllowCustomersToSetTimeZone = _dateTimeSettings.AllowCustomersToSetTimeZone;
            foreach (var tzi in _dateTimeHelper.GetSystemTimeZones())
                model.AvailableTimeZones.Add(new System.Web.Mvc.SelectListItem { Text = tzi.DisplayName, Value = tzi.Id, Selected = (tzi.Id == model.TimeZoneId) });
            if (customer != null)
            {
                model.DisplayVatNumber = _taxSettings.EuVatEnabled;
            }
            else
            {
                model.DisplayVatNumber = false;
            }

            //vendors
            PrepareVendorsModel(model);
            //customer attributes
            PrepareCustomerAttributeModel(model, customer);

            model.GenderEnabled = _customerSettings.GenderEnabled;
            model.DateOfBirthEnabled = _customerSettings.DateOfBirthEnabled;
            model.CompanyEnabled = _customerSettings.CompanyEnabled;
            model.StreetAddressEnabled = _customerSettings.StreetAddressEnabled;
            model.StreetAddress2Enabled = _customerSettings.StreetAddress2Enabled;
            model.ZipPostalCodeEnabled = _customerSettings.ZipPostalCodeEnabled;
            model.CityEnabled = _customerSettings.CityEnabled;
            model.CountryEnabled = _customerSettings.CountryEnabled;
            model.StateProvinceEnabled = _customerSettings.StateProvinceEnabled;
            model.PhoneEnabled = _customerSettings.PhoneEnabled;
            model.FaxEnabled = _customerSettings.FaxEnabled;

            //countries and states
            if (_customerSettings.CountryEnabled)
            {
                model.AvailableCountries.Add(new System.Web.Mvc.SelectListItem { Text = _localizationService.GetResource("Admin.Address.SelectCountry"), Value = "0" });
                foreach (var c in _countryService.GetAllCountries(showHidden: true))
                {
                    model.AvailableCountries.Add(new System.Web.Mvc.SelectListItem
                    {
                        Text = c.Name,
                        Value = c.Id.ToString(),
                        Selected = c.Id == model.CountryId
                    });
                }

                if (_customerSettings.StateProvinceEnabled)
                {
                    //states
                    var states = _stateProvinceService.GetStateProvincesByCountryId(model.CountryId).ToList();
                    if (states.Any())
                    {
                        model.AvailableStates.Add(new System.Web.Mvc.SelectListItem { Text = _localizationService.GetResource("Admin.Address.SelectState"), Value = "0" });

                        foreach (var s in states)
                        {
                            model.AvailableStates.Add(new System.Web.Mvc.SelectListItem { Text = s.Name, Value = s.Id.ToString(), Selected = (s.Id == model.StateProvinceId) });
                        }
                    }
                    else
                    {
                        bool anyCountrySelected = model.AvailableCountries.Any(x => x.Selected);

                        model.AvailableStates.Add(new System.Web.Mvc.SelectListItem
                        {
                            Text = _localizationService.GetResource(anyCountrySelected ? "Admin.Address.OtherNonUS" : "Admin.Address.SelectState"),
                            Value = "0"
                        });
                    }
                }
            }

            //newsletter subscriptions
            model.AvailableNewsletterSubscriptionStores = allStores
                .Select(s => new CustomerVM.StoreVM() { Id = s.Id, Name = s.Name })
                .ToList();

            //customer roles
            var allRoles = _customerService.GetAllCustomerRoles(true);
            var adminRole = allRoles.FirstOrDefault(c => c.SystemName == SystemCustomerRoleNames.Registered);
            //precheck Registered Role as a default role while creating a new customer through admin
            if (customer == null && adminRole != null)
            {
                model.SelectedCustomerRoleIds.Add(adminRole.Id);
            }
            foreach (var role in allRoles)
            {
                model.AvailableCustomerRoles.Add(new System.Web.Mvc.SelectListItem
                {
                    Text = role.Name,
                    Value = role.Id.ToString(),
                    Selected = model.SelectedCustomerRoleIds.Contains(role.Id)
                });
            }

            //reward points history
            if (customer != null)
            {
                model.DisplayRewardPointsHistory = _rewardPointsSettings.Enabled;
                model.AddRewardPointsValue = 0;
                model.AddRewardPointsMessage = "Some comment here...";

                //stores
                foreach (var store in allStores)
                {
                    model.RewardPointsAvailableStores.Add(new System.Web.Mvc.SelectListItem
                    {
                        Text = store.Name,
                        Value = store.Id.ToString(),
                        Selected = (store.Id == _storeContext.CurrentStore.Id)
                    });
                }
            }
            else
            {
                model.DisplayRewardPointsHistory = false;
            }
            ////external authentication records
            //if (customer != null)
            //{
            //    model.AssociatedExternalAuthRecords = GetAssociatedExternalAuthRecords(customer);
            //}
            //sending of the welcome message:
            //1. "admin approval" registration method
            //2. already created customer
            //3. registered
            model.AllowSendingOfWelcomeMessage = _customerSettings.UserRegistrationType == UserRegistrationType.AdminApproval &&
                customer != null &&
                customer.IsRegistered();
            //sending of the activation message
            //1. "email validation" registration method
            //2. already created customer
            //3. registered
            //4. not active
            model.AllowReSendingOfActivationMessage = _customerSettings.UserRegistrationType == UserRegistrationType.EmailValidation &&
                customer != null &&
                customer.IsRegistered() &&
                !customer.Active;
        }

        [NonAction]
        protected virtual void PrepareAddressModel(CustomerAddressVM model, Address address, Customer customer, bool excludeProperties)
        {
            if (customer == null)
                throw new ArgumentNullException("customer");

            model.CustomerId = customer.Id;
            if (address != null)
            {
                if (!excludeProperties)
                {
                    model.Address = address.ToModel();
                }
            }

            if (model.Address == null)
                model.Address = new AddressVM();

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
                model.Address.AvailableCountries.Add(new System.Web.Mvc.SelectListItem { Text = c.Name, Value = c.Id.ToString(), Selected = (c.Id == model.Address.CountryId) });
            //states
            var states = model.Address.CountryId.HasValue ? _stateProvinceService.GetStateProvincesByCountryId(model.Address.CountryId.Value, showHidden: true).ToList() : new List<StateProvince>();
            if (states.Any())
            {
                foreach (var s in states)
                    model.Address.AvailableStates.Add(new System.Web.Mvc.SelectListItem { Text = s.Name, Value = s.Id.ToString(), Selected = (s.Id == model.Address.StateProvinceId) });
            }
            else
                model.Address.AvailableStates.Add(new System.Web.Mvc.SelectListItem { Text = _localizationService.GetResource("Admin.Address.OtherNonUS"), Value = "0" });
            //customer attribute services
            model.Address.PrepareCustomAddressAttributes(address, _addressAttributeService, _addressAttributeParser);
        }

        [NonAction]
        private bool SecondAdminAccountExists(Customer customer)
        {
            var customers = _customerService.GetAllCustomers(customerRoleIds: new[] { _customerService.GetCustomerRoleBySystemName(SystemCustomerRoleNames.Administrators).Id });

            return customers.Any(c => c.Active && c.Id != customer.Id);
        }

        [NonAction]
        protected virtual CustomerRoleVM PrepareCustomerRoleModel(CustomerRole customerRole)
        {
            var model = customerRole.ToModel();
            var product = _productService.GetProductById(customerRole.PurchasedWithProductId);
            if (product != null)
            {
                model.PurchasedWithProductName = product.Name;
            }
            return model;
        }
        #endregion

        #region Methods

        #region Activity log

        [HttpGet]
        [Route("ActivityLogs/{pageIndex:int=0}/{pageSize:int=2147483647}", Name = "CustomerActivityLogs")]
        public HttpResponseMessage ListActivityLog(HttpRequestMessage request, int customerId, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageCustomers))
                {
                    var activityLog = _customerActivityService.GetAllActivities(null, null, customerId, 0, pageIndex, pageSize);
                    var gridModel = new DataSourceResult
                    {
                        Data = activityLog.Select(x =>
                        {
                            var m = new CustomerVM.ActivityLogVM
                            {
                                Id = x.Id,
                                ActivityLogTypeName = x.ActivityLogType.Name,
                                Comment = x.Comment,
                                CreatedOn = _dateTimeHelper.ConvertToUserTime(x.CreatedOnUtc, DateTimeKind.Utc),
                                IpAddress = x.IpAddress
                            };
                            return m;

                        }),
                        Total = activityLog.TotalCount
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

        #region Back in stock subscriptions

        [HttpPost]
        [Route("BackInStockSubscriptionList", Name = "BackInStockSubscriptionList")]
        public HttpResponseMessage BackInStockSubscriptionList(HttpRequestMessage request, DataSourceRequest command, int customerId)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageCustomers))
                {
                    var subscriptions = _backInStockSubscriptionService.GetAllSubscriptionsByCustomerId(customerId, 0, command.Page - 1, command.PageSize);
                    var gridModel = new DataSourceResult
                    {
                        Data = subscriptions.Select(x =>
                        {
                            var store = _storeService.GetStoreById(x.StoreId);
                            var product = x.Product;
                            var m = new CustomerVM.BackInStockSubscriptionVM
                            {
                                Id = x.Id,
                                StoreName = store != null ? store.Name : "Unknown",
                                ProductId = x.ProductId,
                                ProductName = product != null ? product.Name : "Unknown",
                                CreatedOn = _dateTimeHelper.ConvertToUserTime(x.CreatedOnUtc, DateTimeKind.Utc)
                            };
                            return m;

                        }),
                        Total = subscriptions.TotalCount
                    };
                    response = request.CreateResponse<DataSourceResult>(HttpStatusCode.OK, gridModel);
                }
                return response;

            });
        }

        #endregion

        #region Current shopping cart/ wishlist

        [HttpGet]
        [Route("GetCustomerCarts/{customerId:int}/{cartTypeId:int}", Name = "GetCustomerCarts")]
        public HttpResponseMessage GetCartList(HttpRequestMessage request, int customerId, int cartTypeId)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageCustomers))
                {
                    var customer = _customerService.GetCustomerById(customerId);
                    var cart = customer.ShoppingCartItems.Where(x => x.ShoppingCartTypeId == cartTypeId).ToList();

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
                                AttributeInfo = _productAttributeFormatter.FormatAttributes(sci.Product, sci.AttributesXml),
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

        #region Reports
        [HttpGet]
        [Route("Reports", Name = "CustomerReports")]
        public HttpResponseMessage Reports(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageCustomers))
                {
                    var model = new CustomerReportsVM();
                    //customers by number of orders
                    model.BestCustomersByNumberOfOrders = new BestCustomersReportVM();
                    model.BestCustomersByNumberOfOrders.AvailableOrderStatuses = OrderStatus.Pending.ToSelectList(_localizationService, _baseService.WorkContext, false).ToList();
                    model.BestCustomersByNumberOfOrders.AvailableOrderStatuses.Insert(0, new System.Web.Mvc.SelectListItem { Text = _localizationService.GetResource("Admin.Common.All"), Value = "0" });
                    model.BestCustomersByNumberOfOrders.AvailablePaymentStatuses = PaymentStatus.Pending.ToSelectList(_localizationService, _baseService.WorkContext, false).ToList();
                    model.BestCustomersByNumberOfOrders.AvailablePaymentStatuses.Insert(0, new System.Web.Mvc.SelectListItem { Text = _localizationService.GetResource("Admin.Common.All"), Value = "0" });
                    model.BestCustomersByNumberOfOrders.AvailableShippingStatuses = ShippingStatus.NotYetShipped.ToSelectList(_localizationService, _baseService.WorkContext, false).ToList();
                    model.BestCustomersByNumberOfOrders.AvailableShippingStatuses.Insert(0, new System.Web.Mvc.SelectListItem { Text = _localizationService.GetResource("Admin.Common.All"), Value = "0" });

                    //customers by order total
                    model.BestCustomersByOrderTotal = new BestCustomersReportVM();
                    model.BestCustomersByOrderTotal.AvailableOrderStatuses = OrderStatus.Pending.ToSelectList(_localizationService, _baseService.WorkContext, false).ToList();
                    model.BestCustomersByOrderTotal.AvailableOrderStatuses.Insert(0, new System.Web.Mvc.SelectListItem { Text = _localizationService.GetResource("Admin.Common.All"), Value = "0" });
                    model.BestCustomersByOrderTotal.AvailablePaymentStatuses = PaymentStatus.Pending.ToSelectList(_localizationService, _baseService.WorkContext, false).ToList();
                    model.BestCustomersByOrderTotal.AvailablePaymentStatuses.Insert(0, new System.Web.Mvc.SelectListItem { Text = _localizationService.GetResource("Admin.Common.All"), Value = "0" });
                    model.BestCustomersByOrderTotal.AvailableShippingStatuses = ShippingStatus.NotYetShipped.ToSelectList(_localizationService, _baseService.WorkContext, false).ToList();
                    model.BestCustomersByOrderTotal.AvailableShippingStatuses.Insert(0, new System.Web.Mvc.SelectListItem { Text = _localizationService.GetResource("Admin.Common.All"), Value = "0" });

                    response = request.CreateResponse<CustomerReportsVM>(HttpStatusCode.OK, model);
                }
                return response;

            });
        }

        [HttpPost]
        [Route("BestCustomersReportsByOrder", Name = "BestCustomersReportsByOrder")]
        public HttpResponseMessage ReportBestCustomersByOrderTotalList(HttpRequestMessage request, BestCustomersReportVM model, int pageIndex = 0, int pageSize = int.MaxValue)
        {

            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageCustomers))
                {
                    DateTime? startDateValue = (model.StartDate == null) ? null
                            : (DateTime?)_dateTimeHelper.ConvertToUtcTime(model.StartDate.Value, _dateTimeHelper.CurrentTimeZone);

                    DateTime? endDateValue = (model.EndDate == null) ? null
                                    : (DateTime?)_dateTimeHelper.ConvertToUtcTime(model.EndDate.Value, _dateTimeHelper.CurrentTimeZone).AddDays(1);

                    OrderStatus? orderStatus = model.OrderStatusId > 0 ? (OrderStatus?)(model.OrderStatusId) : null;
                    PaymentStatus? paymentStatus = model.PaymentStatusId > 0 ? (PaymentStatus?)(model.PaymentStatusId) : null;
                    ShippingStatus? shippingStatus = model.ShippingStatusId > 0 ? (ShippingStatus?)(model.ShippingStatusId) : null;


                    var items = _customerReportService.GetBestCustomersReport(startDateValue, endDateValue, orderStatus, paymentStatus, shippingStatus, 1, pageIndex, pageSize);

                    var gridModel = new DataSourceResult
                    {
                        Data = items.Select(x =>
                        {
                            var m = new BestCustomerReportLineVM
                            {
                                CustomerId = x.CustomerId,
                                OrderTotal = _priceFormatter.FormatPrice(x.OrderTotal, true, false),
                                OrderCount = x.OrderCount,
                            };
                            var customer = _customerService.GetCustomerById(x.CustomerId);
                            if (customer != null)
                            {
                                m.CustomerName = customer.IsRegistered() ? customer.Email : _localizationService.GetResource("Admin.Customers.Guest");
                            }
                            return m;
                        }),
                        Total = items.TotalCount
                    };

                    response = request.CreateResponse<DataSourceResult>(HttpStatusCode.OK, gridModel);
                }
                return response;

            });
        }

        [HttpPost]
        [Route("BestCustomersReportsByOrderNumber", Name = "BestCustomersReportsByOrderNumber")]
        public HttpResponseMessage ReportBestCustomersByNumberOfOrdersList(HttpRequestMessage request, BestCustomersReportVM model, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageCustomers))
                {
                    DateTime? startDateValue = (model.StartDate == null) ? null
                            : (DateTime?)_dateTimeHelper.ConvertToUtcTime(model.StartDate.Value, _dateTimeHelper.CurrentTimeZone);

                    DateTime? endDateValue = (model.EndDate == null) ? null
                                    : (DateTime?)_dateTimeHelper.ConvertToUtcTime(model.EndDate.Value, _dateTimeHelper.CurrentTimeZone).AddDays(1);

                    OrderStatus? orderStatus = model.OrderStatusId > 0 ? (OrderStatus?)(model.OrderStatusId) : null;
                    PaymentStatus? paymentStatus = model.PaymentStatusId > 0 ? (PaymentStatus?)(model.PaymentStatusId) : null;
                    ShippingStatus? shippingStatus = model.ShippingStatusId > 0 ? (ShippingStatus?)(model.ShippingStatusId) : null;


                    var items = _customerReportService.GetBestCustomersReport(startDateValue, endDateValue, orderStatus, paymentStatus, shippingStatus, 2, pageIndex, pageSize);
                    var gridModel = new DataSourceResult
                    {
                        Data = items.Select(x =>
                        {
                            var m = new BestCustomerReportLineVM
                            {
                                CustomerId = x.CustomerId,
                                OrderTotal = _priceFormatter.FormatPrice(x.OrderTotal, true, false),
                                OrderCount = x.OrderCount,
                            };
                            var customer = _customerService.GetCustomerById(x.CustomerId);
                            if (customer != null)
                            {
                                m.CustomerName = customer.IsRegistered() ? customer.Email : _localizationService.GetResource("Admin.Customers.Guest");
                            }
                            return m;
                        }),
                        Total = items.TotalCount
                    };
                    response = request.CreateResponse<DataSourceResult>(HttpStatusCode.OK, gridModel);
                }
                return response;

            });
        }

        [HttpGet]
        [Route("RegisteredCustomersReports", Name = "RegisteredCustomersReports")]
        public HttpResponseMessage ReportRegisteredCustomersList(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageCustomers))
                {
                    var model = GetReportRegisteredCustomersModel();
                    var gridModel = new DataSourceResult
                    {
                        Data = model,
                        Total = model.Count
                    };

                    response = request.CreateResponse<DataSourceResult>(HttpStatusCode.OK, gridModel);
                }
                return response;

            });
        }

        [HttpGet]
        [Route("LoadCustomerStatistics/{period}", Name = "LoadCustomerStatistics")]
        public HttpResponseMessage LoadCustomerStatistics(HttpRequestMessage request, string period)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageCustomers))
                {

                    var result = new List<object>();

                    var nowDt = _dateTimeHelper.ConvertToUserTime(DateTime.Now);
                    var timeZone = _dateTimeHelper.CurrentTimeZone;
                    var searchCustomerRoleIds = new[] { _customerService.GetCustomerRoleBySystemName(SystemCustomerRoleNames.Registered).Id };

                    var culture = new CultureInfo(_baseService.WorkContext.WorkingLanguage.LanguageCulture);

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
                                        value = _customerService.GetAllCustomers(
                                            createdFromUtc: _dateTimeHelper.ConvertToUtcTime(searchYearDateUser, timeZone),
                                            createdToUtc: _dateTimeHelper.ConvertToUtcTime(searchYearDateUser.AddMonths(1), timeZone),
                                            customerRoleIds: searchCustomerRoleIds,
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
                                        value = _customerService.GetAllCustomers(
                                            createdFromUtc: _dateTimeHelper.ConvertToUtcTime(searchMonthDateUser, timeZone),
                                            createdToUtc: _dateTimeHelper.ConvertToUtcTime(searchMonthDateUser.AddDays(1), timeZone),
                                            customerRoleIds: searchCustomerRoleIds,
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
                                        value = _customerService.GetAllCustomers(
                                            createdFromUtc: _dateTimeHelper.ConvertToUtcTime(searchWeekDateUser, timeZone),
                                            createdToUtc: _dateTimeHelper.ConvertToUtcTime(searchWeekDateUser.AddDays(1), timeZone),
                                            customerRoleIds: searchCustomerRoleIds,
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
                return response;

            });
        }

        #endregion

        #region Orders

        [HttpGet]
        [Route("Orders/{customerId:int}/{pageIndex:int=0}/{pageSize:int=2147483647}", Name = "CustomerOrders")]
        public HttpResponseMessage OrderList(HttpRequestMessage request, int customerId, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageCustomers))
                {
                    var orders = _orderService.SearchOrders(customerId: customerId);

                    var gridModel = new DataSourceResult
                    {
                        Data = orders.Skip((pageIndex) * pageSize).Take(pageSize)
                            .Select(order =>
                            {
                                var store = _storeService.GetStoreById(order.StoreId);
                                var orderModel = new CustomerVM.OrderVM
                                {
                                    Id = order.Id,
                                    OrderStatus = order.OrderStatus.GetLocalizedEnum(_localizationService, _baseService.WorkContext),
                                    OrderStatusId = order.OrderStatusId,
                                    PaymentStatus = order.PaymentStatus.GetLocalizedEnum(_localizationService, _baseService.WorkContext),
                                    ShippingStatus = order.ShippingStatus.GetLocalizedEnum(_localizationService, _baseService.WorkContext),
                                    OrderTotal = _priceFormatter.FormatPrice(order.OrderTotal, true, false),
                                    StoreName = store != null ? store.Name : "Unknown",
                                    CreatedOn = _dateTimeHelper.ConvertToUserTime(order.CreatedOnUtc, DateTimeKind.Utc),
                                    CustomOrderNumber = order.CustomOrderNumber
                                };
                                return orderModel;
                            }),
                        Total = orders.Count
                    };

                    response = request.CreateResponse<DataSourceResult>(HttpStatusCode.OK, gridModel);
                }
                return response;

            });
        }

        #endregion

        #region Addresses

        [HttpGet]
        [Route("{customerId:int}/Addresses", Name = "CustomerAddresses")]
        public HttpResponseMessage AddressesSelect(HttpRequestMessage request, int customerId)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageCustomers))
                {
                    var customer = _customerService.GetCustomerById(customerId);
                    if (customer == null)
                        throw new ArgumentException("No customer found with the specified id", "customerId");

                    var addresses = customer.Addresses.OrderByDescending(a => a.CreatedOnUtc).ThenByDescending(a => a.Id).ToList();
                    var gridModel = new DataSourceResult
                    {
                        Data = addresses.Select(x =>
                        {
                            var model = x.ToModel();
                            var addressHtmlSb = new StringBuilder("<div>");
                            if (_addressSettings.CompanyEnabled && !String.IsNullOrEmpty(model.Company))
                                addressHtmlSb.AppendFormat("{0}<br />", HttpUtility.HtmlEncode(model.Company));
                            if (_addressSettings.StreetAddressEnabled && !String.IsNullOrEmpty(model.Address1))
                                addressHtmlSb.AppendFormat("{0}<br />", HttpUtility.HtmlEncode(model.Address1));
                            if (_addressSettings.StreetAddress2Enabled && !String.IsNullOrEmpty(model.Address2))
                                addressHtmlSb.AppendFormat("{0}<br />", HttpUtility.HtmlEncode(model.Address2));
                            if (_addressSettings.CityEnabled && !String.IsNullOrEmpty(model.City))
                                addressHtmlSb.AppendFormat("{0},", HttpUtility.HtmlEncode(model.City));
                            if (_addressSettings.StateProvinceEnabled && !String.IsNullOrEmpty(model.StateProvinceName))
                                addressHtmlSb.AppendFormat("{0},", HttpUtility.HtmlEncode(model.StateProvinceName));
                            if (_addressSettings.ZipPostalCodeEnabled && !String.IsNullOrEmpty(model.ZipPostalCode))
                                addressHtmlSb.AppendFormat("{0}<br />", HttpUtility.HtmlEncode(model.ZipPostalCode));
                            if (_addressSettings.CountryEnabled && !String.IsNullOrEmpty(model.CountryName))
                                addressHtmlSb.AppendFormat("{0}", HttpUtility.HtmlEncode(model.CountryName));
                            var customAttributesFormatted = _addressAttributeFormatter.FormatAttributes(x.CustomAttributes);
                            if (!String.IsNullOrEmpty(customAttributesFormatted))
                            {
                                //already encoded
                                addressHtmlSb.AppendFormat("<br />{0}", customAttributesFormatted);
                            }
                            addressHtmlSb.Append("</div>");
                            model.AddressHtml = addressHtmlSb.ToString();
                            return model;
                        }),
                        Total = addresses.Count
                    };
                    response = request.CreateResponse<DataSourceResult>(HttpStatusCode.OK, gridModel);
                }
                return response;

            });


        }

        [HttpPost]
        [Route("DeleteAddress")]
        public HttpResponseMessage AddressDelete(HttpRequestMessage request, int id, int customerId)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageCustomers))
                {

                    var customer = _customerService.GetCustomerById(customerId);
                    if (customer == null)
                        throw new ArgumentException("No customer found with the specified id", "customerId");

                    var address = customer.Addresses.FirstOrDefault(a => a.Id == id);
                    if (address == null)
                    {
                        //No customer found with the specified id
                        response = request.CreateErrorResponse(HttpStatusCode.NotFound, "This customer does not have this address");
                        return response;
                    }
                    customer.RemoveAddress(address);
                    _customerService.UpdateCustomer(customer);
                    //now delete the address record
                    _addressService.DeleteAddress(address);

                    _baseService.Commit();
                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                return response;

            });
        }

        [HttpGet]
        [Route("{customerId:int}/CreateAddressModal")]
        public HttpResponseMessage AddressCreate(HttpRequestMessage request, int customerId)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageCustomers))
                {
                    var customer = _customerService.GetCustomerById(customerId);
                    if (customer == null)
                    {
                        //No customer found with the specified id
                        string uri = Url.Link("DefaultCustomerPageLoad", null);
                        response.Headers.Location = new Uri(uri);
                        return response;
                    }

                    var model = new CustomerAddressVM();
                    PrepareAddressModel(model, null, customer, false);

                    response = request.CreateResponse<CustomerAddressVM>(HttpStatusCode.OK, model);
                }
                return response;

            });
        }

        [HttpPost]
        [Route("CreateAddress")]
        public HttpResponseMessage AddressCreate(HttpRequestMessage request, CustomerAddressVM model)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageCustomers))
                {
                    var customer = _customerService.GetCustomerById(model.CustomerId);
                    if (customer == null)
                    {
                        //No customer found with the specified id
                        string uri = Url.Link("DefaultCustomerPageLoad", null);
                        response.Headers.Location = new Uri(uri);
                        return response;
                    }

                    //custom address attributes
                    //var customAttributes = form.ParseCustomAddressAttributes(_addressAttributeParser, _addressAttributeService);
                    //var customAttributeWarnings = _addressAttributeParser.GetAttributeWarnings(customAttributes);
                    //foreach (var error in customAttributeWarnings)
                    //{
                    //    ModelState.AddModelError("", error);
                    //}

                    if (ModelState.IsValid)
                    {
                        var address = model.Address.ToEntity();
                        //address.CustomAttributes = customAttributes;
                        address.CreatedOnUtc = DateTime.UtcNow;
                        //some validation
                        if (address.CountryId == 0)
                            address.CountryId = null;
                        if (address.StateProvinceId == 0)
                            address.StateProvinceId = null;
                        customer.Addresses.Add(address);
                        _customerService.UpdateCustomer(customer);

                        _baseService.Commit();
                        string uri = Url.Link("CustomerAddressEdit", new { addressId = address.Id, customerId = model.CustomerId });
                        response.Headers.Location = new Uri(uri);
                        return response;
                    }

                    //If we got this far, something failed, redisplay form
                    PrepareAddressModel(model, null, customer, true);

                    response = request.CreateResponse<CustomerAddressVM>(HttpStatusCode.OK, model);
                }
                return response;

            });


        }

        [HttpGet]
        [Route("{customerId:int}/AddressEditModal/{addressId:int}", Name = "CustomerAddressEdit")]
        public HttpResponseMessage AddressEdit(HttpRequestMessage request, int addressId, int customerId)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageCustomers))
                {
                    var customer = _customerService.GetCustomerById(customerId);
                    if (customer == null)
                    {
                        //No customer found with the specified id
                        string uri = Url.Link("DefaultCustomerPageLoad", null);
                        response.Headers.Location = new Uri(uri);
                        return response;
                    }

                    var address = _addressService.GetAddressById(addressId);
                    if (address == null)
                    {
                        //No address found with the specified id
                        string uri = Url.Link("GetCustomerById", new { id = customer.Id });
                        response.Headers.Location = new Uri(uri);
                        return response;
                    }

                    var model = new CustomerAddressVM();
                    PrepareAddressModel(model, address, customer, false);

                    response = request.CreateResponse<CustomerAddressVM>(HttpStatusCode.OK, model);
                }
                return response;

            });
        }

        [HttpPost]
        //[System.Web.Mvc.ValidateInput(false)]
        [Route("EditAddress")]
        public HttpResponseMessage AddressEdit(HttpRequestMessage request, CustomerAddressVM model/*, System.Web.Mvc.FormCollection form*/)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageCustomers))
                {
                    var customer = _customerService.GetCustomerById(model.CustomerId);
                    if (customer == null)
                    {
                        //No customer found with the specified id
                        string uri = Url.Link("DefaultCustomerPageLoad", null);
                        response.Headers.Location = new Uri(uri);
                        return response;
                    }

                    var address = _addressService.GetAddressById(model.Address.Id);
                    if (address == null)
                    {
                        //No address found with the specified id
                        string uri = Url.Link("GetCustomerById", new { id = customer.Id });
                        response.Headers.Location = new Uri(uri);
                        return response;
                    }

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

                        _baseService.Commit();
                        string uri = Url.Link("CustomerAddressEdit", new { addressId = model.Address.Id, customerId = model.CustomerId });
                        response.Headers.Location = new Uri(uri);
                        return response;
                    }

                    //If we got this far, something failed, redisplay form
                    PrepareAddressModel(model, address, customer, true);

                    response = request.CreateResponse<CustomerAddressVM>(HttpStatusCode.OK, model);
                    return response;
                }
                return response;

            });
        }

        #endregion

        #region Customers
        [HttpGet]
        [Route("", Name = "DefaultCustomerPageLoad")]
        public HttpResponseMessage List(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageCustomers))
                {
                    //load registered customers by default
                    var defaultRoleIds = new List<int> { _customerService.GetCustomerRoleBySystemName(SystemCustomerRoleNames.Registered).Id };
                    var model = new CustomerListVM
                    {
                        UsernamesEnabled = _customerSettings.UsernamesEnabled,
                        DateOfBirthEnabled = _customerSettings.DateOfBirthEnabled,
                        CompanyEnabled = _customerSettings.CompanyEnabled,
                        PhoneEnabled = _customerSettings.PhoneEnabled,
                        ZipPostalCodeEnabled = _customerSettings.ZipPostalCodeEnabled,
                        SearchCustomerRoleIds = defaultRoleIds,
                    };
                    var allRoles = _customerService.GetAllCustomerRoles(true);
                    foreach (var role in allRoles)
                    {
                        model.AvailableCustomerRoles.Add(new System.Web.Mvc.SelectListItem
                        {
                            Text = role.Name,
                            Value = role.Id.ToString(),
                            Selected = defaultRoleIds.Any(x => x == role.Id)
                        });
                    }

                    response = request.CreateResponse<CustomerListVM>(HttpStatusCode.OK, model);
                }
                return response;

            });
        }

        [HttpGet]
        [Route("GetCustomerById/{customerId:int}", Name = "GetCustomerById")]
        public HttpResponseMessage GetCustomerById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageCustomers))
                {
                    var customer = _customerService.GetCustomerById(id);
                    if (customer == null || customer.Deleted)
                    {
                        //No customer found with the specified id
                        string uri = Url.Link("DefaultCustomerPageLoad", null);
                        response.Headers.Location = new Uri(uri);
                    }

                    var model = new CustomerVM();
                    PrepareCustomerModel(model, customer, false);

                    response = request.CreateResponse<CustomerVM>(HttpStatusCode.OK, model);
                }
                return response;

            });


        }

        [HttpPost]
        [Route("SearchCustomer")]
        public HttpResponseMessage CustomerList(HttpRequestMessage request, CustomerListVM model, int[] searchCustomerRoleIds, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            //we use own own binder for searchCustomerRoleIds property 
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageCustomers))
                {
                    var searchDayOfBirth = 0;
                    int searchMonthOfBirth = 0;
                    if (!String.IsNullOrWhiteSpace(model.SearchDayOfBirth))
                        searchDayOfBirth = Convert.ToInt32(model.SearchDayOfBirth);
                    if (!String.IsNullOrWhiteSpace(model.SearchMonthOfBirth))
                        searchMonthOfBirth = Convert.ToInt32(model.SearchMonthOfBirth);

                    var customers = _customerService.GetAllCustomers(
                        customerRoleIds: searchCustomerRoleIds,
                        email: model.SearchEmail,
                        username: model.SearchUsername,
                        firstName: model.SearchFirstName,
                        lastName: model.SearchLastName,
                        dayOfBirth: searchDayOfBirth,
                        monthOfBirth: searchMonthOfBirth,
                        company: model.SearchCompany,
                        phone: model.SearchPhone,
                        zipPostalCode: model.SearchZipPostalCode,
                        ipAddress: model.SearchIpAddress,
                        loadOnlyWithShoppingCart: false,
                        pageIndex: pageIndex,
                        pageSize: pageSize);
                    var gridModel = new DataSourceResult
                    {
                        Data = customers.Select(PrepareCustomerModelForList),
                        Total = customers.TotalCount
                    };
                    response = request.CreateResponse<DataSourceResult>(HttpStatusCode.OK, gridModel);
                }
                return response;

            });


        }

        [HttpGet]
        [Route("CreateCustomerModal")]
        public HttpResponseMessage Create(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageCustomers))
                {
                    var model = new CustomerVM();
                    PrepareCustomerModel(model, null, false);
                    //default value
                    model.Active = true;

                    response = request.CreateResponse<CustomerVM>(HttpStatusCode.OK, model);
                }
                return response;

            });
        }

        [HttpPost]
        [Route("CreateCustomer")]
        public HttpResponseMessage Create(HttpRequestMessage request, CustomerVM model, bool continueEditing = false, System.Web.Mvc.FormCollection form = null)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageCustomers))
                {

                    if (!String.IsNullOrWhiteSpace(model.Email))
                    {
                        var cust2 = _customerService.GetCustomerByEmail(model.Email);
                        if (cust2 != null)
                            ModelState.AddModelError("", "Email is already registered");
                    }
                    if (!String.IsNullOrWhiteSpace(model.Username) & _customerSettings.UsernamesEnabled)
                    {
                        var cust2 = _customerService.GetCustomerByUsername(model.Username);
                        if (cust2 != null)
                            ModelState.AddModelError("", "Username is already registered");
                    }

                    //validate customer roles
                    var allCustomerRoles = _customerService.GetAllCustomerRoles(true);
                    var newCustomerRoles = new List<CustomerRole>();
                    foreach (var customerRole in allCustomerRoles)
                        if (model.SelectedCustomerRoleIds.Contains(customerRole.Id))
                            newCustomerRoles.Add(customerRole);
                    var customerRolesError = ValidateCustomerRoles(newCustomerRoles);
                    if (!String.IsNullOrEmpty(customerRolesError))
                    {
                        ModelState.AddModelError("", customerRolesError);
                        LogError(customerRolesError);
                    }

                    // Ensure that valid email address is entered if Registered role is checked to avoid registered customers with empty email address
                    if (newCustomerRoles.Any() && newCustomerRoles.FirstOrDefault(c => c.SystemName == SystemCustomerRoleNames.Registered) != null && !CommonHelper.IsValidEmail(model.Email))
                    {
                        ModelState.AddModelError("", _localizationService.GetResource("Admin.Customers.Customers.ValidEmailRequiredRegisteredRole"));
                        LogError(_localizationService.GetResource("Admin.Customers.Customers.ValidEmailRequiredRegisteredRole"));
                    }

                    //custom customer attributes
                    var customerAttributesXml = form == null ? null : ParseCustomCustomerAttributes(form);
                    if (customerAttributesXml != null && newCustomerRoles.Any() && newCustomerRoles.FirstOrDefault(c => c.SystemName == SystemCustomerRoleNames.Registered) != null)
                    {
                        var customerAttributeWarnings = _customerAttributeParser.GetAttributeWarnings(customerAttributesXml);
                        foreach (var error in customerAttributeWarnings)
                        {
                            ModelState.AddModelError("", error);
                        }
                    }

                    if (ModelState.IsValid)
                    {
                        var customer = new Customer
                        {
                            CustomerGuid = Guid.NewGuid(),
                            Email = model.Email,
                            Username = model.Username,
                            VendorId = model.VendorId,
                            AdminComment = model.AdminComment,
                            IsTaxExempt = model.IsTaxExempt,
                            Active = model.Active,
                            CreatedOnUtc = DateTime.UtcNow,
                            LastActivityDateUtc = DateTime.UtcNow,
                            RegisteredInStoreId = _storeContext.CurrentStore.Id
                        };
                        _customerService.InsertCustomer(customer);

                        //form fields
                        if (_dateTimeSettings.AllowCustomersToSetTimeZone)
                            _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.TimeZoneId, model.TimeZoneId);
                        if (_customerSettings.GenderEnabled)
                            _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.Gender, model.Gender);
                        _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.FirstName, model.FirstName);
                        _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.LastName, model.LastName);
                        if (_customerSettings.DateOfBirthEnabled)
                            _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.DateOfBirth, model.DateOfBirth);
                        if (_customerSettings.CompanyEnabled)
                            _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.Company, model.Company);
                        if (_customerSettings.StreetAddressEnabled)
                            _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.StreetAddress, model.StreetAddress);
                        if (_customerSettings.StreetAddress2Enabled)
                            _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.StreetAddress2, model.StreetAddress2);
                        if (_customerSettings.ZipPostalCodeEnabled)
                            _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.ZipPostalCode, model.ZipPostalCode);
                        if (_customerSettings.CityEnabled)
                            _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.City, model.City);
                        if (_customerSettings.CountryEnabled)
                            _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.CountryId, model.CountryId);
                        if (_customerSettings.CountryEnabled && _customerSettings.StateProvinceEnabled)
                            _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.StateProvinceId, model.StateProvinceId);
                        if (_customerSettings.PhoneEnabled)
                            _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.Phone, model.Phone);
                        if (_customerSettings.FaxEnabled)
                            _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.Fax, model.Fax);

                        //custom customer attributes
                        if (customerAttributesXml != null)
                        {
                            _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.CustomCustomerAttributes, customerAttributesXml);
                        }



                        //newsletter subscriptions
                        //if (!String.IsNullOrEmpty(customer.Email))
                        //{
                        //    var allStores = _storeService.GetAllStores();
                        //    foreach (var store in allStores)
                        //    {
                        //        var newsletterSubscription = _newsLetterSubscriptionService
                        //            .GetNewsLetterSubscriptionByEmailAndStoreId(customer.Email, store.Id);
                        //        if (model.SelectedNewsletterSubscriptionStoreIds != null &&
                        //            model.SelectedNewsletterSubscriptionStoreIds.Contains(store.Id))
                        //        {
                        //            //subscribed
                        //            if (newsletterSubscription == null)
                        //            {
                        //                _newsLetterSubscriptionService.InsertNewsLetterSubscription(new NewsLetterSubscription
                        //                {
                        //                    NewsLetterSubscriptionGuid = Guid.NewGuid(),
                        //                    Email = customer.Email,
                        //                    Active = true,
                        //                    StoreId = store.Id,
                        //                    CreatedOnUtc = DateTime.UtcNow
                        //                });
                        //            }
                        //        }
                        //        else
                        //        {
                        //            //not subscribed
                        //            if (newsletterSubscription != null)
                        //            {
                        //                _newsLetterSubscriptionService.DeleteNewsLetterSubscription(newsletterSubscription);
                        //            }
                        //        }
                        //    }
                        //}

                        //password
                        if (!String.IsNullOrWhiteSpace(model.Password))
                        {
                            var changePassRequest = new ChangePasswordRequest(model.Email, false, _customerSettings.DefaultPasswordFormat, model.Password);
                            var changePassResult = _customerRegistrationService.ChangePassword(changePassRequest);
                            if (!changePassResult.Success)
                            {
                                foreach (var changePassError in changePassResult.Errors)
                                    LogError(changePassError);
                            }
                        }

                        //customer roles
                        foreach (var customerRole in newCustomerRoles)
                        {
                            //ensure that the current customer cannot add to "Administrators" system role if he's not an admin himself
                            if (customerRole.SystemName == SystemCustomerRoleNames.Administrators &&
                                !_baseService.WorkContext.CurrentCustomer.IsAdmin())
                                continue;

                            customer.CustomerRoles.Add(customerRole);
                        }
                        _customerService.UpdateCustomer(customer);


                        //ensure that a customer with a vendor associated is not in "Administrators" role
                        //otherwise, he won't have access to other functionality in admin area
                        if (customer.IsAdmin() && customer.VendorId > 0)
                        {
                            customer.VendorId = 0;
                            _customerService.UpdateCustomer(customer);
                            LogError(_localizationService.GetResource("Admin.Customers.Customers.AdminCouldNotbeVendor"));
                        }

                        //ensure that a customer in the Vendors role has a vendor account associated.
                        //otherwise, he will have access to ALL products
                        if (customer.IsVendor() && customer.VendorId == 0)
                        {
                            var vendorRole = customer
                                .CustomerRoles
                                .FirstOrDefault(x => x.SystemName == SystemCustomerRoleNames.Vendors);
                            customer.CustomerRoles.Remove(vendorRole);
                            _customerService.UpdateCustomer(customer);
                            LogError(_localizationService.GetResource("Admin.Customers.Customers.CannotBeInVendoRoleWithoutVendorAssociated"));
                        }

                        //activity log
                        _customerActivityService.InsertActivity("AddNewCustomer", _localizationService.GetResource("ActivityLog.AddNewCustomer"), customer.Id);

                        _baseService.Commit();
                        if (continueEditing)
                        {
                            string uri = Url.Link("GetCustomerById", new { id = customer.Id });
                            response.Headers.Location = new Uri(uri);
                        }
                        else
                        {
                            string uri = Url.Link("DefaultCustomerPageLoad", null);
                            response.Headers.Location = new Uri(uri);
                        }
                        return response;
                    }

                    //If we got this far, something failed, redisplay form
                    PrepareCustomerModel(model, null, true);

                    response = request.CreateResponse<CustomerVM>(HttpStatusCode.OK, model);
                }
                return response;

            });
        }

        [HttpPost]
        [Route("EditCustomer")]
        public HttpResponseMessage Edit(HttpRequestMessage request, CustomerVM model, bool continueEditing = false, System.Web.Mvc.FormCollection form = null)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageCustomers))
                {

                    var customer = _customerService.GetCustomerById(model.Id);
                    if (customer == null || customer.Deleted)
                    {
                        //No customer found with the specified id
                        string uri = Url.Link("DefaultCustomerPageLoad", null);
                        response.Headers.Location = new Uri(uri);
                        return response;
                    }

                    //validate customer roles
                    var allCustomerRoles = _customerService.GetAllCustomerRoles(true);
                    var newCustomerRoles = new List<CustomerRole>();
                    foreach (var customerRole in allCustomerRoles)
                        if (model.SelectedCustomerRoleIds.Contains(customerRole.Id))
                            newCustomerRoles.Add(customerRole);
                    var customerRolesError = ValidateCustomerRoles(newCustomerRoles);
                    if (!String.IsNullOrEmpty(customerRolesError))
                    {
                        ModelState.AddModelError("", customerRolesError);
                        LogError(customerRolesError);
                    }

                    // Ensure that valid email address is entered if Registered role is checked to avoid registered customers with empty email address
                    if (newCustomerRoles.Any() && newCustomerRoles.FirstOrDefault(c => c.SystemName == SystemCustomerRoleNames.Registered) != null && !CommonHelper.IsValidEmail(model.Email))
                    {
                        ModelState.AddModelError("", _localizationService.GetResource("Admin.Customers.Customers.ValidEmailRequiredRegisteredRole"));
                        LogError(_localizationService.GetResource("Admin.Customers.Customers.ValidEmailRequiredRegisteredRole"));
                    }

                    //custom customer attributes
                    var customerAttributesXml = form == null ? null : ParseCustomCustomerAttributes(form);
                    if (customerAttributesXml != null && newCustomerRoles.Any() && newCustomerRoles.FirstOrDefault(c => c.SystemName == SystemCustomerRoleNames.Registered) != null)
                    {
                        var customerAttributeWarnings = _customerAttributeParser.GetAttributeWarnings(customerAttributesXml);
                        foreach (var error in customerAttributeWarnings)
                        {
                            ModelState.AddModelError("", error);
                        }
                    }

                    if (ModelState.IsValid)
                    {
                        try
                        {
                            customer.AdminComment = model.AdminComment;
                            customer.IsTaxExempt = model.IsTaxExempt;

                            //prevent deactivation of the last active administrator
                            if (!customer.IsAdmin() || model.Active || SecondAdminAccountExists(customer))
                                customer.Active = model.Active;
                            else
                                LogError(_localizationService.GetResource("Admin.Customers.Customers.AdminAccountShouldExists.Deactivate"));

                            //email
                            if (!String.IsNullOrWhiteSpace(model.Email))
                            {
                                _customerRegistrationService.SetEmail(customer, model.Email, false);
                            }
                            else
                            {
                                customer.Email = model.Email;
                            }

                            //username
                            if (_customerSettings.UsernamesEnabled)
                            {
                                if (!String.IsNullOrWhiteSpace(model.Username))
                                {
                                    _customerRegistrationService.SetUsername(customer, model.Username);
                                }
                                else
                                {
                                    customer.Username = model.Username;
                                }
                            }

                            //VAT number
                            if (_taxSettings.EuVatEnabled)
                            {
                                var prevVatNumber = customer.GetAttribute<string>(SystemCustomerAttributeNames.VatNumber, _genericAttributeService);

                                _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.VatNumber, model.VatNumber);
                                //set VAT number status
                                if (!String.IsNullOrEmpty(model.VatNumber))
                                {
                                    if (!model.VatNumber.Equals(prevVatNumber, StringComparison.InvariantCultureIgnoreCase))
                                    {
                                        _genericAttributeService.SaveAttribute(customer,
                                            SystemCustomerAttributeNames.VatNumberStatusId,
                                            (int)_taxService.GetVatNumberStatus(model.VatNumber));
                                    }
                                }
                                else
                                {
                                    _genericAttributeService.SaveAttribute(customer,
                                        SystemCustomerAttributeNames.VatNumberStatusId,
                                        (int)VatNumberStatus.Empty);
                                }
                            }

                            //vendor
                            customer.VendorId = model.VendorId;

                            //form fields
                            if (_dateTimeSettings.AllowCustomersToSetTimeZone)
                                _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.TimeZoneId, model.TimeZoneId);
                            if (_customerSettings.GenderEnabled)
                                _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.Gender, model.Gender);
                            _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.FirstName, model.FirstName);
                            _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.LastName, model.LastName);
                            if (_customerSettings.DateOfBirthEnabled)
                                _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.DateOfBirth, model.DateOfBirth);
                            if (_customerSettings.CompanyEnabled)
                                _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.Company, model.Company);
                            if (_customerSettings.StreetAddressEnabled)
                                _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.StreetAddress, model.StreetAddress);
                            if (_customerSettings.StreetAddress2Enabled)
                                _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.StreetAddress2, model.StreetAddress2);
                            if (_customerSettings.ZipPostalCodeEnabled)
                                _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.ZipPostalCode, model.ZipPostalCode);
                            if (_customerSettings.CityEnabled)
                                _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.City, model.City);
                            if (_customerSettings.CountryEnabled)
                                _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.CountryId, model.CountryId);
                            if (_customerSettings.CountryEnabled && _customerSettings.StateProvinceEnabled)
                                _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.StateProvinceId, model.StateProvinceId);
                            if (_customerSettings.PhoneEnabled)
                                _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.Phone, model.Phone);
                            if (_customerSettings.FaxEnabled)
                                _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.Fax, model.Fax);

                            //custom customer attributes
                            if (customerAttributesXml != null)
                                _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.CustomCustomerAttributes, customerAttributesXml);

                            //newsletter subscriptions
                            //if (!String.IsNullOrEmpty(customer.Email))
                            //{
                            //    var allStores = _storeService.GetAllStores();
                            //    foreach (var store in allStores)
                            //    {
                            //        var newsletterSubscription = _newsLetterSubscriptionService
                            //            .GetNewsLetterSubscriptionByEmailAndStoreId(customer.Email, store.Id);
                            //        if (model.SelectedNewsletterSubscriptionStoreIds != null &&
                            //            model.SelectedNewsletterSubscriptionStoreIds.Contains(store.Id))
                            //        {
                            //            //subscribed
                            //            if (newsletterSubscription == null)
                            //            {
                            //                _newsLetterSubscriptionService.InsertNewsLetterSubscription(new NewsLetterSubscription
                            //                {
                            //                    NewsLetterSubscriptionGuid = Guid.NewGuid(),
                            //                    Email = customer.Email,
                            //                    Active = true,
                            //                    StoreId = store.Id,
                            //                    CreatedOnUtc = DateTime.UtcNow
                            //                });
                            //            }
                            //        }
                            //        else
                            //        {
                            //            //not subscribed
                            //            if (newsletterSubscription != null)
                            //            {
                            //                _newsLetterSubscriptionService.DeleteNewsLetterSubscription(newsletterSubscription);
                            //            }
                            //        }
                            //    }
                            //}


                            //customer roles
                            foreach (var customerRole in allCustomerRoles)
                            {
                                //ensure that the current customer cannot add/remove to/from "Administrators" system role
                                //if he's not an admin himself
                                if (customerRole.SystemName == SystemCustomerRoleNames.Administrators &&
                                    !_baseService.WorkContext.CurrentCustomer.IsAdmin())
                                    continue;

                                if (model.SelectedCustomerRoleIds.Contains(customerRole.Id))
                                {
                                    //new role
                                    if (customer.CustomerRoles.Count(cr => cr.Id == customerRole.Id) == 0)
                                        customer.CustomerRoles.Add(customerRole);
                                }
                                else
                                {
                                    //prevent attempts to delete the administrator role from the user, if the user is the last active administrator
                                    if (customerRole.SystemName == SystemCustomerRoleNames.Administrators && !SecondAdminAccountExists(customer))
                                    {
                                        LogError(_localizationService.GetResource("Admin.Customers.Customers.AdminAccountShouldExists.DeleteRole"));
                                        continue;
                                    }

                                    //remove role
                                    if (customer.CustomerRoles.Count(cr => cr.Id == customerRole.Id) > 0)
                                        customer.CustomerRoles.Remove(customerRole);
                                }
                            }
                            _customerService.UpdateCustomer(customer);


                            //ensure that a customer with a vendor associated is not in "Administrators" role
                            //otherwise, he won't have access to the other functionality in admin area
                            if (customer.IsAdmin() && customer.VendorId > 0)
                            {
                                customer.VendorId = 0;
                                _customerService.UpdateCustomer(customer);
                                LogError(_localizationService.GetResource("Admin.Customers.Customers.AdminCouldNotbeVendor"));
                            }

                            //ensure that a customer in the Vendors role has a vendor account associated.
                            //otherwise, he will have access to ALL products
                            if (customer.IsVendor() && customer.VendorId == 0)
                            {
                                var vendorRole = customer
                                    .CustomerRoles
                                    .FirstOrDefault(x => x.SystemName == SystemCustomerRoleNames.Vendors);
                                customer.CustomerRoles.Remove(vendorRole);
                                _customerService.UpdateCustomer(customer);
                                LogError(_localizationService.GetResource("Admin.Customers.Customers.CannotBeInVendoRoleWithoutVendorAssociated"));
                            }


                            //activity log
                            _customerActivityService.InsertActivity("EditCustomer", _localizationService.GetResource("ActivityLog.EditCustomer"), customer.Id);

                            _baseService.Commit();
                            if (continueEditing)
                            {
                                string uri = Url.Link("GetCustomerById", new { id = customer.Id });
                                response.Headers.Location = new Uri(uri);
                                return response;
                            }
                            else
                            {
                                //No customer found with the specified id
                                string uri = Url.Link("DefaultCustomerPageLoad", null);
                                response.Headers.Location = new Uri(uri);
                                return response;
                            }
                        }
                        catch (Exception exc)
                        {
                            LogError(exc);
                        }
                    }


                    //If we got this far, something failed, redisplay form
                    PrepareCustomerModel(model, customer, true);

                    response = request.CreateResponse<CustomerVM>(HttpStatusCode.OK, model);
                }
                return response;

            });


        }

        [HttpPost]
        [Route("DeleteCustomer")]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageCustomers))
                {

                    var customer = _customerService.GetCustomerById(id);
                    if (customer == null)
                    {
                        //No customer found with the specified id
                        string nUri = Url.Link("DefaultCustomerPageLoad", null);
                        response = request.CreateResponse(HttpStatusCode.OK, new { Success = false, RedirectUrl = nUri });
                        return response;
                    }

                    try
                    {
                        //prevent attempts to delete the user, if it is the last active administrator
                        if (customer.IsAdmin() && !SecondAdminAccountExists(customer))
                        {
                            LogError(_localizationService.GetResource("Admin.Customers.Customers.AdminAccountShouldExists.DeleteAdministrator"));
                            string newUri = Url.Link("GetCustomerById", new { id = customer.Id });
                            response.Headers.Location = new Uri(newUri);
                            //response = request.CreateResponse(HttpStatusCode.OK, new { Success = true, RedirectUrl = uri });
                            return response;
                        }

                        //ensure that the current customer cannot delete "Administrators" if he's not an admin himself
                        if (customer.IsAdmin() && !_baseService.WorkContext.CurrentCustomer.IsAdmin())
                        {
                            LogError(_localizationService.GetResource("Admin.Customers.Customers.OnlyAdminCanDeleteAdmin"));
                            string newUri = Url.Link("GetCustomerById", new { id = customer.Id });
                            response.Headers.Location = new Uri(newUri);
                            //response = request.CreateResponse(HttpStatusCode.OK, new { Success = true, RedirectUrl = uri });
                            return response;
                        }

                        //delete
                        _customerService.DeleteCustomer(customer);

                        ////remove newsletter subscription (if exists)
                        //foreach (var store in _storeService.GetAllStores())
                        //{
                        //    var subscription = _newsLetterSubscriptionService.GetNewsLetterSubscriptionByEmailAndStoreId(customer.Email, store.Id);
                        //    if (subscription != null)
                        //        _newsLetterSubscriptionService.DeleteNewsLetterSubscription(subscription);
                        //}

                        //activity log
                        _customerActivityService.InsertActivity("DeleteCustomer", _localizationService.GetResource("ActivityLog.DeleteCustomer"), customer.Id);

                        _baseService.Commit();
                        {
                            //No customer found with the specified id
                            string nUri = Url.Link("DefaultCustomerPageLoad", null);
                            response = request.CreateResponse(HttpStatusCode.OK, new { Success = false, RedirectUrl = nUri });
                            return response;
                        }
                    }
                    catch (Exception exc)
                    {
                        LogError(exc);
                        _baseService.Commit();
                        string newUri = Url.Link("GetCustomerById", new { id = customer.Id });
                        response.Headers.Location = new Uri(newUri);
                        //response = request.CreateResponse(HttpStatusCode.OK, new { Success = true, RedirectUrl = uri });
                        return response;
                    }
                }
                return response;

            });
        }

        [HttpPost]
        [Route("ChangePassword", Name = "ChangePassword")]
        public HttpResponseMessage ChangePassword(HttpRequestMessage request, CustomerVM model)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageCustomers))
                {
                    var customer = _customerService.GetCustomerById(model.Id);
                    if (customer == null)
                    {
                        //No customer found with the specified id
                        string newUri = Url.Link("DefaultCustomerPageLoad", null);
                        response.Headers.Location = new Uri(newUri);
                        return response;
                    }

                    //ensure that the current customer cannot change passwords of "Administrators" if he's not an admin himself
                    if (customer.IsAdmin() && !_baseService.WorkContext.CurrentCustomer.IsAdmin())
                    {
                        LogError(_localizationService.GetResource("Admin.Customers.Customers.OnlyAdminCanChangePassword"));
                        string newUri = Url.Link("GetCustomerById", new { id = customer.Id });
                        response.Headers.Location = new Uri(newUri);
                        //response = request.CreateResponse(HttpStatusCode.OK, new { Success = true, RedirectUrl = uri });
                        return response;
                    }

                    if (ModelState.IsValid)
                    {
                        var changePassRequest = new ChangePasswordRequest(model.Email, false, _customerSettings.DefaultPasswordFormat, model.Password);
                        var changePassResult = _customerRegistrationService.ChangePassword(changePassRequest);
                        if (!changePassResult.Success)
                        {
                            foreach (var error in changePassResult.Errors)
                                LogError(error);
                        }
                        _baseService.Commit();
                    }
                    string uri = Url.Link("GetCustomerById", new { id = customer.Id });
                    response = request.CreateResponse(HttpStatusCode.OK);
                    response.Headers.Location = new Uri(uri);
                    return response;
                }
                return response;

            });
        }

        [HttpPost]
        [Route("MarkVatNumberAsValid", Name = "MarkVatNumberAsValid")]
        public HttpResponseMessage MarkVatNumberAsValid(HttpRequestMessage request, CustomerVM model)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageCustomers))
                {
                    var customer = _customerService.GetCustomerById(model.Id);
                    if (customer == null)
                    {
                        //No customer found with the specified id
                        string nUri = Url.Link("DefaultCustomerPageLoad", null);
                        response = request.CreateResponse(HttpStatusCode.OK, new { Success = false, RedirectUrl = nUri });
                        return response;
                    }

                    _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.VatNumberStatusId, (int)VatNumberStatus.Valid);

                    _baseService.Commit();
                    string newUri = Url.Link("GetCustomerById", new { id = customer.Id });
                    response.Headers.Location = new Uri(newUri);
                    //response = request.CreateResponse(HttpStatusCode.OK, new { Success = true, RedirectUrl = uri });
                    return response;
                }
                return response;

            });
        }

        [HttpPost]
        [Route("RemoveAffiliate", Name = "RemoveAffiliate")]
        public HttpResponseMessage RemoveAffiliate(HttpRequestMessage request, CustomerVM model)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageCustomers))
                {

                    var customer = _customerService.GetCustomerById(model.Id);
                    if (customer == null)
                    {
                        //No customer found with the specified id
                        string nUri = Url.Link("DefaultCustomerPageLoad", null);
                        response = request.CreateResponse(HttpStatusCode.OK, new { Success = false, RedirectUrl = nUri });
                        return response;
                    }

                    customer.AffiliateId = 0;
                    _customerService.UpdateCustomer(customer);

                    _baseService.Commit();
                    string newUri = Url.Link("GetCustomerById", new { id = customer.Id });
                    response.Headers.Location = new Uri(newUri);
                    //response = request.CreateResponse(HttpStatusCode.OK, new { Success = true, RedirectUrl = uri });
                    return response;
                }
                return response;

            });


        }

        [HttpPost]
        [Route("Impersonate", Name = "Impersonate")]
        public HttpResponseMessage Impersonate(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageCustomers))
                {

                    var customer = _customerService.GetCustomerById(id);
                    if (customer == null)
                    {
                        //No customer found with the specified id
                        string nUri = Url.Link("DefaultCustomerPageLoad", null);
                        response = request.CreateResponse(HttpStatusCode.OK, new { Success = false, RedirectUrl = nUri });
                        return response;
                    }

                    //ensure that a non-admin user cannot impersonate as an administrator
                    //otherwise, that user can simply impersonate as an administrator and gain additional administrative privileges
                    if (!_baseService.WorkContext.CurrentCustomer.IsAdmin() && customer.IsAdmin())
                    {
                        LogError(_localizationService.GetResource("Admin.Customers.Customers.NonAdminNotImpersonateAsAdminError"));
                        string newUri = Url.Link("GetCustomerById", new { id = customer.Id });
                        response.Headers.Location = new Uri(newUri);
                        //response = request.CreateResponse(HttpStatusCode.OK, new { Success = true, RedirectUrl = uri });
                        return response;
                    }

                    //activity log
                    _customerActivityService.InsertActivity("Impersonation.Started", _localizationService.GetResource("ActivityLog.Impersonation.Started.StoreOwner"), customer.Email, customer.Id);
                    _customerActivityService.InsertActivity(customer, "Impersonation.Started", _localizationService.GetResource("ActivityLog.Impersonation.Started.Customer"), _baseService.WorkContext.CurrentCustomer.Email, _baseService.WorkContext.CurrentCustomer.Id);

                    //ensure login is not required
                    customer.RequireReLogin = false;
                    _customerService.UpdateCustomer(customer);
                    _genericAttributeService.SaveAttribute<int?>(_baseService.WorkContext.CurrentCustomer, SystemCustomerAttributeNames.ImpersonatedCustomerId, customer.Id);

                    _baseService.Commit();

                    var newUrl = this.Url.Link("Default", new { Controller = "Home", Action = "Index", Area = "" });
                    response = request.CreateResponse(HttpStatusCode.OK, new { Success = true, RedirectUrl = newUrl });
                    return response;
                }
                return response;

            });
        }

        [HttpPost]
        [Route("SendWelcomeMessage", Name = "SendWelcomeMessage")]
        public HttpResponseMessage SendWelcomeMessage(HttpRequestMessage request, CustomerVM model)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageCustomers))
                {

                    var customer = _customerService.GetCustomerById(model.Id);
                    if (customer == null)
                    {
                        //No customer found with the specified id
                        string nUri = Url.Link("DefaultCustomerPageLoad", null);
                        response = request.CreateResponse(HttpStatusCode.OK, new { Success = false, RedirectUrl = nUri });
                        return response;
                    }

                    //_workflowMessageService.SendCustomerWelcomeMessage(customer, _baseService.WorkContext.WorkingLanguage.Id);

                    //SuccessNotification(_localizationService.GetResource("Admin.Customers.Customers.SendWelcomeMessage.Success"));

                    string newUri = Url.Link("GetCustomerById", new { id = customer.Id });
                    response.Headers.Location = new Uri(newUri);
                    //response = request.CreateResponse(HttpStatusCode.OK, new { Success = true, RedirectUrl = uri });
                    return response;
                }
                return response;

            });


        }

        [HttpPost]
        [Route("ReSendActivationMessage", Name = "ReSendActivationMessage")]
        public HttpResponseMessage ReSendActivationMessage(HttpRequestMessage request, CustomerVM model)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageCustomers))
                {

                    var customer = _customerService.GetCustomerById(model.Id);
                    if (customer == null)
                    {
                        //No customer found with the specified id
                        string nUri = Url.Link("DefaultCustomerPageLoad", null);
                        response = request.CreateResponse(HttpStatusCode.OK, new { Success = false, RedirectUrl = nUri });
                        return response;
                    }

                    //email validation message
                    _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.AccountActivationToken, Guid.NewGuid().ToString());
                    //_workflowMessageService.SendCustomerEmailValidationMessage(customer, _baseService.WorkContext.WorkingLanguage.Id);

                    //SuccessNotification(_localizationService.GetResource("Admin.Customers.Customers.ReSendActivationMessage.Success"));

                    string newUri = Url.Link("GetCustomerById", new { id = customer.Id });
                    response.Headers.Location = new Uri(newUri);
                    //response = request.CreateResponse(HttpStatusCode.OK, new { Success = true, RedirectUrl = uri });
                    return response;
                }
                return response;

            });
        }

        [HttpPost]
        [Route("SendEmail", Name = "SendEmail")]
        public HttpResponseMessage SendEmail(HttpRequestMessage request, CustomerVM model)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageCustomers))
                {

                    var customer = _customerService.GetCustomerById(model.Id);
                    if (customer == null)
                    {
                        //No customer found with the specified id
                        string nUri = Url.Link("DefaultCustomerPageLoad", null);
                        response = request.CreateResponse(HttpStatusCode.OK, new { Success = false, RedirectUrl = nUri });
                        return response;
                    }

                    try
                    {
                        if (String.IsNullOrWhiteSpace(customer.Email))
                            throw new DreamSaleException("Customer email is empty");
                        if (!CommonHelper.IsValidEmail(customer.Email))
                            throw new DreamSaleException("Customer email is not valid");
                        if (String.IsNullOrWhiteSpace(model.SendEmail.Subject))
                            throw new DreamSaleException("Email subject is empty");
                        if (String.IsNullOrWhiteSpace(model.SendEmail.Body))
                            throw new DreamSaleException("Email body is empty");

                        var emailAccount = _emailAccountService.GetEmailAccountById(_emailAccountSettings.DefaultEmailAccountId);
                        if (emailAccount == null)
                            emailAccount = _emailAccountService.GetAllEmailAccounts().FirstOrDefault();
                        if (emailAccount == null)
                            throw new DreamSaleException("Email account can't be loaded");
                        var email = new QueuedEmail
                        {
                            Priority = QueuedEmailPriority.High,
                            EmailAccountId = emailAccount.Id,
                            FromName = emailAccount.DisplayName,
                            From = emailAccount.Email,
                            ToName = customer.GetFullName(_genericAttributeService),
                            To = customer.Email,
                            Subject = model.SendEmail.Subject,
                            Body = model.SendEmail.Body,
                            CreatedOnUtc = DateTime.UtcNow,
                            DontSendBeforeDateUtc = (model.SendEmail.SendImmediately || !model.SendEmail.DontSendBeforeDate.HasValue) ?
                                null : (DateTime?)_dateTimeHelper.ConvertToUtcTime(model.SendEmail.DontSendBeforeDate.Value)
                        };
                        _queuedEmailService.InsertQueuedEmail(email);
                        _baseService.Commit();
                        //SuccessNotification(_localizationService.GetResource("Admin.Customers.Customers.SendEmail.Queued"));
                    }
                    catch (Exception exc)
                    {
                        LogError(exc);
                        _baseService.Commit();
                    }

                    string newUri = Url.Link("GetCustomerById", new { id = customer.Id });
                    response.Headers.Location = new Uri(newUri);
                    //response = request.CreateResponse(HttpStatusCode.OK, new { Success = true, RedirectUrl = uri });
                    return response;
                }
                return response;

            });
        }

        [HttpPost]
        [Route("SendPm", Name = "SendPm")]
        public HttpResponseMessage SendPm(HttpRequestMessage request, CustomerVM model)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageCustomers))
                {

                    var customer = _customerService.GetCustomerById(model.Id);
                    if (customer == null)
                    {
                        //No customer found with the specified id
                        string nUri = Url.Link("DefaultCustomerPageLoad", null);
                        response = request.CreateResponse(HttpStatusCode.OK, new { Success = false, RedirectUrl = nUri });
                        return response;
                    }

                    try
                    {
                        if (!_forumSettings.AllowPrivateMessages)
                            throw new DreamSaleException("Private messages are disabled");
                        if (customer.IsGuest())
                            throw new DreamSaleException("Customer should be registered");
                        if (String.IsNullOrWhiteSpace(model.SendPm.Subject))
                            throw new DreamSaleException("PM subject is empty");
                        if (String.IsNullOrWhiteSpace(model.SendPm.Message))
                            throw new DreamSaleException("PM message is empty");


                        var privateMessage = new PrivateMessage
                        {
                            StoreId = _storeContext.CurrentStore.Id,
                            ToCustomerId = customer.Id,
                            FromCustomerId = _baseService.WorkContext.CurrentCustomer.Id,
                            Subject = model.SendPm.Subject,
                            Text = model.SendPm.Message,
                            IsDeletedByAuthor = false,
                            IsDeletedByRecipient = false,
                            IsRead = false,
                            CreatedOnUtc = DateTime.UtcNow
                        };

                        //_forumService.InsertPrivateMessage(privateMessage);
                        //SuccessNotification(_localizationService.GetResource("Admin.Customers.Customers.SendPM.Sent"));
                        _baseService.Commit();
                    }
                    catch (Exception exc)
                    {
                        LogError(exc);
                        _baseService.Commit();
                    }

                    string newUri = Url.Link("GetCustomerById", new { id = customer.Id });
                    response.Headers.Location = new Uri(newUri);
                    //response = request.CreateResponse(HttpStatusCode.OK, new { Success = true, RedirectUrl = uri });
                    return response;
                }
                return response;

            });


        }

        #endregion

        #region Customer roles

        [HttpGet]
        [Route("Roles/{pageIndex:int=0}/{pageSize:int=2147483640}", Name = "CustomerRoles")]
        public HttpResponseMessage GetCustomerRoles(HttpRequestMessage request, int pageIdex = 0, int pageSize = int.MaxValue)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageCustomers))
                {
                    var customerRoles = _customerService.GetAllCustomerRoles(true);
                    var gridModel = new DataSourceResult
                    {
                        Data = customerRoles.Select(PrepareCustomerRoleModel),
                        Total = customerRoles.Count()
                    };
                    response = request.CreateResponse<DataSourceResult>(HttpStatusCode.OK, gridModel);
                }
                return response;

            });
        }

        [HttpGet]
        [Route("GetCustomerRoleById/{id:int}", Name = "GetCustomerRoleById")]
        public HttpResponseMessage GetCustomerRoleById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageCustomers))
                {
                    var customerRole = _customerService.GetCustomerRoleById(id);
                    if (customerRole == null)
                    {
                        //No customer found with the specified id
                        string newUri = Url.Link("CustomerRoles", null);
                        response = request.CreateResponse(HttpStatusCode.OK, new { Success = true, RedirectUrl = newUri });
                        response.Headers.Location = new Uri(newUri);
                        return response;
                    }

                    var model = PrepareCustomerRoleModel(customerRole);
                    response = request.CreateResponse<CustomerRoleVM>(HttpStatusCode.OK, model);
                }
                return response;

            });
        }

        [HttpGet]
        [Route("CreateRoleModal")]
        public HttpResponseMessage GetCreateRole(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageCustomers))
                {
                    var model = new CustomerRoleVM();
                    //default values
                    model.Active = true;

                    response = request.CreateResponse<CustomerRoleVM>(HttpStatusCode.OK, model);
                }
                return response;

            });
        }

        [HttpPost]
        [Route("AddCustomerRole")]
        public HttpResponseMessage AddCustomerRole(HttpRequestMessage request, CustomerRoleVM model, bool continueEditing = false)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageCustomers))
                {
                    if (ModelState.IsValid)
                    {
                        var customerRole = model.ToEntity();
                        _customerService.InsertCustomerRole(customerRole);

                        //activity log
                        _customerActivityService.InsertActivity("AddNewCustomerRole", _localizationService.GetResource("ActivityLog.AddNewCustomerRole"), customerRole.Name);

                        _baseService.Commit();
                        //SuccessNotification(_localizationService.GetResource("Admin.Customers.CustomerRoles.Added"));
                        if (continueEditing)
                        {
                            // Generate a link to the update item and set the Location header in the response.
                            string uri = Url.Link("GetCustomerRoleById", new { id = customerRole.Id });
                            response.Headers.Location = new Uri(uri);
                        }
                        else
                        {
                            string uri = Url.Link("CustomerRoles", null);
                            response.Headers.Location = new Uri(uri);
                        }
                        return response;
                    }
                }
                return response;

            });
        }

        [HttpPost]
        [Route("EditCustomerRole")]
        public HttpResponseMessage EditCustomerRole(HttpRequestMessage request, CustomerRoleVM model, bool continueEditing = false)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageCustomers))
                {

                    var customerRole = _customerService.GetCustomerRoleById(model.Id);
                    if (customerRole == null)
                    {
                        //No customer role found with the specified id
                        string uri = Url.Link("CustomerRoles", null);
                        response.Headers.Location = new Uri(uri);
                        return response;
                    }

                    try
                    {
                        if (ModelState.IsValid)
                        {
                            if (customerRole.IsSystemRole && !model.Active)
                                throw new DreamSaleException(_localizationService.GetResource("Admin.Customers.CustomerRoles.Fields.Active.CantEditSystem"));

                            if (customerRole.IsSystemRole && !customerRole.SystemName.Equals(model.SystemName, StringComparison.InvariantCultureIgnoreCase))
                                throw new DreamSaleException(_localizationService.GetResource("Admin.Customers.CustomerRoles.Fields.SystemName.CantEditSystem"));

                            if (SystemCustomerRoleNames.Registered.Equals(customerRole.SystemName, StringComparison.InvariantCultureIgnoreCase) &&
                                model.PurchasedWithProductId > 0)
                                throw new DreamSaleException(_localizationService.GetResource("Admin.Customers.CustomerRoles.Fields.PurchasedWithProduct.Registered"));

                            customerRole = model.ToEntity(customerRole);
                            _customerService.UpdateCustomerRole(customerRole);

                            //activity log
                            _customerActivityService.InsertActivity("EditCustomerRole", _localizationService.GetResource("ActivityLog.EditCustomerRole"), customerRole.Name);

                            _baseService.Commit();
                            //SuccessNotification(_localizationService.GetResource("Admin.Customers.CustomerRoles.Updated"));
                            if (continueEditing)
                            {
                                // Generate a link to the update item and set the Location header in the response.
                                string uri = Url.Link("GetCustomerRoleById", new { id = customerRole.Id });
                                response.Headers.Location = new Uri(uri);
                                return response;
                            }
                            else
                            {
                                string uri = Url.Link("CustomerRoles", null);
                                response.Headers.Location = new Uri(uri);
                            }
                            return response;
                        }

                        //If we got this far, something failed, redisplay form
                        response = request.CreateResponse<CustomerRoleVM>(HttpStatusCode.OK, model);
                        return response;
                    }
                    catch (Exception exc)
                    {
                        LogError(exc);
                        _baseService.Commit();
                        string uri = Url.Link("GetCustomerRoleById", new { id = customerRole.Id });
                        response.Headers.Location = new Uri(uri);
                        return response;
                    }
                }
                return response;

            });
        }

        [HttpPost]
        [Route("DeleteCustomerRole")]
        public HttpResponseMessage DeleteCustomerRole(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.NotFound);
                if (_permissionService.Authorize(StandardPermissionProvider.ManageCustomers))
                {
                    if (!ModelState.IsValid)
                    {
                        response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                    }
                    else
                    {
                        var customerRole = _customerService.GetCustomerRoleById(id);
                        try
                        {
                            if (customerRole != null)
                            {
                                _customerService.DeleteCustomerRole(customerRole);
                                //activity log
                                _customerActivityService.InsertActivity("DeleteCustomerRole", _localizationService.GetResource("ActivityLog.DeleteCustomerRole"), customerRole.Name);

                                _baseService.Commit();
                            }
                            string uri = Url.Link("CustomerRoles", null);
                            response.Headers.Location = new Uri(uri);
                        }
                        catch (Exception exc)
                        {
                            LogError(exc);
                            _baseService.Commit();
                            string uri = Url.Link("GetCustomerRoleById", new { id = customerRole.Id });
                            response.Headers.Location = new Uri(uri);
                        }
                        return response;
                    }
                }

                return response;
            });
        }

        [HttpGet]
        [Route("AssociateProductToCustomerRoleModal", Name = "AssociateProductToCustomerRoleModal")]
        public HttpResponseMessage AssociateProductToCustomerRolePopup(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageCustomers))
                {
                    var model = new CustomerRoleVM.AssociateProductToCustomerRoleVM();
                    //a vendor should have access only to his products
                    model.IsLoggedInAsVendor = _baseService.WorkContext.CurrentVendor != null;
                    string allText = _localizationService.GetResource("Admin.Common.All");

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

                    //product types
                    model.AvailableProductTypes = ProductType.SimpleProduct.ToSelectList(_localizationService, _baseService.WorkContext, false).ToList();
                    model.AvailableProductTypes.Insert(0, new System.Web.Mvc.SelectListItem { Text = allText, Value = "0" });

                    response = request.CreateResponse<CustomerRoleVM.AssociateProductToCustomerRoleVM>(HttpStatusCode.OK, model);
                }
                return response;

            });


        }

        [HttpPost]
        [Route("AssociateProductToCustomerRolePopupList", Name = "AssociateProductToCustomerRolePopupList")]
        public HttpResponseMessage AssociateProductToCustomerRolePopupList(HttpRequestMessage request, CustomerRoleVM.AssociateProductToCustomerRoleVM model, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageCustomers))
                {
                    //a vendor should have access only to his products
                    if (_baseService.WorkContext.CurrentVendor != null)
                    {
                        model.SearchVendorId = _baseService.WorkContext.CurrentVendor.Id;
                    }

                    var products = _productService.SearchProducts(
                        categoryIds: new List<int> { model.SearchCategoryId },
                        manufacturerId: model.SearchManufacturerId,
                        storeId: model.SearchStoreId,
                        vendorId: model.SearchVendorId,
                        productType: model.SearchProductTypeId > 0 ? (ProductType?)model.SearchProductTypeId : null,
                        keywords: model.SearchProductName,
                        pageIndex: pageIndex,
                        pageSize: pageSize,
                        showHidden: true
                        );
                    var gridModel = new DataSourceResult();
                    gridModel.Data = products.Select(x => x.ToModel());
                    gridModel.Total = products.TotalCount;

                    response = request.CreateResponse<DataSourceResult>(HttpStatusCode.OK, gridModel);
                }
                return response;

            });
        }

        [HttpPost]
        [Route("AssociateProductToCustomerRolePopup", Name = "AssociateProductToCustomerRolePopup")]
        public HttpResponseMessage AssociateProductToCustomerRolePopup(HttpRequestMessage request, string productNameInput, CustomerRoleVM.AssociateProductToCustomerRoleVM model)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageCustomers))
                {
                    var associatedProduct = _productService.GetProductById(model.AssociatedToProductId);
                    if (associatedProduct == null)
                    {
                        response = request.CreateErrorResponse(HttpStatusCode.NotFound, "Cannot load a product");
                        return response;
                    }

                    //a vendor should have access only to his products
                    if (_baseService.WorkContext.CurrentVendor != null && associatedProduct.VendorId != _baseService.WorkContext.CurrentVendor.Id)
                    {
                        response = request.CreateErrorResponse(HttpStatusCode.NotFound, "This is not your product");
                        return response;
                    }

                    //a vendor should have access only to his products
                    model.IsLoggedInAsVendor = _baseService.WorkContext.CurrentVendor != null;

                    response = request.CreateResponse<CustomerRoleVM.AssociateProductToCustomerRoleVM>(HttpStatusCode.OK, model);
                }
                return response;

            });


        }
        #endregion

        //[HttpGet]
        //[Route("All")]
        //public HttpResponseMessage GetAllCustomers(HttpRequestMessage request, int[] customerRoleIds, DateTime? createdFromUtc = null,
        //    DateTime? createdToUtc = null, int affiliateId = 0, int vendorId = 0,
        //    string email = null, string username = null,
        //    string firstName = null, string lastName = null,
        //    int dayOfBirth = 0, int monthOfBirth = 0,
        //    string company = null, string phone = null, string zipPostalCode = null,
        //    string ipAddress = null, bool loadOnlyWithShoppingCart = false, ShoppingCartType? sct = null,
        //    int pageIndex = 0, int pageSize = int.MaxValue)
        //{
        //    return CreateHttpResponse(request, () =>
        //    {
        //        HttpResponseMessage response = null;
        //        var customers = _customerService.GetAllCustomers(createdFromUtc, createdToUtc, affiliateId, vendorId, customerRoleIds, email, username, firstName, lastName, dayOfBirth, monthOfBirth, company, phone, zipPostalCode, ipAddress, loadOnlyWithShoppingCart, sct, pageIndex, pageSize);

        //        response = request.CreateResponse<List<Customer>>(HttpStatusCode.OK, customers.ToList());

        //        return response;
        //    });
        //}

        //[Route("GetById/{customerId}")]
        //public HttpResponseMessage GetCustomerById(HttpRequestMessage request, int customerId)
        //{
        //    if (customerId == 0)
        //    {
        //        return null;
        //    }

        //    return CreateHttpResponse(request, () =>
        //    {
        //        HttpResponseMessage response = null;
        //        var customer = _customerService.GetCustomerById(customerId);

        //        response = request.CreateResponse<Customer>(HttpStatusCode.OK, customer);

        //        return response;
        //    });
        //}
        #endregion
    }
}
