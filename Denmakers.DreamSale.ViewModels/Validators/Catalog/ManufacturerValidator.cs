using Denmakers.DreamSale.Data.Context;
using Denmakers.DreamSale.Model.Catalog;
using Denmakers.DreamSale.Services.Localization;
using Denmakers.DreamSale.ViewModels.AdminVM.Catalog;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Denmakers.DreamSale.ViewModels.Validators.Catalog
{
    public partial class ManufacturerValidator : BaseValidator<ManufacturerVM>
    {
        public ManufacturerValidator(ILocalizationService localizationService, IDbContext dbContext)
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(localizationService.GetResource("Admin.Catalog.Manufacturers.Fields.Name.Required"));
            RuleFor(x => x.PageSizeOptions).Must(ValidatorUtilities.PageSizeOptionsValidator).WithMessage(localizationService.GetResource("Admin.Catalog.Manufacturers.Fields.PageSizeOptions.ShouldHaveUniqueItems"));
            Custom(x =>
            {
                if (!x.AllowCustomersToSelectPageSize && x.PageSize <= 0)
                    return new ValidationFailure("PageSize", localizationService.GetResource("Admin.Catalog.Manufacturers.Fields.PageSize.Positive"));

                return null;
            });

            SetDatabaseValidationRules<Category>(dbContext, localizationService);
        }
    }
}
