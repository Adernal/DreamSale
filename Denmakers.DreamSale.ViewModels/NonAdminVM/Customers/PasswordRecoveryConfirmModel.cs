using Denmakers.DreamSale.ViewModels.Validators.Customers;
using FluentValidation.Attributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Denmakers.DreamSale.ViewModels.NonAdminVM.Customers
{
    [Validator(typeof(PasswordRecoveryConfirmValidator))]
    public partial class PasswordRecoveryConfirmModel
    {
        public int Id { get; set; }
        [AllowHtml]
        [DataType(DataType.Password)]
        [DisplayName("Account.PasswordRecovery.NewPassword")]
        public string NewPassword { get; set; }

        [AllowHtml]
        [DataType(DataType.Password)]
        [DisplayName("Account.PasswordRecovery.ConfirmNewPassword")]
        public string ConfirmNewPassword { get; set; }

        public bool DisablePasswordChanging { get; set; }
        public string Result { get; set; }
    }
}
