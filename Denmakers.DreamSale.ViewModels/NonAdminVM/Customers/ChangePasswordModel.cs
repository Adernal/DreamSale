using FluentValidation.Attributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Denmakers.DreamSale.ViewModels.Validators.Customers;

namespace Denmakers.DreamSale.ViewModels.NonAdminVM.Customers
{
    [Validator(typeof(ChangePasswordValidator))]
    public partial class ChangePasswordModel
    {
        [AllowHtml]
        [DataType(DataType.Password)]
        [DisplayName("Account.ChangePassword.Fields.OldPassword")]
        public string OldPassword { get; set; }

        [AllowHtml]
        [DataType(DataType.Password)]
        [DisplayName("Account.ChangePassword.Fields.NewPassword")]
        public string NewPassword { get; set; }

        [AllowHtml]
        [DataType(DataType.Password)]
        [DisplayName("Account.ChangePassword.Fields.ConfirmNewPassword")]
        public string ConfirmNewPassword { get; set; }

        public string Result { get; set; }

    }
}
