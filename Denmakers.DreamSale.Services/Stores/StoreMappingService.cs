using System;
using Denmakers.DreamSale.Model;
using Denmakers.DreamSale.Model.Stores;
using System.Collections.Generic;
using Denmakers.DreamSale.Data.Repositories;
using System.Linq;
using Denmakers.DreamSale.Data.Infrastructure;

namespace Denmakers.DreamSale.Services.Stores
{
    public partial class StoreMappingService : IStoreMappingService
    {
        #region Fields
        private readonly IRepository<StoreMapping> _storeMappingRepository;
        protected readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Ctor
        public StoreMappingService(IRepository<StoreMapping> storeMappingRepository, IUnitOfWork unitOfWork)
        {
            this._storeMappingRepository = storeMappingRepository;
            //this._unitOfWork = unitOfWork;
        }
        #endregion

        #region Interface implementation
        public IList<StoreMapping> GetStoreMappings<T>(T entity) where T : BaseEntity, IStoreMappingSupported
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            int entityId = entity.Id;
            string entityName = typeof(T).Name;

            var query = from sm in _storeMappingRepository.Table
                        where sm.EntityId == entityId &&
                        sm.EntityName == entityName
                        select sm;
            var storeMappings = query.ToList();
            return storeMappings;
        }
        public StoreMapping GetStoreMappingById(int storeMappingId)
        {
            if (storeMappingId == 0)
                return null;

            return _storeMappingRepository.GetById(storeMappingId);
        }
        public int[] GetStoresIdsWithAccess<T>(T entity) where T : BaseEntity, IStoreMappingSupported
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            int entityId = entity.Id;
            string entityName = typeof(T).Name;

            var query = from sm in _storeMappingRepository.Table
                        where sm.EntityId == entityId &&
                        sm.EntityName == entityName
                        select sm.StoreId;
            return query.ToArray();
        }
        public bool Authorize<T>(T entity) where T : BaseEntity, IStoreMappingSupported
        {
            return Authorize(entity, 1);
        }
        public bool Authorize<T>(T entity, int storeId) where T : BaseEntity, IStoreMappingSupported
        {
            if (entity == null)
                return false;

            if (storeId == 0)
                //return true if no store specified/found
                return true;

            if (!entity.LimitedToStores)
                return true;

            foreach (var storeIdWithAccess in GetStoresIdsWithAccess(entity))
                if (storeId == storeIdWithAccess)
                    //yes, we have such permission
                    return true;

            //no permission found
            return false;
        }

        public void InsertStoreMapping(StoreMapping storeMapping)
        {
            if (storeMapping == null)
                throw new ArgumentNullException("storeMapping");

            _storeMappingRepository.Insert(storeMapping);
            //_unitOfWork.Commit();
        }
        public void InsertStoreMapping<T>(T entity, int storeId) where T : BaseEntity, IStoreMappingSupported
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            if (storeId == 0)
                throw new ArgumentOutOfRangeException("storeId");

            int entityId = entity.Id;
            string entityName = typeof(T).Name;

            var storeMapping = new StoreMapping
            {
                EntityId = entityId,
                EntityName = entityName,
                StoreId = storeId
            };

            InsertStoreMapping(storeMapping);
        }
        public void UpdateStoreMapping(StoreMapping storeMapping)
        {
            if (storeMapping == null)
                throw new ArgumentNullException("storeMapping");

            _storeMappingRepository.Update(storeMapping);
        }
        public void DeleteStoreMapping(StoreMapping storeMapping)
        {
            if (storeMapping == null)
                throw new ArgumentNullException("storeMapping");

            _storeMappingRepository.Delete(storeMapping);
            //_unitOfWork.Commit();
        }
        #endregion
    }
}
