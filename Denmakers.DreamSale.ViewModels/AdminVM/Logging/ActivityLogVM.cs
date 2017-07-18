using System;
using System.ComponentModel;

namespace Denmakers.DreamSale.ViewModels.AdminVM.Logging
{
    public partial class ActivityLogVM
    {
        public int Id { get; set; }
        [DisplayName("Admin.Configuration.ActivityLog.ActivityLog.Fields.ActivityLogType")]
        public string ActivityLogTypeName { get; set; }
        [DisplayName("Admin.Configuration.ActivityLog.ActivityLog.Fields.Customer")]
        public int CustomerId { get; set; }
        [DisplayName("Admin.Configuration.ActivityLog.ActivityLog.Fields.Customer")]
        public string CustomerEmail { get; set; }
        [DisplayName("Admin.Configuration.ActivityLog.ActivityLog.Fields.Comment")]
        public string Comment { get; set; }
        [DisplayName("Admin.Configuration.ActivityLog.ActivityLog.Fields.CreatedOn")]
        public DateTime CreatedOn { get; set; }
        [DisplayName("Admin.Customers.Customers.ActivityLog.IpAddress")]
        public string IpAddress { get; set; }
    }
}
