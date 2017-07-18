using Denmakers.DreamSale.Data.Context;
using Denmakers.DreamSale.Model.Customers;
using Denmakers.DreamSale.Services.Localization;
using Denmakers.DreamSale.ViewModels.AdminVM.Customers;
using FluentValidation;

namespace Denmakers.DreamSale.ViewModels.Validators.Customers
{
    public partial class CustomerRoleValidator : BaseValidator<CustomerRoleVM>
    {
        public CustomerRoleValidator(ILocalizationService localizationService, IDbContext dbContext)
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(localizationService.GetResource("Admin.Customers.CustomerRoles.Fields.Name.Required"));

            SetDatabaseValidationRules<CustomerRole>(dbContext, localizationService);
        }
    }
}
