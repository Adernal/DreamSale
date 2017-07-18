using Denmakers.DreamSale.Model;
using Denmakers.DreamSale.Model.Stores;
using System.Collections.Generic;

namespace Denmakers.DreamSale.Services.Stores
{
    public partial interface IStoreMappingService
    {
        IList<StoreMapping> GetStoreMappings<T>(T entity) where T : BaseEntity, IStoreMappingSupported;
        StoreMapping GetStoreMappingById(int storeMappingId);
        int[] GetStoresIdsWithAccess<T>(T entity) where T : BaseEntity, IStoreMappingSupported;
        bool Authorize<T>(T entity) where T : BaseEntity, IStoreMappingSupported;
        bool Authorize<T>(T entity, int storeId) where T : BaseEntity, IStoreMappingSupported;

        void InsertStoreMapping(StoreMapping storeMapping);
        void InsertStoreMapping<T>(T entity, int storeId) where T : BaseEntity, IStoreMappingSupported;
        void UpdateStoreMapping(StoreMapping storeMapping);
        void DeleteStoreMapping(StoreMapping storeMapping);
    }
}
