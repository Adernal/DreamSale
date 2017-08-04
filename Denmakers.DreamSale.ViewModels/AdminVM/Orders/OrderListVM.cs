using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Denmakers.DreamSale.ViewModels.AdminVM.Orders
{
    public partial class OrderListVM
    {
        public OrderListVM()
        {
            OrderStatusIds = new List<int>();
            PaymentStatusIds = new List<int>();
            ShippingStatusIds = new List<int>();
            AvailableOrderStatuses = new List<SelectListItem>();
            AvailablePaymentStatuses = new List<SelectListItem>();
            AvailableShippingStatuses = new List<SelectListItem>();
            AvailableStores = new List<SelectListItem>();
            AvailableVendors = new List<SelectListItem>();
            AvailableWarehouses = new List<SelectListItem>();
            AvailablePaymentMethods = new List<SelectListItem>();
            AvailableCountries = new List<SelectListItem>();
            CustomProperties = new Dictionary<string, object>();
        }

        public int Id { get; set; }
        public Dictionary<string, object> CustomProperties { get; set; }
        [DisplayName("Admin.Orders.List.StartDate")]
        [UIHint("DateNullable")]
        public DateTime? StartDate { get; set; }

        [DisplayName("Admin.Orders.List.EndDate")]
        [UIHint("DateNullable")]
        public DateTime? EndDate { get; set; }

        [DisplayName("Admin.Orders.List.OrderStatus")]
        [UIHint("MultiSelect")]
        public List<int> OrderStatusIds { get; set; }

        [DisplayName("Admin.Orders.List.PaymentStatus")]
        [UIHint("MultiSelect")]
        public List<int> PaymentStatusIds { get; set; }

        [DisplayName("Admin.Orders.List.ShippingStatus")]
        [UIHint("MultiSelect")]
        public List<int> ShippingStatusIds { get; set; }

        [DisplayName("Admin.Orders.List.PaymentMethod")]
        public string PaymentMethodSystemName { get; set; }

        [DisplayName("Admin.Orders.List.Store")]
        public int StoreId { get; set; }

        [DisplayName("Admin.Orders.List.Vendor")]
        public int VendorId { get; set; }

        [DisplayName("Admin.Orders.List.Warehouse")]
        public int WarehouseId { get; set; }

        [DisplayName("Admin.Orders.List.Product")]
        public int ProductId { get; set; }

        [DisplayName("Admin.Orders.List.BillingEmail")]
        [AllowHtml]
        public string BillingEmail { get; set; }

        [DisplayName("Admin.Orders.List.BillingLastName")]
        [AllowHtml]
        public string BillingLastName { get; set; }

        [DisplayName("Admin.Orders.List.BillingCountry")]
        public int BillingCountryId { get; set; }

        [DisplayName("Admin.Orders.List.OrderNotes")]
        [AllowHtml]
        public string OrderNotes { get; set; }

        [DisplayName("Admin.Orders.List.GoDirectlyToNumber")]
        public string GoDirectlyToCustomOrderNumber { get; set; }

        public bool IsLoggedInAsVendor { get; set; }


        public IList<SelectListItem> AvailableOrderStatuses { get; set; }
        public IList<SelectListItem> AvailablePaymentStatuses { get; set; }
        public IList<SelectListItem> AvailableShippingStatuses { get; set; }
        public IList<SelectListItem> AvailableStores { get; set; }
        public IList<SelectListItem> AvailableVendors { get; set; }
        public IList<SelectListItem> AvailableWarehouses { get; set; }
        public IList<SelectListItem> AvailablePaymentMethods { get; set; }
        public IList<SelectListItem> AvailableCountries { get; set; }
    }
}
