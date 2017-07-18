using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Denmakers.DreamSale.ViewModels.AdminVM.Customers
{
    public partial class BestCustomersReportVM
    {
        public BestCustomersReportVM()
        {
            AvailableOrderStatuses = new List<System.Web.Mvc.SelectListItem>();
            AvailablePaymentStatuses = new List<System.Web.Mvc.SelectListItem>();
            AvailableShippingStatuses = new List<System.Web.Mvc.SelectListItem>();
        }
        public int Id { get; set; }
        [DisplayName("Admin.Customers.Reports.BestBy.StartDate")]
        [UIHint("DateNullable")]
        public DateTime? StartDate { get; set; }

        [DisplayName("Admin.Customers.Reports.BestBy.EndDate")]
        [UIHint("DateNullable")]
        public DateTime? EndDate { get; set; }

        [DisplayName("Admin.Customers.Reports.BestBy.OrderStatus")]
        public int OrderStatusId { get; set; }
        [DisplayName("Admin.Customers.Reports.BestBy.PaymentStatus")]
        public int PaymentStatusId { get; set; }
        [DisplayName("Admin.Customers.Reports.BestBy.ShippingStatus")]
        public int ShippingStatusId { get; set; }

        public IList<System.Web.Mvc.SelectListItem> AvailableOrderStatuses { get; set; }
        public IList<System.Web.Mvc.SelectListItem> AvailablePaymentStatuses { get; set; }
        public IList<System.Web.Mvc.SelectListItem> AvailableShippingStatuses { get; set; }
    }
}
