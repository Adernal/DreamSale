using Denmakers.DreamSale.Data.Infrastructure;
using Denmakers.DreamSale.Data.Repositories;
using Denmakers.DreamSale.Model.Stores;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Denmakers.DreamSale.Services.Stores
{
    public partial class StoreService : IStoreService
    {
        #region Fields
        protected readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Store> _storeRepository;

        #endregion

        #region Ctor

        public StoreService(IRepository<Store> storeRepository, IUnitOfWork unitOfWork)
        {
            this._storeRepository = storeRepository;
            //this._unitOfWork = unitOfWork;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Deletes a store
        /// </summary>
        /// <param name="store">Store</param>
        public virtual void DeleteStore(Store store)
        {
            if (store == null)
                throw new ArgumentNullException("store");

            var allStores = GetAllStores();
            if (allStores.Count == 1)
                throw new Exception("You cannot delete the only configured store");

            _storeRepository.Delete(store);
            //_unitOfWork.Commit();
        }

        /// <summary>
        /// Gets all stores
        /// </summary>
        /// <returns>Stores</returns>
        public virtual IList<Store> GetAllStores()
        {
            var query = from s in _storeRepository.Table
                        orderby s.DisplayOrder, s.Id
                        select s;
            var stores = query.ToList();
            return stores;
        }

        /// <summary>
        /// Gets a store 
        /// </summary>
        /// <param name="storeId">Store identifier</param>
        /// <returns>Store</returns>
        public virtual Store GetStoreById(int storeId)
        {
            if (storeId == 0)
                return null;

            return _storeRepository.GetById(storeId);
        }

        /// <summary>
        /// Inserts a store
        /// </summary>
        /// <param name="store">Store</param>
        public virtual void InsertStore(Store store)
        {
            if (store == null)
                throw new ArgumentNullException("store");

            _storeRepository.Insert(store);
            //_unitOfWork.Commit();
        }

        /// <summary>
        /// Updates the store
        /// </summary>
        /// <param name="store">Store</param>
        public virtual void UpdateStore(Store store)
        {
            if (store == null)
                throw new ArgumentNullException("store");

            _storeRepository.Update(store);
        }

        #endregion
    }
}
