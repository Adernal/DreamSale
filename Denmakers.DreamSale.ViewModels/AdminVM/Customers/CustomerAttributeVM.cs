using Denmakers.DreamSale.ViewModels.Validators.Customers;
using FluentValidation.Attributes;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;

namespace Denmakers.DreamSale.ViewModels.AdminVM.Customers
{
    [Validator(typeof(CustomerAttributeValidator))]
    public partial class CustomerAttributeVM
    {
        public CustomerAttributeVM()
        {
            Locales = new List<CustomerAttributeLocalizedVM>();
            CustomProperties = new Dictionary<string, object>();
        }

        public int Id { get; set; }
        public Dictionary<string, object> CustomProperties { get; set; }

        [DisplayName("Admin.Customers.CustomerAttributes.Fields.Name")]
        [AllowHtml]
        public string Name { get; set; }

        [DisplayName("Admin.Customers.CustomerAttributes.Fields.IsRequired")]
        public bool IsRequired { get; set; }

        [DisplayName("Admin.Customers.CustomerAttributes.Fields.AttributeControlType")]
        public int AttributeControlTypeId { get; set; }
        [DisplayName("Admin.Customers.CustomerAttributes.Fields.AttributeControlType")]
        [AllowHtml]
        public string AttributeControlTypeName { get; set; }

        [DisplayName("Admin.Customers.CustomerAttributes.Fields.DisplayOrder")]
        public int DisplayOrder { get; set; }


        public IList<CustomerAttributeLocalizedVM> Locales { get; set; }

    }

    public partial class CustomerAttributeLocalizedVM
    {
        public int Id { get; set; }
        public int LanguageId { get; set; }

        [DisplayName("Admin.Customers.CustomerAttributes.Fields.Name")]
        [AllowHtml]
        public string Name { get; set; }

    }
}
