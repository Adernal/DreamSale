using System.Web.Mvc;
using System.ComponentModel;
using System.Collections.Generic;
using FluentValidation.Attributes;
using Denmakers.DreamSale.ViewModels.Validators.Catalog;

namespace Denmakers.DreamSale.ViewModels.AdminVM.Catalog
{
    [Validator(typeof(SpecificationAttributeOptionValidator))]
    public partial class SpecificationAttributeOptionVM
    {
        public SpecificationAttributeOptionVM()
        {
            Locales = new List<SpecificationAttributeOptionLocalizedModel>();
            CustomProperties = new Dictionary<string, object>();
        }

        public int Id { get; set; }

        public Dictionary<string, object> CustomProperties { get; set; }

        public int SpecificationAttributeId { get; set; }

        [DisplayName("Name")]
        [AllowHtml]
        public string Name { get; set; }

        [DisplayName("RGB color")]
        [AllowHtml]
        public string ColorSquaresRgb { get; set; }
        [DisplayName("Specify color")]
        public bool EnableColorSquaresRgb { get; set; }

        [DisplayName("Display order")]
        public int DisplayOrder { get; set; }

        [DisplayName("Number of associated products")]
        public int NumberOfAssociatedProducts { get; set; }

        public IList<SpecificationAttributeOptionLocalizedModel> Locales { get; set; }

    }

    public partial class SpecificationAttributeOptionLocalizedModel
    {
        public int LanguageId { get; set; }

        [DisplayName("Name")]
        [AllowHtml]
        public string Name { get; set; }
    }
}
