using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;

namespace Denmakers.DreamSale.ViewModels.AdminVM.Catalog
{
    public partial class BulkEditListVM
    {
        public BulkEditListVM()
        {
            AvailableCategories = new List<SelectListItem>();
            AvailableManufacturers = new List<SelectListItem>();
            AvailableProductTypes = new List<SelectListItem>();
            CustomProperties = new Dictionary<string, object>();
        }

        public int Id { get; set; }
        public Dictionary<string, object> CustomProperties { get; set; }
        [DisplayName("Admin.Catalog.BulkEdit.List.SearchProductName")]
        [AllowHtml]
        public string SearchProductName { get; set; }

        [DisplayName("Admin.Catalog.BulkEdit.List.SearchCategory")]
        public int SearchCategoryId { get; set; }

        [DisplayName("Admin.Catalog.BulkEdit.List.SearchManufacturer")]
        public int SearchManufacturerId { get; set; }
        [DisplayName("Admin.Catalog.Products.List.SearchProductType")]
        public int SearchProductTypeId { get; set; }
        public IList<SelectListItem> AvailableProductTypes { get; set; }


        public IList<SelectListItem> AvailableCategories { get; set; }
        public IList<SelectListItem> AvailableManufacturers { get; set; }
    }
}
