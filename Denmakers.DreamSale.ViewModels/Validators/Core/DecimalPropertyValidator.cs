using Denmakers.DreamSale.Services.Helpers;
using FluentValidation.Validators;

namespace Denmakers.DreamSale.ViewModels.Validators
{
    public class DecimalPropertyValidator : PropertyValidator
    {
        private readonly decimal _maxValue;

        protected override bool IsValid(PropertyValidatorContext context)
        {
            decimal value;
            if (decimal.TryParse(context.PropertyValue.ToString(), out value))
            {
                return RoundingHelper.RoundPrice(value) < _maxValue;
            }
            return false;
        }

        public DecimalPropertyValidator(decimal maxValue) :
            base("Decimal value is out of range")
        {
            this._maxValue = maxValue;
        }
    }
}
