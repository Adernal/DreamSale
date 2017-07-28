using Denmakers.DreamSale.Data.Context;
using Denmakers.DreamSale.Data.Infrastructure;
using Denmakers.DreamSale.Data.Repositories;
using Denmakers.DreamSale.Helpers;
using Denmakers.DreamSale.Model.Catalog;
using Denmakers.DreamSale.Model.Discounts;
using Denmakers.DreamSale.Model.Logging;
using Denmakers.DreamSale.RESTAPI.Infrastructure;
using Denmakers.DreamSale.Services.Categories;
using Denmakers.DreamSale.Services.Configuration;
using Denmakers.DreamSale.Services.Customers;
using Denmakers.DreamSale.Services.Discounts;
using Denmakers.DreamSale.Services.Localization;
using Denmakers.DreamSale.Services.Logging;
using Denmakers.DreamSale.Services.Manufacturers;
using Denmakers.DreamSale.Services.Media;
using Denmakers.DreamSale.Services.Products;
using Denmakers.DreamSale.Services.Security;
using Denmakers.DreamSale.Services.Seo;
using Denmakers.DreamSale.Services.Stores;
using Denmakers.DreamSale.Services.Vendors;
using Denmakers.DreamSale.ViewModels.AdminVM.Catalog;
using Denmakers.DreamSale.ViewModels.Mapper;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Denmakers.DreamSale.RESTAPI.Controllers
{
    [RoutePrefix("api/Manufacturers")]
    public class ManufacturerController : ApiControllerBase
    {
        #region Fields
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly ICustomerService _customerService;
        private readonly IStoreService _storeService;
        private readonly IStoreMappingService _storeMappingService;
        private readonly IUrlRecordService _urlRecordService;
        private readonly IPictureService _pictureService;
        private readonly ILanguageService _languageService;
        private readonly ILocalizationService _localizationService;
        private readonly ILocalizedEntityService _localizedEntityService;
        private readonly IDiscountService _discountService;
        private readonly ICustomerActivityService _customerActivityService;
        private readonly IVendorService _vendorService;
        private readonly IAclService _aclService;
        private readonly IPermissionService _permissionService;
        private readonly ISettingService _settingService;
        private readonly IManufacturerService _manufacturerService;
        private readonly IManufacturerTemplateService _manufacturerTemplateService;

        //private readonly IImportManager _importManager;
        //private readonly IExportManager _exportManager;
        private readonly CatalogSettings _catalogSettings;
        #endregion

        #region Constructors
        public ManufacturerController(IRepository<Log> log, IUnitOfWork unitOfWork, IWorkContext workContext, IWebHelper webHelper,
            ICategoryService categoryService, IProductService productService,
            ICustomerService customerService,
            IUrlRecordService urlRecordService,
            IPictureService pictureService,
            ILanguageService languageService,
            ILocalizationService localizationService,
            ILocalizedEntityService localizedEntityService,
            IDiscountService discountService,
            IPermissionService permissionService,
            IAclService aclService,
            IStoreService storeService,
            IStoreMappingService storeMappingService,
            //IExportManager exportManager,
            //IImportManager importManager,
            IVendorService vendorService,
            ICustomerActivityService customerActivityService,
            ISettingService settingService,
            IManufacturerTemplateService manufacturerTemplateService,
            IManufacturerService manufacturerService
            )
            : base(log, unitOfWork, workContext, webHelper)
        {
            this._categoryService = categoryService;
            this._manufacturerTemplateService = manufacturerTemplateService;
            this._manufacturerService = manufacturerService;
            this._productService = productService;
            this._customerService = customerService;
            this._urlRecordService = urlRecordService;
            this._pictureService = pictureService;
            this._languageService = languageService;
            this._localizationService = localizationService;
            this._localizedEntityService = localizedEntityService;
            this._discountService = discountService;
            this._permissionService = permissionService;
            this._vendorService = vendorService;
            this._aclService = aclService;
            this._storeService = storeService;
            this._storeMappingService = storeMappingService;
            //this._importManager = importManager;
            //this._exportManager = exportManager;
            this._customerActivityService = customerActivityService;
            this._settingService = settingService;
            this._catalogSettings = this._settingService.LoadSetting<CatalogSettings>();
        }

        #endregion

        #region Utilities
        [NonAction]
        protected virtual void UpdatePictureSeoNames(Manufacturer manufacturer)
        {
            var picture = _pictureService.GetPictureById(manufacturer.PictureId);
            if (picture != null)
                _pictureService.SetSeoFilename(picture.Id, _pictureService.GetPictureSeName(manufacturer.Name));
        }

        [NonAction]
        protected virtual void PrepareTemplatesModel(ManufacturerVM model)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            var templates = _manufacturerTemplateService.GetAllManufacturerTemplates();
            foreach (var template in templates)
            {
                model.AvailableManufacturerTemplates.Add(new System.Web.Mvc.SelectListItem
                {
                    Text = template.Name,
                    Value = template.Id.ToString()
                });
            }
        }

        [NonAction]
        protected virtual void PrepareDiscountModel(ManufacturerVM model, Manufacturer manufacturer, bool excludeProperties)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            if (!excludeProperties && manufacturer != null)
                model.SelectedDiscountIds = manufacturer.AppliedDiscounts.Select(d => d.Id).ToList();

            foreach (var discount in _discountService.GetAllDiscounts(DiscountType.AssignedToManufacturers, showHidden: true))
            {
                model.AvailableDiscounts.Add(new System.Web.Mvc.SelectListItem
                {
                    Text = discount.Name,
                    Value = discount.Id.ToString(),
                    Selected = model.SelectedDiscountIds.Contains(discount.Id)
                });
            }
        }

        [NonAction]
        protected virtual void PrepareAclModel(ManufacturerVM model, Manufacturer manufacturer, bool excludeProperties)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            if (!excludeProperties && manufacturer != null)
                model.SelectedCustomerRoleIds = _aclService.GetCustomerRoleIdsWithAccess(manufacturer).ToList();

            var allRoles = _customerService.GetAllCustomerRoles(true);
            foreach (var role in allRoles)
            {
                model.AvailableCustomerRoles.Add(new System.Web.Mvc.SelectListItem
                {
                    Text = role.Name,
                    Value = role.Id.ToString(),
                    Selected = model.SelectedCustomerRoleIds.Contains(role.Id)
                });
            }
        }

        [NonAction]
        protected virtual void PrepareStoresMappingModel(ManufacturerVM model, Manufacturer manufacturer, bool excludeProperties)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            if (!excludeProperties && manufacturer != null)
                model.SelectedStoreIds = _storeMappingService.GetStoresIdsWithAccess(manufacturer).ToList();

            var allStores = _storeService.GetAllStores();
            foreach (var store in allStores)
            {
                model.AvailableStores.Add(new System.Web.Mvc.SelectListItem
                {
                    Text = store.Name,
                    Value = store.Id.ToString(),
                    Selected = model.SelectedStoreIds.Contains(store.Id)
                });
            }
        }

        [NonAction]
        protected virtual void SaveManufacturerAcl(Manufacturer manufacturer, ManufacturerVM model)
        {
            manufacturer.SubjectToAcl = model.SelectedCustomerRoleIds.Any();

            var existingAclRecords = _aclService.GetAclRecords(manufacturer);
            var allCustomerRoles = _customerService.GetAllCustomerRoles(true);
            foreach (var customerRole in allCustomerRoles)
            {
                if (model.SelectedCustomerRoleIds.Contains(customerRole.Id))
                {
                    //new role
                    if (existingAclRecords.Count(acl => acl.CustomerRoleId == customerRole.Id) == 0)
                        _aclService.InsertAclRecord(manufacturer, customerRole.Id);
                }
                else
                {
                    //remove role
                    var aclRecordToDelete = existingAclRecords.FirstOrDefault(acl => acl.CustomerRoleId == customerRole.Id);
                    if (aclRecordToDelete != null)
                        _aclService.DeleteAclRecord(aclRecordToDelete);
                }
            }
        }

        [NonAction]
        protected virtual void SaveStoreMappings(Manufacturer manufacturer, ManufacturerVM model)
        {
            manufacturer.LimitedToStores = model.SelectedStoreIds.Any();

            var existingStoreMappings = _storeMappingService.GetStoreMappings(manufacturer);
            var allStores = _storeService.GetAllStores();
            foreach (var store in allStores)
            {
                if (model.SelectedStoreIds.Contains(store.Id))
                {
                    //new store
                    if (existingStoreMappings.Count(sm => sm.StoreId == store.Id) == 0)
                        _storeMappingService.InsertStoreMapping(manufacturer, store.Id);
                }
                else
                {
                    //remove store
                    var storeMappingToDelete = existingStoreMappings.FirstOrDefault(sm => sm.StoreId == store.Id);
                    if (storeMappingToDelete != null)
                        _storeMappingService.DeleteStoreMapping(storeMappingToDelete);
                }
            }
        }

        [NonAction]
        protected virtual void UpdateLocales(Manufacturer manufacturer, ManufacturerVM model)
        {
            foreach (var localized in model.Locales)
            {
                _localizedEntityService.SaveLocalizedValue(manufacturer,
                                                               x => x.Name,
                                                               localized.Name,
                                                               localized.LanguageId);

                _localizedEntityService.SaveLocalizedValue(manufacturer,
                                                           x => x.Description,
                                                           localized.Description,
                                                           localized.LanguageId);

                _localizedEntityService.SaveLocalizedValue(manufacturer,
                                                           x => x.MetaKeywords,
                                                           localized.MetaKeywords,
                                                           localized.LanguageId);

                _localizedEntityService.SaveLocalizedValue(manufacturer,
                                                           x => x.MetaDescription,
                                                           localized.MetaDescription,
                                                           localized.LanguageId);

                _localizedEntityService.SaveLocalizedValue(manufacturer,
                                                           x => x.MetaTitle,
                                                           localized.MetaTitle,
                                                           localized.LanguageId);

                //search engine name
                _urlRecordService.SaveSlug(manufacturer, localized.SeName, localized.LanguageId);
            }
        }
        #endregion

        #region Manufacturer: CRUD
        [HttpGet]
        [Route("DefaultPageLoad")]
        public HttpResponseMessage DefaultPageLoad(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");

                var model = new ManufacturerListVM();
                model.AvailableStores.Add(new System.Web.Mvc.SelectListItem { Text = _localizationService.GetResource("Admin.Common.All"), Value = "0" });
                foreach (var s in _storeService.GetAllStores())
                    model.AvailableStores.Add(new System.Web.Mvc.SelectListItem { Text = s.Name, Value = s.Id.ToString() });

                response = request.CreateResponse<ManufacturerListVM>(HttpStatusCode.OK, model);

                return response;
            });
        }

        [HttpGet]
        [Route("", Name = "ManufacturerList")]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                var manufacturers = _manufacturerService.GetAllManufacturers();
                if (manufacturers != null)
                {
                    var gridModel = new DataSourceResult
                    {
                        Data = manufacturers.Select(x => x.ToModel()),
                        Total = manufacturers.TotalCount
                    };
                    response = request.CreateResponse<DataSourceResult>(HttpStatusCode.OK, gridModel);
                }
                return response;
            });
        }

        [HttpGet]
        [Route("{Id}", Name = "GetManufacturerById")]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No item found");
                var manufacturer = _manufacturerService.GetManufacturerById(id);
                if (manufacturer != null && !manufacturer.Deleted)
                {
                    var model = manufacturer.ToModel();
                    //templates
                    PrepareTemplatesModel(model);
                    //discounts
                    PrepareDiscountModel(model, manufacturer, false);
                    //ACL
                    PrepareAclModel(model, manufacturer, false);
                    //Stores
                    PrepareStoresMappingModel(model, manufacturer, false);
                    
                    response = request.CreateResponse<ManufacturerVM>(HttpStatusCode.OK, model);
                }
                return response;
            });
        }

        [HttpGet]
        [Route("Search/{manufacturerName}/{storeId:int=0}/{pageIndex:int=0}/{pageSize:int=214748364}")]
        public HttpResponseMessage GetBySearchCriteria(HttpRequestMessage request, string manufacturerName, int storeId, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                var manufacturers = _manufacturerService.GetAllManufacturers(manufacturerName, storeId, pageIndex, pageSize, true);
                if (manufacturers != null)
                {
                    var gridModel = new DataSourceResult
                    {
                        Data = manufacturers.Select(x => x.ToModel()),
                        Total = manufacturers.TotalCount
                    };
                    response = request.CreateResponse<DataSourceResult>(HttpStatusCode.OK, gridModel);
                }
                return response;
            });
        }

        [HttpGet]
        [Route("GetAddData")]
        public HttpResponseMessage GetAddData(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var model = new ManufacturerVM();
                //templates
                PrepareTemplatesModel(model);
                //discounts
                PrepareDiscountModel(model, null, true);
                //ACL
                PrepareAclModel(model, null, false);
                //Stores
                PrepareStoresMappingModel(model, null, false);
                //default values
                model.PageSize = _catalogSettings.DefaultManufacturerPageSize;
                model.PageSizeOptions = _catalogSettings.DefaultManufacturerPageSizeOptions;
                model.Published = true;
                model.AllowCustomersToSelectPageSize = true;

                response = request.CreateResponse<ManufacturerVM>(HttpStatusCode.OK, model);

                return response;
            });
        }

        [HttpPost]
        [Route("Add")]
        public HttpResponseMessage Add(HttpRequestMessage request, ManufacturerVM model, bool continueEditing = false)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (ModelState.IsValid)
                {
                    var manufacturer = model.ToEntity();
                    manufacturer.CreatedOnUtc = DateTime.UtcNow;
                    manufacturer.UpdatedOnUtc = DateTime.UtcNow;
                    _manufacturerService.InsertManufacturer(manufacturer);
                    //search engine name
                    _urlRecordService.SaveSlug(manufacturer, model.SeName, 0);
                    //locales
                    UpdateLocales(manufacturer, model);
                    //discounts
                    var allDiscounts = _discountService.GetAllDiscounts(DiscountType.AssignedToManufacturers, showHidden: true);
                    foreach (var discount in allDiscounts)
                    {
                        if (model.SelectedDiscountIds != null && model.SelectedDiscountIds.Contains(discount.Id))
                            manufacturer.AppliedDiscounts.Add(discount);
                    }
                    _manufacturerService.UpdateManufacturer(manufacturer);
                    //update picture seo file name
                    UpdatePictureSeoNames(manufacturer);
                    //ACL (customer roles)
                    SaveManufacturerAcl(manufacturer, model);
                    //Stores
                    SaveStoreMappings(manufacturer, model);

                    //activity log
                    _customerActivityService.InsertActivity("AddNewManufacturer", _localizationService.GetResource("ActivityLog.AddNewManufacturer"), manufacturer.Name);

                    _unitOfWork.Commit();
                    response = request.CreateResponse<Manufacturer>(HttpStatusCode.Created, manufacturer);
                    if (continueEditing)
                    {
                        RedirectToRoute("GetManufacturerById", new { id = manufacturer.Id });
                        //// Generate a link to the update item and set the Location header in the response.
                        //string uri = Url.Link("GetManufacturerById", new { id = manufacturer.Id });
                        //response.Headers.Location = new Uri(uri);
                    }
                    else
                    {
                        string uri = Url.Link("GetAll", null);
                        response.Headers.Location = new Uri(uri);
                    }
                }
                else
                {
                    //If we got this far, something failed, redisplay form
                    //templates
                    PrepareTemplatesModel(model);
                    //discounts
                    PrepareDiscountModel(model, null, true);
                    //ACL
                    PrepareAclModel(model, null, true);
                    //Stores
                    PrepareStoresMappingModel(model, null, true);

                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }

                return response;
            });
        }

        [HttpPost]
        [Route("Update")]
        public HttpResponseMessage Update(HttpRequestMessage request, ManufacturerVM model, bool continueEditing = false)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.NotFound);
                var manufacturer = _manufacturerService.GetManufacturerById(model.Id);
                if (!ModelState.IsValid)
                {
                    //If we got this far, something failed, redisplay form
                    //templates
                    PrepareTemplatesModel(model);
                    //discounts
                    PrepareDiscountModel(model, manufacturer, true);
                    //ACL
                    PrepareAclModel(model, manufacturer, true);
                    //Stores
                    PrepareStoresMappingModel(model, manufacturer, true);

                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {

                    if (manufacturer != null && !manufacturer.Deleted)
                    {
                        int prevPictureId = manufacturer.PictureId;
                        manufacturer = model.ToEntity(manufacturer);
                        manufacturer.UpdatedOnUtc = DateTime.UtcNow;
                        _manufacturerService.UpdateManufacturer(manufacturer);
                        _urlRecordService.SaveSlug(manufacturer, model.SeName, 0);
                        //locales
                        UpdateLocales(manufacturer, model);
                        //discounts
                        var allDiscounts = _discountService.GetAllDiscounts(DiscountType.AssignedToManufacturers, showHidden: true);
                        foreach (var discount in allDiscounts)
                        {
                            if (model.SelectedDiscountIds != null && model.SelectedDiscountIds.Contains(discount.Id))
                            {
                                //new discount
                                if (manufacturer.AppliedDiscounts.Count(d => d.Id == discount.Id) == 0)
                                    manufacturer.AppliedDiscounts.Add(discount);
                            }
                            else
                            {
                                //remove discount
                                if (manufacturer.AppliedDiscounts.Count(d => d.Id == discount.Id) > 0)
                                    manufacturer.AppliedDiscounts.Remove(discount);
                            }
                        }
                        _manufacturerService.UpdateManufacturer(manufacturer);
                        //delete an old picture (if deleted or updated)
                        if (prevPictureId > 0 && prevPictureId != manufacturer.PictureId)
                        {
                            var prevPicture = _pictureService.GetPictureById(prevPictureId);
                            if (prevPicture != null)
                                _pictureService.DeletePicture(prevPicture);
                        }
                        //update picture seo file name
                        UpdatePictureSeoNames(manufacturer);
                        //ACL
                        SaveManufacturerAcl(manufacturer, model);
                        //Stores
                        SaveStoreMappings(manufacturer, model);

                        //activity log
                        _customerActivityService.InsertActivity("EditManufacturer", _localizationService.GetResource("ActivityLog.EditManufacturer"), manufacturer.Name);

                        _unitOfWork.Commit();
                        response = request.CreateResponse<Manufacturer>(HttpStatusCode.OK, manufacturer);
                        if (continueEditing)
                        {
                            // Generate a link to the update item and set the Location header in the response.
                            string uri = Url.Link("GetManufacturerById", new { id = manufacturer.Id });
                            response.Headers.Location = new Uri(uri);
                        }
                    }
                }
                return response;
            });
        }

        [HttpPost]
        [Route("Delete")]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.NotFound);
                if (!ModelState.IsValid)
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var manufacturer = _manufacturerService.GetManufacturerById(id);
                    if (manufacturer != null)
                    {
                        _manufacturerService.DeleteManufacturer(manufacturer);

                        //activity log
                        _customerActivityService.InsertActivity("DeleteManufacturer", _localizationService.GetResource("ActivityLog.DeleteManufacturer"), manufacturer.Name);

                        _unitOfWork.Commit();
                        response = request.CreateResponse<Manufacturer>(HttpStatusCode.OK, manufacturer);
                    }
                }

                return response;
            });
        }

        #endregion

        #region Products
        [HttpGet]
        [Route("{manufacturerId:int}/Products/{pageIndex:int=0}/{pageSize:int=2147383647}")]
        public HttpResponseMessage GetProductsBymanufacturerIdId(HttpRequestMessage request, int manufacturerId, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                var productManufacturers = _manufacturerService.GetProductManufacturersByManufacturerId(manufacturerId, pageIndex, pageSize, true);
                if (productManufacturers != null)
                {
                    var gridModel = new DataSourceResult
                    {
                        Data = productManufacturers
                                .Select(x => new ManufacturerVM.ManufacturerProductVM
                                {
                                    Id = x.Id,
                                    ManufacturerId = x.ManufacturerId,
                                    ProductId = x.ProductId,
                                    ProductName = _productService.GetProductById(x.ProductId).Name,
                                    IsFeaturedProduct = x.IsFeaturedProduct,
                                    DisplayOrder = x.DisplayOrder
                                }),
                        Total = productManufacturers.TotalCount
                    };
                    response = request.CreateResponse(HttpStatusCode.OK, gridModel);
                }
                return response;
            });
        }

        [HttpPost]
        [Route("SaveProduct")]
        public HttpResponseMessage SaveProduct(HttpRequestMessage request, ManufacturerVM.AddManufacturerProductVM model)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (model.SelectedProductIds != null)
                {
                    foreach (int id in model.SelectedProductIds)
                    {
                        var product = _productService.GetProductById(id);
                        if (product != null)
                        {
                            var existingProductmanufacturers = _manufacturerService.GetProductManufacturersByManufacturerId(model.ManufacturerId, showHidden: true);
                            if (existingProductmanufacturers.FindProductManufacturer(id, model.ManufacturerId) == null)
                            {
                                _manufacturerService.InsertProductManufacturer(
                                    new ProductManufacturer
                                    {
                                        ManufacturerId = model.ManufacturerId,
                                        ProductId = id,
                                        IsFeaturedProduct = false,
                                        DisplayOrder = 1
                                    });

                                _unitOfWork.Commit();
                                response = request.CreateResponse<ManufacturerVM.AddManufacturerProductVM>(HttpStatusCode.Created, model);
                            }
                        }
                    }
                }

                
                return response;
            });
        }

        [HttpPost]
        [Route("ProductUpdate")]
        public HttpResponseMessage ProductUpdate(HttpRequestMessage request, ManufacturerVM.ManufacturerProductVM model)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.NotFound);
                if (!ModelState.IsValid)
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var productManufacturer = _manufacturerService.GetProductManufacturerById(model.Id);
                    if (productManufacturer != null)
                    {
                        productManufacturer.IsFeaturedProduct = model.IsFeaturedProduct;
                        productManufacturer.DisplayOrder = model.DisplayOrder;
                        _manufacturerService.UpdateProductManufacturer(productManufacturer);

                        _unitOfWork.Commit();
                        response = request.CreateResponse(HttpStatusCode.OK);
                    }
                }
                return response;
            });
        }

        [HttpPost]
        [Route("ProductDelete")]
        public HttpResponseMessage ProductDelete(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.NotFound);
                if (!ModelState.IsValid)
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var productManufacturer = _manufacturerService.GetProductManufacturerById(id);
                    if (productManufacturer != null)
                    {
                        _manufacturerService.DeleteProductManufacturer(productManufacturer);

                        _unitOfWork.Commit();
                        response = request.CreateResponse(HttpStatusCode.OK);
                    }
                }
                return response;
            });
        }

        //[HttpGet]
        //[Route("{categoryId:int}/ProductAddPopupData")]
        //public HttpResponseMessage ProductAddPopupData(HttpRequestMessage request, int categoryId)
        //{
        //    return CreateHttpResponse(request, () =>
        //    {
        //        HttpResponseMessage response = null;
        //        var model = new CategoryVM.AddCategoryProductVM();
        //        string allString = _localizationService.GetResource("Admin.Common.All");
        //        //categories
        //        model.AvailableCategories.Add(new System.Web.Mvc.SelectListItem { Text = allString, Value = "0" });
        //        var categories = SelectListHelper.GetCategoryList(_categoryService, true);
        //        foreach (var c in categories)
        //            model.AvailableCategories.Add(c);

        //        //manufacturers
        //        model.AvailableManufacturers.Add(new System.Web.Mvc.SelectListItem { Text = allString, Value = "0" });
        //        var manufacturers = SelectListHelper.GetManufacturerList(_manufacturerService, true);
        //        foreach (var m in manufacturers)
        //            model.AvailableManufacturers.Add(m);

        //        //stores
        //        model.AvailableStores.Add(new System.Web.Mvc.SelectListItem { Text = allString, Value = "0" });
        //        foreach (var s in _storeService.GetAllStores())
        //            model.AvailableStores.Add(new System.Web.Mvc.SelectListItem { Text = s.Name, Value = s.Id.ToString() });

        //        //vendors
        //        model.AvailableVendors.Add(new System.Web.Mvc.SelectListItem { Text = allString, Value = "0" });
        //        var vendors = SelectListHelper.GetVendorList(_vendorService, true);
        //        foreach (var v in vendors)
        //            model.AvailableVendors.Add(v);

        //        //product types
        //        model.AvailableProductTypes = ProductType.SimpleProduct.ToSelectList(_localizationService, _workContext, false).ToList();
        //        model.AvailableProductTypes.Insert(0, new System.Web.Mvc.SelectListItem { Text = allString, Value = "0" });


        //        response = request.CreateResponse<CategoryVM.AddCategoryProductVM>(HttpStatusCode.Created, model);
        //        return response;
        //    });
        //}

        //[HttpGet]
        //[Route("{pageSize:int=0}/{pageSize:int=2147483647}/ProductAddPopup")]
        //public HttpResponseMessage ProductAddPopup(HttpRequestMessage request, CategoryVM.AddCategoryProductVM model, int pageIndex = 0, int pageSize = int.MaxValue)
        //{
        //    return CreateHttpResponse(request, () =>
        //    {
        //        HttpResponseMessage response = null;
        //        var gridModel = new DataSourceResult();
        //        var products = _productService.SearchProducts(
        //            categoryIds: new List<int> { model.SearchCategoryId },
        //            manufacturerId: model.SearchManufacturerId,
        //            storeId: model.SearchStoreId,
        //            vendorId: model.SearchVendorId,
        //            productType: model.SearchProductTypeId > 0 ? (ProductType?)model.SearchProductTypeId : null,
        //            keywords: model.SearchProductName,
        //            pageIndex: pageIndex,
        //            pageSize: pageSize,
        //            showHidden: true
        //            );
        //        gridModel.Data = products;// products.Select(x => x.ToModel());
        //        gridModel.Total = products.TotalCount;


        //        response = request.CreateResponse<DataSourceResult>(HttpStatusCode.Created, gridModel);
        //        return response;
        //    });
        //}
        #endregion
    }
}
