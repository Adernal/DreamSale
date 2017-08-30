using Denmakers.DreamSale.ViewModels.Validators.Customers;
using FluentValidation.Attributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Denmakers.DreamSale.ViewModels.NonAdminVM.Customers
{
    [Validator(typeof(LoginValidator))]
    public partial class LoginModel
    {
        public int Id { get; set; }
        public bool CheckoutAsGuest { get; set; }

        [DisplayName("Account.Login.Fields.Email")]
        [AllowHtml]
        public string Email { get; set; }

        public bool UsernamesEnabled { get; set; }
        [DisplayName("Account.Login.Fields.UserName")]
        [AllowHtml]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        [DisplayName("Account.Login.Fields.Password")]
        [AllowHtml]
        public string Password { get; set; }

        [DisplayName("Account.Login.Fields.RememberMe")]
        public bool RememberMe { get; set; }

        public bool DisplayCaptcha { get; set; }
    }
}
