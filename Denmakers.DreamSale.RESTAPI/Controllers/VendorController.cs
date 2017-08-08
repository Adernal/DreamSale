using Denmakers.DreamSale.Common;
using Denmakers.DreamSale.Data.Context;
using Denmakers.DreamSale.Data.Infrastructure;
using Denmakers.DreamSale.Data.Repositories;
using Denmakers.DreamSale.Helpers;
using Denmakers.DreamSale.Model.Directory;
using Denmakers.DreamSale.Model.Logging;
using Denmakers.DreamSale.Model.Seo;
using Denmakers.DreamSale.Model.Vendors;
using Denmakers.DreamSale.RESTAPI.Infrastructure;
using Denmakers.DreamSale.Services;
using Denmakers.DreamSale.Services.Addresses;
using Denmakers.DreamSale.Services.Configuration;
using Denmakers.DreamSale.Services.Customers;
using Denmakers.DreamSale.Services.Directory;
using Denmakers.DreamSale.Services.Helpers;
using Denmakers.DreamSale.Services.Localization;
using Denmakers.DreamSale.Services.Logging;
using Denmakers.DreamSale.Services.Media;
using Denmakers.DreamSale.Services.Security;
using Denmakers.DreamSale.Services.Seo;
using Denmakers.DreamSale.Services.Vendors;
using Denmakers.DreamSale.ViewModels.AdminVM.Vendors;
using Denmakers.DreamSale.ViewModels.Mapper;
using Denmakers.DreamSale.ViewModels.Validators.Vendors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Denmakers.DreamSale.RESTAPI.Controllers
{
    [RoutePrefix("api/Vendor")]
    public class VendorController : ApiControllerBase
    {
        #region Fields

        private readonly ICustomerService _customerService;
        private readonly ILocalizationService _localizationService;
        private readonly IVendorService _vendorService;
        private readonly IPermissionService _permissionService;
        private readonly IUrlRecordService _urlRecordService;
        private readonly ILanguageService _languageService;
        private readonly ILocalizedEntityService _localizedEntityService;
        private readonly IPictureService _pictureService;
        private readonly IDateTimeHelper _dateTimeHelper;
        private readonly ICustomerActivityService _customerActivityService;
        private readonly IAddressService _addressService;
        private readonly ICountryService _countryService;
        private readonly IStateProvinceService _stateProvinceService;

        private readonly ISettingService _settingService;
        private readonly VendorSettings _vendorSettings;
        private readonly SeoSettings _seoSettings;

        #endregion

        #region Constructors

        public VendorController(IBaseService baseService, ILogger logger, IWebHelper webHelper,
            ICustomerService customerService,
            ILocalizationService localizationService,
            IVendorService vendorService,
            IPermissionService permissionService,
            IUrlRecordService urlRecordService,
            ILanguageService languageService,
            ILocalizedEntityService localizedEntityService,
            IPictureService pictureService,
            IDateTimeHelper dateTimeHelper,
            ICustomerActivityService customerActivityService,
            IAddressService addressService,
            ICountryService countryService,
            IStateProvinceService stateProvinceService,
            ISettingService settingService)
            : base(baseService, logger, webHelper)
        {
            this._customerService = customerService;
            this._localizationService = localizationService;
            this._vendorService = vendorService;
            this._permissionService = permissionService;
            this._urlRecordService = urlRecordService;
            this._languageService = languageService;
            this._localizedEntityService = localizedEntityService;
            this._pictureService = pictureService;
            this._dateTimeHelper = dateTimeHelper;
            this._customerActivityService = customerActivityService;
            this._addressService = addressService;
            this._countryService = countryService;
            this._stateProvinceService = stateProvinceService;
            this._settingService = settingService;
            this._vendorSettings = _settingService.LoadSetting<VendorSettings>();
            this._seoSettings = _settingService.LoadSetting<SeoSettings>();
        }

        #endregion

        #region Utilities

        [NonAction]
        protected virtual void UpdatePictureSeoNames(Vendor vendor)
        {
            var picture = _pictureService.GetPictureById(vendor.PictureId);
            if (picture != null)
                _pictureService.SetSeoFilename(picture.Id, _pictureService.GetPictureSeName(vendor.Name));
        }

        [NonAction]
        protected virtual void UpdateLocales(Vendor vendor, VendorVM model)
        {
            foreach (var localized in model.Locales)
            {
                _localizedEntityService.SaveLocalizedValue(vendor,
                                                               x => x.Name,
                                                               localized.Name,
                                                               localized.LanguageId);

                _localizedEntityService.SaveLocalizedValue(vendor,
                                                           x => x.Description,
                                                           localized.Description,
                                                           localized.LanguageId);

                _localizedEntityService.SaveLocalizedValue(vendor,
                                                           x => x.MetaKeywords,
                                                           localized.MetaKeywords,
                                                           localized.LanguageId);

                _localizedEntityService.SaveLocalizedValue(vendor,
                                                           x => x.MetaDescription,
                                                           localized.MetaDescription,
                                                           localized.LanguageId);

                _localizedEntityService.SaveLocalizedValue(vendor,
                                                           x => x.MetaTitle,
                                                           localized.MetaTitle,
                                                           localized.LanguageId);

                //search engine name
                //var seName = vendor.ValidateSeName(localized.SeName, localized.Name, false, _urlRecordService, _seoSettings);
                //_urlRecordService.SaveSlug(vendor, seName, localized.LanguageId);
            }
        }

        [NonAction]
        protected virtual void PrepareVendorModel(VendorVM model, Vendor vendor, bool excludeProperties, bool prepareEntireAddressModel)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            var address = _addressService.GetAddressById(vendor != null ? vendor.AddressId : 0);

            if (vendor != null)
            {
                if (!excludeProperties)
                {
                    if (address != null)
                    {
                        model.Address = address.ToModel();
                    }
                }

                //associated customer emails
                model.AssociatedCustomers = _customerService
                    .GetAllCustomers(vendorId: vendor.Id)
                    .Select(c => new VendorVM.AssociatedCustomerInfo()
                    {
                        Id = c.Id,
                        Email = c.Email
                    })
                    .ToList();
            }

            if (prepareEntireAddressModel)
            {
                model.Address.CountryEnabled = true;
                model.Address.StateProvinceEnabled = true;
                model.Address.CityEnabled = true;
                model.Address.StreetAddressEnabled = true;
                model.Address.StreetAddress2Enabled = true;
                model.Address.ZipPostalCodeEnabled = true;
                model.Address.PhoneEnabled = true;
                model.Address.FaxEnabled = true;

                //address
                model.Address.AvailableCountries.Add(new System.Web.Mvc.SelectListItem { Text = _localizationService.GetResource("Admin.Address.SelectCountry"), Value = "0" });
                foreach (var c in _countryService.GetAllCountries(showHidden: true))
                    model.Address.AvailableCountries.Add(new System.Web.Mvc.SelectListItem { Text = c.Name, Value = c.Id.ToString(), Selected = (address != null && c.Id == address.CountryId) });

                var states = model.Address.CountryId.HasValue ? _stateProvinceService.GetStateProvincesByCountryId(model.Address.CountryId.Value, showHidden: true).ToList() : new List<StateProvince>();
                if (states.Any())
                {
                    foreach (var s in states)
                        model.Address.AvailableStates.Add(new System.Web.Mvc.SelectListItem { Text = s.Name, Value = s.Id.ToString(), Selected = (address != null && s.Id == address.StateProvinceId) });
                }
                else
                    model.Address.AvailableStates.Add(new System.Web.Mvc.SelectListItem { Text = _localizationService.GetResource("Admin.Address.OtherNonUS"), Value = "0" });
            }
        }

        #endregion

        #region Vendors
        [HttpGet]
        [Route("EmptyVendorList", Name = "EmptyVendorList")]
        public HttpResponseMessage EmptyVendorList(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (true)
                {
                    var model = new VendorListVM();
                    response = request.CreateResponse<VendorListVM>(HttpStatusCode.OK, model);
                }
                return response;

            });
        }

        [HttpGet]
        [Route("", Name = "VendorList")]
        public HttpResponseMessage GetAllVendors(HttpRequestMessage request, string name = "", int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (true)
                {
                    var vendors = _vendorService.GetAllVendors(name, pageIndex, pageSize, true);
                    var gridModel = new DataSourceResult
                    {
                        Data = vendors.Select(x =>
                        {
                            var vendorModel = x.ToModel();
                            PrepareVendorModel(vendorModel, x, false, false);
                            return vendorModel;
                        }),
                        Total = vendors.TotalCount,
                    };
                    response = request.CreateResponse<DataSourceResult>(HttpStatusCode.OK, gridModel);
                }
                return response;

            });
        }

        [HttpGet]
        [Route("GetVendorById/{id:int}", Name = "GetVendorById")]
        public HttpResponseMessage GetVendorById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (true)
                {
                    var vendor = _vendorService.GetVendorById(id);
                    if (vendor == null || vendor.Deleted)
                    {
                        //No vendor found with the specified id
                        string newUri = Url.Link("VendorList", null);
                        response = request.CreateResponse(HttpStatusCode.OK, new { Success = true, RedirectUrl = newUri });
                        response.Headers.Location = new Uri(newUri);
                        return response;
                    }

                    var model = vendor.ToModel();
                    PrepareVendorModel(model, vendor, false, true);

                    response = request.CreateResponse<VendorVM>(HttpStatusCode.OK, model);
                }
                return response;

            });
        }

        [HttpGet]
        [Route("GetCreateVendorModel")]
        public HttpResponseMessage Create(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (true)
                {
                    var model = new VendorVM();
                    PrepareVendorModel(model, null, false, true);
                    
                    //default values
                    model.PageSize = 6;
                    model.Active = true;
                    model.AllowCustomersToSelectPageSize = true;
                    model.PageSizeOptions = _vendorSettings.DefaultVendorPageSizeOptions;

                    //default value
                    model.Active = true;

                    response = request.CreateResponse<VendorVM>(HttpStatusCode.OK, model);
                }
                return response;

            });
        }

        [HttpPost]
        [Route("CreateVendor", Name = "CreateVendor")]
        public HttpResponseMessage Create(HttpRequestMessage request, VendorVM model, bool continueEditing = false)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.NotFound, "No items found");
                if (true)
                {

                    if (ModelState.IsValid)
                    {
                        var vendor = model.ToEntity();
                        _vendorService.InsertVendor(vendor);

                        //activity log
                        _customerActivityService.InsertActivity("AddNewVendor", _localizationService.GetResource("ActivityLog.AddNewVendor"), vendor.Id);

                        //search engine name
                        //model.SeName = vendor.ValidateSeName(model.SeName, vendor.Name, true, _urlRecordService, _seoSettings);
                        //_urlRecordService.SaveSlug(vendor, model.SeName, 0);

                        //address
                        var address = model.Address.ToEntity();
                        address.CreatedOnUtc = DateTime.UtcNow;
                        //some validation
                        if (address.CountryId == 0)
                            address.CountryId = null;
                        if (address.StateProvinceId == 0)
                            address.StateProvinceId = null;
                        _addressService.InsertAddress(address);
                        vendor.AddressId = address.Id;
                        _vendorService.UpdateVendor(vendor);

                        //locales
                        UpdateLocales(vendor, model);
                        //update picture seo file name
                        UpdatePictureSeoNames(vendor);

                        _baseService.Commit();
                        response = request.CreateResponse<VendorVM>(HttpStatusCode.Created, model);
                        if (continueEditing)
                        {
                            string uri = Url.Link("GetVendorById", new { id = vendor.Id });
                            response.Headers.Location = new Uri(uri);
                        }
                        else
                        {
                            string uri = Url.Link("VendorList", null);
                            response.Headers.Location = new Uri(uri);
                        }
                        return response;
                    }
                }
                return response;

            });


        }

        [HttpPost]
        [Route("EditVendor", Name = "EditVendor")]
        public HttpResponseMessage EditVendor(HttpRequestMessage request, VendorVM model, bool continueEditing = false)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (true)
                {

                    var vendor = _vendorService.GetVendorById(model.Id);
                    if (vendor == null || vendor.Deleted)
                    {
                        //No customer role found with the specified id
                        string uri = Url.Link("VendorList", null);
                        response.Headers.Location = new Uri(uri);
                        return response;
                    }

                    try
                    {
                        if (ModelState.IsValid)
                        {
                            int prevPictureId = vendor.PictureId;
                            vendor = model.ToEntity(vendor);
                            _vendorService.UpdateVendor(vendor);

                            //activity log
                            _customerActivityService.InsertActivity("EditVendor", _localizationService.GetResource("ActivityLog.EditVendor"), vendor.Id);

                            //search engine name
                            //model.SeName = vendor.ValidateSeName(model.SeName, vendor.Name, true, _urlRecordService, _seoSettings);
                            //_urlRecordService.SaveSlug(vendor, model.SeName, 0);

                            //address
                            var address = _addressService.GetAddressById(vendor.AddressId);
                            if (address == null)
                            {
                                address = model.Address.ToEntity();
                                address.CreatedOnUtc = DateTime.UtcNow;
                                //some validation
                                if (address.CountryId == 0)
                                    address.CountryId = null;
                                if (address.StateProvinceId == 0)
                                    address.StateProvinceId = null;

                                _addressService.InsertAddress(address);
                                vendor.AddressId = address.Id;
                                _vendorService.UpdateVendor(vendor);
                            }
                            else
                            {
                                address = model.Address.ToEntity(address);
                                //some validation
                                if (address.CountryId == 0)
                                    address.CountryId = null;
                                if (address.StateProvinceId == 0)
                                    address.StateProvinceId = null;

                                _addressService.UpdateAddress(address);
                            }


                            //locales
                            UpdateLocales(vendor, model);
                            //delete an old picture (if deleted or updated)
                            if (prevPictureId > 0 && prevPictureId != vendor.PictureId)
                            {
                                var prevPicture = _pictureService.GetPictureById(prevPictureId);
                                if (prevPicture != null)
                                    _pictureService.DeletePicture(prevPicture);
                            }
                            //update picture seo file name
                            UpdatePictureSeoNames(vendor);

                            _baseService.Commit();
                            response = request.CreateResponse<VendorVM>(HttpStatusCode.OK, model);
                            //SuccessNotification(_localizationService.GetResource("Admin.Vendors.Updated"));
                            if (continueEditing)
                            {
                                // Generate a link to the update item and set the Location header in the response.
                                string uri = Url.Link("GetVendorById", new { id = vendor.Id });
                                response.Headers.Location = new Uri(uri);
                            }
                            else
                            {
                                string uri = Url.Link("VendorList", null);
                                response.Headers.Location = new Uri(uri);
                                RedirectToRoute("VendorList", new { pageIndex = 1, pageSize = 57788, showHidden = true});
                            }
                            return response;
                        }

                        //If we got this far, something failed, redisplay form
                        PrepareVendorModel(model, vendor, true, true);
                        response = request.CreateResponse<VendorVM>(HttpStatusCode.OK, model);
                        return response;
                    }
                    catch (Exception exc)
                    {
                        LogError(exc);
                        string uri = Url.Link("GetVendorById", new { id = vendor.Id });
                        response.Headers.Location = new Uri(uri);
                        return response;
                    }
                }
                return response;

            });
        }


        [HttpPost]
        [Route("DeleteVendor")]
        public HttpResponseMessage DeleteVendor(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.NotFound);
                if (true)
                {
                    if (!ModelState.IsValid)
                    {
                        response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                    }
                    else
                    {
                        var vendor = _vendorService.GetVendorById(id);
                        try
                        {
                            if (vendor != null)
                            {
                                //clear associated customer references
                                var associatedCustomers = _customerService.GetAllCustomers(vendorId: vendor.Id);
                                foreach (var customer in associatedCustomers)
                                {
                                    customer.VendorId = 0;
                                    _customerService.UpdateCustomer(customer);
                                }

                                //delete a vendor
                                _vendorService.DeleteVendor(vendor);

                                //activity log
                                _customerActivityService.InsertActivity("DeleteVendor", _localizationService.GetResource("ActivityLog.DeleteVendor"), vendor.Id);
                            }
                            _baseService.Commit();
                            string uri = Url.Link("VendorList", null);
                            response.Headers.Location = new Uri(uri);
                        }
                        catch (Exception exc)
                        {
                            LogError(exc);
                            string uri = Url.Link("GetVendorById", new { id = vendor.Id });
                            response.Headers.Location = new Uri(uri);
                        }
                        return response;
                    }
                }

                return response;
            });
        }

        #endregion

        #region Vendor notes

        [HttpPost]
        [Route("{vendorId}/vendorNotes")]
        public HttpResponseMessage VendorNotesSelect(HttpRequestMessage request, int vendorId)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (true)
                {
                    var vendor = _vendorService.GetVendorById(vendorId);
                    if (vendor == null)
                        throw new ArgumentException("No vendor found with the specified id");

                    var vendorNoteModels = new List<VendorVM.VendorNote>();
                    foreach (var vendorNote in vendor.VendorNotes
                        .OrderByDescending(vn => vn.CreatedOnUtc))
                    {
                        vendorNoteModels.Add(new VendorVM.VendorNote
                        {
                            Id = vendorNote.Id,
                            VendorId = vendorNote.VendorId,
                            Note = vendorNote.FormatVendorNoteText(),
                            CreatedOn = _dateTimeHelper.ConvertToUserTime(vendorNote.CreatedOnUtc, DateTimeKind.Utc)
                        });
                    }

                    var gridModel = new DataSourceResult
                    {
                        Data = vendorNoteModels,
                        Total = vendorNoteModels.Count
                    };

                    response = request.CreateResponse<DataSourceResult>(HttpStatusCode.OK, gridModel);
                }
                return response;

            });
        }

        [HttpPost]
        [Route("{vendorId:int}/vendorNotes/Add")]
        public HttpResponseMessage VendorNoteAdd(HttpRequestMessage request, int vendorId, string message)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (true)
                {
                    var vendor = _vendorService.GetVendorById(vendorId);
                    if (vendor == null)
                    {
                        response = request.CreateResponse(HttpStatusCode.OK, new { Result = false });
                        return response;
                    }

                    var vendorNote = new VendorNote
                    {
                        Note = message,
                        CreatedOnUtc = DateTime.UtcNow,
                    };
                    vendor.VendorNotes.Add(vendorNote);
                    _vendorService.UpdateVendor(vendor);

                    _baseService.Commit();
                    response = request.CreateResponse(HttpStatusCode.OK, new { Result = true });
                }
                return response;

            });

            
        }

        [HttpPost]
        [Route("{vendorId:int}/vendorNotes/Delete/{id:int}")]
        public HttpResponseMessage VendorNoteDelete(HttpRequestMessage request, int id, int vendorId)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (true)
                {
                    var vendor = _vendorService.GetVendorById(vendorId);
                    if (vendor == null)
                        throw new ArgumentException("No vendor found with the specified id");

                    var vendorNote = vendor.VendorNotes.FirstOrDefault(vn => vn.Id == id);
                    if (vendorNote == null)
                        throw new ArgumentException("No vendor note found with the specified id");
                    _vendorService.DeleteVendorNote(vendorNote);

                    _baseService.Commit();
                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                return response;

            });

            
        }

        #endregion
    }
}
