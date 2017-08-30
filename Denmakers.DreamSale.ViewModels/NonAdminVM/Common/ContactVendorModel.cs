using Denmakers.DreamSale.ViewModels.Validators.Common;
using FluentValidation.Attributes;
using System.ComponentModel;
using System.Web.Mvc;

namespace Denmakers.DreamSale.ViewModels.NonAdminVM.Common
{
    [Validator(typeof(ContactVendorValidator))]
    public partial class ContactVendorModel
    {
        public int Id { get; set; }
        public int VendorId { get; set; }
        public string VendorName { get; set; }

        [AllowHtml]
        [DisplayName("ContactVendor.Email")]
        public string Email { get; set; }

        [AllowHtml]
        [DisplayName("ContactVendor.Subject")]
        public string Subject { get; set; }
        public bool SubjectEnabled { get; set; }

        [AllowHtml]
        [DisplayName("ContactVendor.Enquiry")]
        public string Enquiry { get; set; }

        [AllowHtml]
        [DisplayName("ContactVendor.FullName")]
        public string FullName { get; set; }

        public bool SuccessfullySent { get; set; }
        public string Result { get; set; }

        public bool DisplayCaptcha { get; set; }
    }
}
