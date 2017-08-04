using System.ComponentModel;

namespace Denmakers.DreamSale.ViewModels.AdminVM.Orders
{
    public partial class BestsellersReportLineVM
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        [DisplayName("Product name")]
        public string ProductName { get; set; }

        [DisplayName("Total amount")]
        public string TotalAmount { get; set; }

        [DisplayName("Total quantity")]
        public decimal TotalQuantity { get; set; }
    }
}
