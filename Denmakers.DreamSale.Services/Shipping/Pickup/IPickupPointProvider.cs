using Denmakers.DreamSale.Model.Common;
using Denmakers.DreamSale.Services.Shipping.Tracking;

namespace Denmakers.DreamSale.Services.Shipping.Pickup
{
    public partial interface IPickupPointProvider
    {
        #region Properties

        /// <summary>
        /// Gets a shipment tracker
        /// </summary>
        IShipmentTracker ShipmentTracker { get; }

        #endregion

        #region Methods
        GetPickupPointsResponse GetPickupPoints(Address address);
        #endregion
    }
}
