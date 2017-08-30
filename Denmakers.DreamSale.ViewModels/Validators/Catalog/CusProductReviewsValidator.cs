using Denmakers.DreamSale.Services.Localization;
using Denmakers.DreamSale.ViewModels.NonAdminVM.Catalog;
using FluentValidation;

namespace Denmakers.DreamSale.ViewModels.Validators.Catalog
{
    public partial class CusProductReviewsValidator : BaseValidator<ProductReviewsModel>
    {
        public CusProductReviewsValidator(ILocalizationService localizationService)
        {
            RuleFor(x => x.AddProductReview.Title).NotEmpty().WithMessage(localizationService.GetResource("Reviews.Fields.Title.Required")).When(x => x.AddProductReview != null);
            RuleFor(x => x.AddProductReview.Title).Length(1, 200).WithMessage(string.Format(localizationService.GetResource("Reviews.Fields.Title.MaxLengthValidation"), 200)).When(x => x.AddProductReview != null && !string.IsNullOrEmpty(x.AddProductReview.Title));
            RuleFor(x => x.AddProductReview.ReviewText).NotEmpty().WithMessage(localizationService.GetResource("Reviews.Fields.ReviewText.Required")).When(x => x.AddProductReview != null);
        }
    }
}
