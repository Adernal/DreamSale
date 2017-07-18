using Denmakers.DreamSale.Model.Catalog;
using Denmakers.DreamSale.ViewModels.Validators.Addresses;
using FluentValidation.Attributes;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;

namespace Denmakers.DreamSale.ViewModels.AdminVM.Addresses
{
    [Validator(typeof(AddressValidator))]
    public partial class AddressVM
    {
        public AddressVM()
        {
            AvailableCountries = new List<SelectListItem>();
            AvailableStates = new List<SelectListItem>();
            CustomAddressAttributes = new List<AddressAttributeVM>();
            CustomProperties = new Dictionary<string, object>();
        }
        public int Id { get; set; }
        public Dictionary<string, object> CustomProperties { get; set; }

        [DisplayName("Admin.Address.Fields.FirstName")]
        [AllowHtml]
        public string FirstName { get; set; }

        [DisplayName("Admin.Address.Fields.LastName")]
        [AllowHtml]
        public string LastName { get; set; }

        [DisplayName("Admin.Address.Fields.Email")]
        [AllowHtml]
        public string Email { get; set; }

        [DisplayName("Admin.Address.Fields.Company")]
        [AllowHtml]
        public string Company { get; set; }

        [DisplayName("Admin.Address.Fields.Country")]
        public int? CountryId { get; set; }

        [DisplayName("Admin.Address.Fields.Country")]
        [AllowHtml]
        public string CountryName { get; set; }

        [DisplayName("Admin.Address.Fields.StateProvince")]
        public int? StateProvinceId { get; set; }

        [DisplayName("Admin.Address.Fields.StateProvince")]
        [AllowHtml]
        public string StateProvinceName { get; set; }

        [DisplayName("Admin.Address.Fields.City")]
        [AllowHtml]
        public string City { get; set; }

        [DisplayName("Admin.Address.Fields.Address1")]
        [AllowHtml]
        public string Address1 { get; set; }

        [DisplayName("Admin.Address.Fields.Address2")]
        [AllowHtml]
        public string Address2 { get; set; }

        [DisplayName("Admin.Address.Fields.ZipPostalCode")]
        [AllowHtml]
        public string ZipPostalCode { get; set; }

        [DisplayName("Admin.Address.Fields.PhoneNumber")]
        [AllowHtml]
        public string PhoneNumber { get; set; }

        [DisplayName("Admin.Address.Fields.FaxNumber")]
        [AllowHtml]
        public string FaxNumber { get; set; }

        //address in HTML format (usually used in grids)
        [DisplayName("Admin.Address")]
        public string AddressHtml { get; set; }

        //formatted custom address attributes
        public string FormattedCustomAddressAttributes { get; set; }
        public IList<AddressAttributeVM> CustomAddressAttributes { get; set; }


        public IList<SelectListItem> AvailableCountries { get; set; }
        public IList<SelectListItem> AvailableStates { get; set; }



        public bool FirstNameEnabled { get; set; }
        public bool FirstNameRequired { get; set; }
        public bool LastNameEnabled { get; set; }
        public bool LastNameRequired { get; set; }
        public bool EmailEnabled { get; set; }
        public bool EmailRequired { get; set; }
        public bool CompanyEnabled { get; set; }
        public bool CompanyRequired { get; set; }
        public bool CountryEnabled { get; set; }
        public bool CountryRequired { get; set; }
        public bool StateProvinceEnabled { get; set; }
        public bool CityEnabled { get; set; }
        public bool CityRequired { get; set; }
        public bool StreetAddressEnabled { get; set; }
        public bool StreetAddressRequired { get; set; }
        public bool StreetAddress2Enabled { get; set; }
        public bool StreetAddress2Required { get; set; }
        public bool ZipPostalCodeEnabled { get; set; }
        public bool ZipPostalCodeRequired { get; set; }
        public bool PhoneEnabled { get; set; }
        public bool PhoneRequired { get; set; }
        public bool FaxEnabled { get; set; }
        public bool FaxRequired { get; set; }


        #region Nested classes

        public partial class AddressAttributeVM
        {
            public AddressAttributeVM()
            {
                Values = new List<AddressAttributeValueVM>();
            }

            public int Id { get; set; }
            public string Name { get; set; }

            public bool IsRequired { get; set; }

            /// <summary>
            /// Selected value for textboxes
            /// </summary>
            public string DefaultValue { get; set; }

            public AttributeControlType AttributeControlType { get; set; }

            public IList<AddressAttributeValueVM> Values { get; set; }
        }

        public partial class AddressAttributeValueVM
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public bool IsPreSelected { get; set; }
        }

        #endregion
    }
}
