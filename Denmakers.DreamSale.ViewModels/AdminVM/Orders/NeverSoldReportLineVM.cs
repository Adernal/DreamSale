using System.ComponentModel;

namespace Denmakers.DreamSale.ViewModels.AdminVM.Orders
{
    public partial class NeverSoldReportLineVM
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        [DisplayName("Admin.SalesReport.NeverSold.Fields.Name")]
        public string ProductName { get; set; }
    }
}
