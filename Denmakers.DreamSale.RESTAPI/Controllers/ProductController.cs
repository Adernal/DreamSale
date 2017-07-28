using Denmakers.DreamSale.Data.Context;
using Denmakers.DreamSale.Data.Infrastructure;
using Denmakers.DreamSale.Data.Repositories;
using Denmakers.DreamSale.Helpers;
using Denmakers.DreamSale.Model.Catalog;
using Denmakers.DreamSale.Model.Directory;
using Denmakers.DreamSale.Model.Discounts;
using Denmakers.DreamSale.Model.Logging;
using Denmakers.DreamSale.Model.Seo;
using Denmakers.DreamSale.Model.Tax;
using Denmakers.DreamSale.Model.Vendors;
using Denmakers.DreamSale.RESTAPI.Infrastructure;
using Denmakers.DreamSale.Services;
using Denmakers.DreamSale.Services.Categories;
using Denmakers.DreamSale.Services.Configuration;
using Denmakers.DreamSale.Services.Customers;
using Denmakers.DreamSale.Services.Directory;
using Denmakers.DreamSale.Services.Discounts;
using Denmakers.DreamSale.Services.Helpers;
using Denmakers.DreamSale.Services.Localization;
using Denmakers.DreamSale.Services.Logging;
using Denmakers.DreamSale.Services.Manufacturers;
using Denmakers.DreamSale.Services.Media;
using Denmakers.DreamSale.Services.Orders;
using Denmakers.DreamSale.Services.Products;
using Denmakers.DreamSale.Services.Security;
using Denmakers.DreamSale.Services.Seo;
using Denmakers.DreamSale.Services.Shipping;
using Denmakers.DreamSale.Services.Shipping.Date;
using Denmakers.DreamSale.Services.Stores;
using Denmakers.DreamSale.Services.Tax;
using Denmakers.DreamSale.Services.Vendors;
using Denmakers.DreamSale.ViewModels.AdminVM.Catalog;
using Denmakers.DreamSale.ViewModels.AdminVM.Orders;
using Denmakers.DreamSale.ViewModels.Mapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
//using System.Web.Mvc;

namespace Denmakers.DreamSale.RESTAPI.Controllers
{
    [RoutePrefix("api/Products")]
    public class ProductController : ApiControllerBase
    {
        #region Fields

        private readonly IProductService _productService;
        private readonly IProductTemplateService _productTemplateService;
        private readonly ICategoryService _categoryService;
        private readonly IManufacturerService _manufacturerService;
        private readonly ICustomerService _customerService;
        private readonly IUrlRecordService _urlRecordService;
        private readonly ILanguageService _languageService;
        private readonly ILocalizationService _localizationService;
        private readonly ILocalizedEntityService _localizedEntityService;
        private readonly ISpecificationAttributeService _specificationAttributeService;
        private readonly IPictureService _pictureService;
        private readonly IProductTagService _productTagService;
        private readonly ICopyProductService _copyProductService;
        private readonly ITaxCategoryService _taxCategoryService;
        //private readonly IPdfService _pdfService;
        //private readonly IExportManager _exportManager;
        //private readonly IImportManager _importManager;
        private readonly ICustomerActivityService _customerActivityService;
        private readonly IPermissionService _permissionService;
        private readonly IAclService _aclService;
        private readonly IStoreService _storeService;
        private readonly IOrderService _orderService;
        private readonly IStoreMappingService _storeMappingService;
        private readonly IVendorService _vendorService;
        private readonly IDateRangeService _dateRangeService;
        private readonly IShippingService _shippingService;
        private readonly IShipmentService _shipmentService;
        private readonly ICurrencyService _currencyService;
        private readonly IMeasureService _measureService;
        //private readonly ICacheManager _cacheManager;
        private readonly IDateTimeHelper _dateTimeHelper;
        private readonly IDiscountService _discountService;
        private readonly IProductAttributeService _productAttributeService;
        private readonly IBackInStockSubscriptionService _backInStockSubscriptionService;
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IProductAttributeFormatter _productAttributeFormatter;
        private readonly IProductAttributeParser _productAttributeParser;
        private readonly IDownloadService _downloadService;
        private readonly ISettingService _settingService;

        private readonly CurrencySettings _currencySettings;
        private readonly TaxSettings _taxSettings;
        private readonly MeasureSettings _measureSettings;
        private readonly VendorSettings _vendorSettings;
        private readonly SeoSettings _seoSettings;
        #endregion

        #region Constructors

        public ProductController(IRepository<Log> log, IUnitOfWork unitOfWork, IWorkContext workContext, IWebHelper webHelper,
            IProductService productService,
            IProductTemplateService productTemplateService,
            ICategoryService categoryService,
            IManufacturerService manufacturerService,
            ICustomerService customerService,
            IUrlRecordService urlRecordService,
            ILanguageService languageService,
            ILocalizationService localizationService,
            ILocalizedEntityService localizedEntityService,
            ISpecificationAttributeService specificationAttributeService,
            IPictureService pictureService,
            ITaxCategoryService taxCategoryService,
            IProductTagService productTagService,
            ICopyProductService copyProductService,
            //IPdfService pdfService,
            //IExportManager exportManager,
            //IImportManager importManager,
            ICustomerActivityService customerActivityService,
            IPermissionService permissionService,
            IAclService aclService,
            IStoreService storeService,
            IOrderService orderService,
            IStoreMappingService storeMappingService,
            IVendorService vendorService,
            IDateRangeService dateRangeService,
            IShippingService shippingService,
            IShipmentService shipmentService,
            ICurrencyService currencyService,
            IMeasureService measureService,
            //ICacheManager cacheManager,
            IDateTimeHelper dateTimeHelper,
            IDiscountService discountService,
            IProductAttributeService productAttributeService,
            IBackInStockSubscriptionService backInStockSubscriptionService,
            IShoppingCartService shoppingCartService,
            IProductAttributeFormatter productAttributeFormatter,
            IProductAttributeParser productAttributeParser,
            IDownloadService downloadService,
            ISettingService settingService
            )
            : base(log, unitOfWork, workContext, webHelper)
        {
            this._productService = productService;
            this._productTemplateService = productTemplateService;
            this._categoryService = categoryService;
            this._manufacturerService = manufacturerService;
            this._customerService = customerService;
            this._urlRecordService = urlRecordService;
            this._languageService = languageService;
            this._localizationService = localizationService;
            this._localizedEntityService = localizedEntityService;
            this._specificationAttributeService = specificationAttributeService;
            this._pictureService = pictureService;
            this._taxCategoryService = taxCategoryService;
            this._productTagService = productTagService;
            this._copyProductService = copyProductService;
            //this._pdfService = pdfService;
            //this._exportManager = exportManager;
            //this._importManager = importManager;
            this._customerActivityService = customerActivityService;
            this._permissionService = permissionService;
            this._aclService = aclService;
            this._storeService = storeService;
            this._orderService = orderService;
            this._storeMappingService = storeMappingService;
            this._vendorService = vendorService;
            this._dateRangeService = dateRangeService;
            this._shippingService = shippingService;
            this._shipmentService = shipmentService;
            this._measureService = measureService;
            //this._cacheManager = cacheManager;
            this._dateTimeHelper = dateTimeHelper;
            this._discountService = discountService;
            this._productAttributeService = productAttributeService;
            this._backInStockSubscriptionService = backInStockSubscriptionService;
            this._shoppingCartService = shoppingCartService;
            this._productAttributeFormatter = productAttributeFormatter;
            this._productAttributeParser = productAttributeParser;
            this._downloadService = downloadService;

            this._settingService = settingService;
            this._taxSettings = _settingService.LoadSetting<TaxSettings>();
            this._measureSettings = _settingService.LoadSetting<MeasureSettings>();
            this._vendorSettings = _settingService.LoadSetting<VendorSettings>();
            this._currencySettings = _settingService.LoadSetting<CurrencySettings>();
            this._seoSettings = _settingService.LoadSetting<SeoSettings>();
        }

        #endregion

