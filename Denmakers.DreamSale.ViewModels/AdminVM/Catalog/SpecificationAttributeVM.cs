using System.Web.Mvc;
using System.ComponentModel;
using System.Collections.Generic;
using FluentValidation.Attributes;
using Denmakers.DreamSale.ViewModels.Validators.Catalog;

namespace Denmakers.DreamSale.ViewModels.AdminVM.Catalog
{
    [Validator(typeof(SpecificationAttributeValidator))]
    public partial class SpecificationAttributeVM
    {
        public SpecificationAttributeVM()
        {
            Locales = new List<SpecificationAttributeLocalizedModel>();
            CustomProperties = new Dictionary<string, object>();
        }

        public int Id { get; set; }

        public Dictionary<string, object> CustomProperties { get; set; }

        [DisplayName("Name")]
        [AllowHtml]
        public string Name { get; set; }

        [DisplayName("Display order")]
        public int DisplayOrder { get; set; }


        public IList<SpecificationAttributeLocalizedModel> Locales { get; set; }

    }

    public partial class SpecificationAttributeLocalizedModel
    {
        public int LanguageId { get; set; }

        [DisplayName("Name")]
        [AllowHtml]
        public string Name { get; set; }
    }
}