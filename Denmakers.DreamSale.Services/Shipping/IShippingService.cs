using Denmakers.DreamSale.Model.Common;
using Denmakers.DreamSale.Model.Customers;
using Denmakers.DreamSale.Model.Orders;
using Denmakers.DreamSale.Model.Shipping;
using Denmakers.DreamSale.Services.Shipping.Pickup;
using System.Collections.Generic;

namespace Denmakers.DreamSale.Services.Shipping
{
    public partial interface IShippingService
    {
        #region Shipping methods
        /// <summary>
        /// Gets a shipping method
        /// </summary>
        /// <param name="shippingMethodId">The shipping method identifier</param>
        /// <returns>Shipping method</returns>
        ShippingMethod GetShippingMethodById(int shippingMethodId);

        /// <summary>
        /// Gets all shipping methods
        /// </summary>
        /// <param name="filterByCountryId">The country indentifier to filter by</param>
        /// <returns>Shipping methods</returns>
        IList<ShippingMethod> GetAllShippingMethods(int? filterByCountryId = null);

        /// <summary>
        /// Inserts a shipping method
        /// </summary>
        /// <param name="shippingMethod">Shipping method</param>
        void InsertShippingMethod(ShippingMethod shippingMethod);

        /// <summary>
        /// Updates the shipping method
        /// </summary>
        /// <param name="shippingMethod">Shipping method</param>
        void UpdateShippingMethod(ShippingMethod shippingMethod);

        /// <summary>
        /// Deletes a shipping method
        /// </summary>
        /// <param name="shippingMethod">The shipping method</param>
        void DeleteShippingMethod(ShippingMethod shippingMethod);
        #endregion

        #region Warehouses

        /// <summary>
        /// Deletes a warehouse
        /// </summary>
        /// <param name="warehouse">The warehouse</param>
        void DeleteWarehouse(Warehouse warehouse);

        /// <summary>
        /// Gets a warehouse
        /// </summary>
        /// <param name="warehouseId">The warehouse identifier</param>
        /// <returns>Warehouse</returns>
        Warehouse GetWarehouseById(int warehouseId);

        /// <summary>
        /// Gets all warehouses
        /// </summary>
        /// <returns>Warehouses</returns>
        IList<Warehouse> GetAllWarehouses();

        /// <summary>
        /// Inserts a warehouse
        /// </summary>
        /// <param name="warehouse">Warehouse</param>
        void InsertWarehouse(Warehouse warehouse);

        /// <summary>
        /// Updates the warehouse
        /// </summary>
        /// <param name="warehouse">Warehouse</param>
        void UpdateWarehouse(Warehouse warehouse);

        #endregion

        #region Workflow

        /// <summary>
        /// Gets shopping cart item weight (of one item)
        /// </summary>
        /// <param name="shoppingCartItem">Shopping cart item</param>
        /// <returns>Shopping cart item weight</returns>
        decimal GetShoppingCartItemWeight(ShoppingCartItem shoppingCartItem);

        /// <summary>
        /// Gets shopping cart weight
        /// </summary>
        /// <param name="request">Request</param>
        /// <param name="includeCheckoutAttributes">A value indicating whether we should calculate weights of selected checkotu attributes</param>
        /// <returns>Total weight</returns>
        decimal GetTotalWeight(GetShippingOptionRequest request, bool includeCheckoutAttributes = true);

        /// <summary>
        /// Get dimensions of associated products (for quantity 1)
        /// </summary>
        /// <param name="shoppingCartItem">Shopping cart item</param>
        /// <param name="width">Width</param>
        /// <param name="length">Length</param>
        /// <param name="height">Height</param>
        void GetAssociatedProductDimensions(ShoppingCartItem shoppingCartItem,
            out decimal width, out decimal length, out decimal height);

        /// <summary>
        /// Get total dimensions
        /// </summary>
        /// <param name="packageItems">Package items</param>
        /// <param name="width">Width</param>
        /// <param name="length">Length</param>
        /// <param name="height">Height</param>
        void GetDimensions(IList<GetShippingOptionRequest.PackageItem> packageItems,
            out decimal width, out decimal length, out decimal height);

        /// <summary>
        /// Get the nearest warehouse for the specified address
        /// </summary>
        /// <param name="address">Address</param>
        /// <param name="warehouses">List of warehouses, if null all warehouses are used.</param>
        /// <returns></returns>
        Warehouse GetNearestWarehouse(Address address, IList<Warehouse> warehouses = null);

        /// <summary>
        /// Create shipment packages (requests) from shopping cart
        /// </summary>
        /// <param name="cart">Shopping cart</param>
        /// <param name="shippingAddress">Shipping address</param>
        /// <param name="storeId">Load records allowed only in a specified store; pass 0 to load all records</param>
        /// <returns>Shipment packages (requests)</returns>
        /// <param name="shippingFromMultipleLocations">Value indicating whether shipping is done from multiple locations (warehouses)</param>
        IList<GetShippingOptionRequest> CreateShippingOptionRequests(IList<ShoppingCartItem> cart,
            Address shippingAddress, int storeId, out bool shippingFromMultipleLocations);

        /// <summary>
        ///  Gets available shipping options
        /// </summary>
        /// <param name="cart">Shopping cart</param>
        /// <param name="shippingAddress">Shipping address</param>
        /// <param name="customer">Load records allowed only to a specified customer; pass null to ignore ACL permissions</param>
        /// <param name="allowedShippingRateComputationMethodSystemName">Filter by shipping rate computation method identifier; null to load shipping options of all shipping rate computation methods</param>
        /// <param name="storeId">Load records allowed only in a specified store; pass 0 to load all records</param>
        /// <returns>Shipping options</returns>
        //GetShippingOptionResponse GetShippingOptions(IList<ShoppingCartItem> cart, Address shippingAddress,
        //    Customer customer = null, string allowedShippingRateComputationMethodSystemName = "", int storeId = 0);

        /// <summary>
        /// Gets available pickup points
        /// </summary>
        /// <param name="address">Address</param>
        /// <param name="customer">Load records allowed only to a specified customer; pass null to ignore ACL permissions</param>
        /// <param name="providerSystemName">Filter by provider identifier; null to load pickup points of all providers</param>
        /// <param name="storeId">Load records allowed only in a specified store; pass 0 to load all records</param>
        /// <returns>Pickup points</returns>
        //GetPickupPointsResponse GetPickupPoints(Address address, Customer customer = null, string providerSystemName = null, int storeId = 0);

        #endregion
    }
}
