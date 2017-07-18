using System.ComponentModel;
using System.Web.Mvc;

namespace Denmakers.DreamSale.ViewModels.AdminVM.Catalog
{
    public partial class BulkEditProductVM
    {
        public int Id { get; set; }

        [DisplayName("Admin.Catalog.BulkEdit.Fields.Name")]
        [AllowHtml]
        public string Name { get; set; }

        [DisplayName("Admin.Catalog.BulkEdit.Fields.SKU")]
        [AllowHtml]
        public string Sku { get; set; }

        [DisplayName("Admin.Catalog.BulkEdit.Fields.Price")]
        public decimal Price { get; set; }

        [DisplayName("Admin.Catalog.BulkEdit.Fields.OldPrice")]
        public decimal OldPrice { get; set; }

        [DisplayName("Admin.Catalog.BulkEdit.Fields.ManageInventoryMethod")]
        public string ManageInventoryMethod { get; set; }

        [DisplayName("Admin.Catalog.BulkEdit.Fields.StockQuantity")]
        public int StockQuantity { get; set; }

        [DisplayName("Admin.Catalog.BulkEdit.Fields.Published")]
        public bool Published { get; set; }
    }
}
