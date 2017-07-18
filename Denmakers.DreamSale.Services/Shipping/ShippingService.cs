using Denmakers.DreamSale.Data.Infrastructure;
using Denmakers.DreamSale.Data.Repositories;
using Denmakers.DreamSale.Model.Shipping;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Denmakers.DreamSale.Services.Shipping
{
    public partial class ShippingService : IShippingService
    {
        #region Fields
        private readonly IRepository<Warehouse> _warehouseRepository;
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Ctor
        public ShippingService(IRepository<Warehouse> warehouseRepository, IUnitOfWork unitOfWork)
        {
            this._warehouseRepository = warehouseRepository;
            this._unitOfWork = unitOfWork;
        }
        #endregion

        #region Methods
        #region Warehouses
        /// <summary>
        /// Gets a warehouse by id
        /// </summary>
        /// <param name="warehouseId">The warehouse identifier</param>
        /// <returns>Warehouse</returns>
        public virtual Warehouse GetWarehouseById(int warehouseId)
        {
            if (warehouseId == 0)
                return null;

            return _warehouseRepository.GetById(warehouseId);
        }

        /// <summary>
        /// Gets all warehouses
        /// </summary>
        /// <returns>Warehouses</returns>
        public virtual IList<Warehouse> GetAllWarehouses()
        {
            var query = from wh in _warehouseRepository.Table
                        orderby wh.Name
                        select wh;
            var warehouses = query.ToList();
            return warehouses;
        }

        /// <summary>
        /// Inserts a warehouse
        /// </summary>
        /// <param name="warehouse">Warehouse</param>
        public virtual void InsertWarehouse(Warehouse warehouse)
        {
            if (warehouse == null)
                throw new ArgumentNullException("warehouse");

            _warehouseRepository.Insert(warehouse);

            _unitOfWork.Commit();
        }

        /// <summary>
        /// Updates the warehouse
        /// </summary>
        /// <param name="warehouse">Warehouse</param>
        public virtual void UpdateWarehouse(Warehouse warehouse)
        {
            if (warehouse == null)
                throw new ArgumentNullException("warehouse");

            _warehouseRepository.Update(warehouse);

            _unitOfWork.Commit();
        }

        /// <summary>
        /// Deletes a warehouse
        /// </summary>
        /// <param name="warehouse">The warehouse</param>
        public virtual void DeleteWarehouse(Warehouse warehouse)
        {
            if (warehouse == null)
                throw new ArgumentNullException("warehouse");

            _warehouseRepository.Delete(warehouse);

            _unitOfWork.Commit();
        }
        #endregion
        #endregion
    }
}
