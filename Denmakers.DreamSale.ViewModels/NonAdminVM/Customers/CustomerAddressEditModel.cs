using Denmakers.DreamSale.ViewModels.NonAdminVM.Addresses;

namespace Denmakers.DreamSale.ViewModels.NonAdminVM.Customers
{
    public partial class CustomerAddressEditModel
    {
        public CustomerAddressEditModel()
        {
            this.Address = new AddressModel();
        }
        public int Id { get; set; }
        public AddressModel Address { get; set; }
    }
}
