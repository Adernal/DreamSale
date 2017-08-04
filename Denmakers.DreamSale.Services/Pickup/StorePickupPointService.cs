using Denmakers.DreamSale.Common;
using Denmakers.DreamSale.Data.Repositories;
using Denmakers.DreamSale.Model.Pickup;
using System;
using System.Linq;

namespace Denmakers.DreamSale.Services.Pickup
{
    public partial class StorePickupPointService : IStorePickupPointService
    {
        #region Fields

        //private readonly ICacheManager _cacheManager;
        private readonly IRepository<StorePickupPoint> _storePickupPointRepository;

        #endregion

        #region Ctor
        public StorePickupPointService(IRepository<StorePickupPoint> storePickupPointRepository)
        {
            this._storePickupPointRepository = storePickupPointRepository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets all pickup points
        /// </summary>
        /// <param name="storeId">The store identifier; pass 0 to load all records</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Pickup points</returns>
        public virtual IPagedList<StorePickupPoint> GetAllStorePickupPoints(int storeId = 0, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _storePickupPointRepository.Table;
            if (storeId > 0)
                query = query.Where(point => point.StoreId == storeId || point.StoreId == 0);
            query = query.OrderBy(point => point.Name);

            return new PagedList<StorePickupPoint>(query, pageIndex, pageSize);
        }

        /// <summary>
        /// Gets a pickup point
        /// </summary>
        /// <param name="pickupPointId">Pickup point identifier</param>
        /// <returns>Pickup point</returns>
        public virtual StorePickupPoint GetStorePickupPointById(int pickupPointId)
        {
            if (pickupPointId == 0)
                return null;

            return _storePickupPointRepository.GetById(pickupPointId);
        }

        /// <summary>
        /// Inserts a pickup point
        /// </summary>
        /// <param name="pickupPoint">Pickup point</param>
        public virtual void InsertStorePickupPoint(StorePickupPoint pickupPoint)
        {
            if (pickupPoint == null)
                throw new ArgumentNullException("pickupPoint");

            _storePickupPointRepository.Insert(pickupPoint);
        }

        /// <summary>
        /// Updates the pickup point
        /// </summary>
        /// <param name="pickupPoint">Pickup point</param>
        public virtual void UpdateStorePickupPoint(StorePickupPoint pickupPoint)
        {
            if (pickupPoint == null)
                throw new ArgumentNullException("pickupPoint");

            _storePickupPointRepository.Update(pickupPoint);
        }

        /// <summary>
        /// Deletes a pickup point
        /// </summary>
        /// <param name="pickupPoint">Pickup point</param>
        public virtual void DeleteStorePickupPoint(StorePickupPoint pickupPoint)
        {
            if (pickupPoint == null)
                throw new ArgumentNullException("pickupPoint");

            _storePickupPointRepository.Delete(pickupPoint);
        }

        #endregion
    }
}
