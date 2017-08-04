using Denmakers.DreamSale.Data.Context;
using Denmakers.DreamSale.Model.Orders;
using Denmakers.DreamSale.Services.Localization;
using Denmakers.DreamSale.ViewModels.AdminVM.Orders;
using FluentValidation;

namespace Denmakers.DreamSale.ViewModels.Validators.Orders
{
    public partial class CheckoutAttributeValidator : BaseValidator<CheckoutAttributeVM>
    {
        public CheckoutAttributeValidator(ILocalizationService localizationService, IDbContext dbContext)
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(localizationService.GetResource("Admin.Catalog.Attributes.CheckoutAttributes.Fields.Name.Required"));

            SetDatabaseValidationRules<CheckoutAttribute>(dbContext, localizationService);
        }
    }
}
