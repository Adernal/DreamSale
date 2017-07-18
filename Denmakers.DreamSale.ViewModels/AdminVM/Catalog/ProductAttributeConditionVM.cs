using System.ComponentModel;
using System.Collections.Generic;
using Denmakers.DreamSale.Model.Catalog;

namespace Denmakers.DreamSale.ViewModels.AdminVM.Catalog
{
    public partial class ProductAttributeConditionVM
    {
        public ProductAttributeConditionVM()
        {
            ProductAttributes = new List<ProductAttributeVM>();
            CustomProperties = new Dictionary<string, object>();
        }

        public int Id { get; set; }
        public Dictionary<string, object> CustomProperties { get; set; }

        [DisplayName("Enable condition")]
        public bool EnableCondition { get; set; }

        [DisplayName("Attributes")]
        public int SelectedProductAttributeId { get; set; }
        public IList<ProductAttributeVM> ProductAttributes { get; set; }

        public int ProductAttributeMappingId { get; set; }

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
