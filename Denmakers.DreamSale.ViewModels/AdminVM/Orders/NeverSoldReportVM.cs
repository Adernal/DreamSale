using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Denmakers.DreamSale.ViewModels.AdminVM.Orders
{
    public partial class NeverSoldReportVM
    {
        public NeverSoldReportVM()
        {
            AvailableCategories = new List<SelectListItem>();
            AvailableManufacturers = new List<SelectListItem>();
            AvailableStores = new List<SelectListItem>();
            AvailableVendors = new List<SelectListItem>();
            CustomProperties = new Dictionary<string, object>();
        }

        public int Id { get; set; }
        public Dictionary<string, object> CustomProperties { get; set; }

        [DisplayName("Admin.SalesReport.NeverSold.StartDate")]
        [UIHint("DateNullable")]
        public DateTime? StartDate { get; set; }

        [DisplayName("Admin.SalesReport.NeverSold.EndDate")]
        [UIHint("DateNullable")]
        public DateTime? EndDate { get; set; }

        [DisplayName("Admin.SalesReport.NeverSold.SearchCategory")]
        public int SearchCategoryId { get; set; }
        public IList<SelectListItem> AvailableCategories { get; set; }

        [DisplayName("Admin.SalesReport.NeverSold.SearchManufacturer")]
        public int SearchManufacturerId { get; set; }
        public IList<SelectListItem> AvailableManufacturers { get; set; }

        [DisplayName("Admin.SalesReport.NeverSold.SearchStore")]
        public int SearchStoreId { get; set; }
        public IList<SelectListItem> AvailableStores { get; set; }

        [DisplayName("Admin.SalesReport.NeverSold.SearchVendor")]
        public int SearchVendorId { get; set; }
        public IList<SelectListItem> AvailableVendors { get; set; }

        public bool IsLoggedInAsVendor { get; set; }
    }
}
