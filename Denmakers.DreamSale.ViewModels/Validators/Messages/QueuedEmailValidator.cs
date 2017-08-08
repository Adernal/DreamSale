using Denmakers.DreamSale.Data.Context;
using Denmakers.DreamSale.Model.Messages;
using Denmakers.DreamSale.Services.Localization;
using Denmakers.DreamSale.ViewModels.AdminVM.Messages;
using FluentValidation;

namespace Denmakers.DreamSale.ViewModels.Validators.Messages
{
    public partial class QueuedEmailValidator : BaseValidator<QueuedEmailVM>
    {
        public QueuedEmailValidator(ILocalizationService localizationService, IDbContext dbContext)
        {
            RuleFor(x => x.From).NotEmpty().WithMessage(localizationService.GetResource("Admin.System.QueuedEmails.Fields.From.Required"));
            RuleFor(x => x.To).NotEmpty().WithMessage(localizationService.GetResource("Admin.System.QueuedEmails.Fields.To.Required"));

            RuleFor(x => x.SentTries).NotNull().WithMessage(localizationService.GetResource("Admin.System.QueuedEmails.Fields.SentTries.Required"))
                                    .InclusiveBetween(0, 99999).WithMessage(localizationService.GetResource("Admin.System.QueuedEmails.Fields.SentTries.Range"));

            SetDatabaseValidationRules<QueuedEmail>(dbContext, localizationService);

        }
    }
}
