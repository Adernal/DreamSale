using Denmakers.DreamSale.Model.Stores;

namespace Denmakers.DreamSale.Data.Context
{
    public interface IStoreContext
    {
        /// <summary>
        /// Gets or sets the current store
        /// </summary>
        Store CurrentStore { get; }
    }
}
