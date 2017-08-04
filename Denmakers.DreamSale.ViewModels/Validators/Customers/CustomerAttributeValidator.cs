using Denmakers.DreamSale.Data.Context;
using Denmakers.DreamSale.Model.Customers;
using Denmakers.DreamSale.Services.Localization;
using FluentValidation;
using static Denmakers.DreamSale.ViewModels.AdminVM.Customers.CustomerVM;

namespace Denmakers.DreamSale.ViewModels.Validators.Customers
{
    public partial class CustomerAttributeValidator : BaseValidator<CustomerAttributeVM>
    {
        public CustomerAttributeValidator(ILocalizationService localizationService, IDbContext dbContext)
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(localizationService.GetResource("Admin.Customers.CustomerAttributes.Fields.Name.Required"));

            SetDatabaseValidationRules<CustomerAttribute>(dbContext, localizationService);
        }
    }
}
