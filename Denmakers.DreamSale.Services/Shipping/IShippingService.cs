using Denmakers.DreamSale.Model.Shipping;
using System.Collections.Generic;

namespace Denmakers.DreamSale.Services.Shipping
{
    public partial interface IShippingService
    {
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
    }
}
