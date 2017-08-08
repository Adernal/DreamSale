using Denmakers.DreamSale.Data.Context;
using Denmakers.DreamSale.Model.Messages;
using Denmakers.DreamSale.Services.Localization;
using Denmakers.DreamSale.ViewModels.AdminVM.Messages;
using FluentValidation;

namespace Denmakers.DreamSale.ViewModels.Validators.Messages
{
    public partial class EmailAccountValidator : BaseValidator<EmailAccountVM>
    {
        public EmailAccountValidator(ILocalizationService localizationService, IDbContext dbContext)
        {
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.Email).EmailAddress().WithMessage(localizationService.GetResource("Admin.Common.WrongEmail"));

            RuleFor(x => x.DisplayName).NotEmpty();

            SetDatabaseValidationRules<EmailAccount>(dbContext, localizationService);
        }
    }
}
