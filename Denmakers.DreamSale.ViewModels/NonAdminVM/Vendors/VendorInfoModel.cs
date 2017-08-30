using Denmakers.DreamSale.ViewModels.Validators.Vendors;
using FluentValidation.Attributes;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;

namespace Denmakers.DreamSale.ViewModels.NonAdminVM.Vendors
{
    [Validator(typeof(VendorInfoValidator))]
    public class VendorInfoModel
    {
        public VendorInfoModel()
        {
            this.CustomProperties = new Dictionary<string, object>();
        }
        public int Id { get; set; }

        public Dictionary<string, object> CustomProperties { get; set; }

        [DisplayName("Account.VendorInfo.Name")]
        [AllowHtml]
        public string Name { get; set; }

        [DisplayName("Account.VendorInfo.Email")]
        [AllowHtml]
        public string Email { get; set; }

        [DisplayName("Account.VendorInfo.Description")]
        [AllowHtml]
        public string Description { get; set; }

        [DisplayName("Account.VendorInfo.Picture")]
        public string PictureUrl { get; set; }
    }
}
