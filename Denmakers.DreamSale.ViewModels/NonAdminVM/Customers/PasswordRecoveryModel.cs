using Denmakers.DreamSale.ViewModels.Validators.Customers;
using FluentValidation.Attributes;
using System.ComponentModel;
using System.Web.Mvc;

namespace Denmakers.DreamSale.ViewModels.NonAdminVM.Customers
{
    [Validator(typeof(PasswordRecoveryValidator))]
    public partial class PasswordRecoveryModel
    {
        public int Id { get; set; }
        [AllowHtml]
        [DisplayName("Account.PasswordRecovery.Email")]
        public string Email { get; set; }

        public string Result { get; set; }
    }
}
