using Denmakers.DreamSale.Model.Common;
using Denmakers.DreamSale.Model.Customers;
using Denmakers.DreamSale.Model.Directory;
using Denmakers.DreamSale.ViewModels.NonAdminVM.Addresses;
using System;
using System.Collections.Generic;

namespace Denmakers.DreamSale.ViewModels.Factories
{
    public partial interface IAddressModelFactory
    {
        /// <summary>
        /// Prepare address model
        /// </summary>
        /// <param name="model">Address model</param>
        /// <param name="address">Address entity</param>
        /// <param name="excludeProperties">Whether to exclude populating of model properties from the entity</param>
        /// <param name="addressSettings">Address settings</param>
        /// <param name="loadCountries">Countries loading function; pass null if countries do not need to load</param>
        /// <param name="prePopulateWithCustomerFields">Whether to populate model properties with the customer fields (used with the customer entity)</param>
        /// <param name="customer">Customer entity; required if prePopulateWithCustomerFields is true</param>
        /// <param name="overrideAttributesXml">Overridden address attributes in XML format; pass null to use CustomAttributes of the address entity</param>
        void PrepareAddressModel(AddressModel model,
            Address address, bool excludeProperties,
            AddressSettings addressSettings,
            Func<IList<Country>> loadCountries = null,
            bool prePopulateWithCustomerFields = false,
            Customer customer = null,
            string overrideAttributesXml = "");
    }
}