        #region Utilities
        [System.Web.Http.NonAction]
        protected virtual void PrepareAclModel(ProductVM model, Product product, bool excludeProperties)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            if (!excludeProperties && product != null)
                model.SelectedCustomerRoleIds = _aclService.GetCustomerRoleIdsWithAccess(product).ToList();

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
        protected virtual void SaveProductAcl(Product product, ProductVM model)
        {
            product.SubjectToAcl = model.SelectedCustomerRoleIds.Any();

            var existingAclRecords = _aclService.GetAclRecords(product);
            var allCustomerRoles = _customerService.GetAllCustomerRoles(true);
            foreach (var customerRole in allCustomerRoles)
            {
                if (model.SelectedCustomerRoleIds.Contains(customerRole.Id))
                {
                    //new role
                    if (existingAclRecords.Count(acl => acl.CustomerRoleId == customerRole.Id) == 0)
                        _aclService.InsertAclRecord(product, customerRole.Id);
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

        [System.Web.Http.NonAction]
        protected virtual void PrepareStoresMappingModel(ProductVM model, Product product, bool excludeProperties)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            if (!excludeProperties && product != null)
                model.SelectedStoreIds = _storeMappingService.GetStoresIdsWithAccess(product).ToList();

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
        protected virtual void SaveStoreMappings(Product product, ProductVM model)
        {
            product.LimitedToStores = model.SelectedStoreIds.Any();

            var existingStoreMappings = _storeMappingService.GetStoreMappings(product);
            var allStores = _storeService.GetAllStores();
            foreach (var store in allStores)
            {
                if (model.SelectedStoreIds.Contains(store.Id))
                {
                    //new store
                    if (existingStoreMappings.Count(sm => sm.StoreId == store.Id) == 0)
                        _storeMappingService.InsertStoreMapping(product, store.Id);
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

        [System.Web.Http.NonAction]
        protected virtual void PrepareCategoryMappingModel(ProductVM model, Product product, bool excludeProperties)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            if (!excludeProperties && product != null)
                model.SelectedCategoryIds = _categoryService.GetProductCategoriesByProductId(product.Id, true).Select(c => c.CategoryId).ToList();

            var allCategories = SelectListHelper.GetCategoryList(_categoryService, true);
            foreach (var c in allCategories)
            {
                c.Selected = model.SelectedCategoryIds.Contains(int.Parse(c.Value));
                model.AvailableCategories.Add(c);
            }
        }

        [NonAction]
        protected virtual void SaveCategoryMappings(Product product, ProductVM model)
        {
            var existingProductCategories = _categoryService.GetProductCategoriesByProductId(product.Id, true);

            //delete categories
            foreach (var existingProductCategory in existingProductCategories)
                if (!model.SelectedCategoryIds.Contains(existingProductCategory.CategoryId))
                    _categoryService.DeleteProductCategory(existingProductCategory);

            //add categories
            foreach (var categoryId in model.SelectedCategoryIds)
                if (existingProductCategories.FindProductCategory(product.Id, categoryId) == null)
                {
                    //find next display order
                    var displayOrder = 1;
                    var existingCategoryMapping = _categoryService.GetProductCategoriesByCategoryId(categoryId, showHidden: true);
                    if (existingCategoryMapping.Any())
                        displayOrder = existingCategoryMapping.Max(x => x.DisplayOrder) + 1;
                    _categoryService.InsertProductCategory(new ProductCategory
                    {
                        ProductId = product.Id,
                        CategoryId = categoryId,
                        DisplayOrder = displayOrder
                    });
                }
        }

        [System.Web.Http.NonAction]
        protected virtual void PrepareManufacturerMappingModel(ProductVM model, Product product, bool excludeProperties)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            if (!excludeProperties && product != null)
                model.SelectedManufacturerIds = _manufacturerService.GetProductManufacturersByProductId(product.Id, true).Select(c => c.ManufacturerId).ToList();

            var allManufacturers = SelectListHelper.GetManufacturerList(_manufacturerService, true);
            foreach (var m in allManufacturers)
            {
                m.Selected = model.SelectedManufacturerIds.Contains(int.Parse(m.Value));
                model.AvailableManufacturers.Add(m);
            }
        }

        [NonAction]
        protected virtual void SaveManufacturerMappings(Product product, ProductVM model)
        {
            var existingProductManufacturers = _manufacturerService.GetProductManufacturersByProductId(product.Id, true);

            //delete manufacturers
            foreach (var existingProductManufacturer in existingProductManufacturers)
                if (!model.SelectedManufacturerIds.Contains(existingProductManufacturer.ManufacturerId))
                    _manufacturerService.DeleteProductManufacturer(existingProductManufacturer);

            //add manufacturers
            foreach (var manufacturerId in model.SelectedManufacturerIds)
                if (existingProductManufacturers.FindProductManufacturer(product.Id, manufacturerId) == null)
                {
                    //find next display order
                    var displayOrder = 1;
                    var existingManufacturerMapping = _manufacturerService.GetProductManufacturersByManufacturerId(manufacturerId, showHidden: true);
                    if (existingManufacturerMapping.Any())
                        displayOrder = existingManufacturerMapping.Max(x => x.DisplayOrder) + 1;
                    _manufacturerService.InsertProductManufacturer(new ProductManufacturer()
                    {
                        ProductId = product.Id,
                        ManufacturerId = manufacturerId,
                        DisplayOrder = displayOrder
                    });
                }
        }

        [System.Web.Http.NonAction]
        protected virtual void PrepareDiscountMappingModel(ProductVM model, Product product, bool excludeProperties)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            if (!excludeProperties && product != null)
                model.SelectedDiscountIds = product.AppliedDiscounts.Select(d => d.Id).ToList();

            foreach (var discount in _discountService.GetAllDiscounts(DiscountType.AssignedToSkus, showHidden: true))
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
        protected virtual void SaveDiscountMappings(Product product, ProductVM model)
        {
            var allDiscounts = _discountService.GetAllDiscounts(DiscountType.AssignedToSkus, showHidden: true);

            foreach (var discount in allDiscounts)
            {
                if (model.SelectedDiscountIds != null && model.SelectedDiscountIds.Contains(discount.Id))
                {
                    //new discount
                    if (product.AppliedDiscounts.Count(d => d.Id == discount.Id) == 0)
                        product.AppliedDiscounts.Add(discount);
                }
                else
                {
                    //remove discount
                    if (product.AppliedDiscounts.Count(d => d.Id == discount.Id) > 0)
                        product.AppliedDiscounts.Remove(discount);
                }
            }

            _productService.UpdateProduct(product);
            _productService.UpdateHasDiscountsApplied(product);
        }


        [System.Web.Http.NonAction]
        protected virtual void PrepareAddProductAttributeCombinationModel(AddProductAttributeCombinationVM model, Product product)
        {
            if (model == null)
                throw new ArgumentNullException("model");
            if (product == null)
                throw new ArgumentNullException("product");

            model.ProductId = product.Id;
            model.StockQuantity = 10000;
            model.NotifyAdminForQuantityBelow = 1;

            var attributes = _productAttributeService.GetProductAttributeMappingsByProductId(product.Id)
                //ignore non-combinable attributes for combinations
                .Where(x => !x.IsNonCombinable())
                .ToList();
            foreach (var attribute in attributes)
            {
                var attributeModel = new AddProductAttributeCombinationVM.ProductAttributeVM
                {
                    Id = attribute.Id,
                    ProductAttributeId = attribute.ProductAttributeId,
                    Name = attribute.ProductAttribute.Name,
                    TextPrompt = attribute.TextPrompt,
                    IsRequired = attribute.IsRequired,
                    AttributeControlType = attribute.AttributeControlType
                };

                if (attribute.ShouldHaveValues())
                {
                    //values
                    var attributeValues = _productAttributeService.GetProductAttributeValues(attribute.Id);
                    foreach (var attributeValue in attributeValues)
                    {
                        var attributeValueModel = new AddProductAttributeCombinationVM.ProductAttributeValueVM
                        {
                            Id = attributeValue.Id,
                            Name = attributeValue.Name,
                            IsPreSelected = attributeValue.IsPreSelected
                        };
                        attributeModel.Values.Add(attributeValueModel);
                    }
                }

                model.ProductAttributes.Add(attributeModel);
            }
        }

        [System.Web.Http.NonAction]
        protected virtual void PrepareProductModel(ProductVM model, Product product, bool setPredefinedValues, bool excludeProperties)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            if (product != null)
            {
                var parentGroupedProduct = _productService.GetProductById(product.ParentGroupedProductId);
                if (parentGroupedProduct != null)
                {
                    model.AssociatedToProductId = product.ParentGroupedProductId;
                    model.AssociatedToProductName = parentGroupedProduct.Name;
                }

                model.CreatedOn = _dateTimeHelper.ConvertToUserTime(product.CreatedOnUtc, DateTimeKind.Utc);
                model.UpdatedOn = _dateTimeHelper.ConvertToUserTime(product.UpdatedOnUtc, DateTimeKind.Utc);
            }

            model.PrimaryStoreCurrencyCode = _currencyService.GetCurrencyById(_currencySettings.PrimaryStoreCurrencyId).CurrencyCode;
            model.BaseWeightIn = _measureService.GetMeasureWeightById(_measureSettings.BaseWeightId).Name;
            model.BaseDimensionIn = _measureService.GetMeasureDimensionById(_measureSettings.BaseDimensionId).Name;

            //little performance hack here
            //there's no need to load attributes when creating a new product
            //anyway they're not used (you need to save a product before you map them)
            if (product != null)
            {
                //product attributes
                foreach (var productAttribute in _productAttributeService.GetAllProductAttributes())
                {
                    model.AvailableProductAttributes.Add(new System.Web.Mvc.SelectListItem
                    {
                        Text = productAttribute.Name,
                        Value = productAttribute.Id.ToString()
                    });
                }

                //specification attributes
                var availableSpecificationAttributes = new List<System.Web.Mvc.SelectListItem>();
                foreach (var sa in _specificationAttributeService.GetSpecificationAttributes())
                {
                    availableSpecificationAttributes.Add(new System.Web.Mvc.SelectListItem
                    {
                        Text = sa.Name,
                        Value = sa.Id.ToString()
                    });
                }
                model.AddSpecificationAttributeModel.AvailableAttributes = availableSpecificationAttributes;

                //options of preselected specification attribute
                if (model.AddSpecificationAttributeModel.AvailableAttributes.Any())
                {
                    var selectedAttributeId = int.Parse(model.AddSpecificationAttributeModel.AvailableAttributes.First().Value);
                    foreach (var sao in _specificationAttributeService.GetSpecificationAttributeOptionsBySpecificationAttribute(selectedAttributeId))
                        model.AddSpecificationAttributeModel.AvailableOptions.Add(new System.Web.Mvc.SelectListItem
                        {
                            Text = sao.Name,
                            Value = sao.Id.ToString()
                        });
                }
                //default specs values
                model.AddSpecificationAttributeModel.ShowOnProductPage = true;
            }


            //copy product
            if (product != null)
            {
                model.CopyProductModel.Id = product.Id;
                model.CopyProductModel.Name = string.Format(_localizationService.GetResource("Admin.Catalog.Products.Copy.Name.New"), product.Name);
                model.CopyProductModel.Published = true;
                model.CopyProductModel.CopyImages = true;
            }

            //templates
            var templates = _productTemplateService.GetAllProductTemplates();
            foreach (var template in templates)
            {
                model.AvailableProductTemplates.Add(new System.Web.Mvc.SelectListItem
                {
                    Text = template.Name,
                    Value = template.Id.ToString()
                });
            }
            //supported product types
            foreach (var productType in ProductType.SimpleProduct.ToSelectList(_localizationService, _workContext, false).ToList())
            {
                var productTypeId = int.Parse(productType.Value);
                model.ProductsTypesSupportedByProductTemplates.Add(productTypeId, new List<System.Web.Mvc.SelectListItem>());
                foreach (var template in templates)
                {
                    if (String.IsNullOrEmpty(template.IgnoredProductTypes) ||
                        !((IList<int>)TypeDescriptor.GetConverter(typeof(List<int>)).ConvertFrom(template.IgnoredProductTypes)).Contains(productTypeId))
                    {
                        model.ProductsTypesSupportedByProductTemplates[productTypeId].Add(new System.Web.Mvc.SelectListItem
                        {
                            Text = template.Name,
                            Value = template.Id.ToString()
                        });
                    }
                }
            }

            //vendors
            model.IsLoggedInAsVendor = _workContext.CurrentVendor != null;
            model.AvailableVendors.Add(new System.Web.Mvc.SelectListItem
            {
                Text = _localizationService.GetResource("Admin.Catalog.Products.Fields.Vendor.None"),
                Value = "0"
            });
            var vendors = SelectListHelper.GetVendorList(_vendorService, true);
            foreach (var v in vendors)
                model.AvailableVendors.Add(v);

            //delivery dates
            model.AvailableDeliveryDates.Add(new System.Web.Mvc.SelectListItem
            {
                Text = _localizationService.GetResource("Admin.Catalog.Products.Fields.DeliveryDate.None"),
                Value = "0"
            });
            var deliveryDates = _dateRangeService.GetAllDeliveryDates();
            foreach (var deliveryDate in deliveryDates)
            {
                model.AvailableDeliveryDates.Add(new System.Web.Mvc.SelectListItem
                {
                    Text = deliveryDate.Name,
                    Value = deliveryDate.Id.ToString()
                });
            }

            //product availability ranges
            model.AvailableProductAvailabilityRanges.Add(new System.Web.Mvc.SelectListItem
            {
                Text = _localizationService.GetResource("Admin.Catalog.Products.Fields.ProductAvailabilityRange.None"),
                Value = "0"
            });
            foreach (var range in _dateRangeService.GetAllProductAvailabilityRanges())
            {
                model.AvailableProductAvailabilityRanges.Add(new System.Web.Mvc.SelectListItem
                {
                    Text = range.Name,
                    Value = range.Id.ToString()
                });
            }

            //warehouses
            var warehouses = _shippingService.GetAllWarehouses();
            model.AvailableWarehouses.Add(new System.Web.Mvc.SelectListItem
            {
                Text = _localizationService.GetResource("Admin.Catalog.Products.Fields.Warehouse.None"),
                Value = "0"
            });
            foreach (var warehouse in warehouses)
            {
                model.AvailableWarehouses.Add(new System.Web.Mvc.SelectListItem
                {
                    Text = warehouse.Name,
                    Value = warehouse.Id.ToString()
                });
            }

            //multiple warehouses
            foreach (var warehouse in warehouses)
            {
                var pwiModel = new ProductVM.ProductWarehouseInventoryVM
                {
                    WarehouseId = warehouse.Id,
                    WarehouseName = warehouse.Name
                };
                if (product != null)
                {
                    var pwi = product.ProductWarehouseInventory.FirstOrDefault(x => x.WarehouseId == warehouse.Id);
                    if (pwi != null)
                    {
                        pwiModel.WarehouseUsed = true;
                        pwiModel.StockQuantity = pwi.StockQuantity;
                        pwiModel.ReservedQuantity = pwi.ReservedQuantity;
                        pwiModel.PlannedQuantity = _shipmentService.GetQuantityInShipments(product, pwi.WarehouseId, true, true);
                    }
                }
                model.ProductWarehouseInventoryModels.Add(pwiModel);
            }

            //product tags
            if (product != null)
            {
                var result = new StringBuilder();
                for (int i = 0; i < product.ProductTags.Count; i++)
                {
                    var pt = product.ProductTags.ToList()[i];
                    result.Append(pt.Name);
                    if (i != product.ProductTags.Count - 1)
                        result.Append(", ");
                }
                model.ProductTags = result.ToString();
            }

            //tax categories
            var taxCategories = _taxCategoryService.GetAllTaxCategories();
            model.AvailableTaxCategories.Add(new System.Web.Mvc.SelectListItem { Text = _localizationService.GetResource("Admin.Configuration.Settings.Tax.TaxCategories.None"), Value = "0" });
            foreach (var tc in taxCategories)
                model.AvailableTaxCategories.Add(new System.Web.Mvc.SelectListItem { Text = tc.Name, Value = tc.Id.ToString(), Selected = product != null && !setPredefinedValues && tc.Id == product.TaxCategoryId });

            //baseprice units
            var measureWeights = _measureService.GetAllMeasureWeights();
            foreach (var mw in measureWeights)
                model.AvailableBasepriceUnits.Add(new System.Web.Mvc.SelectListItem { Text = mw.Name, Value = mw.Id.ToString(), Selected = product != null && !setPredefinedValues && mw.Id == product.BasepriceUnitId });
            foreach (var mw in measureWeights)
                model.AvailableBasepriceBaseUnits.Add(new System.Web.Mvc.SelectListItem { Text = mw.Name, Value = mw.Id.ToString(), Selected = product != null && !setPredefinedValues && mw.Id == product.BasepriceBaseUnitId });

            //last stock quantity
            if (product != null)
            {
                model.LastStockQuantity = product.StockQuantity;
            }

            //default values
            if (setPredefinedValues)
            {
                model.MaximumCustomerEnteredPrice = 1000;
                model.MaxNumberOfDownloads = 10;
                model.RecurringCycleLength = 100;
                model.RecurringTotalCycles = 10;
                model.RentalPriceLength = 1;
                model.StockQuantity = 10000;
                model.NotifyAdminForQuantityBelow = 1;
                model.OrderMinimumQuantity = 1;
                model.OrderMaximumQuantity = 10000;
                model.TaxCategoryId = _taxSettings.DefaultTaxCategoryId;
                model.UnlimitedDownloads = true;
                model.IsShipEnabled = true;
                model.AllowCustomerReviews = true;
                model.Published = true;
                model.VisibleIndividually = true;
            }

            //editor settings
            var productEditorSettings = _settingService.LoadSetting<ProductEditorSettings>();
            model.ProductEditorSettingsModel = productEditorSettings.ToModel();
        }

        [System.Web.Http.NonAction]
        protected virtual List<int> GetChildCategoryIds(int parentCategoryId)
        {
            var categoriesIds = new List<int>();
            var categories = _categoryService.GetAllCategoriesByParentCategoryId(parentCategoryId, true);
            foreach (var category in categories)
            {
                categoriesIds.Add(category.Id);
                categoriesIds.AddRange(GetChildCategoryIds(category.Id));
            }
            return categoriesIds;
        }

        [System.Web.Http.NonAction]
        protected virtual string[] ParseProductTags(string productTags)
        {
            var result = new List<string>();
            if (!String.IsNullOrWhiteSpace(productTags))
            {
                string[] values = productTags.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string val1 in values)
                    if (!String.IsNullOrEmpty(val1.Trim()))
                        result.Add(val1.Trim());
            }
            return result.ToArray();
        }



        [NonAction]
        protected virtual void UpdatePictureSeoNames(Product product)
        {
            foreach (var pp in product.ProductPictures)
                _pictureService.SetSeoFilename(pp.PictureId, _pictureService.GetPictureSeName(product.Name));
        }
        #endregion

        #region Methods

        #region Products: CRUD
        [HttpGet]
        [Route("DefaultPageLoad", Name = "DefaultProductPageLoad")]
        public HttpResponseMessage DefaultPageLoad(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                string allText = _localizationService.GetResource("Admin.Common.All");
                var model = new ProductListVM();
                //a vendor should have access only to his products
                model.IsLoggedInAsVendor = _workContext.CurrentVendor != null;
                model.AllowVendorsToImportProducts = _vendorSettings.AllowVendorsToImportProducts;

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

                //warehouses
                model.AvailableWarehouses.Add(new System.Web.Mvc.SelectListItem { Text = allText, Value = "0" });
                foreach (var wh in _shippingService.GetAllWarehouses())
                    model.AvailableWarehouses.Add(new System.Web.Mvc.SelectListItem { Text = wh.Name, Value = wh.Id.ToString() });

                //vendors
                model.AvailableVendors.Add(new System.Web.Mvc.SelectListItem { Text = allText, Value = "0" });
                var vendors = SelectListHelper.GetVendorList(_vendorService, true);
                foreach (var v in vendors)
                    model.AvailableVendors.Add(v);

                //product types
                model.AvailableProductTypes = ProductType.SimpleProduct.ToSelectList(_localizationService, _workContext, false).ToList();
                model.AvailableProductTypes.Insert(0, new System.Web.Mvc.SelectListItem { Text = allText, Value = "0" });

                //"published" property
                //0 - all (according to "ShowHidden" parameter)
                //1 - published only
                //2 - unpublished only
                model.AvailablePublishedOptions.Add(new System.Web.Mvc.SelectListItem { Text = _localizationService.GetResource("Admin.Catalog.Products.List.SearchPublished.All"), Value = "0" });
                model.AvailablePublishedOptions.Add(new System.Web.Mvc.SelectListItem { Text = _localizationService.GetResource("Admin.Catalog.Products.List.SearchPublished.PublishedOnly"), Value = "1" });
                model.AvailablePublishedOptions.Add(new System.Web.Mvc.SelectListItem { Text = _localizationService.GetResource("Admin.Catalog.Products.List.SearchPublished.UnpublishedOnly"), Value = "2" });

                response = request.CreateResponse<ProductListVM>(HttpStatusCode.OK, model);

                return response;
            });
        }

