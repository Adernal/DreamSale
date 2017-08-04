using Denmakers.DreamSale.Data.Context;
using Denmakers.DreamSale.Model.Common;
using Denmakers.DreamSale.Model.Shipping;
using Denmakers.DreamSale.Services.Addresses;
using Denmakers.DreamSale.Services.Directory;
using Denmakers.DreamSale.Services.Localization;
using Denmakers.DreamSale.Services.Pickup;
using Denmakers.DreamSale.Services.Shipping.Tracking;

namespace Denmakers.DreamSale.Services.Shipping.Pickup
{
    public class PickupInStoreProvider : IPickupPointProvider
    {
        #region Fields

        private readonly IAddressService _addressService;
        private readonly ICountryService _countryService;
        private readonly ILocalizationService _localizationService;
        private readonly IStateProvinceService _stateProvinceService;
        private readonly IStoreContext _storeContext;
        private readonly IStorePickupPointService _storePickupPointService;
        //private readonly StorePickupPointObjectContext _objectContext;

        #endregion

        #region Properties

        /// <summary>
        /// Gets a shipment tracker
        /// </summary>
        public IShipmentTracker ShipmentTracker
        {
            get { return null; }
        }

        #endregion

        #region Ctor

        public PickupInStoreProvider(IAddressService addressService,
            ICountryService countryService,
            ILocalizationService localizationService,
            IStateProvinceService stateProvinceService,
            IStoreContext storeContext,
            IStorePickupPointService storePickupPointService
            //StorePickupPointObjectContext objectContext
            )
        {
            this._addressService = addressService;
            this._countryService = countryService;
            this._localizationService = localizationService;
            this._stateProvinceService = stateProvinceService;
            this._storeContext = storeContext;
            this._storePickupPointService = storePickupPointService;
            //this._objectContext = objectContext;
        }

        #endregion

        #region Methods
        /// <summary>
        /// Get pickup points for the address
        /// </summary>
        /// <param name="address">Address</param>
        /// <returns>Represents a response of getting pickup points</returns>
        public GetPickupPointsResponse GetPickupPoints(Address address)
        {
            var result = new GetPickupPointsResponse();

            foreach (var point in _storePickupPointService.GetAllStorePickupPoints(_storeContext.CurrentStore.Id))
            {
                var pointAddress = _addressService.GetAddressById(point.AddressId);
                if (pointAddress != null)
                    result.PickupPoints.Add(new PickupPoint
                    {
                        Id = point.Id.ToString(),
                        Name = point.Name,
                        Description = point.Description,
                        Address = pointAddress.Address1,
                        City = pointAddress.City,
                        StateAbbreviation = pointAddress.StateProvince != null ? pointAddress.StateProvince.Abbreviation : string.Empty,
                        CountryCode = pointAddress.Country != null ? pointAddress.Country.TwoLetterIsoCode : string.Empty,
                        ZipPostalCode = pointAddress.ZipPostalCode,
                        OpeningHours = point.OpeningHours,
                        PickupFee = point.PickupFee,
                        ProviderSystemName = ""// PluginDescriptor.SystemName
                    });
            }

            if (result.PickupPoints.Count == 0)
                result.AddError(_localizationService.GetResource("Plugins.Pickup.PickupInStore.NoPickupPoints"));

            return result;
        }
        #endregion
    }
}
