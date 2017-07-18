using Denmakers.DreamSale.ViewModels.AdminVM.Catalog;
using Denmakers.DreamSale.Data.Context;
using Denmakers.DreamSale.Model.Catalog;
using Denmakers.DreamSale.Services.Localization;
using FluentValidation;
using FluentValidation.Results;

namespace Denmakers.DreamSale.ViewModels.Validators.Catalog
{
    public partial class CategoryValidator : BaseValidator<CategoryVM>
    {
        public CategoryValidator(ILocalizationService localizationService, IDbContext dbContext)
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(localizationService.GetResource("Admin.Catalog.Categories.Fields.Name.Required"));
            RuleFor(x => x.PageSizeOptions).Must(ValidatorUtilities.PageSizeOptionsValidator).WithMessage(localizationService.GetResource("Admin.Catalog.Categories.Fields.PageSizeOptions.ShouldHaveUniqueItems"));
            Custom(x =>
            {
                if (!x.AllowCustomersToSelectPageSize && x.PageSize <= 0)
                    return new ValidationFailure("PageSize", localizationService.GetResource("Admin.Catalog.Categories.Fields.PageSize.Positive"));

                return null;
            });

            SetDatabaseValidationRules<Category>(dbContext, localizationService);
        }
    }
}
