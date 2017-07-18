using System;
using System.ComponentModel;

namespace Denmakers.DreamSale.ViewModels.AdminVM.ShoppingCart
{
    public partial class ShoppingCartItemVM
    {
        public int Id { get; set; }
        [DisplayName("Admin.CurrentCarts.Store")]
        public string Store { get; set; }
        [DisplayName("Admin.CurrentCarts.Product")]
        public int ProductId { get; set; }
        [DisplayName("Admin.CurrentCarts.Product")]
        public string ProductName { get; set; }
        public string AttributeInfo { get; set; }

        [DisplayName("Admin.CurrentCarts.UnitPrice")]
        public string UnitPrice { get; set; }
        [DisplayName("Admin.CurrentCarts.Quantity")]
        public int Quantity { get; set; }
        [DisplayName("Admin.CurrentCarts.Total")]
        public string Total { get; set; }
        [DisplayName("Admin.CurrentCarts.UpdatedOn")]
        public DateTime UpdatedOn { get; set; }
    }
}
