using System;
using System.ComponentModel;
using System.Web.Mvc;

namespace Denmakers.DreamSale.ViewModels.AdminVM.Logging
{
    public partial class LogVM
    {
        public int Id { get; set; }
        [DisplayName("Admin.System.Log.Fields.LogLevel")]
        public string LogLevel { get; set; }

        [DisplayName("Admin.System.Log.Fields.ShortMessage")]
        [AllowHtml]
        public string ShortMessage { get; set; }

        [DisplayName("Admin.System.Log.Fields.FullMessage")]
        [AllowHtml]
        public string FullMessage { get; set; }

        [DisplayName("Admin.System.Log.Fields.IPAddress")]
        [AllowHtml]
        public string IpAddress { get; set; }

        [DisplayName("Admin.System.Log.Fields.Customer")]
        public int? CustomerId { get; set; }
        [DisplayName("Admin.System.Log.Fields.Customer")]
        public string CustomerEmail { get; set; }

        [DisplayName("Admin.System.Log.Fields.PageURL")]
        [AllowHtml]
        public string PageUrl { get; set; }

        [DisplayName("Admin.System.Log.Fields.ReferrerURL")]
        [AllowHtml]
        public string ReferrerUrl { get; set; }

        [DisplayName("Admin.System.Log.Fields.CreatedOn")]
        public DateTime CreatedOn { get; set; }
    }
}
