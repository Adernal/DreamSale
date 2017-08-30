using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;

namespace Denmakers.DreamSale.ViewModels.NonAdminVM.ShoppingCart
{
    public partial class EstimateShippingModel
    {
        public EstimateShippingModel()
        {
            AvailableCountries = new List<SelectListItem>();
            AvailableStates = new List<SelectListItem>();
        }
        public int Id { get; set; }
        public bool Enabled { get; set; }

        [DisplayName("ShoppingCart.EstimateShipping.Country")]
        public int? CountryId { get; set; }
        [DisplayName("ShoppingCart.EstimateShipping.StateProvince")]
        public int? StateProvinceId { get; set; }
        [DisplayName("ShoppingCart.EstimateShipping.ZipPostalCode")]
        public string ZipPostalCode { get; set; }

        public IList<SelectListItem> AvailableCountries { get; set; }
        public IList<SelectListItem> AvailableStates { get; set; }
    }

    public partial class EstimateShippingResultModel
    {
        public EstimateShippingResultModel()
        {
            ShippingOptions = new List<ShippingOptionModel>();
            Warnings = new List<string>();
        }
        public int Id { get; set; }
        public IList<ShippingOptionModel> ShippingOptions { get; set; }

        public IList<string> Warnings { get; set; }

        #region Nested Classes

        public partial class ShippingOptionModel
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public string Description { get; set; }

            public string Price { get; set; }
        }

        #endregion
    }
}
