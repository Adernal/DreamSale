using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;

namespace Denmakers.DreamSale.ViewModels.AdminVM.Catalog
{
    public partial class ProductListVM
    {
        public ProductListVM()
        {
            AvailableCategories = new List<SelectListItem>();
            AvailableManufacturers = new List<SelectListItem>();
            AvailableStores = new List<SelectListItem>();
            AvailableWarehouses = new List<SelectListItem>();
            AvailableVendors = new List<SelectListItem>();
            AvailableProductTypes = new List<SelectListItem>();
            AvailablePublishedOptions = new List<SelectListItem>();
            CustomProperties = new Dictionary<string, object>();
        }

        public int Id { get; set; }
        public Dictionary<string, object> CustomProperties { get; set; }

        [DisplayName("Product name")]
        [AllowHtml]
        public string SearchProductName { get; set; }
        [DisplayName("Category")]
        public int SearchCategoryId { get; set; }
        [DisplayName("ASearch sub categories")]
        public bool SearchIncludeSubCategories { get; set; }
        [DisplayName("Manufacturer")]
        public int SearchManufacturerId { get; set; }
        [DisplayName("Store")]
        public int SearchStoreId { get; set; }
        [DisplayName("Vendor")]
        public int SearchVendorId { get; set; }
        [DisplayName("Warehouse")]
        public int SearchWarehouseId { get; set; }
        [DisplayName("Product type")]
        public int SearchProductTypeId { get; set; }
        [DisplayName("Published")]
        public int SearchPublishedId { get; set; }

        [DisplayName("Go directly to product SKU")]
        [AllowHtml]
        public string GoDirectlyToSku { get; set; }

        public bool IsLoggedInAsVendor { get; set; }

        public bool AllowVendorsToImportProducts { get; set; }

        public IList<SelectListItem> AvailableCategories { get; set; }
        public IList<SelectListItem> AvailableManufacturers { get; set; }
        public IList<SelectListItem> AvailableStores { get; set; }
        public IList<SelectListItem> AvailableWarehouses { get; set; }
        public IList<SelectListItem> AvailableVendors { get; set; }
        public IList<SelectListItem> AvailableProductTypes { get; set; }
        public IList<SelectListItem> AvailablePublishedOptions { get; set; }
    }
}
