using System.ComponentModel;

namespace Denmakers.DreamSale.ViewModels.AdminVM.Customers
{
    public partial class RegisteredCustomerReportLineVM
    {
        public int Id { get; set; }

        [DisplayName("Period")]
        public string Period { get; set; }

        [DisplayName("Customers")]
        public int Customers { get; set; }
    }
}
