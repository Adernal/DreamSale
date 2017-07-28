using Denmakers.DreamSale.Data.Context;
using Denmakers.DreamSale.Data.Infrastructure;
using Denmakers.DreamSale.Data.Repositories;
using Denmakers.DreamSale.Helpers;
using Denmakers.DreamSale.Model.Catalog;
using Denmakers.DreamSale.Model.Discounts;
using Denmakers.DreamSale.Model.Logging;
using Denmakers.DreamSale.RESTAPI.Infrastructure;
using Denmakers.DreamSale.Services;
using Denmakers.DreamSale.Services.Categories;
using Denmakers.DreamSale.Services.Configuration;
using Denmakers.DreamSale.Services.Customers;
using Denmakers.DreamSale.Services.Discounts;
using Denmakers.DreamSale.Services.Helpers;
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
    [RoutePrefix("api/Categories")]
    public class CategoryController : ApiControllerBase
    {
        #region Fields
        private readonly ICategoryService _categoryService;
        private readonly ICategoryTemplateService _categoryTemplateService;
        private readonly IManufacturerService _manufacturerService;
        private readonly IProductService _productService;
        private readonly ICustomerService _customerService;
        private readonly IUrlRecordService _urlRecordService;
        private readonly IPictureService _pictureService;
        private readonly ILanguageService _languageService;
        private readonly ILocalizationService _localizationService;
        private readonly ILocalizedEntityService _localizedEntityService;
        private readonly IDiscountService _discountService;
        private readonly IPermissionService _permissionService;
        private readonly IAclService _aclService;
        private readonly IStoreService _storeService;
        private readonly IStoreMappingService _storeMappingService;
        //private readonly IImportManager _importManager;
        //private readonly IExportManager _exportManager;
        private readonly ICustomerActivityService _customerActivityService;
        private readonly IVendorService _vendorService;
        private readonly ISettingService _settingService;
        private readonly CatalogSettings _catalogSettings;
        #endregion

        #region Constructors

        public CategoryController(IRepository<Log> log, IUnitOfWork unitOfWork, IWorkContext workContext, IWebHelper webHelper,
            ICategoryService categoryService, ICategoryTemplateService categoryTemplateService,
            IManufacturerService manufacturerService, IProductService productService,
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
            ISettingService settingService)
            : base(log, unitOfWork, workContext, webHelper)
        {
            this._categoryService = categoryService;
            this._categoryTemplateService = categoryTemplateService;
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
        protected virtual void UpdateLocales(Category category, CategoryVM model)
        {
            foreach (var localized in model.Locales)
            {
                _localizedEntityService.SaveLocalizedValue(category,
                                                               x => x.Name,
                                                               localized.Name,
                                                               localized.LanguageId);

                _localizedEntityService.SaveLocalizedValue(category,
                                                           x => x.Description,
                                                           localized.Description,
                                                           localized.LanguageId);

                _localizedEntityService.SaveLocalizedValue(category,
                                                           x => x.MetaKeywords,
                                                           localized.MetaKeywords,
                                                           localized.LanguageId);

                _localizedEntityService.SaveLocalizedValue(category,
                                                           x => x.MetaDescription,
                                                           localized.MetaDescription,
                                                           localized.LanguageId);

                _localizedEntityService.SaveLocalizedValue(category,
                                                           x => x.MetaTitle,
                                                           localized.MetaTitle,
                                                           localized.LanguageId);

                //search engine name
                _urlRecordService.SaveSlug(category, localized.SeName, localized.LanguageId);
            }
        }

        [NonAction]
        protected virtual void UpdatePictureSeoNames(Category category)
        {
            var picture = _pictureService.GetPictureById(category.PictureId);
            if (picture != null)
                _pictureService.SetSeoFilename(picture.Id, _pictureService.GetPictureSeName(category.Name));
        }

        [NonAction]
        protected virtual void SaveCategoryAcl(Category category)
        {
            var selectedCustomerRoleIds = _aclService.GetCustomerRoleIdsWithAccess(category).ToList();
            category.SubjectToAcl = selectedCustomerRoleIds.Any();

            var existingAclRecords = _aclService.GetAclRecords(category);
            var allCustomerRoles = _customerService.GetAllCustomerRoles(true);
            foreach (var customerRole in allCustomerRoles)
            {
                if (selectedCustomerRoleIds.Contains(customerRole.Id))
                {
                    //new role
                    if (existingAclRecords.Count(acl => acl.CustomerRoleId == customerRole.Id) == 0)
                        _aclService.InsertAclRecord(category, customerRole.Id);
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
        protected virtual void SaveStoreMappings(Category category)
        {
            var selectedStoreIds = _storeMappingService.GetStoresIdsWithAccess(category).ToList();
            category.LimitedToStores = selectedStoreIds.Any();

            var existingStoreMappings = _storeMappingService.GetStoreMappings(category);
            var allStores = _storeService.GetAllStores();
            foreach (var store in allStores)
            {
                if (selectedStoreIds.Contains(store.Id))
                {
                    //new store
                    if (existingStoreMappings.Count(sm => sm.StoreId == store.Id) == 0)
                        _storeMappingService.InsertStoreMapping(category, store.Id);
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
        protected virtual void PrepareAllCategoriesModel(CategoryVM model)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            model.AvailableCategories.Add(new System.Web.Mvc.SelectListItem
            {
                Text = _localizationService.GetResource("Admin.Catalog.Categories.Fields.Parent.None"),
                Value = "0"
            });
            var categories = SelectListHelper.GetCategoryList(_categoryService, true);
            foreach (var c in categories)
                model.AvailableCategories.Add(c);
        }

        [NonAction]
        protected virtual void PrepareTemplatesModel(CategoryVM model)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            var templates = _categoryTemplateService.GetAllCategoryTemplates();
            foreach (var template in templates)
            {
                model.AvailableCategoryTemplates.Add(new System.Web.Mvc.SelectListItem
                {
                    Text = template.Name,
                    Value = template.Id.ToString()
                });
            }
        }

        [NonAction]
        protected virtual void PrepareDiscountModel(CategoryVM model, Category category, bool excludeProperties)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            if (!excludeProperties && category != null)
                model.SelectedDiscountIds = category.AppliedDiscounts.Select(d => d.Id).ToList();

            foreach (var discount in _discountService.GetAllDiscounts(DiscountType.AssignedToCategories, showHidden: true))
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
        protected virtual void PrepareAclModel(CategoryVM model, Category category, bool excludeProperties)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            if (!excludeProperties && category != null)
                model.SelectedCustomerRoleIds = _aclService.GetCustomerRoleIdsWithAccess(category).ToList();

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
        protected virtual void PrepareStoresMappingModel(CategoryVM model, Category category, bool excludeProperties)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            if (!excludeProperties && category != null)
                model.SelectedStoreIds = _storeMappingService.GetStoresIdsWithAccess(category).ToList();

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


        #endregion

        #region Category: CRUD
        [HttpGet]
        [Route("DefaultPageLoad", Name = "DefaultCategoryList")]
        public HttpResponseMessage DefaultPageLoad(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");

                var model = new CategoryListVM();
                model.AvailableStores.Add(new System.Web.Mvc.SelectListItem { Text = _localizationService.GetResource("Admin.Common.All"), Value = "0" });
                foreach (var s in _storeService.GetAllStores())
                    model.AvailableStores.Add(new System.Web.Mvc.SelectListItem { Text = s.Name, Value = s.Id.ToString() });

                response = request.CreateResponse<CategoryListVM>(HttpStatusCode.OK, model);

                return response;
            });
        }

        [HttpGet]
        [Route("", Name = "CategoryList")]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                var categories = _categoryService.GetAllCategories();
                if (categories != null)
                {
                    var gridModel = new DataSourceResult
                    {
                        Data = categories.Select(x =>
                        {
                            var categoryModel = x.ToModel();
                            categoryModel.Breadcrumb = x.GetFormattedBreadCrumb(_categoryService);
                            return categoryModel;
                        }),
                        Total = categories.TotalCount
                    };
                    response = request.CreateResponse<DataSourceResult>(HttpStatusCode.OK, gridModel);
                }
                return response;
            });
        }

        [HttpGet]
        [Route("{Id}", Name = "GetCategoryById")]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No item found");
                var category = _categoryService.GetCategoryById(id);
                if (category != null && !category.Deleted)
                {
                    var model = category.ToModel();
                    //templates
                    PrepareTemplatesModel(model);
                    //categories
                    PrepareAllCategoriesModel(model);
                    //discounts
                    PrepareDiscountModel(model, category, false);
                    //ACL
                    PrepareAclModel(model, category, false);
                    //Stores
                    PrepareStoresMappingModel(model, category, false);
                    response = request.CreateResponse<CategoryVM>(HttpStatusCode.OK, model);
                }
                return response;
            });
        }

        [HttpGet]
        [Route("Search/{categoryName}/{storeId:int=0}/{pageIndex:int=0}/{pageSize:int=214748364}")]
        public HttpResponseMessage GetBySearchCriteria(HttpRequestMessage request, string categoryName, int storeId, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                var categories = _categoryService.GetAllCategories(categoryName, storeId, pageIndex, pageSize, true);
                if (categories != null)
                {
                    var gridModel = new DataSourceResult
                    {
                        Data = categories.Select(x =>
                        {
                            var categoryModel = x.ToModel();
                            categoryModel.Breadcrumb = x.GetFormattedBreadCrumb(_categoryService);
                            return categoryModel;
                        }),
                        Total = categories.TotalCount
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
                var model = new CategoryVM();
                //templates
                PrepareTemplatesModel(model);
                //categories
                PrepareAllCategoriesModel(model);
                //discounts
                PrepareDiscountModel(model, null, true);
                //ACL
                PrepareAclModel(model, null, false);
                //Stores
                PrepareStoresMappingModel(model, null, false);
                //default values
                model.PageSize = _catalogSettings.DefaultCategoryPageSize;
                model.PageSizeOptions = _catalogSettings.DefaultCategoryPageSizeOptions;
                model.Published = true;
                model.IncludeInTopMenu = true;
                model.AllowCustomersToSelectPageSize = true;

                response = request.CreateResponse<CategoryVM>(HttpStatusCode.OK, model);
                return response;
            });
        }

        [HttpGet]
        [Route("GetAddModel")]
        public HttpResponseMessage GetAddModel(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var model = new CategoryVM();
               
                //templates
                PrepareTemplatesModel(model);
                //categories
                PrepareAllCategoriesModel(model);
                //discounts
                PrepareDiscountModel(model, null, true);
                //ACL
                PrepareAclModel(model, null, false);
                //Stores
                PrepareStoresMappingModel(model, null, false);
                //default values
                model.PageSize = _catalogSettings.DefaultCategoryPageSize;
                model.PageSizeOptions = _catalogSettings.DefaultCategoryPageSizeOptions;
                model.Published = true;
                model.IncludeInTopMenu = true;
                model.AllowCustomersToSelectPageSize = true;

                response = request.CreateResponse<CategoryVM>(HttpStatusCode.OK, model);
                return response;
            });
        }

        [HttpPost]
        [Route("Add")]
        public HttpResponseMessage Add(HttpRequestMessage request, CategoryVM model, bool continueEditing = false)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (ModelState.IsValid)
                {
                    var category = model.ToEntity();
                    category.CreatedOnUtc = DateTime.UtcNow;
                    category.UpdatedOnUtc = DateTime.UtcNow;
                    _categoryService.InsertCategory(category);
                    _urlRecordService.SaveSlug(category, category.Name, 0);
                    //locales
                    UpdateLocales(category, model);
                    //discounts
                    var allDiscounts = _discountService.GetAllDiscounts(DiscountType.AssignedToCategories, showHidden: true);
                    foreach (var discount in allDiscounts)
                    {
                        category.AppliedDiscounts.Add(discount);
                    }
                    _categoryService.UpdateCategory(category);

                    //update picture seo file name
                    UpdatePictureSeoNames(category);
                    //ACL (customer roles)
                    SaveCategoryAcl(category);
                    //Stores
                    SaveStoreMappings(category);

                    //activity log
                    _customerActivityService.InsertActivity("AddNewCategory", _localizationService.GetResource("ActivityLog.AddNewCategory"), category.Name);
                    _unitOfWork.Commit();
                    response = request.CreateResponse<Category>(HttpStatusCode.Created, category);
                    if (continueEditing)
                    {
                        // Generate a link to the update item and set the Location header in the response.
                        string uri = Url.Link("GetCategoryById", new { id = category.Id });
                        response.Headers.Location = new Uri(uri);
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
        public HttpResponseMessage Update(HttpRequestMessage request, CategoryVM model, bool continueEditing = false)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.NotFound);
                var category = _categoryService.GetCategoryById(model.Id);
                if (!ModelState.IsValid)
                {
                    //If we got this far, something failed, redisplay form
                    //templates
                    PrepareTemplatesModel(model);
                    //categories
                    PrepareAllCategoriesModel(model);
                    //discounts
                    PrepareDiscountModel(model, category, true);
                    //ACL
                    PrepareAclModel(model, category, true);
                    //Stores
                    PrepareStoresMappingModel(model, category, true);

                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {

                    if (category != null && !category.Deleted)
                    {
                        int prevPictureId = category.PictureId;
                        category = model.ToEntity(category);
                        category.UpdatedOnUtc = DateTime.UtcNow;
                        _categoryService.UpdateCategory(category);

                        _urlRecordService.SaveSlug(category, category.Name, 0);

                        //discounts
                        var allDiscounts = _discountService.GetAllDiscounts(DiscountType.AssignedToCategories, showHidden: true);
                        foreach (var discount in allDiscounts)
                        {
                            var selectedDiscountIds = category.AppliedDiscounts.Select(d => d.Id).ToList();
                            if (selectedDiscountIds != null && selectedDiscountIds.Contains(discount.Id))
                            {
                                //new discount
                                if (category.AppliedDiscounts.Count(d => d.Id == discount.Id) == 0)
                                    category.AppliedDiscounts.Add(discount);
                            }
                            else
                            {
                                //remove discount
                                if (category.AppliedDiscounts.Count(d => d.Id == discount.Id) > 0)
                                    category.AppliedDiscounts.Remove(discount);
                            }
                        }
                        _categoryService.UpdateCategory(category);
                        //delete an old picture (if deleted or updated)
                        if (prevPictureId > 0 && prevPictureId != category.PictureId)
                        {
                            var prevPicture = _pictureService.GetPictureById(prevPictureId);
                            if (prevPicture != null)
                                _pictureService.DeletePicture(prevPicture);
                        }
                        //update picture seo file name
                        UpdatePictureSeoNames(category);
                        //ACL
                        SaveCategoryAcl(category);
                        //Stores
                        SaveStoreMappings(category);

                        //activity log
                        _customerActivityService.InsertActivity("EditCategory", _localizationService.GetResource("ActivityLog.EditCategory"), category.Name);

                        _unitOfWork.Commit();
                        response = request.CreateResponse<Category>(HttpStatusCode.OK, category);
                        if (continueEditing)
                        {
                            // Generate a link to the update item and set the Location header in the response.
                            string uri = Url.Link("GetCategoryById", new { id = category.Id });
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
                    var category = _categoryService.GetCategoryById(id);
                    if (category != null)
                    {
                        _categoryService.DeleteCategory(category);

                        //activity log
                        _customerActivityService.InsertActivity("DeleteCategory", _localizationService.GetResource("ActivityLog.DeleteCategory"), category.Name);

                        _unitOfWork.Commit();
                        response = request.CreateResponse<Category>(HttpStatusCode.OK, category);
                    }
                }

                return response;
            });
        }

        #endregion

        #region Products
        [HttpGet]
        [Route("{categoryId:int}/Products/{pageIndex:int=0}/{pageSize:int=2147383647}", Name ="GetProductsByCategoryId")]
        public HttpResponseMessage GetProductsByCategoryId(HttpRequestMessage request, int categoryId, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                var productCategories = _categoryService.GetProductCategoriesByCategoryId(categoryId, pageIndex, pageSize, true);
                if (productCategories != null)
                {
                    var gridModel = new DataSourceResult
                    {
                        Data = productCategories.Select(x => new
                        {
                            Id = x.Id,
                            CategoryId = x.CategoryId,
                            ProductId = x.ProductId,
                            ProductName = _productService.GetProductById(x.ProductId).Name,
                            IsFeaturedProduct = x.IsFeaturedProduct,
                            DisplayOrder = x.DisplayOrder
                        }),
                        Total = productCategories.TotalCount
                    };
                    response = request.CreateResponse(HttpStatusCode.OK, gridModel);
                }
                return response;
            });
        }

        [HttpPost]
        [Route("SaveProduct")]
        public HttpResponseMessage SaveProduct(HttpRequestMessage request, CategoryVM.AddCategoryProductVM model)
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
                            var existingProductCategories = _categoryService.GetProductCategoriesByCategoryId(model.CategoryId, showHidden: true);
                            if (existingProductCategories.FindProductCategory(id, model.CategoryId) == null)
                            {
                                _categoryService.InsertProductCategory(
                                    new ProductCategory
                                    {
                                        CategoryId = model.CategoryId,
                                        ProductId = id,
                                        IsFeaturedProduct = false,
                                        DisplayOrder = 1
                                    });
                                _unitOfWork.Commit();
                                response = request.CreateResponse<CategoryVM.AddCategoryProductVM>(HttpStatusCode.Created, model);
                            }
                        }
                    }
                }

                
                return response;
            });
        }

        [HttpPost]
        [Route("ProductUpdate")]
        public HttpResponseMessage ProductUpdate(HttpRequestMessage request, CategoryVM.CategoryProductVM model)
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
                    var productCategory = _categoryService.GetProductCategoryById(model.Id);
                    if (productCategory != null)
                    {
                        productCategory.IsFeaturedProduct = model.IsFeaturedProduct;
                        productCategory.DisplayOrder = model.DisplayOrder;
                        _categoryService.UpdateProductCategory(productCategory);

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
                    var productCategory = _categoryService.GetProductCategoryById(id);
                    if (productCategory != null)
                    {
                        _categoryService.DeleteProductCategory(productCategory);

                        _unitOfWork.Commit();
                        response = request.CreateResponse(HttpStatusCode.OK);
                    }
                }
                return response;
            });
        }

        [HttpGet]
        [Route("ProductForAddPopup/{categoryId:int}/ProductAddPopupData", Name = "GetProductForAddPopup")]
        public HttpResponseMessage ProductAddPopupData(HttpRequestMessage request, int categoryId)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var model = new CategoryVM.AddCategoryProductVM();
                string allString = _localizationService.GetResource("Admin.Common.All");
                //categories
                model.AvailableCategories.Add(new System.Web.Mvc.SelectListItem { Text = allString, Value = "0" });
                var categories = SelectListHelper.GetCategoryList(_categoryService, true);
                foreach (var c in categories)
                    model.AvailableCategories.Add(c);

                //manufacturers
                model.AvailableManufacturers.Add(new System.Web.Mvc.SelectListItem { Text = allString, Value = "0" });
                var manufacturers = SelectListHelper.GetManufacturerList(_manufacturerService, true);
                foreach (var m in manufacturers)
                    model.AvailableManufacturers.Add(m);

                //stores
                model.AvailableStores.Add(new System.Web.Mvc.SelectListItem { Text = allString, Value = "0" });
                foreach (var s in _storeService.GetAllStores())
                    model.AvailableStores.Add(new System.Web.Mvc.SelectListItem { Text = s.Name, Value = s.Id.ToString() });

                //vendors
                model.AvailableVendors.Add(new System.Web.Mvc.SelectListItem { Text = allString, Value = "0" });
                var vendors = SelectListHelper.GetVendorList(_vendorService, true);
                foreach (var v in vendors)
                    model.AvailableVendors.Add(v);

                //product types
                model.AvailableProductTypes = ProductType.SimpleProduct.ToSelectList(_localizationService, _workContext, false).ToList();
                model.AvailableProductTypes.Insert(0, new System.Web.Mvc.SelectListItem { Text = allString, Value = "0" });


                response = request.CreateResponse<CategoryVM.AddCategoryProductVM>(HttpStatusCode.Created, model);
                return response;
            });
        }

        //[HttpGet]
        ////[Route("{model:CategoryVM.AddCategoryProductVM}/{pageSize:int=0}/{pageSize:int=2147483647}/Products")]
        //[Route("ProductsForAddPopup/{pageSize:int=0}/{pageSize:int=2147483647}/Products", Name =  "GetProductsForAddPopup")]
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
