using System.ComponentModel;

namespace Denmakers.DreamSale.ViewModels.AdminVM.Orders
{
    public partial class OrderIncompleteReportLineVM
    {
        public int Id { get; set; }
        [DisplayName("Admin.SalesReport.Incomplete.Item")]
        public string Item { get; set; }

        [DisplayName("Admin.SalesReport.Incomplete.Total")]
        public string Total { get; set; }

        [DisplayName("Admin.SalesReport.Incomplete.Count")]
        public int Count { get; set; }

        [DisplayName("Admin.SalesReport.Incomplete.View")]
        public string ViewLink { get; set; }
    }
}
