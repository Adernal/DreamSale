
using DreamSale.Model.Common;

namespace DreamSale.Services.Common
{
    /// <summary>
    /// Address service interface
    /// </summary>
    public partial interface IAddressService
    {
        void DeleteAddress(Address address);

        int GetAddressTotalByCountryId(int countryId);

        int GetAddressTotalByStateProvinceId(int stateProvinceId);

        Address GetAddressById(int addressId);

        void InsertAddress(Address address);

        void UpdateAddress(Address address);

        bool IsAddressValid(Address address);
    }
}