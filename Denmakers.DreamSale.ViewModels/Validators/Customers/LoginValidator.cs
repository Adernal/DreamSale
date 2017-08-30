using Denmakers.DreamSale.Model.Customers;
using Denmakers.DreamSale.Services.Localization;
using Denmakers.DreamSale.ViewModels.NonAdminVM.Customers;
using FluentValidation;

namespace Denmakers.DreamSale.ViewModels.Validators.Customers
{
    public partial class LoginValidator : BaseValidator<LoginModel>
    {
        public LoginValidator(ILocalizationService localizationService, CustomerSettings customerSettings)
        {
            if (!customerSettings.UsernamesEnabled)
            {
                //login by email
                RuleFor(x => x.Email).NotEmpty().WithMessage(localizationService.GetResource("Account.Login.Fields.Email.Required"));
                RuleFor(x => x.Email).EmailAddress().WithMessage(localizationService.GetResource("Common.WrongEmail"));
            }
        }
    }
}