        [HttpPost]
        [Route("", Name = "ProductList")]
        public HttpResponseMessage ProductList(HttpRequestMessage request, ProductListVM model, DataSourceRequest command)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
                {
                    //a vendor should have access only to his products
                    if (_workContext.CurrentVendor != null)
                    {
                        model.SearchVendorId = _workContext.CurrentVendor.Id;
                    }

                    var categoryIds = new List<int> { model.SearchCategoryId };
                    //include subcategories
                    if (model.SearchIncludeSubCategories && model.SearchCategoryId > 0)
                        categoryIds.AddRange(GetChildCategoryIds(model.SearchCategoryId));

                    //0 - all (according to "ShowHidden" parameter)
                    //1 - published only
                    //2 - unpublished only
                    bool? overridePublished = null;
                    if (model.SearchPublishedId == 1)
                        overridePublished = true;
                    else if (model.SearchPublishedId == 2)
                        overridePublished = false;

                    var products = _productService.SearchProducts(
                        categoryIds: categoryIds,
                        manufacturerId: model.SearchManufacturerId,
                        storeId: model.SearchStoreId,
                        vendorId: model.SearchVendorId,
                        warehouseId: model.SearchWarehouseId,
                        productType: model.SearchProductTypeId > 0 ? (ProductType?)model.SearchProductTypeId : null,
                        keywords: model.SearchProductName,
                        pageIndex: command.Page - 1,
                        pageSize: command.PageSize,
                        showHidden: true,
                        overridePublished: overridePublished
                    );
                    var gridModel = new DataSourceResult();
                    gridModel.Data = products.Select(x =>
                    {
                        var productModel = x.ToModel();
                        //little performance optimization: ensure that "FullDescription" is not returned
                        productModel.FullDescription = "";

                        //picture
                        var defaultProductPicture = _pictureService.GetPicturesByProductId(x.Id, 1).FirstOrDefault();
                        productModel.PictureThumbnailUrl = _pictureService.GetPictureUrl(defaultProductPicture, 75, true);
                        //product type
                        productModel.ProductTypeName = x.ProductType.GetLocalizedEnum(_localizationService, _workContext);
                        //friendly stock qantity
                        //if a simple product AND "manage inventory" is "Track inventory", then display
                        if (x.ProductType == ProductType.SimpleProduct && x.ManageInventoryMethod == ManageInventoryMethod.ManageStock)
                            productModel.StockQuantityStr = x.GetTotalStockQuantity().ToString();
                        return productModel;
                    });
                    gridModel.Total = products.TotalCount;

                    response = request.CreateResponse<DataSourceResult>(HttpStatusCode.OK, gridModel);
                }
                return response;
            });
        }

        [HttpGet]
        [Route("{id:int}", Name = "GetProductById")]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No item found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
                {
                    var product = _productService.GetProductById(id);
                    bool isUnauthorizedVendor = _workContext.CurrentVendor != null && product.VendorId != _workContext.CurrentVendor.Id;
                    if ((product != null && !product.Deleted) || isUnauthorizedVendor)
                    {
                        var model = product.ToModel();
                        PrepareProductModel(model, product, false, false);
                        PrepareAclModel(model, product, false);
                        PrepareStoresMappingModel(model, product, false);
                        PrepareCategoryMappingModel(model, product, false);
                        PrepareManufacturerMappingModel(model, product, false);
                        PrepareDiscountMappingModel(model, product, false);
                        response = request.CreateResponse<ProductVM>(HttpStatusCode.OK, model);
                    }
                }
                return response;
            });
        }

        [HttpGet]
        [Route("GoToSku/{sku}")]
        public HttpResponseMessage GoToSku(HttpRequestMessage request, string sku)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
                {
                    //try to load a product entity
                    var product = _productService.GetProductBySku(sku);

                    //if not found, then try to load a product attribute combination
                    if (product == null)
                    {
                        var combination = _productAttributeService.GetProductAttributeCombinationBySku(sku);
                        if (combination != null)
                        {
                            product = combination.Product;
                        }
                    }
                    if (product != null)
                    {
                        string uri = Url.Link("GetProductById", new { id = product.Id });
                        response.Headers.Location = new Uri(uri);
                    }
                }
                return response;
            });
        }

        [HttpGet]
        [Route("GetProductAddData")]
        public HttpResponseMessage GetProductAddData(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
                {
                    //validate maximum number of products per vendor
                    if (_vendorSettings.MaximumProductNumber > 0 &&
                        _workContext.CurrentVendor != null &&
                        _productService.GetNumberOfProductsByVendorId(_workContext.CurrentVendor.Id) >= _vendorSettings.MaximumProductNumber)
                    {
                        LogError(string.Format(_localizationService.GetResource("Admin.Catalog.Products.ExceededMaximumNumber"), _vendorSettings.MaximumProductNumber));

                        string uri = Url.Link("DefaultProductPageLoad", null);
                        response.Headers.Location = new Uri(uri);
                    }
                    else
                    {
                        var model = new ProductVM();

                        PrepareProductModel(model, null, true, true);
                        PrepareAclModel(model, null, false);
                        PrepareStoresMappingModel(model, null, false);
                        PrepareCategoryMappingModel(model, null, false);
                        PrepareManufacturerMappingModel(model, null, false);
                        PrepareDiscountMappingModel(model, null, false);

                        response = request.CreateResponse<ProductVM>(HttpStatusCode.OK, model);
                    }
                }
                return response;
            });
        }

        [HttpPost]
        [Route("Add")]
        public HttpResponseMessage Add(HttpRequestMessage request, ProductVM model, bool continueEditing = false)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
                {
                    //validate maximum number of products per vendor
                    if (_vendorSettings.MaximumProductNumber > 0 &&
                        _workContext.CurrentVendor != null &&
                        _productService.GetNumberOfProductsByVendorId(_workContext.CurrentVendor.Id) >= _vendorSettings.MaximumProductNumber)
                    {
                        LogError(string.Format(_localizationService.GetResource("Admin.Catalog.Products.ExceededMaximumNumber"), _vendorSettings.MaximumProductNumber));

                        string uri = Url.Link("DefaultProductPageLoad", null);
                        response.Headers.Location = new Uri(uri);
                        return response;
                    }

                    if (ModelState.IsValid)
                    {
                        //a vendor should have access only to his products
                        if (_workContext.CurrentVendor != null)
                        {
                            model.VendorId = _workContext.CurrentVendor.Id;
                        }

                        //vendors cannot edit "Show on home page" property
                        if (_workContext.CurrentVendor != null && model.ShowOnHomePage)
                        {
                            model.ShowOnHomePage = false;
                        }

                        //product
                        var product = model.ToEntity();
                        product.CreatedOnUtc = DateTime.UtcNow;
                        product.UpdatedOnUtc = DateTime.UtcNow;
                        _productService.InsertProduct(product);
                        //search engine name
                        model.SeName = product.ValidateSeName(model.SeName, product.Name, true, _urlRecordService, _seoSettings);
                        _urlRecordService.SaveSlug(product, model.SeName, 0);
                        //locales
                        //UpdateLocales(product, model);
                        //categories
                        SaveCategoryMappings(product, model);
                        //manufacturers
                        SaveManufacturerMappings(product, model);
                        //ACL (customer roles)
                        SaveProductAcl(product, model);
                        //stores
                        SaveStoreMappings(product, model);
                        //discounts
                        SaveDiscountMappings(product, model);
                        //tags
                        _productTagService.UpdateProductTags(product, ParseProductTags(model.ProductTags));
                        //warehouses
                        //SaveProductWarehouseInventory(product, model);

                        //quantity change history
                        _productService.AddStockQuantityHistoryEntry(product, product.StockQuantity, product.StockQuantity, product.WarehouseId,
                            _localizationService.GetResource("Admin.StockQuantityHistory.Messages.Edit"));

                        //activity log
                        _customerActivityService.InsertActivity("AddNewProduct", _localizationService.GetResource("ActivityLog.AddNewProduct"), product.Name);

                        _unitOfWork.Commit();
                        response = request.CreateResponse<Product>(HttpStatusCode.Created, product);
                        if (continueEditing)
                        {
                            // Generate a link to the update item and set the Location header in the response.
                            string uri = Url.Link("GetProductById", new { id = product.Id });
                            response.Headers.Location = new Uri(uri);
                        }
                        else
                        {
                            string uri = Url.Link("DefaultProductPageLoad", null);
                            response.Headers.Location = new Uri(uri);
                        }
                    }
                    else
                    {
                        //If we got this far, something failed, redisplay form
                        PrepareProductModel(model, null, false, true);
                        PrepareAclModel(model, null, true);
                        PrepareStoresMappingModel(model, null, true);
                        PrepareCategoryMappingModel(model, null, true);
                        PrepareManufacturerMappingModel(model, null, true);
                        PrepareDiscountMappingModel(model, null, true);

                        response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                    }
                }
                return response;
            });
        }

        [HttpPost]
        [Route("Edit")]
        public HttpResponseMessage Edit(HttpRequestMessage request, ProductVM model, bool continueEditing = false)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.NotFound);
                if (_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
                {
                    var product = _productService.GetProductById(model.Id);
                    if ((product == null || product.Deleted) || _workContext.CurrentVendor != null && product.VendorId != _workContext.CurrentVendor.Id)
                    {
                        string uri = Url.Link("DefaultProductPageLoad", null);
                        response.Headers.Location = new Uri(uri);
                        return response;
                    }
                    //check if the product quantity has been changed while we were editing the product
                    //and if it has been changed then we show error notification
                    //and redirect on the editing page without data saving
                    if (product.StockQuantity != model.LastStockQuantity)
                    {
                        LogError(string.Format(_localizationService.GetResource("Admin.Catalog.Products.Fields.StockQuantity.ChangedWarning")));

                        string uri = Url.Link("GetProductById", new { id = product.Id });
                        response.Headers.Location = new Uri(uri);
                        return response;
                    }

                    if (ModelState.IsValid)
                    {
                        //a vendor should have access only to his products
                        if (_workContext.CurrentVendor != null)
                        {
                            model.VendorId = _workContext.CurrentVendor.Id;
                        }

                        //we do not validate maximum number of products per vendor when editing existing products (only during creation of new products)

                        //vendors cannot edit "Show on home page" property
                        if (_workContext.CurrentVendor != null && model.ShowOnHomePage != product.ShowOnHomePage)
                        {
                            model.ShowOnHomePage = product.ShowOnHomePage;
                        }
                        //some previously used values
                        var prevTotalStockQuantity = product.GetTotalStockQuantity();
                        var prevDownloadId = product.DownloadId;
                        var prevSampleDownloadId = product.SampleDownloadId;
                        var previousStockQuantity = product.StockQuantity;
                        var previousWarehouseId = product.WarehouseId;

                        //product
                        product = model.ToEntity(product);

                        product.UpdatedOnUtc = DateTime.UtcNow;
                        _productService.UpdateProduct(product);
                        //search engine name
                        model.SeName = product.ValidateSeName(model.SeName, product.Name, true, _urlRecordService, _seoSettings);
                        _urlRecordService.SaveSlug(product, model.SeName, 0);
                        //locales
                        //UpdateLocales(product, model);
                        //tags
                        _productTagService.UpdateProductTags(product, ParseProductTags(model.ProductTags));
                        //warehouses
                        //SaveProductWarehouseInventory(product, model);
                        //categories
                        SaveCategoryMappings(product, model);
                        //manufacturers
                        SaveManufacturerMappings(product, model);
                        //ACL (customer roles)
                        SaveProductAcl(product, model);
                        //stores
                        SaveStoreMappings(product, model);
                        //discounts
                        SaveDiscountMappings(product, model);
                        //picture seo names
                        UpdatePictureSeoNames(product);

                        //back in stock notifications
                        if (product.ManageInventoryMethod == ManageInventoryMethod.ManageStock &&
                            product.BackorderMode == BackorderMode.NoBackorders &&
                            product.AllowBackInStockSubscriptions &&
                            product.GetTotalStockQuantity() > 0 &&
                            prevTotalStockQuantity <= 0 &&
                            product.Published &&
                            !product.Deleted)
                        {
                            _backInStockSubscriptionService.SendNotificationsToSubscribers(product);
                        }
                        //delete an old "download" file (if deleted or updated)
                        if (prevDownloadId > 0 && prevDownloadId != product.DownloadId)
                        {
                            var prevDownload = _downloadService.GetDownloadById(prevDownloadId);
                            if (prevDownload != null)
                                _downloadService.DeleteDownload(prevDownload);
                        }
                        //delete an old "sample download" file (if deleted or updated)
                        if (prevSampleDownloadId > 0 && prevSampleDownloadId != product.SampleDownloadId)
                        {
                            var prevSampleDownload = _downloadService.GetDownloadById(prevSampleDownloadId);
                            if (prevSampleDownload != null)
                                _downloadService.DeleteDownload(prevSampleDownload);
                        }

                        //quantity change history
                        if (previousWarehouseId != product.WarehouseId)
                        {
                            //warehouse is changed 
                            //compose a message
                            var oldWarehouseMessage = string.Empty;
                            if (previousWarehouseId > 0)
                            {
                                var oldWarehouse = _shippingService.GetWarehouseById(previousWarehouseId);
                                if (oldWarehouse != null)
                                    oldWarehouseMessage = string.Format(_localizationService.GetResource("Admin.StockQuantityHistory.Messages.EditWarehouse.Old"), oldWarehouse.Name);
                            }
                            var newWarehouseMessage = string.Empty;
                            if (product.WarehouseId > 0)
                            {
                                var newWarehouse = _shippingService.GetWarehouseById(product.WarehouseId);
                                if (newWarehouse != null)
                                    newWarehouseMessage = string.Format(_localizationService.GetResource("Admin.StockQuantityHistory.Messages.EditWarehouse.New"), newWarehouse.Name);
                            }
                            var message = string.Format(_localizationService.GetResource("Admin.StockQuantityHistory.Messages.EditWarehouse"), oldWarehouseMessage, newWarehouseMessage);

                            //record history
                            _productService.AddStockQuantityHistoryEntry(product, -previousStockQuantity, 0, previousWarehouseId, message);
                            _productService.AddStockQuantityHistoryEntry(product, product.StockQuantity, product.StockQuantity, product.WarehouseId, message);

                        }
                        else
                        {
                            _productService.AddStockQuantityHistoryEntry(product, product.StockQuantity - previousStockQuantity, product.StockQuantity,
                                product.WarehouseId, _localizationService.GetResource("Admin.StockQuantityHistory.Messages.Edit"));
                        }

                        //activity log
                        _customerActivityService.InsertActivity("EditProduct", _localizationService.GetResource("ActivityLog.EditProduct"), product.Name);

                        _unitOfWork.Commit();
                        response = request.CreateResponse<Product>(HttpStatusCode.Created, product);
                        if (continueEditing)
                        {
                            // Generate a link to the update item and set the Location header in the response.
                            string uri = Url.Link("GetProductById", new { id = product.Id });
                            response.Headers.Location = new Uri(uri);
                            return response;
                        }
                        else
                        {
                            string uri = Url.Link("DefaultProductPageLoad", null);
                            response.Headers.Location = new Uri(uri);
                            return response;
                        }
                    }
                    else
                    {
                        //If we got this far, something failed, redisplay form
                        PrepareProductModel(model, product, false, true);
                        PrepareAclModel(model, product, true);
                        PrepareStoresMappingModel(model, product, true);
                        PrepareCategoryMappingModel(model, product, true);
                        PrepareManufacturerMappingModel(model, product, true);
                        PrepareDiscountMappingModel(model, product, true);

                        response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
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
                if (_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
                {
                    var product = _productService.GetProductById(id);
                    if (product == null || (_workContext.CurrentVendor != null && product.VendorId != _workContext.CurrentVendor.Id))
                    {
                        string uri = Url.Link("DefaultProductPageLoad", null);
                        response.Headers.Location = new Uri(uri);
                        return response;
                    }

                    _productService.DeleteProduct(product);

                    //activity log
                    _customerActivityService.InsertActivity("DeleteProduct", _localizationService.GetResource("ActivityLog.DeleteProduct"), product.Name);

                    _unitOfWork.Commit();
                    response = request.CreateResponse<Product>(HttpStatusCode.OK, product);
                }
                return response;
            });
        }

        [HttpPost]
        [Route("DeleteSelected")]
        public HttpResponseMessage DeleteSelected(HttpRequestMessage request, ICollection<int> selectedIds)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
                {
                    if (selectedIds != null)
                    {
                        _productService.DeleteProducts(_productService.GetProductsByIds(selectedIds.ToArray()).Where(p => _workContext.CurrentVendor == null || p.VendorId == _workContext.CurrentVendor.Id).ToList());

                        _unitOfWork.Commit();
                    }
                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                return response;
            });
        }

        [HttpPost]
        [Route("CopyProduct")]
        public HttpResponseMessage CopyProduct(HttpRequestMessage request, ProductVM model)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
                {
                    var copyModel = model.CopyProductModel;
                    try
                    {
                        var originalProduct = _productService.GetProductById(copyModel.Id);

                        //a vendor should have access only to his products
                        if (_workContext.CurrentVendor != null && originalProduct.VendorId != _workContext.CurrentVendor.Id)
                        {
                            string link = Url.Link("DefaultProductPageLoad", null);
                            response.Headers.Location = new Uri(link);
                            return response;
                        }

                        var newProduct = _copyProductService.CopyProduct(originalProduct, copyModel.Name, copyModel.Published, copyModel.CopyImages);

                        _unitOfWork.Commit();
                        string uri = Url.Link("GetProductByID", new { id = newProduct.Id });
                        response.Headers.Location = new Uri(uri);
                    }
                    catch (Exception exc)
                    {
                        LogError(exc.Message);
                        string uri = Url.Link("GetProductByID", new { id = copyModel.Id });
                        response.Headers.Location = new Uri(uri);

                    }
                }
                return response;
            });
        }

        #endregion

        #region Required products

        [HttpPost]
        [Route("{productIds}/ProductFriendlyNames", Name = "LoadProductFriendlyNames")]
        public HttpResponseMessage LoadProductFriendlyNames(HttpRequestMessage request, string productIds)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
                {
                    var result = "";

                    if (!String.IsNullOrWhiteSpace(productIds))
                    {
                        var ids = new List<int>();
                        var rangeArray = productIds
                            .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                            .Select(x => x.Trim())
                            .ToList();

                        foreach (string str1 in rangeArray)
                        {
                            int tmp1;
                            if (int.TryParse(str1, out tmp1))
                                ids.Add(tmp1);
                        }

                        var products = _productService.GetProductsByIds(ids.ToArray());
                        for (int i = 0; i <= products.Count - 1; i++)
                        {
                            result += products[i].Name;
                            if (i != products.Count - 1)
                                result += ", ";
                        }
                    }
                    response = request.CreateResponse(HttpStatusCode.OK, new { Text = result });
                }
                else
                {
                    response = request.CreateResponse(HttpStatusCode.Unauthorized, "Unauthorized user");
                }
                return response;
            });
        }

        [HttpGet]
        [Route("{productIdsInput}/RequiredProductAddPopup", Name = "RequiredProductAddPopup")]
        public HttpResponseMessage RequiredProductAddPopup(HttpRequestMessage request, string productIdsInput)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
                {

                    var model = new ProductVM.AddRequiredProductVM();
                    //a vendor should have access only to his products
                    model.IsLoggedInAsVendor = _workContext.CurrentVendor != null;

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
                    model.AvailableProductTypes = ProductType.SimpleProduct.ToSelectList(_localizationService, _workContext, false).ToList();
                    model.AvailableProductTypes.Insert(0, new System.Web.Mvc.SelectListItem { Text = allText, Value = "0" });


                    response = request.CreateResponse<ProductVM.AddRequiredProductVM>(HttpStatusCode.OK, model);
                }
                return response;
            });
        }

        [HttpPost]
        [Route("{pageIndex:int=0}/{pageSize:int=2147493645}/RequiredProductAddPopupList", Name = "RequiredProductAddPopupList")]
        public HttpResponseMessage RequiredProductAddPopupList(HttpRequestMessage request, ProductVM.AddRequiredProductVM model, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
                {
                    //a vendor should have access only to his products
                    if (_workContext.CurrentVendor != null)
                    {
                        model.SearchVendorId = _workContext.CurrentVendor.Id;
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

        #endregion

        #region ProductTag
        [HttpGet]
        [Route("ProductTag/{pageIndex:int=0}/{pageSize:int=2147483647}")]
        public HttpResponseMessage GetProductTag(HttpRequestMessage request, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageProductTags))
                {
                    var tags = _productTagService.GetAllProductTags(pageIndex, pageSize)
                   //order by product count
                   .OrderByDescending(x => _productTagService.GetProductCount(x.Id, 0))
                   .Select(x => new ProductTagVM
                   {
                       Id = x.Id,
                       Name = x.Name,
                       ProductCount = _productTagService.GetProductCount(x.Id, 0)
                   }).ToList();

                    if (tags != null)
                    {
                        var gridModel = new DataSourceResult
                        {
                            Data = tags.Select(t => t),
                            Total = tags.Count
                        };

                        response = request.CreateResponse<DataSourceResult>(HttpStatusCode.OK, gridModel);
                    }
                }
                return response;
            });
        }

        [HttpGet]
        [Route("ProductTag/{Id}", Name = "GetProductTagById")]
        public HttpResponseMessage ProductTagByID(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No item found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageProductTags))
                {
                    var tag = _productTagService.GetProductTagById(id);
                    if (tag != null)
                    {
                        var model = new ProductTagVM
                        {
                            Id = tag.Id,
                            Name = tag.Name,
                            ProductCount = _productTagService.GetProductCount(tag.Id, 0)
                        };

                        response = request.CreateResponse<ProductTagVM>(HttpStatusCode.OK, model);
                    }
                }
                return response;
            });
        }

        [HttpPost]
        [Route("ProductTag/Add")]
        public HttpResponseMessage AddProductTag(HttpRequestMessage request, ProductTagVM model)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (_permissionService.Authorize(StandardPermissionProvider.ManageProductTags))
                {
                    if (!ModelState.IsValid)
                    {
                        response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                    }
                    else
                    {
                        var productTag = new ProductTag();
                        productTag.Id = model.Id;
                        productTag.Name = model.Name;
                        _productTagService.InsertProductTag(productTag);

                        _unitOfWork.Commit();
                        response = request.CreateResponse<ProductTag>(HttpStatusCode.Created, productTag);
                    }
                }
                return response;
            });
        }

        [HttpPost]
        [Route("ProductTag/Update")]
        public HttpResponseMessage UpdateProductTag(HttpRequestMessage request, ProductTagVM model)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (_permissionService.Authorize(StandardPermissionProvider.ManageProductTags))
                {
                    if (!ModelState.IsValid)
                    {
                        response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                    }
                    else
                    {
                        var productTag = _productTagService.GetProductTagById(model.Id);
                        productTag.Name = model.Name;
                        _productTagService.UpdateProductTag(productTag);

                        _unitOfWork.Commit();
                        response = request.CreateResponse<ProductTagVM>(HttpStatusCode.OK, model);
                    }
                }
                return response;
            });
        }

        [HttpPost]
        [Route("ProductTag/Delete/{id:int}")]
        public HttpResponseMessage DeleteProductTag(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.NotFound);
                if (_permissionService.Authorize(StandardPermissionProvider.ManageProductTags))
                {
                    if (!ModelState.IsValid)
                    {
                        response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                    }
                    else
                    {
                        var tag = _productTagService.GetProductTagById(id);
                        if (tag != null)
                        {
                            _productTagService.DeleteProductTag(tag);

                            _unitOfWork.Commit();
                            response = request.CreateResponse<ProductTag>(HttpStatusCode.OK, tag);
                        }
                    }
                }
                return response;
            });
        }
        #endregion

        #region Related products

        [HttpPost]
        [Route("{productId:int}/relatedProducts", Name = "RelatedProductList")]
        public HttpResponseMessage RelatedProductList(HttpRequestMessage request, int productId)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
                {
                    //a vendor should have access only to his products
                    if (_workContext.CurrentVendor != null)
                    {
                        var product = _productService.GetProductById(productId);
                        if (product != null && product.VendorId != _workContext.CurrentVendor.Id)
                        {
                            response = request.CreateErrorResponse(HttpStatusCode.NotFound, "This is not your product");
                            return response;
                        }
                    }

                    var relatedProducts = _productService.GetRelatedProductsByProductId1(productId, true);
                    var relatedProductsModel = relatedProducts
                        .Select(x => new ProductVM.RelatedProductVM
                        {
                            Id = x.Id,
                            //ProductId1 = x.ProductId1,
                            ProductId2 = x.ProductId2,
                            Product2Name = _productService.GetProductById(x.ProductId2).Name,
                            DisplayOrder = x.DisplayOrder
                        })
                        .ToList();

                    var gridModel = new DataSourceResult
                    {
                        Data = relatedProductsModel,
                        Total = relatedProductsModel.Count
                    };


                    response = request.CreateResponse<DataSourceResult>(HttpStatusCode.OK, gridModel);
                }
                return response;
            });
        }

        [HttpPost]
        [Route("RelatedProduct/Update")]
        public HttpResponseMessage RelatedProductUpdate(HttpRequestMessage request, ProductVM.RelatedProductVM model)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
                {
                    var relatedProduct = _productService.GetRelatedProductById(model.Id);
                    if (relatedProduct == null)
                        throw new ArgumentException("No related product found with the specified id");

                    //a vendor should have access only to his products
                    if (_workContext.CurrentVendor != null)
                    {
                        var product = _productService.GetProductById(relatedProduct.ProductId1);
                        if (product != null && product.VendorId != _workContext.CurrentVendor.Id)
                        {
                            response = request.CreateErrorResponse(HttpStatusCode.NotFound, "This is not your product");
                            return response;
                        }
                    }

                    relatedProduct.DisplayOrder = model.DisplayOrder;
                    _productService.UpdateRelatedProduct(relatedProduct);

                    _unitOfWork.Commit();
                    response = request.CreateResponse<RelatedProduct>(HttpStatusCode.OK, relatedProduct);
                }
                return response;
            });
        }

        [HttpPost]
        [Route("RelatedProduct/Delete")]
        public HttpResponseMessage RelatedProductDelete(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
                {
                    var relatedProduct = _productService.GetRelatedProductById(id);
                    if (relatedProduct == null)
                        throw new ArgumentException("No related product found with the specified id");

                    var productId = relatedProduct.ProductId1;

                    //a vendor should have access only to his products
                    if (_workContext.CurrentVendor != null)
                    {
                        var product = _productService.GetProductById(productId);
                        if (product != null && product.VendorId != _workContext.CurrentVendor.Id)
                        {
                            response = request.CreateErrorResponse(HttpStatusCode.NotFound, "This is not your product");
                            return response;
                        }
                    }

                    _productService.DeleteRelatedProduct(relatedProduct);
                    _unitOfWork.Commit();
                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                return response;
            });
        }

        [HttpGet]
        [Route("{productId:int}/RelatedProductAddPopup", Name = "RelatedProductAddPopup")]
        public HttpResponseMessage RelatedProductAddPopup(HttpRequestMessage request, int productId)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                if (_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
                {
                    var model = new ProductVM.AddRelatedProductVM();
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

                    //product types
                    model.AvailableProductTypes = ProductType.SimpleProduct.ToSelectList(_localizationService, _workContext, false).ToList();
                    model.AvailableProductTypes.Insert(0, new System.Web.Mvc.SelectListItem { Text = allText, Value = "0" });

                    response = request.CreateResponse<ProductVM.AddRelatedProductVM>(HttpStatusCode.OK, model);
                }
                return response;
            });
        }

        [HttpPost]
        [Route("{pageIndex:int=0}/{pageSize:int=2147493645}/RelatedProductAddPopupList", Name = "RelatedProductAddPopupList")]
        public HttpResponseMessage RelatedProductAddPopupList(HttpRequestMessage request, ProductVM.AddRelatedProductVM model, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
                {
                    //a vendor should have access only to his products
                    if (_workContext.CurrentVendor != null)
                    {
                        model.SearchVendorId = _workContext.CurrentVendor.Id;
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
        [Route("RelatedProduct/Add")]
        public HttpResponseMessage RelatedProductAddPopup(HttpRequestMessage request, ProductVM.AddRelatedProductVM model)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
                {
                    if (model.SelectedProductIds != null)
                    {
                        foreach (int id in model.SelectedProductIds)
                        {
                            var product = _productService.GetProductById(id);
                            if (product != null)
                            {
                                //a vendor should have access only to his products
                                if (_workContext.CurrentVendor != null && product.VendorId != _workContext.CurrentVendor.Id)
                                    continue;

                                var existingRelatedProducts = _productService.GetRelatedProductsByProductId1(model.ProductId);
                                if (existingRelatedProducts.FindRelatedProduct(model.ProductId, id) == null)
                                {
                                    _productService.InsertRelatedProduct(
                                        new RelatedProduct
                                        {
                                            ProductId1 = model.ProductId,
                                            ProductId2 = id,
                                            DisplayOrder = 1
                                        });
                                    _unitOfWork.Commit();
                                }
                            }
                        }
                    }

                    //a vendor should have access only to his products
                    model.IsLoggedInAsVendor = _workContext.CurrentVendor != null;

                    response = request.CreateResponse<ProductVM.AddRelatedProductVM>(HttpStatusCode.OK, model);
                }
                return response;
            });
        }

        #endregion

        #region Cross-sell products

        [HttpPost]
        [Route("{productId:int}/CrossSellProducts", Name = "CrossSellProductList")]
        public HttpResponseMessage CrossSellProductList(HttpRequestMessage request, DataSourceRequest command, int productId)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
                {
                    //a vendor should have access only to his products
                    if (_workContext.CurrentVendor != null)
                    {
                        var product = _productService.GetProductById(productId);
                        if (product != null && product.VendorId != _workContext.CurrentVendor.Id)
                        {
                            response = request.CreateErrorResponse(HttpStatusCode.NotFound, "This is not your product");
                            return response;
                        }
                    }

                    var crossSellProducts = _productService.GetCrossSellProductsByProductId1(productId, true);
                    var crossSellProductsModel = crossSellProducts
                        .Select(x => new ProductVM.CrossSellProductVM
                        {
                            Id = x.Id,
                            //ProductId1 = x.ProductId1,
                            ProductId2 = x.ProductId2,
                            Product2Name = _productService.GetProductById(x.ProductId2).Name,
                        })
                        .ToList();

                    var gridModel = new DataSourceResult
                    {
                        Data = crossSellProductsModel,
                        Total = crossSellProductsModel.Count()
                    };
                    response = request.CreateResponse<DataSourceResult>(HttpStatusCode.OK, gridModel);

                }
                return response;

            });
        }

        [HttpPost]
        [Route("CrossSellProduct/Delete")]
        public HttpResponseMessage CrossSellProductDelete(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
                {
                    var crossSellProduct = _productService.GetCrossSellProductById(id);
                    if (crossSellProduct == null)
                        throw new ArgumentException("No cross-sell product found with the specified id");

                    var productId = crossSellProduct.ProductId1;

                    //a vendor should have access only to his products
                    if (_workContext.CurrentVendor != null)
                    {
                        var product = _productService.GetProductById(productId);
                        if (product != null && product.VendorId != _workContext.CurrentVendor.Id)
                        {
                            response = request.CreateErrorResponse(HttpStatusCode.NotFound, "This is not your product");
                            return response;
                        }
                    }

                    _productService.DeleteCrossSellProduct(crossSellProduct);
                    _unitOfWork.Commit();
                    response = request.CreateResponse(HttpStatusCode.OK);

                }
                return response;
            });
        }

        [HttpGet]
        [Route("{productId:int}/CrossSellProductAddPopup", Name = "CrossSellProductAddPopup")]
        public HttpResponseMessage CrossSellProductAddPopup(HttpRequestMessage request, int productId)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
                {
                    var model = new ProductVM.AddCrossSellProductVM();
                    //a vendor should have access only to his products
                    model.IsLoggedInAsVendor = _workContext.CurrentVendor != null;
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
                    model.AvailableProductTypes = ProductType.SimpleProduct.ToSelectList(_localizationService, _workContext, false).ToList();
                    model.AvailableProductTypes.Insert(0, new System.Web.Mvc.SelectListItem { Text = allText, Value = "0" });

                    response = request.CreateResponse<ProductVM.AddCrossSellProductVM>(HttpStatusCode.OK, model);

                }
                return response;

            });

        }

        [HttpPost]
        [Route("{pageIndex:int=0}/{pageSize:int=2147493645}/CrossSellProductAddPopupList", Name = "CrossSellProductAddPopupList")]
        public HttpResponseMessage CrossSellProductAddPopupList(HttpRequestMessage request, ProductVM.AddCrossSellProductVM model, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
                {
                    //a vendor should have access only to his products
                    if (_workContext.CurrentVendor != null)
                    {
                        model.SearchVendorId = _workContext.CurrentVendor.Id;
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
        [Route("CrossSellProduct/Add")]
        public HttpResponseMessage CrossSellProductAddPopup(HttpRequestMessage request, ProductVM.AddCrossSellProductVM model)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
                {
                    if (model.SelectedProductIds != null)
                    {
                        foreach (int id in model.SelectedProductIds)
                        {
                            var product = _productService.GetProductById(id);
                            if (product != null)
                            {
                                //a vendor should have access only to his products
                                if (_workContext.CurrentVendor != null && product.VendorId != _workContext.CurrentVendor.Id)
                                    continue;

                                var existingCrossSellProducts = _productService.GetCrossSellProductsByProductId1(model.ProductId);
                                if (existingCrossSellProducts.FindCrossSellProduct(model.ProductId, id) == null)
                                {
                                    _productService.InsertCrossSellProduct(
                                        new CrossSellProduct
                                        {
                                            ProductId1 = model.ProductId,
                                            ProductId2 = id,
                                        });
                                    _unitOfWork.Commit();
                                }
                            }
                        }
                    }

                    //a vendor should have access only to his products
                    model.IsLoggedInAsVendor = _workContext.CurrentVendor != null;
                    response = request.CreateResponse<ProductVM.AddCrossSellProductVM>(HttpStatusCode.OK, model);

                }
                return response;

            });
        }

        #endregion

        #region Associated products

        [HttpPost]
        [Route("{productId:int}/AssociatedProducts", Name = "AssociatedProductList")]
        public HttpResponseMessage AssociatedProductList(HttpRequestMessage request, DataSourceRequest command, int productId)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
                {
                    //a vendor should have access only to his products
                    if (_workContext.CurrentVendor != null)
                    {
                        var product = _productService.GetProductById(productId);
                        if (product != null && product.VendorId != _workContext.CurrentVendor.Id)
                        {
                            response = request.CreateErrorResponse(HttpStatusCode.NotFound, "This is not your product");
                            return response;
                        }
                    }

                    //a vendor should have access only to his products
                    var vendorId = 0;
                    if (_workContext.CurrentVendor != null)
                    {
                        vendorId = _workContext.CurrentVendor.Id;
                    }

                    var associatedProducts = _productService.GetAssociatedProducts(parentGroupedProductId: productId,
                        vendorId: vendorId,
                        showHidden: true);
                    var associatedProductsModel = associatedProducts
                        .Select(x => new ProductVM.AssociatedProducVM
                        {
                            Id = x.Id,
                            ProductName = x.Name,
                            DisplayOrder = x.DisplayOrder
                        })
                        .ToList();

                    var gridModel = new DataSourceResult
                    {
                        Data = associatedProductsModel,
                        Total = associatedProductsModel.Count()
                    };

                    response = request.CreateResponse<DataSourceResult>(HttpStatusCode.OK, gridModel);

                }
                return response;

            });
        }

        [HttpPost]
        [Route("AssociatedProduct/Update")]
        public HttpResponseMessage AssociatedProductUpdate(HttpRequestMessage request, ProductVM.AssociatedProducVM model)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
                {
                    var associatedProduct = _productService.GetProductById(model.Id);
                    if (associatedProduct == null)
                        throw new ArgumentException("No associated product found with the specified id");

                    //a vendor should have access only to his products
                    if (_workContext.CurrentVendor != null && associatedProduct.VendorId != _workContext.CurrentVendor.Id)
                    {
                        response = request.CreateErrorResponse(HttpStatusCode.NotFound, "This is not your product");
                        return response;
                    }

                    associatedProduct.DisplayOrder = model.DisplayOrder;
                    _productService.UpdateProduct(associatedProduct);

                    _unitOfWork.Commit();
                    response = request.CreateResponse(HttpStatusCode.OK);

                }
                return response;

            });
        }

        [HttpPost]
        [Route("AssociatedProduct/Delete")]
        public HttpResponseMessage AssociatedProductDelete(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
                {
                    var product = _productService.GetProductById(id);
                    if (product == null)
                        throw new ArgumentException("No associated product found with the specified id");

                    //a vendor should have access only to his products
                    if (_workContext.CurrentVendor != null && product.VendorId != _workContext.CurrentVendor.Id)
                    {
                        response = request.CreateErrorResponse(HttpStatusCode.NotFound, "This is not your product");
                        return response;
                    }

                        product.ParentGroupedProductId = 0;
                    _productService.UpdateProduct(product);

                    _unitOfWork.Commit();
                    response = request.CreateResponse(HttpStatusCode.OK);

                }
                return response;

            });
        }

        [HttpGet]
        [Route("{productId:int}/AssociatedProductAddPopup", Name = "AssociatedProductAddPopup")]
        public HttpResponseMessage AssociatedProductAddPopup(HttpRequestMessage request, int productId)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
                {

                    var model = new ProductVM.AddAssociatedProductVM();
                    //a vendor should have access only to his products
                    model.IsLoggedInAsVendor = _workContext.CurrentVendor != null;
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
                    model.AvailableProductTypes = ProductType.SimpleProduct.ToSelectList(_localizationService, _workContext, false).ToList();
                    model.AvailableProductTypes.Insert(0, new System.Web.Mvc.SelectListItem { Text = allText, Value = "0" });

                    response = request.CreateResponse<ProductVM.AddAssociatedProductVM>(HttpStatusCode.OK, model);

                }
                return response;

            });
        }

        [HttpPost]
        [Route("{pageIndex:int=0}/{pageSize:int=2147493645}/AssociatedProductAddPopupList", Name = "AssociatedProductAddPopupList")]
        public HttpResponseMessage AssociatedProductAddPopupList(HttpRequestMessage request, ProductVM.AddAssociatedProductVM model, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
                {
                    //a vendor should have access only to his products
                    if (_workContext.CurrentVendor != null)
                    {
                        model.SearchVendorId = _workContext.CurrentVendor.Id;
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
                    gridModel.Data = products.Select(x =>
                    {
                        var productModel = x.ToModel();
                        //display already associated products
                        var parentGroupedProduct = _productService.GetProductById(x.ParentGroupedProductId);
                        if (parentGroupedProduct != null)
                        {
                            productModel.AssociatedToProductId = x.ParentGroupedProductId;
                            productModel.AssociatedToProductName = parentGroupedProduct.Name;
                        }
                        return productModel;
                    });
                    gridModel.Total = products.TotalCount;

                    response = request.CreateResponse<DataSourceResult>(HttpStatusCode.OK, gridModel);

                }
                return response;

            });
        }

        [HttpPost]
        [Route("AssociatedProduct/Add")]
        public HttpResponseMessage AssociatedProductAddPopup(HttpRequestMessage request, ProductVM.AddAssociatedProductVM model)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
                {
                    if (model.SelectedProductIds != null)
                    {
                        foreach (int id in model.SelectedProductIds)
                        {
                            var product = _productService.GetProductById(id);
                            if (product != null)
                            {
                                //a vendor should have access only to his products
                                if (_workContext.CurrentVendor != null && product.VendorId != _workContext.CurrentVendor.Id)
                                    continue;

                                product.ParentGroupedProductId = model.ProductId;
                                _productService.UpdateProduct(product);
                            }
                        }
                    }

                    //a vendor should have access only to his products
                    model.IsLoggedInAsVendor = _workContext.CurrentVendor != null;
                    response = request.CreateResponse<ProductVM.AddAssociatedProductVM>(HttpStatusCode.OK, model);

                }
                return response;

            });
        }

        #endregion

        #region Product pictures
        [HttpGet]
        [Route("{productId:int}/ProductPictureAdd/{pictureId:int}/{displayOrder:int}/{overrideAltAttribute}/{overrideTitleAttribute}", Name = "ProductPictureAdd")]
        public HttpResponseMessage ProductPictureAdd(HttpRequestMessage request, int pictureId, int displayOrder,
            string overrideAltAttribute, string overrideTitleAttribute, int productId)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
                {
                    if (pictureId == 0)
                        throw new ArgumentException();

                    var product = _productService.GetProductById(productId);
                    if (product == null)
                        throw new ArgumentException("No product found with the specified id");

                    //a vendor should have access only to his products
                    if (_workContext.CurrentVendor != null && product.VendorId != _workContext.CurrentVendor.Id)
                    {
                        string uri = Url.Link("DefaultProductPageLoad", null);
                        response.Headers.Location = new Uri(uri);
                        return response;
                    }

                    var picture = _pictureService.GetPictureById(pictureId);
                    if (picture == null)
                        throw new ArgumentException("No picture found with the specified id");

                    _pictureService.UpdatePicture(picture.Id,
                        _pictureService.LoadPictureBinary(picture),
                        picture.MimeType,
                        picture.SeoFilename,
                        overrideAltAttribute,
                        overrideTitleAttribute);

                    //_pictureService.SetSeoFilename(pictureId, _pictureService.GetPictureSeName(product.Name));

                    _productService.InsertProductPicture(new ProductPicture
                    {
                        PictureId = pictureId,
                        ProductId = productId,
                        DisplayOrder = displayOrder,
                    });
                    response = request.CreateResponse(HttpStatusCode.OK, new { Result = true });

                }
                return response;

            });

            
        }

        [HttpPost]
        [Route("{productId:int}/ProductPictureList", Name = "ProductPictureList")]
        public HttpResponseMessage ProductPictureList(HttpRequestMessage request, DataSourceRequest command, int productId)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
                {
                    //a vendor should have access only to his products
                    if (_workContext.CurrentVendor != null)
                    {
                        var product = _productService.GetProductById(productId);
                        if (product != null && product.VendorId != _workContext.CurrentVendor.Id)
                        {
                            response = request.CreateErrorResponse(HttpStatusCode.NotFound, "This is not your product");
                            return response;
                        }
                    }

                    var productPictures = _productService.GetProductPicturesByProductId(productId);
                    var productPicturesModel = productPictures
                        .Select(x =>
                        {
                            var picture = _pictureService.GetPictureById(x.PictureId);
                            if (picture == null)
                                throw new Exception("Picture cannot be loaded");
                            var m = new ProductVM.ProductPictureVM
                            {
                                Id = x.Id,
                                ProductId = x.ProductId,
                                PictureId = x.PictureId,
                                PictureUrl = _pictureService.GetPictureUrl(picture),
                                OverrideAltAttribute = picture.AltAttribute,
                                OverrideTitleAttribute = picture.TitleAttribute,
                                DisplayOrder = x.DisplayOrder
                            };
                            return m;
                        }).ToList();

                    var gridModel = new DataSourceResult
                    {
                        Data = productPicturesModel,
                        Total = productPicturesModel.Count
                    };

                    response = request.CreateResponse<DataSourceResult>(HttpStatusCode.OK, gridModel);

                }
                return response;

            });
        }

        [HttpPost]
        [Route("ProductPicture/Update")]
        public HttpResponseMessage ProductPictureUpdate(HttpRequestMessage request, ProductVM.ProductPictureVM model)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
                {
                    var productPicture = _productService.GetProductPictureById(model.Id);
                    if (productPicture == null)
                        throw new ArgumentException("No product picture found with the specified id");

                    //a vendor should have access only to his products
                    if (_workContext.CurrentVendor != null)
                    {
                        var product = _productService.GetProductById(productPicture.ProductId);
                        if (product != null && product.VendorId != _workContext.CurrentVendor.Id)
                        {
                            response = request.CreateErrorResponse(HttpStatusCode.NotFound, "This is not your product");
                            return response;
                        }
                    }

                    var picture = _pictureService.GetPictureById(productPicture.PictureId);
                    if (picture == null)
                        throw new ArgumentException("No picture found with the specified id");

                    _pictureService.UpdatePicture(picture.Id,
                        _pictureService.LoadPictureBinary(picture),
                        picture.MimeType,
                        picture.SeoFilename,
                        model.OverrideAltAttribute,
                        model.OverrideTitleAttribute);

                    productPicture.DisplayOrder = model.DisplayOrder;
                    _productService.UpdateProductPicture(productPicture);

                    _unitOfWork.Commit();
                    response = request.CreateResponse(HttpStatusCode.OK);

                }
                return response;

            });
        }

        [HttpPost]
        [Route("ProductPicture/Delete")]
        public HttpResponseMessage ProductPictureDelete(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
                {
                    var productPicture = _productService.GetProductPictureById(id);
                    if (productPicture == null)
                        throw new ArgumentException("No product picture found with the specified id");

                    var productId = productPicture.ProductId;

                    //a vendor should have access only to his products
                    if (_workContext.CurrentVendor != null)
                    {
                        var product = _productService.GetProductById(productId);
                        if (product != null && product.VendorId != _workContext.CurrentVendor.Id)
                        {
                            response = request.CreateErrorResponse(HttpStatusCode.NotFound, "This is not your product");
                            return response;
                        }
                    }
                    var pictureId = productPicture.PictureId;
                    _productService.DeleteProductPicture(productPicture);

                    var picture = _pictureService.GetPictureById(pictureId);
                    if (picture == null)
                        throw new ArgumentException("No picture found with the specified id");
                    _pictureService.DeletePicture(picture);

                    _unitOfWork.Commit();
                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                return response;

            });
        }

        #endregion

        #region Product specification attributes
        [HttpGet]
        [Route("{productId:int}/ProductSpecificationAttributeAdd/{attributeTypeId:int}/{specificationAttributeOptionId:int}/{customValue}/{overrideTitleAttribute}/{allowFiltering:bool}/{showOnProductPage:bool}/{displayOrder:int}", Name = "ProductSpecificationAttributeAdd")]
        public HttpResponseMessage ProductSpecificationAttributeAdd(HttpRequestMessage request, int attributeTypeId, int specificationAttributeOptionId,
            string customValue, bool allowFiltering, bool showOnProductPage, int displayOrder, int productId)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
                {
                    if (_workContext.CurrentVendor != null)
                    {
                        var product = _productService.GetProductById(productId);
                        if (product != null && product.VendorId != _workContext.CurrentVendor.Id)
                        {
                            string uri = Url.Link("DefaultProductPageLoad", null);
                            response.Headers.Location = new Uri(uri);
                            return response;
                        }
                    }

                    //we allow filtering only for "Option" attribute type
                    if (attributeTypeId != (int)SpecificationAttributeType.Option)
                    {
                        allowFiltering = false;
                    }
                    //we don't allow CustomValue for "Option" attribute type
                    if (attributeTypeId == (int)SpecificationAttributeType.Option)
                    {
                        customValue = null;
                    }

                    var psa = new ProductSpecificationAttribute
                    {
                        AttributeTypeId = attributeTypeId,
                        SpecificationAttributeOptionId = specificationAttributeOptionId,
                        ProductId = productId,
                        CustomValue = customValue,
                        AllowFiltering = allowFiltering,
                        ShowOnProductPage = showOnProductPage,
                        DisplayOrder = displayOrder,
                    };
                    _specificationAttributeService.InsertProductSpecificationAttribute(psa);

                    response = request.CreateResponse(HttpStatusCode.OK, new { Result = true });
                }
                return response;

            });

            //a vendor should have access only to his products
            
        }

        [HttpPost]
        [Route("{productId:int}/ProductSpecAttrList", Name = "ProductSpecAttrList")]
        public HttpResponseMessage ProductSpecAttrList(HttpRequestMessage request, DataSourceRequest command, int productId)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
                {
                    //a vendor should have access only to his products
                    if (_workContext.CurrentVendor != null)
                    {
                        var product = _productService.GetProductById(productId);
                        if (product != null && product.VendorId != _workContext.CurrentVendor.Id)
                        {
                            response = request.CreateErrorResponse(HttpStatusCode.NotFound, "This is not your product");
                            return response;
                        }
                    }

                    var productrSpecs = _specificationAttributeService.GetProductSpecificationAttributes(productId);

                    var productrSpecsModel = productrSpecs
                        .Select(x =>
                        {
                            var psaModel = new ProductSpecificationAttributeVM
                            {
                                Id = x.Id,
                                AttributeTypeId = x.AttributeTypeId,
                                AttributeTypeName = x.AttributeType.GetLocalizedEnum(_localizationService, _workContext),
                                AttributeId = x.SpecificationAttributeOption.SpecificationAttribute.Id,
                                AttributeName = x.SpecificationAttributeOption.SpecificationAttribute.Name,
                                AllowFiltering = x.AllowFiltering,
                                ShowOnProductPage = x.ShowOnProductPage,
                                DisplayOrder = x.DisplayOrder
                            };
                            switch (x.AttributeType)
                            {
                                case SpecificationAttributeType.Option:
                                    psaModel.ValueRaw = HttpUtility.HtmlEncode(x.SpecificationAttributeOption.Name);
                                    psaModel.SpecificationAttributeOptionId = x.SpecificationAttributeOptionId;
                                    break;
                                case SpecificationAttributeType.CustomText:
                                    psaModel.ValueRaw = HttpUtility.HtmlEncode(x.CustomValue);
                                    break;
                                case SpecificationAttributeType.CustomHtmlText:
                            //do not encode?
                            //psaModel.ValueRaw = x.CustomValue;
                            psaModel.ValueRaw = HttpUtility.HtmlEncode(x.CustomValue);
                                    break;
                                case SpecificationAttributeType.Hyperlink:
                                    psaModel.ValueRaw = x.CustomValue;
                                    break;
                                default:
                                    break;
                            }
                            return psaModel;
                        }).ToList();

                    var gridModel = new DataSourceResult
                    {
                        Data = productrSpecsModel,
                        Total = productrSpecsModel.Count()
                    };

                    response = request.CreateResponse<DataSourceResult>(HttpStatusCode.OK, gridModel);
                }
                return response;

            });
        }

        [HttpPost]
        [Route("ProductSpecAttr/Update")]
        public HttpResponseMessage ProductSpecAttrUpdate(HttpRequestMessage request, ProductSpecificationAttributeVM model)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
                {
                    var psa = _specificationAttributeService.GetProductSpecificationAttributeById(model.Id);
                    if (psa == null)
                    {
                        response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No product specification attribute found with the specified id");
                        return response;
                    }

                    var productId = psa.Product.Id;

                    //a vendor should have access only to his products
                    if (_workContext.CurrentVendor != null)
                    {
                        var product = _productService.GetProductById(productId);
                        if (product != null && product.VendorId != _workContext.CurrentVendor.Id)
                        {
                            response = request.CreateErrorResponse(HttpStatusCode.NotFound, "This is not your product");
                            return response;
                        }
                    }

                    //we allow filtering and change option only for "Option" attribute type
                    if (model.AttributeTypeId == (int)SpecificationAttributeType.Option)
                    {
                        psa.AllowFiltering = model.AllowFiltering;
                        int specificationAttributeOptionId;
                        if (int.TryParse(model.ValueRaw, out specificationAttributeOptionId))
                            psa.SpecificationAttributeOptionId = specificationAttributeOptionId;
                    }

                    psa.ShowOnProductPage = model.ShowOnProductPage;
                    psa.DisplayOrder = model.DisplayOrder;
                    _specificationAttributeService.UpdateProductSpecificationAttribute(psa);

                    _unitOfWork.Commit();
                    response = request.CreateResponse(HttpStatusCode.OK);

                }
                return response;

            });
        }

        [HttpPost]
        [Route("ProductSpecAttr/Delete")]
        public HttpResponseMessage ProductSpecAttrDelete(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
                {
                    var psa = _specificationAttributeService.GetProductSpecificationAttributeById(id);
                    if (psa == null)
                        throw new ArgumentException("No specification attribute found with the specified id");

                    var productId = psa.ProductId;

                    //a vendor should have access only to his products
                    if (_workContext.CurrentVendor != null)
                    {
                        var product = _productService.GetProductById(productId);
                        if (product != null && product.VendorId != _workContext.CurrentVendor.Id)
                        {
                            response = request.CreateErrorResponse(HttpStatusCode.NotFound, "This is not your product");
                            return response;
                        }
                    }

                    _specificationAttributeService.DeleteProductSpecificationAttribute(psa);

                    _unitOfWork.Commit();
                    response = request.CreateResponse(HttpStatusCode.OK);

                }
                return response;

            });

            
        }

        #endregion

        #region Purchased with order

        [HttpPost]
        [Route("{productId:int}/PurchasedWithOrders", Name = "PurchasedWithOrders")]
        public HttpResponseMessage PurchasedWithOrders(HttpRequestMessage request, DataSourceRequest command, int productId)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
                {
                    var product = _productService.GetProductById(productId);
                    if (product == null)
                        throw new ArgumentException("No product found with the specified id");

                    //a vendor should have access only to his products
                    if (_workContext.CurrentVendor != null && product.VendorId != _workContext.CurrentVendor.Id)
                    {

                        response = request.CreateErrorResponse(HttpStatusCode.NotFound, "This is not your product");
                        return response;
                    }

                    var orders = _orderService.SearchOrders(
                        productId: productId,
                        pageIndex: command.Page - 1,
                        pageSize: command.PageSize);
                    var gridModel = new DataSourceResult
                    {
                        Data = orders.Select(x =>
                        {
                            var store = _storeService.GetStoreById(x.StoreId);
                            return new OrderVM
                            {
                                Id = x.Id,
                                StoreName = store != null ? store.Name : "Unknown",
                                OrderStatus = x.OrderStatus.GetLocalizedEnum(_localizationService, _workContext),
                                PaymentStatus = x.PaymentStatus.GetLocalizedEnum(_localizationService, _workContext),
                                ShippingStatus = x.ShippingStatus.GetLocalizedEnum(_localizationService, _workContext),
                                CustomerEmail = x.BillingAddress.Email,
                                CreatedOn = _dateTimeHelper.ConvertToUserTime(x.CreatedOnUtc, DateTimeKind.Utc),
                                CustomOrderNumber = x.CustomOrderNumber
                            };
                        }),
                        Total = orders.TotalCount
                    };

                    response = request.CreateResponse<DataSourceResult>(HttpStatusCode.OK, gridModel);
                }
                return response;

            });
        }

        #endregion

        #region Low stock reports
        [HttpPost]
        [Route("LowStockReport")]
        public HttpResponseMessage LowStockReportList(HttpRequestMessage request, DataSourceRequest command)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
                {
                    int vendorId = 0;
                    //a vendor should have access only to his products
                    if (_workContext.CurrentVendor != null)
                        vendorId = _workContext.CurrentVendor.Id;

                    IList<Product> products = _productService.GetLowStockProducts(vendorId);
                    IList<ProductAttributeCombination> combinations = _productService.GetLowStockProductCombinations(vendorId);

                    var models = new List<LowStockProductVM>();
                    //products
                    foreach (var product in products)
                    {
                        var lowStockModel = new LowStockProductVM
                        {
                            Id = product.Id,
                            Name = product.Name,
                            ManageInventoryMethod = product.ManageInventoryMethod.GetLocalizedEnum(_localizationService, _workContext.WorkingLanguage.Id),
                            StockQuantity = product.GetTotalStockQuantity(),
                            Published = product.Published
                        };
                        models.Add(lowStockModel);
                    }
                    //combinations
                    foreach (var combination in combinations)
                    {
                        var product = combination.Product;
                        var lowStockModel = new LowStockProductVM
                        {
                            Id = product.Id,
                            Name = product.Name,
                            Attributes = _productAttributeFormatter.FormatAttributes(product, combination.AttributesXml, _workContext.CurrentCustomer, "<br />", true, true, true, false),
                            ManageInventoryMethod = product.ManageInventoryMethod.GetLocalizedEnum(_localizationService, _workContext.WorkingLanguage.Id),
                            StockQuantity = combination.StockQuantity,
                            Published = product.Published
                        };
                        models.Add(lowStockModel);
                    }
                    var gridModel = new DataSourceResult
                    {
                        Data = models.Skip((command.Page - 1) * command.PageSize).Take(command.PageSize),
                        Total = models.Count
                    };

                    response = request.CreateResponse<DataSourceResult>(HttpStatusCode.OK, gridModel);
                }
                return response;

            });
        }
        #endregion

        #region Stock quantity history

        [HttpPost]
        [Route("StockQuantityHistory")]
        public HttpResponseMessage StockQuantityHistory(HttpRequestMessage request, DataSourceRequest command, int productId, int warehouseId)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
                {
                    var product = _productService.GetProductById(productId);
                    if (product == null)
                        throw new ArgumentException("No product found with the specified id");

                    //a vendor should have access only to his products
                    if (_workContext.CurrentVendor != null && product.VendorId != _workContext.CurrentVendor.Id)
                    {
                        response = request.CreateErrorResponse(HttpStatusCode.NotFound, "This is not your product");
                        return response;
                    }

                    var stockQuantityHistory = _productService.GetStockQuantityHistory(product, warehouseId, pageIndex: command.Page - 1, pageSize: command.PageSize);

                    var gridModel = new DataSourceResult
                    {
                        Data = stockQuantityHistory.Select(historyEntry =>
                        {
                            var warehouseName = _localizationService.GetResource("Admin.Catalog.Products.Fields.Warehouse.None");
                            if (historyEntry.WarehouseId.HasValue)
                            {
                                var warehouse = _shippingService.GetWarehouseById(historyEntry.WarehouseId.Value);
                                warehouseName = warehouse != null ? warehouse.Name : "Deleted";
                            }

                            var attributesXml = string.Empty;
                            if (historyEntry.CombinationId.HasValue)
                            {
                                var combination = _productAttributeService.GetProductAttributeCombinationById(historyEntry.CombinationId.Value);
                                attributesXml = combination == null ? string.Empty :
                                    _productAttributeFormatter.FormatAttributes(historyEntry.Product, combination.AttributesXml, _workContext.CurrentCustomer, renderGiftCardAttributes: false);
                            }

                            return new ProductVM.StockQuantityHistoryVM
                            {
                                Id = historyEntry.Id,
                                QuantityAdjustment = historyEntry.QuantityAdjustment,
                                StockQuantity = historyEntry.StockQuantity,
                                Message = historyEntry.Message,
                                AttributeCombination = attributesXml,
                                WarehouseName = warehouseName,
                                CreatedOn = _dateTimeHelper.ConvertToUserTime(historyEntry.CreatedOnUtc, DateTimeKind.Utc)
                            };
                        }),
                        Total = stockQuantityHistory.TotalCount
                    };
                    response = request.CreateResponse<DataSourceResult>(HttpStatusCode.OK, gridModel);
                }
                return response;

            });
        }

        #endregion

        #region Product attributes

        [HttpPost]
        [Route("{productId:int}/ProductAttributeMappingList", Name = "ProductAttributeMappingList")]
        public HttpResponseMessage ProductAttributeMappingList(HttpRequestMessage request, DataSourceRequest command, int productId)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
                {
                    var product = _productService.GetProductById(productId);
                    if (product == null)
                        throw new ArgumentException("No product found with the specified id");

                    //a vendor should have access only to his products
                    if (_workContext.CurrentVendor != null && product.VendorId != _workContext.CurrentVendor.Id)
                    {
                        response = request.CreateErrorResponse(HttpStatusCode.NotFound, "This is not your product");
                        return response;
                    }

                    var attributes = _productAttributeService.GetProductAttributeMappingsByProductId(productId);
                    var attributesModel = attributes
                        .Select(x =>
                        {
                            var attributeModel = new ProductVM.ProductAttributeMappingVM
                            {
                                Id = x.Id,
                                ProductId = x.ProductId,
                                ProductAttribute = _productAttributeService.GetProductAttributeById(x.ProductAttributeId).Name,
                                ProductAttributeId = x.ProductAttributeId,
                                TextPrompt = x.TextPrompt,
                                IsRequired = x.IsRequired,
                                AttributeControlType = x.AttributeControlType.GetLocalizedEnum(_localizationService, _workContext),
                                AttributeControlTypeId = x.AttributeControlTypeId,
                                DisplayOrder = x.DisplayOrder
                            };


                            if (x.ShouldHaveValues())
                            {
                                attributeModel.ShouldHaveValues = true;
                                attributeModel.TotalValues = x.ProductAttributeValues.Count;
                            }

                            if (x.ValidationRulesAllowed())
                            {
                                var validationRules = new StringBuilder(string.Empty);
                                attributeModel.ValidationRulesAllowed = true;
                                if (x.ValidationMinLength != null)
                                    validationRules.AppendFormat("{0}: {1}<br />",
                                        _localizationService.GetResource("Admin.Catalog.Products.ProductAttributes.Attributes.ValidationRules.MinLength"),
                                        x.ValidationMinLength);
                                if (x.ValidationMaxLength != null)
                                    validationRules.AppendFormat("{0}: {1}<br />",
                                        _localizationService.GetResource("Admin.Catalog.Products.ProductAttributes.Attributes.ValidationRules.MaxLength"),
                                        x.ValidationMaxLength);
                                if (!string.IsNullOrEmpty(x.ValidationFileAllowedExtensions))
                                    validationRules.AppendFormat("{0}: {1}<br />",
                                        _localizationService.GetResource("Admin.Catalog.Products.ProductAttributes.Attributes.ValidationRules.FileAllowedExtensions"),
                                        HttpUtility.HtmlEncode(x.ValidationFileAllowedExtensions));
                                if (x.ValidationFileMaximumSize != null)
                                    validationRules.AppendFormat("{0}: {1}<br />",
                                        _localizationService.GetResource("Admin.Catalog.Products.ProductAttributes.Attributes.ValidationRules.FileMaximumSize"),
                                        x.ValidationFileMaximumSize);
                                if (!string.IsNullOrEmpty(x.DefaultValue))
                                    validationRules.AppendFormat("{0}: {1}<br />",
                                        _localizationService.GetResource("Admin.Catalog.Products.ProductAttributes.Attributes.ValidationRules.DefaultValue"),
                                        HttpUtility.HtmlEncode(x.DefaultValue));
                                attributeModel.ValidationRulesString = validationRules.ToString();
                            }


                    //currenty any attribute can have condition. why not?
                    attributeModel.ConditionAllowed = true;
                            var conditionAttribute = _productAttributeParser.ParseProductAttributeMappings(x.ConditionAttributeXml).FirstOrDefault();
                            var conditionValue = _productAttributeParser.ParseProductAttributeValues(x.ConditionAttributeXml).FirstOrDefault();
                            if (conditionAttribute != null && conditionValue != null)
                                attributeModel.ConditionString = string.Format("{0}: {1}",
                                    HttpUtility.HtmlEncode(conditionAttribute.ProductAttribute.Name),
                                    HttpUtility.HtmlEncode(conditionValue.Name));
                            else
                                attributeModel.ConditionString = string.Empty;
                            return attributeModel;
                        }).ToList();

                    var gridModel = new DataSourceResult
                    {
                        Data = attributesModel,
                        Total = attributesModel.Count()
                    };
                    response = request.CreateResponse<DataSourceResult>(HttpStatusCode.OK, gridModel);
                }
                return response;

            });
        }

        [HttpPost]
        [Route("ProductAttributeMapping/Add")]
        public HttpResponseMessage ProductAttributeMappingInsert(HttpRequestMessage request, ProductVM.ProductAttributeMappingVM model)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
                {
                    var product = _productService.GetProductById(model.ProductId);
                    if (product == null)
                        throw new ArgumentException("No product found with the specified id");

                    //a vendor should have access only to his products
                    if (_workContext.CurrentVendor != null && product.VendorId != _workContext.CurrentVendor.Id)
                    {
                        response = request.CreateErrorResponse(HttpStatusCode.NotFound, "This is not your product");
                        return response;
                    }

                    //ensure this attribute is not mapped yet
                    if (_productAttributeService.GetProductAttributeMappingsByProductId(product.Id).Any(x => x.ProductAttributeId == model.ProductAttributeId))
                    {
                        var errors = new DataSourceResult { Errors = _localizationService.GetResource("Admin.Catalog.Products.ProductAttributes.Attributes.AlreadyExists") };

                        response = request.CreateResponse(HttpStatusCode.Ambiguous, errors);
                        return response;
                    }

                    //insert mapping
                    var productAttributeMapping = new ProductAttributeMapping
                    {
                        ProductId = model.ProductId,
                        ProductAttributeId = model.ProductAttributeId,
                        TextPrompt = model.TextPrompt,
                        IsRequired = model.IsRequired,
                        AttributeControlTypeId = model.AttributeControlTypeId,
                        DisplayOrder = model.DisplayOrder
                    };
                    _productAttributeService.InsertProductAttributeMapping(productAttributeMapping);

                    //predefined values
                    var predefinedValues = _productAttributeService.GetPredefinedProductAttributeValues(model.ProductAttributeId);
                    foreach (var predefinedValue in predefinedValues)
                    {
                        var pav = new ProductAttributeValue
                        {
                            ProductAttributeMappingId = productAttributeMapping.Id,
                            AttributeValueType = AttributeValueType.Simple,
                            Name = predefinedValue.Name,
                            PriceAdjustment = predefinedValue.PriceAdjustment,
                            WeightAdjustment = predefinedValue.WeightAdjustment,
                            Cost = predefinedValue.Cost,
                            IsPreSelected = predefinedValue.IsPreSelected,
                            DisplayOrder = predefinedValue.DisplayOrder
                        };
                        _productAttributeService.InsertProductAttributeValue(pav);
                        //locales
                        var languages = _languageService.GetAllLanguages(true);
                        //localization
                        foreach (var lang in languages)
                        {
                            var name = predefinedValue.GetLocalized(x => x.Name, lang.Id, _languageService, _localizedEntityService, false, false);
                            if (!String.IsNullOrEmpty(name))
                                _localizedEntityService.SaveLocalizedValue(pav, x => x.Name, name, lang.Id);
                        }
                    }
                    _unitOfWork.Commit();
                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                return response;

            });
        }

        [HttpPost]
        [Route("ProductAttributeMapping/Update")]
        public HttpResponseMessage ProductAttributeMappingUpdate(HttpRequestMessage request, ProductVM.ProductAttributeMappingVM model)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
                {
                    var productAttributeMapping = _productAttributeService.GetProductAttributeMappingById(model.Id);
                    if (productAttributeMapping == null)
                        throw new ArgumentException("No product attribute mapping found with the specified id");

                    var product = _productService.GetProductById(productAttributeMapping.ProductId);
                    if (product == null)
                        throw new ArgumentException("No product found with the specified id");

                    //a vendor should have access only to his products
                    if (_workContext.CurrentVendor != null && product.VendorId != _workContext.CurrentVendor.Id)
                    {
                        response = request.CreateErrorResponse(HttpStatusCode.NotFound, "This is not your product");
                        return response;
                    }

                    productAttributeMapping.ProductAttributeId = model.ProductAttributeId;
                    productAttributeMapping.TextPrompt = model.TextPrompt;
                    productAttributeMapping.IsRequired = model.IsRequired;
                    productAttributeMapping.AttributeControlTypeId = model.AttributeControlTypeId;
                    productAttributeMapping.DisplayOrder = model.DisplayOrder;
                    _productAttributeService.UpdateProductAttributeMapping(productAttributeMapping);

                    _unitOfWork.Commit();
                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                return response;

            });
        }

        [HttpPost]
        [Route("ProductAttributeMapping/Delete")]
        public HttpResponseMessage ProductAttributeMappingDelete(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
                {
                    var productAttributeMapping = _productAttributeService.GetProductAttributeMappingById(id);
                    if (productAttributeMapping == null)
                        throw new ArgumentException("No product attribute mapping found with the specified id");

                    var productId = productAttributeMapping.ProductId;
                    var product = _productService.GetProductById(productId);
                    if (product == null)
                        throw new ArgumentException("No product found with the specified id");


                    //a vendor should have access only to his products
                    if (_workContext.CurrentVendor != null && product.VendorId != _workContext.CurrentVendor.Id)
                    {
                        response = request.CreateErrorResponse(HttpStatusCode.NotFound, "This is not your product");
                        return response;
                    }

                    _productAttributeService.DeleteProductAttributeMapping(productAttributeMapping);

                    _unitOfWork.Commit();
                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                return response;

            });
        }

        #endregion

        #region Product attributes. Validation rules
        [HttpGet]
        [Route("ProductAttributeValidationRulesPopup/{id:int}", Name = "ProductAttributeValidationRulesPopup")]
        public HttpResponseMessage ProductAttributeValidationRulesPopup(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
                {
                    var productAttributeMapping = _productAttributeService.GetProductAttributeMappingById(id);
                    if (productAttributeMapping == null)
                    {
                        //No attribute value found with the specified id
                        string uri = Url.Link("DefaultProductPageLoad", null);
                        response.Headers.Location = new Uri(uri);
                        return response;
                    }

                    var product = _productService.GetProductById(productAttributeMapping.ProductId);
                    if (product == null)
                        throw new ArgumentException("No product found with the specified id");

                    //a vendor should have access only to his products
                    if (_workContext.CurrentVendor != null && product.VendorId != _workContext.CurrentVendor.Id)
                    {
                        //No attribute value found with the specified id
                        string uri = Url.Link("DefaultProductPageLoad", null);
                        response.Headers.Location = new Uri(uri);
                        return response;
                    }

                    var model = new ProductVM.ProductAttributeMappingVM
                    {
                        //prepare only used properties
                        Id = productAttributeMapping.Id,
                        ValidationRulesAllowed = productAttributeMapping.ValidationRulesAllowed(),
                        AttributeControlTypeId = productAttributeMapping.AttributeControlTypeId,
                        ValidationMinLength = productAttributeMapping.ValidationMinLength,
                        ValidationMaxLength = productAttributeMapping.ValidationMaxLength,
                        ValidationFileAllowedExtensions = productAttributeMapping.ValidationFileAllowedExtensions,
                        ValidationFileMaximumSize = productAttributeMapping.ValidationFileMaximumSize,
                        DefaultValue = productAttributeMapping.DefaultValue,
                    };

                    response = request.CreateResponse<ProductVM.ProductAttributeMappingVM>(HttpStatusCode.OK, model);
                }
                return response;

            });
        }

        [HttpPost]
        [Route("ProductAttributeValidationRulesPopup")]
        public HttpResponseMessage ProductAttributeValidationRulesPopup(HttpRequestMessage request, ProductVM.ProductAttributeMappingVM model)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
                {
                    var productAttributeMapping = _productAttributeService.GetProductAttributeMappingById(model.Id);
                    if (productAttributeMapping == null)
                    {
                        //No attribute value found with the specified id
                        string uri = Url.Link("DefaultProductPageLoad", null);
                        response.Headers.Location = new Uri(uri);
                        return response;
                    }

                    var product = _productService.GetProductById(productAttributeMapping.ProductId);
                    if (product == null)
                        throw new ArgumentException("No product found with the specified id");

                    //a vendor should have access only to his products
                    if (_workContext.CurrentVendor != null && product.VendorId != _workContext.CurrentVendor.Id)
                    {
                        //No attribute value found with the specified id
                        string uri = Url.Link("DefaultProductPageLoad", null);
                        response.Headers.Location = new Uri(uri);
                        return response;
                    }

                    if (ModelState.IsValid)
                    {
                        productAttributeMapping.ValidationMinLength = model.ValidationMinLength;
                        productAttributeMapping.ValidationMaxLength = model.ValidationMaxLength;
                        productAttributeMapping.ValidationFileAllowedExtensions = model.ValidationFileAllowedExtensions;
                        productAttributeMapping.ValidationFileMaximumSize = model.ValidationFileMaximumSize;
                        productAttributeMapping.DefaultValue = model.DefaultValue;
                        _productAttributeService.UpdateProductAttributeMapping(productAttributeMapping);

                        _unitOfWork.Commit();
                    }
                    else
                    {
                        //If we got this far, something failed, redisplay form
                        model.ValidationRulesAllowed = productAttributeMapping.ValidationRulesAllowed();
                        model.AttributeControlTypeId = productAttributeMapping.AttributeControlTypeId;
                    }
                    response = request.CreateResponse<ProductVM.ProductAttributeMappingVM>(HttpStatusCode.OK, model);
                }
                return response;

            });
        }

        #endregion

        #region Product attributes. Condition
        [HttpGet]
        [Route("ProductAttributeConditionPopup/{productAttributeMappingId:int}", Name = "ProductAttributeConditionPopup")]
        public HttpResponseMessage ProductAttributeConditionPopup(HttpRequestMessage request, int productAttributeMappingId)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
                {
                    var productAttributeMapping = _productAttributeService.GetProductAttributeMappingById(productAttributeMappingId);
                    if (productAttributeMapping == null)
                    {
                        //No attribute value found with the specified id
                        string uri = Url.Link("DefaultProductPageLoad", null);
                        response.Headers.Location = new Uri(uri);
                        return response;
                    }

                    var product = _productService.GetProductById(productAttributeMapping.ProductId);
                    if (product == null)
                        throw new ArgumentException("No product found with the specified id");

                    //a vendor should have access only to his products
                    if (_workContext.CurrentVendor != null && product.VendorId != _workContext.CurrentVendor.Id)
                    {
                        //No attribute value found with the specified id
                        string uri = Url.Link("DefaultProductPageLoad", null);
                        response.Headers.Location = new Uri(uri);
                        return response;
                    }

                    var model = new ProductAttributeConditionVM();
                    model.ProductAttributeMappingId = productAttributeMapping.Id;
                    model.EnableCondition = !String.IsNullOrEmpty(productAttributeMapping.ConditionAttributeXml);


                    //pre-select attribute and values
                    var selectedPva = _productAttributeParser
                        .ParseProductAttributeMappings(productAttributeMapping.ConditionAttributeXml)
                        .FirstOrDefault();

                    var attributes = _productAttributeService.GetProductAttributeMappingsByProductId(product.Id)
                        //ignore non-combinable attributes (should have selectable values)
                        .Where(x => x.CanBeUsedAsCondition())
                        //ignore this attribute (it cannot depend on itself)
                        .Where(x => x.Id != productAttributeMapping.Id)
                        .ToList();
                    foreach (var attribute in attributes)
                    {
                        var attributeModel = new ProductAttributeConditionVM.ProductAttributeVM
                        {
                            Id = attribute.Id,
                            ProductAttributeId = attribute.ProductAttributeId,
                            Name = attribute.ProductAttribute.Name,
                            TextPrompt = attribute.TextPrompt,
                            IsRequired = attribute.IsRequired,
                            AttributeControlType = attribute.AttributeControlType
                        };

                        if (attribute.ShouldHaveValues())
                        {
                            //values
                            var attributeValues = _productAttributeService.GetProductAttributeValues(attribute.Id);
                            foreach (var attributeValue in attributeValues)
                            {
                                var attributeValueModel = new ProductAttributeConditionVM.ProductAttributeValueVM
                                {
                                    Id = attributeValue.Id,
                                    Name = attributeValue.Name,
                                    IsPreSelected = attributeValue.IsPreSelected
                                };
                                attributeModel.Values.Add(attributeValueModel);
                            }

                            //pre-select attribute and value
                            if (selectedPva != null && attribute.Id == selectedPva.Id)
                            {
                                //attribute
                                model.SelectedProductAttributeId = selectedPva.Id;

                                //values
                                switch (attribute.AttributeControlType)
                                {
                                    case AttributeControlType.DropdownList:
                                    case AttributeControlType.RadioList:
                                    case AttributeControlType.Checkboxes:
                                    case AttributeControlType.ColorSquares:
                                    case AttributeControlType.ImageSquares:
                                        {
                                            if (!String.IsNullOrEmpty(productAttributeMapping.ConditionAttributeXml))
                                            {
                                                //clear default selection
                                                foreach (var item in attributeModel.Values)
                                                    item.IsPreSelected = false;

                                                //select new values
                                                var selectedValues = _productAttributeParser.ParseProductAttributeValues(productAttributeMapping.ConditionAttributeXml);
                                                foreach (var attributeValue in selectedValues)
                                                    foreach (var item in attributeModel.Values)
                                                        if (attributeValue.Id == item.Id)
                                                            item.IsPreSelected = true;
                                            }
                                        }
                                        break;
                                    case AttributeControlType.ReadonlyCheckboxes:
                                    case AttributeControlType.TextBox:
                                    case AttributeControlType.MultilineTextbox:
                                    case AttributeControlType.Datepicker:
                                    case AttributeControlType.FileUpload:
                                    default:
                                        //these attribute types are supported as conditions
                                        break;
                                }
                            }
                        }

                        model.ProductAttributes.Add(attributeModel);
                    }
                    response = request.CreateResponse<ProductAttributeConditionVM>(HttpStatusCode.OK, model);
                }
                return response;

            }); 
        }

        [HttpPost]
        [Route("ProductAttributeConditionPopup")]
        public HttpResponseMessage ProductAttributeConditionPopup(HttpRequestMessage request, ProductAttributeConditionVM model, System.Web.Mvc.FormCollection form)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
                {
                    var productAttributeMapping = _productAttributeService.GetProductAttributeMappingById(model.ProductAttributeMappingId);
                    if (productAttributeMapping == null)
                    {
                        //No attribute value found with the specified id
                        string uri = Url.Link("DefaultProductPageLoad", null);
                        response.Headers.Location = new Uri(uri);
                        return response;
                    }

                    var product = _productService.GetProductById(productAttributeMapping.ProductId);
                    if (product == null)
                        throw new ArgumentException("No product found with the specified id");

                    //a vendor should have access only to his products
                    if (_workContext.CurrentVendor != null && product.VendorId != _workContext.CurrentVendor.Id)
                    {
                        //No attribute value found with the specified id
                        string uri = Url.Link("DefaultProductPageLoad", null);
                        response.Headers.Location = new Uri(uri);
                        return response;
                    }

                    string attributesXml = null;
                    if (model.EnableCondition)
                    {
                        var attribute = _productAttributeService.GetProductAttributeMappingById(model.SelectedProductAttributeId);
                        if (attribute != null)
                        {
                            string controlId = string.Format("product_attribute_{0}", attribute.Id);
                            switch (attribute.AttributeControlType)
                            {
                                case AttributeControlType.DropdownList:
                                case AttributeControlType.RadioList:
                                case AttributeControlType.ColorSquares:
                                case AttributeControlType.ImageSquares:
                                    {
                                        var ctrlAttributes = form[controlId];
                                        if (!String.IsNullOrEmpty(ctrlAttributes))
                                        {
                                            int selectedAttributeId = int.Parse(ctrlAttributes);
                                            if (selectedAttributeId > 0)
                                            {
                                                attributesXml = _productAttributeParser.AddProductAttribute(attributesXml,
                                                    attribute, selectedAttributeId.ToString());
                                            }
                                            else
                                            {
                                                //for conditions we should empty values save even when nothing is selected
                                                //otherwise "attributesXml" will be empty
                                                //hence we won't be able to find a selected attribute
                                                attributesXml = _productAttributeParser.AddProductAttribute(attributesXml,
                                                    attribute, "");
                                            }
                                        }
                                        else
                                        {
                                            //for conditions we should empty values save even when nothing is selected
                                            //otherwise "attributesXml" will be empty
                                            //hence we won't be able to find a selected attribute
                                            attributesXml = _productAttributeParser.AddProductAttribute(attributesXml,
                                                attribute, "");
                                        }
                                    }
                                    break;
                                case AttributeControlType.Checkboxes:
                                    {
                                        var cblAttributes = form[controlId];
                                        if (!String.IsNullOrEmpty(cblAttributes))
                                        {
                                            bool anyValueSelected = false;
                                            foreach (var item in cblAttributes.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                                            {
                                                int selectedAttributeId = int.Parse(item);
                                                if (selectedAttributeId > 0)
                                                {
                                                    attributesXml = _productAttributeParser.AddProductAttribute(attributesXml,
                                                        attribute, selectedAttributeId.ToString());
                                                    anyValueSelected = true;
                                                }
                                            }
                                            if (!anyValueSelected)
                                            {
                                                //for conditions we should save empty values even when nothing is selected
                                                //otherwise "attributesXml" will be empty
                                                //hence we won't be able to find a selected attribute
                                                attributesXml = _productAttributeParser.AddProductAttribute(attributesXml,
                                                    attribute, "");
                                            }
                                        }
                                        else
                                        {
                                            //for conditions we should save empty values even when nothing is selected
                                            //otherwise "attributesXml" will be empty
                                            //hence we won't be able to find a selected attribute
                                            attributesXml = _productAttributeParser.AddProductAttribute(attributesXml,
                                                    attribute, "");
                                        }
                                    }
                                    break;
                                case AttributeControlType.ReadonlyCheckboxes:
                                case AttributeControlType.TextBox:
                                case AttributeControlType.MultilineTextbox:
                                case AttributeControlType.Datepicker:
                                case AttributeControlType.FileUpload:
                                default:
                                    //these attribute types are supported as conditions
                                    break;
                            }
                        }
                    }
                    productAttributeMapping.ConditionAttributeXml = attributesXml;
                    _productAttributeService.UpdateProductAttributeMapping(productAttributeMapping);

                    _unitOfWork.Commit();
                    response = request.CreateResponse<ProductAttributeConditionVM>(HttpStatusCode.OK, model);
                }
                return response;

            });
            
        }

        #endregion

        #region Product editor settings

        [HttpPost]
        [Route("SaveProductEditorSettings", Name = "SaveProductEditorSettings")]
        public HttpResponseMessage SaveProductEditorSettings(HttpRequestMessage request, ProductVM model, string returnUrl = "")
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
                {
                    //vendors cannot manage these settings
                    if (_workContext.CurrentVendor != null)
                    {
                        //No attribute value found with the specified id
                        string uri = Url.Link("DefaultProductPageLoad", null);
                        response.Headers.Location = new Uri(uri);
                        return response;
                    }

                    var productEditorSettings = _settingService.LoadSetting<ProductEditorSettings>();
                    productEditorSettings = model.ProductEditorSettingsModel.ToEntity(productEditorSettings);
                    _settingService.SaveSetting(productEditorSettings);
                    _unitOfWork.Commit();

                    //product list
                    if (String.IsNullOrEmpty(returnUrl))
                    {
                        //No attribute value found with the specified id
                        string uri = Url.Link("DefaultProductPageLoad", null);
                        response.Headers.Location = new Uri(uri);
                        return response;
                    }
                    //prevent open redirection attack
                    //if (!Url.IsLocalUrl(returnUrl))
                    //{
                    //    //No attribute value found with the specified id
                    //    string uri = Url.Link("DefaultProductPageLoad", null);
                    //    response.Headers.Location = new Uri(uri);
                    //    return response;
                    //}
                    //return Redirect(returnUrl);
                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                return response;

            });

            
        }

        #endregion

        #region Tier prices

        [HttpPost]
        [Route("{productId:int}/TierPriceList", Name = "TierPriceList")]
        public HttpResponseMessage TierPriceList(HttpRequestMessage request, DataSourceRequest command, int productId)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
                {
                    var product = _productService.GetProductById(productId);
                    if (product == null)
                        throw new ArgumentException("No product found with the specified id");

                    //a vendor should have access only to his products
                    if (_workContext.CurrentVendor != null && product.VendorId != _workContext.CurrentVendor.Id)
                    {
                        response = request.CreateErrorResponse(HttpStatusCode.NotFound, "This is not your product");
                        return response;
                    }

                    var tierPricesModel = product.TierPrices.OrderBy(x => x.StoreId).ThenBy(x => x.Quantity).ThenBy(x => x.CustomerRoleId).Select(x =>
                    {
                        string storeName;
                        if (x.StoreId > 0)
                        {
                            var store = _storeService.GetStoreById(x.StoreId);
                            storeName = store != null ? store.Name : "Deleted";
                        }
                        else
                            storeName = _localizationService.GetResource("Admin.Catalog.Products.TierPrices.Fields.Store.All");

                        return new ProductVM.TierPriceVM
                        {
                            Id = x.Id,
                            StoreId = x.StoreId,
                            Store = storeName,
                            CustomerRole = x.CustomerRoleId.HasValue ? _customerService.GetCustomerRoleById(x.CustomerRoleId.Value).Name : _localizationService.GetResource("Admin.Catalog.Products.TierPrices.Fields.CustomerRole.All"),
                            ProductId = x.ProductId,
                            CustomerRoleId = x.CustomerRoleId.HasValue ? x.CustomerRoleId.Value : 0,
                            Quantity = x.Quantity,
                            Price = x.Price,
                            StartDateTimeUtc = x.StartDateTimeUtc,
                            EndDateTimeUtc = x.EndDateTimeUtc
                        };
                    }).ToList();

                    var gridModel = new DataSourceResult
                    {
                        Data = tierPricesModel,
                        Total = tierPricesModel.Count
                    };

                    response = request.CreateResponse<DataSourceResult>(HttpStatusCode.OK, gridModel);
                }
                return response;

            });            
        }

        [HttpGet]
        [Route("TierPriceCreatePopup")]
        public HttpResponseMessage TierPriceCreatePopup(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
                {
                    var model = new ProductVM.TierPriceVM();

                    //stores
                    model.AvailableStores.Add(new System.Web.Mvc.SelectListItem { Text = _localizationService.GetResource("Admin.Common.All"), Value = "0" });
                    foreach (var store in _storeService.GetAllStores())
                        model.AvailableStores.Add(new System.Web.Mvc.SelectListItem { Text = store.Name, Value = store.Id.ToString() });

                    //customer roles
                    model.AvailableCustomerRoles.Add(new System.Web.Mvc.SelectListItem { Text = _localizationService.GetResource("Admin.Common.All"), Value = "0" });
                    foreach (var role in _customerService.GetAllCustomerRoles(true))
                        model.AvailableCustomerRoles.Add(new System.Web.Mvc.SelectListItem { Text = role.Name, Value = role.Id.ToString() });

                    response = request.CreateResponse<ProductVM.TierPriceVM>(HttpStatusCode.OK, model);
                }
                return response;

            });

            
        }

        [HttpPost]
        [Route("TierPrice/Add")]
        public HttpResponseMessage TierPriceCreatePopup(HttpRequestMessage request, ProductVM.TierPriceVM model)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
                {
                    var product = _productService.GetProductById(model.ProductId);
                    if (product == null)
                        throw new ArgumentException("No product found with the specified id");

                    //a vendor should have access only to his products
                    if (_workContext.CurrentVendor != null && product.VendorId != _workContext.CurrentVendor.Id)
                    {
                        string uri = Url.Link("DefaultProductPageLoad", null);
                        response.Headers.Location = new Uri(uri);
                    }

                    if (ModelState.IsValid)
                    {
                        var tierPrice = new TierPrice
                        {
                            ProductId = model.ProductId,
                            StoreId = model.StoreId,
                            CustomerRoleId = model.CustomerRoleId > 0 ? model.CustomerRoleId : (int?)null,
                            Quantity = model.Quantity,
                            Price = model.Price,
                            StartDateTimeUtc = model.StartDateTimeUtc,
                            EndDateTimeUtc = model.EndDateTimeUtc
                        };
                        _productService.InsertTierPrice(tierPrice);

                        //update "HasTierPrices" property
                        _productService.UpdateHasTierPricesProperty(product);

                        _unitOfWork.Commit();
                        response = request.CreateResponse<ProductVM.TierPriceVM>(HttpStatusCode.OK, model);
                    }
                    else
                    {
                        //If we got this far, something failed, redisplay form

                        //stores
                        model.AvailableStores.Add(new System.Web.Mvc.SelectListItem { Text = _localizationService.GetResource("Admin.Common.All"), Value = "0" });
                        foreach (var store in _storeService.GetAllStores())
                            model.AvailableStores.Add(new System.Web.Mvc.SelectListItem { Text = store.Name, Value = store.Id.ToString() });

                        //customer roles
                        model.AvailableCustomerRoles.Add(new System.Web.Mvc.SelectListItem { Text = _localizationService.GetResource("Admin.Common.All"), Value = "0" });
                        foreach (var role in _customerService.GetAllCustomerRoles(true))
                            model.AvailableCustomerRoles.Add(new System.Web.Mvc.SelectListItem { Text = role.Name, Value = role.Id.ToString() });

                        response = request.CreateResponse<ProductVM.TierPriceVM>(HttpStatusCode.OK, model);
                    }
                }
                return response;

            });
            
        }

        [HttpGet]
        [Route("TierPriceEditPopup/{id:int}")]
        public HttpResponseMessage TierPriceEditPopup(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
                {
                    var tierPrice = _productService.GetTierPriceById(id);
                    if (tierPrice == null)
                    {
                        string uri = Url.Link("DefaultProductPageLoad", null);
                        response.Headers.Location = new Uri(uri);
                    }

                    var product = _productService.GetProductById(tierPrice.ProductId);
                    if (product == null)
                        throw new ArgumentException("No product found with the specified id");

                    //a vendor should have access only to his products
                    if (_workContext.CurrentVendor != null && product.VendorId != _workContext.CurrentVendor.Id)
                    {
                        string uri = Url.Link("DefaultProductPageLoad", null);
                        response.Headers.Location = new Uri(uri);
                    }

                    var model = new ProductVM.TierPriceVM
                    {
                        Id = tierPrice.Id,
                        CustomerRoleId = tierPrice.CustomerRoleId.HasValue ? tierPrice.CustomerRoleId.Value : 0,
                        StoreId = tierPrice.StoreId,
                        Quantity = tierPrice.Quantity,
                        Price = tierPrice.Price,
                        StartDateTimeUtc = tierPrice.StartDateTimeUtc,
                        EndDateTimeUtc = tierPrice.EndDateTimeUtc
                    };

                    //stores
                    model.AvailableStores.Add(new System.Web.Mvc.SelectListItem { Text = _localizationService.GetResource("Admin.Common.All"), Value = "0" });
                    foreach (var store in _storeService.GetAllStores())
                        model.AvailableStores.Add(new System.Web.Mvc.SelectListItem { Text = store.Name, Value = store.Id.ToString() });

                    //customer roles
                    model.AvailableCustomerRoles.Add(new System.Web.Mvc.SelectListItem { Text = _localizationService.GetResource("Admin.Common.All"), Value = "0" });
                    foreach (var role in _customerService.GetAllCustomerRoles(true))
                        model.AvailableCustomerRoles.Add(new System.Web.Mvc.SelectListItem { Text = role.Name, Value = role.Id.ToString() });

                    response = request.CreateResponse<ProductVM.TierPriceVM>(HttpStatusCode.OK, model);
                }
                return response;

            });

            
        }

        [HttpPost]
        [Route("TierPrice/Update")]
        public HttpResponseMessage TierPriceEditPopup(HttpRequestMessage request, ProductVM.TierPriceVM model)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
                {
                    var tierPrice = _productService.GetTierPriceById(model.Id);
                    if (tierPrice == null)
                    {
                        string uri = Url.Link("DefaultProductPageLoad", null);
                        response.Headers.Location = new Uri(uri);
                    }

                    var product = _productService.GetProductById(tierPrice.ProductId);
                    if (product == null)
                        throw new ArgumentException("No product found with the specified id");

                    //a vendor should have access only to his products
                    if (_workContext.CurrentVendor != null && product.VendorId != _workContext.CurrentVendor.Id)
                    {
                        string uri = Url.Link("DefaultProductPageLoad", null);
                        response.Headers.Location = new Uri(uri);
                    }

                    if (ModelState.IsValid)
                    {
                        tierPrice.StoreId = model.StoreId;
                        tierPrice.CustomerRoleId = model.CustomerRoleId > 0 ? model.CustomerRoleId : (int?)null;
                        tierPrice.Quantity = model.Quantity;
                        tierPrice.Price = model.Price;
                        tierPrice.StartDateTimeUtc = model.StartDateTimeUtc;
                        tierPrice.EndDateTimeUtc = model.EndDateTimeUtc;
                        _productService.UpdateTierPrice(tierPrice);
                        _unitOfWork.Commit();
                    }
                    else
                    {
                        //If we got this far, something failed, redisplay form

                        //stores
                        model.AvailableStores.Add(new System.Web.Mvc.SelectListItem { Text = _localizationService.GetResource("Admin.Common.All"), Value = "0" });
                        foreach (var store in _storeService.GetAllStores())
                            model.AvailableStores.Add(new System.Web.Mvc.SelectListItem { Text = store.Name, Value = store.Id.ToString() });

                        //customer roles
                        model.AvailableCustomerRoles.Add(new System.Web.Mvc.SelectListItem { Text = _localizationService.GetResource("Admin.Common.All"), Value = "0" });
                        foreach (var role in _customerService.GetAllCustomerRoles(true))
                            model.AvailableCustomerRoles.Add(new System.Web.Mvc.SelectListItem { Text = role.Name, Value = role.Id.ToString() });
                        
                    }
                    response = request.CreateResponse<ProductVM.TierPriceVM>(HttpStatusCode.OK, model);
                }
                return response;

            });
        }

        [HttpPost]
        [Route("TierPrice/Delete")]
        public HttpResponseMessage TierPriceDelete(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
                {
                    var tierPrice = _productService.GetTierPriceById(id);
                    if (tierPrice == null)
                        throw new ArgumentException("No tier price found with the specified id");

                    var product = _productService.GetProductById(tierPrice.ProductId);
                    if (product == null)
                        throw new ArgumentException("No product found with the specified id");

                    //a vendor should have access only to his products
                    if (_workContext.CurrentVendor != null && product.VendorId != _workContext.CurrentVendor.Id)
                    {
                        response = request.CreateErrorResponse(HttpStatusCode.NotFound, "This is not your product");
                        return response;
                    }

                    _productService.DeleteTierPrice(tierPrice);

                    //update "HasTierPrices" property
                    _productService.UpdateHasTierPricesProperty(product);

                    _unitOfWork.Commit();
                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                return response;

            });

            
        }

        #endregion

        #region Bulk editing
        [HttpGet]
        [Route("BulkEdit")]
        public HttpResponseMessage BulkEdit(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
                {
                    var model = new BulkEditListVM();
                    //categories
                    model.AvailableCategories.Add(new System.Web.Mvc.SelectListItem { Text = _localizationService.GetResource("Admin.Common.All"), Value = "0" });
                    var categories = SelectListHelper.GetCategoryList(_categoryService, true);
                    foreach (var c in categories)
                        model.AvailableCategories.Add(c);

                    //manufacturers
                    model.AvailableManufacturers.Add(new System.Web.Mvc.SelectListItem { Text = _localizationService.GetResource("Admin.Common.All"), Value = "0" });
                    var manufacturers = SelectListHelper.GetManufacturerList(_manufacturerService, true);
                    foreach (var m in manufacturers)
                        model.AvailableManufacturers.Add(m);

                    //product types
                    model.AvailableProductTypes = ProductType.SimpleProduct.ToSelectList(_localizationService, _workContext, false).ToList();
                    model.AvailableProductTypes.Insert(0, new System.Web.Mvc.SelectListItem { Text = _localizationService.GetResource("Admin.Common.All"), Value = "0" });

                    response = request.CreateResponse<BulkEditListVM>(HttpStatusCode.OK, model);
                }
                return response;
            });
        }

        [HttpPost]
        [Route("BulkEditSelect")]
        public HttpResponseMessage BulkEditSelect(HttpRequestMessage request, BulkEditListVM model, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
                {
                    int vendorId = 0;
                    //a vendor should have access only to his products
                    if (_workContext.CurrentVendor != null)
                        vendorId = _workContext.CurrentVendor.Id;

                    var products = _productService.SearchProducts(categoryIds: new List<int> { model.SearchCategoryId },
                        manufacturerId: model.SearchManufacturerId,
                        vendorId: vendorId,
                        productType: model.SearchProductTypeId > 0 ? (ProductType?)model.SearchProductTypeId : null,
                        keywords: model.SearchProductName,
                        pageIndex: pageIndex,
                        pageSize: pageSize,
                        showHidden: true);

                    var gridModel = new DataSourceResult();
                    gridModel.Data = products.Select(x =>
                    {
                        var productModel = new BulkEditProductVM
                        {
                            Id = x.Id,
                            Name = x.Name,
                            Sku = x.Sku,
                            OldPrice = x.OldPrice,
                            Price = x.Price,
                            ManageInventoryMethod = x.ManageInventoryMethod.GetLocalizedEnum(_localizationService, _workContext.WorkingLanguage.Id),
                            StockQuantity = x.StockQuantity,
                            Published = x.Published
                        };

                        if (x.ManageInventoryMethod == ManageInventoryMethod.ManageStock && x.UseMultipleWarehouses)
                        {
                            //multi-warehouse supported
                            //TODO localize
                            productModel.ManageInventoryMethod += " (multi-warehouse)";
                        }

                        return productModel;
                    });
                    gridModel.Total = products.TotalCount;
                    response = request.CreateResponse<DataSourceResult>(HttpStatusCode.OK, gridModel);
                }
                return response;
            });
        }

        [HttpPost]
        [Route("BulkProducts/Update")]
        public HttpResponseMessage BulkEditUpdate(HttpRequestMessage request, IEnumerable<BulkEditProductVM> products)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
                {
                    if (products != null)
                    {
                        foreach (var pModel in products)
                        {
                            //update
                            var product = _productService.GetProductById(pModel.Id);
                            if (product != null)
                            {
                                //a vendor should have access only to his products
                                if (_workContext.CurrentVendor != null && product.VendorId != _workContext.CurrentVendor.Id)
                                    continue;

                                var prevTotalStockQuantity = product.GetTotalStockQuantity();
                                var previousStockQuantity = product.StockQuantity;

                                product.Name = pModel.Name;
                                product.Sku = pModel.Sku;
                                product.Price = pModel.Price;
                                product.OldPrice = pModel.OldPrice;
                                product.StockQuantity = pModel.StockQuantity;
                                product.Published = pModel.Published;
                                product.UpdatedOnUtc = DateTime.UtcNow;
                                _productService.UpdateProduct(product);

                                //back in stock notifications
                                if (product.ManageInventoryMethod == ManageInventoryMethod.ManageStock &&
                                    product.BackorderMode == BackorderMode.NoBackorders &&
                                    product.AllowBackInStockSubscriptions &&
                                    product.GetTotalStockQuantity() > 0 &&
                                    prevTotalStockQuantity <= 0 &&
                                    product.Published &&
                                    !product.Deleted)
                                {
                                    _backInStockSubscriptionService.SendNotificationsToSubscribers(product);
                                }

                                //quantity change history
                                _productService.AddStockQuantityHistoryEntry(product, product.StockQuantity - previousStockQuantity, product.StockQuantity,
                                    product.WarehouseId, _localizationService.GetResource("Admin.StockQuantityHistory.Messages.Edit"));
                            }
                        }
                        _unitOfWork.Commit();
                    }

                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                return response;

            });
        }

        [HttpPost]
        [Route("BulkProducts/Delete")]
        public HttpResponseMessage BulkEditDelete(HttpRequestMessage request, IEnumerable<BulkEditProductVM> products)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.NotFound, "No items found");
                if (_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
                {
                    if (products != null)
                    {
                        foreach (var pModel in products)
                        {
                            //delete
                            var product = _productService.GetProductById(pModel.Id);
                            if (product != null)
                            {
                                //a vendor should have access only to his products
                                if (_workContext.CurrentVendor != null && product.VendorId != _workContext.CurrentVendor.Id)
                                    continue;

                                _productService.DeleteProduct(product);
                            }
                        }
                        _unitOfWork.Commit();
                    }
                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                return response;
            });
        }

        #endregion

        #endregion
    }
}
