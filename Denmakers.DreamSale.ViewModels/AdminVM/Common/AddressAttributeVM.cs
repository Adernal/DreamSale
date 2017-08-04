using Denmakers.DreamSale.ViewModels.Validators.Addresses;
using FluentValidation.Attributes;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;

namespace Denmakers.DreamSale.ViewModels.AdminVM.Common
{
    [Validator(typeof(AddressAttributeValidator))]
    public partial class AddressAttributeVM
    {
        public AddressAttributeVM()
        {
            Locales = new List<AddressAttributeLocalizedVM>(); CustomProperties = new Dictionary<string, object>();
        }

        public int Id { get; set; }
        public Dictionary<string, object> CustomProperties { get; set; }

        [DisplayName("Admin.Address.AddressAttributes.Fields.Name")]
        [AllowHtml]
        public string Name { get; set; }

        [DisplayName("Admin.Address.AddressAttributes.Fields.IsRequired")]
        public bool IsRequired { get; set; }

        [DisplayName("Admin.Address.AddressAttributes.Fields.AttributeControlType")]
        public int AttributeControlTypeId { get; set; }
        [DisplayName("Admin.Address.AddressAttributes.Fields.AttributeControlType")]
        [AllowHtml]
        public string AttributeControlTypeName { get; set; }

        [DisplayName("Admin.Address.AddressAttributes.Fields.DisplayOrder")]
        public int DisplayOrder { get; set; }


        public IList<AddressAttributeLocalizedVM> Locales { get; set; }

    }

    public partial class AddressAttributeLocalizedVM
    {
        public int Id { get; set; }
        public int LanguageId { get; set; }

        [DisplayName("Admin.Address.AddressAttributes.Fields.Name")]
        [AllowHtml]
        public string Name { get; set; }

    }
}
