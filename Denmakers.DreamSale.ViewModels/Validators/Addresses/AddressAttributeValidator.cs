using Denmakers.DreamSale.Data.Context;
using Denmakers.DreamSale.Model.Common;
using Denmakers.DreamSale.Services.Localization;
using FluentValidation;
using static Denmakers.DreamSale.ViewModels.AdminVM.Addresses.AddressVM;

namespace Denmakers.DreamSale.ViewModels.Validators.Addresses
{
    public partial class AddressAttributeValidator : BaseValidator<AddressAttributeVM>
    {
        public AddressAttributeValidator(ILocalizationService localizationService, IDbContext dbContext)
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(localizationService.GetResource("Admin.Address.AddressAttributes.Fields.Name.Required"));

            SetDatabaseValidationRules<AddressAttribute>(dbContext, localizationService);
        }
    }
}
