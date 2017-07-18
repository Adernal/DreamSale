using System.ComponentModel;

namespace Denmakers.DreamSale.ViewModels.AdminVM.Catalog
{
    public partial class LowStockProductVM
    {
        public int Id { get; set; }

        [DisplayName("Name")]
        public string Name { get; set; }

        public string Attributes { get; set; }

        [DisplayName("Inventory method")]
        public string ManageInventoryMethod { get; set; }

        [DisplayName("Stock quantity")]
        public int StockQuantity { get; set; }

        [DisplayName("Published")]
        public bool Published { get; set; }
    }
}
