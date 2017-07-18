
using Denmakers.DreamSale.Model.Stores;
using System.Collections.Generic;

namespace Denmakers.DreamSale.Services.Stores
{
    public partial interface IStoreService
    {
        /// <summary>
        /// Deletes a store
        /// </summary>
        /// <param name="store">Store</param>
        void DeleteStore(Store store);

        /// <summary>
        /// Gets all stores
        /// </summary>
        /// <returns>Stores</returns>
        IList<Store> GetAllStores();

        /// <summary>
        /// Gets a store 
        /// </summary>
        /// <param name="storeId">Store identifier</param>
        /// <returns>Store</returns>
        Store GetStoreById(int storeId);

        /// <summary>
        /// Inserts a store
        /// </summary>
        /// <param name="store">Store</param>
        void InsertStore(Store store);

        /// <summary>
        /// Updates the store
        /// </summary>
        /// <param name="store">Store</param>
        void UpdateStore(Store store);
    }
}
