using Denmakers.DreamSale.ViewModels.AdminVM.Addresses;

namespace Denmakers.DreamSale.ViewModels.AdminVM.Customers
{
    public partial class CustomerAddressVM
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }

        public AddressVM Address { get; set; }
    }
}
