using System.ComponentModel;

namespace Denmakers.DreamSale.ViewModels.AdminVM.Customers
{
    public partial class BestCustomerReportLineVM
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        [DisplayName("Customer")]
        public string CustomerName { get; set; }

        [DisplayName("Order total")]
        public string OrderTotal { get; set; }

        [DisplayName("Number of orders")]
        public decimal OrderCount { get; set; }
    }
}
