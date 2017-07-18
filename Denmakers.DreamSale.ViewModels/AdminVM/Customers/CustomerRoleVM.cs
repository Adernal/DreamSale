using Denmakers.DreamSale.ViewModels.Validators.Customers;
using FluentValidation.Attributes;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;

namespace Denmakers.DreamSale.ViewModels.AdminVM.Customers
{
    [Validator(typeof(CustomerRoleValidator))]
    public partial class CustomerRoleVM
    {
        public int Id { get; set; }
        [DisplayName("Admin.Customers.CustomerRoles.Fields.Name")]
        [AllowHtml]
        public string Name { get; set; }

        [DisplayName("Admin.Customers.CustomerRoles.Fields.FreeShipping")]
        [AllowHtml]
        public bool FreeShipping { get; set; }

        [DisplayName("Admin.Customers.CustomerRoles.Fields.TaxExempt")]
        public bool TaxExempt { get; set; }

        [DisplayName("Admin.Customers.CustomerRoles.Fields.Active")]
        public bool Active { get; set; }

        [DisplayName("Admin.Customers.CustomerRoles.Fields.IsSystemRole")]
        public bool IsSystemRole { get; set; }

        [DisplayName("Admin.Customers.CustomerRoles.Fields.SystemName")]
        public string SystemName { get; set; }

        [DisplayName("Admin.Customers.CustomerRoles.Fields.EnablePasswordLifetime")]
        public bool EnablePasswordLifetime { get; set; }

        [DisplayName("Admin.Customers.CustomerRoles.Fields.PurchasedWithProduct")]
        public int PurchasedWithProductId { get; set; }

        [DisplayName("Admin.Customers.CustomerRoles.Fields.PurchasedWithProduct")]
        public string PurchasedWithProductName { get; set; }
        public Dictionary<string, object> CustomProperties { get; set; }

        #region Nested classes

        public partial class AssociateProductToCustomerRoleVM
        {
            public AssociateProductToCustomerRoleVM()
            {
                AvailableCategories = new List<SelectListItem>();
                AvailableManufacturers = new List<SelectListItem>();
                AvailableStores = new List<SelectListItem>();
                AvailableVendors = new List<SelectListItem>();
                AvailableProductTypes = new List<SelectListItem>();
                CustomProperties = new Dictionary<string, object>();
            }
            public int Id { get; set; }

            public Dictionary<string, object> CustomProperties { get; set; }

            [DisplayName("Admin.Catalog.Products.List.SearchProductName")]
            [AllowHtml]
            public string SearchProductName { get; set; }
            [DisplayName("Admin.Catalog.Products.List.SearchCategory")]
            public int SearchCategoryId { get; set; }
            [DisplayName("Admin.Catalog.Products.List.SearchManufacturer")]
            public int SearchManufacturerId { get; set; }
            [DisplayName("Admin.Catalog.Products.List.SearchStore")]
            public int SearchStoreId { get; set; }
            [DisplayName("Admin.Catalog.Products.List.SearchVendor")]
            public int SearchVendorId { get; set; }
            [DisplayName("Admin.Catalog.Products.List.SearchProductType")]
            public int SearchProductTypeId { get; set; }

            public IList<SelectListItem> AvailableCategories { get; set; }
            public IList<SelectListItem> AvailableManufacturers { get; set; }
            public IList<SelectListItem> AvailableStores { get; set; }
            public IList<SelectListItem> AvailableVendors { get; set; }
            public IList<SelectListItem> AvailableProductTypes { get; set; }

            //vendor
            public bool IsLoggedInAsVendor { get; set; }


            public int AssociatedToProductId { get; set; }
        }
        #endregion
    }
}
