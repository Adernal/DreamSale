using Denmakers.DreamSale.ViewModels.AdminVM.Addresses;
namespace Denmakers.DreamSale.ViewModels.AdminVM.Orders
{
    public partial class OrderAddressVM
    {
        public int Id { get; set; }
        public int OrderId { get; set; }

        public AddressVM Address { get; set; }
    }
}
