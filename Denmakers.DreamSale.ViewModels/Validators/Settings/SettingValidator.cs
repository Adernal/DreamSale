using Denmakers.DreamSale.Services.Localization;
using Denmakers.DreamSale.ViewModels.AdminVM.Settings;
using FluentValidation;

namespace Denmakers.DreamSale.ViewModels.Validators.Settings
{
    public partial class SettingValidator : BaseValidator<SettingVM>
    {
        public SettingValidator(ILocalizationService localizationService)
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(localizationService.GetResource("Admin.Configuration.Settings.AllSettings.Fields.Name.Required"));
        }
    }
}
