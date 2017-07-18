using Denmakers.DreamSale.ViewModels.Validators.Catalog;
using FluentValidation.Attributes;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Denmakers.DreamSale.ViewModels.AdminVM.Catalog
{
    [Validator(typeof(ManufacturerValidator))]
    public partial class ManufacturerVM
    {
        public ManufacturerVM()
        {
            if (PageSize < 1)
            {
                PageSize = 5;
            }
            Locales = new List<ManufacturerLocalizedVM>();
            AvailableManufacturerTemplates = new List<SelectListItem>();

            AvailableDiscounts = new List<SelectListItem>();
            SelectedDiscountIds = new List<int>();

            SelectedCustomerRoleIds = new List<int>();
            AvailableCustomerRoles = new List<SelectListItem>();

            SelectedStoreIds = new List<int>();
            AvailableStores = new List<SelectListItem>();
            CustomProperties = new Dictionary<string, object>();
        }

        public int Id { get; set; }
        public Dictionary<string, object> CustomProperties { get; set; }

        [DisplayName("Name")]
        [AllowHtml]
        public string Name { get; set; }

        [DisplayName("Description")]
        [AllowHtml]
        public string Description { get; set; }

        [DisplayName("Manufacturer template")]
        public int ManufacturerTemplateId { get; set; }
        public IList<SelectListItem> AvailableManufacturerTemplates { get; set; }

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

        [UIHint("Picture")]
        [DisplayName("Picture")]
        public int PictureId { get; set; }

        [DisplayName("PageSize")]
        public int PageSize { get; set; }

        [DisplayName("Allow customers to select page size")]
        public bool AllowCustomersToSelectPageSize { get; set; }

        [DisplayName("Page size options")]
        public string PageSizeOptions { get; set; }

        [DisplayName("Price ranges")]
        [AllowHtml]
        public string PriceRanges { get; set; }

        [DisplayName("Published")]
        public bool Published { get; set; }

        [DisplayName("Deleted")]
        public bool Deleted { get; set; }

        [DisplayName("Display order")]
        public int DisplayOrder { get; set; }

        public IList<ManufacturerLocalizedVM> Locales { get; set; }


        //ACL (customer roles)
        [DisplayName("Limited to customer roles")]
        [UIHint("MultiSelect")]
        public IList<int> SelectedCustomerRoleIds { get; set; }
        public IList<SelectListItem> AvailableCustomerRoles { get; set; }


        //store mapping
        [DisplayName("Limited to stores")]
        [UIHint("MultiSelect")]
        public IList<int> SelectedStoreIds { get; set; }
        public IList<SelectListItem> AvailableStores { get; set; }


        //discounts
        [DisplayName("Discounts")]
        [UIHint("MultiSelect")]
        public IList<int> SelectedDiscountIds { get; set; }
        public IList<SelectListItem> AvailableDiscounts { get; set; }


        #region Nested classes

        public partial class ManufacturerProductVM
        {
            public int Id { get; set; }

            public Dictionary<string, object> CustomProperties { get; set; }

            public int ManufacturerId { get; set; }

            public int ProductId { get; set; }

            [DisplayName("Product")]
            public string ProductName { get; set; }

            [DisplayName("Is featured Product")]
            public bool IsFeaturedProduct { get; set; }

            [DisplayName("Display order")]
            public int DisplayOrder { get; set; }
        }

        public partial class AddManufacturerProductVM
        {
            public AddManufacturerProductVM()
            {
                AvailableCategories = new List<SelectListItem>();
                AvailableManufacturers = new List<SelectListItem>();
                AvailableStores = new List<SelectListItem>();
                AvailableVendors = new List<SelectListItem>();
                AvailableProductTypes = new List<SelectListItem>();
                CustomProperties = new Dictionary<string, object>();
            }

            public int Id { get; set; }

            [DisplayName("Product name")]
            [AllowHtml]
            public string SearchProductName { get; set; }
            [DisplayName("Category")]
            public int SearchCategoryId { get; set; }
            [DisplayName("Manufacturer")]
            public int SearchManufacturerId { get; set; }
            [DisplayName("Store")]
            public int SearchStoreId { get; set; }
            [DisplayName("Vendor")]
            public int SearchVendorId { get; set; }
            [DisplayName("Product type")]
            public int SearchProductTypeId { get; set; }

            public IList<SelectListItem> AvailableCategories { get; set; }
            public IList<SelectListItem> AvailableManufacturers { get; set; }
            public IList<SelectListItem> AvailableStores { get; set; }
            public IList<SelectListItem> AvailableVendors { get; set; }
            public IList<SelectListItem> AvailableProductTypes { get; set; }

            public int ManufacturerId { get; set; }

            public int[] SelectedProductIds { get; set; }

            public Dictionary<string, object> CustomProperties { get; set; }
        }

        #endregion
    }

    public partial class ManufacturerLocalizedVM
    {
        public int Id { get; set; }

        public int LanguageId { get; set; }

        [DisplayName("Name")]
        [AllowHtml]
        public string Name { get; set; }

        [DisplayName("Description")]
        [AllowHtml]
        public string Description { get; set; }

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
    }
}