using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using FluentValidation.Attributes;
using Denmakers.DreamSale.ViewModels.Validators.Catalog;

namespace Denmakers.DreamSale.ViewModels.AdminVM.Catalog
{
    [Validator(typeof(CategoryValidator))]
    public partial class CategoryVM
    {
        #region Properties
        public int Id { get; set; }
        public Dictionary<string, object> CustomProperties { get; set; }

        [DisplayName("Category name")]
        [AllowHtml]
        public string Name { get; set; }

        [DisplayName("Category description")]
        [AllowHtml]
        public string Description { get; set; }

        [DisplayName("Category Template")]
        public int CategoryTemplateId { get; set; }
        public IList<SelectListItem> AvailableCategoryTemplates { get; set; }

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

        [DisplayName("Parent")]
        public int ParentCategoryId { get; set; }

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

        [DisplayName("Show on home page")]
        public bool ShowOnHomePage { get; set; }

        [DisplayName("Include in top menu")]
        public bool IncludeInTopMenu { get; set; }

        [DisplayName("Published")]
        public bool Published { get; set; }

        [DisplayName("Deleted")]
        public bool Deleted { get; set; }

        [DisplayName("Display order")]
        public int DisplayOrder { get; set; }

        public IList<CategoryLocalizedVM> Locales { get; set; }

        public string Breadcrumb { get; set; }

        //ACL (customer roles)
        [DisplayName("Acl sustomer roles")]
        [UIHint("MultiSelect")]
        public IList<int> SelectedCustomerRoleIds { get; set; }
        public IList<SelectListItem> AvailableCustomerRoles { get; set; }

        //store mapping
        [DisplayName("Limited to stores")]
        [UIHint("MultiSelect")]
        public IList<int> SelectedStoreIds { get; set; }
        public IList<SelectListItem> AvailableStores { get; set; }


        public IList<SelectListItem> AvailableCategories { get; set; }


        //discounts
        [DisplayName("Discounts")]
        [UIHint("MultiSelect")]
        public IList<int> SelectedDiscountIds { get; set; }
        public IList<SelectListItem> AvailableDiscounts { get; set; }


        #endregion
        public CategoryVM()
        {
            if (PageSize < 1)
            {
                PageSize = 5;
            }
            Locales = new List<CategoryLocalizedVM>();
            AvailableCategoryTemplates = new List<SelectListItem>();
            AvailableCategories = new List<SelectListItem>();
            AvailableDiscounts = new List<SelectListItem>();
            SelectedDiscountIds = new List<int>();

            SelectedCustomerRoleIds = new List<int>();
            AvailableCustomerRoles = new List<SelectListItem>();

            SelectedStoreIds = new List<int>();
            AvailableStores = new List<SelectListItem>();
            CustomProperties = new Dictionary<string, object>();
        }
        
        #region Nested classes

        public partial class CategoryProductVM
        {
            public int Id { get; set; }

            public Dictionary<string, object> CustomProperties { get; set; }

            public int CategoryId { get; set; }

            public int ProductId { get; set; }

            [DisplayName("Product")]
            public string ProductName { get; set; }

            [DisplayName("Is featured product")]
            public bool IsFeaturedProduct { get; set; }

            [DisplayName("Display order")]
            public int DisplayOrder { get; set; }
        }

        public partial class AddCategoryProductVM
        {
            public AddCategoryProductVM()
            {
                AvailableCategories = new List<SelectListItem>();
                AvailableManufacturers = new List<SelectListItem>();
                AvailableStores = new List<SelectListItem>();
                AvailableVendors = new List<SelectListItem>();
                AvailableProductTypes = new List<SelectListItem>();
                CustomProperties = new Dictionary<string, object>();
            }
            public int Id { get; set; }
            [DisplayName("Product Name")]
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

            public int CategoryId { get; set; }

            public int[] SelectedProductIds { get; set; }
            public Dictionary<string, object> CustomProperties { get; set; }
        }

        #endregion
    }

    public partial class CategoryLocalizedVM
    {
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