using Denmakers.DreamSale.ViewModels.Validators.Vendors;
using FluentValidation.Attributes;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;

namespace Denmakers.DreamSale.ViewModels.NonAdminVM.Vendors
{
    [Validator(typeof(ApplyVendorValidator))]
    public partial class ApplyVendorModel
    {
        public ApplyVendorModel()
        {
            this.CustomProperties = new Dictionary<string, object>();
        }
        public int Id { get; set; }

        public Dictionary<string, object> CustomProperties { get; set; }

        [DisplayName("Vendors.ApplyAccount.Name")]
        [AllowHtml]
        public string Name { get; set; }

        [DisplayName("Vendors.ApplyAccount.Email")]
        [AllowHtml]
        public string Email { get; set; }

        [DisplayName("Vendors.ApplyAccount.Description")]
        [AllowHtml]
        public string Description { get; set; }

        public bool DisplayCaptcha { get; set; }

        public bool DisableFormInput { get; set; }
        public string Result { get; set; }
    }
}
