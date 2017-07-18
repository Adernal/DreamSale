using Denmakers.DreamSale.Data.Context;
using Denmakers.DreamSale.Model.Vendors;
using Denmakers.DreamSale.Services.Localization;
using Denmakers.DreamSale.ViewModels.AdminVM.Vendors;
using FluentValidation;
using FluentValidation.Results;

namespace Denmakers.DreamSale.ViewModels.Validators.Vendors
{
    public partial class VendorValidator : BaseValidator<VendorVM>
    {
        public VendorValidator(ILocalizationService localizationService, IDbContext dbContext)
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(localizationService.GetResource("Admin.Vendors.Fields.Name.Required"));

            RuleFor(x => x.Email).NotEmpty().WithMessage(localizationService.GetResource("Admin.Vendors.Fields.Email.Required"));
            RuleFor(x => x.Email).EmailAddress().WithMessage(localizationService.GetResource("Admin.Common.WrongEmail"));
            RuleFor(x => x.PageSizeOptions).Must(ValidatorUtilities.PageSizeOptionsValidator).WithMessage(localizationService.GetResource("Admin.Vendors.Fields.PageSizeOptions.ShouldHaveUniqueItems"));
            Custom(x =>
            {
                if (!x.AllowCustomersToSelectPageSize && x.PageSize <= 0)
                    return new ValidationFailure("PageSize", localizationService.GetResource("Admin.Vendors.Fields.PageSize.Positive"));

                return null;
            });

            SetDatabaseValidationRules<Vendor>(dbContext, localizationService);
        }
    }
}
