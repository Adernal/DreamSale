namespace Denmakers.DreamSale.Services.Shipping.Tracking.Fedex
{
    public enum DropoffType
    {
        /// <summary>
        /// Business service center
        /// </summary>
        BusinessServiceCenter = 0,
        /// <summary>
        /// Drop box
        /// </summary>
        DropBox = 10,
        /// <summary>
        /// Regular pickup
        /// </summary>
        RegularPickup = 20,
        /// <summary>
        /// Request courier
        /// </summary>
        RequestCourier = 30,
        /// <summary>
        /// Station
        /// </summary>
        Station = 40,
    }
}
