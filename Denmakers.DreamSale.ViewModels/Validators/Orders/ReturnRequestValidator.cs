using Denmakers.DreamSale.Services.Localization;
using Denmakers.DreamSale.ViewModels.AdminVM.Orders;
using FluentValidation;

namespace Denmakers.DreamSale.ViewModels.Validators.Orders
{
    public partial class ReturnRequestValidator : BaseValidator<ReturnRequestVM>
    {
        public ReturnRequestValidator(ILocalizationService localizationService)
        {
            RuleFor(x => x.ReasonForReturn).NotEmpty().WithMessage(localizationService.GetResource("Admin.ReturnRequests.Fields.ReasonForReturn.Required"));
            RuleFor(x => x.RequestedAction).NotEmpty().WithMessage(localizationService.GetResource("Admin.ReturnRequests.Fields.RequestedAction.Required"));
        }
    }
}
