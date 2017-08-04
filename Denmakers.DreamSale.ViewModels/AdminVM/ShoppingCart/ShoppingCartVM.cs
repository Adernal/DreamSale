using System.ComponentModel;

namespace Denmakers.DreamSale.ViewModels.AdminVM.ShoppingCart
{
    public partial class ShoppingCartVM 
    {
        public int Id { get; set; }
        [DisplayName("Customer")]
        public int CustomerId { get; set; }
        [DisplayName("Customer")]
        public string CustomerEmail { get; set; }

        [DisplayName("Total items")]
        public int TotalItems { get; set; }
    }
}
