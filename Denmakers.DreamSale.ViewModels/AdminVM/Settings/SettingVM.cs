using Denmakers.DreamSale.ViewModels.Validators.Settings;
using FluentValidation.Attributes;
using System.ComponentModel;
using System.Web.Mvc;

namespace Denmakers.DreamSale.ViewModels.AdminVM.Settings
{
    [Validator(typeof(SettingValidator))]
    public partial class SettingVM
    {
        public int Id { get; set; }
        [DisplayName("Setting name")]
        [AllowHtml]
        public string Name { get; set; }

        [DisplayName("Value")]
        [AllowHtml]
        public string Value { get; set; }

        [DisplayName("Store")]
        public string Store { get; set; }
        public int StoreId { get; set; }
    }
}
