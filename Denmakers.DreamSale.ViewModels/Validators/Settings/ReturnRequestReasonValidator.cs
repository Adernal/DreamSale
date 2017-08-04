using Denmakers.DreamSale.Services.Localization;
using Denmakers.DreamSale.ViewModels.AdminVM.Settings;
using FluentValidation;

namespace Denmakers.DreamSale.ViewModels.Validators.Settings
{
    public partial class ReturnRequestReasonValidator : BaseValidator<ReturnRequestReasonVM>
    {
        public ReturnRequestReasonValidator(ILocalizationService localizationService)
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(localizationService.GetResource("Admin.Configuration.Settings.Order.ReturnRequestReasons.Name.Required"));
        }
    }
}
