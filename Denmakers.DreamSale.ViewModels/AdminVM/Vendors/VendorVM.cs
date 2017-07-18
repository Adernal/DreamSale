using Denmakers.DreamSale.ViewModels.AdminVM.Addresses;
using Denmakers.DreamSale.ViewModels.Validators.Vendors;
using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Denmakers.DreamSale.ViewModels.AdminVM.Vendors
{
    [Validator(typeof(VendorValidator))]
    public partial class VendorVM
    {
        public VendorVM()
        {
            if (PageSize < 1)
            {
                PageSize = 5;
            }
            Address = new AddressVM();

            Locales = new List<VendorLocalizedVM>();
            AssociatedCustomers = new List<AssociatedCustomerInfo>();
            CustomProperties = new Dictionary<string, object>();
        }

        public int Id { get; set; }
        public Dictionary<string, object> CustomProperties { get; set; }
        [DisplayName("Admin.Vendors.Fields.Name")]
        [AllowHtml]
        public string Name { get; set; }

        [DisplayName("Admin.Vendors.Fields.Email")]
        [AllowHtml]
        public string Email { get; set; }

        [DisplayName("Admin.Vendors.Fields.Description")]
        [AllowHtml]
        public string Description { get; set; }

        [UIHint("Picture")]
        [DisplayName("Admin.Vendors.Fields.Picture")]
        public int PictureId { get; set; }

        [DisplayName("Admin.Vendors.Fields.AdminComment")]
        [AllowHtml]
        public string AdminComment { get; set; }

        public AddressVM Address { get; set; }

        [DisplayName("Admin.Vendors.Fields.Active")]
        public bool Active { get; set; }

        [DisplayName("Admin.Vendors.Fields.DisplayOrder")]
        public int DisplayOrder { get; set; }


        [DisplayName("Admin.Vendors.Fields.MetaKeywords")]
        [AllowHtml]
        public string MetaKeywords { get; set; }

        [DisplayName("Admin.Vendors.Fields.MetaDescription")]
        [AllowHtml]
        public string MetaDescription { get; set; }

        [DisplayName("Admin.Vendors.Fields.MetaTitle")]
        [AllowHtml]
        public string MetaTitle { get; set; }

        [DisplayName("Admin.Vendors.Fields.SeName")]
        [AllowHtml]
        public string SeName { get; set; }

        [DisplayName("Admin.Vendors.Fields.PageSize")]
        public int PageSize { get; set; }

        [DisplayName("Admin.Vendors.Fields.AllowCustomersToSelectPageSize")]
        public bool AllowCustomersToSelectPageSize { get; set; }

        [DisplayName("Admin.Vendors.Fields.PageSizeOptions")]
        public string PageSizeOptions { get; set; }

        public IList<VendorLocalizedVM> Locales { get; set; }

        [DisplayName("Admin.Vendors.Fields.AssociatedCustomerEmails")]
        public IList<AssociatedCustomerInfo> AssociatedCustomers { get; set; }



        //vendor notes
        [DisplayName("Admin.Vendors.VendorNotes.Fields.Note")]
        [AllowHtml]
        public string AddVendorNoteMessage { get; set; }




        #region Nested classes

        public class AssociatedCustomerInfo
        {
            public int Id { get; set; }
            public string Email { get; set; }
        }


        public partial class VendorNote
        {
            public int Id { get; set; }
            public int VendorId { get; set; }
            [DisplayName("Admin.Vendors.VendorNotes.Fields.Note")]
            public string Note { get; set; }
            [DisplayName("Admin.Vendors.VendorNotes.Fields.CreatedOn")]
            public DateTime CreatedOn { get; set; }
        }
        #endregion

    }

    public partial class VendorLocalizedVM
    {
        public int Id { get; set; }
        public int LanguageId { get; set; }

        [DisplayName("Admin.Vendors.Fields.Name")]
        [AllowHtml]
        public string Name { get; set; }

        [DisplayName("Admin.Vendors.Fields.Description")]
        [AllowHtml]
        public string Description { get; set; }

        [DisplayName("Admin.Vendors.Fields.MetaKeywords")]
        [AllowHtml]
        public string MetaKeywords { get; set; }

        [DisplayName("Admin.Vendors.Fields.MetaDescription")]
        [AllowHtml]
        public string MetaDescription { get; set; }

        [DisplayName("Admin.Vendors.Fields.MetaTitle")]
        [AllowHtml]
        public string MetaTitle { get; set; }

        [DisplayName("Admin.Vendors.Fields.SeName")]
        [AllowHtml]
        public string SeName { get; set; }
    }
}
