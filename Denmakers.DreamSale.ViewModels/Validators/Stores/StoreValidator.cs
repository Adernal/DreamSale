using Denmakers.DreamSale.Data.Context;
using Denmakers.DreamSale.Model.Stores;
using Denmakers.DreamSale.Services.Localization;
using Denmakers.DreamSale.ViewModels.AdminVM.Stores;
using FluentValidation;

namespace Denmakers.DreamSale.ViewModels.Validators.Stores
{
    public partial class StoreValidator : BaseValidator<StoreVM>
    {
        public StoreValidator(ILocalizationService localizationService, IDbContext dbContext)
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(localizationService.GetResource("Admin.Configuration.Stores.Fields.Name.Required"));
            RuleFor(x => x.Url).NotEmpty().WithMessage(localizationService.GetResource("Admin.Configuration.Stores.Fields.Url.Required"));

            SetDatabaseValidationRules<Store>(dbContext, localizationService);
        }
    }
}
