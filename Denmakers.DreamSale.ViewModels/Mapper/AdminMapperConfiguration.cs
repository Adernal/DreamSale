using System;
using AutoMapper;
using Denmakers.DreamSale.Model.Catalog;
using Denmakers.DreamSale.ViewModels.AdminVM.Catalog;
using Denmakers.DreamSale.ViewModels.AdminVM.Addresses;
using Denmakers.DreamSale.Model.Common;
using Denmakers.DreamSale.ViewModels.AdminVM.Customers;
using Denmakers.DreamSale.Model.Customers;
using Denmakers.DreamSale.ViewModels.AdminVM.Vendors;
using Denmakers.DreamSale.Model.Vendors;
using Denmakers.DreamSale.ViewModels.AdminVM.Settings;
using Denmakers.DreamSale.Model.Shipping;
using Denmakers.DreamSale.Model.Orders;
using Denmakers.DreamSale.ViewModels.AdminVM.Orders;
using Denmakers.DreamSale.ViewModels.AdminVM.Common;
using Denmakers.DreamSale.Model.Messages;
using Denmakers.DreamSale.ViewModels.AdminVM.Messages;
using Denmakers.DreamSale.Model.Stores;
using Denmakers.DreamSale.ViewModels.AdminVM.Stores;

namespace Denmakers.DreamSale.ViewModels.Mapper
{
    public class AdminMapperConfiguration : IMapperConfiguration
    {
        public Action<IMapperConfigurationExpression> GetConfiguration()
        {
            Action<IMapperConfigurationExpression> action = cfg =>
            {
                #region category
                // Domain To ViewModel Mapping 
                cfg.CreateMap<Category, CategoryVM>()
                    .ForMember(dest => dest.AvailableCategoryTemplates, mo => mo.Ignore())
                    .ForMember(dest => dest.Locales, mo => mo.Ignore())
                    .ForMember(dest => dest.Breadcrumb, mo => mo.Ignore())
                    .ForMember(dest => dest.AvailableCategories, mo => mo.Ignore())
                    .ForMember(dest => dest.AvailableDiscounts, mo => mo.Ignore())
                    .ForMember(dest => dest.SelectedDiscountIds, mo => mo.Ignore())
                    //.ForMember(dest => dest.SeName, mo => mo.MapFrom(src => src.GetSeName(0, true, false)))
                    .ForMember(dest => dest.SeName, mo => mo.Ignore())
                    .ForMember(dest => dest.AvailableCustomerRoles, mo => mo.Ignore())
                    .ForMember(dest => dest.SelectedCustomerRoleIds, mo => mo.Ignore())
                    .ForMember(dest => dest.AvailableStores, mo => mo.Ignore())
                    .ForMember(dest => dest.SelectedStoreIds, mo => mo.Ignore())
                    .ForMember(dest => dest.CustomProperties, mo => mo.Ignore());
                
                // ViewModel To Domain Mapping
                cfg.CreateMap<CategoryVM, Category>()
                    .ForMember(dest => dest.CreatedOnUtc, mo => mo.Ignore())
                    .ForMember(dest => dest.UpdatedOnUtc, mo => mo.Ignore())
                    .ForMember(dest => dest.Deleted, mo => mo.Ignore())
                    .ForMember(dest => dest.SubjectToAcl, mo => mo.Ignore())
                    .ForMember(dest => dest.AppliedDiscounts, mo => mo.Ignore())
                    .ForMember(dest => dest.LimitedToStores, mo => mo.Ignore());
                #endregion

                #region Manufacturer
                //manufacturer
                cfg.CreateMap<Manufacturer, ManufacturerVM>()
                    .ForMember(dest => dest.AvailableManufacturerTemplates, mo => mo.Ignore())
                    .ForMember(dest => dest.Locales, mo => mo.Ignore())
                    .ForMember(dest => dest.AvailableDiscounts, mo => mo.Ignore())
                    .ForMember(dest => dest.SelectedDiscountIds, mo => mo.Ignore())
                    //.ForMember(dest => dest.SeName, mo => mo.MapFrom(src => src.GetSeName(0, true, false)))
                    .ForMember(dest => dest.SeName, mo => mo.Ignore())
                    .ForMember(dest => dest.AvailableCustomerRoles, mo => mo.Ignore())
                    .ForMember(dest => dest.SelectedCustomerRoleIds, mo => mo.Ignore())
                    .ForMember(dest => dest.AvailableStores, mo => mo.Ignore())
                    .ForMember(dest => dest.SelectedStoreIds, mo => mo.Ignore())
                    .ForMember(dest => dest.CustomProperties, mo => mo.Ignore());
                cfg.CreateMap<ManufacturerVM, Manufacturer>()
                    .ForMember(dest => dest.SubjectToAcl, mo => mo.Ignore())
                    .ForMember(dest => dest.CreatedOnUtc, mo => mo.Ignore())
                    .ForMember(dest => dest.UpdatedOnUtc, mo => mo.Ignore())
                    .ForMember(dest => dest.Deleted, mo => mo.Ignore())
                    .ForMember(dest => dest.LimitedToStores, mo => mo.Ignore())
                    .ForMember(dest => dest.AppliedDiscounts, mo => mo.Ignore());
                ;
                #endregion

                #region Products
                //products
                cfg.CreateMap<Product, ProductVM>()
                    .ForMember(dest => dest.ProductsTypesSupportedByProductTemplates, mo => mo.Ignore())
                    .ForMember(dest => dest.ProductTypeName, mo => mo.Ignore())
                    .ForMember(dest => dest.AssociatedToProductId, mo => mo.Ignore())
                    .ForMember(dest => dest.AssociatedToProductName, mo => mo.Ignore())
                    .ForMember(dest => dest.StockQuantityStr, mo => mo.Ignore())
                    .ForMember(dest => dest.CreatedOn, mo => mo.Ignore())
                    .ForMember(dest => dest.UpdatedOn, mo => mo.Ignore())
                    .ForMember(dest => dest.ProductTags, mo => mo.Ignore())
                    .ForMember(dest => dest.PictureThumbnailUrl, mo => mo.Ignore())
                    .ForMember(dest => dest.AvailableVendors, mo => mo.Ignore())
                    .ForMember(dest => dest.AvailableProductTemplates, mo => mo.Ignore())
                    .ForMember(dest => dest.Locales, mo => mo.Ignore())
                    .ForMember(dest => dest.AvailableCategories, mo => mo.Ignore())
                    .ForMember(dest => dest.AvailableManufacturers, mo => mo.Ignore())
                    .ForMember(dest => dest.AvailableProductAttributes, mo => mo.Ignore())
                    .ForMember(dest => dest.AddPictureModel, mo => mo.Ignore())
                    .ForMember(dest => dest.ProductPictureModels, mo => mo.Ignore())
                    .ForMember(dest => dest.AddSpecificationAttributeModel, mo => mo.Ignore())
                    .ForMember(dest => dest.CopyProductModel, mo => mo.Ignore())
                    .ForMember(dest => dest.ProductWarehouseInventoryModels, mo => mo.Ignore())
                    .ForMember(dest => dest.IsLoggedInAsVendor, mo => mo.Ignore())
                    //.ForMember(dest => dest.SeName, mo => mo.MapFrom(src => src.GetSeName(0, true, false)))
                    .ForMember(dest => dest.SeName, mo => mo.Ignore())
                    .ForMember(dest => dest.AvailableCustomerRoles, mo => mo.Ignore())
                    .ForMember(dest => dest.SelectedCustomerRoleIds, mo => mo.Ignore())
                    .ForMember(dest => dest.AvailableStores, mo => mo.Ignore())
                    .ForMember(dest => dest.SelectedStoreIds, mo => mo.Ignore())
                    .ForMember(dest => dest.AvailableTaxCategories, mo => mo.Ignore())
                    .ForMember(dest => dest.PrimaryStoreCurrencyCode, mo => mo.Ignore())
                    .ForMember(dest => dest.BaseDimensionIn, mo => mo.Ignore())
                    .ForMember(dest => dest.BaseWeightIn, mo => mo.Ignore())
                    .ForMember(dest => dest.Locales, mo => mo.Ignore())
                    .ForMember(dest => dest.AvailableDiscounts, mo => mo.Ignore())
                    .ForMember(dest => dest.SelectedCategoryIds, mo => mo.Ignore())
                    .ForMember(dest => dest.SelectedManufacturerIds, mo => mo.Ignore())
                    .ForMember(dest => dest.SelectedDiscountIds, mo => mo.Ignore())
                    .ForMember(dest => dest.AvailableDeliveryDates, mo => mo.Ignore())
                    .ForMember(dest => dest.AvailableProductAvailabilityRanges, mo => mo.Ignore())
                    .ForMember(dest => dest.AvailableWarehouses, mo => mo.Ignore())
                    .ForMember(dest => dest.AvailableBasepriceUnits, mo => mo.Ignore())
                    .ForMember(dest => dest.AvailableBasepriceBaseUnits, mo => mo.Ignore())
                    .ForMember(dest => dest.LastStockQuantity, mo => mo.Ignore())
                    .ForMember(dest => dest.ProductEditorSettingsModel, mo => mo.Ignore())
                    .ForMember(dest => dest.StockQuantityHistory, mo => mo.Ignore())
                    .ForMember(dest => dest.CustomProperties, mo => mo.Ignore());

                cfg.CreateMap<ProductVM, Product>()
                    .ForMember(dest => dest.ProductTags, mo => mo.Ignore())
                    .ForMember(dest => dest.CreatedOnUtc, mo => mo.Ignore())
                    .ForMember(dest => dest.UpdatedOnUtc, mo => mo.Ignore())
                    .ForMember(dest => dest.ParentGroupedProductId, mo => mo.Ignore())
                    .ForMember(dest => dest.ProductType, mo => mo.Ignore())
                    .ForMember(dest => dest.Deleted, mo => mo.Ignore())
                    .ForMember(dest => dest.ApprovedRatingSum, mo => mo.Ignore())
                    .ForMember(dest => dest.NotApprovedRatingSum, mo => mo.Ignore())
                    .ForMember(dest => dest.ApprovedTotalReviews, mo => mo.Ignore())
                    .ForMember(dest => dest.NotApprovedTotalReviews, mo => mo.Ignore())
                    .ForMember(dest => dest.ProductCategories, mo => mo.Ignore())
                    .ForMember(dest => dest.ProductManufacturers, mo => mo.Ignore())
                    .ForMember(dest => dest.ProductPictures, mo => mo.Ignore())
                    .ForMember(dest => dest.ProductReviews, mo => mo.Ignore())
                    .ForMember(dest => dest.ProductSpecificationAttributes, mo => mo.Ignore())
                    .ForMember(dest => dest.ProductWarehouseInventory, mo => mo.Ignore())
                    .ForMember(dest => dest.HasTierPrices, mo => mo.Ignore())
                    .ForMember(dest => dest.HasDiscountsApplied, mo => mo.Ignore())
                    .ForMember(dest => dest.BackorderMode, mo => mo.Ignore())
                    .ForMember(dest => dest.DownloadActivationType, mo => mo.Ignore())
                    .ForMember(dest => dest.GiftCardType, mo => mo.Ignore())
                    .ForMember(dest => dest.LowStockActivity, mo => mo.Ignore())
                    .ForMember(dest => dest.ManageInventoryMethod, mo => mo.Ignore())
                    .ForMember(dest => dest.RecurringCyclePeriod, mo => mo.Ignore())
                    .ForMember(dest => dest.RentalPricePeriod, mo => mo.Ignore())
                    .ForMember(dest => dest.ProductAttributeMappings, mo => mo.Ignore())
                    .ForMember(dest => dest.ProductAttributeCombinations, mo => mo.Ignore())
                    .ForMember(dest => dest.TierPrices, mo => mo.Ignore())
                    .ForMember(dest => dest.AppliedDiscounts, mo => mo.Ignore())
                    .ForMember(dest => dest.SubjectToAcl, mo => mo.Ignore())
                    .ForMember(dest => dest.LimitedToStores, mo => mo.Ignore());
                #endregion

                #region Product Attributes
                cfg.CreateMap<ProductAttribute, ProductAttributeVM>()
                    .ForMember(dest => dest.Locales, mo => mo.Ignore())
                    .ForMember(dest => dest.CustomProperties, mo => mo.Ignore());

                cfg.CreateMap<ProductAttributeVM, ProductAttribute>();
                #endregion

                #region Specification Attributes
                cfg.CreateMap<SpecificationAttribute, SpecificationAttributeVM>()
                    .ForMember(dest => dest.Locales, mo => mo.Ignore())
                    .ForMember(dest => dest.CustomProperties, mo => mo.Ignore());
                cfg.CreateMap<SpecificationAttributeVM, SpecificationAttribute>()
                    .ForMember(dest => dest.SpecificationAttributeOptions, mo => mo.Ignore());

                cfg.CreateMap<SpecificationAttributeOption, SpecificationAttributeOptionVM>()
                    .ForMember(dest => dest.NumberOfAssociatedProducts, mo => mo.Ignore())
                    .ForMember(dest => dest.Locales, mo => mo.Ignore())
                    .ForMember(dest => dest.EnableColorSquaresRgb, mo => mo.Ignore())
                    .ForMember(dest => dest.CustomProperties, mo => mo.Ignore());
                cfg.CreateMap<SpecificationAttributeOptionVM, SpecificationAttributeOption>()
                    .ForMember(dest => dest.SpecificationAttribute, mo => mo.Ignore());
                #endregion

                #region checkout attributes
                cfg.CreateMap<CheckoutAttribute, CheckoutAttributeVM>()
                    .ForMember(dest => dest.AvailableTaxCategories, mo => mo.Ignore())
                    .ForMember(dest => dest.AttributeControlTypeName, mo => mo.Ignore())
                    .ForMember(dest => dest.Locales, mo => mo.Ignore())
                    .ForMember(dest => dest.AvailableStores, mo => mo.Ignore())
                    .ForMember(dest => dest.SelectedStoreIds, mo => mo.Ignore())
                    .ForMember(dest => dest.ConditionAllowed, mo => mo.Ignore())
                    .ForMember(dest => dest.ConditionVM, mo => mo.Ignore())
                    .ForMember(dest => dest.CustomProperties, mo => mo.Ignore());
                cfg.CreateMap<CheckoutAttributeVM, CheckoutAttribute>()
                    .ForMember(dest => dest.AttributeControlType, mo => mo.Ignore())
                    .ForMember(dest => dest.ConditionAttributeXml, mo => mo.Ignore())
                    .ForMember(dest => dest.CheckoutAttributeValues, mo => mo.Ignore())
                    .ForMember(dest => dest.LimitedToStores, mo => mo.Ignore());
                #endregion

                #region customer attributes
                cfg.CreateMap<CustomerAttribute, CustomerAttributeVM>()
                   .ForMember(dest => dest.AttributeControlTypeName, mo => mo.Ignore())
                   .ForMember(dest => dest.Locales, mo => mo.Ignore())
                   .ForMember(dest => dest.CustomProperties, mo => mo.Ignore());
                cfg.CreateMap<CustomerAttributeVM, CustomerAttribute>()
                    .ForMember(dest => dest.AttributeControlType, mo => mo.Ignore())
                    .ForMember(dest => dest.CustomerAttributeValues, mo => mo.Ignore());
                #endregion

                #region Address
                //address
                cfg.CreateMap<Address, AddressVM>()
                    .ForMember(dest => dest.AddressHtml, mo => mo.Ignore())
                    .ForMember(dest => dest.CustomAddressAttributes, mo => mo.Ignore())
                    .ForMember(dest => dest.FormattedCustomAddressAttributes, mo => mo.Ignore())
                    .ForMember(dest => dest.AvailableCountries, mo => mo.Ignore())
                    .ForMember(dest => dest.AvailableStates, mo => mo.Ignore())
                    .ForMember(dest => dest.FirstNameEnabled, mo => mo.Ignore())
                    .ForMember(dest => dest.FirstNameRequired, mo => mo.Ignore())
                    .ForMember(dest => dest.LastNameEnabled, mo => mo.Ignore())
                    .ForMember(dest => dest.LastNameRequired, mo => mo.Ignore())
                    .ForMember(dest => dest.EmailEnabled, mo => mo.Ignore())
                    .ForMember(dest => dest.EmailRequired, mo => mo.Ignore())
                    .ForMember(dest => dest.CompanyEnabled, mo => mo.Ignore())
                    .ForMember(dest => dest.CompanyRequired, mo => mo.Ignore())
                    .ForMember(dest => dest.CountryEnabled, mo => mo.Ignore())
                    .ForMember(dest => dest.CountryRequired, mo => mo.Ignore())
                    .ForMember(dest => dest.StateProvinceEnabled, mo => mo.Ignore())
                    .ForMember(dest => dest.CityEnabled, mo => mo.Ignore())
                    .ForMember(dest => dest.CityRequired, mo => mo.Ignore())
                    .ForMember(dest => dest.StreetAddressEnabled, mo => mo.Ignore())
                    .ForMember(dest => dest.StreetAddressRequired, mo => mo.Ignore())
                    .ForMember(dest => dest.StreetAddress2Enabled, mo => mo.Ignore())
                    .ForMember(dest => dest.StreetAddress2Required, mo => mo.Ignore())
                    .ForMember(dest => dest.ZipPostalCodeEnabled, mo => mo.Ignore())
                    .ForMember(dest => dest.ZipPostalCodeRequired, mo => mo.Ignore())
                    .ForMember(dest => dest.PhoneEnabled, mo => mo.Ignore())
                    .ForMember(dest => dest.PhoneRequired, mo => mo.Ignore())
                    .ForMember(dest => dest.FaxEnabled, mo => mo.Ignore())
                    .ForMember(dest => dest.FaxRequired, mo => mo.Ignore())
                    .ForMember(dest => dest.CountryName,
                        mo => mo.MapFrom(src => src.Country != null ? src.Country.Name : null))
                    .ForMember(dest => dest.StateProvinceName,
                        mo => mo.MapFrom(src => src.StateProvince != null ? src.StateProvince.Name : null))
                    .ForMember(dest => dest.CustomProperties, mo => mo.Ignore());
                cfg.CreateMap<AddressVM, Address>()
                    .ForMember(dest => dest.CreatedOnUtc, mo => mo.Ignore())
                    .ForMember(dest => dest.Country, mo => mo.Ignore())
                    .ForMember(dest => dest.CustomAttributes, mo => mo.Ignore())
                    .ForMember(dest => dest.StateProvince, mo => mo.Ignore());

                #endregion

                #region address attributes
                cfg.CreateMap<AddressAttribute, AddressAttributeVM>()
                    .ForMember(dest => dest.AttributeControlTypeName, mo => mo.Ignore())
                    .ForMember(dest => dest.Locales, mo => mo.Ignore())
                    .ForMember(dest => dest.CustomProperties, mo => mo.Ignore());
                cfg.CreateMap<AddressAttributeVM, AddressAttribute>()
                    .ForMember(dest => dest.AttributeControlType, mo => mo.Ignore())
                    .ForMember(dest => dest.AddressAttributeValues, mo => mo.Ignore());
                #endregion

                #region Customer Roles
                //customer roles
                cfg.CreateMap<CustomerRole, CustomerRoleVM>()
                    .ForMember(dest => dest.PurchasedWithProductName, mo => mo.Ignore())
                    .ForMember(dest => dest.CustomProperties, mo => mo.Ignore());
                cfg.CreateMap<CustomerRoleVM, CustomerRole>()
                    .ForMember(dest => dest.PermissionRecords, mo => mo.Ignore());
                #endregion

                #region Vendor
                cfg.CreateMap<Vendor, VendorVM>()
                    .ForMember(dest => dest.AssociatedCustomers, mo => mo.Ignore())
                    .ForMember(dest => dest.Address, mo => mo.Ignore())
                    .ForMember(dest => dest.AddVendorNoteMessage, mo => mo.Ignore())
                    .ForMember(dest => dest.Locales, mo => mo.Ignore())
                    .ForMember(dest => dest.SeName, mo => mo.Ignore())
                    .ForMember(dest => dest.CustomProperties, mo => mo.Ignore());
                cfg.CreateMap<VendorVM, Vendor>()
                    .ForMember(dest => dest.VendorNotes, mo => mo.Ignore())
                    .ForMember(dest => dest.Deleted, mo => mo.Ignore());
                #endregion

                #region Queued email
                cfg.CreateMap<QueuedEmail, QueuedEmailVM>()
                    .ForMember(dest => dest.EmailAccountName,
                        mo => mo.MapFrom(src => src.EmailAccount != null ? src.EmailAccount.FriendlyName : string.Empty))
                    .ForMember(dest => dest.CreatedOn, mo => mo.Ignore())
                    .ForMember(dest => dest.PriorityName, mo => mo.Ignore())
                    .ForMember(dest => dest.DontSendBeforeDate, mo => mo.Ignore())
                    .ForMember(dest => dest.SendImmediately, mo => mo.Ignore())
                    .ForMember(dest => dest.SentOn, mo => mo.Ignore())
                    .ForMember(dest => dest.CustomProperties, mo => mo.Ignore());
                cfg.CreateMap<QueuedEmailVM, QueuedEmail>()
                    .ForMember(dest => dest.Priority, dt => dt.Ignore())
                    .ForMember(dest => dest.PriorityId, dt => dt.Ignore())
                    .ForMember(dest => dest.CreatedOnUtc, dt => dt.Ignore())
                    .ForMember(dest => dest.DontSendBeforeDateUtc, mo => mo.Ignore())
                    .ForMember(dest => dest.SentOnUtc, mo => mo.Ignore())
                    .ForMember(dest => dest.EmailAccount, mo => mo.Ignore())
                    .ForMember(dest => dest.EmailAccountId, mo => mo.Ignore())
                    .ForMember(dest => dest.AttachmentFilePath, mo => mo.Ignore())
                    .ForMember(dest => dest.AttachmentFileName, mo => mo.Ignore());
                #endregion

                #region Email Account
                cfg.CreateMap<EmailAccount, EmailAccountVM>()
                    .ForMember(dest => dest.Password, mo => mo.Ignore())
                    .ForMember(dest => dest.IsDefaultEmailAccount, mo => mo.Ignore())
                    .ForMember(dest => dest.SendTestEmailTo, mo => mo.Ignore())
                    .ForMember(dest => dest.CustomProperties, mo => mo.Ignore());
                cfg.CreateMap<EmailAccountVM, EmailAccount>()
                    .ForMember(dest => dest.Password, mo => mo.Ignore());
                #endregion

                #region Stores
                cfg.CreateMap<Store, StoreVM>()
                    .ForMember(dest => dest.AvailableLanguages, mo => mo.Ignore())
                    .ForMember(dest => dest.Locales, mo => mo.Ignore())
                    .ForMember(dest => dest.CustomProperties, mo => mo.Ignore());
                cfg.CreateMap<StoreVM, Store>();
                #endregion

                #region Settings

                #region Product Editor Settings
                cfg.CreateMap<ProductEditorSettings, ProductEditorSettingsVM>()
                    .ForMember(dest => dest.CustomProperties, mo => mo.Ignore());
                cfg.CreateMap<ProductEditorSettingsVM, ProductEditorSettings>();
                #endregion

                #region VendorSettings
                cfg.CreateMap<VendorSettings, VendorSettingsVM>()
                    .ForMember(dest => dest.ActiveStoreScopeConfiguration, mo => mo.Ignore())
                    .ForMember(dest => dest.VendorsBlockItemsToDisplay_OverrideForStore, mo => mo.Ignore())
                    .ForMember(dest => dest.ShowVendorOnProductDetailsPage_OverrideForStore, mo => mo.Ignore())
                    .ForMember(dest => dest.AllowCustomersToContactVendors_OverrideForStore, mo => mo.Ignore())
                    .ForMember(dest => dest.AllowCustomersToApplyForVendorAccount_OverrideForStore, mo => mo.Ignore())
                    .ForMember(dest => dest.AllowSearchByVendor_OverrideForStore, mo => mo.Ignore())
                    .ForMember(dest => dest.AllowVendorsToEditInfo_OverrideForStore, mo => mo.Ignore())
                    .ForMember(dest => dest.NotifyStoreOwnerAboutVendorInformationChange_OverrideForStore, mo => mo.Ignore())
                    .ForMember(dest => dest.CustomProperties, mo => mo.Ignore())
                    .ForMember(dest => dest.MaximumProductNumber_OverrideForStore, mo => mo.Ignore())
                    .ForMember(dest => dest.AllowVendorsToImportProducts_OverrideForStore, mo => mo.Ignore());
                cfg.CreateMap<VendorSettingsVM, VendorSettings>()
                    .ForMember(dest => dest.DefaultVendorPageSizeOptions, mo => mo.Ignore());

                #endregion

                #region Shippings
                cfg.CreateMap<ShippingSettings, ShippingSettingsVM>()
                    .ForMember(dest => dest.ShippingOriginAddress, mo => mo.Ignore())
                    .ForMember(dest => dest.ActiveStoreScopeConfiguration, mo => mo.Ignore())
                    .ForMember(dest => dest.AllowPickUpInStore_OverrideForStore, mo => mo.Ignore())
                    .ForMember(dest => dest.ShipToSameAddress_OverrideForStore, mo => mo.Ignore())
                    .ForMember(dest => dest.DisplayPickupPointsOnMap_OverrideForStore, mo => mo.Ignore())
                    .ForMember(dest => dest.GoogleMapsApiKey_OverrideForStore, mo => mo.Ignore())
                    .ForMember(dest => dest.UseWarehouseLocation_OverrideForStore, mo => mo.Ignore())
                    .ForMember(dest => dest.NotifyCustomerAboutShippingFromMultipleLocations_OverrideForStore,
                        mo => mo.Ignore())
                    .ForMember(dest => dest.FreeShippingOverXEnabled_OverrideForStore, mo => mo.Ignore())
                    .ForMember(dest => dest.FreeShippingOverXValue_OverrideForStore, mo => mo.Ignore())
                    .ForMember(dest => dest.FreeShippingOverXIncludingTax_OverrideForStore, mo => mo.Ignore())
                    .ForMember(dest => dest.EstimateShippingEnabled_OverrideForStore, mo => mo.Ignore())
                    .ForMember(dest => dest.DisplayShipmentEventsToCustomers_OverrideForStore, mo => mo.Ignore())
                    .ForMember(dest => dest.DisplayShipmentEventsToStoreOwner_OverrideForStore, mo => mo.Ignore())
                    .ForMember(dest => dest.HideShippingTotal_OverrideForStore, mo => mo.Ignore())
                    .ForMember(dest => dest.BypassShippingMethodSelectionIfOnlyOne_OverrideForStore, mo => mo.Ignore())
                    .ForMember(dest => dest.ConsiderAssociatedProductsDimensions_OverrideForStore, mo => mo.Ignore())
                    .ForMember(dest => dest.ShippingOriginAddress_OverrideForStore, mo => mo.Ignore())
                    .ForMember(dest => dest.CustomProperties, mo => mo.Ignore());
                cfg.CreateMap<ShippingSettingsVM, ShippingSettings>()
                    .ForMember(dest => dest.ActiveShippingRateComputationMethodSystemNames, mo => mo.Ignore())
                    .ForMember(dest => dest.ActivePickupPointProviderSystemNames, mo => mo.Ignore())
                    .ForMember(dest => dest.ReturnValidOptionsIfThereAreAny, mo => mo.Ignore())
                    .ForMember(dest => dest.UseCubeRootMethod, mo => mo.Ignore());
                #endregion

                #endregion

                #region Return request reason
                cfg.CreateMap<ReturnRequestReason, ReturnRequestReasonVM>()
                    .ForMember(dest => dest.Locales, mo => mo.Ignore())
                    .ForMember(dest => dest.CustomProperties, mo => mo.Ignore());
                cfg.CreateMap<ReturnRequestReasonVM, ReturnRequestReason>();
                #endregion

            };
            return action;
        }
        /// <summary>
        /// Order of this mapper implementation
        /// </summary>
        public int Order
        {
            get { return 0; }
        }
    }
}
