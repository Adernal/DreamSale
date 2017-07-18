using Denmakers.DreamSale.Model.Catalog;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Denmakers.DreamSale.ViewModels.AdminVM.Catalog
{
    public partial class AddProductAttributeCombinationVM
    {
        public AddProductAttributeCombinationVM()
        {
            ProductAttributes = new List<ProductAttributeVM>();
            Warnings = new List<string>();
            CustomProperties = new Dictionary<string, object>();
        }

        public int Id { get; set; }
        public Dictionary<string, object> CustomProperties { get; set; }

        [DisplayName("Stock quantity")]
        public int StockQuantity { get; set; }

        [DisplayName("Allow out of stock")]
        public bool AllowOutOfStockOrders { get; set; }

        [DisplayName("Sku")]
        public string Sku { get; set; }

        [DisplayName("Manufacturer part number")]
        public string ManufacturerPartNumber { get; set; }

        [DisplayName("Gtin")]
        public string Gtin { get; set; }

        [DisplayName("Overridden price")]
        [UIHint("DecimalNullable")]
        public decimal? OverriddenPrice { get; set; }

        [DisplayName("Notify admin for quantity below")]
        public int NotifyAdminForQuantityBelow { get; set; }

        public IList<ProductAttributeVM> ProductAttributes { get; set; }

        public IList<string> Warnings { get; set; }

        public int ProductId { get; set; }

        #region Nested classes

        public partial class ProductAttributeVM
        {
            public ProductAttributeVM()
            {
                Values = new List<ProductAttributeValueVM>();
                CustomProperties = new Dictionary<string, object>();
            }

            public int Id { get; set; }
            public Dictionary<string, object> CustomProperties { get; set; }

            public int ProductAttributeId { get; set; }

            public string Name { get; set; }

            public string TextPrompt { get; set; }

            public bool IsRequired { get; set; }

            public AttributeControlType AttributeControlType { get; set; }

            public IList<ProductAttributeValueVM> Values { get; set; }
        }

        public partial class ProductAttributeValueVM
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public bool IsPreSelected { get; set; }
        }
        #endregion
    }
}