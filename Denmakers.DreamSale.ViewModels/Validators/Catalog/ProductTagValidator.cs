using Denmakers.DreamSale.Data.Context;
using Denmakers.DreamSale.Model.Catalog;
using Denmakers.DreamSale.Services.Localization;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Denmakers.DreamSale.ViewModels.Validators.Catalog
{
    class ProductTagValidator : BaseValidator<ProductTag>
    {
        public ProductTagValidator(ILocalizationService localizationService, IDbContext dbContext)
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(localizationService.GetResource("Admin.Catalog.Products.Fields.Name.Required"));

            SetDatabaseValidationRules<ProductTag>(dbContext, localizationService);
        }
    }
}
