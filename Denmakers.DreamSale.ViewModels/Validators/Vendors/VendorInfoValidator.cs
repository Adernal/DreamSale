using Denmakers.DreamSale.Services.Localization;
using Denmakers.DreamSale.ViewModels.NonAdminVM.Vendors;
using FluentValidation;

namespace Denmakers.DreamSale.ViewModels.Validators.Vendors
{
    public partial class VendorInfoValidator : BaseValidator<VendorInfoModel>
    {
        public VendorInfoValidator(ILocalizationService localizationService)
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(localizationService.GetResource("Account.VendorInfo.Name.Required"));

            RuleFor(x => x.Email).NotEmpty().WithMessage(localizationService.GetResource("Account.VendorInfo.Email.Required"));
            RuleFor(x => x.Email).EmailAddress().WithMessage(localizationService.GetResource("Common.WrongEmail"));
        }
    }
}
