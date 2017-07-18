using System.Web.Mvc;
using System.ComponentModel;
using System.Collections.Generic;
using FluentValidation.Attributes;
using Denmakers.DreamSale.ViewModels.Validators.Catalog;

namespace Denmakers.DreamSale.ViewModels.AdminVM.Catalog
{
    [Validator(typeof(ProductTagValidator))]
    public partial class ProductTagVM
    {
        public ProductTagVM()
        {
            Locales = new List<ProductTagLocalizedVM>();
            CustomProperties = new Dictionary<string, object>();
        }
        public int Id { get; set; }

        public Dictionary<string, object> CustomProperties { get; set; }

        [DisplayName("Name")]
        [AllowHtml]
        public string Name { get; set; }

        [DisplayName("Product count")]
        public int ProductCount { get; set; }

        public IList<ProductTagLocalizedVM> Locales { get; set; }
    }

    public partial class ProductTagLocalizedVM
    {
        public int LanguageId { get; set; }

        [DisplayName("Name")]
        [AllowHtml]
        public string Name { get; set; }
    }
}