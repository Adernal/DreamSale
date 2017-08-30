using Denmakers.DreamSale.ViewModels.NonAdminVM.Media;
using System.Collections.Generic;

namespace Denmakers.DreamSale.ViewModels.NonAdminVM.ShoppingCart
{
    public partial class MiniShoppingCartModel
    {
        public MiniShoppingCartModel()
        {
            Items = new List<ShoppingCartItemModel>();
        }
        public int Id { get; set; }
        public IList<ShoppingCartItemModel> Items { get; set; }
        public int TotalProducts { get; set; }
        public string SubTotal { get; set; }
        public bool DisplayShoppingCartButton { get; set; }
        public bool DisplayCheckoutButton { get; set; }
        public bool CurrentCustomerIsGuest { get; set; }
        public bool AnonymousCheckoutAllowed { get; set; }
        public bool ShowProductImages { get; set; }


        #region Nested Classes

        public partial class ShoppingCartItemModel
        {
            public ShoppingCartItemModel()
            {
                Picture = new PictureModel();
            }
            public int Id { get; set; }
            public int ProductId { get; set; }

            public string ProductName { get; set; }

            public string ProductSeName { get; set; }

            public int Quantity { get; set; }

            public string UnitPrice { get; set; }

            public string AttributeInfo { get; set; }

            public PictureModel Picture { get; set; }
        }

        #endregion
    }
}
