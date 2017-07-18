using Denmakers.DreamSale.Data.Context;
using Denmakers.DreamSale.Model.Catalog;
using Denmakers.DreamSale.Services.Localization;
using Denmakers.DreamSale.ViewModels.AdminVM.Catalog;
using FluentValidation;

namespace Denmakers.DreamSale.ViewModels.Validators.Catalog
{
    public partial class ProductAttributeValueVMValidator : BaseValidator<ProductVM.ProductAttributeValueVM>
    {
        public ProductAttributeValueVMValidator(ILocalizationService localizationService, IDbContext dbContext)
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(localizationService.GetResource("Admin.Catalog.Products.ProductAttributes.Attributes.Values.Fields.Name.Required"));

            RuleFor(x => x.Quantity)
                .GreaterThanOrEqualTo(1)
                .WithMessage(localizationService.GetResource("Admin.Catalog.Products.ProductAttributes.Attributes.Values.Fields.Quantity.GreaterThanOrEqualTo1"))
                .When(x => x.AttributeValueTypeId == (int)AttributeValueType.AssociatedToProduct && !x.CustomerEntersQty);

            RuleFor(x => x.AssociatedProductId)
                .GreaterThanOrEqualTo(1)
                .WithMessage(localizationService.GetResource("Admin.Catalog.Products.ProductAttributes.Attributes.Values.Fields.AssociatedProduct.Choose"))
                .When(x => x.AttributeValueTypeId == (int)AttributeValueType.AssociatedToProduct);

            SetDatabaseValidationRules<Product>(dbContext, localizationService);
        }
    }
}
