using Denmakers.DreamSale.Data.Context;
using Denmakers.DreamSale.Model.Catalog;
using Denmakers.DreamSale.Services.Localization;
using Denmakers.DreamSale.ViewModels.AdminVM.Catalog;
using FluentValidation;

namespace Denmakers.DreamSale.ViewModels.Validators.Catalog
{
    public partial class ProductValidator : BaseValidator<ProductVM>
    {
        public ProductValidator(ILocalizationService localizationService, IDbContext dbContext)
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(localizationService.GetResource("Admin.Catalog.Products.Fields.Name.Required"));

            SetDatabaseValidationRules<Product>(dbContext, localizationService);
        }
    }
}
