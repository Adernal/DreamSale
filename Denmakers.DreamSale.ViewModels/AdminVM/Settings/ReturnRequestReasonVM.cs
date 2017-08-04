using Denmakers.DreamSale.ViewModels.Validators.Settings;
using FluentValidation.Attributes;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;

namespace Denmakers.DreamSale.ViewModels.AdminVM.Settings
{
    [Validator(typeof(ReturnRequestReasonValidator))]
    public partial class ReturnRequestReasonVM
    {
        public ReturnRequestReasonVM()
        {
            Locales = new List<ReturnRequestReasonLocalizedVM>();
            CustomProperties = new Dictionary<string, object>();
        }

        public int Id { get; set; }
        public Dictionary<string, object> CustomProperties { get; set; }

        [DisplayName("Admin.Configuration.Settings.Order.ReturnRequestReasons.Name")]
        [AllowHtml]
        public string Name { get; set; }

        [DisplayName("Admin.Configuration.Settings.Order.ReturnRequestReasons.DisplayOrder")]
        public int DisplayOrder { get; set; }

        public IList<ReturnRequestReasonLocalizedVM> Locales { get; set; }
    }

    public partial class ReturnRequestReasonLocalizedVM
    {
        public int Id { get; set; }
        public int LanguageId { get; set; }

        [DisplayName("Admin.Configuration.Settings.Order.ReturnRequestReasons.Name")]
        [AllowHtml]
        public string Name { get; set; }

    }
}
