using Denmakers.DreamSale.ViewModels.AdminVM.Settings;
using Denmakers.DreamSale.ViewModels.Validators.Catalog;
using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Denmakers.DreamSale.ViewModels.AdminVM.Catalog
{
    [Validator(typeof(ProductValidator))]
    public partial class ProductVM
    {
        public ProductVM()
        {
            Locales = new List<ProductLocalizedVM>();
            ProductPictureModels = new List<ProductPictureVM>();
            CopyProductModel = new CopyProductVM();
            AddPictureModel = new ProductPictureVM();
            AddSpecificationAttributeModel = new AddProductSpecificationAttributeVM();
            ProductWarehouseInventoryModels = new List<ProductWarehouseInventoryVM>();
            ProductEditorSettingsModel = new ProductEditorSettingsVM();
            StockQuantityHistory = new StockQuantityHistoryVM();

            AvailableBasepriceUnits = new List<System.Web.Mvc.SelectListItem>();
            AvailableBasepriceBaseUnits = new List<System.Web.Mvc.SelectListItem>();
            AvailableProductTemplates = new List<System.Web.Mvc.SelectListItem>();
            AvailableTaxCategories = new List<System.Web.Mvc.SelectListItem>();
            AvailableDeliveryDates = new List<System.Web.Mvc.SelectListItem>();
            AvailableProductAvailabilityRanges = new List<System.Web.Mvc.SelectListItem>();
            AvailableWarehouses = new List<System.Web.Mvc.SelectListItem>();
            AvailableProductAttributes = new List<System.Web.Mvc.SelectListItem>();
            ProductsTypesSupportedByProductTemplates = new Dictionary<int, IList<System.Web.Mvc.SelectListItem>>();

            AvailableVendors = new List<System.Web.Mvc.SelectListItem>();

            SelectedStoreIds = new List<int>();
            AvailableStores = new List<System.Web.Mvc.SelectListItem>();

            SelectedManufacturerIds = new List<int>();
            AvailableManufacturers = new List<System.Web.Mvc.SelectListItem>();

            SelectedCategoryIds = new List<int>();
            AvailableCategories = new List<System.Web.Mvc.SelectListItem>();

            SelectedCustomerRoleIds = new List<int>();
            AvailableCustomerRoles = new List<System.Web.Mvc.SelectListItem>();

            SelectedDiscountIds = new List<int>();
            AvailableDiscounts = new List<System.Web.Mvc.SelectListItem>();
            CustomProperties = new Dictionary<string, object>();
        }

        public Dictionary<string, object> CustomProperties { get; set; }

        [DisplayName("ID")]
        public int Id { get; set; }

        //picture thumbnail
        [DisplayName("Picture")]
        public string PictureThumbnailUrl { get; set; }

        [DisplayName("Product type")]
        public int ProductTypeId { get; set; }

        [DisplayName("Product type")]
        public string ProductTypeName { get; set; }


        [DisplayName("Associated to product")]
        public int AssociatedToProductId { get; set; }
        [DisplayName("Associated to product")]
        public string AssociatedToProductName { get; set; }

        [DisplayName("Visible individually")]
        public bool VisibleIndividually { get; set; }

        [DisplayName("Admin.Catalog.Products.Fields.ProductTemplate")]
        public int ProductTemplateId { get; set; }
        public IList<System.Web.Mvc.SelectListItem> AvailableProductTemplates { get; set; }
        //<product type ID, list of supported product template IDs>
        public Dictionary<int, IList<System.Web.Mvc.SelectListItem>> ProductsTypesSupportedByProductTemplates { get; set; }

        [DisplayName("Product name")]
        [AllowHtml]
        public string Name { get; set; }

        [DisplayName("Short description")]
        [AllowHtml]
        public string ShortDescription { get; set; }

        [DisplayName("Full description")]
        [AllowHtml]
        public string FullDescription { get; set; }

        [DisplayName("Admin comment")]
        [AllowHtml]
        public string AdminComment { get; set; }

        [DisplayName("Show on home page")]
        public bool ShowOnHomePage { get; set; }

        [DisplayName("MetaKeywords")]
        [AllowHtml]
        public string MetaKeywords { get; set; }

        [DisplayName("MetaDescription")]
        [AllowHtml]
        public string MetaDescription { get; set; }

        [DisplayName("MetaTitle")]
        [AllowHtml]
        public string MetaTitle { get; set; }

        [DisplayName("SeName")]
        [AllowHtml]
        public string SeName { get; set; }

        [DisplayName("Allow customer reviews")]
        public bool AllowCustomerReviews { get; set; }

        [DisplayName("Product tags")]
        public string ProductTags { get; set; }

        [DisplayName("Admin.Catalog.Products.Fields.Sku")]
        [AllowHtml]
        public string Sku { get; set; }

        [DisplayName("Admin.Catalog.Products.Fields.ManufacturerPartNumber")]
        [AllowHtml]
        public string ManufacturerPartNumber { get; set; }

        [DisplayName("Admin.Catalog.Products.Fields.GTIN")]
        [AllowHtml]
        public virtual string Gtin { get; set; }

        [DisplayName("Admin.Catalog.Products.Fields.IsGiftCard")]
        public bool IsGiftCard { get; set; }
        [DisplayName("Admin.Catalog.Products.Fields.GiftCardType")]
        public int GiftCardTypeId { get; set; }
        [DisplayName("Admin.Catalog.Products.Fields.OverriddenGiftCardAmount")]
        [UIHint("DecimalNullable")]
        public decimal? OverriddenGiftCardAmount { get; set; }

        [DisplayName("Admin.Catalog.Products.Fields.RequireOtherProducts")]
        public bool RequireOtherProducts { get; set; }

        [DisplayName("Admin.Catalog.Products.Fields.RequiredProductIds")]
        public string RequiredProductIds { get; set; }

        [DisplayName("Admin.Catalog.Products.Fields.AutomaticallyAddRequiredProducts")]
        public bool AutomaticallyAddRequiredProducts { get; set; }

        [DisplayName("Admin.Catalog.Products.Fields.IsDownload")]
        public bool IsDownload { get; set; }

        [DisplayName("Admin.Catalog.Products.Fields.Download")]
        [UIHint("Download")]
        public int DownloadId { get; set; }

        [DisplayName("Admin.Catalog.Products.Fields.UnlimitedDownloads")]
        public bool UnlimitedDownloads { get; set; }

        [DisplayName("Admin.Catalog.Products.Fields.MaxNumberOfDownloads")]
        public int MaxNumberOfDownloads { get; set; }

        [DisplayName("Admin.Catalog.Products.Fields.DownloadExpirationDays")]
        [UIHint("Int32Nullable")]
        public int? DownloadExpirationDays { get; set; }

        [DisplayName("Admin.Catalog.Products.Fields.DownloadActivationType")]
        public int DownloadActivationTypeId { get; set; }

        [DisplayName("Admin.Catalog.Products.Fields.HasSampleDownload")]
        public bool HasSampleDownload { get; set; }

        [DisplayName("Admin.Catalog.Products.Fields.SampleDownload")]
        [UIHint("Download")]
        public int SampleDownloadId { get; set; }

        [DisplayName("Admin.Catalog.Products.Fields.HasUserAgreement")]
        public bool HasUserAgreement { get; set; }

        [DisplayName("Admin.Catalog.Products.Fields.UserAgreementText")]
        [AllowHtml]
        public string UserAgreementText { get; set; }

        [DisplayName("Admin.Catalog.Products.Fields.IsRecurring")]
        public bool IsRecurring { get; set; }

        [DisplayName("Admin.Catalog.Products.Fields.RecurringCycleLength")]
        public int RecurringCycleLength { get; set; }

        [DisplayName("Admin.Catalog.Products.Fields.RecurringCyclePeriod")]
        public int RecurringCyclePeriodId { get; set; }

        [DisplayName("Admin.Catalog.Products.Fields.RecurringTotalCycles")]
        public int RecurringTotalCycles { get; set; }

        [DisplayName("Admin.Catalog.Products.Fields.IsRental")]
        public bool IsRental { get; set; }

        [DisplayName("Admin.Catalog.Products.Fields.RentalPriceLength")]
        public int RentalPriceLength { get; set; }

        [DisplayName("Admin.Catalog.Products.Fields.RentalPricePeriod")]
        public int RentalPricePeriodId { get; set; }

        [DisplayName("Admin.Catalog.Products.Fields.IsShipEnabled")]
        public bool IsShipEnabled { get; set; }

        [DisplayName("Admin.Catalog.Products.Fields.IsFreeShipping")]
        public bool IsFreeShipping { get; set; }

        [DisplayName("Admin.Catalog.Products.Fields.ShipSeparately")]
        public bool ShipSeparately { get; set; }

        [DisplayName("Admin.Catalog.Products.Fields.AdditionalShippingCharge")]
        public decimal AdditionalShippingCharge { get; set; }

        [DisplayName("Admin.Catalog.Products.Fields.DeliveryDate")]
        public int DeliveryDateId { get; set; }
        public IList<System.Web.Mvc.SelectListItem> AvailableDeliveryDates { get; set; }

        [DisplayName("Admin.Catalog.Products.Fields.IsTaxExempt")]
        public bool IsTaxExempt { get; set; }

        [DisplayName("Admin.Catalog.Products.Fields.TaxCategory")]
        public int TaxCategoryId { get; set; }
        public IList<System.Web.Mvc.SelectListItem> AvailableTaxCategories { get; set; }

        [DisplayName("Admin.Catalog.Products.Fields.IsTelecommunicationsOrBroadcastingOrElectronicServices")]
        public bool IsTelecommunicationsOrBroadcastingOrElectronicServices { get; set; }

        [DisplayName("Admin.Catalog.Products.Fields.ManageInventoryMethod")]
        public int ManageInventoryMethodId { get; set; }

        [DisplayName("Admin.Catalog.Products.Fields.ProductAvailabilityRange")]
        public int ProductAvailabilityRangeId { get; set; }
        public IList<System.Web.Mvc.SelectListItem> AvailableProductAvailabilityRanges { get; set; }

        [DisplayName("Admin.Catalog.Products.Fields.UseMultipleWarehouses")]
        public bool UseMultipleWarehouses { get; set; }

        [DisplayName("Admin.Catalog.Products.Fields.Warehouse")]
        public int WarehouseId { get; set; }
        public IList<System.Web.Mvc.SelectListItem> AvailableWarehouses { get; set; }

        [DisplayName("Admin.Catalog.Products.Fields.StockQuantity")]
        public int StockQuantity { get; set; }
        public int LastStockQuantity { get; set; }
        [DisplayName("Admin.Catalog.Products.Fields.StockQuantity")]
        public string StockQuantityStr { get; set; }

        [DisplayName("Admin.Catalog.Products.Fields.DisplayStockAvailability")]
        public bool DisplayStockAvailability { get; set; }

        [DisplayName("Admin.Catalog.Products.Fields.DisplayStockQuantity")]
        public bool DisplayStockQuantity { get; set; }

        [DisplayName("Admin.Catalog.Products.Fields.MinStockQuantity")]
        public int MinStockQuantity { get; set; }

        [DisplayName("Admin.Catalog.Products.Fields.LowStockActivity")]
        public int LowStockActivityId { get; set; }

        [DisplayName("Admin.Catalog.Products.Fields.NotifyAdminForQuantityBelow")]
        public int NotifyAdminForQuantityBelow { get; set; }

        [DisplayName("Admin.Catalog.Products.Fields.BackorderMode")]
        public int BackorderModeId { get; set; }

        [DisplayName("Admin.Catalog.Products.Fields.AllowBackInStockSubscriptions")]
        public bool AllowBackInStockSubscriptions { get; set; }

        [DisplayName("Admin.Catalog.Products.Fields.OrderMinimumQuantity")]
        public int OrderMinimumQuantity { get; set; }

        [DisplayName("Admin.Catalog.Products.Fields.OrderMaximumQuantity")]
        public int OrderMaximumQuantity { get; set; }

        [DisplayName("Admin.Catalog.Products.Fields.AllowedQuantities")]
        public string AllowedQuantities { get; set; }

        [DisplayName("Admin.Catalog.Products.Fields.AllowAddingOnlyExistingAttributeCombinations")]
        public bool AllowAddingOnlyExistingAttributeCombinations { get; set; }

        [DisplayName("Admin.Catalog.Products.Fields.NotReturnable")]
        public bool NotReturnable { get; set; }

        [DisplayName("Admin.Catalog.Products.Fields.DisableBuyButton")]
        public bool DisableBuyButton { get; set; }

        [DisplayName("Admin.Catalog.Products.Fields.DisableWishlistButton")]
        public bool DisableWishlistButton { get; set; }

        [DisplayName("Admin.Catalog.Products.Fields.AvailableForPreOrder")]
        public bool AvailableForPreOrder { get; set; }

        [DisplayName("Admin.Catalog.Products.Fields.PreOrderAvailabilityStartDateTimeUtc")]
        [UIHint("DateTimeNullable")]
        public DateTime? PreOrderAvailabilityStartDateTimeUtc { get; set; }

        [DisplayName("Admin.Catalog.Products.Fields.CallForPrice")]
        public bool CallForPrice { get; set; }

        [DisplayName("Admin.Catalog.Products.Fields.Price")]
        public decimal Price { get; set; }

        [DisplayName("Admin.Catalog.Products.Fields.OldPrice")]
        public decimal OldPrice { get; set; }

        [DisplayName("Admin.Catalog.Products.Fields.ProductCost")]
        public decimal ProductCost { get; set; }

        [DisplayName("Admin.Catalog.Products.Fields.CustomerEntersPrice")]
        public bool CustomerEntersPrice { get; set; }

        [DisplayName("Admin.Catalog.Products.Fields.MinimumCustomerEnteredPrice")]
        public decimal MinimumCustomerEnteredPrice { get; set; }

        [DisplayName("Admin.Catalog.Products.Fields.MaximumCustomerEnteredPrice")]
        public decimal MaximumCustomerEnteredPrice { get; set; }

        [DisplayName("Admin.Catalog.Products.Fields.BasepriceEnabled")]
        public bool BasepriceEnabled { get; set; }
        [DisplayName("Admin.Catalog.Products.Fields.BasepriceAmount")]
        public decimal BasepriceAmount { get; set; }
        [DisplayName("Admin.Catalog.Products.Fields.BasepriceUnit")]
        public int BasepriceUnitId { get; set; }
        public IList<System.Web.Mvc.SelectListItem> AvailableBasepriceUnits { get; set; }
        [DisplayName("Admin.Catalog.Products.Fields.BasepriceBaseAmount")]
        public decimal BasepriceBaseAmount { get; set; }
        [DisplayName("Admin.Catalog.Products.Fields.BasepriceBaseUnit")]
        public int BasepriceBaseUnitId { get; set; }
        public IList<System.Web.Mvc.SelectListItem> AvailableBasepriceBaseUnits { get; set; }


        [DisplayName("Admin.Catalog.Products.Fields.MarkAsNew")]
        public bool MarkAsNew { get; set; }
        [DisplayName("Admin.Catalog.Products.Fields.MarkAsNewStartDateTimeUtc")]
        [UIHint("DateTimeNullable")]
        public DateTime? MarkAsNewStartDateTimeUtc { get; set; }
        [DisplayName("Admin.Catalog.Products.Fields.MarkAsNewEndDateTimeUtc")]
        [UIHint("DateTimeNullable")]
        public DateTime? MarkAsNewEndDateTimeUtc { get; set; }


        [DisplayName("Admin.Catalog.Products.Fields.Weight")]
        public decimal Weight { get; set; }

        [DisplayName("Admin.Catalog.Products.Fields.Length")]
        public decimal Length { get; set; }

        [DisplayName("Admin.Catalog.Products.Fields.Width")]
        public decimal Width { get; set; }

        [DisplayName("Admin.Catalog.Products.Fields.Height")]
        public decimal Height { get; set; }

        [DisplayName("Admin.Catalog.Products.Fields.AvailableStartDateTime")]
        [UIHint("DateTimeNullable")]
        public DateTime? AvailableStartDateTimeUtc { get; set; }

        [DisplayName("Admin.Catalog.Products.Fields.AvailableEndDateTime")]
        [UIHint("DateTimeNullable")]
        public DateTime? AvailableEndDateTimeUtc { get; set; }

        [DisplayName("Admin.Catalog.Products.Fields.DisplayOrder")]
        public int DisplayOrder { get; set; }

        [DisplayName("Admin.Catalog.Products.Fields.Published")]
        public bool Published { get; set; }

        [DisplayName("Admin.Catalog.Products.Fields.CreatedOn")]
        public DateTime? CreatedOn { get; set; }
        [DisplayName("Admin.Catalog.Products.Fields.UpdatedOn")]
        public DateTime? UpdatedOn { get; set; }


        public string PrimaryStoreCurrencyCode { get; set; }
        public string BaseDimensionIn { get; set; }
        public string BaseWeightIn { get; set; }

        public IList<ProductLocalizedVM> Locales { get; set; }



        //ACL (customer roles)
        [DisplayName("Admin.Catalog.Products.Fields.AclCustomerRoles")]
        [UIHint("MultiSelect")]
        public IList<int> SelectedCustomerRoleIds { get; set; }
        public IList<System.Web.Mvc.SelectListItem> AvailableCustomerRoles { get; set; }

        //store mapping
        [DisplayName("Admin.Catalog.Products.Fields.LimitedToStores")]
        [UIHint("MultiSelect")]
        public IList<int> SelectedStoreIds { get; set; }
        public IList<System.Web.Mvc.SelectListItem> AvailableStores { get; set; }

        //categories
        [DisplayName("Admin.Catalog.Products.Fields.Categories")]
        [UIHint("MultiSelect")]
        public IList<int> SelectedCategoryIds { get; set; }
        public IList<System.Web.Mvc.SelectListItem> AvailableCategories { get; set; }

        //manufacturers
        [DisplayName("Admin.Catalog.Products.Fields.Manufacturers")]
        [UIHint("MultiSelect")]
        public IList<int> SelectedManufacturerIds { get; set; }
        public IList<System.Web.Mvc.SelectListItem> AvailableManufacturers { get; set; }

        //vendors
        [DisplayName("Admin.Catalog.Products.Fields.Vendor")]
        public int VendorId { get; set; }
        public IList<System.Web.Mvc.SelectListItem> AvailableVendors { get; set; }

        //discounts
        [DisplayName("Admin.Catalog.Products.Fields.Discounts")]
        [UIHint("MultiSelect")]
        public IList<int> SelectedDiscountIds { get; set; }
        public IList<System.Web.Mvc.SelectListItem> AvailableDiscounts { get; set; }

        //vendor
        public bool IsLoggedInAsVendor { get; set; }

        //product attributes
        public IList<System.Web.Mvc.SelectListItem> AvailableProductAttributes { get; set; }

        //pictures
        public ProductPictureVM AddPictureModel { get; set; }
        public IList<ProductPictureVM> ProductPictureModels { get; set; }

        //add specification attribute model
        public AddProductSpecificationAttributeVM AddSpecificationAttributeModel { get; set; }

        //multiple warehouses
        [DisplayName("Admin.Catalog.Products.ProductWarehouseInventory")]
        public IList<ProductWarehouseInventoryVM> ProductWarehouseInventoryModels { get; set; }

        //copy product
        public CopyProductVM CopyProductModel { get; set; }

        //editor settings
        public ProductEditorSettingsVM ProductEditorSettingsModel { get; set; }

        //stock quantity history
        public StockQuantityHistoryVM StockQuantityHistory { get; set; }

        #region Nested classes

        public partial class AddRequiredProductVM
        {
            public AddRequiredProductVM()
            {
                AvailableCategories = new List<System.Web.Mvc.SelectListItem>();
                AvailableManufacturers = new List<System.Web.Mvc.SelectListItem>();
                AvailableStores = new List<System.Web.Mvc.SelectListItem>();
                AvailableVendors = new List<System.Web.Mvc.SelectListItem>();
                AvailableProductTypes = new List<System.Web.Mvc.SelectListItem>();
                CustomProperties = new Dictionary<string, object>();

            }
            public int Id { get; set; }

            public Dictionary<string, object> CustomProperties { get; set; }


            [DisplayName("Admin.Catalog.Products.List.SearchProductName")]
            [AllowHtml]
            public string SearchProductName { get; set; }
            [DisplayName("Admin.Catalog.Products.List.SearchCategory")]
            public int SearchCategoryId { get; set; }
            [DisplayName("Admin.Catalog.Products.List.SearchManufacturer")]
            public int SearchManufacturerId { get; set; }
            [DisplayName("Admin.Catalog.Products.List.SearchStore")]
            public int SearchStoreId { get; set; }
            [DisplayName("Admin.Catalog.Products.List.SearchVendor")]
            public int SearchVendorId { get; set; }
            [DisplayName("Admin.Catalog.Products.List.SearchProductType")]
            public int SearchProductTypeId { get; set; }

            public IList<System.Web.Mvc.SelectListItem> AvailableCategories { get; set; }
            public IList<System.Web.Mvc.SelectListItem> AvailableManufacturers { get; set; }
            public IList<System.Web.Mvc.SelectListItem> AvailableStores { get; set; }
            public IList<System.Web.Mvc.SelectListItem> AvailableVendors { get; set; }
            public IList<System.Web.Mvc.SelectListItem> AvailableProductTypes { get; set; }

            //vendor
            public bool IsLoggedInAsVendor { get; set; }
        }

        public partial class AddProductSpecificationAttributeVM
        {
            public AddProductSpecificationAttributeVM()
            {
                AvailableAttributes = new List<System.Web.Mvc.SelectListItem>();
                AvailableOptions = new List<System.Web.Mvc.SelectListItem>();
                CustomProperties = new Dictionary<string, object>();
            }
            public int Id { get; set; }

            public Dictionary<string, object> CustomProperties { get; set; }

            [DisplayName("Admin.Catalog.Products.SpecificationAttributes.Fields.SpecificationAttribute")]
            public int SpecificationAttributeId { get; set; }

            [DisplayName("Admin.Catalog.Products.SpecificationAttributes.Fields.AttributeType")]
            public int AttributeTypeId { get; set; }

            [DisplayName("Admin.Catalog.Products.SpecificationAttributes.Fields.SpecificationAttributeOption")]
            public int SpecificationAttributeOptionId { get; set; }

            [AllowHtml]
            [DisplayName("Admin.Catalog.Products.SpecificationAttributes.Fields.CustomValue")]
            public string CustomValue { get; set; }

            [DisplayName("Admin.Catalog.Products.SpecificationAttributes.Fields.AllowFiltering")]
            public bool AllowFiltering { get; set; }

            [DisplayName("Admin.Catalog.Products.SpecificationAttributes.Fields.ShowOnProductPage")]
            public bool ShowOnProductPage { get; set; }

            [DisplayName("Admin.Catalog.Products.SpecificationAttributes.Fields.DisplayOrder")]
            public int DisplayOrder { get; set; }

            public IList<System.Web.Mvc.SelectListItem> AvailableAttributes { get; set; }
            public IList<System.Web.Mvc.SelectListItem> AvailableOptions { get; set; }
        }

        public partial class ProductPictureVM
        {
            public int Id { get; set; }

            public Dictionary<string, object> CustomProperties { get; set; }

            public int ProductId { get; set; }

            [UIHint("Picture")]
            [DisplayName("Admin.Catalog.Products.Pictures.Fields.Picture")]
            public int PictureId { get; set; }

            [DisplayName("Admin.Catalog.Products.Pictures.Fields.Picture")]
            public string PictureUrl { get; set; }

            [DisplayName("Admin.Catalog.Products.Pictures.Fields.DisplayOrder")]
            public int DisplayOrder { get; set; }

            [DisplayName("Admin.Catalog.Products.Pictures.Fields.OverrideAltAttribute")]
            [AllowHtml]
            public string OverrideAltAttribute { get; set; }

            [DisplayName("Admin.Catalog.Products.Pictures.Fields.OverrideTitleAttribute")]
            [AllowHtml]
            public string OverrideTitleAttribute { get; set; }
        }

        public partial class RelatedProductVM
        {
            public int Id { get; set; }
            public Dictionary<string, object> CustomProperties { get; set; }

            public int ProductId2 { get; set; }

            [DisplayName("Admin.Catalog.Products.RelatedProducts.Fields.Product")]
            public string Product2Name { get; set; }

            [DisplayName("Admin.Catalog.Products.RelatedProducts.Fields.DisplayOrder")]
            public int DisplayOrder { get; set; }
        }
        public partial class AddRelatedProductVM
        {
            public int Id { get; set; }
            public Dictionary<string, object> CustomProperties { get; set; }

            public AddRelatedProductVM()
            {
                AvailableCategories = new List<System.Web.Mvc.SelectListItem>();
                AvailableManufacturers = new List<System.Web.Mvc.SelectListItem>();
                AvailableStores = new List<System.Web.Mvc.SelectListItem>();
                AvailableVendors = new List<System.Web.Mvc.SelectListItem>();
                AvailableProductTypes = new List<System.Web.Mvc.SelectListItem>();
                CustomProperties = new Dictionary<string, object>();
            }

            [DisplayName("Admin.Catalog.Products.List.SearchProductName")]
            [AllowHtml]
            public string SearchProductName { get; set; }
            [DisplayName("Admin.Catalog.Products.List.SearchCategory")]
            public int SearchCategoryId { get; set; }
            [DisplayName("Admin.Catalog.Products.List.SearchManufacturer")]
            public int SearchManufacturerId { get; set; }
            [DisplayName("Admin.Catalog.Products.List.SearchStore")]
            public int SearchStoreId { get; set; }
            [DisplayName("Admin.Catalog.Products.List.SearchVendor")]
            public int SearchVendorId { get; set; }
            [DisplayName("Admin.Catalog.Products.List.SearchProductType")]
            public int SearchProductTypeId { get; set; }

            public IList<System.Web.Mvc.SelectListItem> AvailableCategories { get; set; }
            public IList<System.Web.Mvc.SelectListItem> AvailableManufacturers { get; set; }
            public IList<System.Web.Mvc.SelectListItem> AvailableStores { get; set; }
            public IList<System.Web.Mvc.SelectListItem> AvailableVendors { get; set; }
            public IList<System.Web.Mvc.SelectListItem> AvailableProductTypes { get; set; }

            public int ProductId { get; set; }

            public int[] SelectedProductIds { get; set; }

            //vendor
            public bool IsLoggedInAsVendor { get; set; }
        }

        public partial class AssociatedProducVM
        {
            public int Id { get; set; }
            [DisplayName("Product")]
            public string ProductName { get; set; }
            [DisplayName("Display order")]
            public int DisplayOrder { get; set; }
        }
        public partial class AddAssociatedProductVM
        {
            public AddAssociatedProductVM()
            {
                AvailableCategories = new List<System.Web.Mvc.SelectListItem>();
                AvailableManufacturers = new List<System.Web.Mvc.SelectListItem>();
                AvailableStores = new List<System.Web.Mvc.SelectListItem>();
                AvailableVendors = new List<System.Web.Mvc.SelectListItem>();
                AvailableProductTypes = new List<System.Web.Mvc.SelectListItem>();
                CustomProperties = new Dictionary<string, object>();
            }

            public int Id { get; set; }
            public Dictionary<string, object> CustomProperties { get; set; }

            [DisplayName("Admin.Catalog.Products.List.SearchProductName")]
            [AllowHtml]
            public string SearchProductName { get; set; }
            [DisplayName("Admin.Catalog.Products.List.SearchCategory")]
            public int SearchCategoryId { get; set; }
            [DisplayName("Admin.Catalog.Products.List.SearchManufacturer")]
            public int SearchManufacturerId { get; set; }
            [DisplayName("Admin.Catalog.Products.List.SearchStore")]
            public int SearchStoreId { get; set; }
            [DisplayName("Admin.Catalog.Products.List.SearchVendor")]
            public int SearchVendorId { get; set; }
            [DisplayName("Admin.Catalog.Products.List.SearchProductType")]
            public int SearchProductTypeId { get; set; }

            public IList<System.Web.Mvc.SelectListItem> AvailableCategories { get; set; }
            public IList<System.Web.Mvc.SelectListItem> AvailableManufacturers { get; set; }
            public IList<System.Web.Mvc.SelectListItem> AvailableStores { get; set; }
            public IList<System.Web.Mvc.SelectListItem> AvailableVendors { get; set; }
            public IList<System.Web.Mvc.SelectListItem> AvailableProductTypes { get; set; }

            public int ProductId { get; set; }

            public int[] SelectedProductIds { get; set; }

            //vendor
            public bool IsLoggedInAsVendor { get; set; }
        }

        public partial class CrossSellProductVM
        {
            public int Id { get; set; }
            public int ProductId2 { get; set; }

            [DisplayName("Admin.Catalog.Products.CrossSells.Fields.Product")]
            public string Product2Name { get; set; }
        }
        public partial class AddCrossSellProductVM
        {
            public AddCrossSellProductVM()
            {
                AvailableCategories = new List<System.Web.Mvc.SelectListItem>();
                AvailableManufacturers = new List<System.Web.Mvc.SelectListItem>();
                AvailableStores = new List<System.Web.Mvc.SelectListItem>();
                AvailableVendors = new List<System.Web.Mvc.SelectListItem>();
                AvailableProductTypes = new List<System.Web.Mvc.SelectListItem>();
                CustomProperties = new Dictionary<string, object>();
            }

            public int Id { get; set; }
            public Dictionary<string, object> CustomProperties { get; set; }

            [DisplayName("Admin.Catalog.Products.List.SearchProductName")]
            [AllowHtml]
            public string SearchProductName { get; set; }
            [DisplayName("Admin.Catalog.Products.List.SearchCategory")]
            public int SearchCategoryId { get; set; }
            [DisplayName("Admin.Catalog.Products.List.SearchManufacturer")]
            public int SearchManufacturerId { get; set; }
            [DisplayName("Admin.Catalog.Products.List.SearchStore")]
            public int SearchStoreId { get; set; }
            [DisplayName("Admin.Catalog.Products.List.SearchVendor")]
            public int SearchVendorId { get; set; }
            [DisplayName("Admin.Catalog.Products.List.SearchProductType")]
            public int SearchProductTypeId { get; set; }

            public IList<System.Web.Mvc.SelectListItem> AvailableCategories { get; set; }
            public IList<System.Web.Mvc.SelectListItem> AvailableManufacturers { get; set; }
            public IList<System.Web.Mvc.SelectListItem> AvailableStores { get; set; }
            public IList<System.Web.Mvc.SelectListItem> AvailableVendors { get; set; }
            public IList<System.Web.Mvc.SelectListItem> AvailableProductTypes { get; set; }

            public int ProductId { get; set; }

            public int[] SelectedProductIds { get; set; }

            //vendor
            public bool IsLoggedInAsVendor { get; set; }
        }

        public partial class TierPriceVM
        {
            public TierPriceVM()
            {
                AvailableStores = new List<System.Web.Mvc.SelectListItem>();
                AvailableCustomerRoles = new List<System.Web.Mvc.SelectListItem>();
                CustomProperties = new Dictionary<string, object>();
            }

            public int Id { get; set; }
            public Dictionary<string, object> CustomProperties { get; set; }

            public int ProductId { get; set; }

            [DisplayName("Admin.Catalog.Products.TierPrices.Fields.CustomerRole")]
            public int CustomerRoleId { get; set; }
            public IList<System.Web.Mvc.SelectListItem> AvailableCustomerRoles { get; set; }
            public string CustomerRole { get; set; }

            [DisplayName("Admin.Catalog.Products.TierPrices.Fields.Store")]
            public int StoreId { get; set; }
            public IList<System.Web.Mvc.SelectListItem> AvailableStores { get; set; }
            public string Store { get; set; }

            [DisplayName("Admin.Catalog.Products.TierPrices.Fields.Quantity")]
            public int Quantity { get; set; }

            [DisplayName("Admin.Catalog.Products.TierPrices.Fields.Price")]
            public decimal Price { get; set; }

            [DisplayName("Admin.Catalog.Products.TierPrices.Fields.StartDateTimeUtc")]
            [UIHint("DateTimeNullable")]
            public DateTime? StartDateTimeUtc { get; set; }

            [DisplayName("Admin.Catalog.Products.TierPrices.Fields.EndDateTimeUtc")]
            [UIHint("DateTimeNullable")]
            public DateTime? EndDateTimeUtc { get; set; }
        }

        public partial class ProductWarehouseInventoryVM
        {
            public int Id { get; set; }

            [DisplayName("Admin.Catalog.Products.ProductWarehouseInventory.Fields.Warehouse")]
            public int WarehouseId { get; set; }
            [DisplayName("Admin.Catalog.Products.ProductWarehouseInventory.Fields.Warehouse")]
            public string WarehouseName { get; set; }

            [DisplayName("Admin.Catalog.Products.ProductWarehouseInventory.Fields.WarehouseUsed")]
            public bool WarehouseUsed { get; set; }

            [DisplayName("Admin.Catalog.Products.ProductWarehouseInventory.Fields.StockQuantity")]
            public int StockQuantity { get; set; }

            [DisplayName("Admin.Catalog.Products.ProductWarehouseInventory.Fields.ReservedQuantity")]
            public int ReservedQuantity { get; set; }

            [DisplayName("Admin.Catalog.Products.ProductWarehouseInventory.Fields.PlannedQuantity")]
            public int PlannedQuantity { get; set; }
        }


        public partial class ProductAttributeMappingVM
        {
            public int Id { get; set; }

            public int ProductId { get; set; }

            public int ProductAttributeId { get; set; }
            [DisplayName("Admin.Catalog.Products.ProductAttributes.Attributes.Fields.Attribute")]
            public string ProductAttribute { get; set; }

            [DisplayName("Admin.Catalog.Products.ProductAttributes.Attributes.Fields.TextPrompt")]
            [AllowHtml]
            public string TextPrompt { get; set; }

            [DisplayName("Admin.Catalog.Products.ProductAttributes.Attributes.Fields.IsRequired")]
            public bool IsRequired { get; set; }

            public int AttributeControlTypeId { get; set; }
            [DisplayName("Admin.Catalog.Products.ProductAttributes.Attributes.Fields.AttributeControlType")]
            public string AttributeControlType { get; set; }

            [DisplayName("Admin.Catalog.Products.ProductAttributes.Attributes.Fields.DisplayOrder")]
            public int DisplayOrder { get; set; }

            public bool ShouldHaveValues { get; set; }
            public int TotalValues { get; set; }

            //validation fields
            [DisplayName("Admin.Catalog.Products.ProductAttributes.Attributes.ValidationRules")]
            public bool ValidationRulesAllowed { get; set; }
            [DisplayName("Admin.Catalog.Products.ProductAttributes.Attributes.ValidationRules.MinLength")]
            [UIHint("Int32Nullable")]
            public int? ValidationMinLength { get; set; }
            [DisplayName("Admin.Catalog.Products.ProductAttributes.Attributes.ValidationRules.MaxLength")]
            [UIHint("Int32Nullable")]
            public int? ValidationMaxLength { get; set; }
            [AllowHtml]
            [DisplayName("Admin.Catalog.Products.ProductAttributes.Attributes.ValidationRules.FileAllowedExtensions")]
            public string ValidationFileAllowedExtensions { get; set; }
            [DisplayName("Admin.Catalog.Products.ProductAttributes.Attributes.ValidationRules.FileMaximumSize")]
            [UIHint("Int32Nullable")]
            public int? ValidationFileMaximumSize { get; set; }
            [AllowHtml]
            [DisplayName("Admin.Catalog.Products.ProductAttributes.Attributes.ValidationRules.DefaultValue")]
            public string DefaultValue { get; set; }
            public string ValidationRulesString { get; set; }

            //condition
            [DisplayName("Admin.Catalog.Products.ProductAttributes.Attributes.Condition")]
            public bool ConditionAllowed { get; set; }
            public string ConditionString { get; set; }
        }
        public partial class ProductAttributeValueListVM
        {
            public int Id { get; set; }
            public int ProductId { get; set; }

            public string ProductName { get; set; }

            public int ProductAttributeMappingId { get; set; }

            public string ProductAttributeName { get; set; }
        }

        [Validator(typeof(ProductAttributeValueVMValidator))]
        public partial class ProductAttributeValueVM
        {
            public ProductAttributeValueVM()
            {
                ProductPictureModels = new List<ProductPictureVM>();
                Locales = new List<ProductAttributeValueLocalizedVM>();
                CustomProperties = new Dictionary<string, object>();
            }

            public int Id { get; set; }

            public Dictionary<string, object> CustomProperties { get; set; }

            public int ProductAttributeMappingId { get; set; }

            [DisplayName("Admin.Catalog.Products.ProductAttributes.Attributes.Values.Fields.AttributeValueType")]
            public int AttributeValueTypeId { get; set; }
            [DisplayName("Admin.Catalog.Products.ProductAttributes.Attributes.Values.Fields.AttributeValueType")]
            public string AttributeValueTypeName { get; set; }

            [DisplayName("Admin.Catalog.Products.ProductAttributes.Attributes.Values.Fields.AssociatedProduct")]
            public int AssociatedProductId { get; set; }
            [DisplayName("Admin.Catalog.Products.ProductAttributes.Attributes.Values.Fields.AssociatedProduct")]
            public string AssociatedProductName { get; set; }

            [DisplayName("Admin.Catalog.Products.ProductAttributes.Attributes.Values.Fields.Name")]
            [AllowHtml]
            public string Name { get; set; }

            [DisplayName("Admin.Catalog.Products.ProductAttributes.Attributes.Values.Fields.ColorSquaresRgb")]
            [AllowHtml]
            public string ColorSquaresRgb { get; set; }
            public bool DisplayColorSquaresRgb { get; set; }

            [DisplayName("Admin.Catalog.Products.ProductAttributes.Attributes.Values.Fields.ImageSquaresPicture")]
            [UIHint("Picture")]
            public int ImageSquaresPictureId { get; set; }
            public bool DisplayImageSquaresPicture { get; set; }

            [DisplayName("Admin.Catalog.Products.ProductAttributes.Attributes.Values.Fields.PriceAdjustment")]
            public decimal PriceAdjustment { get; set; }
            [DisplayName("Admin.Catalog.Products.ProductAttributes.Attributes.Values.Fields.PriceAdjustment")]
            //used only on the values list page
            public string PriceAdjustmentStr { get; set; }

            [DisplayName("Admin.Catalog.Products.ProductAttributes.Attributes.Values.Fields.WeightAdjustment")]
            public decimal WeightAdjustment { get; set; }
            [DisplayName("Admin.Catalog.Products.ProductAttributes.Attributes.Values.Fields.WeightAdjustment")]
            //used only on the values list page
            public string WeightAdjustmentStr { get; set; }

            [DisplayName("Admin.Catalog.Products.ProductAttributes.Attributes.Values.Fields.Cost")]
            public decimal Cost { get; set; }

            [DisplayName("Admin.Catalog.Products.ProductAttributes.Attributes.Values.Fields.CustomerEntersQty")]
            public bool CustomerEntersQty { get; set; }

            [DisplayName("Admin.Catalog.Products.ProductAttributes.Attributes.Values.Fields.Quantity")]
            public int Quantity { get; set; }

            [DisplayName("Admin.Catalog.Products.ProductAttributes.Attributes.Values.Fields.IsPreSelected")]
            public bool IsPreSelected { get; set; }

            [DisplayName("Admin.Catalog.Products.ProductAttributes.Attributes.Values.Fields.DisplayOrder")]
            public int DisplayOrder { get; set; }

            [DisplayName("Admin.Catalog.Products.ProductAttributes.Attributes.Values.Fields.Picture")]
            public int PictureId { get; set; }
            [DisplayName("Admin.Catalog.Products.ProductAttributes.Attributes.Values.Fields.Picture")]
            public string PictureThumbnailUrl { get; set; }

            public IList<ProductPictureVM> ProductPictureModels { get; set; }
            public IList<ProductAttributeValueLocalizedVM> Locales { get; set; }

            #region Nested classes

            public partial class AssociateProductToAttributeValueVM
            {
                public AssociateProductToAttributeValueVM()
                {
                    AvailableCategories = new List<System.Web.Mvc.SelectListItem>();
                    AvailableManufacturers = new List<System.Web.Mvc.SelectListItem>();
                    AvailableStores = new List<System.Web.Mvc.SelectListItem>();
                    AvailableVendors = new List<System.Web.Mvc.SelectListItem>();
                    AvailableProductTypes = new List<System.Web.Mvc.SelectListItem>();
                    CustomProperties = new Dictionary<string, object>();
                }
                public int Id { get; set; }
                public Dictionary<string, object> CustomProperties { get; set; }
                [DisplayName("Admin.Catalog.Products.List.SearchProductName")]
                [AllowHtml]
                public string SearchProductName { get; set; }
                [DisplayName("Admin.Catalog.Products.List.SearchCategory")]
                public int SearchCategoryId { get; set; }
                [DisplayName("Admin.Catalog.Products.List.SearchManufacturer")]
                public int SearchManufacturerId { get; set; }
                [DisplayName("Admin.Catalog.Products.List.SearchStore")]
                public int SearchStoreId { get; set; }
                [DisplayName("Admin.Catalog.Products.List.SearchVendor")]
                public int SearchVendorId { get; set; }
                [DisplayName("Admin.Catalog.Products.List.SearchProductType")]
                public int SearchProductTypeId { get; set; }

                public IList<System.Web.Mvc.SelectListItem> AvailableCategories { get; set; }
                public IList<System.Web.Mvc.SelectListItem> AvailableManufacturers { get; set; }
                public IList<System.Web.Mvc.SelectListItem> AvailableStores { get; set; }
                public IList<System.Web.Mvc.SelectListItem> AvailableVendors { get; set; }
                public IList<System.Web.Mvc.SelectListItem> AvailableProductTypes { get; set; }

                //vendor
                public bool IsLoggedInAsVendor { get; set; }


                public int AssociatedToProductId { get; set; }
            }
            #endregion
        }
        public partial class ProductAttributeValueLocalizedVM
        {
            public int LanguageId { get; set; }

            [DisplayName("Admin.Catalog.Products.ProductAttributes.Attributes.Values.Fields.Name")]
            [AllowHtml]
            public string Name { get; set; }
        }
        public partial class ProductAttributeCombinationVM
        {
            public int Id { get; set; }
            public Dictionary<string, object> CustomProperties { get; set; }
            public int ProductId { get; set; }

            [DisplayName("Admin.Catalog.Products.ProductAttributes.AttributeCombinations.Fields.Attributes")]
            [AllowHtml]
            public string AttributesXml { get; set; }

            [AllowHtml]
            public string Warnings { get; set; }

            [DisplayName("Admin.Catalog.Products.ProductAttributes.AttributeCombinations.Fields.StockQuantity")]
            public int StockQuantity { get; set; }

            [DisplayName("Admin.Catalog.Products.ProductAttributes.AttributeCombinations.Fields.AllowOutOfStockOrders")]
            public bool AllowOutOfStockOrders { get; set; }

            [DisplayName("Admin.Catalog.Products.ProductAttributes.AttributeCombinations.Fields.Sku")]
            public string Sku { get; set; }

            [DisplayName("Admin.Catalog.Products.ProductAttributes.AttributeCombinations.Fields.ManufacturerPartNumber")]
            public string ManufacturerPartNumber { get; set; }

            [DisplayName("Admin.Catalog.Products.ProductAttributes.AttributeCombinations.Fields.Gtin")]
            public string Gtin { get; set; }

            [DisplayName("Admin.Catalog.Products.ProductAttributes.AttributeCombinations.Fields.OverriddenPrice")]
            [UIHint("DecimalNullable")]
            public decimal? OverriddenPrice { get; set; }

            [DisplayName("Admin.Catalog.Products.ProductAttributes.AttributeCombinations.Fields.NotifyAdminForQuantityBelow")]
            public int NotifyAdminForQuantityBelow { get; set; }

        }

        #region Stock quantity history

        public partial class StockQuantityHistoryVM
        {
            public int Id { get; set; }
            public Dictionary<string, object> CustomProperties { get; set; }

            [DisplayName("Admin.Catalog.Products.List.SearchWarehouse")]
            public int SearchWarehouseId { get; set; }

            [DisplayName("Admin.Catalog.Products.StockQuantityHistory.Fields.Warehouse")]
            [AllowHtml]
            public string WarehouseName { get; set; }

            [DisplayName("Admin.Catalog.Products.StockQuantityHistory.Fields.Combination")]
            [AllowHtml]
            public string AttributeCombination { get; set; }

            [DisplayName("Admin.Catalog.Products.StockQuantityHistory.Fields.QuantityAdjustment")]
            public int QuantityAdjustment { get; set; }

            [DisplayName("Admin.Catalog.Products.StockQuantityHistory.Fields.StockQuantity")]
            public int StockQuantity { get; set; }

            [DisplayName("Admin.Catalog.Products.StockQuantityHistory.Fields.Message")]
            [AllowHtml]
            public string Message { get; set; }

            [DisplayName("Admin.Catalog.Products.StockQuantityHistory.Fields.CreatedOn")]
            [UIHint("DecimalNullable")]
            public DateTime CreatedOn { get; set; }
        }

        #endregion

        #endregion
    }

    public partial class ProductLocalizedVM
    {
        public int LanguageId { get; set; }

        [DisplayName("Admin.Catalog.Products.Fields.Name")]
        [AllowHtml]
        public string Name { get; set; }

        [DisplayName("Admin.Catalog.Products.Fields.ShortDescription")]
        [AllowHtml]
        public string ShortDescription { get; set; }

        [DisplayName("Admin.Catalog.Products.Fields.FullDescription")]
        [AllowHtml]
        public string FullDescription { get; set; }

        [DisplayName("Admin.Catalog.Products.Fields.MetaKeywords")]
        [AllowHtml]
        public string MetaKeywords { get; set; }

        [DisplayName("Admin.Catalog.Products.Fields.MetaDescription")]
        [AllowHtml]
        public string MetaDescription { get; set; }

        [DisplayName("Admin.Catalog.Products.Fields.MetaTitle")]
        [AllowHtml]
        public string MetaTitle { get; set; }

        [DisplayName("Admin.Catalog.Products.Fields.SeName")]
        [AllowHtml]
        public string SeName { get; set; }
    }
}
