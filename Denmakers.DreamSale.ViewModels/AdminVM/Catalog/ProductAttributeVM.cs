using System.Web.Mvc;
using System.ComponentModel;
using System.Collections.Generic;
using FluentValidation.Attributes;
using Denmakers.DreamSale.ViewModels.Validators.Catalog;

namespace Denmakers.DreamSale.ViewModels.AdminVM.Catalog
{
    [Validator(typeof(ProductAttributeValidator))]
    public partial class ProductAttributeVM
    {
        public ProductAttributeVM()
        {
            Locales = new List<ProductAttributeLocalizedVM>();
            CustomProperties = new Dictionary<string, object>();
        }

        public int Id { get; set; }
        public Dictionary<string, object> CustomProperties { get; set; }

        [DisplayName("Name")]
        [AllowHtml]
        public string Name { get; set; }

        [DisplayName("Description")]
        [AllowHtml]
        public string Description { get; set; }



        public IList<ProductAttributeLocalizedVM> Locales { get; set; }

        #region Nested classes

        public partial class UsedByProductVM
        {
            public int Id { get; set; }

            [DisplayName("Product")]
            public string ProductName { get; set; }
            [DisplayName("Published")]
            public bool Published { get; set; }
        }

        #endregion
    }

    public partial class ProductAttributeLocalizedVM
    {
        public int LanguageId { get; set; }

        [DisplayName("Name")]
        [AllowHtml]
        public string Name { get; set; }

        [DisplayName("Description")]
        [AllowHtml]
        public string Description { get; set; }
    }


    //[Validator(typeof(PredefinedProductAttributeValueVMValidator))]
    public partial class PredefinedProductAttributeValueVM
    {
        public PredefinedProductAttributeValueVM()
        {
            Locales = new List<PredefinedProductAttributeValueLocalizedVM>();
            CustomProperties = new Dictionary<string, object>();
        }


        public int Id { get; set; }
        public Dictionary<string, object> CustomProperties { get; set; }

        public int ProductAttributeId { get; set; }

        [DisplayName("Name")]
        [AllowHtml]
        public string Name { get; set; }

        [DisplayName("Price adjustment")]
        public decimal PriceAdjustment { get; set; }
        [DisplayName("Price adjustment")]
        //used only on the values list page
        public string PriceAdjustmentStr { get; set; }

        [DisplayName("Weight adjustment")]
        public decimal WeightAdjustment { get; set; }
        [DisplayName("Weight adjustment")]
        //used only on the values list page
        public string WeightAdjustmentStr { get; set; }

        [DisplayName("Cost")]
        public decimal Cost { get; set; }

        [DisplayName("Is pre-selected")]
        public bool IsPreSelected { get; set; }

        [DisplayName("Display order")]
        public int DisplayOrder { get; set; }

        public IList<PredefinedProductAttributeValueLocalizedVM> Locales { get; set; }
    }
    public partial class PredefinedProductAttributeValueLocalizedVM
    {
        public int LanguageId { get; set; }

        [DisplayName("Name")]
        [AllowHtml]
        public string Name { get; set; }
    }
}
