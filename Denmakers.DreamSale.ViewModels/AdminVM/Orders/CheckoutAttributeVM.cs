using Denmakers.DreamSale.Model.Catalog;
using Denmakers.DreamSale.ViewModels.Validators.Orders;
using FluentValidation.Attributes;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Denmakers.DreamSale.ViewModels.AdminVM.Orders
{
    [Validator(typeof(CheckoutAttributeValidator))]
    public partial class CheckoutAttributeVM
    {
        public CheckoutAttributeVM()
        {
            Locales = new List<CheckoutAttributeLocalizedVM>();
            AvailableTaxCategories = new List<SelectListItem>();

            SelectedStoreIds = new List<int>();
            AvailableStores = new List<SelectListItem>();
            CustomProperties = new Dictionary<string, object>();
        }
        public int Id { get; set; }
        public Dictionary<string, object> CustomProperties { get; set; }

        [DisplayName("Admin.Catalog.Attributes.CheckoutAttributes.Fields.Name")]
        [AllowHtml]
        public string Name { get; set; }

        [DisplayName("Admin.Catalog.Attributes.CheckoutAttributes.Fields.TextPrompt")]
        [AllowHtml]
        public string TextPrompt { get; set; }

        [DisplayName("Admin.Catalog.Attributes.CheckoutAttributes.Fields.IsRequired")]
        public bool IsRequired { get; set; }

        [DisplayName("Admin.Catalog.Attributes.CheckoutAttributes.Fields.ShippableProductRequired")]
        public bool ShippableProductRequired { get; set; }

        [DisplayName("Admin.Catalog.Attributes.CheckoutAttributes.Fields.IsTaxExempt")]
        public bool IsTaxExempt { get; set; }

        [DisplayName("Admin.Catalog.Attributes.CheckoutAttributes.Fields.TaxCategory")]
        public int TaxCategoryId { get; set; }
        public IList<SelectListItem> AvailableTaxCategories { get; set; }

        [DisplayName("Admin.Catalog.Attributes.CheckoutAttributes.Fields.AttributeControlType")]
        public int AttributeControlTypeId { get; set; }
        [DisplayName("Admin.Catalog.Attributes.CheckoutAttributes.Fields.AttributeControlType")]
        [AllowHtml]
        public string AttributeControlTypeName { get; set; }

        [DisplayName("Admin.Catalog.Attributes.CheckoutAttributes.Fields.DisplayOrder")]
        public int DisplayOrder { get; set; }


        [DisplayName("Admin.Catalog.Attributes.CheckoutAttributes.Fields.MinLength")]
        [UIHint("Int32Nullable")]
        public int? ValidationMinLength { get; set; }

        [DisplayName("Admin.Catalog.Attributes.CheckoutAttributes.Fields.MaxLength")]
        [UIHint("Int32Nullable")]
        public int? ValidationMaxLength { get; set; }

        [DisplayName("Admin.Catalog.Attributes.CheckoutAttributes.Fields.FileAllowedExtensions")]
        public string ValidationFileAllowedExtensions { get; set; }

        [DisplayName("Admin.Catalog.Attributes.CheckoutAttributes.Fields.FileMaximumSize")]
        [UIHint("Int32Nullable")]
        public int? ValidationFileMaximumSize { get; set; }

        [DisplayName("Admin.Catalog.Attributes.CheckoutAttributes.Fields.DefaultValue")]
        public string DefaultValue { get; set; }

        public IList<CheckoutAttributeLocalizedVM> Locales { get; set; }

        //condition
        public bool ConditionAllowed { get; set; }
        public ConditionVM ConditionVM { get; set; }

        //store mapping
        [DisplayName("Admin.Catalog.Attributes.CheckoutAttributes.Fields.LimitedToStores")]
        [UIHint("MultiSelect")]
        public IList<int> SelectedStoreIds { get; set; }
        public IList<SelectListItem> AvailableStores { get; set; }

    }

    public partial class ConditionVM
    {
        public ConditionVM()
        {
            CustomProperties = new Dictionary<string, object>();
        }
        public int Id { get; set; }
        public Dictionary<string, object> CustomProperties { get; set; }

        [DisplayName("Admin.Catalog.Attributes.CheckoutAttributes.Condition.EnableCondition")]
        public bool EnableCondition { get; set; }

        [DisplayName("Admin.Catalog.Attributes.CheckoutAttributes.Condition.Attributes")]
        public int SelectedAttributeId { get; set; }

        public IList<AttributeConditionVM> ConditionAttributes { get; set; }
    }

    public partial class AttributeConditionVM
    {
        public AttributeConditionVM()
        {
            CustomProperties = new Dictionary<string, object>();
        }
        public int Id { get; set; }
        public Dictionary<string, object> CustomProperties { get; set; }
        public string Name { get; set; }

        public AttributeControlType AttributeControlType { get; set; }

        public IList<SelectListItem> Values { get; set; }

        public string SelectedValueId { get; set; }
    }

    public partial class CheckoutAttributeLocalizedVM
    {
        public int Id { get; set; }
        public int LanguageId { get; set; }

        [DisplayName("Admin.Catalog.Attributes.CheckoutAttributes.Fields.Name")]
        [AllowHtml]
        public string Name { get; set; }

        [DisplayName("Admin.Catalog.Attributes.CheckoutAttributes.Fields.TextPrompt")]
        [AllowHtml]
        public string TextPrompt { get; set; }

    }
}
