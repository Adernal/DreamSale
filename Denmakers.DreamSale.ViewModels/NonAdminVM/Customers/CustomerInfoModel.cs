using Denmakers.DreamSale.ViewModels.Validators.Customers;
using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;

namespace Denmakers.DreamSale.ViewModels.NonAdminVM.Customers
{
    [Validator(typeof(CustomerInfoValidator))]
    public partial class CustomerInfoModel
    {
        public CustomerInfoModel()
        {
            this.AvailableTimeZones = new List<SelectListItem>();
            this.AvailableCountries = new List<SelectListItem>();
            this.AvailableStates = new List<SelectListItem>();
            this.AssociatedExternalAuthRecords = new List<AssociatedExternalAuthModel>();
            this.CustomerAttributes = new List<CustomerAttributeModel>();
        }
        public int Id { get; set; }
        [DisplayName("Account.Fields.Email")]
        [AllowHtml]
        public string Email { get; set; }
        [DisplayName("Account.Fields.EmailToRevalidate")]
        public string EmailToRevalidate { get; set; }

        public bool CheckUsernameAvailabilityEnabled { get; set; }
        public bool AllowUsersToChangeUsernames { get; set; }
        public bool UsernamesEnabled { get; set; }
        [DisplayName("Account.Fields.Username")]
        [AllowHtml]
        public string Username { get; set; }

        //form fields & properties
        public bool GenderEnabled { get; set; }
        [DisplayName("Account.Fields.Gender")]
        public string Gender { get; set; }

        [DisplayName("Account.Fields.FirstName")]
        [AllowHtml]
        public string FirstName { get; set; }
        [DisplayName("Account.Fields.LastName")]
        [AllowHtml]
        public string LastName { get; set; }


        public bool DateOfBirthEnabled { get; set; }
        [DisplayName("Account.Fields.DateOfBirth")]
        public int? DateOfBirthDay { get; set; }
        [DisplayName("Account.Fields.DateOfBirth")]
        public int? DateOfBirthMonth { get; set; }
        [DisplayName("Account.Fields.DateOfBirth")]
        public int? DateOfBirthYear { get; set; }
        public bool DateOfBirthRequired { get; set; }
        public DateTime? ParseDateOfBirth()
        {
            if (!DateOfBirthYear.HasValue || !DateOfBirthMonth.HasValue || !DateOfBirthDay.HasValue)
                return null;

            DateTime? dateOfBirth = null;
            try
            {
                dateOfBirth = new DateTime(DateOfBirthYear.Value, DateOfBirthMonth.Value, DateOfBirthDay.Value);
            }
            catch { }
            return dateOfBirth;
        }

        public bool CompanyEnabled { get; set; }
        public bool CompanyRequired { get; set; }
        [DisplayName("Account.Fields.Company")]
        [AllowHtml]
        public string Company { get; set; }

        public bool StreetAddressEnabled { get; set; }
        public bool StreetAddressRequired { get; set; }
        [DisplayName("Account.Fields.StreetAddress")]
        [AllowHtml]
        public string StreetAddress { get; set; }

        public bool StreetAddress2Enabled { get; set; }
        public bool StreetAddress2Required { get; set; }
        [DisplayName("Account.Fields.StreetAddress2")]
        [AllowHtml]
        public string StreetAddress2 { get; set; }

        public bool ZipPostalCodeEnabled { get; set; }
        public bool ZipPostalCodeRequired { get; set; }
        [DisplayName("Account.Fields.ZipPostalCode")]
        [AllowHtml]
        public string ZipPostalCode { get; set; }

        public bool CityEnabled { get; set; }
        public bool CityRequired { get; set; }
        [DisplayName("Account.Fields.City")]
        [AllowHtml]
        public string City { get; set; }

        public bool CountryEnabled { get; set; }
        public bool CountryRequired { get; set; }
        [DisplayName("Account.Fields.Country")]
        public int CountryId { get; set; }
        public IList<SelectListItem> AvailableCountries { get; set; }

        public bool StateProvinceEnabled { get; set; }
        public bool StateProvinceRequired { get; set; }
        [DisplayName("Account.Fields.StateProvince")]
        public int StateProvinceId { get; set; }
        public IList<SelectListItem> AvailableStates { get; set; }

        public bool PhoneEnabled { get; set; }
        public bool PhoneRequired { get; set; }
        [DisplayName("Account.Fields.Phone")]
        [AllowHtml]
        public string Phone { get; set; }

        public bool FaxEnabled { get; set; }
        public bool FaxRequired { get; set; }
        [DisplayName("Account.Fields.Fax")]
        [AllowHtml]
        public string Fax { get; set; }

        public bool NewsletterEnabled { get; set; }
        [DisplayName("Account.Fields.Newsletter")]
        public bool Newsletter { get; set; }

        //preferences
        public bool SignatureEnabled { get; set; }
        [DisplayName("Account.Fields.Signature")]
        [AllowHtml]
        public string Signature { get; set; }

        //time zone
        [DisplayName("Account.Fields.TimeZone")]
        public string TimeZoneId { get; set; }
        public bool AllowCustomersToSetTimeZone { get; set; }
        public IList<SelectListItem> AvailableTimeZones { get; set; }

        //EU VAT
        [DisplayName("Account.Fields.VatNumber")]
        [AllowHtml]
        public string VatNumber { get; set; }
        public string VatNumberStatusNote { get; set; }
        public bool DisplayVatNumber { get; set; }

        //external authentication
        [DisplayName("Account.AssociatedExternalAuth")]
        public IList<AssociatedExternalAuthModel> AssociatedExternalAuthRecords { get; set; }
        public int NumberOfExternalAuthenticationProviders { get; set; }

        public IList<CustomerAttributeModel> CustomerAttributes { get; set; }

        #region Nested classes

        public partial class AssociatedExternalAuthModel 
        {
            public int Id { get; set; }
            public string Email { get; set; }

            public string ExternalIdentifier { get; set; }

            public string AuthMethodName { get; set; }
        }

        #endregion
    }
}
