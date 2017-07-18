namespace Denmakers.DreamSale.ViewModels.AdminVM.Customers
{
    public partial class CustomerReportsVM
    {
        public int Id { get; set; }
        public BestCustomersReportVM BestCustomersByOrderTotal { get; set; }
        public BestCustomersReportVM BestCustomersByNumberOfOrders { get; set; }
    }
}
