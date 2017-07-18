using Denmakers.DreamSale.Services.Localization;
using Denmakers.DreamSale.ViewModels.AdminVM.Catalog;
using FluentValidation;

namespace Denmakers.DreamSale.ViewModels.Validators.Catalog
{
    public partial class SpecificationAttributeOptionValidator : BaseValidator<SpecificationAttributeOptionVM>
    {
        public SpecificationAttributeOptionValidator(ILocalizationService localizationService)
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(localizationService.GetResource("Admin.Catalog.Attributes.SpecificationAttributes.Options.Fields.Name.Required"));
        }
    }
}