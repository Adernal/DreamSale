using Denmakers.DreamSale.Model.Catalog;
using Denmakers.DreamSale.ViewModels.Validators.Customers;
using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Denmakers.DreamSale.ViewModels.AdminVM.Customers
{
    [Validator(typeof(CustomerValidator))]
    public partial class CustomerVM
    {
        public CustomerVM()
        {
            this.AvailableTimeZones = new List<SelectListItem>();
            this.SendEmail = new SendEmailVM() { SendImmediately = true };
            this.SendPm = new SendPmVM();

            this.SelectedCustomerRoleIds = new List<int>();
            this.AvailableCustomerRoles = new List<SelectListItem>();

            this.AssociatedExternalAuthRecords = new List<AssociatedExternalAuthVM>();
            this.AvailableCountries = new List<SelectListItem>();
            this.AvailableStates = new List<SelectListItem>();
            this.AvailableVendors = new List<SelectListItem>();
            this.CustomerAttributes = new List<CustomerAttributeVM>();
            this.AvailableNewsletterSubscriptionStores = new List<StoreVM>();
            this.RewardPointsAvailableStores = new List<SelectListItem>();
        }

        public int Id { get; set; }
        public bool UsernamesEnabled { get; set; }

        [DisplayName("Admin.Customers.Customers.Fields.Username")]
        [AllowHtml]
        public string Username { get; set; }

        [DisplayName("Admin.Customers.Customers.Fields.Email")]
        [AllowHtml]
        public string Email { get; set; }

        [DisplayName("Admin.Customers.Customers.Fields.Password")]
        [AllowHtml]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("Admin.Customers.Customers.Fields.Vendor")]
        public int VendorId { get; set; }
        public IList<SelectListItem> AvailableVendors { get; set; }

        //form fields & properties
        public bool GenderEnabled { get; set; }
        [DisplayName("Admin.Customers.Customers.Fields.Gender")]
        public string Gender { get; set; }

        [DisplayName("Admin.Customers.Customers.Fields.FirstName")]
        [AllowHtml]
        public string FirstName { get; set; }
        [DisplayName("Admin.Customers.Customers.Fields.LastName")]
        [AllowHtml]
        public string LastName { get; set; }
        [DisplayName("Admin.Customers.Customers.Fields.FullName")]
        public string FullName { get; set; }

        public bool DateOfBirthEnabled { get; set; }
        [UIHint("DateNullable")]
        [DisplayName("Admin.Customers.Customers.Fields.DateOfBirth")]
        public DateTime? DateOfBirth { get; set; }

        public bool CompanyEnabled { get; set; }
        [DisplayName("Admin.Customers.Customers.Fields.Company")]
        [AllowHtml]
        public string Company { get; set; }

        public bool StreetAddressEnabled { get; set; }
        [DisplayName("Admin.Customers.Customers.Fields.StreetAddress")]
        [AllowHtml]
        public string StreetAddress { get; set; }

        public bool StreetAddress2Enabled { get; set; }
        [DisplayName("Admin.Customers.Customers.Fields.StreetAddress2")]
        [AllowHtml]
        public string StreetAddress2 { get; set; }

        public bool ZipPostalCodeEnabled { get; set; }
        [DisplayName("Admin.Customers.Customers.Fields.ZipPostalCode")]
        [AllowHtml]
        public string ZipPostalCode { get; set; }

        public bool CityEnabled { get; set; }
        [DisplayName("Admin.Customers.Customers.Fields.City")]
        [AllowHtml]
        public string City { get; set; }

        public bool CountryEnabled { get; set; }
        [DisplayName("Admin.Customers.Customers.Fields.Country")]
        public int CountryId { get; set; }
        public IList<SelectListItem> AvailableCountries { get; set; }

        public bool StateProvinceEnabled { get; set; }
        [DisplayName("Admin.Customers.Customers.Fields.StateProvince")]
        public int StateProvinceId { get; set; }
        public IList<SelectListItem> AvailableStates { get; set; }

        public bool PhoneEnabled { get; set; }
        [DisplayName("Admin.Customers.Customers.Fields.Phone")]
        [AllowHtml]
        public string Phone { get; set; }

        public bool FaxEnabled { get; set; }
        [DisplayName("Admin.Customers.Customers.Fields.Fax")]
        [AllowHtml]
        public string Fax { get; set; }

        public List<CustomerAttributeVM> CustomerAttributes { get; set; }

        [DisplayName("Admin.Customers.Customers.Fields.RegisteredInStore")]
        public string RegisteredInStore { get; set; }



        [DisplayName("Admin.Customers.Customers.Fields.AdminComment")]
        [AllowHtml]
        public string AdminComment { get; set; }

        [DisplayName("Admin.Customers.Customers.Fields.IsTaxExempt")]
        public bool IsTaxExempt { get; set; }

        [DisplayName("Admin.Customers.Customers.Fields.Active")]
        public bool Active { get; set; }

        [DisplayName("Admin.Customers.Customers.Fields.Affiliate")]
        public int AffiliateId { get; set; }
        [DisplayName("Admin.Customers.Customers.Fields.Affiliate")]
        public string AffiliateName { get; set; }




        //time zone
        [DisplayName("Admin.Customers.Customers.Fields.TimeZoneId")]
        [AllowHtml]
        public string TimeZoneId { get; set; }

        public bool AllowCustomersToSetTimeZone { get; set; }

        public IList<SelectListItem> AvailableTimeZones { get; set; }





        //EU VAT
        [DisplayName("Admin.Customers.Customers.Fields.VatNumber")]
        [AllowHtml]
        public string VatNumber { get; set; }

        public string VatNumberStatusNote { get; set; }

        public bool DisplayVatNumber { get; set; }





        //registration date
        [DisplayName("Admin.Customers.Customers.Fields.CreatedOn")]
        public DateTime CreatedOn { get; set; }
        [DisplayName("Admin.Customers.Customers.Fields.LastActivityDate")]
        public DateTime LastActivityDate { get; set; }

        //IP adderss
        [DisplayName("Admin.Customers.Customers.Fields.IPAddress")]
        public string LastIpAddress { get; set; }


        [DisplayName("Admin.Customers.Customers.Fields.LastVisitedPage")]
        public string LastVisitedPage { get; set; }


        //customer roles
        [DisplayName("Admin.Customers.Customers.Fields.CustomerRoles")]
        public string CustomerRoleNames { get; set; }
        public List<SelectListItem> AvailableCustomerRoles { get; set; }
        [DisplayName("Admin.Customers.Customers.Fields.CustomerRoles")]
        [UIHint("MultiSelect")]
        public IList<int> SelectedCustomerRoleIds { get; set; }


        //newsletter subscriptions (per store)
        [DisplayName("Admin.Customers.Customers.Fields.Newsletter")]
        public List<StoreVM> AvailableNewsletterSubscriptionStores { get; set; }
        [DisplayName("Admin.Customers.Customers.Fields.Newsletter")]
        public int[] SelectedNewsletterSubscriptionStoreIds { get; set; }



        //reward points history
        public bool DisplayRewardPointsHistory { get; set; }
        [DisplayName("Admin.Customers.Customers.RewardPoints.Fields.AddRewardPointsValue")]
        public int AddRewardPointsValue { get; set; }
        [DisplayName("Admin.Customers.Customers.RewardPoints.Fields.AddRewardPointsMessage")]
        [AllowHtml]
        public string AddRewardPointsMessage { get; set; }
        [DisplayName("Admin.Customers.Customers.RewardPoints.Fields.AddRewardPointsStore")]
        public int AddRewardPointsStoreId { get; set; }
        [DisplayName("Admin.Customers.Customers.RewardPoints.Fields.AddRewardPointsStore")]
        public IList<SelectListItem> RewardPointsAvailableStores { get; set; }



        //send email model
        public SendEmailVM SendEmail { get; set; }
        //send PM model
        public SendPmVM SendPm { get; set; }
        //send the welcome message
        public bool AllowSendingOfWelcomeMessage { get; set; }
        //re-send the activation message
        public bool AllowReSendingOfActivationMessage { get; set; }

        [DisplayName("Admin.Customers.Customers.AssociatedExternalAuth")]
        public IList<AssociatedExternalAuthVM> AssociatedExternalAuthRecords { get; set; }


        #region Nested classes

        public partial class StoreVM
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public partial class AssociatedExternalAuthVM
        {
            public int Id { get; set; }
            [DisplayName("Admin.Customers.Customers.AssociatedExternalAuth.Fields.Email")]
            public string Email { get; set; }

            [DisplayName("Admin.Customers.Customers.AssociatedExternalAuth.Fields.ExternalIdentifier")]
            public string ExternalIdentifier { get; set; }

            [DisplayName("Admin.Customers.Customers.AssociatedExternalAuth.Fields.AuthMethodName")]
            public string AuthMethodName { get; set; }
        }

        public partial class RewardPointsHistoryVM
        {
            public int Id { get; set; }
            [DisplayName("Admin.Customers.Customers.RewardPoints.Fields.Store")]
            public string StoreName { get; set; }

            [DisplayName("Admin.Customers.Customers.RewardPoints.Fields.Points")]
            public int Points { get; set; }

            [DisplayName("Admin.Customers.Customers.RewardPoints.Fields.PointsBalance")]
            public string PointsBalance { get; set; }

            [DisplayName("Admin.Customers.Customers.RewardPoints.Fields.Message")]
            [AllowHtml]
            public string Message { get; set; }

            [DisplayName("Admin.Customers.Customers.RewardPoints.Fields.Date")]
            public DateTime CreatedOn { get; set; }
        }

        public partial class SendEmailVM
        {
            public int Id { get; set; }
            [DisplayName("Admin.Customers.Customers.SendEmail.Subject")]
            [AllowHtml]
            public string Subject { get; set; }

            [DisplayName("Admin.Customers.Customers.SendEmail.Body")]
            [AllowHtml]
            public string Body { get; set; }

            [DisplayName("Admin.Customers.Customers.SendEmail.SendImmediately")]
            public bool SendImmediately { get; set; }

            [DisplayName("Admin.Customers.Customers.SendEmail.DontSendBeforeDate")]
            [UIHint("DateTimeNullable")]
            public DateTime? DontSendBeforeDate { get; set; }
        }

        public partial class SendPmVM
        {
            public int Id { get; set; }
            [DisplayName("Admin.Customers.Customers.SendPM.Subject")]
            public string Subject { get; set; }

            [DisplayName("Admin.Customers.Customers.SendPM.Message")]
            public string Message { get; set; }
        }

        public partial class OrderVM
        {
            public int Id { get; set; }
            [DisplayName("Admin.Customers.Customers.Orders.CustomOrderNumber")]
            public string CustomOrderNumber { get; set; }

            [DisplayName("Admin.Customers.Customers.Orders.OrderStatus")]
            public string OrderStatus { get; set; }
            [DisplayName("Admin.Customers.Customers.Orders.OrderStatus")]
            public int OrderStatusId { get; set; }

            [DisplayName("Admin.Customers.Customers.Orders.PaymentStatus")]
            public string PaymentStatus { get; set; }

            [DisplayName("Admin.Customers.Customers.Orders.ShippingStatus")]
            public string ShippingStatus { get; set; }

            [DisplayName("Admin.Customers.Customers.Orders.OrderTotal")]
            public string OrderTotal { get; set; }

            [DisplayName("Admin.Customers.Customers.Orders.Store")]
            public string StoreName { get; set; }

            [DisplayName("Admin.Customers.Customers.Orders.CreatedOn")]
            public DateTime CreatedOn { get; set; }
        }

        public partial class ActivityLogVM
        {
            public int Id { get; set; }
            [DisplayName("Admin.Customers.Customers.ActivityLog.ActivityLogType")]
            public string ActivityLogTypeName { get; set; }
            [DisplayName("Admin.Customers.Customers.ActivityLog.Comment")]
            public string Comment { get; set; }
            [DisplayName("Admin.Customers.Customers.ActivityLog.CreatedOn")]
            public DateTime CreatedOn { get; set; }
            [DisplayName("Admin.Customers.Customers.ActivityLog.IpAddress")]
            public string IpAddress { get; set; }
        }

        public partial class BackInStockSubscriptionVM
        {
            public int Id { get; set; }

            [DisplayName("Admin.Customers.Customers.BackInStockSubscriptions.Store")]
            public string StoreName { get; set; }
            [DisplayName("Admin.Customers.Customers.BackInStockSubscriptions.Product")]
            public int ProductId { get; set; }
            [DisplayName("Admin.Customers.Customers.BackInStockSubscriptions.Product")]
            public string ProductName { get; set; }
            [DisplayName("Admin.Customers.Customers.BackInStockSubscriptions.CreatedOn")]
            public DateTime CreatedOn { get; set; }
        }

        public partial class CustomerAttributeVM
        {
            public CustomerAttributeVM()
            {
                Values = new List<CustomerAttributeValueVM>();
            }

            public int Id { get; set; }
            public string Name { get; set; }

            public bool IsRequired { get; set; }

            /// <summary>
            /// Default value for textboxes
            /// </summary>
            public string DefaultValue { get; set; }

            public AttributeControlType AttributeControlType { get; set; }

            public IList<CustomerAttributeValueVM> Values { get; set; }

        }

        public partial class CustomerAttributeValueVM
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public bool IsPreSelected { get; set; }
        }

        #endregion
    }
}
