using Denmakers.DreamSale.ViewModels.NonAdminVM.Addresses;
using System.Collections.Generic;

namespace Denmakers.DreamSale.ViewModels.NonAdminVM.Customers
{
    public partial class CustomerAddressListModel
    {
        public CustomerAddressListModel()
        {
            Addresses = new List<AddressModel>();
        }
        public int Id { get; set; }
        public IList<AddressModel> Addresses { get; set; }
    }
}
