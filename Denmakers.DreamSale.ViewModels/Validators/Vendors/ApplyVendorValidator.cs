using Denmakers.DreamSale.Services.Localization;
using Denmakers.DreamSale.ViewModels.NonAdminVM.Vendors;
using FluentValidation;

namespace Denmakers.DreamSale.ViewModels.Validators.Vendors
{
    public partial class ApplyVendorValidator : BaseValidator<ApplyVendorModel>
    {
        public ApplyVendorValidator(ILocalizationService localizationService)
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(localizationService.GetResource("Vendors.ApplyAccount.Name.Required"));

            RuleFor(x => x.Email).NotEmpty().WithMessage(localizationService.GetResource("Vendors.ApplyAccount.Email.Required"));
            RuleFor(x => x.Email).EmailAddress().WithMessage(localizationService.GetResource("Common.WrongEmail"));
        }
    }
}
