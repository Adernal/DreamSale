using Denmakers.DreamSale.Services.Localization;
using Denmakers.DreamSale.ViewModels.NonAdminVM.Customers;
using FluentValidation;

namespace Denmakers.DreamSale.ViewModels.Validators.Customers
{
    public partial class PasswordRecoveryValidator : BaseValidator<PasswordRecoveryModel>
    {
        public PasswordRecoveryValidator(ILocalizationService localizationService)
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage(localizationService.GetResource("Account.PasswordRecovery.Email.Required"));
            RuleFor(x => x.Email).EmailAddress().WithMessage(localizationService.GetResource("Common.WrongEmail"));
        }
    }
}
